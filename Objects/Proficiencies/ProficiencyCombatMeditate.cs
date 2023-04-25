using FTKAPI.Objects;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using GridEditor;
using Logger = FTKAPI.Utils.Logger;
using FTKAPI.APIs.BattleAPI;

namespace CommunityDLC.Objects.Proficiencies
{
    public class ProficiencyCombatMeditate : CustomProficiency
    {
        public ProficiencyCombatMeditate()
        {
            Target = CharacterDummy.TargetType.None;
            TargetFriendly = true;
            Category = Category.Petrify;
            ProficiencyPrefab = this;
            SlotOverride = 1;
            PerSlotSkillRoll = 0f;
            //BattleButton = CommunityDLC.assetBundleIcons.LoadAsset<Sprite>("Assets/Icons/weaponBlade.png");
            Name = new("Combat Meditation");
            ID = "CustomCombatMeditationProf";
            Tint = new Color(.8f, .6f, 1f, 1f);
            Harmless = true;
            WpnTypeOverride = Weapon.WeaponType.magic;
            IsEndOnTurn = false;
            Quickness = 0.5f;
            RepeatCount = 2;
        }

        public override void AddToDummy(CharacterDummy _dummy)
        {
            base.AddToDummy(_dummy);
            Category[] c = new Category[7]
            {
            Category.Bleed,
            Category.Fire,
            Category.Ice,
            Category.Lightning,
            Category.Acid,
            Category.Water,
            Category.Death
            };
            CharacterOverworld cow = _dummy.m_CharacterOverworld;
            cow.m_CharacterStats.SetPoison(-1, _broadcast: true, _hud: false);
            cow.m_CharacterStats.SetDisease(string.Empty, -1, _overrideType: false, _broadcast: true, _hud: false);
            _dummy.RemoveProficiencyTypes(c);
            _doPetrifyFreeze(_dummy, CharacterEventListener.CombatAnimTrigger.Revive, 2.25f);

        }

        public override void End(CharacterDummy _dummy)
        {
            base.End(_dummy);
            _resetCharacter(_dummy, (_dummy.m_DamageInfo.m_NewHealth <= 0) ? CharacterEventListener.CombatAnimTrigger.Death : CharacterEventListener.CombatAnimTrigger.Revive);
            BattleAPI.Instance.SetAFlag(_dummy, true, SetFlags.Crit, true);

        }

        private void _doPetrifyFreeze(CharacterDummy _dummy, CharacterEventListener.CombatAnimTrigger _animTrigger, float _freezeTime)
        {
            StartCoroutine(_petrifyFreezeSequence(_dummy, _animTrigger, _freezeTime));
        }

        private IEnumerator _petrifyFreezeSequence(CharacterDummy _dummy, CharacterEventListener.CombatAnimTrigger _animTrigger, float _freezeTime)
        {
            ParticleSystem[] ps = _dummy.m_EventListener.GetComponentsInChildren<ParticleSystem>();
            ParticleSystem[] array = ps;
            foreach (ParticleSystem particleSystem in array)
            {
                particleSystem.gameObject.SetActive(value: false);
            }
            List<Renderer> rendererList = new List<Renderer>(_dummy.m_EventListener.GetComponentsInChildren<Renderer>());
            Material stoneMaterial = FTKHex.Instance.m_StoneHeroMaterials[MiniHexStoneHero.StoneHeroType.StoneHeroBronze];
            _dummy.m_EventListener.CombatTrigger(_animTrigger, _forceAnim: true);
            float currentTime = 0f;
            float animationTime = _dummy.m_EventListener.m_Animator.speed;
            float timePerMaterial = _freezeTime / (float)rendererList.Count;
            float currentMaterialTime = 0f;
            FTKRandom rand = new FTKRandom();
            while (currentTime < _freezeTime)
            {
                currentTime += Time.deltaTime;
                currentMaterialTime += Time.deltaTime;
                if (currentMaterialTime > timePerMaterial)
                {
                    currentMaterialTime = 0f;
                    _setStoneMaterial(rendererList, stoneMaterial, rand);
                }
                float freezePercent = currentTime / _freezeTime;
                _dummy.m_EventListener.m_Animator.speed = (1f - freezePercent) * animationTime;
                yield return null;
            }
            while (rendererList.Count > 0)
            {
                _setStoneMaterial(rendererList, stoneMaterial, rand);
                yield return null;
            }
            _dummy.m_EventListener.m_Animator.speed = 0f;
        }

        private void _setStoneMaterial(List<Renderer> _renderList, Material _mat, FTKRandom _rand)
        {
            Renderer randomElementFromList = _rand.GetRandomElementFromList(_renderList);
            _renderList.Remove(randomElementFromList);
            Material[] array = new Material[randomElementFromList.materials.Length];
            for (int i = 0; i < randomElementFromList.materials.Length; i++)
            {
                array[i] = _mat;
            }
            randomElementFromList.materials = array;
        }

        private void _resetCharacter(CharacterDummy _dummy, CharacterEventListener.CombatAnimTrigger _followUpAnim)
        {
            _dummy.CreateAvatar(_useLightProbe: true);
            _dummy.InitDummyIconsAndFx();
            _dummy.m_EventListener.m_Animator.speed = 1f;
            if (_followUpAnim != CharacterEventListener.CombatAnimTrigger.None)
            {
                _dummy.m_EventListener.CombatTrigger(_followUpAnim);
            }
            _dummy.m_DummyFX.PlayHitEffect(FTK_hitEffect.ID.petrifyBreak, _overworld: false);
        }
    }
}
