﻿// Decompiled with JetBrains decompiler
// Type: SRPG.UnitEquipmentWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  [FlowNode.Pin(1, "ウインドウを表示", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(2, "ウインドウの表示要素を再読み込み", FlowNode.PinTypes.Input, 2)]
  [FlowNode.Pin(3, "アイテムの汎用装備が選択された", FlowNode.PinTypes.Input, 3)]
  [FlowNode.Pin(100, "装備アイテムが装着された", FlowNode.PinTypes.Output, 100)]
  [FlowNode.Pin(101, "入手クエストが選択された", FlowNode.PinTypes.Output, 101)]
  [FlowNode.Pin(102, "アイテムが作成された", FlowNode.PinTypes.Output, 102)]
  [FlowNode.Pin(103, "アイテムが一括作成された", FlowNode.PinTypes.Output, 103)]
  [FlowNode.Pin(104, "汎用装備が選択された", FlowNode.PinTypes.Output, 104)]
  [FlowNode.Pin(110, "クエスト書庫が選択された", FlowNode.PinTypes.Output, 110)]
  public class UnitEquipmentWindow : MonoBehaviour, IFlowInterface
  {
    public UnitEquipmentWindow.EquipEvent OnEquip;
    public UnitEquipmentWindow.EquipEvent OnCommonEquip;
    public UnitEquipmentWindow.EquipReloadEvent OnReload;
    public GameObject SubWindow;
    public SRPG_Button EquipButton;
    public SRPG_Button CommonEquipButton;
    public SRPG_Button ConfirmQuestButton;
    public SRPG_Button ConfirmRecipeButton;
    public SRPG_Button CreateButton;
    public SRPG_Button CreateButtonAll;
    public RectTransform RecipeParent;
    public RectTransform QuestsParent;
    public RectTransform SelectedItem;
    public RectTransform ItemTreeParent;
    public RectTransform ItemTreeArrow;
    public ListItemEvents ItemTreeTemplate;
    public RectTransform RecipeListParent;
    public RectTransform RecipeListLine;
    public ListItemEvents RecipeListItemTemplate;
    public RectTransform QuestListParent;
    public GameObject QuestListItemTemplate;
    public RectTransform EquipItemParamParent;
    public GameObject EquipItemParamTemplate;
    private EquipData mEquipmentData;
    private List<GameObject> mEquipmentParameters = new List<GameObject>(5);
    private List<ItemParam> mItemParamTree = new List<ItemParam>(4);
    private List<GameObject> ItemTree = new List<GameObject>();
    private List<EquipRecipeItem> RecipeItems = new List<EquipRecipeItem>();
    private List<GameObject> GainedQuests = new List<GameObject>();
    private List<GameObject> GainedDetailQuests = new List<GameObject>();
    private List<GameObject> GainedDetailQuestsMask = new List<GameObject>();
    private bool mCreateItemSuccessed;
    private ItemParam LastUpadatedItemParam;
    public GameObject[] NoCommonUI;
    public GameObject[] CommonUI;
    public GameObject QuestCommonEquipButton;
    public GameObject RecipeCommonEquipButton;
    public Text EquipAmount;
    public Color EquipAmountColor;
    public Color EquipAmountColorZero;
    private UnitData SelectedUnitData;
    [StringIsResourcePath(typeof (GameObject))]
    [SerializeField]
    private string QuestDetail;
    [StringIsResourcePath(typeof (GameObject))]
    [SerializeField]
    private string QuestDetailMulti;
    protected QuestParam mCurrentQuest;
    private LoadRequest mReqQuestDetail;
    private LoadRequest mReqQuestDefaultDetail;
    private LoadRequest mReqQuestMultiDetail;
    private NeedEquipItemList mNeedEquipItemList;
    private List<UnitEquipmentWindow.ResetDefaultColor> EquipButtonColor = new List<UnitEquipmentWindow.ResetDefaultColor>();
    private List<UnitEquipmentWindow.ResetDefaultColor> CommonButtonEquipColor = new List<UnitEquipmentWindow.ResetDefaultColor>();

    private bool IsTreeTop => this.mItemParamTree.Count == 1;

    public void Activated(int pinID)
    {
      if (pinID == 1)
      {
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.SubWindow, (UnityEngine.Object) null))
          this.SubWindow.SetActive(false);
        this.Refresh();
      }
      if (pinID == 2)
      {
        this.mCreateItemSuccessed = true;
        this.Refresh();
        if (this.OnReload != null)
          this.OnReload();
        this.mCreateItemSuccessed = false;
      }
      if (pinID != 3)
        return;
      this.CommonEquip();
    }

    private void Awake()
    {
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.SubWindow, (UnityEngine.Object) null))
        return;
      this.SubWindow.SetActive(false);
    }

    private void Start()
    {
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.EquipButton, (UnityEngine.Object) null))
      {
        this.EquipButton.AddListener(new SRPG_Button.ButtonClickEvent(this.OnEquipClick));
        this.SetResetColor(this.EquipButtonColor, (Graphic) ((Component) this.EquipButton).gameObject.GetComponent<Image>());
        this.SetResetColor(this.EquipButtonColor, (Graphic) ((Component) this.EquipButton).gameObject.GetComponentInChildren<Text>());
      }
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.CommonEquipButton, (UnityEngine.Object) null))
      {
        this.CommonEquipButton.AddListener(new SRPG_Button.ButtonClickEvent(this.OnCommonEquipClick));
        this.SetResetColor(this.CommonButtonEquipColor, (Graphic) ((Component) this.EquipButton).gameObject.GetComponent<Image>());
        this.SetResetColor(this.CommonButtonEquipColor, (Graphic) ((Component) this.EquipButton).gameObject.GetComponentInChildren<Text>());
      }
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.CreateButton, (UnityEngine.Object) null))
        this.CreateButton.AddListener(new SRPG_Button.ButtonClickEvent(this.OnCreateClick));
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.CreateButtonAll, (UnityEngine.Object) null))
        this.CreateButtonAll.AddListener(new SRPG_Button.ButtonClickEvent(this.OnCreateAllClick));
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.ConfirmQuestButton, (UnityEngine.Object) null))
        this.ConfirmQuestButton.AddListener(new SRPG_Button.ButtonClickEvent(this.OnConfirmQuestClick));
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.ConfirmRecipeButton, (UnityEngine.Object) null))
        this.ConfirmRecipeButton.AddListener(new SRPG_Button.ButtonClickEvent(this.OnConfirmRecipeClick));
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.ItemTreeTemplate, (UnityEngine.Object) null))
        ((Component) this.ItemTreeTemplate).gameObject.SetActive(false);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.RecipeListItemTemplate, (UnityEngine.Object) null))
        ((Component) this.RecipeListItemTemplate).gameObject.SetActive(false);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.QuestListItemTemplate, (UnityEngine.Object) null))
        this.QuestListItemTemplate.SetActive(false);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.EquipItemParamTemplate, (UnityEngine.Object) null))
        this.EquipItemParamTemplate.SetActive(false);
      if (!string.IsNullOrEmpty(this.QuestDetail))
        this.mReqQuestDetail = AssetManager.LoadAsync<GameObject>(this.QuestDetail);
      if (!string.IsNullOrEmpty(this.QuestDetailMulti))
        this.mReqQuestMultiDetail = AssetManager.LoadAsync<GameObject>(this.QuestDetailMulti);
      this.Refresh();
    }

    public void SetResetColor(
      List<UnitEquipmentWindow.ResetDefaultColor> EquipButtonColorList,
      Graphic graphic)
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) graphic, (UnityEngine.Object) null))
        return;
      EquipButtonColorList.Add(new UnitEquipmentWindow.ResetDefaultColor(graphic));
    }

    private int GetSelectedJobIndex(UnitData unit)
    {
      for (int selectedJobIndex = 0; selectedJobIndex < unit.JobCount; ++selectedJobIndex)
      {
        if (unit.Jobs[selectedJobIndex].UniqueID == (long) GlobalVars.SelectedJobUniqueID)
          return selectedJobIndex;
      }
      return 0;
    }

    private void OnConfirmQuestClick(SRPG_Button button)
    {
      if (!((Selectable) button).IsInteractable())
        return;
      this.SubWindow.SetActive(true);
    }

    private void OnConfirmRecipeClick(SRPG_Button button)
    {
      if (!((Selectable) button).IsInteractable())
        return;
      this.SubWindow.SetActive(true);
    }

    private void OnEquipClick(SRPG_Button button)
    {
      if (!((Selectable) button).IsInteractable())
        return;
      if (this.OnEquip != null)
      {
        this.OnEquip();
      }
      else
      {
        UnitData unitDataByUniqueId = MonoSingleton<GameManager>.Instance.Player.FindUnitDataByUniqueID((long) GlobalVars.SelectedUnitUniqueID);
        if (unitDataByUniqueId == null)
          return;
        int selectedEquipmentSlot = (int) GlobalVars.SelectedEquipmentSlot;
        if (!MonoSingleton<GameManager>.Instance.Player.SetUnitEquipment(unitDataByUniqueId, selectedEquipmentSlot))
          return;
        GameParameter.UpdateAll(((Component) this).gameObject);
        int selectedJobIndex = this.GetSelectedJobIndex(unitDataByUniqueId);
        ItemData itemDataByItemParam = MonoSingleton<GameManager>.Instance.Player.FindItemDataByItemParam(unitDataByUniqueId.GetRankupEquipData(selectedJobIndex, selectedEquipmentSlot).ItemParam);
        GlobalVars.SelectedEquipUniqueID.Set(itemDataByItemParam.UniqueID);
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 100);
      }
    }

    private void OnCreateClick(SRPG_Button button) => this.OnCreateClick();

    private void OnCreateClick()
    {
      GlobalVars.SelectedCreateItemID = DataSource.FindDataOfClass<ItemParam>(((Component) this.SelectedItem).gameObject, (ItemParam) null).iname;
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 102);
    }

    private void OnCreateAllClick(SRPG_Button button)
    {
      GlobalVars.SelectedCreateItemID = DataSource.FindDataOfClass<ItemParam>(((Component) this.SelectedItem).gameObject, (ItemParam) null).iname;
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 103);
    }

    private void OnCommonEquipClick(SRPG_Button button)
    {
      if (!((Selectable) button).IsInteractable())
        return;
      UnitData unitDataByUniqueId = MonoSingleton<GameManager>.Instance.Player.FindUnitDataByUniqueID((long) GlobalVars.SelectedUnitUniqueID);
      int selectedJobIndex = this.GetSelectedJobIndex(unitDataByUniqueId);
      EquipData rankupEquipData = unitDataByUniqueId.GetRankupEquipData(selectedJobIndex, (int) GlobalVars.SelectedEquipmentSlot);
      ItemParam commonEquip = MonoSingleton<GameManager>.Instance.MasterParam.GetCommonEquip(rankupEquipData.ItemParam, unitDataByUniqueId.Jobs[selectedJobIndex].Rank == 0);
      if (commonEquip == null)
        return;
      UIUtility.ConfirmBox(LocalizedText.Get("sys.COMMON_EQUIP_SOUL_CONFIRM", (object) commonEquip.name, (object) rankupEquipData.ItemParam.name), new UIUtility.DialogResultEvent(this.CommonEquip), (UIUtility.DialogResultEvent) null);
    }

    private void CommonEquip(GameObject go = null)
    {
      if (this.OnCommonEquip == null)
        return;
      this.OnCommonEquip();
    }

    public void CommonEquiped() => FlowNode_GameObject.ActivateOutputLinks((Component) this, 104);

    public void SetRestorationRecipeItem(List<ItemParam> item)
    {
      this.mItemParamTree.Clear();
      this.mItemParamTree.AddRange((IEnumerable<ItemParam>) item.ToArray());
      this.RefreshItemTree();
      this.RefreshRecipeItems();
      this.RefreshGainedQuests();
      GameParameter.UpdateAll(((Component) this).gameObject);
    }

    private void OnItemTreeSelect(GameObject go)
    {
      int index = this.ItemTree.IndexOf(go) + 1;
      if (index == this.ItemTree.Count)
        return;
      int count = this.mItemParamTree.Count - index;
      if (count > 0)
        this.mItemParamTree.RemoveRange(index, count);
      this.RefreshItemTree();
      this.RefreshRecipeItems();
      this.RefreshGainedQuests();
      GameParameter.UpdateAll(((Component) this).gameObject);
    }

    private void OnRecipeItemSelect(GameObject go)
    {
      this.mItemParamTree.Add(DataSource.FindDataOfClass<RecipeItemParameter>(go, (RecipeItemParameter) null).Item);
      this.RefreshItemTree();
      this.RefreshRecipeItems();
      this.RefreshGainedQuests();
      GameParameter.UpdateAll(((Component) this).gameObject);
    }

    private void OpenQuestDetail(QuestParam param)
    {
      this.mCurrentQuest = param;
      DataSource.Bind<QuestParam>(((Component) this).gameObject, this.mCurrentQuest);
      QuestCampaignData[] questCampaigns = MonoSingleton<GameManager>.Instance.FindQuestCampaigns(this.mCurrentQuest);
      DataSource.Bind<QuestCampaignData[]>(((Component) this).gameObject, questCampaigns.Length != 0 ? questCampaigns : (QuestCampaignData[]) null);
      if (this.mCurrentQuest == null)
        return;
      this.mReqQuestDefaultDetail = !this.mCurrentQuest.IsMulti ? this.mReqQuestDetail : this.mReqQuestMultiDetail;
      if (this.mReqQuestDefaultDetail == null)
        return;
      GameObject gameObject = UnityEngine.Object.Instantiate(this.mReqQuestDefaultDetail.asset) as GameObject;
      DataSource.Bind<QuestParam>(gameObject, this.mCurrentQuest);
      gameObject.SetActive(true);
    }

    private void OnQuestDetailSelect(SRPG_Button button)
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) button, (UnityEngine.Object) null) || !((Selectable) button).interactable)
        return;
      int index = this.GainedDetailQuests.IndexOf(((Component) button).gameObject);
      if (index < 0)
        return;
      QuestParam dataOfClass = DataSource.FindDataOfClass<QuestParam>(this.GainedDetailQuests[index], (QuestParam) null);
      if (dataOfClass == null)
        return;
      this.OpenQuestDetail(dataOfClass);
    }

    private void OnQuestDetailSelectMask(SRPG_Button button)
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) button, (UnityEngine.Object) null) || !((Selectable) button).interactable)
        return;
      int index = this.GainedDetailQuestsMask.IndexOf(((Component) button).gameObject);
      if (index < 0)
        return;
      QuestParam dataOfClass = DataSource.FindDataOfClass<QuestParam>(this.GainedDetailQuestsMask[index], (QuestParam) null);
      if (dataOfClass == null)
        return;
      this.OpenQuestDetail(dataOfClass);
    }

    private void OnQuestSelect(SRPG_Button button)
    {
      PlayerData player = MonoSingleton<GameManager>.Instance.Player;
      int index = this.GainedQuests.IndexOf(((Component) button).gameObject);
      if (index < 0)
        return;
      QuestParam quest = DataSource.FindDataOfClass<QuestParam>(this.GainedQuests[index], (QuestParam) null);
      if (quest == null)
        return;
      ArchiveParam archiveByArea = MonoSingleton<GameManager>.Instance.FindArchiveByArea(quest.ChapterID);
      if (archiveByArea != null && archiveByArea.IsAvailable() && MonoSingleton<GameManager>.Instance.Player.CheckUnlock(UnlockTargets.Archive))
      {
        if (LevelLock.IsNeedCheckUnlockConds(quest))
        {
          UnlockTargets targetByQuestId = LevelLock.GetTargetByQuestId(quest.iname, UnlockTargets.EventQuest);
          if (LevelLock.ShowLockMessage(player.Lv, player.VipRank, targetByQuestId))
            return;
        }
        if (Array.Find<QuestParam>(MonoSingleton<GameManager>.Instance.Player.AvailableQuests, (Predicate<QuestParam>) (p => p == quest)) == null)
          UIUtility.NegativeSystemMessage((string) null, LocalizedText.Get("sys.DISABLE_QUEST_CHALLENGE"), (UIUtility.DialogResultEvent) null);
        else if (MonoSingleton<GameManager>.Instance.Player.IsQuestArchiveOpen(archiveByArea.iname))
        {
          GlobalVars.SelectedQuestID = quest.iname;
          FlowNode_GameObject.ActivateOutputLinks((Component) this, 101);
        }
        else
        {
          GlobalVars.SelectedArchiveID = archiveByArea.iname;
          FlowNode_GameObject.ActivateOutputLinks((Component) this, 110);
        }
      }
      else if (!quest.IsDateUnlock())
      {
        UIUtility.NegativeSystemMessage((string) null, LocalizedText.Get("sys.DISABLE_QUEST_DATE_UNLOCK"), (UIUtility.DialogResultEvent) null);
      }
      else
      {
        if (LevelLock.IsNeedCheckUnlockConds(quest))
        {
          UnlockTargets targetByQuestId = LevelLock.GetTargetByQuestId(quest.iname, UnlockTargets.EventQuest);
          if (LevelLock.ShowLockMessage(player.Lv, player.VipRank, targetByQuestId))
            return;
        }
        if (Array.Find<QuestParam>(MonoSingleton<GameManager>.Instance.Player.AvailableQuests, (Predicate<QuestParam>) (p => p == quest)) == null)
        {
          UIUtility.NegativeSystemMessage((string) null, LocalizedText.Get("sys.DISABLE_QUEST_CHALLENGE"), (UIUtility.DialogResultEvent) null);
        }
        else
        {
          GlobalVars.SelectedQuestID = quest.iname;
          FlowNode_GameObject.ActivateOutputLinks((Component) this, 101);
        }
      }
    }

    private void RefreshEquipButton(UnitData unit)
    {
      bool flag = false;
      int selectedJobIndex = this.GetSelectedJobIndex(unit);
      EquipData rankupEquipData = unit.GetRankupEquipData(selectedJobIndex, (int) GlobalVars.SelectedEquipmentSlot);
      if (rankupEquipData != null && rankupEquipData.IsValid() && !rankupEquipData.IsEquiped())
      {
        if (MonoSingleton<GameManager>.Instance.Player.HasItem(rankupEquipData.ItemID))
          flag = rankupEquipData.ItemParam.equipLv <= unit.Lv;
        else if (unit.Jobs[selectedJobIndex].Rank <= 0 && rankupEquipData.ItemParam.IsCommon)
        {
          ItemParam commonEquip = MonoSingleton<GameManager>.Instance.MasterParam.GetCommonEquip(rankupEquipData.ItemParam, true);
          flag = commonEquip != null && MonoSingleton<GameManager>.Instance.Player.HasItem(commonEquip.iname);
        }
      }
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.EquipButton, (UnityEngine.Object) null))
        ((Selectable) this.EquipButton).interactable = flag;
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.CommonEquipButton, (UnityEngine.Object) null))
        return;
      ((Selectable) this.CommonEquipButton).interactable = flag;
    }

    private void Refresh() => this.Refresh((UnitData) null, (int) GlobalVars.SelectedEquipmentSlot);

    public void Refresh(UnitData unit, int slot)
    {
      if (unit == null && this.SelectedUnitData != null)
      {
        unit = this.SelectedUnitData;
        GlobalVars.SelectedUnitUniqueID.Set(this.SelectedUnitData.UniqueID);
        GlobalVars.SelectedLSChangeUnitUniqueID.Set(this.SelectedUnitData.UniqueID);
      }
      else
        this.SelectedUnitData = unit;
      if (unit == null)
        return;
      int selectedJobIndex = this.GetSelectedJobIndex(unit);
      EquipData[] rankupEquips = unit.GetRankupEquips(selectedJobIndex);
      if (slot < 0 || rankupEquips.Length <= slot)
        return;
      EquipData equipData = rankupEquips[slot];
      ItemParam itemParam = equipData.ItemParam;
      if (this.mItemParamTree.Count == 0 || this.mItemParamTree[0] != itemParam)
      {
        this.mItemParamTree.Clear();
        this.mItemParamTree.Add(itemParam);
      }
      bool is_common = false;
      if (equipData != null && equipData.IsValid() && equipData.IsEquiped())
      {
        this.mEquipmentData = equipData;
      }
      else
      {
        this.mEquipmentData = new EquipData();
        ItemData itemDataByItemId = MonoSingleton<GameManager>.Instance.Player.FindItemDataByItemID(itemParam.iname);
        ItemParam item_param = itemDataByItemId == null ? MonoSingleton<GameManager>.Instance.MasterParam.GetItemParam(itemParam.iname) : itemDataByItemId.Param;
        ItemParam commonEquip = MonoSingleton<GameManager>.Instance.MasterParam.GetCommonEquip(item_param, true);
        ItemData itemData = (ItemData) null;
        if (commonEquip != null)
          itemData = MonoSingleton<GameManager>.Instance.Player.FindItemDataByItemID(commonEquip.iname);
        if ((itemDataByItemId == null || itemDataByItemId.Num <= 0) && item_param.IsCommon && unit.Jobs[selectedJobIndex].Rank == 0 && itemData != null && itemData.Num > 0)
        {
          this.mEquipmentData.Setup(commonEquip.iname);
          is_common = true;
        }
        else
          this.mEquipmentData.Setup(itemParam.iname);
      }
      this.ActivateCommonUI(is_common);
      for (int index = 0; index < this.mEquipmentParameters.Count; ++index)
        this.mEquipmentParameters[index].SetActive(false);
      if (this.mEquipmentData != null && this.mEquipmentData.Skill != null)
      {
        BuffEffect buffEffect = this.mEquipmentData.Skill.GetBuffEffect();
        if (buffEffect != null && buffEffect.targets != null)
        {
          for (int index = 0; index < buffEffect.targets.Count; ++index)
          {
            if (index >= this.mEquipmentParameters.Count)
            {
              GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.EquipItemParamTemplate);
              gameObject.transform.SetParent((Transform) this.EquipItemParamParent, false);
              this.mEquipmentParameters.Add(gameObject);
            }
            GameObject equipmentParameter = this.mEquipmentParameters[index];
            EquipItemParameter data = DataSource.FindDataOfClass<EquipItemParameter>(equipmentParameter, (EquipItemParameter) null) ?? new EquipItemParameter();
            data.equip = this.mEquipmentData;
            data.param_index = index;
            DataSource.Bind<EquipItemParameter>(equipmentParameter, data);
            equipmentParameter.SetActive(true);
          }
        }
      }
      this.RefreshEquipButton(unit);
      DataSource.Bind<EquipData>(((Component) this).gameObject, this.mEquipmentData);
      this.RefreshItemTree(this.mCreateItemSuccessed);
      this.RefreshRecipeItems();
      this.RefreshGainedQuests();
      ItemData itemDataByItemId1 = MonoSingleton<GameManager>.Instance.Player.FindItemDataByItemID(this.mEquipmentData.ItemParam.iname);
      ((Graphic) this.EquipAmount).color = itemDataByItemId1 == null || itemDataByItemId1.Num == 0 ? this.EquipAmountColorZero : this.EquipAmountColor;
      GameParameter.UpdateAll(((Component) this).gameObject);
    }

    private void RefreshItemTree(bool created = false)
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.ItemTreeTemplate, (UnityEngine.Object) null) || UnityEngine.Object.op_Equality((UnityEngine.Object) this.ItemTreeParent, (UnityEngine.Object) null) || UnityEngine.Object.op_Equality((UnityEngine.Object) this.ItemTreeArrow, (UnityEngine.Object) null))
        return;
      for (int index = 0; index < this.ItemTree.Count; ++index)
        this.ItemTree[index].gameObject.SetActive(false);
      if (created && this.mItemParamTree.Count > 1)
      {
        RecipeItem recipeItem = Array.Find<RecipeItem>(MonoSingleton<GameManager>.Instance.GetRecipeParam(this.mItemParamTree[this.mItemParamTree.Count - 2].recipe).items, (Predicate<RecipeItem>) (p => p.iname == this.mItemParamTree[this.mItemParamTree.Count - 1].iname));
        if (recipeItem != null)
        {
          int num = recipeItem.num;
          if (MonoSingleton<GameManager>.Instance.Player.GetItemAmount(this.mItemParamTree[this.mItemParamTree.Count - 1].iname) >= num)
            this.mItemParamTree.RemoveAt(this.mItemParamTree.Count - 1);
        }
      }
      for (int index = 0; index < this.mItemParamTree.Count; ++index)
      {
        if (index >= this.ItemTree.Count)
        {
          ((Component) this.ItemTreeTemplate).gameObject.SetActive(index > 0);
          ((Component) this.ItemTreeArrow).gameObject.SetActive(index > 0);
          ListItemEvents listItemEvents = UnityEngine.Object.Instantiate<ListItemEvents>(this.ItemTreeTemplate);
          ((Component) listItemEvents).transform.SetParent((Transform) this.ItemTreeParent, false);
          this.ItemTree.Add(((Component) listItemEvents).gameObject);
          ((Component) this.ItemTreeArrow).gameObject.SetActive(false);
          ((Component) this.ItemTreeTemplate).gameObject.SetActive(false);
        }
        ListItemEvents component = this.ItemTree[index].GetComponent<ListItemEvents>();
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component, (UnityEngine.Object) null))
          component.OnSelect = new ListItemEvents.ListItemEvent(this.OnItemTreeSelect);
        this.ItemTree[index].gameObject.SetActive(true);
        DataSource.Bind<ItemParam>(this.ItemTree[index].gameObject, this.mItemParamTree[index]);
      }
      for (int index = 0; index < this.ItemTree.Count; ++index)
      {
        if (!UnityEngine.Object.op_Equality((UnityEngine.Object) this.ItemTree[index], (UnityEngine.Object) null))
        {
          EquipTree component = this.ItemTree[index].GetComponent<EquipTree>();
          if (!UnityEngine.Object.op_Equality((UnityEngine.Object) component, (UnityEngine.Object) null))
            component.SetIsLast(this.mItemParamTree.Count - 1 == index);
        }
      }
    }

    private void RefreshRecipeItems()
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.RecipeParent, (UnityEngine.Object) null) || UnityEngine.Object.op_Equality((UnityEngine.Object) this.SelectedItem, (UnityEngine.Object) null))
        return;
      ((Component) this.RecipeParent).gameObject.SetActive(false);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.CreateButton, (UnityEngine.Object) null))
        ((Component) this.CreateButton).gameObject.SetActive(false);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.CreateButtonAll, (UnityEngine.Object) null))
        ((Component) this.CreateButtonAll).gameObject.SetActive(false);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.ConfirmRecipeButton, (UnityEngine.Object) null))
        ((Component) this.ConfirmRecipeButton).gameObject.SetActive(false);
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.RecipeListItemTemplate, (UnityEngine.Object) null) || UnityEngine.Object.op_Equality((UnityEngine.Object) this.RecipeListParent, (UnityEngine.Object) null) || UnityEngine.Object.op_Equality((UnityEngine.Object) this.RecipeListLine, (UnityEngine.Object) null))
        return;
      for (int index = 0; index < this.RecipeItems.Count; ++index)
        ((Component) this.RecipeItems[index]).gameObject.SetActive(false);
      int index1 = this.mItemParamTree.Count - 1;
      DataSource.Bind<ItemParam>(((Component) this.SelectedItem).gameObject, this.mItemParamTree[index1]);
      RecipeParam recipeParam = MonoSingleton<GameManager>.Instance.GetRecipeParam(this.mItemParamTree[index1].recipe);
      if (recipeParam == null)
        return;
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.ConfirmRecipeButton, (UnityEngine.Object) null))
        ((Component) this.ConfirmRecipeButton).gameObject.SetActive(true);
      ((Component) this.RecipeParent).gameObject.SetActive(true);
      this.ActiveCommonEquipButton(this.IsCommonEquipUI((long) GlobalVars.SelectedUnitUniqueID, (int) GlobalVars.SelectedEquipmentSlot));
      for (int index2 = 0; index2 < recipeParam.items.Length; ++index2)
      {
        if (index2 >= this.RecipeItems.Count)
        {
          ((Component) this.RecipeListItemTemplate).gameObject.SetActive(index2 > 0);
          ((Component) this.RecipeListLine).gameObject.SetActive(index2 > 0);
          ListItemEvents listItemEvents = UnityEngine.Object.Instantiate<ListItemEvents>(this.RecipeListItemTemplate);
          ((Component) listItemEvents).transform.SetParent((Transform) this.RecipeListParent, false);
          this.RecipeItems.Add(((Component) listItemEvents).gameObject.GetComponent<EquipRecipeItem>());
          ((Component) this.RecipeListLine).gameObject.SetActive(false);
          ((Component) this.RecipeListItemTemplate).gameObject.SetActive(false);
        }
        ListItemEvents component = ((Component) this.RecipeItems[index2]).GetComponent<ListItemEvents>();
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component, (UnityEngine.Object) null))
          component.OnSelect = new ListItemEvents.ListItemEvent(this.OnRecipeItemSelect);
        ((Component) this.RecipeItems[index2]).gameObject.SetActive(true);
        RecipeItem recipeItem = recipeParam.items[index2];
        RecipeItemParameter data = new RecipeItemParameter()
        {
          Item = MonoSingleton<GameManager>.Instance.GetItemParam(recipeItem.iname),
          RecipeItem = recipeItem
        };
        data.Amount = MonoSingleton<GameManager>.Instance.Player.GetItemAmount(data.Item.iname);
        data.RequiredAmount = recipeItem.num;
        DataSource.Bind<RecipeItemParameter>(((Component) this.RecipeItems[index2]).gameObject, data);
      }
      int cost = 0;
      Dictionary<string, int> consumes = (Dictionary<string, int>) null;
      bool is_ikkatsu = false;
      this.mNeedEquipItemList = new NeedEquipItemList();
      bool flag1 = MonoSingleton<GameManager>.GetInstanceDirect().Player.CheckEnableCreateItem(this.mItemParamTree[index1], ref is_ikkatsu, ref cost, ref consumes, this.mNeedEquipItemList);
      List<RecipeTree> recipeTreeChildren = this.mNeedEquipItemList.GetCurrentRecipeTreeChildren();
      bool flag2 = recipeTreeChildren != null && recipeTreeChildren.Count > 0;
      EquipRecipeItem component1 = ((Component) this.SelectedItem).GetComponent<EquipRecipeItem>();
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component1, (UnityEngine.Object) null))
        component1.SetIsCommonLine(this.mNeedEquipItemList.IsEnoughCommon());
      for (int index3 = 0; index3 < this.RecipeItems.Count; ++index3)
      {
        this.RecipeItems[index3].SetIsCommonLine(flag2 && !flag1 && this.mNeedEquipItemList.IsEnoughCommon());
        RecipeItemParameter recipe = DataSource.FindDataOfClass<RecipeItemParameter>(((Component) this.RecipeItems[index3]).gameObject, (RecipeItemParameter) null);
        RecipeTree recipeTree = !flag2 ? (RecipeTree) null : recipeTreeChildren.Find((Predicate<RecipeTree>) (x => !string.IsNullOrEmpty(x.iname) && x.iname == recipe.Item.iname));
        bool flag3 = this.mNeedEquipItemList.IsEnoughCommon();
        this.RecipeItems[index3].SetIsCommon(recipeTree != null && recipeTree.IsCommon && flag3);
      }
      if (!flag1 && !this.mNeedEquipItemList.IsEnoughCommon())
        return;
      if (is_ikkatsu)
      {
        if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.CreateButtonAll, (UnityEngine.Object) null))
          return;
        ((Component) this.CreateButtonAll).gameObject.SetActive(true);
      }
      else
      {
        if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.CreateButton, (UnityEngine.Object) null))
          return;
        ((Component) this.CreateButton).gameObject.SetActive(true);
      }
    }

    private void RefreshGainedQuests()
    {
      this.ClearPanel();
      ((Component) this.QuestsParent).gameObject.SetActive(false);
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.QuestListItemTemplate, (UnityEngine.Object) null) || UnityEngine.Object.op_Equality((UnityEngine.Object) this.QuestListParent, (UnityEngine.Object) null))
        return;
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.ConfirmQuestButton, (UnityEngine.Object) null))
        ((Component) this.ConfirmQuestButton).gameObject.SetActive(false);
      int index = this.mItemParamTree.Count - 1;
      if (MonoSingleton<GameManager>.Instance.GetRecipeParam(this.mItemParamTree[index].recipe) != null)
        return;
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.ConfirmQuestButton, (UnityEngine.Object) null))
        ((Component) this.ConfirmQuestButton).gameObject.SetActive(true);
      this.ActiveCommonEquipButton(this.IsCommonEquipUI((long) GlobalVars.SelectedUnitUniqueID, (int) GlobalVars.SelectedEquipmentSlot));
      ItemParam data = this.mItemParamTree[index];
      DataSource.Bind<ItemParam>(((Component) this.QuestsParent).gameObject, data);
      ((Component) this.QuestsParent).gameObject.SetActive(true);
      if (this.LastUpadatedItemParam != data)
      {
        this.SetScrollTop();
        this.LastUpadatedItemParam = data;
      }
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) QuestDropParam.Instance, (UnityEngine.Object) null))
      {
        QuestParam[] availableQuests = MonoSingleton<GameManager>.Instance.Player.AvailableQuests;
        foreach (QuestParam itemDropQuest in QuestDropParam.Instance.GetItemDropQuestList(data, GlobalVars.GetDropTableGeneratedDateTime()))
          this.AddPanel(itemDropQuest, availableQuests);
      }
      GlobalVars.SelectedItemParamTree.Clear();
      GlobalVars.SelectedItemParamTree.AddRange((IEnumerable<ItemParam>) this.mItemParamTree.ToArray());
    }

    private void SetScrollTop()
    {
      RectTransform component = ((Component) this.QuestListParent).GetComponent<RectTransform>();
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) component, (UnityEngine.Object) null))
        return;
      Vector2 anchoredPosition = component.anchoredPosition;
      anchoredPosition.y = 0.0f;
      component.anchoredPosition = anchoredPosition;
    }

    public void ClearPanel(bool stop_coroutine = true)
    {
      this.GainedQuests.Clear();
      this.GainedDetailQuests.Clear();
      this.GainedDetailQuestsMask.Clear();
      for (int index = 0; index < ((Transform) this.QuestListParent).childCount; ++index)
      {
        GameObject gameObject = ((Component) ((Transform) this.QuestListParent).GetChild(index)).gameObject;
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.QuestListItemTemplate, (UnityEngine.Object) gameObject))
          UnityEngine.Object.Destroy((UnityEngine.Object) gameObject);
      }
    }

    private void AddPanel(QuestParam questparam, QuestParam[] availableQuests)
    {
      if (questparam == null || questparam.IsMulti)
        return;
      GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.QuestListItemTemplate);
      SRPG_Button component1 = gameObject.GetComponent<SRPG_Button>();
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component1, (UnityEngine.Object) null))
        component1.AddListener(new SRPG_Button.ButtonClickEvent(this.OnQuestSelect));
      this.GainedQuests.Add(gameObject);
      UnitEquipmentQuestItem component2 = gameObject.GetComponent<UnitEquipmentQuestItem>();
      SRPG_Button srpgButton = (SRPG_Button) null;
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component2, (UnityEngine.Object) null))
      {
        srpgButton = component2.QuestDetail.GetComponent<SRPG_Button>();
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) srpgButton, (UnityEngine.Object) null))
        {
          srpgButton.AddListener(new SRPG_Button.ButtonClickEvent(this.OnQuestDetailSelect));
          this.GainedDetailQuests.Add(component2.QuestDetail);
        }
        SRPG_Button component3 = component2.QuestDetailMask.GetComponent<SRPG_Button>();
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component3, (UnityEngine.Object) null))
        {
          component3.AddListener(new SRPG_Button.ButtonClickEvent(this.OnQuestDetailSelectMask));
          this.GainedDetailQuestsMask.Add(component2.QuestDetailMask);
        }
      }
      Button component4 = gameObject.GetComponent<Button>();
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component4, (UnityEngine.Object) null))
      {
        bool flag1 = questparam.IsDateUnlock();
        bool flag2 = Array.Find<QuestParam>(availableQuests, (Predicate<QuestParam>) (p => p == questparam)) != null;
        ((Selectable) component4).interactable = flag1 && flag2 && LevelLock.IsPlayableQuest(questparam);
        if (!((Selectable) component4).interactable && flag2 && questparam.Chapter != null && MonoSingleton<GameManager>.Instance.Player.IsQuestArchiveOpenByArea(questparam.Chapter.iname) && LevelLock.IsPlayableQuest(questparam))
          ((Selectable) component4).interactable = true;
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) srpgButton, (UnityEngine.Object) null))
          ((Selectable) srpgButton).interactable = ((Selectable) component4).interactable;
      }
      DataSource.Bind<QuestParam>(gameObject, questparam);
      gameObject.transform.SetParent((Transform) this.QuestListParent, false);
      gameObject.SetActive(true);
    }

    public bool IsCommonEquipUI(long unit_id, int slot)
    {
      if (!this.IsTreeTop)
        return false;
      GameManager instance = MonoSingleton<GameManager>.Instance;
      UnitData unitDataByUniqueId = instance.Player.FindUnitDataByUniqueID(unit_id);
      int selectedJobIndex = this.GetSelectedJobIndex(unitDataByUniqueId);
      if (unitDataByUniqueId.GetRankupEquipData(selectedJobIndex, slot).IsEquiped())
        return false;
      JobData job = unitDataByUniqueId.Jobs[selectedJobIndex];
      if (job.Rank == 0)
        return false;
      string rankupItem = job.GetRankupItems(job.Rank)[slot];
      ItemData itemDataByItemId1 = instance.Player.FindItemDataByItemID(rankupItem);
      ItemParam itemParam = instance.GetItemParam(rankupItem);
      if (itemParam == null || !itemParam.IsCommon || itemParam.equipLv > unitDataByUniqueId.Lv)
        return false;
      if (itemDataByItemId1 != null && itemDataByItemId1.Num >= 1)
        return true;
      ItemParam commonEquip = instance.MasterParam.GetCommonEquip(itemParam, job.Rank == 0);
      if (commonEquip == null)
        return false;
      ItemData itemDataByItemId2 = instance.Player.FindItemDataByItemID(commonEquip.iname);
      if (itemDataByItemId2 == null)
        return true;
      if (instance.MasterParam.FixParam.EquipCommonPieceNum == null)
        return false;
      int num1 = (int) instance.MasterParam.FixParam.EquipCommonPieceNum[itemParam.rare];
      int num2 = job.Rank <= 0 ? 1 : num1;
      return itemDataByItemId2 == null || itemDataByItemId2.Num < num2 || job.Rank > 0;
    }

    public void ActivateCommonUI(bool is_common)
    {
      if (this.CommonUI != null)
      {
        for (int index1 = 0; index1 < this.CommonUI.Length; ++index1)
        {
          if (!UnityEngine.Object.op_Equality((UnityEngine.Object) this.CommonUI[index1], (UnityEngine.Object) null))
          {
            this.CommonUI[index1].SetActive(is_common);
            for (int index2 = 0; index2 < this.EquipButtonColor.Count; ++index2)
              this.EquipButtonColor[index2].Reset();
          }
        }
      }
      if (this.NoCommonUI == null)
        return;
      for (int index3 = 0; index3 < this.NoCommonUI.Length; ++index3)
      {
        if (!UnityEngine.Object.op_Equality((UnityEngine.Object) this.NoCommonUI[index3], (UnityEngine.Object) null))
        {
          this.NoCommonUI[index3].SetActive(!is_common);
          for (int index4 = 0; index4 < this.CommonButtonEquipColor.Count; ++index4)
            this.CommonButtonEquipColor[index4].Reset();
        }
      }
    }

    public void ActiveCommonEquipButton(bool is_common)
    {
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.QuestCommonEquipButton, (UnityEngine.Object) null))
        this.QuestCommonEquipButton.SetActive(is_common);
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.RecipeCommonEquipButton, (UnityEngine.Object) null))
        return;
      this.RecipeCommonEquipButton.SetActive(is_common);
    }

    public delegate void EquipEvent();

    public delegate void EquipReloadEvent();

    public class ResetDefaultColor
    {
      private Graphic graphic;
      private Color color;

      public ResetDefaultColor(Graphic graphic)
      {
        this.graphic = graphic;
        this.color = graphic.color;
      }

      public void Reset() => this.graphic.color = this.color;
    }
  }
}
