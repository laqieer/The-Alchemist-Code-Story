// Decompiled with JetBrains decompiler
// Type: SRPG.EnhanceEquipDetailWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using GR;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace SRPG
{
  [FlowNode.Pin(2, "Reset", FlowNode.PinTypes.Input, 2)]
  [FlowNode.Pin(1, "Refresh", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(100, "強化", FlowNode.PinTypes.Output, 100)]
  public class EnhanceEquipDetailWindow : SRPG_FixedList, IFlowInterface
  {
    public GameObject Unit;
    public List<Button> Equipments;
    public List<RawImage> EquipmentRawImages;
    public List<GameObject> EquipmentCursors;
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
    public Text TxtJob;
    public Text TxtCost;
    public Text TxtComment;
    public Text TxtDisableEnhanceOnGauge;
    private UnitData mUnit;
    private int mJobIndex;
    private GameObject mSelectedEquipItem;
    private EnhanceEquipData mEnhanceEquipData;
    private List<GameObject> mEnhanceParameters;
    private GameObject mSelectedMaterialItem;
    private List<EnhanceMaterial> mEnhanceMaterials;
    private List<EnhanceMaterial> mEnableEnhanceMaterials;
    private List<ItemData> mMaterialItems;

    public override RectTransform ListParent
    {
      get
      {
        if ((UnityEngine.Object) this.ItemLayoutParent != (UnityEngine.Object) null)
          return this.ItemLayoutParent.GetComponent<RectTransform>();
        return (RectTransform) null;
      }
    }

    protected override void Start()
    {
      base.Start();
      if ((UnityEngine.Object) this.ItemTemplate == (UnityEngine.Object) null)
        return;
      List<ItemData> items = MonoSingleton<GameManager>.Instance.Player.Items;
      this.mMaterialItems = new List<ItemData>(items.Count);
      for (int index = 0; index < items.Count; ++index)
      {
        if (items[index].CheckEquipEnhanceMaterial() && !items[index].Param.is_valuables)
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
        }
        return (int) dsc.Param.enhace_point - (int) src.Param.enhace_point;
      }));
      this.mUnit = MonoSingleton<GameManager>.Instance.Player.FindUnitDataByUniqueID((long) GlobalVars.SelectedUnitUniqueID);
      this.mJobIndex = (int) GlobalVars.SelectedUnitJobIndex;
      this.mEnhanceEquipData = new EnhanceEquipData();
      this.mEnhanceMaterials = new List<EnhanceMaterial>(this.mMaterialItems.Count);
      this.mEnableEnhanceMaterials = new List<EnhanceMaterial>(this.mMaterialItems.Count);
      this.mEnhanceParameters = new List<GameObject>(5);
      this.mSelectedEquipItem = (GameObject) null;
      this.mSelectedMaterialItem = (GameObject) null;
      if ((UnityEngine.Object) this.ItemTemplate != (UnityEngine.Object) null && this.ItemTemplate.activeInHierarchy)
      {
        this.ItemTemplate.transform.SetSiblingIndex(0);
        this.ItemTemplate.SetActive(false);
      }
      if ((UnityEngine.Object) this.BtnJob != (UnityEngine.Object) null)
        this.BtnJob.onClick.AddListener(new UnityAction(this.OnJobChange));
      if ((UnityEngine.Object) this.BtnAdd != (UnityEngine.Object) null)
        this.BtnAdd.onClick.AddListener(new UnityAction(this.OnAddMaterial));
      if ((UnityEngine.Object) this.BtnSub != (UnityEngine.Object) null)
        this.BtnSub.onClick.AddListener(new UnityAction(this.OnSubMaterial));
      if ((UnityEngine.Object) this.BtnEnhance != (UnityEngine.Object) null)
        this.BtnEnhance.onClick.AddListener(new UnityAction(this.OnEnhance));
      if ((UnityEngine.Object) this.TxtComment != (UnityEngine.Object) null)
        this.TxtComment.text = LocalizedText.Get("sys.ENHANCE_EQUIPMENT_MESSAGE");
      if ((UnityEngine.Object) this.TxtDisableEnhanceOnGauge != (UnityEngine.Object) null)
        this.TxtDisableEnhanceOnGauge.text = LocalizedText.Get("sys.DIABLE_ENHANCE_MESSAGE");
      for (int index = 0; index < this.Equipments.Count; ++index)
      {
        int slot = index;
        this.Equipments[index].onClick.AddListener((UnityAction) (() => this.OnSelectEquipment(slot)));
      }
      this.RefreshData();
    }

    public void Activated(int pinID)
    {
      switch (pinID)
      {
        case 1:
          this.mUnit = MonoSingleton<GameManager>.Instance.Player.FindUnitDataByUniqueID((long) GlobalVars.SelectedUnitUniqueID);
          this.mJobIndex = (int) GlobalVars.SelectedUnitJobIndex;
          this.mSelectedMaterialItem = (GameObject) null;
          this.ClearEnhancedMaterial();
          if (!this.HasStarted)
            break;
          this.RefreshData();
          break;
        case 2:
          this.mUnit = MonoSingleton<GameManager>.Instance.Player.FindUnitDataByUniqueID((long) GlobalVars.SelectedUnitUniqueID);
          this.mJobIndex = (int) GlobalVars.SelectedUnitJobIndex;
          this.mSelectedEquipItem = (GameObject) null;
          this.mSelectedMaterialItem = (GameObject) null;
          this.ClearEnhancedMaterial();
          if (!this.HasStarted)
            break;
          this.RefreshData();
          break;
      }
    }

    protected override void Update()
    {
      base.Update();
    }

    private void RefreshData()
    {
      JobData jobData = this.mUnit.GetJobData(this.mJobIndex);
      DataSource.Bind<UnitData>(this.Unit, this.mUnit);
      for (int index = 0; index < this.Equipments.Count; ++index)
      {
        EquipData equip = jobData.Equips[index];
        DataSource.Bind<EquipData>(this.Equipments[index].gameObject, equip);
        bool flag = equip != null && (equip.IsValid() && equip.IsEquiped());
        this.Equipments[index].interactable = flag;
        this.EquipmentRawImages[index].gameObject.SetActive(flag);
        this.EquipmentCursors[index].SetActive((UnityEngine.Object) this.mSelectedEquipItem == (UnityEngine.Object) this.Equipments[index].gameObject);
      }
      this.mEnhanceEquipData.equip = (EquipData) null;
      this.mEnhanceEquipData.gainexp = 0;
      this.mEnhanceEquipData.is_enhanced = false;
      for (int index = 0; index < this.mEnhanceParameters.Count; ++index)
        this.mEnhanceParameters[index].SetActive(false);
      this.BtnEnhance.interactable = false;
      EquipData equipData = !((UnityEngine.Object) this.mSelectedEquipItem != (UnityEngine.Object) null) ? (EquipData) null : DataSource.FindDataOfClass<EquipData>(this.mSelectedEquipItem, (EquipData) null);
      int num1 = 0;
      int num2 = 0;
      for (int index = 0; index < this.mMaterialItems.Count; ++index)
      {
        ItemData item = this.mMaterialItems[index];
        EnhanceMaterial enhanceMaterial1 = this.mEnhanceMaterials.Find((Predicate<EnhanceMaterial>) (p => p.item == item));
        if (enhanceMaterial1 == null)
        {
          EnhanceMaterial enhanceMaterial2 = new EnhanceMaterial();
          enhanceMaterial2.item = item;
          enhanceMaterial2.num = 0;
          this.mEnhanceMaterials.Add(enhanceMaterial2);
          enhanceMaterial1 = enhanceMaterial2;
        }
        if (equipData != null)
        {
          num1 += (int) item.Param.enhace_cost * equipData.GetEnhanceCostScale() / 100 * enhanceMaterial1.num;
          num2 += (int) item.Param.enhace_point * enhanceMaterial1.num;
        }
      }
      bool flag1 = false;
      if ((UnityEngine.Object) this.EquipSelectParent != (UnityEngine.Object) null)
      {
        DataSource.Bind<EnhanceEquipData>(this.EquipSelectParent, (EnhanceEquipData) null);
        this.EquipSelectParent.gameObject.SetActive(false);
      }
      if ((UnityEngine.Object) this.TxtComment != (UnityEngine.Object) null)
        this.TxtComment.gameObject.SetActive(false);
      if ((UnityEngine.Object) this.mSelectedEquipItem != (UnityEngine.Object) null)
      {
        this.mEnhanceEquipData.equip = equipData;
        this.mEnhanceEquipData.gainexp = num2;
        if (equipData != null)
        {
          BuffEffect buffEffect = equipData.Skill.GetBuffEffect(SkillEffectTargets.Target);
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
              data.equip = equipData;
              data.param_index = index;
              DataSource.Bind<EquipItemParameter>(enhanceParameter, data);
              enhanceParameter.SetActive(true);
            }
          }
          flag1 = true;
          if (equipData.Rank == equipData.GetRankCap())
          {
            if ((UnityEngine.Object) this.TxtComment != (UnityEngine.Object) null)
            {
              this.TxtComment.text = LocalizedText.Get("sys.DIABLE_ENHANCE_RANKCAP_MESSAGE");
              this.TxtComment.gameObject.SetActive(true);
            }
            flag1 = false;
          }
        }
        if ((UnityEngine.Object) this.EquipSelectParent != (UnityEngine.Object) null)
        {
          DataSource.Bind<EnhanceEquipData>(this.EquipSelectParent, this.mEnhanceEquipData);
          this.EquipSelectParent.gameObject.SetActive(true);
        }
      }
      else if ((UnityEngine.Object) this.TxtComment != (UnityEngine.Object) null)
      {
        this.TxtComment.text = LocalizedText.Get("sys.ENHANCE_EQUIPMENT_MESSAGE");
        this.TxtComment.gameObject.SetActive(true);
      }
      this.mEnhanceEquipData.is_enhanced = flag1;
      if ((UnityEngine.Object) this.SelectedParent != (UnityEngine.Object) null)
      {
        DataSource.Bind<EnhanceEquipData>(this.SelectedParent, this.mEnhanceEquipData);
        this.SelectedParent.gameObject.SetActive(flag1);
      }
      if ((UnityEngine.Object) this.TxtDisableEnhanceOnGauge != (UnityEngine.Object) null)
        this.TxtDisableEnhanceOnGauge.gameObject.SetActive(!flag1);
      if ((UnityEngine.Object) this.TxtCost != (UnityEngine.Object) null)
        this.TxtCost.text = num1.ToString();
      if ((UnityEngine.Object) this.TxtJob != (UnityEngine.Object) null)
        this.TxtJob.text = jobData.Name;
      if (flag1)
      {
        int num3 = equipData.CalcRankFromExp(equipData.Exp + num2);
        this.BtnEnhance.interactable = equipData != null && equipData.Rank < num3;
      }
      if ((UnityEngine.Object) this.BtnAdd != (UnityEngine.Object) null)
        this.BtnAdd.interactable = flag1 && (UnityEngine.Object) this.mSelectedMaterialItem != (UnityEngine.Object) null;
      if ((UnityEngine.Object) this.BtnSub != (UnityEngine.Object) null)
        this.BtnSub.interactable = flag1 && (UnityEngine.Object) this.mSelectedMaterialItem != (UnityEngine.Object) null;
      this.mEnableEnhanceMaterials.Clear();
      for (int index = 0; index < this.mEnhanceMaterials.Count; ++index)
      {
        if (this.mEnhanceMaterials[index].item != null && this.mEnhanceMaterials[index].item.Num != 0)
          this.mEnableEnhanceMaterials.Add(this.mEnhanceMaterials[index]);
      }
      this.SetData((object[]) this.mEnableEnhanceMaterials.ToArray(), typeof (EnhanceMaterial));
      GameParameter.UpdateAll(this.gameObject);
    }

    protected override GameObject CreateItem()
    {
      return UnityEngine.Object.Instantiate<GameObject>(this.ItemTemplate);
    }

    protected override void OnUpdateItem(GameObject go, int index)
    {
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
      this.mSelectedMaterialItem = (GameObject) null;
      this.ClearEnhancedMaterial();
      this.RefreshData();
    }

    private void OnSelectEquipment(int slot)
    {
      if ((UnityEngine.Object) this.mSelectedEquipItem == (UnityEngine.Object) this.Equipments[slot].gameObject)
        return;
      this.ClearEnhancedMaterial();
      this.ClearMaterialSelect();
      this.mSelectedEquipItem = this.Equipments[slot].gameObject;
      GlobalVars.SelectedEquipmentSlot.Set(slot);
      this.RefreshData();
    }

    protected override void OnItemSelect(GameObject go)
    {
      if (!((UnityEngine.Object) this.mSelectedMaterialItem != (UnityEngine.Object) go))
        return;
      this.mSelectedMaterialItem = go;
      EnhanceMaterial dataOfClass = DataSource.FindDataOfClass<EnhanceMaterial>(this.mSelectedMaterialItem, (EnhanceMaterial) null);
      if (dataOfClass != null)
      {
        for (int index = 0; index < this.mEnhanceMaterials.Count; ++index)
          this.mEnhanceMaterials[index].selected = dataOfClass == this.mEnhanceMaterials[index];
      }
      this.RefreshData();
    }

    private void OnAddMaterial()
    {
      if ((UnityEngine.Object) this.mSelectedMaterialItem == (UnityEngine.Object) null)
        return;
      EnhanceMaterial dataOfClass = DataSource.FindDataOfClass<EnhanceMaterial>(this.mSelectedMaterialItem, (EnhanceMaterial) null);
      if (dataOfClass != null)
      {
        if (!this.CheckEquipItemEnhance())
        {
          UIUtility.NegativeSystemMessage(LocalizedText.Get("sys.FAILED_ENHANCE"), LocalizedText.Get("sys.DIABLE_ENHANCE_RANKCAP_MESSAGE"), (UIUtility.DialogResultEvent) null, (GameObject) null, false, -1);
          return;
        }
        dataOfClass.num = Math.Min(++dataOfClass.num, dataOfClass.item.Num);
        DataSource.Bind<EnhanceMaterial>(this.mSelectedMaterialItem, dataOfClass);
      }
      this.RefreshData();
    }

    private void OnSubMaterial()
    {
      if ((UnityEngine.Object) this.mSelectedMaterialItem == (UnityEngine.Object) null)
        return;
      EnhanceMaterial dataOfClass = DataSource.FindDataOfClass<EnhanceMaterial>(this.mSelectedMaterialItem, (EnhanceMaterial) null);
      if (dataOfClass != null)
      {
        dataOfClass.num = Math.Max(--dataOfClass.num, 0);
        DataSource.Bind<EnhanceMaterial>(this.mSelectedMaterialItem, dataOfClass);
      }
      this.RefreshData();
    }

    private bool CheckEquipItemEnhance()
    {
      if (this.mEnhanceEquipData == null || this.mEnhanceEquipData.equip == null)
        return false;
      EquipData equip = this.mEnhanceEquipData.equip;
      int current = equip.Exp + this.mEnhanceEquipData.gainexp;
      return equip.CalcRankFromExp(current) < equip.GetRankCap();
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
        UIUtility.ConfirmBox(LocalizedText.Get("sys.ENHANCE_ITEM_RARITY_CAUTION"), (string) null, new UIUtility.DialogResultEvent(this.OnDecide), new UIUtility.DialogResultEvent(this.OnCancel), (GameObject) null, false, -1);
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

    public override void GotoPreviousPage()
    {
      this.ClearMaterialSelect();
      this.RefreshData();
      base.GotoPreviousPage();
    }

    public override void GotoNextPage()
    {
      this.ClearMaterialSelect();
      this.RefreshData();
      base.GotoNextPage();
    }

    public void ClearMaterialSelect()
    {
      this.mSelectedMaterialItem = (GameObject) null;
      for (int index = 0; index < this.mEnhanceMaterials.Count; ++index)
        this.mEnhanceMaterials[index].selected = false;
    }
  }
}
