using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GridEditor;
using FTKAPI.Objects;
using FTKAPI.Managers;
using CommunityDLC.UIElements;
using BepInEx.Logging;
using Logger = FTKAPI.Utils.Logger;

namespace CommunityDLC.Objects.CharacterSkills
{
    internal class FreeInn : FTKAPI_CharacterSkill
    {
        public FreeInn() 
        {
            Trigger = TriggerType.EndTurn;
            Name = new("On the House");
            Description = new("Gain health and focus when stopping by a town based on talent. Must be night, or the hero must be in dire need.");
            SkillInfo = (FTK_characterSkill.ID)SkillManager.AddSkill(new CustomSkill(FTK_characterSkill.ID.Refocus)
            {
                ID = "PeoplesChampion",
                HudDisplay = new("On the House!")
            });
        }

        public override void Skill(CharacterOverworld _player, TriggerType _trig)
        {
            switch(Trigger)
            {
                case TriggerType.EndTurn:
                    if (Proc(_player))
                    {

                        float scaledTalent = Math.Max(0,_player.m_CharacterStats.Talent - 0.7f)/0.25f;
                        float healPercent = scaledTalent * 0.25f * Bonus(_player);
                        int amount = DLCUtils.HealByPercentage(_player.m_CurrentDummy, healPercent);
                        if ((bool)_player.GetCurrentDummy())
                        {
                            _player.GetCurrentDummy().PlayCharacterAbilityEvent(SkillInfo);
                        }
                        else
                        {
                            _player.PlayCharacterAbilityEvent(SkillInfo);
                        }
                        _player.m_CharacterStats.GainSpecificHealth(amount);
                        _player.m_CharacterStats.UpdateFocusPoints(1);
                    }
                    break;
            }
            
        }
        private bool Proc(CharacterOverworld _player)
        {
            if (_player.IsInAir() || _player.IsInBoat() || _player.IsInDungeon())
            {
                return false;
            }
            if (Bonus(_player) == 0) 
            {
                return false;
            }
            bool nearTown = false;
            List<HexLand> list = new List<HexLand>();
            _player.m_HexLand.GetRange(1, HexLand.SelectType.Land, list);
            foreach (HexLand item in list)
            {
                if (!item.m_POI || item.m_POI.HasEncounterQuest())
                {
                    continue;
                }
                if (item.m_POI is MiniHexTown)
                {
                    nearTown = true;
                    break;
                }
            }

            return nearTown;

        }

        private float Bonus(CharacterOverworld _player)
        {
            float bonus = 0f;
            if (FTKHub.Instance.m_TimeOfDayProperties.IsItNight())
            {
                bonus += 1f;
            }

            if (GameLogic.Instance.m_WeatherManager.IsItBadWeather())
            {
                bonus += 0.5f;
            }
            if (_player.m_CharacterStats.m_PoisonLvl > 0)
            {
                bonus += 0.5f;
            }
            if (_player.m_CurrentDummy != null)
            {
                if (DLCUtils.GetDummyHealthPercent(_player.m_CurrentDummy) < 0.5f)
                {
                    bonus += 0.5f;
                }
            }
            return bonus;
        }
    }
}
