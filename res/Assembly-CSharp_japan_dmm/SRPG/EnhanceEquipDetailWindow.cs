// Decompiled with JetBrains decompiler
// Type: SRPG.EnhanceEquipDetailWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  [FlowNode.Pin(1, "Refresh", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(2, "Reset", FlowNode.PinTypes.Input, 2)]
  [FlowNode.Pin(3, "Max", FlowNode.PinTypes.Input, 3)]
  [FlowNode.Pin(4, "Clear", FlowNode.PinTypes.Input, 4)]
  [FlowNode.Pin(5, "全体強化確認", FlowNode.PinTypes.Input, 5)]
  [FlowNode.Pin(100, "強化", FlowNode.PinTypes.Output, 100)]
  [FlowNode.Pin(101, "全体強化開始", FlowNode.PinTypes.Output, 101)]
  [FlowNode.Pin(200, "コストが足りない", FlowNode.PinTypes.Output, 200)]
  [FlowNode.Pin(201, "素材が足りない", FlowNode.PinTypes.Output, 201)]
  [FlowNode.Pin(300, "  強化失敗", FlowNode.PinTypes.Output, 300)]
  public class EnhanceEquipDetailWindow : MonoBehaviour, IFlowInterface
  {
    private const int PIN_IN_REFRESH = 1;
    private const int PIN_IN_RESET = 2;
    private const int PIN_IN_MATERIA_MAX = 3;
    private const int PIN_IN_MATERIA_CLEAR = 4;
    private const int PIN_IN_STRENGEMAX = 5;
    private const int PIN_OUT_STRENGE = 100;
    private const int PIN_OUT_STRENGEMAX = 101;
    private const int PIN_OUT_STRENGECOSTERROR = 200;
    private const int PIN_OUT_STRENGEMATERROR = 201;
    private const int PIN_OUT_ERROR = 300;
    public GameObject Unit;
    public List<Button> Equipments;
    public List<RawImage> EquipmentRawImages;
    public List<GameObject> EquipmentCursors;
    public GameObject EmptyLabel;
    public Transform ParamUpLayoutParent;
    public GameObject ParamUpTemplate;
    public Transform ItemLayoutParent;
    public GameObject ItemTemplate;
    public GameObject EquipSelectParent;
    public GameObject SelectedParent;
    public GameObject EnhanceGaugeParent;
    public GameObject ExpUpTemplate;
    public Text TxtExpUpValue;
    public Button BtnJob;
    public Button BtnAdd;
    public Button BtnSub;
    public Button BtnEnhance;
    public Button BtnAllEnhance;
    public Button BtnMax;
    public Button BtnClear;
    public Text TxtJob;
    public Text TxtCost;
    public Text TxtComment;
    public Text TxtDisableEnhanceOnGauge;
    public Slider EquipSelectCountSlider;
    public readonly string ALL_EQUIPENHANCE_UI_PATH = "UI/EquipEnhanceConfirmWindow";
    [SerializeField]
    private ContentController mContentController;
    [CustomEnum(typeof (SystemSound.ECue), -1)]
    [SerializeField]
    private SystemSound.ECue SliderSound = SystemSound.ECue.ScrollList;
    private UnitData mUnit;
    private int mJobIndex;
    private GameObject mSelectedEquipItem;
    private EnhanceEquipData mEnhanceEquipData;
    private List<GameObject> mEnhanceParameters;
    private string mSelectedMaterialItemId = string.Empty;
    private List<EnhanceMaterial> mEnhanceMaterials;
    private Dictionary<string, EnhanceMaterial> mEnhanceMaterialsDict;
    private List<EnhanceMaterial> mEnableEnhanceMaterials;
    private List<ItemData> mMaterialItems;
    private List<ItemData> mEnhanceItems;
    private List<EnhanceEquipIconParam> mEnhanceEquipIconParam = new List<EnhanceEquipIconParam>();
    private Vector2 mAnchorPosition = Vector2.zero;
    private EnhanceEquipDetailWindow.EnhanceEquipTouchController mTouchControllerAdd;
    private EnhanceEquipDetailWindow.EnhanceEquipTouchController mTouchControllerSub;
    private bool endAddDialog;

    private void Start()
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.ItemTemplate, (UnityEngine.Object) null))
        return;
      this.CreateItemList();
      this.mEnhanceParameters = new List<GameObject>(5);
      this.mUnit = MonoSingleton<GameManager>.Instance.Player.FindUnitDataByUniqueID((long) GlobalVars.SelectedUnitUniqueID);
      this.mJobIndex = (int) GlobalVars.SelectedUnitJobIndex;
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.ItemTemplate, (UnityEngine.Object) null) && this.ItemTemplate.activeInHierarchy)
      {
        this.ItemTemplate.transform.SetSiblingIndex(0);
        this.ItemTemplate.SetActive(false);
      }
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.BtnJob, (UnityEngine.Object) null))
      {
        // ISSUE: method pointer
        ((UnityEvent) this.BtnJob.onClick).AddListener(new UnityAction((object) this, __methodptr(OnJobChange)));
      }
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.BtnAdd, (UnityEngine.Object) null))
      {
        // ISSUE: method pointer
        ((UnityEvent) this.BtnAdd.onClick).AddListener(new UnityAction((object) this, __methodptr(OnAddMaterial)));
        this.mTouchControllerAdd = ((Component) this.BtnAdd).gameObject.AddComponent<EnhanceEquipDetailWindow.EnhanceEquipTouchController>();
        this.mTouchControllerAdd.OnPointerDownFunc = new EnhanceEquipDetailWindow.EnhanceEquipTouchController.DelegateOnPointerDown(this.OnLevelDown);
        this.mTouchControllerAdd.OnPointerUpFunc = new EnhanceEquipDetailWindow.EnhanceEquipTouchController.DelegateOnPointerUp(this.OnLevelUp);
        this.mTouchControllerAdd.LevelupFunc = new EnhanceEquipDetailWindow.EnhanceEquipTouchController.DelegateLevelUp(this.OnAddMaterial);
      }
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.BtnSub, (UnityEngine.Object) null))
      {
        // ISSUE: method pointer
        ((UnityEvent) this.BtnSub.onClick).AddListener(new UnityAction((object) this, __methodptr(OnSubMaterial)));
        this.mTouchControllerSub = ((Component) this.BtnSub).gameObject.AddComponent<EnhanceEquipDetailWindow.EnhanceEquipTouchController>();
        this.mTouchControllerSub.OnPointerDownFunc = new EnhanceEquipDetailWindow.EnhanceEquipTouchController.DelegateOnPointerDown(this.OnLevelDown);
        this.mTouchControllerSub.OnPointerUpFunc = new EnhanceEquipDetailWindow.EnhanceEquipTouchController.DelegateOnPointerUp(this.OnLevelUp);
        this.mTouchControllerSub.LevelupFunc = new EnhanceEquipDetailWindow.EnhanceEquipTouchController.DelegateLevelUp(this.OnSubMaterial);
      }
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.BtnEnhance, (UnityEngine.Object) null))
      {
        // ISSUE: method pointer
        ((UnityEvent) this.BtnEnhance.onClick).AddListener(new UnityAction((object) this, __methodptr(OnEnhance)));
      }
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.TxtComment, (UnityEngine.Object) null))
        this.TxtComment.text = LocalizedText.Get("sys.ENHANCE_EQUIPMENT_MESSAGE");
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.TxtDisableEnhanceOnGauge, (UnityEngine.Object) null))
        this.TxtDisableEnhanceOnGauge.text = LocalizedText.Get("sys.DIABLE_ENHANCE_MESSAGE");
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.EquipSelectCountSlider, (UnityEngine.Object) null))
      {
        this.EquipSelectCountSlider.minValue = 0.0f;
        this.EquipSelectCountSlider.maxValue = 0.0f;
        this.EquipSelectCountSlider.value = 0.0f;
        // ISSUE: method pointer
        ((UnityEvent<float>) this.EquipSelectCountSlider.onValueChanged).AddListener(new UnityAction<float>((object) this, __methodptr(OnSelectMaterial)));
      }
      for (int index = 0; index < this.Equipments.Count; ++index)
      {
        // ISSUE: object of a compiler-generated type is created
        // ISSUE: method pointer
        ((UnityEvent) this.Equipments[index].onClick).AddListener(new UnityAction((object) new EnhanceEquipDetailWindow.\u003CStart\u003Ec__AnonStorey0()
        {
          \u0024this = this,
          slot = index
        }, __methodptr(\u003C\u003Em__0)));
      }
      this.RefreshData();
      ((Behaviour) this).enabled = true;
    }

    public void Activated(int pinID)
    {
      switch (pinID)
      {
        case 1:
          this.CreateItemList();
          this.mUnit = MonoSingleton<GameManager>.Instance.Player.FindUnitDataByUniqueID((long) GlobalVars.SelectedUnitUniqueID);
          this.mJobIndex = (int) GlobalVars.SelectedUnitJobIndex;
          this.ClearEnhancedMaterial();
          this.RefreshData();
          break;
        case 2:
          this.CreateItemList();
          this.mUnit = MonoSingleton<GameManager>.Instance.Player.FindUnitDataByUniqueID((long) GlobalVars.SelectedUnitUniqueID);
          this.mJobIndex = (int) GlobalVars.SelectedUnitJobIndex;
          this.mSelectedEquipItem = (GameObject) null;
          this.ClearEnhancedMaterial();
          this.RefreshData();
          break;
        case 3:
          this.OnMaxMaterial();
          break;
        case 4:
          this.OnClearMaterial();
          break;
        case 5:
          this.AllEquipmentEnhance();
          break;
      }
    }

    private void Update()
    {
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mTouchControllerAdd, (UnityEngine.Object) null))
        this.mTouchControllerAdd.UpdateTimer(Time.unscaledDeltaTime);
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mTouchControllerSub, (UnityEngine.Object) null))
        return;
      this.mTouchControllerSub.UpdateTimer(Time.unscaledDeltaTime);
    }

    private void RefreshData()
    {
      JobData jobData = this.mUnit.GetJobData(this.mJobIndex);
      DataSource.Bind<UnitData>(this.Unit, this.mUnit);
      bool flag1 = false;
      bool active = true;
      for (int index = 0; index < this.Equipments.Count; ++index)
      {
        EquipData equip = jobData.Equips[index];
        DataSource.Bind<EquipData>(((Component) this.Equipments[index]).gameObject, equip);
        bool flag2 = equip != null && equip.IsValid() && equip.IsEquiped();
        ((Selectable) this.Equipments[index]).interactable = flag2;
        ((Component) this.EquipmentRawImages[index]).gameObject.SetActive(flag2);
        this.EquipmentCursors[index].SetActive(UnityEngine.Object.op_Equality((UnityEngine.Object) this.mSelectedEquipItem, (UnityEngine.Object) ((Component) this.Equipments[index]).gameObject));
        if (equip.Rank < equip.GetRankCap() && flag2)
          flag1 = true;
        if (flag2)
          active = false;
      }
      GameUtility.SetGameObjectActive(this.EmptyLabel, active);
      this.mEnhanceEquipData.equip = (EquipData) null;
      this.mEnhanceEquipData.gainexp = 0;
      this.mEnhanceEquipData.is_enhanced = false;
      for (int index = 0; index < this.mEnhanceParameters.Count; ++index)
        this.mEnhanceParameters[index].SetActive(false);
      ((Selectable) this.BtnEnhance).interactable = false;
      EquipData dataOfClass = !UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mSelectedEquipItem, (UnityEngine.Object) null) ? (EquipData) null : DataSource.FindDataOfClass<EquipData>(this.mSelectedEquipItem, (EquipData) null);
      int num1 = 0;
      int num2 = 0;
      for (int index = 0; index < this.mMaterialItems.Count; ++index)
      {
        ItemData mMaterialItem = this.mMaterialItems[index];
        EnhanceMaterial enhanceMaterial = (EnhanceMaterial) null;
        if (this.mEnhanceMaterialsDict.ContainsKey(mMaterialItem.ItemID))
          enhanceMaterial = this.mEnhanceMaterialsDict[mMaterialItem.ItemID];
        if (dataOfClass != null && enhanceMaterial != null)
        {
          num1 += mMaterialItem.Param.enhace_cost * dataOfClass.GetEnhanceCostScale() / 100 * enhanceMaterial.num;
          num2 += mMaterialItem.Param.enhace_point * enhanceMaterial.num;
        }
      }
      int num3 = 0;
      if (this.mEnhanceItems != null)
      {
        for (int index = 0; index < this.mEnhanceItems.Count; ++index)
          num3 += this.mEnhanceItems[index].Num;
      }
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.BtnAllEnhance, (UnityEngine.Object) null))
        ((Selectable) this.BtnAllEnhance).interactable = flag1 && num3 > 0;
      bool flag3 = false;
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.EquipSelectParent, (UnityEngine.Object) null))
      {
        DataSource.Bind<EnhanceEquipData>(this.EquipSelectParent, (EnhanceEquipData) null);
        this.EquipSelectParent.gameObject.SetActive(false);
      }
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.TxtComment, (UnityEngine.Object) null))
        ((Component) this.TxtComment).gameObject.SetActive(false);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mSelectedEquipItem, (UnityEngine.Object) null))
      {
        this.mEnhanceEquipData.equip = dataOfClass;
        this.mEnhanceEquipData.gainexp = num2;
        if (dataOfClass != null)
        {
          BuffEffect buffEffect = dataOfClass.Skill.GetBuffEffect();
          if (buffEffect != null && buffEffect.targets != null)
          {
            for (int index = 0; index < buffEffect.targets.Count; ++index)
            {
              if (index >= this.mEnhanceParameters.Count)
              {
                this.ParamUpTemplate.SetActive(true);
                GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.ParamUpTemplate);
                gameObject.transform.SetParent(this.ParamUpLayoutParent, false);
                this.mEnhanceParameters.Add(gameObject);
                this.ParamUpTemplate.SetActive(false);
              }
              GameObject enhanceParameter = this.mEnhanceParameters[index];
              EquipItemParameter data = DataSource.FindDataOfClass<EquipItemParameter>(enhanceParameter, (EquipItemParameter) null) ?? new EquipItemParameter();
              data.equip = dataOfClass;
              data.param_index = index;
              DataSource.Bind<EquipItemParameter>(enhanceParameter, data);
              enhanceParameter.SetActive(true);
            }
          }
          flag3 = true;
          if (dataOfClass.Rank == dataOfClass.GetRankCap())
          {
            if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.TxtComment, (UnityEngine.Object) null))
            {
              this.TxtComment.text = LocalizedText.Get("sys.DIABLE_ENHANCE_RANKCAP_MESSAGE");
              ((Component) this.TxtComment).gameObject.SetActive(true);
            }
            flag3 = false;
          }
        }
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.EquipSelectParent, (UnityEngine.Object) null))
        {
          DataSource.Bind<EnhanceEquipData>(this.EquipSelectParent, this.mEnhanceEquipData);
          this.EquipSelectParent.gameObject.SetActive(true);
        }
      }
      else if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.TxtComment, (UnityEngine.Object) null))
      {
        this.TxtComment.text = LocalizedText.Get("sys.ENHANCE_EQUIPMENT_MESSAGE");
        ((Component) this.TxtComment).gameObject.SetActive(true);
      }
      this.mEnhanceEquipData.is_enhanced = flag3;
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.SelectedParent, (UnityEngine.Object) null))
      {
        DataSource.Bind<EnhanceEquipData>(this.SelectedParent, this.mEnhanceEquipData);
        this.SelectedParent.gameObject.SetActive(flag3);
      }
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.TxtDisableEnhanceOnGauge, (UnityEngine.Object) null))
        ((Component) this.TxtDisableEnhanceOnGauge).gameObject.SetActive(!flag3);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.TxtCost, (UnityEngine.Object) null))
      {
        int overExp = this.CheckEquipItemEnhanceOverflow(dataOfClass);
        if (overExp != 0)
        {
          List<ItemData> list = new List<ItemData>();
          for (int index = 0; index < this.mEnhanceMaterials.Count; ++index)
          {
            if (this.mEnhanceMaterials[index].num > 0)
              list.Add(this.mEnhanceMaterials[index].item);
          }
          num1 += this.GetJustCost(dataOfClass, list, overExp);
        }
        this.TxtCost.text = num1.ToString();
      }
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.TxtJob, (UnityEngine.Object) null))
        this.TxtJob.text = jobData.Name;
      if (flag3 && dataOfClass != null)
      {
        int num4 = dataOfClass.CalcRankFromExp(dataOfClass.Exp + num2);
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.BtnEnhance, (UnityEngine.Object) null))
          ((Selectable) this.BtnEnhance).interactable = dataOfClass.Rank < num4;
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.BtnMax, (UnityEngine.Object) null))
          ((Selectable) this.BtnMax).interactable = num4 < dataOfClass.GetRankCap() && num3 > 0;
      }
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.BtnAdd, (UnityEngine.Object) null))
        ((Selectable) this.BtnAdd).interactable = flag3 && !string.IsNullOrEmpty(this.mSelectedMaterialItemId);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.BtnSub, (UnityEngine.Object) null))
        ((Selectable) this.BtnSub).interactable = flag3 && !string.IsNullOrEmpty(this.mSelectedMaterialItemId);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.BtnClear, (UnityEngine.Object) null))
        ((Selectable) this.BtnClear).interactable = num2 > 0;
      this.mEnableEnhanceMaterials.Clear();
      for (int index = 0; index < this.mEnhanceMaterials.Count; ++index)
      {
        if (this.mEnhanceMaterials[index].item != null && this.mEnhanceMaterials[index].item.Num != 0)
          this.mEnableEnhanceMaterials.Add(this.mEnhanceMaterials[index]);
      }
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.EquipSelectCountSlider, (UnityEngine.Object) null))
      {
        if (string.IsNullOrEmpty(this.mSelectedMaterialItemId))
        {
          ((UnityEventBase) this.EquipSelectCountSlider.onValueChanged).RemoveAllListeners();
          Slider selectCountSlider = this.EquipSelectCountSlider;
          float num5 = 0.0f;
          this.EquipSelectCountSlider.maxValue = num5;
          double num6 = (double) num5;
          selectCountSlider.minValue = (float) num6;
          this.EquipSelectCountSlider.value = 0.0f;
          // ISSUE: method pointer
          ((UnityEvent<float>) this.EquipSelectCountSlider.onValueChanged).AddListener(new UnityAction<float>((object) this, __methodptr(OnSelectMaterial)));
        }
        if ((double) this.EquipSelectCountSlider.value == (double) this.EquipSelectCountSlider.minValue && UnityEngine.Object.op_Inequality((UnityEngine.Object) this.BtnSub, (UnityEngine.Object) null))
          ((Selectable) this.BtnSub).interactable = false;
        if ((double) this.EquipSelectCountSlider.value == (double) this.EquipSelectCountSlider.maxValue && UnityEngine.Object.op_Inequality((UnityEngine.Object) this.BtnAdd, (UnityEngine.Object) null))
          ((Selectable) this.BtnAdd).interactable = false;
      }
      GameParameter.UpdateAll(((Component) this).gameObject);
    }

    private void OnJobChange()
    {
      int mJobIndex = this.mJobIndex;
      do
      {
        this.mJobIndex = ++this.mJobIndex % this.mUnit.JobCount;
      }
      while (!this.mUnit.GetJobData(this.mJobIndex).IsActivated);
      if (mJobIndex == this.mJobIndex)
        return;
      this.mSelectedEquipItem = (GameObject) null;
      this.mSelectedMaterialItemId = string.Empty;
      this.ClearEnhancedMaterial();
      this.RefreshData();
    }

    private void OnSelectEquipment(int slot)
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.mSelectedEquipItem, (UnityEngine.Object) ((Component) this.Equipments[slot]).gameObject))
        return;
      this.ClearEnhancedMaterial();
      this.ClearMaterialSelect();
      this.mSelectedEquipItem = ((Component) this.Equipments[slot]).gameObject;
      GlobalVars.SelectedEquipmentSlot.Set(slot);
      this.RefreshData();
    }

    public void OnItemSelect(GameObject go)
    {
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) go, (UnityEngine.Object) null))
        return;
      EnhanceMaterial dataOfClass = DataSource.FindDataOfClass<EnhanceMaterial>(go, (EnhanceMaterial) null);
      int num1 = dataOfClass.num;
      if (dataOfClass == null || !(this.mSelectedMaterialItemId != dataOfClass.item.ItemID))
        return;
      this.mSelectedMaterialItemId = dataOfClass.item.ItemID;
      for (int index = 0; index < this.mEnhanceMaterials.Count; ++index)
        this.mEnhanceMaterials[index].selected = dataOfClass == this.mEnhanceMaterials[index];
      if (this.mEnhanceEquipData != null && UnityEngine.Object.op_Inequality((UnityEngine.Object) this.EquipSelectCountSlider, (UnityEngine.Object) null))
      {
        int num2 = Math.Min(this.GetEquipItemEnhanceCount(dataOfClass.item, num1, this.mEnhanceEquipData.gainexp, true) + num1, dataOfClass.item.Num);
        ((UnityEventBase) this.EquipSelectCountSlider.onValueChanged).RemoveAllListeners();
        this.EquipSelectCountSlider.minValue = 0.0f;
        this.EquipSelectCountSlider.maxValue = (float) num2;
        this.EquipSelectCountSlider.value = (float) num1;
        // ISSUE: method pointer
        ((UnityEvent<float>) this.EquipSelectCountSlider.onValueChanged).AddListener(new UnityAction<float>((object) this, __methodptr(OnSelectMaterial)));
      }
      this.RefreshData();
    }

    private void AllEquipmentEnhance()
    {
      this.mSelectedMaterialItemId = string.Empty;
      this.ClearEnhancedMaterial();
      this.RefreshData();
      GameObject selectedEquipItem = this.mSelectedEquipItem;
      EquipData equip1 = this.mEnhanceEquipData.equip;
      if (this.mEnhanceItems == null || this.mEnhanceItems.Count == 0)
        return;
      PlayerData player = MonoSingleton<GameManager>.Instance.Player;
      int[] numArray = new int[this.mEnhanceItems.Count];
      int cost = 0;
      for (int index = 0; index < this.mEnhanceItems.Count; ++index)
        numArray[index] = this.mEnhanceItems[index].Num;
      JobData jobData = this.mUnit.GetJobData(this.mJobIndex);
      int data = 0;
      for (int index1 = 0; index1 < this.Equipments.Count; ++index1)
      {
        if (!UnityEngine.Object.op_Equality((UnityEngine.Object) ((Component) this.Equipments[index1]).gameObject, (UnityEngine.Object) null))
        {
          bool flag = false;
          EquipData equip2 = jobData.Equips[index1];
          if ((equip2 == null ? 0 : (!equip2.IsValid() ? 0 : (equip2.IsEquiped() ? 1 : 0))) != 0)
          {
            int num1 = 0;
            this.mSelectedEquipItem = ((Component) this.Equipments[index1]).gameObject;
            EquipData dataOfClass = DataSource.FindDataOfClass<EquipData>(this.mSelectedEquipItem, (EquipData) null);
            this.mEnhanceEquipData.equip = !UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mSelectedEquipItem, (UnityEngine.Object) null) ? (EquipData) null : DataSource.FindDataOfClass<EquipData>(this.mSelectedEquipItem, (EquipData) null);
            for (int index2 = 0; index2 < this.mEnhanceItems.Count; ++index2)
            {
              int itemEnhanceCount = this.GetEquipItemEnhanceCount(this.mEnhanceItems[index2], startExp: num1);
              if (itemEnhanceCount > numArray[index2])
                itemEnhanceCount = numArray[index2];
              if (itemEnhanceCount > 0)
              {
                num1 += itemEnhanceCount * this.mEnhanceMaterials[index2].item.Param.enhace_point;
                cost += this.mEnhanceItems[index2].Param.enhace_cost * dataOfClass.GetEnhanceCostScale() / 100 * itemEnhanceCount;
                numArray[index2] -= itemEnhanceCount;
                flag = true;
              }
            }
            if (this.CheckEquipItemEnhance(num1))
            {
              for (int index3 = this.mEnhanceItems.Count - 1; index3 >= 0; --index3)
              {
                if (numArray[index3] > 0)
                {
                  num1 += this.mEnhanceMaterials[index3].item.Param.enhace_point;
                  cost += this.mEnhanceItems[index3].Param.enhace_cost * dataOfClass.GetEnhanceCostScale() / 100;
                  --numArray[index3];
                  flag = true;
                  break;
                }
              }
            }
            if (flag)
            {
              int num2 = this.mEnhanceEquipData.equip.CalcRankFromExp(this.mEnhanceEquipData.equip.Exp + num1) - this.mEnhanceEquipData.equip.CalcRankFromExp(this.mEnhanceEquipData.equip.Exp);
              if (num2 > 0)
                data += num2;
            }
            int overExp = this.CheckEquipItemEnhanceOverflow(dataOfClass, num1);
            if (overExp != 0)
              cost += this.GetJustCost(dataOfClass, this.mEnhanceItems, overExp);
          }
        }
      }
      this.mSelectedEquipItem = selectedEquipItem;
      this.mEnhanceEquipData.equip = equip1;
      GameObject gameObject1 = AssetManager.Load<GameObject>(this.ALL_EQUIPENHANCE_UI_PATH);
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) gameObject1, (UnityEngine.Object) null))
      {
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 300);
      }
      else
      {
        GameObject gameObject2 = UnityEngine.Object.Instantiate<GameObject>(gameObject1);
        if (UnityEngine.Object.op_Equality((UnityEngine.Object) gameObject2, (UnityEngine.Object) null))
        {
          FlowNode_GameObject.ActivateOutputLinks((Component) this, 300);
        }
        else
        {
          EquipEnhanceConfirmWindow componentInChildren = gameObject2.GetComponentInChildren<EquipEnhanceConfirmWindow>();
          if (UnityEngine.Object.op_Equality((UnityEngine.Object) componentInChildren, (UnityEngine.Object) null))
          {
            FlowNode_GameObject.ActivateOutputLinks((Component) this, 300);
          }
          else
          {
            bool flag = true;
            for (int index = 0; index < this.mEnhanceItems.Count; ++index)
            {
              if (this.mEnhanceItems[index].Num - numArray[index] > 0)
              {
                componentInChildren.SetupItem(this.mEnhanceItems[index], this.mEnhanceItems[index].Num - numArray[index]);
                flag = false;
              }
            }
            if (flag)
            {
              FlowNode_GameObject.ActivateOutputLinks((Component) this, 300);
            }
            else
            {
              if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.BtnEnhance, (UnityEngine.Object) null))
                ((Selectable) this.BtnEnhance).interactable = false;
              if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.BtnAllEnhance, (UnityEngine.Object) null))
                ((Selectable) this.BtnAllEnhance).interactable = false;
              componentInChildren.SetupCost(cost);
              componentInChildren.OnSelectedEvent = new EquipEnhanceConfirmWindow.OnEnhanceSelectedEvent(this.OnAllEquipEnhanceEvent);
              componentInChildren.OnCanceledEvent = new EquipEnhanceConfirmWindow.OnEnhanceSelectedEvent(this.OnAllEquipEnhanceCancelEvent);
              if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) ((Component) ((Component) this).transform.root).gameObject, (UnityEngine.Object) null))
                return;
              DataSource.Bind<int>(((Component) ((Component) this).transform.root).gameObject, data);
            }
          }
        }
      }
    }

    private void OnAllEquipEnhanceEvent()
    {
      GlobalVars.SelectedUnitUniqueID.Set(this.mUnit.UniqueID);
      GlobalVars.SelectedUnitJobIndex.Set(this.mJobIndex);
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 101);
    }

    private void OnAllEquipEnhanceCancelEvent() => this.RefreshData();

    private void EnhanceLimitedMaterials(int addExp)
    {
      if (!this.CheckEquipItemEnhance(addExp))
        return;
      for (int index = this.mMaterialItems.Count - 1; index >= 0; --index)
      {
        if (this.mMaterialItems[index] != null && this.mMaterialItems[index].ItemType == EItemType.ExpUpEquip && this.mMaterialItems[index].Num > this.mEnhanceMaterials[index].num)
        {
          ++this.mEnhanceMaterials[index].num;
          this.mEnhanceMaterials[index].selected = true;
          break;
        }
      }
    }

    private void OnMaxMaterial()
    {
      this.mSelectedMaterialItemId = string.Empty;
      this.ClearEnhancedMaterial();
      this.RefreshData();
      int num = 0;
      for (int index = 0; index < this.mMaterialItems.Count; ++index)
      {
        if (this.mMaterialItems[index] != null && this.mMaterialItems[index].ItemType == EItemType.ExpUpEquip)
        {
          int itemEnhanceCount = this.GetEquipItemEnhanceCount(this.mMaterialItems[index], startExp: num);
          if (itemEnhanceCount > 0)
          {
            this.mEnhanceMaterials[index].num = itemEnhanceCount;
            num += this.mEnhanceMaterials[index].num * this.mEnhanceMaterials[index].item.Param.enhace_point;
            this.mEnhanceMaterials[index].iconParam.Refresh();
          }
        }
        else
          break;
      }
      this.EnhanceLimitedMaterials(num);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mContentController, (UnityEngine.Object) null))
      {
        this.mContentController.scroller.StopMovement();
        this.mContentController.anchoredPosition = Vector2.zero;
      }
      this.RefreshData();
    }

    private void OnClearMaterial()
    {
      this.mSelectedMaterialItemId = string.Empty;
      this.ClearEnhancedMaterial();
      this.RefreshData();
    }

    private void OnAddMaterial()
    {
      if (string.IsNullOrEmpty(this.mSelectedMaterialItemId))
        return;
      EnhanceMaterial enhanceMaterial = (EnhanceMaterial) null;
      if (this.mEnhanceMaterialsDict.ContainsKey(this.mSelectedMaterialItemId))
        enhanceMaterial = this.mEnhanceMaterialsDict[this.mSelectedMaterialItemId];
      if (enhanceMaterial == null)
        return;
      if (!this.CheckEquipItemEnhance())
      {
        if (this.endAddDialog)
          return;
        UIUtility.NegativeSystemMessage(LocalizedText.Get("sys.FAILED_ENHANCE"), LocalizedText.Get("sys.DIABLE_ENHANCE_RANKCAP_MESSAGE"), (UIUtility.DialogResultEvent) (go => this.endAddDialog = false));
        this.endAddDialog = true;
      }
      else
      {
        enhanceMaterial.num = Math.Min(++enhanceMaterial.num, enhanceMaterial.item.Num);
        if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.EquipSelectCountSlider, (UnityEngine.Object) null))
          return;
        this.EquipSelectCountSlider.value = (float) enhanceMaterial.num;
      }
    }

    private void OnSubMaterial()
    {
      if (string.IsNullOrEmpty(this.mSelectedMaterialItemId))
        return;
      EnhanceMaterial enhanceMaterial = (EnhanceMaterial) null;
      if (this.mEnhanceMaterialsDict.ContainsKey(this.mSelectedMaterialItemId))
        enhanceMaterial = this.mEnhanceMaterialsDict[this.mSelectedMaterialItemId];
      if (enhanceMaterial == null)
        return;
      enhanceMaterial.num = Math.Max(--enhanceMaterial.num, 0);
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.EquipSelectCountSlider, (UnityEngine.Object) null))
        return;
      this.EquipSelectCountSlider.value = (float) enhanceMaterial.num;
    }

    private void OnLevelDown(
      EnhanceEquipDetailWindow.EnhanceEquipTouchController controller)
    {
    }

    private void OnLevelUp(
      EnhanceEquipDetailWindow.EnhanceEquipTouchController controller)
    {
    }

    private bool CheckEquipItemEnhance(int addExp = 0)
    {
      if (this.mEnhanceEquipData == null || this.mEnhanceEquipData.equip == null)
        return false;
      EquipData equip = this.mEnhanceEquipData.equip;
      int current = equip.Exp + this.mEnhanceEquipData.gainexp + addExp;
      return equip.CalcRankFromExp(current) < equip.GetRankCap();
    }

    private int CheckEquipItemEnhanceOverflow(EquipData equip, int addExp = 0)
    {
      if (this.mEnhanceEquipData == null || this.mEnhanceEquipData.equip == null)
        return 0;
      int num1 = equip.Exp + this.mEnhanceEquipData.gainexp + addExp;
      int num2 = equip.CalcRankFromExp(equip.Exp);
      int rankCap = equip.GetRankCap();
      int needExp = equip.GetNeedExp(rankCap);
      return needExp - num1 < 0 && num2 < rankCap ? Math.Abs(needExp - num1) : 0;
    }

    private int GetJustCost(EquipData equip, List<ItemData> list, int overExp)
    {
      if (list == null && list.Count == 0)
        return 0;
      int index1 = 0;
      float num1 = (float) (list[index1].Param.enhace_cost * equip.GetEnhanceCostScale()) / 100f / (float) list[index1].Param.enhace_point;
      for (int index2 = 1; index2 < list.Count; ++index2)
      {
        float num2 = (float) (list[index2].Param.enhace_cost * equip.GetEnhanceCostScale()) / 100f / (float) list[index2].Param.enhace_point;
        if ((double) num1 > (double) num2)
          index1 = index2;
      }
      int num3 = list[index1].Param.enhace_point - overExp;
      int num4 = list[index1].Param.enhace_cost * equip.GetEnhanceCostScale() / 100;
      float num5 = (float) num4 / (float) list[index1].Param.enhace_point;
      return -num4 + (int) ((double) num3 * (double) num5);
    }

    private void OnEnhance()
    {
      bool flag = false;
      for (int index = 0; index < this.mEnhanceMaterials.Count; ++index)
      {
        EnhanceMaterial mEnhanceMaterial = this.mEnhanceMaterials[index];
        if (mEnhanceMaterial.num != 0 && (mEnhanceMaterial.item.Rarity > 1 || mEnhanceMaterial.item.ItemType == EItemType.Material))
          flag = true;
      }
      GlobalVars.SelectedUnitUniqueID.Set(this.mUnit.UniqueID);
      GlobalVars.SelectedUnitJobIndex.Set(this.mJobIndex);
      GlobalVars.SelectedEquipData = DataSource.FindDataOfClass<EquipData>(this.mSelectedEquipItem, (EquipData) null);
      GlobalVars.SelectedEnhanceMaterials = this.mEnhanceMaterials;
      if (flag)
        UIUtility.ConfirmBox(LocalizedText.Get("sys.ENHANCE_ITEM_RARITY_CAUTION"), (string) null, new UIUtility.DialogResultEvent(this.OnDecide), new UIUtility.DialogResultEvent(this.OnCancel));
      else
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 100);
    }

    private void OnDecide(GameObject go)
    {
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 100);
    }

    private void OnCancel(GameObject go)
    {
      GlobalVars.SelectedEquipData = (EquipData) null;
      GlobalVars.SelectedEnhanceMaterials = (List<EnhanceMaterial>) null;
    }

    private void ClearEnhancedMaterial()
    {
      for (int index = 0; index < this.mEnhanceMaterials.Count; ++index)
      {
        this.mEnhanceMaterials[index].num = 0;
        this.mEnhanceMaterials[index].selected = false;
      }
    }

    public void ClearMaterialSelect()
    {
      this.mSelectedMaterialItemId = string.Empty;
      for (int index = 0; index < this.mEnhanceMaterials.Count; ++index)
        this.mEnhanceMaterials[index].selected = false;
    }

    private int GetEquipItemEnhanceCount(
      ItemData item,
      int selectedCount = 0,
      int startExp = 0,
      bool allowOverflow = false)
    {
      if (this.mEnhanceEquipData == null || this.mEnhanceEquipData.equip == null || item == null || item.Param == null)
        return 0;
      int enhacePoint = item.Param.enhace_point;
      if (enhacePoint <= 0 || item.Num - selectedCount <= 0)
        return 0;
      EquipData equip = this.mEnhanceEquipData.equip;
      int itemEnhanceCount = 0;
      while (true)
      {
        int current = equip.Exp + itemEnhanceCount * enhacePoint + startExp;
        if (equip.CalcRankFromExp(current) < equip.GetRankCap())
        {
          if (itemEnhanceCount < item.Num)
            ++itemEnhanceCount;
          else
            goto label_8;
        }
        else
          break;
      }
      bool flag = true;
      if (allowOverflow || !flag || itemEnhanceCount <= 0)
        return itemEnhanceCount;
      int num = enhacePoint - 1;
      if (num > 0)
      {
        int current = equip.Exp + (itemEnhanceCount - 1) * enhacePoint + num + startExp;
        if (equip.CalcRankFromExp(current) >= equip.GetRankCap())
          --itemEnhanceCount;
      }
      return itemEnhanceCount;
label_8:
      return itemEnhanceCount;
    }

    private void OnSelectMaterial(float value)
    {
      if (string.IsNullOrEmpty(this.mSelectedMaterialItemId))
        return;
      EnhanceMaterial enhanceMaterial = (EnhanceMaterial) null;
      if (this.mEnhanceMaterialsDict.ContainsKey(this.mSelectedMaterialItemId))
        enhanceMaterial = this.mEnhanceMaterialsDict[this.mSelectedMaterialItemId];
      if (enhanceMaterial != null)
      {
        enhanceMaterial.num = Math.Min((int) value, enhanceMaterial.item.Num);
        enhanceMaterial.iconParam.Refresh();
      }
      SystemSound.Play(this.SliderSound);
      this.RefreshData();
    }

    private void CreateItemList()
    {
      List<ItemData> items = MonoSingleton<GameManager>.Instance.Player.Items;
      this.mMaterialItems = new List<ItemData>(items.Count);
      this.mEnhanceItems = new List<ItemData>();
      for (int index = 0; index < items.Count; ++index)
      {
        if (items[index].CheckEquipEnhanceMaterial() && !items[index].Param.is_valuables && items[index].Num > 0 && items[index].Param.enhace_point > 0)
          this.mMaterialItems.Add(items[index]);
      }
      this.mMaterialItems.Sort((Comparison<ItemData>) ((src, dsc) =>
      {
        if (src.ItemType != dsc.ItemType)
        {
          if (src.ItemType == EItemType.ExpUpEquip)
            return -1;
          if (dsc.ItemType == EItemType.ExpUpEquip)
            return 1;
          if (src.ItemType == EItemType.ExpGuildFacility)
            return -1;
          if (dsc.ItemType == EItemType.ExpGuildFacility)
            return 1;
        }
        return dsc.Param.enhace_point - src.Param.enhace_point;
      }));
      for (int index = 0; index < this.mMaterialItems.Count; ++index)
      {
        if (this.mMaterialItems[index].ItemType == EItemType.ExpUpEquip)
          this.mEnhanceItems.Add(this.mMaterialItems[index]);
      }
      this.mEnhanceEquipData = new EnhanceEquipData();
      this.mEnhanceMaterials = new List<EnhanceMaterial>(this.mMaterialItems.Count);
      this.mEnhanceMaterialsDict = new Dictionary<string, EnhanceMaterial>(this.mMaterialItems.Count);
      this.mEnableEnhanceMaterials = new List<EnhanceMaterial>(this.mMaterialItems.Count);
      this.mSelectedEquipItem = (GameObject) null;
      this.mSelectedMaterialItemId = string.Empty;
      ContentSource source = new ContentSource();
      this.mEnhanceEquipIconParam.Clear();
      this.mEnhanceMaterialsDict.Clear();
      for (int index = 0; index < this.mMaterialItems.Count; ++index)
      {
        ItemData mMaterialItem = this.mMaterialItems[index];
        EnhanceMaterial enhanceMaterial1 = (EnhanceMaterial) null;
        if (this.mEnhanceMaterialsDict.ContainsKey(mMaterialItem.ItemID))
          enhanceMaterial1 = this.mEnhanceMaterialsDict[mMaterialItem.ItemID];
        if (enhanceMaterial1 == null)
        {
          EnhanceMaterial enhanceMaterial2 = new EnhanceMaterial();
          enhanceMaterial2.item = mMaterialItem;
          enhanceMaterial2.num = 0;
          this.mEnhanceMaterials.Add(enhanceMaterial2);
          enhanceMaterial1 = enhanceMaterial2;
        }
        EnhanceEquipIconParam enhanceEquipIconParam = new EnhanceEquipIconParam();
        enhanceEquipIconParam.mEnhanceMaterial = enhanceMaterial1;
        enhanceEquipIconParam.Initialize(source);
        this.mEnhanceEquipIconParam.Add(enhanceEquipIconParam);
        enhanceMaterial1.iconParam = enhanceEquipIconParam;
        if (!this.mEnhanceMaterialsDict.ContainsKey(mMaterialItem.ItemID))
          this.mEnhanceMaterialsDict[mMaterialItem.ItemID] = enhanceMaterial1;
      }
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mContentController, (UnityEngine.Object) null))
        return;
      this.mAnchorPosition = this.mContentController.GetAnchorePos();
      source.SetTable((ContentSource.Param[]) this.mEnhanceEquipIconParam.ToArray());
      this.mContentController.Initialize(source, this.mAnchorPosition);
      this.mContentController.ForceUpdate();
    }

    public class EnhanceEquipTouchController : 
      MonoBehaviour,
      IPointerDownHandler,
      IEventSystemHandler
    {
      public EnhanceEquipDetailWindow.EnhanceEquipTouchController.DelegateOnPointerDown OnPointerDownFunc;
      public EnhanceEquipDetailWindow.EnhanceEquipTouchController.DelegateOnPointerUp OnPointerUpFunc;
      public EnhanceEquipDetailWindow.EnhanceEquipTouchController.DelegateLevelUp LevelupFunc;
      public float UseLevelSpan = 0.25f;
      public float HoldDuration;
      public bool Holding;
      private int UseNum;
      private int NextSettingIndex;
      private Vector2 mDragStartPos;

      public void OnPointerDown(PointerEventData eventData)
      {
        if (this.OnPointerDownFunc == null)
          return;
        this.OnPointerDownFunc(this);
        this.Holding = true;
        this.mDragStartPos = eventData.position;
      }

      public void OnPointerUp()
      {
        if (this.OnPointerUpFunc != null)
          this.OnPointerUpFunc(this);
        this.StatusReset();
      }

      public void OnDestroy()
      {
        this.StatusReset();
        if (this.OnPointerDownFunc != null)
          this.OnPointerDownFunc = (EnhanceEquipDetailWindow.EnhanceEquipTouchController.DelegateOnPointerDown) null;
        if (this.OnPointerUpFunc == null)
          return;
        this.OnPointerUpFunc = (EnhanceEquipDetailWindow.EnhanceEquipTouchController.DelegateOnPointerUp) null;
      }

      public void UpdateTimer(float deltaTime)
      {
        if (this.LevelupFunc == null)
          return;
        bool mouseButton = Input.GetMouseButton(0);
        if (this.Holding && !mouseButton)
        {
          if ((double) this.HoldDuration < (double) this.UseLevelSpan && this.UseNum < 1)
            ++this.UseNum;
          this.OnPointerUp();
        }
        else
        {
          GameSettings instance = GameSettings.Instance;
          float num = (float) (instance.HoldMargin * instance.HoldMargin);
          Vector2 vector2 = Vector2.op_Subtraction(this.mDragStartPos, Vector2.op_Implicit(Input.mousePosition));
          bool flag = (double) ((Vector2) ref vector2).sqrMagnitude > (double) num;
          if ((double) this.HoldDuration < (double) this.UseLevelSpan && this.UseNum < 1 && flag)
          {
            this.StatusReset();
          }
          else
          {
            if (!this.Holding)
              return;
            this.HoldDuration += Time.unscaledDeltaTime;
            if ((double) this.HoldDuration < (double) this.UseLevelSpan)
              return;
            this.HoldDuration -= this.UseLevelSpan;
            this.LevelupFunc();
            ++this.UseNum;
            GameSettings.HoldCountSettings[] holdCount = instance.HoldCount;
            if (holdCount.Length <= this.NextSettingIndex || holdCount[this.NextSettingIndex].Count >= this.UseNum)
              return;
            this.UseLevelSpan = holdCount[this.NextSettingIndex].UseSpan;
            ++this.NextSettingIndex;
          }
        }
      }

      public void StatusReset()
      {
        this.HoldDuration = 0.0f;
        this.Holding = false;
        this.UseNum = 0;
        this.NextSettingIndex = 0;
        ((Vector2) ref this.mDragStartPos).Set(0.0f, 0.0f);
      }

      public delegate void DelegateOnPointerDown(
        EnhanceEquipDetailWindow.EnhanceEquipTouchController controller);

      public delegate void DelegateOnPointerUp(
        EnhanceEquipDetailWindow.EnhanceEquipTouchController controller);

      public delegate void DelegateLevelUp();
    }
  }
}
