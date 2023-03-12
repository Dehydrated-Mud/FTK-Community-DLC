using Google2u;
using Logger = FTKAPI.Utils.Logger;
namespace CommunityDLC.Objects.Proficiencies;
public class ProficiencyLongTaunt : ProficiencyBase
{
	public override void AddToDummy(CharacterDummy _dummy)
	{
		base.AddToDummy(_dummy);
		_dummy.m_TauntResist = (_dummy.m_TauntArmor = _dummy.m_CharacterOverworld.m_CharacterStats.m_PlayerLevel);
		AudioManager.Instance.AudioEvent("Play_com_abi_taunt");
		_dummy.SpawnHudTextRPC(FTKHub.Localized<TextMisc>("STR_HudTaunt"), string.Empty);
		Logger.LogWarning("Adding armor to dummy, amount: " + _dummy.m_CharacterOverworld.m_CharacterStats.m_PlayerLevel);
	}

	public override void End(CharacterDummy _dummy)
	{
		base.End(_dummy);
		int playerLevel = _dummy.m_CharacterOverworld.m_CharacterStats.m_PlayerLevel;
		_dummy.m_TauntArmor = 0;
		_dummy.m_TauntResist = 0;
	}
}
