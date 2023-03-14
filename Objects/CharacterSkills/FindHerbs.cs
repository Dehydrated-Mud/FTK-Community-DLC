using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FTKAPI.Objects.SkillHooks;
using MonoMod.Cil;
using Mono.Cecil.Cil;
using Logger = FTKAPI.Utils.Logger;


namespace CommunityDLC.Objects.CharacterSkills
{
    public class FindHerbs : BaseModule
    {
        public override void Initialize()
        {
            Unload();
            On.CharacterSkills.FindHerb += FindHerbHook;
        }

        internal bool FindHerbHook(On.CharacterSkills.orig_FindHerb _orig, CharacterOverworld _player)
        {
            bool proc = _orig(_player);
            bool isInfinite = false;
            if (_player.IsInDungeon())
            {
                MiniHexDungeon miniHexDungeon = (MiniHexDungeon)_player.GetPOI();
                isInfinite = miniHexDungeon.GetDungeonDefinition().IsInfinite();
            }
            GameFlow.Instance.UpdateFindHerbRoundCoolDown(_value: !isInfinite);
            return proc;
        }

        public override void Unload()
        {
            On.CharacterSkills.FindHerb -= FindHerbHook;
        }
    }
}
