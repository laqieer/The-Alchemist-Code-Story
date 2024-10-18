// Decompiled with JetBrains decompiler
// Type: SRPG.GuildFacilityEnhance
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace SRPG
{
  [FlowNode.Pin(30, "素材選択クリア", FlowNode.PinTypes.Input, 30)]
  [FlowNode.Pin(40, "施設強化開始", FlowNode.PinTypes.Input, 40)]
  [FlowNode.Pin(50, "素材選択(次ページ)", FlowNode.PinTypes.Input, 50)]
  [FlowNode.Pin(60, "素材選択(前ページ)", FlowNode.PinTypes.Input, 60)]
  [FlowNode.Pin(70, "データ、表示を更新", FlowNode.PinTypes.Input, 70)]
  [FlowNode.Pin(80, "施設強化演出", FlowNode.PinTypes.Input, 80)]
  [FlowNode.Pin(1010, "施設強化リクエスト", FlowNode.PinTypes.Output, 1010)]
  [FlowNode.Pin(1020, "施設強化演出完了", FlowNode.PinTypes.Output, 1020)]
  public class GuildFacilityEnhance : MonoBehaviour, IFlowInterface
  {
    [SerializeField]
    private float ENHANCE_EFFECT_TIME = 1f;
    [SerializeField]
    private int SELECTABLE_LIMIT = 12;
    [SerializeField]
    private int ENHANCE_ITEM_COUNT_MAX = 6;
    private List<GameObject> mCreatedViewItems = new List<GameObject>();
    private List<ItemData> mEnhanceItems = new List<ItemData>();
    private List<GameObject> mCreatedEnhanceItemObjects = new List<GameObject>();
    private List<EnhanceMaterial> mEnhanceMaterials = new List<EnhanceMaterial>();
    private List<EnhanceMaterial> mSelectedEnhanceMaterials = new List<EnhanceMaterial>();
    private List<GameObject> mCreatedFacilityEffectTexts = new List<GameObject>();
    private const int PIN_INPUT_CLEAR_ENHANCE_MATERIALS = 30;
    private const int PIN_INPUT_START_FACILITY_ENHANCE = 40;
    private const int PIN_INPUT_SELECT_MATERIAL_PAGE_NEXT = 50;
    private const int PIN_INPUT_SELECT_MATERIAL_PAGE_BACK = 60;
    private const int PIN_INPUT_RESET_ALL = 70;
    private const int PIN_INPUT_START_ENHANCE_ANIMATION = 80;
    private const int PIN_OUTPUT_REQUEST_FACILITY_ENHANCE = 1010;
    private const int PIN_OUTPUT_START_ENHANCE_ANIMATION = 1020;
    [SerializeField]
    private GameObject mFacilityViewItemTemplate;
    [SerializeField]
    private GameObject mEnhanceItemTemplate;
    [SerializeField]
    private GameObject mFacilityListRoot;
    [SerializeField]
    private GameObject mFacilityEnhanceRoot;
    [SerializeField]
    private GameObject mFacilityEnhanceSliderRoot;
    [SerializeField]
    private Slider mFacilityEnhanceSlider;
    [SerializeField]
    private Slider mFacilityExpGauge;
    [SerializeField]
    private Text mFacilityNextExpText;
    [SerializeField]
    private Text mFacilityEnhanceAfterLevelText;
    [SerializeField]
    private GameObject mFacilityEffectTemplate;
    [SerializeField]
    private Text mCurrentInvestPoint;
    [SerializeField]
    private Button mEnhanceSubmitButton;
    [SerializeField]
    private Button mEnhanceClearButton;
    [SerializeField]
    private Button mPageNextObject;
    [SerializeField]
    private Button mPageBackObject;
    [SerializeField]
    private GameObject mEnhanceLevelupEffect;
    [SerializeField]
    private Button mEnhanceMaterialPlusButton;
    [SerializeField]
    private Button mEnhanceMaterialMinusButton;
    [SerializeField]
    private Button mEnhanceMaterialMaxButton;
    [SerializeField]
    private GameObject mFacilityLevelMaxMask;
    [SerializeField]
    private GameObject mNormalLevelObject;
    [SerializeField]
    private GameObject mMaxLevelObject;
    private static GuildFacilityEnhance mInstance;
    private GuildData mTargetGuild;
    private GuildFacilityData mTargetFacility;
    private EnhanceMaterial mTargetEnhanceMaterial;
    private int mCurrentPage;
    private int mPageMax;
    private int mBeforeEnhanceInvestPoint;
    private GameObject mConfirmWindow;
    private GameObject mErrorWindow;
    private GuildFacilityEnhance.EffectCallBack mCallback;

    public static GuildFacilityEnhance Instance
    {
      get
      {
        return GuildFacilityEnhance.mInstance;
      }
    }

    public GuildFacilityData TargetFacility
    {
      get
      {
        return this.mTargetFacility;
      }
    }

    public List<EnhanceMaterial> SelectedEnhanceMaterials
    {
      get
      {
        return this.mSelectedEnhanceMaterials;
      }
    }

    public void Activated(int pinID)
    {
      switch (pinID)
      {
        case 30:
          this.ClearSelectEnhanceMaterial(true);
          break;
        case 40:
          this.StartGuildFacilityEnhance();
          break;
        case 50:
          this.PageNext();
          break;
        case 60:
          this.PageBack();
          break;
        case 70:
          this.ResetAll();
          break;
        case 80:
          this.StartCoroutine(this.LevelupAnimation(this.mTargetFacility, new GuildFacilityEnhance.EffectCallBack(this.EndLevelupAnimation)));
          break;
      }
    }

    private void Awake()
    {
      GuildFacilityEnhance.mInstance = this;
    }

    private void Start()
    {
      this.Init();
      this.Refresh_FacilityList();
    }

    private void Update()
    {
      this.mFacilityEnhanceSliderRoot.SetActive(this.mTargetEnhanceMaterial != null);
      this.mEnhanceSubmitButton.interactable = this.mSelectedEnhanceMaterials.Count > 0;
      this.mEnhanceClearButton.interactable = this.mSelectedEnhanceMaterials.Count > 0;
      this.mPageNextObject.interactable = this.mCurrentPage < this.mPageMax;
      this.mPageBackObject.interactable = this.mCurrentPage > 0;
      this.mEnhanceMaterialMaxButton.interactable = (this.ErrorCheck_LvMax() || this.ErrorCheck_InvestLimit() ? 1 : (this.ErrorCheck_ItemLimit() ? 1 : 0)) == 0;
      this.mEnhanceMaterialPlusButton.interactable = (this.ErrorCheck_LvMax() || this.ErrorCheck_InvestLimit() ? 1 : (this.ErrorCheck_ItemLimit() ? 1 : 0)) == 0;
      this.mEnhanceMaterialMinusButton.interactable = (this.mTargetEnhanceMaterial == null ? 1 : (this.mTargetEnhanceMaterial.num <= 0 ? 1 : 0)) == 0;
    }

    private void Init()
    {
      SerializeValueBehaviour component = this.GetComponent<SerializeValueBehaviour>();
      if (!((UnityEngine.Object) component != (UnityEngine.Object) null))
        return;
      this.mTargetGuild = component.list.GetObject<GuildData>(GuildSVB_Key.GUILD, (GuildData) null);
      DataSource.Bind<GuildData>(this.gameObject, this.mTargetGuild, false);
    }

    private void ResetAll()
    {
      this.mTargetFacility = Array.Find<GuildFacilityData>(MonoSingleton<GameManager>.Instance.Player.Guild.Facilities, (Predicate<GuildFacilityData>) (facility => facility.Iname == this.mTargetFacility.Iname));
      DataSource.Bind<GuildFacilityData>(this.mFacilityEnhanceRoot, this.mTargetFacility, false);
      this.Refresh_FacilityList();
      this.Refresh_FacilityEnhance(this.mCurrentPage, true);
      this.ClearSelectEnhanceMaterial(false);
    }

    private int GetSelectedMaterialsTotalExp(bool is_enhance_animation = false, bool is_simple_total = false)
    {
      int a = 0;
      for (int index = 0; index < this.mSelectedEnhanceMaterials.Count; ++index)
        a += this.mSelectedEnhanceMaterials[index].item.Param.facility_point * this.mSelectedEnhanceMaterials[index].num;
      if (is_simple_total)
        return a;
      int b = MonoSingleton<GameManager>.Instance.MasterParam.FixParam.GuildInvestLimit - (!is_enhance_animation ? MonoSingleton<GameManager>.Instance.Player.PlayerGuild.InvestPoint : this.mBeforeEnhanceInvestPoint);
      return Mathf.Min(a, b);
    }

    private int GetRestInvestPoint()
    {
      return Mathf.Max(0, MonoSingleton<GameManager>.Instance.MasterParam.FixParam.GuildInvestLimit - MonoSingleton<GameManager>.Instance.Player.PlayerGuild.InvestPoint);
    }

    private void StartGuildFacilityEnhance()
    {
      if (this.mTargetGuild == null || this.mSelectedEnhanceMaterials.Count <= 0)
        return;
      bool flag = false;
      for (int index = 0; index < this.mSelectedEnhanceMaterials.Count; ++index)
      {
        if (this.mSelectedEnhanceMaterials[index].item.Rarity > 1 || this.mSelectedEnhanceMaterials[index].item.ItemType == EItemType.Material)
        {
          flag = true;
          break;
        }
      }
      if (flag && (UnityEngine.Object) this.mConfirmWindow == (UnityEngine.Object) null)
        this.mConfirmWindow = UIUtility.ConfirmBox(LocalizedText.Get("sys.GUILDFACILITY_ENHANCE_RARITY_CAUTION"), new UIUtility.DialogResultEvent(this.RequestGuildFacilityEnhance), (UIUtility.DialogResultEvent) null, (GameObject) null, false, -1, (string) null, (string) null);
      else
        this.RequestGuildFacilityEnhance(this.gameObject);
    }

    private void RequestGuildFacilityEnhance(GameObject obj)
    {
      this.mBeforeEnhanceInvestPoint = MonoSingleton<GameManager>.Instance.Player.PlayerGuild.InvestPoint;
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 1010);
    }

    public void Refresh_FacilityList()
    {
      this.mFacilityViewItemTemplate.SetActive(false);
      for (int index = 0; index < this.mCreatedViewItems.Count; ++index)
        this.mCreatedViewItems[index].SetActive(false);
      int num = this.mTargetGuild.Facilities.Length - this.mCreatedViewItems.Count;
      for (int index = 0; index < num; ++index)
      {
        GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.mFacilityViewItemTemplate);
        gameObject.transform.SetParent(this.mFacilityViewItemTemplate.transform.parent, false);
        this.mCreatedViewItems.Add(gameObject);
      }
      for (int index = 0; index < this.mTargetGuild.Facilities.Length; ++index)
      {
        this.mCreatedViewItems[index].SetActive(true);
        DataSource.Bind<GuildFacilityData>(this.mCreatedViewItems[index], this.mTargetGuild.Facilities[index], false);
      }
      GameParameter.UpdateAll(this.gameObject);
    }

    private List<ItemData> GetPlayerEnhanceItmes()
    {
      List<ItemData> itemDataList = new List<ItemData>();
      List<ItemData> items = MonoSingleton<GameManager>.Instance.Player.Items;
      for (int index = 0; index < items.Count; ++index)
      {
        if (items[index].CheckEquipEnhanceMaterial() && !items[index].Param.is_valuables && (items[index].Num > 0 && items[index].Param.facility_point > 0))
          itemDataList.Add(items[index]);
      }
      itemDataList.Sort((Comparison<ItemData>) ((src, dsc) =>
      {
        if (src.ItemType != dsc.ItemType)
        {
          if (src.ItemType == EItemType.ExpGuildFacility)
            return -1;
          if (dsc.ItemType == EItemType.ExpGuildFacility)
            return 1;
          if (src.ItemType == EItemType.ExpUpEquip)
            return -1;
          if (dsc.ItemType == EItemType.ExpUpEquip)
            return 1;
        }
        return dsc.Param.facility_point - src.Param.facility_point;
      }));
      return itemDataList;
    }

    private void SetActive_FacilityLevelMaxUI(bool is_level_max)
    {
      this.mFacilityLevelMaxMask.SetActive(is_level_max);
      this.mNormalLevelObject.SetActive(!is_level_max);
      this.mMaxLevelObject.SetActive(is_level_max);
    }

    private void Refresh_FacilityEnhance(int page, bool is_update_items = true)
    {
      this.SetActive_FacilityLevelMaxUI(this.mTargetFacility.Level >= GuildFacilityData.GetLevelMax(this.mTargetFacility.Param.Type));
      this.mEnhanceItemTemplate.SetActive(false);
      if (is_update_items)
      {
        this.mEnhanceItems = this.GetPlayerEnhanceItmes();
        this.mEnhanceMaterials.Clear();
        for (int index = 0; index < this.mEnhanceItems.Count; ++index)
          this.mEnhanceMaterials.Add(new EnhanceMaterial()
          {
            item = this.mEnhanceItems[index],
            num = 0,
            selected = false
          });
        this.mPageMax = this.mEnhanceItems.Count / this.ENHANCE_ITEM_COUNT_MAX;
        this.mPageMax += this.mEnhanceItems.Count % this.ENHANCE_ITEM_COUNT_MAX != 0 ? 0 : -1;
        this.mPageMax = Mathf.Max(0, this.mPageMax);
      }
      for (int index = 0; index < this.mCreatedEnhanceItemObjects.Count; ++index)
        this.mCreatedEnhanceItemObjects[index].SetActive(false);
      int num1 = this.ENHANCE_ITEM_COUNT_MAX - this.mCreatedEnhanceItemObjects.Count;
      for (int index = 0; index < num1; ++index)
      {
        GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.mEnhanceItemTemplate);
        gameObject.transform.SetParent(this.mEnhanceItemTemplate.transform.parent, false);
        this.mCreatedEnhanceItemObjects.Add(gameObject);
      }
      page = Mathf.Clamp(page, 0, this.mPageMax);
      int num2 = page * this.ENHANCE_ITEM_COUNT_MAX;
      for (int index1 = 0; index1 < this.ENHANCE_ITEM_COUNT_MAX; ++index1)
      {
        int index2 = num2 + index1;
        if (index2 < this.mEnhanceItems.Count)
        {
          this.mCreatedEnhanceItemObjects[index1].SetActive(true);
          DataSource.Bind<ItemData>(this.mCreatedEnhanceItemObjects[index1], this.mEnhanceItems[index2], false);
          DataSource.Bind<EnhanceMaterial>(this.mCreatedEnhanceItemObjects[index1], this.mEnhanceMaterials[index2], false);
        }
        else
          break;
      }
      GameParameter.UpdateAll(this.mFacilityEnhanceRoot);
    }

    private void PageNext()
    {
      ++this.mCurrentPage;
      this.mCurrentPage = Mathf.Clamp(this.mCurrentPage, 0, this.mPageMax);
      this.Refresh_FacilityEnhance(this.mCurrentPage, false);
    }

    private void PageBack()
    {
      --this.mCurrentPage;
      this.mCurrentPage = Mathf.Clamp(this.mCurrentPage, 0, this.mPageMax);
      this.Refresh_FacilityEnhance(this.mCurrentPage, false);
    }

    private void Refresh_FacilityExp(int total_add_exp = -1)
    {
      bool flag = total_add_exp >= 0;
      if (!flag)
        total_add_exp = this.GetSelectedMaterialsTotalExp(false, false);
      int new_level = 0;
      int rest_next_exp = 0;
      GuildFacilityData.SimlateEnhance(this.mTargetFacility, total_add_exp, out new_level, out rest_next_exp);
      this.mFacilityEnhanceAfterLevelText.text = new_level.ToString();
      this.mFacilityNextExpText.text = rest_next_exp.ToString();
      int levelMax = GuildFacilityData.GetLevelMax(this.mTargetFacility.Param.Type);
      float needExp = (float) GuildFacilityData.GetNeedExp(new_level, new_level + 1, this.mTargetFacility.Param.Type);
      float num1 = new_level >= levelMax ? 1f : needExp;
      float num2 = this.mFacilityExpGauge.maxValue;
      if (new_level < levelMax)
        num2 = (float) (this.mTargetFacility.Exp + total_add_exp - GuildFacilityData.GetNeedExp(1, new_level, this.mTargetFacility.Param.Type));
      this.mFacilityExpGauge.maxValue = num1;
      this.mFacilityExpGauge.minValue = 0.0f;
      this.mFacilityExpGauge.value = num2;
      this.Refresh_FacilityEffect(new_level);
      int num3 = total_add_exp;
      if (new_level >= levelMax)
        num3 = GuildFacilityData.GetNeedExp(1, levelMax, this.mTargetFacility.Param.Type) - this.mTargetFacility.Exp;
      this.mCurrentInvestPoint.text = Mathf.Min(MonoSingleton<GameManager>.Instance.MasterParam.FixParam.GuildInvestLimit, (!flag ? MonoSingleton<GameManager>.Instance.Player.PlayerGuild.InvestPoint : this.mBeforeEnhanceInvestPoint) + num3).ToString();
    }

    private void Refresh_FacilityEffect(int after_level)
    {
      if (this.mTargetFacility == null)
        return;
      this.mFacilityEffectTemplate.SetActive(false);
      GuildFacilityEffectParam effect1 = this.mTargetFacility.GetEffect();
      GuildFacilityEffectParam effect2 = this.mTargetFacility.Param.GetEffect(after_level);
      for (int index = 0; index < this.mCreatedFacilityEffectTexts.Count; ++index)
        this.mCreatedFacilityEffectTexts[index].SetActive(false);
      int num1 = effect2.GetEffectCount() - this.mCreatedFacilityEffectTexts.Count;
      for (int index = 0; index < num1; ++index)
      {
        GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.mFacilityEffectTemplate);
        gameObject.transform.SetParent(this.mFacilityEffectTemplate.transform.parent, false);
        this.mCreatedFacilityEffectTexts.Add(gameObject);
      }
      int index1 = 0;
      if (effect1.member_count > 0)
      {
        this.mCreatedFacilityEffectTexts[index1].SetActive(true);
        SerializeValueBehaviour component = this.mCreatedFacilityEffectTexts[index1].GetComponent<SerializeValueBehaviour>();
        component.list.GetComponent<Text>(GuildFacilitySVB_Key.EFFECT_NAME).text = LocalizedText.Get("sys.GUILD_FACILITY_EFFECT_MEMBER_COUNT");
        component.list.GetComponent<Text>(GuildFacilitySVB_Key.EFFECT_VALUE).text = effect1.member_count.ToString();
        this.SetGuildFacilityEffectPlus(component, effect2.member_count - effect1.member_count);
        ++index1;
      }
      if (effect1.sub_master <= 0)
        return;
      this.mCreatedFacilityEffectTexts[index1].SetActive(true);
      SerializeValueBehaviour component1 = this.mCreatedFacilityEffectTexts[index1].GetComponent<SerializeValueBehaviour>();
      component1.list.GetComponent<Text>(GuildFacilitySVB_Key.EFFECT_NAME).text = LocalizedText.Get("sys.GUILD_FACILITY_EFFECT_SUBMASTER_COUNT");
      component1.list.GetComponent<Text>(GuildFacilitySVB_Key.EFFECT_VALUE).text = effect1.sub_master.ToString();
      this.SetGuildFacilityEffectPlus(component1, effect2.sub_master - effect1.sub_master);
      int num2 = index1 + 1;
    }

    private void SetGuildFacilityEffectPlus(SerializeValueBehaviour svb, int dif_value)
    {
      svb.list.GetComponent<Text>(GuildFacilitySVB_Key.EFFECT_PLUS_ICON).gameObject.SetActive(dif_value != 0);
      svb.list.GetComponent<Text>(GuildFacilitySVB_Key.EFFECT_PLUS).gameObject.SetActive(dif_value != 0);
      svb.list.GetComponent<Text>(GuildFacilitySVB_Key.EFFECT_PLUS).text = dif_value.ToString();
    }

    private void SetupInvestGuildFacilityUI(EnhanceMaterial mat)
    {
      this.mTargetEnhanceMaterial = mat;
      if (this.mTargetEnhanceMaterial == null)
        return;
      int levelMax = GuildFacilityData.GetLevelMax(this.mTargetFacility.Param.Type);
      int facilityPoint = this.mTargetEnhanceMaterial.item.Param.facility_point;
      int num1 = (int) Mathf.Ceil((float) MonoSingleton<GameManager>.Instance.MasterParam.FixParam.GuildInvestLimit / (float) facilityPoint);
      int num2 = (int) Mathf.Ceil((float) (GuildFacilityData.GetNeedExp(1, levelMax, this.mTargetFacility.Param.Type) - this.mTargetFacility.Exp) / (float) facilityPoint);
      int num3 = this.mTargetEnhanceMaterial.item.Num;
      int num4 = num1;
      if (num4 > num2)
        num4 = num2;
      if (num4 > num3)
        num4 = num3;
      this.mFacilityEnhanceSlider.onValueChanged.RemoveAllListeners();
      this.mFacilityEnhanceSlider.maxValue = (float) num4;
      this.mFacilityEnhanceSlider.minValue = 0.0f;
      this.mFacilityEnhanceSlider.value = (float) mat.num;
      this.mFacilityEnhanceSlider.onValueChanged.AddListener(new UnityAction<float>(this.OnChange_GuildFacilityEnhanceMaterialSlider));
    }

    private void ClearSelectEnhanceMaterial(bool is_keep_select = true)
    {
      for (int index = 0; index < this.mSelectedEnhanceMaterials.Count; ++index)
      {
        this.mSelectedEnhanceMaterials[index].num = 0;
        this.mSelectedEnhanceMaterials[index].selected = false;
      }
      this.mSelectedEnhanceMaterials.Clear();
      if (is_keep_select)
      {
        if (this.mTargetEnhanceMaterial != null)
          this.mTargetEnhanceMaterial.selected = true;
      }
      else
        this.mTargetEnhanceMaterial = (EnhanceMaterial) null;
      this.Refresh_FacilityExp(-1);
      this.SetupInvestGuildFacilityUI(this.mTargetEnhanceMaterial);
      this.RefreshFacilityEnhanceSlider();
      GameParameter.UpdateAll(this.mFacilityEnhanceRoot);
    }

    private bool ErrorCheck_LvMax()
    {
      return this.mTargetFacility != null && this.GetSelectedMaterialsTotalExp(false, true) >= GuildFacilityData.GetNeedExp(1, GuildFacilityData.GetLevelMax(this.mTargetFacility.Param.Type), this.mTargetFacility.Param.Type) - this.mTargetFacility.Exp;
    }

    private bool ErrorCheck_InvestLimit()
    {
      return this.mTargetFacility != null && this.GetSelectedMaterialsTotalExp(false, true) >= this.GetRestInvestPoint();
    }

    private bool ErrorCheck_ItemLimit()
    {
      return this.mTargetFacility != null && !this.mSelectedEnhanceMaterials.Contains(this.mTargetEnhanceMaterial) && this.mSelectedEnhanceMaterials.Count >= this.SELECTABLE_LIMIT;
    }

    private int GetNeedNum_LvMax()
    {
      if (this.mTargetFacility == null)
        return 0;
      int num1 = 0;
      if (this.mTargetEnhanceMaterial != null)
        num1 = this.mTargetEnhanceMaterial.item.Param.facility_point * this.mTargetEnhanceMaterial.num;
      int num2 = GuildFacilityData.GetNeedExp(1, GuildFacilityData.GetLevelMax(this.mTargetFacility.Param.Type), this.mTargetFacility.Param.Type) - this.mTargetFacility.Exp - (this.GetSelectedMaterialsTotalExp(false, true) - num1);
      if (num2 <= 0)
        return 0;
      return (int) Mathf.Ceil((float) num2 / (float) this.mTargetEnhanceMaterial.item.Param.facility_point);
    }

    private int GetNeedNum_InvestLimit()
    {
      if (this.mTargetFacility == null)
        return 0;
      int num1 = 0;
      if (this.mTargetEnhanceMaterial != null)
        num1 = this.mTargetEnhanceMaterial.item.Param.facility_point * this.mTargetEnhanceMaterial.num;
      int num2 = this.GetRestInvestPoint() - (this.GetSelectedMaterialsTotalExp(false, true) - num1);
      if (num2 <= 0)
        return 0;
      return (int) Mathf.Ceil((float) num2 / (float) this.mTargetEnhanceMaterial.item.Param.facility_point);
    }

    private void RefreshFacilityEnhanceSlider()
    {
      this.mFacilityEnhanceSlider.interactable = (this.ErrorCheck_LvMax() || this.ErrorCheck_InvestLimit() ? 1 : (this.ErrorCheck_ItemLimit() ? 1 : 0)) == 0 || (this.mTargetEnhanceMaterial == null ? 1 : (this.mTargetEnhanceMaterial.num <= 0 ? 1 : 0)) == 0;
    }

    public void OnClick_GuildFacilityItem(GameObject item)
    {
      if ((UnityEngine.Object) item == (UnityEngine.Object) null)
        return;
      GuildFacilityData dataOfClass = DataSource.FindDataOfClass<GuildFacilityData>(item, (GuildFacilityData) null);
      if (dataOfClass == null)
        return;
      this.mCurrentPage = 0;
      this.mTargetFacility = dataOfClass;
      DataSource.Bind<GuildFacilityData>(this.mFacilityEnhanceRoot, this.mTargetFacility, false);
      this.mFacilityEnhanceRoot.SetActive(true);
      this.Refresh_FacilityExp(-1);
      this.Refresh_FacilityEnhance(this.mCurrentPage, true);
      this.ClearSelectEnhanceMaterial(false);
    }

    public void OnClick_GuildFacilityEnhanceMaterial(GameObject obj)
    {
      EnhanceMaterial dataOfClass = DataSource.FindDataOfClass<EnhanceMaterial>(obj, (EnhanceMaterial) null);
      if (dataOfClass == null)
        return;
      for (int index = 0; index < this.mEnhanceMaterials.Count; ++index)
        this.mEnhanceMaterials[index].selected = dataOfClass == this.mEnhanceMaterials[index];
      this.SetupInvestGuildFacilityUI(dataOfClass);
      this.RefreshFacilityEnhanceSlider();
      GameParameter.UpdateAll(this.mFacilityEnhanceRoot);
    }

    private void OnChange_GuildFacilityEnhanceMaterialSlider(float value)
    {
      bool flag = (double) this.mTargetEnhanceMaterial.num <= (double) value;
      this.mTargetEnhanceMaterial.num = (int) value;
      if (this.mTargetEnhanceMaterial.num > 0 && !this.mSelectedEnhanceMaterials.Contains(this.mTargetEnhanceMaterial))
        this.mSelectedEnhanceMaterials.Add(this.mTargetEnhanceMaterial);
      if (this.mTargetEnhanceMaterial.num <= 0 && this.mSelectedEnhanceMaterials.Contains(this.mTargetEnhanceMaterial))
        this.mSelectedEnhanceMaterials.Remove(this.mTargetEnhanceMaterial);
      if (flag)
      {
        if (this.ErrorCheck_LvMax())
        {
          if (this.mTargetEnhanceMaterial.num != this.GetNeedNum_LvMax() && (UnityEngine.Object) this.mErrorWindow == (UnityEngine.Object) null)
          {
            this.mErrorWindow = UIUtility.SystemMessage(LocalizedText.Get("sys.GUILDFACILITY_ERROR_MSG_ENHANCE_LEVEL_MAX"), new UIUtility.DialogResultEvent(this.OnClose_ErrorWindow), (GameObject) null, false, -1);
            this.mFacilityEnhanceSlider.interactable = false;
          }
          this.mTargetEnhanceMaterial.num = this.GetNeedNum_LvMax();
        }
        if (this.ErrorCheck_InvestLimit())
        {
          int needNumInvestLimit = this.GetNeedNum_InvestLimit();
          if (this.mTargetEnhanceMaterial.num != needNumInvestLimit && (UnityEngine.Object) this.mErrorWindow == (UnityEngine.Object) null)
          {
            this.mErrorWindow = UIUtility.SystemMessage(LocalizedText.Get("sys.GUILDFACILITY_ERROR_MSG_ENHANCE_INVEST_LIMIT"), new UIUtility.DialogResultEvent(this.OnClose_ErrorWindow), (GameObject) null, false, -1);
            this.mFacilityEnhanceSlider.interactable = false;
          }
          this.mTargetEnhanceMaterial.num = needNumInvestLimit;
        }
        if (this.ErrorCheck_ItemLimit())
        {
          this.mTargetEnhanceMaterial.num = 0;
          if ((UnityEngine.Object) this.mErrorWindow == (UnityEngine.Object) null)
          {
            this.mErrorWindow = UIUtility.SystemMessage(LocalizedText.Get("sys.GUILDFACILITY_ERROR_MSG_ENHANCE_SELECTABLE_LIMIT"), new UIUtility.DialogResultEvent(this.OnClose_ErrorWindow), (GameObject) null, false, -1);
            this.mFacilityEnhanceSlider.interactable = false;
          }
        }
      }
      this.SetupInvestGuildFacilityUI(this.mTargetEnhanceMaterial);
      if (this.mTargetEnhanceMaterial.num > 0 && !this.mSelectedEnhanceMaterials.Contains(this.mTargetEnhanceMaterial))
        this.mSelectedEnhanceMaterials.Add(this.mTargetEnhanceMaterial);
      if (this.mTargetEnhanceMaterial.num <= 0 && this.mSelectedEnhanceMaterials.Contains(this.mTargetEnhanceMaterial))
        this.mSelectedEnhanceMaterials.Remove(this.mTargetEnhanceMaterial);
      this.Refresh_FacilityExp(-1);
      GameParameter.UpdateAll(this.mFacilityEnhanceRoot);
    }

    private void OnClose_ErrorWindow(GameObject go)
    {
      this.mFacilityEnhanceSlider.interactable = true;
    }

    public void OnClick_EnhanceMaterialCountUp()
    {
      ++this.mFacilityEnhanceSlider.value;
    }

    public void OnClick_EnhanceMaterialCountDown()
    {
      --this.mFacilityEnhanceSlider.value;
    }

    public void OnClick_EnhanceMaterialCountMax()
    {
      int materialsTotalExp = this.GetSelectedMaterialsTotalExp(false, false);
      int num1 = 0;
      int num2 = Mathf.Max(0, GuildFacilityData.GetNeedExp(1, GuildFacilityData.GetLevelMax(this.mTargetFacility.Param.Type), this.mTargetFacility.Param.Type) - this.mTargetFacility.Exp - materialsTotalExp);
      if (num2 > 0)
        num1 = (int) Mathf.Ceil((float) num2 / (float) this.mTargetEnhanceMaterial.item.Param.facility_point);
      int num3 = (int) Mathf.Ceil((float) Mathf.Max(0, this.GetRestInvestPoint() - materialsTotalExp) / (float) this.mTargetEnhanceMaterial.item.Param.facility_point);
      int num4 = this.mTargetEnhanceMaterial.item.Num - this.mTargetEnhanceMaterial.num;
      int num5 = num1;
      if (num5 > num3)
        num5 = num3;
      if (num5 > num4)
        num5 = num4;
      if (num5 <= 0)
        return;
      this.mFacilityEnhanceSlider.value += (float) num5;
    }

    [DebuggerHidden]
    private IEnumerator LevelupAnimation(GuildFacilityData facility, GuildFacilityEnhance.EffectCallBack callback)
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new GuildFacilityEnhance.\u003CLevelupAnimation\u003Ec__Iterator0() { callback = callback, \u0024this = this };
    }

    private void EndLevelupAnimation()
    {
      if ((UnityEngine.Object) GuildLobby.Instance != (UnityEngine.Object) null)
        GuildLobby.Instance.Refresh();
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 1020);
    }

    private enum eUIType
    {
      NONE,
      LIST,
      ENHANCE,
    }

    public delegate void EffectCallBack();
  }
}
