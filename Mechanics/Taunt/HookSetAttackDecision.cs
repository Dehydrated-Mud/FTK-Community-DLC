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


namespace CommunityDLC.Mechanics
{
    internal class HookSetAttackDecision : BaseModule
    {
        public override void Initialize()
        {
            On.EnemyDummy.SetAttackDecision += SetAttackDecisionHook;
        }

        private EnemyAttackDecision SetAttackDecisionHook(On.EnemyDummy.orig_SetAttackDecision orig, EnemyDummy self)
        {
            if (EncounterSession.Instance.AnyPlayersTaunting())
            {
                List<FTK_proficiencyTable.ID> tauntAbleIDs = NoAOEAttack(self);
                FTK_proficiencyTable.ID iD = FTK_proficiencyTable.ID.None;
                string text = "prof";
                /*if (self.m_EnemyCombat.m_UseFirstProfAsReg)
                {
                    iD = self.m_EventListener.m_Weapon.GetProficiencyIDs()[0];
                }*/
                if (tauntAbleIDs.Count > 0)
                {
                    Logger.LogInfo("Taunt-Compatible attack detected!");
                    text = "prof";
                    iD = tauntAbleIDs[UnityEngine.Random.Range(0, tauntAbleIDs.Count)];
                    Logger.LogInfo("Returning ability " + iD + " with attack type " + FTK_proficiencyTableDB.GetDB().GetEntry(iD).m_Target);
                }
                Logger.LogInfo("I am returning an attack of type: " + iD);
                self.m_EnemyAttackDecision = new EnemyAttackDecision(iD); //Must set the attribute at the same time as returning the decision because reconsiderAttack uses this attribute to determine whether or not to bypass the decision!
                return new EnemyAttackDecision(iD);
                EncounterSession.Instance.AddCombatEventToActiveLogEntry("[MODIFIED] " + self.GameLogID + " is attempting " + text + "(" + iD.ToString() + ")", _includeInOutputLog: true);

            }
            else
            {
                Logger.LogWarning("Nobody Taunting");
                return orig(self);
            }
        }
        internal List<FTK_proficiencyTable.ID> NoAOEAttack(EnemyDummy self)
        {
            Logger.LogInfo("Beginning Taunt Bypass");
            List<FTK_proficiencyTable.ID> proficiencyIDs = self.m_EventListener.m_Weapon.GetProficiencyIDs();
            List<FTK_proficiencyTable.ID> tauntAbleIDs = new List<FTK_proficiencyTable.ID>();
            foreach (FTK_proficiencyTable.ID ii in proficiencyIDs)
            {
                FTK_proficiencyTable entry3 = FTK_proficiencyTableDB.GetDB().GetEntry(ii);
                FTK_proficiencyTable knife = FTK_proficiencyTableDB.GetDB().GetEntry((FTK_proficiencyTable.ID)18);
                Logger.LogMessage("CATEGORY FOR KNIFE??????: " + knife.m_ProficiencyPrefab.m_Category.ToString());
                Logger.LogInfo("Found proficiency with attack type: " + entry3.m_Target);
                if (entry3.m_Target != CharacterDummy.TargetType.Splash & entry3.m_Target != CharacterDummy.TargetType.Aoe)
                {
                    tauntAbleIDs.Add(ii);
                }
            }
            return tauntAbleIDs;
        }

        public override void Unload()
        {
            On.EnemyDummy.SetAttackDecision -= SetAttackDecisionHook;
        }
    }
}
