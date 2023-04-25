using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommunityDLC.Objects
{
    public class DLCUtils
    {
        /// <summary>
        /// Returns the amount to heal a character by based a percentage of their health
        /// </summary>
        /// <param name="_dum">dummy to heal</param>
        /// <param name="_fac">health percentage</param>
        /// <returns></returns>
        public static int HealByPercentage(CharacterDummy _dum, float _fac)
        {
            return (int)Math.Min((float)(_dum.m_CharacterOverworld.m_CharacterStats.MaxHealth * _fac), (float)(_dum.m_CharacterOverworld.m_CharacterStats.MaxHealth - _dum.GetCurrentHealth()));
        }
    }
}
