using FTKAPI.Objects;
using GridEditor;
using Logger = FTKAPI.Utils.Logger;
using CommunityDLC.Objects.Proficiencies;
using FTKAPI.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CombatAnimTrigger = CharacterEventListener.CombatAnimTrigger;
using FTKAPI.APIs.BattleAPI;

namespace CommunityDLC.Objects.CharacterSkills
{
    using ProficiencyManager = FTKAPI.Managers.ProficiencyManager;
    public class CombatMeditation : FTKAPI_CharacterSkill
    {
        private Dictionary<FTKPlayerID, bool> m_Cooldowns = new Dictionary<FTKPlayerID, bool>();
        public CombatMeditation() 
        {
            Trigger = TriggerType.ProfButton | TriggerType.AnimOverride | TriggerType.StartCombatTurn;
            Name = new CustomLocalizedString("Skill: Combat Meditation");
            Description = new CustomLocalizedString($"Can voluntarily put oneself into a petrified state, rejuvinating health and focus in combat. {Environment.NewLine} On returning to combat the player is guaranteed a critical hit. {Environment.NewLine} Removes up to one poison and one disease.");
            SkillInfo = FTK_characterSkill.ID.Discipline;

            FTK_proficiencyTable.ID prof = (FTK_proficiencyTable.ID)ProficiencyManager.Instance.enums["CustomCombatMeditationProf"];

            SpecialProf = new ProfInfoContainer
            {
                //ByPassSlots = true,
                AttackProficiency = prof,
                AttackAnim = CharacterDummy.AttackAnim.DirectAttack,
                OverrideAttackAnim = true,
                CategoryDescription = new CustomLocalizedString("Meditate").GetLocalizedString(),
                OverrideSkillCheck = FTK_weaponStats2.SkillType.fortitude
            };    
            
        }
        public override void Skill(CharacterOverworld _cow, TriggerType _trig)
        {
            switch(_trig)
            {
                case TriggerType.StartCombatTurn:
                    
                    CharacterDummy characterDummy = _cow.GetCurrentDummy();

                    if (characterDummy.Petrified)
                    {
                        characterDummy.SpawnHudText("Combat Meditation");
                        int healAmount = DLCUtils.HealByPercentage(characterDummy, 0.1f);
                        if (healAmount > 0)
                        {
                            int newHealth = characterDummy.GetCurrentHealth() + healAmount;
                            _cow.m_CharacterStats.SetSpecificHealth(newHealth, true);
                        }
                        _cow.m_CharacterStats.UpdateFocusPoints(1);
                    }
                    break;
            }
        }
        /*public override ProfInfoContainer GetProfs(CharacterOverworld _player, TriggerType _trig, Weapon _weapon)
        {
            Logger.LogWarning("Combat Meditation: Entered GetProfs Method");
            switch (_trig)
            {
                case TriggerType.ProfButton:
                    FTKPlayerID id = _player.m_FTKPlayerID;
                    if (!m_Cooldowns.ContainsKey(id))
                    {
                        Logger.LogWarning($"Combat Meditation: Registering player {id.TurnIndex}");
                        m_Cooldowns[id] = false;
                    }
                    if (!m_Cooldowns[id])
                    {
                        //m_Cooldowns[id] = true;
                        return SpecialProf;
                    }
                    break;
                case TriggerType.AnimOverride:
                    return SpecialProf;
            }
            Logger.LogWarning("Combat Meditation: Player did not meet requirements, returning empty list");
            return new ProfInfoContainer();
        }*/

        public override void Skill(CharacterOverworld _player, Query query)
        {
            Logger.LogInfo($"Combat meditation recived query {query} from BattleAPI");
            switch(query)
            {
                case Query.StartCombatTurn:

                    FTKPlayerID id = _player.m_FTKPlayerID;
                    if (!m_Cooldowns.ContainsKey(id))
                    {
                        Logger.LogWarning($"Combat Meditation: Registering player {id.TurnIndex}");
                        m_Cooldowns[id] = false;
                    }

                    ProfInfoContainer newContainer = new ProfInfoContainer(SpecialProf)
                    {
                        IsCoolingDown = m_Cooldowns[id],
                    };
                    BattleAPI.Instance.SendProfInfo(newContainer);
                    
                    break;
            }
        }
    }
}
