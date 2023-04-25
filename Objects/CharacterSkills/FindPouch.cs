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
    public class FindPouch : FTKAPI_CharacterSkill
    {
        int divisor;
        public FindPouch() 
        {
            divisor = 12;
            Name = new CustomLocalizedString("Passive Skill: Find Tinder Pouch");
            Description = new CustomLocalizedString($"(Vitality/{divisor})% chance to randomly find a tinder pouch at the end of the player's turn. 1/3 of that chance in dungeons.");
            Trigger = TriggerType.EndTurn;

            int customSkill = SkillManager.AddSkill(new CustomSkill() { 
                ID = "FindTinderPouch",
                BaseSkill = FTK_characterSkill.ID.FindHerb,
                HudDisplay = new CustomLocalizedString("Find Pouch")
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
                        _player.AddItemToBackpack(FTK_itembase.ID.conTinderbox);
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
            if (!_player.IsInDungeon() && (_player.IsInAir() || _player.IsInBoat() || _player.GetIsInPOI() || GameLogic.Instance.m_WeatherManager.IsItBadWeather()))
            {
                return false;
            }
            float num = _player.m_CharacterStats.Vitality / (float)divisor;
            bool endDungeon = false;
            if (_player.IsInDungeon())
            {
                MiniHexDungeon miniHexDungeon = (MiniHexDungeon)_player.GetPOI();
                num /= (float)miniHexDungeon.GetAlivePlayersInside().Count;
                endDungeon = miniHexDungeon.GetDungeonDefinition().m_IsEndDungeon;
            }
            if (UnityEngine.Random.value < num)
            {
                return true;
            }
            return false;
        }
    }
}
