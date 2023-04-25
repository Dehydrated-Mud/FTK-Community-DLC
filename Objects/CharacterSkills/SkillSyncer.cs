using BepInEx.Logging;
using FTKAPI.Objects;
using Photon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CommunityDLC.Objects.SkillTree;
using Logger = FTKAPI.Utils.Logger;
using GridEditor;

namespace CommunityDLC.Objects.CharacterSkills
{
    public class SkillSyncer : PunBehaviour
    {
        public SkillSyncer() 
        {
            SkillContainer.Instance.Syncer = this;
            TreeManager.Instance.Syncer = this;
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
        public void SyncDiscipline(bool _proc)
        {
            photonView.RPC("SyncDisciplineRPC", PhotonTargets.All, _proc);
        }
        [PunRPC]
        public void SyncDisciplineRPC(bool _proc)
        {
            SkillContainer.Instance.discipline.proc = _proc;
        }
        public void SyncImpervious(FTKPlayerID player, int armor, int resist)
        {
            photonView.RPC("SyncImperviousRPC", PhotonTargets.All, new object[3]
            {
                player, armor, resist
            });
        }

        [PunRPC]
        public void SyncImperviousRPC(FTKPlayerID player, int armor, int resist)
        {
            CharacterOverworld _cow = FTKHub.Instance.GetCharacterOverworldByFID(player);
            CustomCharacterStats _cs = _cow.gameObject.GetComponent<CustomCharacterStats>();
            _cs.imperviousArmor = armor;
            _cs.imperviousResistance = resist;
        }

        public void AddRemoveModifier(FTKPlayerID player, FTK_characterModifier.ID id, bool adding = true)
        {
            photonView.RPC("AddRemoveModifierRPC", PhotonTargets.All, new object[3]
            {
                player, id, adding
            });
        }

        [PunRPC]
        public void AddRemoveModifierRPC(FTKPlayerID player, FTK_characterModifier.ID id, bool adding)
        {
            CharacterStats stats = FTKHub.Instance.GetCharacterOverworldByFID(player).m_CharacterStats;
            if (adding)
            {
                stats.AddCharacterMod(id);
            }
            else
            {
                stats.RemoveCharacterMod(id);
            }
        }
    }
}
