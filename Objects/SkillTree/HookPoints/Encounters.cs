using Logger = FTKAPI.Utils.Logger;
using CommunityDLC.Objects.SkillTree.MileStones;
using FTKAPI.Objects.SkillHooks;
using GridEditor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommunityDLC.Objects.SkillTree.HookPoints
{
    public class HookEncounters : BaseModule
    {
        public override void Initialize()
        {
            On.MiniEncounter.UseMiniEncounterRPC += UseMiniEncounterRPCHook;
            //On.MiniHexUtility.OnUseStoneTable += OnUseStoneTableHook;
            On.EncounterSessionMC._getEnemiesDrops += EnemiesListHook;
        }
        public override void Unload()
        {
            On.MiniEncounter.UseMiniEncounterRPC -= UseMiniEncounterRPCHook;
            On.EncounterSessionMC._getEnemiesDrops -= EnemiesListHook;
        }
        
        private string[] EnemiesListHook(On.EncounterSessionMC.orig__getEnemiesDrops _orig, EncounterSessionMC _this)
        {
            bool wasSolo = _this.m_AllCombtatants.Length == 1;
            //Get IDs of all enemies in combat
            List<FTK_enemyCombat.ID> enemies = _this.m_EnemyStatuses.Values.Select(e => (FTK_enemyCombat.ID)Enum.Parse(typeof(FTK_enemyCombat.ID), e.m_EnemyType, ignoreCase: true)).ToList();
            if (enemies.Count > 0)
            {
                Logger.LogWarning(enemies[0]);
            }
            MileStoneContainer milestone = new MileStoneContainer
            {
                Solo = wasSolo,
                Enemies = enemies
            };
            // Only send the milestone to living players
            if (_this.m_AllCombtatantsAlive.Count > 0)
            {
                foreach (FTKPlayerID player in _this.m_AllCombtatantsAlive)
                {
                    TreeManager.Instance.m_modTrees[player.TurnIndex].Achieved(milestone);
                }
            }
            
            return _orig(_this);
        }
        private void UseMiniEncounterRPCHook(On.MiniEncounter.orig_UseMiniEncounterRPC orig, MiniEncounter _this, FTKPlayerID _player, bool _fx, bool _consume, bool _success)
        {
            orig(_this, _player, _fx, _consume, _success);
            if (_success)
            {
                EncounterMilestone container = new EncounterMilestone(_this.GetDBEntry().m_ID);
                int player = _player.TurnIndex;
                ModifierTree modifierTree = TreeManager.Instance.m_modTrees[player];
                Logger.LogInfo($"Sending encounter milestone for Player {player} who has passed the {_this.GetDBEntry().m_ID} encounter with success {_success}");
                modifierTree.Achieved(container);
            }
        }
        private void OnUseStoneTableHook(On.MiniHexUtility.orig_OnUseStoneTable orig, MiniHexUtility _this, CharacterOverworld player)
        {
            orig(_this, player);
            int playerID = player.m_FTKPlayerID.TurnIndex;
            ModifierTree tree = TreeManager.Instance.m_modTrees[playerID];
            tree.Achieved(new EncounterMilestone(FTK_miniEncounter.ID.BloodRitual));
        }
    }
}
