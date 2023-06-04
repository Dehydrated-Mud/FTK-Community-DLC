using FTKAPI.APIs.BattleAPI;
using FTKAPI.Objects;
using GridEditor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommunityDLC.Objects.CharacterSkills
{
    internal class CallOfTheHunter : FTKAPI_CharacterSkill
    {
        public CallOfTheHunter() 
        {
            Trigger = TriggerType.KillShot;
            Name = new("Passive Skill: Call of the Hunter");
            Description = new("Grants the Hunter additional XP for big game kills (Bears, Jaguars, etc.)");
        }

        public override void Skill(CharacterOverworld cow, TriggerType trig, AttackAttempt atk)
        {
            switch (trig)
            {
                case TriggerType.KillShot:
                    if (atk.m_DamagedDummy is EnemyDummy)
                    {
                        EnemyDummy enemy = (EnemyDummy) atk.m_DamagedDummy;
                        if (DLCUtils.bigGame.Contains(FTK_enemyCombat.GetEnum(enemy.m_EnemyCombat.m_ID))) 
                        {
                            int xp = (cow.m_CharacterStats.m_PlayerLevel + 1) * 2;
                            BattleAPI.Instance.AddXp(cow, xp);
                        }
                    }
                    break;
            }
        }
    }

    internal class Livelihood : FTKAPI_CharacterSkill
    {
        public Livelihood()
        {
            Trigger = TriggerType.KillShot;
            Name = new("Passive Skill: Hunter's Livlihood");
            Description = new("Grants the Hunter additional Gold for big game kills (Bears, Jaguars, etc.)");
        }

        public override void Skill(CharacterOverworld cow, TriggerType trig, AttackAttempt atk)
        {
            switch (trig)
            {
                case TriggerType.KillShot:
                    if (atk.m_DamagedDummy is EnemyDummy)
                    {
                        EnemyDummy enemy = (EnemyDummy)atk.m_DamagedDummy;
                        if (DLCUtils.bigGame.Contains(FTK_enemyCombat.GetEnum(enemy.m_EnemyCombat.m_ID)))
                        {
                            int gold = (cow.m_CharacterStats.m_PlayerLevel + 1) * 2;
                            BattleAPI.Instance.AddGold(cow, gold);
                        }
                    }
                    break;
            }
        }
    }
}
