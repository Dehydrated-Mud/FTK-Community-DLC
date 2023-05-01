using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using CommunityDLC.Objects.Modifiers;
using CommunityDLC.Objects.SkillTree.Leaves;
using CommunityDLC.PhotonHooks;
using FTKAPI.Objects;
using FTKAPI.Objects.SkillHooks;
using UnityEngine;
using Logger = FTKAPI.Utils.Logger;

namespace CommunityDLC.UIElements
{
    public enum CustomModType
    {
        None = 0,
        WeaponMod
    }
    [AttributeUsage(AttributeTargets.All, Inherited = false, AllowMultiple = true)]
    public class CustomModDisplayNameDLC : CustomModDisplayName
    {
        public CustomModType m_CustomModType;
        public float m_default;
        public CustomModDisplayNameDLC(string _display, string _toolTip, ModType _modType, CustomModType _custom , float _default, bool _percent = false) : base(_display, _toolTip, _modType, _percent)
        {
            m_CustomModType = _custom;
            m_default = _default;
        }
        public CustomModDisplayNameDLC(string _display, string _toolTip, ModType _modType, CustomModType _custom, bool _percent = false) : base(_display, _toolTip, _modType, _percent)
        {
            m_CustomModType = _custom;
            m_default = 0f;
        }
    }

    internal class HookGetModDisplay : BaseModule
    {
        public override void Initialize()
        {
            On.CharacterSkills.GetModDisplay += GetModDisplayHook;
        }

        private string GetModDisplayHook(On.CharacterSkills.orig_GetModDisplay orig, object _o, bool _format)
        {

            //Logger.LogWarning("Entering our new GetModDisplay method!");
            string text = orig(_o, _format);
            if (text != string.Empty && !text.EndsWith(Environment.NewLine))
            {
                text += Environment.NewLine;
            }
            if (_o is DLCCustomModifier)
            {
                DLCCustomModifier _mod = (DLCCustomModifier)_o;
                if (_mod.m_DisplayName != "")
                {
                    text += _mod.m_DisplayName + "\n";
                }
                else if (_mod is GenericGiveItem)
                {
                    GenericGiveItem _mod2 = (GenericGiveItem)_o;
                    text += _mod2.GetLocalizedName() + "\n";
                }
                else if (_mod is GenericPermanentAura)
                {
                    GenericPermanentAura _mod2 = (GenericPermanentAura)_o;
                    text += _mod2.GetLocalizedName() + "\n";
                }
                Type type = _mod.GetType();
                PropertyInfo[] fields = type.GetProperties(BindingFlags.Instance | BindingFlags.Public);// | System.Reflection.BindingFlags.DeclaredOnly);
                PropertyInfo[] array = fields;
                foreach (PropertyInfo fieldInfo in array)
                {
                    object[] customAttributes = fieldInfo.GetCustomAttributes(typeof(CustomModDisplayNameDLC), inherit: false);
                    if (customAttributes.Length == 0)
                    {
                        continue;
                    }
                    CustomModDisplayNameDLC modDisplayName = (CustomModDisplayNameDLC)customAttributes[0];
                    if (fieldInfo.PropertyType == typeof(int))
                    {
                        int num2 = (int)fieldInfo.GetValue(_o, null);
                        if (num2 != 0)
                        {
                            Color color = SetColor(num2, modDisplayName.m_CustomModType, modDisplayName.m_ModType);
                            string text3 = BuildString(num2, modDisplayName.m_CustomModType, modDisplayName.m_ModType, false, _format, isfloat: false);
                            text3 += modDisplayName.m_DisplayName + "\n";
                            text = text + text3;
                        }
                    }
                        /*if (fieldInfo.FieldType == typeof(int))
                        {
                            int num = (int)fieldInfo.GetValue(_mod);
                            string text2 = num + " " + modDisplayName.m_DisplayName;
                            if (num != 0)
                            {
                                Color color = VisualParams.Instance.m_ColorTints.m_CharacterModTypeColor[modDisplayName.m_ModType];
                                if (num > 0)
                                {
                                    text2 = "+" + text2;
                                }
                                else
                                {
                                    color = VisualParams.Instance.m_ColorTints.m_BadStatColor;
                                }
                                if (_format)
                                {
                                    text2 = FTKUI.GetKeyInfoRichText(color, _bold: false, text2);
                                }
                                text = text + text2 + "\n";
                            }
                        }*/
                    if (fieldInfo.PropertyType == typeof(float))
                    {
                        float num2 = (float)fieldInfo.GetValue(_o, null);
                        if (num2 != 0)
                        {
                            Color color = SetColor(num2, modDisplayName.m_CustomModType, modDisplayName.m_ModType);
                            string text3 = BuildString(num2, modDisplayName.m_CustomModType, modDisplayName.m_ModType, modDisplayName.m_Percent, _format) + modDisplayName.m_DisplayName;
                            text = text + text3 + "\n";
                        }
                    }
                    else if (fieldInfo.PropertyType == typeof(WeaponMod)) 
                    {
                        WeaponMod wepMod = (WeaponMod)fieldInfo.GetValue(_o, null);
                        string txt4 = "";
                        if (wepMod.m_AtkFac != 0)
                        {
                            txt4 += BuildString(wepMod.m_AtkFac, CustomModType.WeaponMod, ModType.StatMod, true, true) + modDisplayName.m_DisplayName + "\n";
                        }
                        if (wepMod.m_AtkAdd != 0)
                        {
                            txt4 += BuildString(wepMod.m_AtkAdd, CustomModType.WeaponMod, ModType.StatMod, false, true, false) + modDisplayName.m_DisplayName + "\n";
                        }
                        if (wepMod.m_DefAdd != 0)
                        {
                            txt4 += BuildString(wepMod.m_DefAdd, CustomModType.WeaponMod, ModType.StatMod, true, true) + modDisplayName.m_DisplayName + "\n";
                        }
                        if (wepMod.m_DefFac != 0)
                        {
                            txt4 += BuildString(wepMod.m_DefFac, CustomModType.WeaponMod, ModType.StatMod, false, true, false) + modDisplayName.m_DisplayName + "\n";
                        }
                        if (txt4 != "")
                        {
                            text += txt4;
                        }
                    }
                    /*else if (fieldInfo.FieldType == typeof(bool))
                    {
                        if ((bool)fieldInfo.GetValue(_mod))
                        {
                            string text4 = modDisplayName.m_DisplayName;
                            Color color3 = VisualParams.Instance.m_ColorTints.m_CharacterModTypeColor[modDisplayName.m_ModType];
                            if (_format)
                            {
                                text4 = FTKUI.GetKeyInfoRichText(color3, _bold: false, text4);
                            }
                            text = text + text4 + "\n";
                        }
                    }*/
                }
            }
            return text.TrimEnd(new char[] { '\n' }); ;
        }

