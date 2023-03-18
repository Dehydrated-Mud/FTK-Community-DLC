using BepInEx.Logging;
using Photon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Logger = FTKAPI.Utils.Logger;
namespace CommunityDLC.Objects.CharacterSkills
{
    public class SkillSyncer : PunBehaviour
    {
        public SkillSyncer() 
        {
            SkillContainer.Instance.Syncer = this;
            Logger.LogWarning("Creating skill syncer, claiming container");
        }
        public void SyncDivine(bool _proc)
        {
            photonView.RPC("SyncDivineRPC", PhotonTargets.All, _proc);
        }
        [PunRPC]
        public void SyncDivineRPC(bool _proc)
        {
            SkillContainer.Instance.divine.proc = _proc;
        }
    }
}
