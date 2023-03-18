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
        }
        public void SyncDivine(bool _proc)
        {
            Syncer.SyncDivine(_proc);
        }
        public SkillSyncer Syncer
        {
            get => this.skillSyncer;
            set => this.skillSyncer = value;
        }
    }
}
