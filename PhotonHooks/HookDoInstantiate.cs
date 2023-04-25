using System;
using FTKAPI.Objects.SkillHooks;
using FTKAPI.Objects;
using UnityEngine;
using MonoMod.Cil;
using Mono.Cecil.Cil;
using ExitGames.Client.Photon;
using Logger = FTKAPI.Utils.Logger;
using CommunityDLC.Objects.SkillTree;

namespace CommunityDLC.PhotonHooks
{
    internal class HookDoInstantiate : BaseModule
    {
        public override void Initialize()
        {
            Unload();
            On.NetworkingPeer.DoInstantiate += DoInstantiateHook;
        }

        private GameObject DoInstantiateHook(On.NetworkingPeer.orig_DoInstantiate orig, PhotonPeer self, Hashtable evData, PhotonPlayer photonPlayer, GameObject resourceGameObject)
        {
            //Logger.LogWarning("In DoInstatntiateHook");
            GameObject gameObject = orig(self, evData, photonPlayer, resourceGameObject);
            if (gameObject.GetComponent<CharacterOverworld>())
            {
                if (!gameObject.GetComponent<CustomCharacterStatsDLC>())
                {
                    gameObject.AddComponent<CustomCharacterStatsDLC>();
                }
                CharacterOverworld _char = gameObject.GetComponent<CharacterOverworld>();
                TreeManager.Instance.RegisterTree(_char.m_FTKPlayerID.m_TurnIndex, _char.m_CharacterStats.m_CharacterClass, _char.m_CharacterStats);
                Logger.LogInfo("Registering skill tree for player: " + _char.m_FTKPlayerID + " with character class: " + _char.m_CharacterStats.m_CharacterClass);
            }
            return gameObject;
        }

        public override void Unload()
        {
            On.NetworkingPeer.DoInstantiate -= DoInstantiateHook;
        }
    }
}
