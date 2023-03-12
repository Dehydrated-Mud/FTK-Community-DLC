using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using IL;
using Mono.Cecil.Cil;
using MonoMod.Cil;
using MonoMod.RuntimeDetour;
using MonoMod.Utils;
using FTKAPI.Objects.SkillHooks;
using Logger = FTKAPI.Utils.Logger;
using UnityEngine;
using GridEditor;

namespace CommunityDLC.Mechanics.Taunt
{
    internal class HookApplySlotCombatAction : BaseModule
    {
        public static HookApplySlotCombatAction instance;
        public static HookApplySlotCombatAction Instance
        {
            get
            {
                if(instance == null)
                {
                    instance= new HookApplySlotCombatAction();
                }
                return instance;
            }
        }
        public override void Initialize()
        {
            Unload();
            IL.SlotControl.ApplySlotCombatAction += ApplySlotCombatActionHook;
        }
        private void ApplySlotCombatActionHook(ILContext il)
        {

            ILCursor c = new ILCursor(il);
            c.GotoNext(
                x => x.MatchLdarg(0),//SlotControl
                x => x.MatchLdarg(1),//_cow
                x => x.MatchLdarg(3)//_slotsuccess
                );
            c.Index += 4;
            c.RemoveRange(2);
            c.EmitDelegate<Action<SlotControl, CharacterOverworld, int, int>>((_this, _cow, _slotSuccess, _slotAmount) =>
            {
                if(_slotSuccess == 0)
                {
                    _this.CalculateShieldTaunt(_cow, false);
                }
                else if(_slotSuccess == _slotAmount) 
                {
                    _this.GetDummy(_cow).AddProfToDummy(new FTK_proficiencyTable.ID[1] { (FTK_proficiencyTable.ID)Prof }, _fx: false, _hud: false);
                    _this.GetDummy(_cow).PlayCharacterAbilityEventRPC(FTK_characterSkill.ID.ShieldTaunt);
                    _this.BypassCombatSlotsRPC(_this.GetDummy(_cow).FID, _resetSlots: false, _leaveCombat: false, _alternateAttack: false);
                }
                else if((float)_slotSuccess/(_slotAmount) >= 0.5f) 
                {
                    _this.CalculateShieldTaunt(_cow, true);
                }
            });
        }
        public override void Unload()
        {
            IL.SlotControl.ApplySlotCombatAction -= ApplySlotCombatActionHook;
        }
    }
}
