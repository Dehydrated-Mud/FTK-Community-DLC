using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GridEditor;
using FTKAPI.Managers;
using FTKAPI.Objects;

namespace CommunityDLC.Objects.CharacterSkills
{
    internal class DrinkThat : FTKAPI_CharacterSkill
    {
        public DrinkThat() 
        {
            Name = new("Passive: You gonna drink that?");
            Description = new("Chance to randomly find spirits at the end of a turn.");
            Trigger = TriggerType.EndTurn;
            int customSkill = SkillManager.AddSkill(new CustomSkill()
            {
                ID = "AreYouGonnaDrinkThat",
                BaseSkill = FTK_characterSkill.ID.FindHerb,
                ProcSoundID = "Play_com_abi_encourage",
                HudDisplay = new CustomLocalizedString("Are you gonna drink that?")
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
                        FTK_itembase.ID item = DLCUtils.GetWeightedDropItemDirect(new List<FTK_itembase.ID> { FTK_itembase.ID.conRum, FTK_itembase.ID.conWine, FTK_itembase.ID.conMead, FTK_itembase.ID.conFahrulMule });
                        _player.AddItemToBackpack(item);
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
            float num = _player.m_CharacterStats.Luck / 4;
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
