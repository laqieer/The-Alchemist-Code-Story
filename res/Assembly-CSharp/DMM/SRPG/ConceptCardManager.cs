﻿// Decompiled with JetBrains decompiler
// Type: SRPG.ConceptCardManager
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  [FlowNode.Pin(0, "初期化", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(1, "選択素材等クリア", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(10, "強化アニメ再生", FlowNode.PinTypes.Input, 10)]
  [FlowNode.Pin(110, "強化アニメ再生後", FlowNode.PinTypes.Output, 110)]
  [FlowNode.Pin(12, "限界突破アニメ再生", FlowNode.PinTypes.Input, 12)]
  [FlowNode.Pin(112, "限界突破アニメ再生後", FlowNode.PinTypes.Output, 112)]
  [FlowNode.Pin(11, "トラストマスター再生", FlowNode.PinTypes.Input, 11)]
  [FlowNode.Pin(111, "トラストマスター再生後", FlowNode.PinTypes.Output, 111)]
  [FlowNode.Pin(13, "グループスキル強化再生", FlowNode.PinTypes.Input, 13)]
  [FlowNode.Pin(113, "グループスキル強化再生後", FlowNode.PinTypes.Output, 113)]
  [FlowNode.Pin(14, "VisionMaster再生", FlowNode.PinTypes.Input, 14)]
  [FlowNode.Pin(114, "VisionMaster再生後", FlowNode.PinTypes.Output, 114)]
  [FlowNode.Pin(200, "TIPS表示", FlowNode.PinTypes.Output, 200)]
  public class ConceptCardManager : MonoBehaviour, IFlowInterface
  {
    private static ConceptCardManager _instance;
    public const int PIN_INIT = 0;
    public const int PIN_CLEAR_MAT = 1;
    public const int PIN_SELL = 3;
    public const int PIN_ENHANCE_ANIM = 10;
    public const int PIN_TRUSTMASTER_ANIM = 11;
    public const int PIN_AWAKE_ANIM = 12;
    public const int PIN_GROUPSKILL_POWERUP_ANIM = 13;
    public const int PIN_GROUPSKILL_MAX_POWERUP_ANIM = 14;
    public const int PIN_ENHANCE_ANIM_OUTPUT = 110;
    public const int PIN_TRUSTMASTER_ANIM_OUTPUT = 111;
    public const int PIN_AWAKE_ANIM_OUTPUT = 112;
    public const int PIN_GROUPSKILL_POWERUP_ANIM_OUTPUT = 113;
    public const int PIN_GROUPSKILL_MAX_POWERUP_ANIM_OUTPUT = 114;
    public const int PIN_TIPS_EQUIPMENT_OUTPUT = 200;
    [SerializeField]
    private GameObject mConceptCardList;
    [SerializeField]
    private GameObject mConceptCardEnhanceList;
    [SerializeField]
    private GameObject mConceptCardDetail;
    [SerializeField]
    private GameObject mConceptCardCheck;
    [SerializeField]
    private GameObject mConceptCardSellList;
    [SerializeField]
    private GameObject mConceptCardSellCheckList;
    [Space(10f)]
    private ConceptCardDetailLevel mLevelObject;
    [HideInInspector]
    public bool ToggleSameSelectCard;
    [HideInInspector]
    public ConceptCardListSortWindow.Type SortType;
    [HideInInspector]
    public ConceptCardListSortWindow.Type SortOrderType;
    private FilterConceptCardPrefs mFilterPrefs;
    private OLong mSelectedUniqueID;
    private MultiConceptCard mSelectedMaterials = new MultiConceptCard();
    [HideInInspector]
    public int CostConceptCardRare;
    private List<SelecteConceptCardMaterial> mBulkSelectedMaterialList = new List<SelecteConceptCardMaterial>();
    private ConceptCardData mSelectedConceptCardMaterial;
    private Dictionary<string, int> mSelectedAwakeMaterialList = new Dictionary<string, int>();
    private ConceptCardDetail mInsConceptCardDetail;

    public static ConceptCardManager Instance => ConceptCardManager._instance;

    private void Awake()
    {
      ConceptCardManager._instance = this;
      this.LoadSortFilterData();
    }

    private void OnDestroy() => ConceptCardManager._instance = (ConceptCardManager) null;

    public FilterConceptCardPrefs FilterPrefs => this.mFilterPrefs;

    public bool IsBranceListActive => this.mConceptCardList.activeSelf;

    public bool IsEnhanceListActive => this.mConceptCardEnhanceList.activeSelf;

    public bool IsSellListActive => this.mConceptCardSellList.activeSelf;

    public bool IsDetailActive => this.mConceptCardDetail.activeSelf;

    public ConceptCardDetail ConceptCardDetail => this.mInsConceptCardDetail;

    public static string ParseTrustFormat(ConceptCardData card, int trust)
    {
      int num1 = 0;
      if (card != null)
      {
        int num2 = (int) MonoSingleton<GameManager>.Instance.MasterParam.FixParam.CardTrustMax * (card.AwakeCountCap + 1);
        num1 = Mathf.Min(trust, num2);
      }
      return ((float) (num1 / 10 * 10) / 100f).ToString("F1");
    }

    public static void SubstituteTrustFormat(
      ConceptCardData card,
      Text txt,
      int trust,
      bool notChangeColor = false)
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) txt, (UnityEngine.Object) null) || card == null)
        return;
      string trustFormat = ConceptCardManager.ParseTrustFormat(card, trust);
      txt.text = trustFormat;
      if (notChangeColor)
        return;
      if (trust >= MonoSingleton<GameManager>.Instance.MasterParam.GetConceptCardTrustMax(card) && card.GetReward() != null)
        ((Graphic) txt).color = Color.red;
      else
        ((Graphic) txt).color = Color.white;
    }

    public static void CalcTotalExpTrust(
      ConceptCardData selectedCard,
      MultiConceptCard materials,
      out int mixTotalExp,
      out int mixTrustExp,
      out int mixTotalAwakeLv)
    {
      int num = 0;
      mixTotalExp = 0;
      mixTrustExp = 0;
      mixTotalAwakeLv = 0;
      MasterParam masterParam = MonoSingleton<GameManager>.Instance.MasterParam;
      foreach (ConceptCardData conceptCardData in materials.GetList())
      {
        mixTotalExp += conceptCardData.MixExp;
        mixTrustExp += conceptCardData.Param.en_trust;
        if (selectedCard != null && selectedCard.Param.iname == conceptCardData.Param.iname)
        {
          mixTrustExp += (int) masterParam.FixParam.CardTrustPileUp;
          mixTrustExp += (int) conceptCardData.Trust;
        }
        if (selectedCard != null && selectedCard.Param.iname == conceptCardData.Param.iname)
        {
          ++num;
          num += (int) conceptCardData.AwakeCount;
        }
      }
      if (selectedCard != null)
        num = Mathf.Min(num, selectedCard.AwakeCountCap - (int) selectedCard.AwakeCount);
      mixTotalAwakeLv = (int) MonoSingleton<GameManager>.Instance.MasterParam.FixParam.CardAwakeUnlockLevelCap * num;
    }

    public static int CalcTotalTrust(ConceptCardData selectedCard, MultiConceptCard materials)
    {
      MasterParam masterParam = MonoSingleton<GameManager>.Instance.MasterParam;
      int trust = (int) selectedCard.Trust;
      foreach (ConceptCardData conceptCardData in materials.GetList())
      {
        trust += conceptCardData.Param.en_trust;
        if (selectedCard != null && selectedCard.Param.iname == conceptCardData.Param.iname)
        {
          trust += (int) masterParam.FixParam.CardTrustPileUp;
          trust += (int) conceptCardData.Trust;
        }
      }
      return trust;
    }

    public static bool ContainsTrustMax(ConceptCardData selectedCard, MultiConceptCard materials)
    {
      int conceptCardTrustMax = MonoSingleton<GameManager>.Instance.MasterParam.GetConceptCardTrustMax(selectedCard);
      foreach (ConceptCardData conceptCardData in materials.GetList())
      {
        if ((int) conceptCardData.Trust >= conceptCardTrustMax)
          return true;
      }
      return false;
    }

    public static bool CalcTotalUnacquiredTrustBonus(
      ConceptCardData selectedCard,
      MultiConceptCard materials)
    {
      foreach (ConceptCardData conceptCardData in materials.GetList())
      {
        if ((int) conceptCardData.Trust > 0 && conceptCardData.GetReward() != null && (int) conceptCardData.AwakeCount + 1 > conceptCardData.TrustBonus)
          return true;
      }
      return false;
    }

    public static int CalcTotalTrustBonusMixCount(
      ConceptCardData selectedCard,
      MultiConceptCard materials)
    {
      int trustBonus = selectedCard.TrustBonus;
      foreach (ConceptCardData conceptCardData in materials.GetList())
      {
        if (selectedCard != null && selectedCard.Param.iname == conceptCardData.Param.iname)
          trustBonus += conceptCardData.TrustBonus;
      }
      return trustBonus;
    }

    public static int CalcTotalAwakeCount(ConceptCardData selectedCard, MultiConceptCard materials)
    {
      int awakeCount = (int) selectedCard.AwakeCount;
      foreach (ConceptCardData conceptCardData in materials.GetList())
      {
        if (selectedCard != null && selectedCard.Param.iname == conceptCardData.Param.iname)
        {
          ++awakeCount;
          awakeCount += (int) conceptCardData.AwakeCount;
        }
      }
      return awakeCount;
    }

    public static bool ContainsAwakeCountMax(
      ConceptCardData selectedCard,
      MultiConceptCard materials)
    {
      foreach (ConceptCardData conceptCardData in materials.GetList())
      {
        if ((int) conceptCardData.AwakeCount >= conceptCardData.AwakeCountCap)
          return true;
      }
      return false;
    }

    public static void CalcTotalExpTrustMaterialData(out int mixTotalExp, out int mixTrustExp)
    {
      mixTotalExp = 0;
      mixTrustExp = 0;
      ConceptCardManager instance = ConceptCardManager.Instance;
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) instance, (UnityEngine.Object) null) || instance.BulkSelectedMaterialList.Count == 0)
        return;
      foreach (SelecteConceptCardMaterial selectedMaterial in instance.BulkSelectedMaterialList)
      {
        mixTotalExp += selectedMaterial.mSelectedData.MixExp * selectedMaterial.mSelectNum;
        mixTrustExp += selectedMaterial.mSelectedData.Param.en_trust * selectedMaterial.mSelectNum;
      }
    }

    public static void CalcTotalExpTrust(
      out int mixTotalExp,
      out int mixTrustExp,
      out int mixTotalAwakeLv)
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) ConceptCardManager.Instance, (UnityEngine.Object) null))
      {
        mixTotalExp = 0;
        mixTrustExp = 0;
        mixTotalAwakeLv = 0;
      }
      else
        ConceptCardManager.CalcTotalExpTrust(ConceptCardManager.Instance.SelectedConceptCardData, ConceptCardManager.Instance.SelectedMaterials, out mixTotalExp, out mixTrustExp, out mixTotalAwakeLv);
    }

    public static void GalcTotalSellZeny(MultiConceptCard materials, out int totalSellZeny)
    {
      totalSellZeny = 0;
      foreach (ConceptCardData conceptCardData in materials.GetList())
        totalSellZeny += conceptCardData.SellGold;
    }

    public static void CalcTotalSellCoin(MultiConceptCard materials, out int totalSellCoin)
    {
      totalSellCoin = 0;
      foreach (ConceptCardData conceptCardData in materials.GetList())
        totalSellCoin += conceptCardData.SellCoinItemNum;
    }

    public static void GalcTotalMixZeny(MultiConceptCard materials, out int totalMixZeny)
    {
      totalMixZeny = 0;
      foreach (ConceptCardData conceptCardData in materials.GetList())
        totalMixZeny += conceptCardData.Param.en_cost;
    }

    public static void GalcTotalMixZenyMaterialData(out int totalMixZeny)
    {
      totalMixZeny = 0;
      ConceptCardManager instance = ConceptCardManager.Instance;
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) instance, (UnityEngine.Object) null) || instance.BulkSelectedMaterialList.Count == 0)
        return;
      foreach (SelecteConceptCardMaterial selectedMaterial in instance.BulkSelectedMaterialList)
        totalMixZeny += selectedMaterial.mSelectedData.Param.en_cost * selectedMaterial.mSelectNum;
    }

    public static string GetWarningTextByMaterials(MultiConceptCard materials)
    {
      string empty = string.Empty;
      bool flag = false;
      foreach (ConceptCardData conceptCardData in materials.GetList())
      {
        if ((int) conceptCardData.Rarity >= 3)
          flag = true;
      }
      if (flag)
        empty = LocalizedText.Get("sys.CONCEPT_CARD_WARNING_HIGH_RARITY");
      return empty;
    }

    public void Activated(int pinID)
    {
      switch (pinID)
      {
        case 0:
          this.Init();
          break;
        case 1:
          this.ClearMaterials();
          break;
        case 10:
          this.mLevelObject.StartLevelupAnimation(new ConceptCardDetailLevel.EffectCallBack(this.EnhanceAnimCallBack));
          break;
        case 11:
          this.mLevelObject.StartTrustMasterAnimation(new ConceptCardDetailLevel.EffectCallBack(this.TrustMasterAnimCallBack));
          break;
        case 12:
          this.mLevelObject.StartAwakeAnimation(new ConceptCardDetailLevel.EffectCallBack(this.AwakeAnimCallBack));
          break;
        case 13:
          this.mLevelObject.StartGroupSkillPowerUpAnimation(new ConceptCardDetailLevel.EffectCallBack(this.GroupSkillPowerUpAnimCallBack));
          break;
        case 14:
          this.mLevelObject.StartGroupSkillMaxPowerUpAnimation(new ConceptCardDetailLevel.EffectCallBack(this.GroupSkillMaxPowerUpAnimCallBack));
          break;
      }
    }

    private void EnhanceAnimCallBack()
    {
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 110);
    }

    private void AwakeAnimCallBack()
    {
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 112);
    }

    private void GroupSkillPowerUpAnimCallBack()
    {
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 113);
    }

    private void GroupSkillMaxPowerUpAnimCallBack()
    {
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 114);
    }

    private void TrustMasterAnimCallBack()
    {
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 111);
    }

    public ConceptCardData SelectedConceptCardData
    {
      set => this.mSelectedUniqueID = value.UniqueID;
      get
      {
        return MonoSingleton<GameManager>.Instance.Player.ConceptCards.Find((Predicate<ConceptCardData>) (ccd => (long) ccd.UniqueID == (long) this.mSelectedUniqueID));
      }
    }

    public ConceptCardData SelectedConceptCardMaterialData
    {
      set => this.mSelectedConceptCardMaterial = value;
      get => this.mSelectedConceptCardMaterial;
    }

    public bool IsEqualsSelectedConceptCardData(ConceptCardData ccd)
    {
      if (ccd == null)
        return false;
      ConceptCardData selectedConceptCardData = this.SelectedConceptCardData;
      return selectedConceptCardData != null && (long) ccd.UniqueID == (long) selectedConceptCardData.UniqueID;
    }

    public MultiConceptCard SelectedMaterials
    {
      set => this.mSelectedMaterials = value;
      get => this.mSelectedMaterials;
    }

    private void ClearMaterials() => this.mSelectedMaterials.Clear();

    public List<SelecteConceptCardMaterial> BulkSelectedMaterialList
    {
      set => this.mBulkSelectedMaterialList = value;
      get => this.mBulkSelectedMaterialList;
    }

    public Dictionary<string, int> SelectedAwakeMaterialList
    {
      set => this.mSelectedAwakeMaterialList = value;
      get => this.mSelectedAwakeMaterialList;
    }

    private void Init()
    {
      this.CallConceptCardInit(this.mConceptCardList);
      this.CallConceptCardInit(this.mConceptCardEnhanceList);
      this.CallConceptCardInit(this.mConceptCardSellList);
      this.CallConceptCardInit(this.mConceptCardDetail);
      this.mInsConceptCardDetail = this.mConceptCardDetail.GetComponent<ConceptCardDetail>();
      this.mInsConceptCardDetail.Init();
      this.mLevelObject = this.mInsConceptCardDetail.Description.Level;
      this.CallConceptCardInit(this.mConceptCardCheck);
      this.CallConceptCardInit(this.mConceptCardSellCheckList);
      MonoSingleton<GameManager>.Instance.Player.UpdateConceptCardTrophyAll();
      if (!MonoSingleton<GameManager>.Instance.Player.ConceptCards.Any<ConceptCardData>((Func<ConceptCardData, bool>) (card => card.Param.type == eCardType.Equipment)))
        return;
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 200);
    }

    private void CallConceptCardInit(GameObject obj)
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) obj, (UnityEngine.Object) null))
        return;
      ConceptCardList component1 = !UnityEngine.Object.op_Inequality((UnityEngine.Object) obj, (UnityEngine.Object) null) ? (ConceptCardList) null : obj.GetComponent<ConceptCardList>();
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component1, (UnityEngine.Object) null))
      {
        component1.Init();
      }
      else
      {
        ConceptCardScrollList component2 = !UnityEngine.Object.op_Inequality((UnityEngine.Object) obj, (UnityEngine.Object) null) ? (ConceptCardScrollList) null : obj.GetComponent<ConceptCardScrollList>();
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component2, (UnityEngine.Object) null))
        {
          component2.Init(MonoSingleton<GameManager>.Instance.Player.ConceptCards);
        }
        else
        {
          ConceptCardSellCheckList component3 = !UnityEngine.Object.op_Inequality((UnityEngine.Object) obj, (UnityEngine.Object) null) ? (ConceptCardSellCheckList) null : obj.GetComponent<ConceptCardSellCheckList>();
          if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) component3, (UnityEngine.Object) null))
            return;
          component3.Init();
        }
      }
    }

    public void GetTotalExp(out int mixTotalExp, out int mixTrustExp)
    {
      mixTotalExp = 0;
      mixTrustExp = 0;
      foreach (ConceptCardData conceptCardData in this.SelectedMaterials.GetList())
      {
        mixTotalExp += conceptCardData.MixExp;
        mixTrustExp += conceptCardData.Param.en_trust;
      }
    }

    public void SetupLevelupAnimation()
    {
      int mixTotalExp;
      int mixTrustExp;
      ConceptCardManager.CalcTotalExpTrust(this.SelectedConceptCardData, this.SelectedMaterials, out mixTotalExp, out mixTrustExp, out int _);
      this.mLevelObject.SetupLevelupAnimation(mixTotalExp, mixTrustExp);
    }

    public void SetupBulkLevelupAnimation()
    {
      int mixTotalExp;
      int mixTrustExp;
      ConceptCardManager.CalcTotalExpTrustMaterialData(out mixTotalExp, out mixTrustExp);
      this.mLevelObject.SetupLevelupAnimation(mixTotalExp, mixTrustExp);
    }

    public void LoadSortFilterData()
    {
      this.mFilterPrefs = FilterConceptCardPrefs.Load();
      this.SortType = ConceptCardListSortWindow.LoadDataType();
      this.SortOrderType = ConceptCardListSortWindow.LoadDataOrderType();
    }

    public bool IsSellList(GameObject list_object)
    {
      return UnityEngine.Object.op_Equality((UnityEngine.Object) list_object, (UnityEngine.Object) this.mConceptCardSellList);
    }

    public bool IsEnhanceList(GameObject list_object)
    {
      return UnityEngine.Object.op_Equality((UnityEngine.Object) list_object, (UnityEngine.Object) this.mConceptCardEnhanceList);
    }

    public void SetFilterPrefs(FilterConceptCardPrefs filter_prefs)
    {
      this.mFilterPrefs = filter_prefs;
    }
  }
}
