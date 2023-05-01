using FTKAPI.APIs.BattleAPI;
using FTKAPI.Objects;
using GridEditor;
using HarmonyLib;
using SimpleBindDemo;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Logger = FTKAPI.Utils.Logger;

namespace CommunityDLC.Objects.CharacterSkills
{
    public class FocusHealer : FTKAPI_CharacterSkill
    {
        private CustomLocalizedString focusHealedTextSpawn = new("Focus Healed");
        private SpecialCombatAction specialAction = new SpecialCombatAction
        {
            Taunt = true
        };
        public FocusHealer() 
        {
            Trigger = TriggerType.AnyLandedAttack | TriggerType.EndTurn | TriggerType.ConvertFocusToAction | TriggerType.RollSlots;
            Name = new CustomLocalizedString("Passive Skill: Focus Healer");
            Description = new CustomLocalizedString("If this player has used focus at some point in their turn in the overworld, then at the end of their turn ally's within 2 hexes will receive healing. In combat, the party is healed if this player uses focus on an attack and that attack is not dodged.");
        }

        public override void Skill(CharacterOverworld _cow, Query query)
        {
            switch(query)
            {
                case Query.StartCombat:
                    if (_cow.GetCurrentDummy()) 
                    {
                        BattleAPI.Instance.DeclareSpecialAction(_cow.GetCurrentDummy(), specialAction);
                    }
                    break;
            }
        }
        public override void Skill(CharacterOverworld _player, TriggerType _trig, AttackAttempt _atk)
        {
            //Logger.LogWarning("Attempting combat focus heal");
            switch (_trig)
            {
                case TriggerType.AnyLandedAttack:
                    if (_atk.m_AttackFocused > 0)
                    {
                        int _newHealth = 0;
                        List<CharacterDummy> CombatPlayerMembers = EncounterSession.Instance.GetOtherCombatPlayerMembers(_player.m_CurrentDummy);
                        CombatPlayerMembers = CombatPlayerMembers.AddItem(_player.m_CurrentDummy).ToList();
                        foreach(CharacterDummy characterDummy in CombatPlayerMembers) 
                        {
                            int healAmount = DLCUtils.HealByPercentage(_player.m_CurrentDummy, 0.08f);
                            _newHealth = _player.m_CurrentDummy.GetCurrentHealth() + healAmount;
                            characterDummy.SpawnHudText("Focus Healing +" + healAmount + "HP", string.Empty);
                            characterDummy.m_CharacterOverworld.m_CharacterStats.SetSpecificHealth(_newHealth, true);
                            if (characterDummy != _player.m_CurrentDummy)
                            {
                                characterDummy.PlayCharacterAbilityEvent(FTK_characterSkill.ID.None);
                            }
                        }
                    }
                    break;
            }
        }
        public override void Skill(CharacterOverworld _player, TriggerType _trig)
        {
            Logger.LogWarning("Attempting end turn focus heal");
            float _conv = 2.89f;
            switch (_trig) 
            {
                case TriggerType.RollSlots:
                    if (_player.m_CharacterStats.SpentFocus > 0 && !proc)
                    {
                        proc = true;
                    }
                    break;
                case TriggerType.ConvertFocusToAction:
                    if (_player.m_CharacterStats.m_FocusPoints > 0 && _player.m_CharacterStats.m_ActionPoints < 9 && !proc)
                    {
                        proc = true;
                    }
                    break;
                case TriggerType.EndTurn:
                    if (proc && !_player.m_CharacterStats.m_IsInCombat && !_player.IsInDungeon())
                    {
                        Logger.LogWarning("Met Criteria");
                        Logger.LogWarning(FTKHub.Instance.m_CharacterOverworlds.Count);
                        foreach (CharacterOverworld characterOverworld in FTKHub.Instance.m_CharacterOverworlds)
                        {
                            float magnitude = (_player.transform.position - characterOverworld.transform.position).magnitude;
                            //Logger.LogWarning("Distance to target is: " + magnitude);
                            if (magnitude <= 2f * _conv)
                            {
                                int healAmount = DLCUtils.HealByPercentage(characterOverworld.m_CurrentDummy, 0.15f);
                                int _newHealth = characterOverworld.m_CurrentDummy.GetCurrentHealth() + healAmount;
                                if (healAmount > 0)
                                {
                                    characterOverworld.SpawnHudText(focusHealedTextSpawn.GetLocalizedString() + " +" + healAmount + "HP", string.Empty);
                                    characterOverworld.m_CharacterStats.SetSpecificHealth(_newHealth, true);
                                    characterOverworld.PlayCharacterAbilityEvent(FTK_characterSkill.ID.None);
                                }
                            }
                        }
                    }
                    proc = false;
                    break;
            }
        }
    }
}
