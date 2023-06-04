using FTKAPI.Objects;
using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GridEditor;
using CommunityDLC.Objects.CustomSkills;
using FTKAPI.Managers;

namespace CommunityDLC.Objects.CharacterSkills
{
    public class FindNook : FTKAPI_CharacterSkill
    {
        public FindNook() 
        {

            Name = new CustomLocalizedString("Passive Skill: Find Nook");
            Description = new CustomLocalizedString($"The Hobo is experienced in finding a place to sleep in even the most inhospitable environments. Chance to proc is based on all stats. Increased chance to proc when the weather is poor.");
            Trigger = TriggerType.EndTurn;

            int customSkill = SkillManager.AddSkill(new CustomSkill() { 
                ID = "FindNook",
                BaseSkill = FTK_characterSkill.ID.EnergyBoost,
                HudDisplay = new CustomLocalizedString("Find Nook")
            });

            SkillInfo = (FTK_characterSkill.ID)customSkill;
        }

        public override void Skill(CharacterOverworld _player, TriggerType _trig)
        {
            switch (_trig)
            {
                case TriggerType.EndTurn:

                    if (Proc(_player))
                    {
                        int heal = DLCUtils.HealByPercentage(_player.m_CurrentDummy, 0.2f);
                        _player.m_CharacterStats.GainSpecificHealth(heal);
                        if ((bool)_player.GetCurrentDummy())
                        {
                            _player.GetCurrentDummy().PlayCharacterAbilityEvent(SkillInfo);
                        }
                        else
                        {
                            _player.PlayCharacterAbilityEvent(SkillInfo);
                        }
                    }
                    break;
            }
        }

        private bool Proc(CharacterOverworld _player)
        {
            int fac = 2;

            if (!FTKHub.Instance.m_TimeOfDayProperties.IsItNight() && !GameLogic.Instance.m_WeatherManager.IsItBadWeather()) 
            {
                return false;
            }

            if (!_player.IsInDungeon() && (_player.IsInAir() || _player.IsInBoat() || _player.GetIsInPOI()))
            {
                return false;
            }

            if (GameLogic.Instance.m_WeatherManager.IsItBadWeather())
            {
                fac *= 2;
            }

            if (_player.m_CharacterStats.m_HealthCurrent / _player.m_CharacterStats.MaxHealth < 0.2f)
            {
                fac *= 2;
            }

            float num = Math.Min(0.5f, (float)fac * (_player.m_CharacterStats.Awareness * _player.m_CharacterStats.Fortitude * _player.m_CharacterStats.Vitality * _player.m_CharacterStats.Toughness * _player.m_CharacterStats.Talent * _player.m_CharacterStats.Luck));

            if (UnityEngine.Random.value < num)
            {
                return true;
            }
            return false;
        }
    }
}
