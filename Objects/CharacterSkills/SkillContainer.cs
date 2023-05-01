using CommunityDLC.Mechanics;
using FTKAPI.Managers;
using FTKAPI.Objects;
using Photon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Logger = FTKAPI.Utils.Logger;
namespace CommunityDLC.Objects.CharacterSkills
{
    public class SkillContainer
    {
        public static SkillContainer instance;
        public static SkillContainer Instance
        {
            get
            {
                if (instance == null)
                {
                    Logger.LogWarning("No SkillContainer instance found, initializing instance...");
                    instance = new SkillContainer();
                }
                return instance;
            }
        }
        internal SkillSyncer skillSyncer;
        public FTKAPI_CharacterSkill focusHealer;
        public FTKAPI_CharacterSkill divine;
        public FTKAPI_CharacterSkill findPouch;
        public FTKAPI_CharacterSkill justiceHeavyDamage;
        public FTKAPI_CharacterSkill groupMeditate;
        public FTKAPI_CharacterSkill discipline;
        public FTKAPI_CharacterSkill combatMeditation;
        public FTKAPI_CharacterSkill bloodRush;
        public FTKAPI_CharacterSkill steadfast;
        public FTKAPI_CharacterSkill crushingBlow;
        public FTKAPI_CharacterSkill bluntForceTrauma;
        public FTKAPI_CharacterSkill rebuttal;
        public FTKAPI_CharacterSkill alwaysPrepared;
        public SkillContainer() 
        {
            Reset();
        }
        public void Reset()
        {
            SkillManager.Instance.successfulLoads = 0;
            SkillManager.Instance.enums.Clear();
            SkillManager.Instance.customDictionary.Clear();
            SkillManager.Instance.moddedDictionary.Clear();
            focusHealer = new FocusHealer();
            divine = new DivineIntervention(this);
            findPouch = new FindPouch();
            justiceHeavyDamage = new JusticeHeavyDamage();
            groupMeditate = new GroupMeditation();
            discipline = new Discipline();
            combatMeditation = new CombatMeditation();
            bloodRush = new RushPlus();
            steadfast = new Steadfast();
            crushingBlow = new CrushingBlow();
            bluntForceTrauma = new BluntForceTrauma();
            rebuttal = new BlockReflect();
            alwaysPrepared = new AlwaysPrepared();
        }
        public void SyncDivine(bool _proc)
        {
            Syncer.SyncDivine(_proc);
        }
        public void SyncDiscipline(bool _proc)
        {
            Syncer.SyncDiscipline(_proc);
        }
        public SkillSyncer Syncer
        {
            get => this.skillSyncer;
            set => this.skillSyncer = value;
        }
    }
}