        private string BuildString(float num, CustomModType type, ModType type1, bool percent, bool format, bool isfloat = true)
        {
            string built = BuildPrefix(num, type) + BuildNumber(num, type, isfloat) + BuildSuffix(num, type, percent);
            if (format)
            {
                Color color = SetColor(num, type, type1);
                built = FTKUI.GetKeyInfoRichText(color, _bold: false, built);
            }
            return built;
        }
        private string BuildPrefix(float num, CustomModType type)
        {
            string prefix = "";
            switch(type)
            {
                default: 
                    if (num > 0f)
                    {
                        prefix = "+";
                    }
                    break;
            }
            return prefix;

        }

        private string BuildSuffix(float num, CustomModType type, bool percent) 
        {
            string suffix = "";
            if (percent)
            {
                suffix = "%";
            }
            return suffix;
        }

        private string BuildNumber(float num, CustomModType type, bool isfloat)
        {
            string number = "";
            switch (type)
            {
                default:
                    if (isfloat)
                    {
                        number = FTKUtil.RoundToInt(num * 100f).ToString();
                    }
                    else
                    {
                        number = num.ToString();
                    }
                    break;
            }
            return number;
        }

        private Color SetColor(float num, CustomModType type, ModType type1)
        {
            Color color = VisualParams.Instance.m_ColorTints.m_CharacterModTypeColor[type1];
            switch (type)
            {
                case CustomModType.WeaponMod:
                    if (num > 0f)
                    {
                        color = new Color(0.2f, 0.7f, .2f, 1f);
                    }
                    break;
                default:
                    if (num <= 0)
                    {
                        color = VisualParams.Instance.m_ColorTints.m_BadStatColor;
                    }
                    break;
            }
            return color;
        }
        public override void Unload()
        {
            On.CharacterSkills.GetModDisplay -= GetModDisplayHook;
        }
    }
}


