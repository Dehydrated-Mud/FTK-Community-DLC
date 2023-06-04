using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GridEditor;
using FTKAPI.Managers;
using FTKAPI.Objects;

namespace CommunityDLC.Objects.CharacterSkills
{
    internal class EatThat : FTKAPI_CharacterSkill
    {
        public EatThat() 
        {
            Name = new("Passive: You gonna eat that?");
            Description = new("Chance to randomly find snickerdoodles at the end of a turn.");
            Trigger = TriggerType.EndTurn;
            int customSkill = SkillManager.AddSkill(new CustomSkill()
            {
                ID = "AreYouGonnaEatThat",
                BaseSkill = FTK_characterSkill.ID.FindHerb,
                HudDisplay = new CustomLocalizedString("Are you gonna eat that?"),
                ProcSoundID = "Play_exp_abi_find_herb"
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
                        if ((bool)_player.GetCurrentDummy())
                        {
                            _player.GetCurrentDummy().PlayCharacterAbilityEvent(SkillInfo);
                        }
                        else
                        {
                            _player.PlayCharacterAbilityEvent(SkillInfo);
                        }
                        for (int i = 0; i < UnityEngine.Random.Range(1, 5); i++)
                        {
                            _player.AddItemToBackpack(FTK_itembase.ID.conCookie);
                        }
                    }
                    break;
            }
        }

        private bool Proc(CharacterOverworld _player)
        {
            if (!_player.IsInDungeon() && (_player.IsInAir() || _player.IsInBoat() || _player.GetIsInPOI() || GameLogic.Instance.m_WeatherManager.IsItBadWeather()))
            {
                return false;
            }
            float num = _player.m_CharacterStats.Luck / 3;
            if (_player.IsInDungeon())
            {
                MiniHexDungeon miniHexDungeon = (MiniHexDungeon)_player.GetPOI();
                num /= (float)miniHexDungeon.GetAlivePlayersInside().Count; 
            }
            if (UnityEngine.Random.value < num)
            {
                return true;
            }
            return false;
        }
    }
}
