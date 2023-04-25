using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FTKAPI.Objects.SkillHooks;
using UnityEngine;
using MonoMod.Cil;
using Mono.Cecil.Cil;
namespace CommunityDLC.PhotonHooks
{
    internal class HookInstantiate : BaseModule
    {
        public override void Initialize()
        {
            Unload();
            IL.PhotonNetwork.Instantiate_string_Vector3_Quaternion_byte_ObjectArray += InstantiateHook;
        }
        private void InstantiateHook(ILContext il)
        {
            ILCursor c = new ILCursor(il);
            c.GotoNext(
                x => x.MatchLdarg(0),
                x => true,
                x => true,
                x => true,
                x => true,
                x => x.MatchStloc(0)
                );
            c.Index += 6;
            c.Emit(OpCodes.Ldloc_0);
            c.EmitDelegate<Action<GameObject>>((_value) =>
            {
                _value.AddComponent<CustomCharacterStatsDLC>();
            });
        }
    }
}
