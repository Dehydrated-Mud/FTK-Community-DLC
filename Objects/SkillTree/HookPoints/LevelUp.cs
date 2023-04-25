using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FTKAPI.Objects.SkillHooks;
using MonoMod.Cil;
using Mono.Cecil.Cil;
using UnityEngine;
using Logger = FTKAPI.Utils.Logger;

namespace CommunityDLC.Objects.SkillTree
{
    public class HookLevelUp : BaseModule
    {
        public override void Initialize()
        {
            IL.CharacterStats.Update += UpdateHook;    
        }
        private void UpdateHook(ILContext il)
        {
            ILCursor c = new ILCursor(il);
            c.GotoNext(
                x => x.MatchLdarg(0),
                x => x.MatchLdloc(0),
                x => x.MatchStfld<CharacterStats>("m_PlayerLevel")
                ) ;
            c.Index += 3;
            c.Emit(OpCodes.Ldarg_0);
            c.EmitDelegate<Action<CharacterStats>>((_this) =>
            {
                
                ModifierTree skillTree = null;
                try
                {
                    skillTree = TreeManager.Instance.m_modTrees[_this.m_CharacterOverworld.m_FTKPlayerID.m_TurnIndex];
                }
                catch(KeyNotFoundException e) 
                {
                    Logger.LogError(e.Message);
                    Logger.LogError("Could not find a Skill Tree in the dictionary for player: " + _this.m_CharacterOverworld.m_FTKPlayerID);
                }
                
                if (skillTree != null)
                {
                    MileStoneContainer mileStone = new MileStoneContainer() { Level = _this.m_PlayerLevel };
                    skillTree.Achieved(mileStone);
                }
                else
                {
                    Logger.LogError("Cannot send milestone, no Skill tree was found for " + _this.m_CharacterOverworld.m_PlayerName);
                }
            });
        }
        public override void Unload()
        {
            IL.CharacterStats.Update -= UpdateHook;
        }
    }
}
