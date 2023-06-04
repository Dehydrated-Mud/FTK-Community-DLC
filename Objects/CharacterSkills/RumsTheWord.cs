using FTKAPI.APIs.BattleAPI;
using FTKAPI.Objects;
using FTKAPI.Managers;
using GridEditor;
using Google2u;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommunityDLC.Objects.CharacterSkills
{
    internal class RumsTheWord : FTKAPI_CharacterSkill
    {
        public RumsTheWord() 
        {
            Name = new("Rum's the Word");
            Description = new("Chance to apply a random spirit to the team at the beginning of combat.");
            SkillInfo = (FTK_characterSkill.ID)SkillManager.AddSkill(new CustomSkill(FTK_characterSkill.ID.EnergyBoost)
            {
                HudDisplay = Name,
                ID = "RumsTheWord"
            }) ;
        }

        public override void Skill(CharacterOverworld _cow, Query query)
        {
            switch(query)
            {
                case Query.StartCombatTurn:
                    CharacterDummy dummy = _cow.GetCombatDummy();
                    if (dummy)
                    {
                        if (!dummy.m_HadTurnThisSession)
                        {
                            List<CharacterDummy> otherCombatPlayerMembers = EncounterSession.Instance.GetOtherCombatPlayerMembers(dummy);
                            List<CharacterDummy> aliveCombatEnemies = EncounterSession.Instance.GetAliveCombatEnemies();
                            float num = 1f;
                            if (otherCombatPlayerMembers.Count == 0)
                            {
                                num *= 2f;
                            }
                            if (_cow.m_CharacterStats.GetHealthPercent() < 0.5f)
                            {
                                num *= 2f;
                            }
                            foreach (EnemyDummy item in aliveCombatEnemies)
                            {
                                if (item.m_EnemyCombat.m_IsBoss || item.m_EnemyCombat.m_IsScourge || _cow.m_CharacterStats.m_PlayerLevel < item.m_EnemyCombat.GetEnemyLevelDisplay())
                                {
                                    num *= 2f;
                                }
                            }

                            if (UnityEngine.Random.value < num)
                            {

                                foreach (CharacterDummy ally in EncounterSession.Instance.m_PlayerDummies.Values)
                                {
                                    string dispSpirit = String.Empty;
                                    ally.AddProfToDummy(ChoseSpirit(ref dispSpirit), true, true);
                                    ally.PlayCharacterAbilityEvent(SkillInfo);
                                    ally.SpawnHudText(dispSpirit);
                                }
                            }
                        }
                    }
                    break;
            }
        }

        private FTK_proficiencyTable.ID[] ChoseSpirit(ref string _name)
        {
            List<string> names = new List<string> { FTKHub.Localized<TextItems>("STR_conRum"), FTKHub.Localized<TextItems>("STR_conWine"), FTKHub.Localized<TextItems>("STR_conFahrulMule"), FTKHub.Localized<TextItems>("STR_conMead") };
            FTK_proficiencyTable.ID[] rum = { FTK_proficiencyTable.ID.rumArmorUp, FTK_proficiencyTable.ID.rumResistUp, FTK_proficiencyTable.ID.hilResConfuse };
            FTK_proficiencyTable.ID[] reserve = { FTK_proficiencyTable.ID.hilResAttackUp, FTK_proficiencyTable.ID.hilResConfuse };
            FTK_proficiencyTable.ID[] mule = { FTK_proficiencyTable.ID.orbAttackUp };
            FTK_proficiencyTable.ID[] mead = { FTK_proficiencyTable.ID.enProtectSelf };
            List<FTK_proficiencyTable.ID[]> choseFrom = new List<FTK_proficiencyTable.ID[]> { rum, reserve, mule, mead };
            int chosenIndex = UnityEngine.Random.Range(0, names.Count);
            _name = names[chosenIndex];
            return choseFrom[chosenIndex];
        }
    }
}
