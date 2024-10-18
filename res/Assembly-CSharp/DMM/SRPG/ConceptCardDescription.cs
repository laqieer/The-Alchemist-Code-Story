// Decompiled with JetBrains decompiler
// Type: SRPG.ConceptCardDescription
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  [FlowNode.Pin(1, "変更前のスキルを表示", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(2, "変更前のスキルを非表示", FlowNode.PinTypes.Input, 2)]
  [FlowNode.Pin(11, "リーダースキル詳細表示", FlowNode.PinTypes.Input, 11)]
  public class ConceptCardDescription : MonoBehaviour, IFlowInterface
  {
    public const int INPUT_ENABLE_BASE_SKILL = 1;
    public const int INPUT_DIABLE_BASE_SKILL = 2;
    public const int INPUT_OPEN_LEADER_SKILL_DETAIL = 11;
    [SerializeField]
    private string PREFAB_PATH_BONUS_WINDOW = "UI/ConceptCardLevelBonusDetail";
    [SerializeField]
    public ConceptCardDetailLevel Level;
    [SerializeField]
    public ConceptCardDetailStatus Status;
    [SerializeField]
    public ConceptCardDetailSkin SkinPrefab;
    [SerializeField]
    private GameObject AbilityTemplate;
    [SerializeField]
    private GameObject mCardAbilityDescriptionPrefab;
    [SerializeField]
    private ConceptCardDetailGetUnit GetUnit;
    [SerializeField]
    private Selectable m_ShowBaseToggle;
    [SerializeField]
    private GameObject LeaderSkillInfo;
    [SerializeField]
    private GameObject LeaderSkillLock;
    [SerializeField]
    private GameObject Prefab_LeaderSkillDetail;
    [SerializeField]
    private SRPG_Button LeaderSkillDetailButton;
    private GameObject mAbilityDetailParent;
    private GameObject mCardAbilityDescription;
    private List<ConceptCardDetailSkin> Skin = new List<ConceptCardDetailSkin>();
    private List<AbilityDeriveList> m_AbilityDeriveList = new List<AbilityDeriveList>();
    private ConceptCardData mConceptCardData;
    private UnitData m_UnitData;
    [SerializeField]
    private Button OpenBonusButton;
    [SerializeField]
    private GameObject GroupIconsParent;
    private GameObject mBonusWindow;
    private Canvas abilityCanvas;
    private Canvas bonusCanvas;
    private GameObject mLeaderSkillDetail;
    private static ConceptCardDescription _instance;
    private bool mIsEnhance;
    [SerializeField]
    private GameObject DecreaseEffectObject;
    [SerializeField]
    private Text DecreaseEffectText;
    private bool mEnableDecreaseEffectObject;
    private ConceptCardDescription.ConceptCardEnhanceInfo mEnhanceInfo;

    public static bool IsEnhance
    {
      get
      {
        return UnityEngine.Object.op_Inequality((UnityEngine.Object) ConceptCardDescription._instance, (UnityEngine.Object) null) && ConceptCardDescription._instance.mIsEnhance;
      }
    }

    public static ConceptCardDescription.ConceptCardEnhanceInfo EnhanceInfo
    {
      get
      {
        return UnityEngine.Object.op_Inequality((UnityEngine.Object) ConceptCardDescription._instance, (UnityEngine.Object) null) && ConceptCardDescription.IsEnhance ? ConceptCardDescription._instance.mEnhanceInfo : (ConceptCardDescription.ConceptCardEnhanceInfo) null;
      }
    }

    private void Awake() => ConceptCardDescription._instance = this;

    public void Activated(int pinID)
    {
      if (pinID != 1)
      {
        if (pinID != 2)
        {
          if (pinID == 11)
            ;
        }
        else
          this.SwitchBaseSkillEnable(false);
      }
      else
        this.SwitchBaseSkillEnable(true);
    }

    private void SwitchBaseSkillEnable(bool enable)
    {
      foreach (AbilityDeriveList abilityDerive in this.m_AbilityDeriveList)
        abilityDerive.SwitchBaseAbilityEnable(enable);
    }

    public void SetConceptCardData(
      ConceptCardData data,
      GameObject ability_detail_parent,
      bool bEnhance,
      bool is_first_get_unit = false,
      UnitData unitData = null,
      bool enableDecreaseEffectObject = false)
    {
      this.mConceptCardData = data;
      if (this.mConceptCardData == null)
        return;
      this.m_UnitData = unitData;
      this.mIsEnhance = bEnhance;
      this.mEnableDecreaseEffectObject = enableDecreaseEffectObject;
      int mixTotalExp;
      int mixTrustExp;
      int mixTotalAwakeLv;
      ConceptCardManager.CalcTotalExpTrust(out mixTotalExp, out mixTrustExp, out mixTotalAwakeLv);
      this.mEnhanceInfo = new ConceptCardDescription.ConceptCardEnhanceInfo(data, mixTotalExp, mixTrustExp, mixTotalAwakeLv);
      this.CreatePrefab(ability_detail_parent);
      this.SetParam((ConceptCardDetailBase) this.Level, this.mConceptCardData, mixTotalExp, mixTrustExp, mixTotalAwakeLv);
      this.SetParam((ConceptCardDetailBase) this.Status, this.mConceptCardData, mixTotalExp, mixTrustExp, mixTotalAwakeLv);
      bool active = this.mConceptCardData.EquipEffects != null && this.mConceptCardData.EquipEffects.Where<ConceptCardEquipEffect>((Func<ConceptCardEquipEffect, bool>) (effect => effect.EquipSkill != null && effect.IsDecreaseEffectOnSub)).Count<ConceptCardEquipEffect>() > 0 && enableDecreaseEffectObject && this.mConceptCardData.CurrentDecreaseEffectRate > 0;
      GameUtility.SetGameObjectActive(this.DecreaseEffectObject, active);
      if (active)
        ConceptCardUtility.SetDecreaseEffectRateText(this.DecreaseEffectText, this.mConceptCardData.CurrentDecreaseEffectRate);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.LeaderSkillInfo, (UnityEngine.Object) null))
      {
        if (this.mConceptCardData.LeaderSkill == null)
        {
          this.LeaderSkillInfo.SetActive(false);
        }
        else
        {
          this.LeaderSkillInfo.SetActive(true);
          DataSource.Bind<SkillData>(this.LeaderSkillInfo, this.mConceptCardData.LeaderSkill);
          GameParameter.UpdateAll(this.LeaderSkillInfo);
        }
      }
      if (this.Skin != null)
      {
        for (int index = 0; index < this.Skin.Count; ++index)
          this.SetParam((ConceptCardDetailBase) this.Skin[index], this.mConceptCardData);
      }
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.GetUnit, (UnityEngine.Object) null))
      {
        if (!is_first_get_unit)
          ((Component) this.GetUnit).gameObject.SetActive(false);
        this.SetParam((ConceptCardDetailBase) this.GetUnit, this.mConceptCardData);
      }
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.OpenBonusButton, (UnityEngine.Object) null))
      {
        bool flag = this.mConceptCardData.IsEnableAwake;
        if (!this.mConceptCardData.Param.IsExistAddCardSkillBuffAwake() && !this.mConceptCardData.Param.IsExistAddCardSkillBuffLvMax())
          flag = false;
        ((Selectable) this.OpenBonusButton).interactable = flag;
      }
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.GroupIconsParent, (UnityEngine.Object) null))
      {
        DataSource.Bind<ConceptCardParam>(this.GroupIconsParent, this.mConceptCardData.Param);
        GameParameter.UpdateAll(this.GroupIconsParent);
      }
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.LeaderSkillDetailButton, (UnityEngine.Object) null))
        this.LeaderSkillDetailButton.AddListener(new SRPG_Button.ButtonClickEvent(this.OpenLeaderSkillDetail));
      this.Refresh();
    }

    private void OpenLeaderSkillDetail(SRPG_Button button)
    {
      if (!((Selectable) button).IsInteractable() || !UnityEngine.Object.op_Inequality((UnityEngine.Object) this.LeaderSkillDetailButton, (UnityEngine.Object) null) || !UnityEngine.Object.op_Inequality((UnityEngine.Object) this.Prefab_LeaderSkillDetail, (UnityEngine.Object) null) || !UnityEngine.Object.op_Equality((UnityEngine.Object) this.mLeaderSkillDetail, (UnityEngine.Object) null))
        return;
      this.mLeaderSkillDetail = UnityEngine.Object.Instantiate<GameObject>(this.Prefab_LeaderSkillDetail);
      DataSource.Bind<SkillData>(this.mLeaderSkillDetail, this.mConceptCardData.LeaderSkill);
    }

    private void Refresh()
    {
      if (this.mConceptCardData == null)
        return;
      this.Refresh((ConceptCardDetailBase) this.Level);
      this.Refresh((ConceptCardDetailBase) this.Status);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.LeaderSkillLock, (UnityEngine.Object) null) && this.mConceptCardData.LeaderSkill != null)
        this.LeaderSkillLock.SetActive(!this.mConceptCardData.LeaderSkillIsAvailable());
      if (this.Skin != null)
      {
        for (int index = 0; index < this.Skin.Count; ++index)
          this.Refresh((ConceptCardDetailBase) this.Skin[index]);
      }
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.GetUnit, (UnityEngine.Object) null))
        this.Refresh((ConceptCardDetailBase) this.GetUnit);
      foreach (Scrollbar componentsInChild in ((Component) this).GetComponentsInChildren<Scrollbar>())
        componentsInChild.value = 1f;
    }

    public void Refresh(ConceptCardDetailBase concept)
    {
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) concept, (UnityEngine.Object) null))
        return;
      concept.Refresh();
    }

    public void SetParam(ConceptCardDetailBase concept, ConceptCardData data)
    {
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) concept, (UnityEngine.Object) null))
        return;
      concept.SetParam(data);
    }

    public void SetParam(
      ConceptCardDetailBase concept,
      ConceptCardData data,
      int addExp,
      int addTrust,
      int addAwakeLv)
    {
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) concept, (UnityEngine.Object) null))
        return;
      concept.SetParam(data, addExp, addTrust, addAwakeLv);
    }

    public void CreatePrefab(GameObject ability_detail_parent)
    {
      this.CreateSkinPrefab();
      this.CreateSkillPrefab(ability_detail_parent);
    }

    public void CreateSkinPrefab()
    {
      List<ConceptCardEquipEffect> equipEffects = this.mConceptCardData.EquipEffects;
      int index1 = 0;
      if (equipEffects != null)
      {
        for (int index2 = 0; index2 < equipEffects.Count; ++index2)
        {
          ConceptCardEquipEffect effect = equipEffects[index2];
          if (!string.IsNullOrEmpty(effect.Skin))
          {
            ConceptCardDetailSkin conceptCardDetailSkin;
            if (this.Skin.Count <= index1)
            {
              conceptCardDetailSkin = UnityEngine.Object.Instantiate<ConceptCardDetailSkin>(this.SkinPrefab);
              this.Skin.Add(conceptCardDetailSkin);
            }
            else
              conceptCardDetailSkin = this.Skin[index1];
            ((Component) conceptCardDetailSkin).gameObject.SetActive(true);
            ((Component) conceptCardDetailSkin).transform.SetParent(((Component) this.SkinPrefab).transform.parent, false);
            conceptCardDetailSkin.SetEquipEffect(effect);
            ++index1;
          }
        }
      }
      for (int index3 = index1; index3 < this.Skin.Count; ++index3)
        ((Component) this.Skin[index3]).gameObject.SetActive(false);
    }

    public void CreateSkillPrefab(GameObject ability_detail_parent)
    {
      List<ConceptCardSkillDatailData> abilityDatailData = this.mConceptCardData.GetAbilityDatailData();
      this.mAbilityDetailParent = ability_detail_parent;
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.m_ShowBaseToggle, (UnityEngine.Object) null))
        this.m_ShowBaseToggle.interactable = false;
      for (int index = 0; index < this.m_AbilityDeriveList.Count; ++index)
        ((Component) this.m_AbilityDeriveList[index]).gameObject.SetActive(false);
      int decreaseEffectRate = this.mConceptCardData.CurrentDecreaseEffectRate;
      for (int index = 0; index < abilityDatailData.Count; ++index)
      {
        ConceptCardSkillDatailData conceptCardSkillDatailData = abilityDatailData[index];
        AbilityDeriveList go;
        if (index < this.m_AbilityDeriveList.Count)
        {
          go = this.m_AbilityDeriveList[index];
        }
        else
        {
          go = this.CreateAbilityListItem();
          this.m_AbilityDeriveList.Add(go);
        }
        go.SetupWithConceptCard(conceptCardSkillDatailData, this.m_UnitData, new ConceptCardDetailAbility.ClickDetail(this.OnClickDetail), this.mEnableDecreaseEffectObject, decreaseEffectRate);
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.m_ShowBaseToggle, (UnityEngine.Object) null))
          this.m_ShowBaseToggle.interactable |= go.HasDerive;
        GameUtility.SetGameObjectActive((Component) go, true);
      }
      GameUtility.SetGameObjectActive(this.AbilityTemplate, false);
      this.AbilityTemplate.gameObject.SetActive(false);
    }

    private AbilityDeriveList CreateAbilityListItem()
    {
      GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.AbilityTemplate);
      gameObject.gameObject.SetActive(true);
      gameObject.transform.SetParent(this.AbilityTemplate.transform.parent, false);
      return gameObject.GetComponent<AbilityDeriveList>();
    }

    public void OnClickOpenBonusButton()
    {
      if (this.mConceptCardData == null)
        return;
      this.StartCoroutine(this.OpenBonusWindow(this.mConceptCardData));
    }

    [DebuggerHidden]
    private IEnumerator OpenBonusWindow(ConceptCardData concept_card)
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new ConceptCardDescription.\u003COpenBonusWindow\u003Ec__Iterator0()
      {
        concept_card = concept_card,
        \u0024this = this
      };
    }

    private void OnDestoryBonusWindow(GameObject obj)
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.bonusCanvas, (UnityEngine.Object) null))
        return;
      UnityEngine.Object.Destroy((UnityEngine.Object) ((Component) this.bonusCanvas).gameObject);
      this.bonusCanvas = (Canvas) null;
    }

    public void OnClickDetail(ConceptCardSkillDatailData data)
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.mAbilityDetailParent, (UnityEngine.Object) null) || UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mCardAbilityDescription, (UnityEngine.Object) null))
        return;
      this.mCardAbilityDescription = UnityEngine.Object.Instantiate<GameObject>(this.mCardAbilityDescriptionPrefab);
      DataSource.Bind<ConceptCardSkillDatailData>(this.mCardAbilityDescription, data);
      this.abilityCanvas = UIUtility.PushCanvas();
      this.mCardAbilityDescription.transform.SetParent(((Component) this.abilityCanvas).transform, false);
      this.mCardAbilityDescription.AddComponent<DestroyEventListener>().Listeners += new DestroyEventListener.DestroyEvent(this.OnDestroyAbilityDescription);
    }

    private void OnDestroyAbilityDescription(GameObject go)
    {
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.abilityCanvas, (UnityEngine.Object) null))
        return;
      UnityEngine.Object.Destroy((UnityEngine.Object) ((Component) this.abilityCanvas).gameObject);
      this.abilityCanvas = (Canvas) null;
    }

    private void OnDestroy()
    {
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.abilityCanvas, (UnityEngine.Object) null))
      {
        UnityEngine.Object.Destroy((UnityEngine.Object) ((Component) this.abilityCanvas).gameObject);
        this.abilityCanvas = (Canvas) null;
      }
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mLeaderSkillDetail, (UnityEngine.Object) null))
        return;
      UnityEngine.Object.Destroy((UnityEngine.Object) this.mLeaderSkillDetail);
    }

    public class ConceptCardEnhanceInfo
    {
      private ConceptCardData target;
      private int add_exp;
      private int add_trust;
      private int add_awake_lv;
      private int predict_lv;
      private int predict_awake;

      public ConceptCardEnhanceInfo(
        ConceptCardData _concept_card,
        int _add_exp,
        int _add_trust,
        int _add_awake_lv)
      {
        this.target = _concept_card;
        this.add_exp = _add_exp;
        this.add_trust = _add_trust;
        this.add_awake_lv = _add_awake_lv;
        this.predict_lv = MonoSingleton<GameManager>.Instance.MasterParam.CalcConceptCardLevel((int) this.target.Rarity, (int) this.target.Exp + this.add_exp, Mathf.Min((int) this.target.LvCap, (int) this.target.CurrentLvCap + this.add_awake_lv));
        this.predict_awake = Mathf.Min(this.target.AwakeCountCap, (int) this.target.AwakeCount + this.add_awake_lv / (int) MonoSingleton<GameManager>.Instance.MasterParam.FixParam.CardAwakeUnlockLevelCap);
      }

      public ConceptCardData Target => this.target;

      public int addExp => this.add_exp;

      public int addTrust => this.add_trust;

      public int addAwakeLv => this.add_awake_lv;

      public int predictLv => this.predict_lv;

      public int predictAwake => this.predict_awake;

      public void Clear()
      {
        this.target = (ConceptCardData) null;
        this.add_exp = 0;
        this.add_trust = 0;
        this.add_awake_lv = 0;
        this.predict_lv = 0;
      }
    }
  }
}
