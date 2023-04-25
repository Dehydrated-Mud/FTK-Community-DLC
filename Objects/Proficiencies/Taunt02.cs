using FTKAPI.Objects;
using Google2u;
using HutongGames.PlayMaker.Actions;
using GridEditor;
using Logger = FTKAPI.Utils.Logger;
using CommunityDLC.PhotonHooks;
using CommunityDLC.Objects.CharacterSkills;

namespace CommunityDLC.Objects.Proficiencies {
    public class Taunt02 : CustomProficiency {
        public Taunt02() {
            ID = "taunt02";
            Name = new("Long Taunt");
            SlotOverride = 2;
            ProficiencyPrefab = this;
            Category = Category.Taunt;
            IsEndOnTurn = true;
            RepeatCount = 1;
        }

        public override void AddToDummy(CharacterDummy _dummy)
        {
            base.AddToDummy(_dummy);
            CustomCharacterStats customStats = _dummy.m_CharacterOverworld.gameObject.GetComponent<CustomCharacterStats>();
            _dummy.m_TauntResist = (_dummy.m_TauntArmor = _dummy.m_CharacterOverworld.m_CharacterStats.m_PlayerLevel);
            if (customStats != null)
            {
                SkillContainer.Instance.skillSyncer.SyncImpervious(_dummy.m_CharacterOverworld.m_FTKPlayerID, _dummy.m_TauntArmor, _dummy.m_TauntResist);
                //customStats.imperviousArmor += _dummy.m_TauntArmor;
                //customStats.imperviousResistance += _dummy.m_TauntResist;
            }
            else
            {
                Logger.LogError("Could not find customstats on AddToDummy!");
            }
            AudioManager.Instance.AudioEvent("Play_com_abi_taunt");
            _dummy.SpawnHudTextRPC(FTKHub.Localized<TextMisc>("STR_HudTaunt"), string.Empty);
            Logger.LogWarning("Adding armor to dummy, amount: " + _dummy.m_CharacterOverworld.m_CharacterStats.m_PlayerLevel);
        }

        public override void End(CharacterDummy _dummy)
        {
            base.End(_dummy);
            CustomCharacterStats customStats = _dummy.m_CharacterOverworld.gameObject.GetComponent<CustomCharacterStats>();
            if (customStats != null)
            {
                SkillContainer.Instance.skillSyncer.SyncImpervious(_dummy.m_CharacterOverworld.m_FTKPlayerID, _dummy.m_TauntArmor, _dummy.m_TauntResist);
            }
            else
            {
                Logger.LogError("Could not find customstats on End!");
            }
            int playerLevel = _dummy.m_CharacterOverworld.m_CharacterStats.m_PlayerLevel;
            _dummy.m_TauntArmor = 0;
            _dummy.m_TauntResist = 0;
            _dummy.AddProfToDummy(new FTK_proficiencyTable.ID[1] { FTK_proficiencyTable.ID.taunt }, _fx: false, _hud: false);
            //_dummy.PlayCharacterAbilityEventRPC(FTK_characterSkill.ID.ShieldTaunt);
        }
    }
}
