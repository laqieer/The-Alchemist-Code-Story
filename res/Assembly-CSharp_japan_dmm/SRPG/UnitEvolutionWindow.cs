// Decompiled with JetBrains decompiler
// Type: SRPG.UnitEvolutionWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  [FlowNode.Pin(0, "表示を更新", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(2, "閉じる", FlowNode.PinTypes.Input, 2)]
  [FlowNode.Pin(3, "詳細ボタンが押された", FlowNode.PinTypes.Input, 3)]
  [FlowNode.Pin(100, "ユニットが進化した", FlowNode.PinTypes.Output, 100)]
  [FlowNode.Pin(101, "入手クエストが選択された", FlowNode.PinTypes.Output, 101)]
  [FlowNode.Pin(102, "閉じる：完了", FlowNode.PinTypes.Output, 102)]
  public class UnitEvolutionWindow : MonoBehaviour, IFlowInterface
  {
    [HelpBox("アイテムの親となるゲームオブジェクト")]
    public RectTransform ListParent;
    [HelpBox("アイテムスロットの雛形")]
    public ListItemEvents ItemSlotTemplate;
    [HelpBox("不要なスロットの雛形")]
    public GameObject UnusedSlotTemplate;
    [HelpBox("不要なスロットを表示する")]
    public bool ShowUnusedSlots = true;
    [HelpBox("最大スロット数")]
    public int MaxSlots = 5;
    [HelpBox("足りてないものを表示するラベル")]
    public Text HelpText;
    public Button EvolveButton;
    public UnitData Unit;
    public ScrollRect ScrollParent;
    public Transform QuestListParent;
    public GameObject QuestListItemTemplate;
    public SRPG_Button MainPanelCloseBtn;
    public GameObject ItemSlotRoot;
    public GameObject ItemSlotBox;
    public GameObject MainPanel;
    public GameObject SubPanel;
    private List<GameObject> mItems = new List<GameObject>();
    private UnitData mCurrentUnit;
    private List<GameObject> mBoxs = new List<GameObject>();
    private List<GameObject> mGainedQuests = new List<GameObject>();
    private string mLastSelectItemIname;
    private float mDecelerationRate;
    public UnitEvolutionWindow.UnitEvolveEvent OnEvolve;
    public UnitEvolutionWindow.EvolveCloseEvent OnEvolveClose;
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

    public void Activated(int pinID)
    {
      switch (pinID)
      {
        case 0:
          this.Refresh2();
          break;
        case 2:
          if (this.OnEvolveClose != null)
            this.OnEvolveClose();
          FlowNode_GameObject.ActivateOutputLinks((Component) this, 102);
          break;
        case 3:
          if (!(FlowNode_ButtonEvent.currentValue is SerializeValueList currentValue))
            break;
          QuestParam dataOfClass = DataSource.FindDataOfClass<QuestParam>(currentValue.GetGameObject("_self"), (QuestParam) null);
          if (dataOfClass == null)
            break;
          this.OpenQuestDetail(dataOfClass);
          break;
      }
    }

    private void Start()
    {
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.ItemSlotTemplate, (UnityEngine.Object) null))
        ((Component) this.ItemSlotTemplate).gameObject.SetActive(false);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.UnusedSlotTemplate, (UnityEngine.Object) null))
        this.UnusedSlotTemplate.gameObject.SetActive(false);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.EvolveButton, (UnityEngine.Object) null))
      {
        // ISSUE: method pointer
        ((UnityEvent) this.EvolveButton.onClick).AddListener(new UnityAction((object) this, __methodptr(OnEvolveClick)));
      }
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.ItemSlotBox, (UnityEngine.Object) null))
        this.ItemSlotBox.SetActive(false);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.SubPanel, (UnityEngine.Object) null))
        this.SubPanel.SetActive(false);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.QuestListItemTemplate, (UnityEngine.Object) null))
        this.QuestListItemTemplate.SetActive(false);
      if (!string.IsNullOrEmpty(this.QuestDetail))
        this.mReqQuestDetail = AssetManager.LoadAsync<GameObject>(this.QuestDetail);
      if (!string.IsNullOrEmpty(this.QuestDetailMulti))
        this.mReqQuestMultiDetail = AssetManager.LoadAsync<GameObject>(this.QuestDetailMulti);
      this.Refresh2();
    }

    private void OnEvolveClick()
    {
      if (this.OnEvolveClose != null)
        this.OnEvolveClose();
      if (this.OnEvolve != null)
      {
        this.OnEvolve();
      }
      else
      {
        MonoSingleton<GameManager>.Instance.Player.RarityUpUnit(this.mCurrentUnit);
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 100);
      }
    }

    private RecipeParam GetCurrentRecipe(UnitData unit)
    {
      unit = unit != null ? unit : this.mCurrentUnit;
      if (unit != null)
        return MonoSingleton<GameManager>.Instance.GetRecipeParam(unit.UnitParam.recipes[unit.Rarity]);
      DebugUtility.LogError("UnitEvolutionWindow.cs => GetCurrentRecipe():unit and mCurrentUnit is Null References!");
      return (RecipeParam) null;
    }

    private void OnItemSelect2(GameObject go)
    {
      if (this.mCurrentUnit == null)
      {
        DebugUtility.LogError("UnitEvolutionWindow.cs => OnItemSelect2():mCurrentUnit is Null or Empty!");
      }
      else
      {
        int index = this.mItems.IndexOf(go);
        if (index < 0)
          return;
        this.RefreshSubPanel(index);
      }
    }

    private void RefreshSubPanel(int index = -1)
    {
      this.ClearPanel();
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.MainPanelCloseBtn, (UnityEngine.Object) null))
      {
        DebugUtility.LogWarning("UnitEvolutionWindow.cs => RefreshSubPanel():MainPanelCloseBtn is Null References!");
      }
      else
      {
        ((Component) this.MainPanelCloseBtn).gameObject.SetActive(false);
        if (index < 0)
        {
          DebugUtility.LogWarning("UnitEvolutionWindow.cs => RefreshSubPanel():index is 0!");
        }
        else
        {
          RecipeParam currentRecipe = this.GetCurrentRecipe(this.mCurrentUnit);
          if (currentRecipe == null)
          {
            DebugUtility.LogError("UnitEvolutionWindow.cs => RefreshSubPanel():recipeParam is Null References!");
          }
          else
          {
            ItemParam itemParam = MonoSingleton<GameManager>.GetInstanceDirect().GetItemParam(currentRecipe.items[index].iname);
            if (itemParam == null)
            {
              DebugUtility.LogError("UnitEvolutionWindow.cs => RefreshSubPanel():itemParam is Null References!");
            }
            else
            {
              this.SubPanel.SetActive(true);
              DataSource.Bind<ItemParam>(this.SubPanel, itemParam);
              GameParameter.UpdateAll(this.SubPanel.gameObject);
              if (this.mLastSelectItemIname != itemParam.iname)
              {
                this.ResetScrollPosition();
                this.mLastSelectItemIname = itemParam.iname;
              }
              if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) QuestDropParam.Instance, (UnityEngine.Object) null))
                return;
              QuestParam[] availableQuests = MonoSingleton<GameManager>.Instance.Player.AvailableQuests;
              foreach (QuestParam itemDropQuest in QuestDropParam.Instance.GetItemDropQuestList(itemParam, GlobalVars.GetDropTableGeneratedDateTime()))
              {
                QuestParam qp = itemDropQuest;
                DebugUtility.Log("QuestList:" + qp.iname);
                bool isActive = Array.Find<QuestParam>(availableQuests, (Predicate<QuestParam>) (p => p.iname == qp.iname)) != null;
                this.AddList(qp, isActive);
              }
            }
          }
        }
      }
    }

    private void AddList(QuestParam qparam, bool isActive = false)
    {
      if (qparam == null || qparam.IsMulti)
        DebugUtility.LogWarning("UnitEvolutionWindow.cs => AddList():qparam is Null Reference!");
      else if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.QuestListItemTemplate, (UnityEngine.Object) null))
        DebugUtility.LogWarning("UnitEvolutionWindow.cs => AddList():QuestListItemTemplate is Null Reference!");
      else if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.QuestListParent, (UnityEngine.Object) null))
      {
        DebugUtility.LogWarning("UnitEvolutionWindow.cs => AddList():QuestListParent is Null Reference!");
      }
      else
      {
        GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.QuestListItemTemplate);
        this.mGainedQuests.Add(gameObject);
        SRPG_Button component1 = gameObject.GetComponent<SRPG_Button>();
        bool flag = qparam.IsDateUnlock() && LevelLock.IsPlayableQuest(qparam);
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component1, (UnityEngine.Object) null))
        {
          component1.AddListener(new SRPG_Button.ButtonClickEvent(this.OnQuestSelect));
          ((Selectable) component1).interactable = flag && isActive;
        }
        UnitEquipmentQuestItem component2 = gameObject.GetComponent<UnitEquipmentQuestItem>();
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component2, (UnityEngine.Object) null) && UnityEngine.Object.op_Inequality((UnityEngine.Object) component2.QuestDetail, (UnityEngine.Object) null))
        {
          Button component3 = component2.QuestDetail.GetComponent<Button>();
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component3, (UnityEngine.Object) null))
            ((Selectable) component3).interactable = flag && isActive;
        }
        DataSource.Bind<QuestParam>(gameObject, qparam);
        gameObject.transform.SetParent(this.QuestListParent, false);
        gameObject.SetActive(true);
      }
    }

    private void ClearPanel()
    {
      GameUtility.DestroyGameObjects(this.mGainedQuests);
      this.mGainedQuests.Clear();
    }

    private void ResetScrollPosition()
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.ScrollParent, (UnityEngine.Object) null))
        return;
      this.mDecelerationRate = this.ScrollParent.decelerationRate;
      this.ScrollParent.decelerationRate = 0.0f;
      RectTransform questListParent = this.QuestListParent as RectTransform;
      questListParent.anchoredPosition = new Vector2(questListParent.anchoredPosition.x, 0.0f);
      this.StartCoroutine(this.RefreshScrollRect());
    }

    [DebuggerHidden]
    private IEnumerator RefreshScrollRect()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new UnitEvolutionWindow.\u003CRefreshScrollRect\u003Ec__Iterator0()
      {
        \u0024this = this
      };
    }

    private void OnQuestSelect(SRPG_Button button)
    {
      QuestParam quest = DataSource.FindDataOfClass<QuestParam>(this.mGainedQuests[this.mGainedQuests.IndexOf(((Component) button).gameObject)], (QuestParam) null);
      if (quest == null)
        DebugUtility.LogWarning("UnitEvolutionWindow.cs => OnQuestSelect():quest is Null Reference!");
      else if (!quest.IsDateUnlock())
        UIUtility.NegativeSystemMessage((string) null, LocalizedText.Get("sys.DISABLE_QUEST_DATE_UNLOCK"), (UIUtility.DialogResultEvent) null);
      else if (Array.Find<QuestParam>(MonoSingleton<GameManager>.GetInstanceDirect().Player.AvailableQuests, (Predicate<QuestParam>) (p => p.iname == quest.iname)) == null)
      {
        UIUtility.NegativeSystemMessage((string) null, LocalizedText.Get("sys.DISABLE_QUEST_CHALLENGE"), (UIUtility.DialogResultEvent) null);
      }
      else
      {
        PlayerData player = MonoSingleton<GameManager>.Instance.Player;
        if (LevelLock.IsNeedCheckUnlockConds(quest))
        {
          UnlockTargets targetByQuestId = LevelLock.GetTargetByQuestId(quest.iname, UnlockTargets.EventQuest);
          if (LevelLock.ShowLockMessage(player.Lv, player.VipRank, targetByQuestId))
            return;
        }
        GlobalVars.SelectedQuestID = quest.iname;
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 101);
      }
    }

    public void Refresh2()
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.ItemSlotTemplate, (UnityEngine.Object) null))
        DebugUtility.LogWarning("UnitEvolutionWindow.cs => Refresh2():ItemSlotTemplate is Null or Empty!");
      else if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.ItemSlotRoot, (UnityEngine.Object) null))
        DebugUtility.LogWarning("UnitEvolutionWindow.cs => Refresh2():ItemSlotRoot is Null or Empty!");
      else if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.ItemSlotBox, (UnityEngine.Object) null))
        DebugUtility.LogWarning("UnitEvolutionWindow.cs => Refresh2():ItemSlotBox is Null or Empty!");
      else if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.SubPanel, (UnityEngine.Object) null))
      {
        DebugUtility.LogWarning("UnitEvolutionWindow.cs => Refresh2():SubPanel is Null References!");
      }
      else
      {
        this.SubPanel.SetActive(false);
        if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.MainPanelCloseBtn, (UnityEngine.Object) null))
        {
          DebugUtility.LogWarning("UnitEvolutionWindow.cs => Refresh2():MainPanelCloseBtn is Null References!");
        }
        else
        {
          ((Component) this.MainPanelCloseBtn).gameObject.SetActive(true);
          GameManager instanceDirect = MonoSingleton<GameManager>.GetInstanceDirect();
          if (UnityEngine.Object.op_Equality((UnityEngine.Object) instanceDirect, (UnityEngine.Object) null))
            return;
          UnitData unitData = this.Unit == null ? instanceDirect.Player.FindUnitDataByUniqueID((long) GlobalVars.SelectedUnitUniqueID) : this.Unit;
          if (unitData == null)
            return;
          this.mCurrentUnit = unitData;
          GameUtility.DestroyGameObjects(this.mItems);
          this.mItems.Clear();
          GameUtility.DestroyGameObjects(this.mBoxs);
          this.mBoxs.Clear();
          DataSource.Bind<UnitData>(((Component) this).gameObject, unitData);
          GameParameter.UpdateAll(((Component) this).gameObject);
          string key = (string) null;
          bool flag = unitData.CheckUnitRarityUp();
          RecipeParam currentRecipe = this.GetCurrentRecipe(unitData);
          DataSource.Bind<RecipeParam>(((Component) this).gameObject, currentRecipe);
          if (string.IsNullOrEmpty(key) && currentRecipe != null && currentRecipe.cost > instanceDirect.Player.Gold)
          {
            key = "sys.GOLD_NOT_ENOUGH";
            flag = false;
          }
          if (string.IsNullOrEmpty(key) && unitData.Lv < unitData.GetRarityLevelCap(unitData.Rarity))
          {
            key = "sys.LEVEL_NOT_ENOUGH";
            flag = false;
          }
          if (currentRecipe == null)
            return;
          if (currentRecipe.items == null || currentRecipe.items.Length <= 0)
          {
            DebugUtility.LogWarning("UnitEvolutionWindow.cs => Refresh2():recipe_param.items is Null or Count 0!");
          }
          else
          {
            int length = currentRecipe.items.Length;
            GridLayoutGroup component = this.ItemSlotBox.GetComponent<GridLayoutGroup>();
            if (UnityEngine.Object.op_Equality((UnityEngine.Object) component, (UnityEngine.Object) null))
            {
              DebugUtility.LogWarning("UnitEvolutionWindow.cs => Refresh2():gridlayout is Not Component [GridLayoutGroup]!");
            }
            else
            {
              GameObject gameObject1 = UnityEngine.Object.Instantiate<GameObject>(this.ItemSlotBox);
              gameObject1.transform.SetParent(this.ItemSlotRoot.transform, false);
              gameObject1.SetActive(true);
              this.mBoxs.Add(gameObject1);
              if (length > component.constraintCount)
              {
                GameObject gameObject2 = UnityEngine.Object.Instantiate<GameObject>(this.ItemSlotBox);
                gameObject2.transform.SetParent(this.ItemSlotRoot.transform, false);
                gameObject2.SetActive(true);
                this.mBoxs.Add(gameObject2);
              }
              for (int index1 = 0; index1 < length; ++index1)
              {
                RecipeItem recipeItem = currentRecipe.items[index1];
                if (recipeItem != null && !string.IsNullOrEmpty(recipeItem.iname))
                {
                  int index2 = 0;
                  if (length > component.constraintCount)
                  {
                    if (length % 2 == 0)
                    {
                      if (index1 >= component.constraintCount - 1)
                        index2 = 1;
                    }
                    else if (index1 >= component.constraintCount)
                      index2 = 1;
                  }
                  ListItemEvents listItemEvents = UnityEngine.Object.Instantiate<ListItemEvents>(this.ItemSlotTemplate);
                  ((Component) listItemEvents).transform.SetParent(this.mBoxs[index2].transform, false);
                  this.mItems.Add(((Component) listItemEvents).gameObject);
                  listItemEvents.OnSelect = new ListItemEvents.ListItemEvent(this.OnItemSelect2);
                  ((Component) listItemEvents).gameObject.SetActive(true);
                  ItemParam itemParam = instanceDirect.GetItemParam(recipeItem.iname);
                  if (itemParam == null)
                  {
                    DebugUtility.LogWarning("UnitEvolutionWindow.cs => Refresh2():item_param is Null References!");
                    return;
                  }
                  DataSource.Bind<ItemParam>(((Component) listItemEvents).gameObject, itemParam);
                  JobEvolutionRecipe data = new JobEvolutionRecipe();
                  data.Item = itemParam;
                  data.RecipeItem = recipeItem;
                  data.Amount = instanceDirect.Player.GetItemAmount(recipeItem.iname);
                  data.RequiredAmount = recipeItem.num;
                  if (data.Amount < data.RequiredAmount)
                  {
                    flag = false;
                    if (key == null)
                      key = "sys.ITEM_NOT_ENOUGH";
                  }
                  DataSource.Bind<JobEvolutionRecipe>(((Component) listItemEvents).gameObject, data);
                }
              }
              if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.HelpText, (UnityEngine.Object) null))
              {
                ((Component) this.HelpText).gameObject.SetActive(!string.IsNullOrEmpty(key));
                if (!string.IsNullOrEmpty(key))
                  this.HelpText.text = LocalizedText.Get(key);
              }
              if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.EvolveButton, (UnityEngine.Object) null))
                return;
              ((Selectable) this.EvolveButton).interactable = flag;
            }
          }
        }
      }
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

    public delegate void UnitEvolveEvent();

    public delegate void EvolveCloseEvent();
  }
}
