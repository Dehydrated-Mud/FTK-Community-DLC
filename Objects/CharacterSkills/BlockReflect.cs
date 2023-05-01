using FTKAPI.APIs.BattleAPI;
using FTKAPI.Objects;
using GridEditor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommunityDLC.Objects.CharacterSkills
{
    internal class BlockReflect : FTKAPI_CharacterSkill
    {
        private CharacterDummy attacker;
        public BlockReflect() 
        {
            Name = new("Passive Skill: Rebuttal");
            Description = new("When blocking a melee attack, a large amount of damage reflection is applied to the attacker.");
            Trigger = TriggerType.BlockedAttack;
        }

        public override void Skill(CharacterOverworld cow, TriggerType trig, AttackAttempt _atk)
        {
            switch(trig)
            {
                case TriggerType.BlockedAttack:
                    attacker = _atk.m_AttackingDummy;
                    
                    if (_atk.m_VictimHealthAfter > 0 && (_atk.m_WeaponType == Weapon.WeaponType.axe || _atk.m_WeaponType == Weapon.WeaponType.bladed || _atk.m_WeaponType == Weapon.WeaponType.blunt || _atk.m_WeaponType == Weapon.WeaponType.spear || _atk.m_WeaponType == Weapon.WeaponType.monster || _atk.m_WeaponType == Weapon.WeaponType.unarmed))
                    {
                        FTK_weaponStats2 weapon = FTK_weaponStats2DB.Get(cow.m_WeaponID);
                        if (weapon.m_ObjectSlot == FTK_itembase.ObjectSlot.twoHands) {
                            int dmg = FTKUtil.RoundToInt((0.5f) * weapon._maxdmg);
                            BattleAPI.Instance.SetAFloat(attacker, dmg, SetFloats.DamageReflection, CombatValueOperators.Add);
                        }
                    }
                    break;
            }
        }
    }
}
