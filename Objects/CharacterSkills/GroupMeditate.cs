using FTKAPI.Objects;
using GridEditor;
using HarmonyLib;
using SimpleBindDemo;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FTKAPI.Managers;

using Logger = FTKAPI.Utils.Logger;

namespace CommunityDLC.Objects.CharacterSkills
{
    public class GroupMeditation : FTKAPI_CharacterSkill
    {
        FTK_characterSkill.ID groupMeditate = FTK_characterSkill.ID.None;
        public GroupMeditation() 
        {
            Trigger = TriggerType.EndTurn;
            Name = new CustomLocalizedString("Passive Skill: Group Meditation");
            Description = new CustomLocalizedString("Small chance, based on player's vitality and awareness, for the player and any ally within one hex to regain 2 focus on turn end.");
            groupMeditate = (FTK_characterSkill.ID)SkillManager.AddSkill(new CustomSkill(FTK_characterSkill.ID.Refocus) 
            {
                HudDisplay = new CustomLocalizedString("Group Meditation"),
            });
        }
        public override void Skill(CharacterOverworld _player, TriggerType _trig)
        {
            Logger.LogInfo("Attempting end turn group meditation");
            float _conv = 2.89f;
            switch (_trig) 
            {
                case TriggerType.EndTurn:
                    if (!_player.m_CharacterStats.m_IsInCombat)
                    {
                        float baseProbability = _player.m_CharacterStats.m_AugmentedAwareness * _player.m_CharacterStats.m_AugmentedFortitude;
                        float factor = _player.IsInDungeon()? 6 : 3;
                        
                        //Logger.LogWarning("Met Criteria");
                        //Logger.LogWarning(FTKHub.Instance.m_CharacterOverworlds.Count);
                        foreach (CharacterOverworld characterOverworld in FTKHub.Instance.m_CharacterOverworlds)
                        {
                            float magnitude = (_player.transform.position - characterOverworld.transform.position).magnitude;
                            Logger.LogWarning("Distance to target is: " + magnitude);
                            if (UnityEngine.Random.value < baseProbability/factor && (magnitude <= _conv || _player.IsInDungeon()))
                            {
                                int addFocus = GetFocus(characterOverworld, 2);
                                if (addFocus > 0)
                                {
                                    if (characterOverworld.GetCurrentDummy())
                                    {
                                        characterOverworld.GetCurrentDummy().PlayCharacterAbilityEvent(groupMeditate);
                                        characterOverworld.GetCurrentDummy().SpawnHudTextRPC($"+{addFocus} Focus");
                                    }
                                    else
                                    {
                                        characterOverworld.PlayCharacterAbilityEvent(groupMeditate);
                                        characterOverworld.SpawnHudTextRPC($"+{addFocus} Focus");
                                    }
                                    characterOverworld.m_CharacterStats.UpdateFocusPoints(addFocus, true);

                                    
                                }
                            }
                        }
                    }
                    break;
            }
        }
        internal int GetFocus(CharacterOverworld player, int max)
        {
            return Math.Min(2, Math.Max(0,player.m_CharacterStats.MaxFocus - player.m_CharacterStats.m_FocusPoints));
        }
    }
}
