// Decompiled with JetBrains decompiler
// Type: SRPG.UnitTobiraEnhanceWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  [FlowNode.Pin(0, "表示を更新", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(50, "素材をクリック", FlowNode.PinTypes.Input, 50)]
  [FlowNode.Pin(51, "クエストをクリック", FlowNode.PinTypes.Input, 51)]
  [FlowNode.Pin(52, "クエスト詳細ボタンをクリック", FlowNode.PinTypes.Input, 52)]
  [FlowNode.Pin(100, "扉強化ボタン押下", FlowNode.PinTypes.Input, 100)]
  [FlowNode.Pin(101, "入手クエストが選択された", FlowNode.PinTypes.Output, 101)]
  [FlowNode.Pin(102, "閉じる：完了", FlowNode.PinTypes.Output, 102)]
  [FlowNode.Pin(103, "代用アイテムを使うか確認", FlowNode.PinTypes.Output, 103)]
  public class UnitTobiraEnhanceWindow : MonoBehaviour, IFlowInterface
  {
    public const int ON_CLICK_ELEMENT_BUTTON = 50;
    public const int ON_CLICK_QUEST_BUTTON = 51;
    public const int ON_CLICK_QUESTDETAIL_BUTTON = 52;
    public const int ON_CLICK_TOBIRA_ENHANCE_BUTTON = 100;
    public const int OUT_SELECT_QUEST = 101;
    public const int OUT_CLOSE_WINDOW = 102;
    public const int OUT_IS_USE_SUB_PIESE = 103;
    [HelpBox("アイテムの親となるゲームオブジェクト")]
    public RectTransform ListParent;
    [HelpBox("アイテムスロットの雛形")]
    public GameObject ItemSlotTemplate;
    [HelpBox("不要なスロットの雛形")]
    public GameObject UnusedSlotTemplate;
    [HelpBox("不要なスロットを表示する")]
    public bool ShowUnusedSlots = true;
    [HelpBox("最大スロット数")]
    public int MaxSlots = 5;
    [HelpBox("足りてないものを表示するラベル")]
    public Text HelpText;
    [SerializeField]
    private Button EnhanceTobiraButton;
    [SerializeField]
    private ScrollRect ScrollParent;
    [SerializeField]
    private Transform QuestListParent;
    [SerializeField]
    private GameObject QuestListItemTemplate;
    [SerializeField]
    private Button MainPanelCloseBtn;
    [SerializeField]
    private GameObject ItemSlotRoot;
    [SerializeField]
    private GameObject ItemSlotBox;
    [SerializeField]
    private GameObject MainPanel;
    [SerializeField]
    private GameObject SubPanel;
    [SerializeField]
    private Text TitleText;
    [SerializeField]
    private Text MessageText;
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
    private UnitData mCurrentUnit;
    private TobiraData mCurrentTobira;
    private List<GameObject> mItems = new List<GameObject>();
    private List<GameObject> mBoxs = new List<GameObject>();
    private List<GameObject> mGainedQuests = new List<GameObject>();
    private string mLastSelectItemIname;
    private float mDecelerationRate;
    public UnitTobiraEnhanceWindow.CallbackEvent OnCallback;
    private static bool isUseSubPiece;
    private const string CHECK_SUB_PIECE_PREFAB_PATH = "UI/UnitTobiraCommonItemConfirmWindow";

    public static bool IsUseSubPiese => UnitTobiraEnhanceWindow.isUseSubPiece;

    public void Initialize(UnitData unit, TobiraData tobira)
    {
      this.mCurrentUnit = unit;
      this.mCurrentTobira = tobira;
      UnitTobiraEnhanceWindow.isUseSubPiece = false;
      if (!string.IsNullOrEmpty(this.QuestDetail))
        this.mReqQuestDetail = AssetManager.LoadAsync<GameObject>(this.QuestDetail);
      if (string.IsNullOrEmpty(this.QuestDetailMulti))
        return;
      this.mReqQuestMultiDetail = AssetManager.LoadAsync<GameObject>(this.QuestDetailMulti);
    }

    public void Activated(int pinID)
    {
      switch (pinID)
      {
        case 0:
          this.Refresh();
          break;
        case 50:
          if (!(FlowNode_ButtonEvent.currentValue is SerializeValueList currentValue1))
            break;
          TobiraEnhanceRecipe dataSource = currentValue1.GetDataSource<TobiraEnhanceRecipe>("_self");
          if (dataSource == null)
            break;
          this.RefreshSubPanel(dataSource.Index);
          break;
        case 51:
          if (!(FlowNode_ButtonEvent.currentValue is SerializeValueList currentValue2))
            break;
          QuestParam dataOfClass1 = DataSource.FindDataOfClass<QuestParam>(currentValue2.GetGameObject("_self"), (QuestParam) null);
          if (dataOfClass1 == null)
            break;
          this.OnQuestSelect(dataOfClass1);
          break;
        case 52:
          if (!(FlowNode_ButtonEvent.currentValue is SerializeValueList currentValue3))
            break;
          QuestParam dataOfClass2 = DataSource.FindDataOfClass<QuestParam>(currentValue3.GetGameObject("_self"), (QuestParam) null);
          if (dataOfClass2 == null)
            break;
          this.OpenQuestDetail(dataOfClass2);
          break;
        case 100:
          if (UnitTobiraEnhanceWindow.isUseSubPiece)
          {
            GameObject gameObject = AssetManager.Load<GameObject>("UI/UnitTobiraCommonItemConfirmWindow");
            if (UnityEngine.Object.op_Inequality((UnityEngine.Object) gameObject, (UnityEngine.Object) null))
            {
              UnitTobiraCommonItemConfirmWindow component = UnityEngine.Object.Instantiate<GameObject>(gameObject, ((Component) this).transform, false).GetComponent<UnitTobiraCommonItemConfirmWindow>();
              if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component, (UnityEngine.Object) null))
              {
                component.Initialize(this.mCurrentUnit, this.mCurrentTobira, this.GetCurrentRecipe());
                component.OnEnhanceCallback = new UnitTobiraCommonItemConfirmWindow.CallbackEvent(this.RequestTobiraEnhance);
                component.OnCloseCallback = new UnitTobiraCommonItemConfirmWindow.CallbackEvent(this.CloseWindow);
              }
              else
                DebugUtility.LogError("UnitTobiraEnhanceWindow.cs => Activated():subPiecePopupWindow is Not Component [UnitTobiraCommonItemConfirmWindow]!");
            }
            FlowNode_GameObject.ActivateOutputLinks((Component) this, 103);
            break;
          }
          if (this.OnCallback != null)
            this.OnCallback();
          FlowNode_GameObject.ActivateOutputLinks((Component) this, 102);
          break;
      }
    }

    private void RequestTobiraEnhance()
    {
      if (this.OnCallback != null)
        this.OnCallback();
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 102);
    }

    private void CloseWindow() => FlowNode_GameObject.ActivateOutputLinks((Component) this, 102);

    private void Start()
    {
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.ItemSlotTemplate, (UnityEngine.Object) null))
        this.ItemSlotTemplate.SetActive(false);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.UnusedSlotTemplate, (UnityEngine.Object) null))
        this.UnusedSlotTemplate.SetActive(false);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.ItemSlotBox, (UnityEngine.Object) null))
        this.ItemSlotBox.SetActive(false);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.SubPanel, (UnityEngine.Object) null))
        this.SubPanel.SetActive(false);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.QuestListItemTemplate, (UnityEngine.Object) null))
        this.QuestListItemTemplate.SetActive(false);
      string categoryName = TobiraParam.GetCategoryName(this.mCurrentTobira.Param.TobiraCategory);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.TitleText, (UnityEngine.Object) null))
        this.TitleText.text = string.Format(LocalizedText.Get("sys.TOBIRA_ENHANCE_ITEM_BTN_ENHANCE_TITLE"), (object) categoryName);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.MessageText, (UnityEngine.Object) null))
        this.MessageText.text = string.Format(LocalizedText.Get("sys.TOBIRA_ENHANCE_ITEM_BTN_ENHANCE_MESSAGE"), (object) categoryName);
      this.Refresh();
    }

    private void OnQuestSelect(QuestParam quest)
    {
      if (quest == null)
        DebugUtility.LogWarning("UnitTobiraEnhanceWindow.cs => OnQuestSelect():quest is Null Reference!");
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

    private void RefreshSubPanel(int index)
    {
      GameUtility.DestroyGameObjects(this.mGainedQuests);
      this.mGainedQuests.Clear();
      ((Component) this.MainPanelCloseBtn).gameObject.SetActive(false);
      if (index < 0)
      {
        DebugUtility.LogWarning("UnitTobiraEnhanceWindow.cs => RefreshSubPanel():index is 0!");
      }
      else
      {
        TobiraRecipeParam currentRecipe = this.GetCurrentRecipe();
        if (currentRecipe == null)
        {
          DebugUtility.LogError("UnitTobiraEnhanceWindow.cs => RefreshSubPanel():recipeParam is Null References!");
        }
        else
        {
          ItemParam data = (ItemParam) null;
          int num = 0;
          if (currentRecipe.UnitPieceNum > 0)
          {
            if (index == num)
            {
              data = MonoSingleton<GameManager>.Instance.GetItemParam(this.mCurrentUnit.UnitParam.piece);
              goto label_22;
            }
            else
              ++num;
          }
          if (currentRecipe.ElementNum > 0)
          {
            if (index == num)
            {
              data = this.mCurrentUnit.GetElementPieceParam();
              goto label_22;
            }
            else
              ++num;
          }
          if (currentRecipe.UnlockElementNum > 0)
          {
            if (index == num)
            {
              data = MonoSingleton<GameManager>.Instance.GetItemParam(this.mCurrentUnit.GetUnlockTobiraElementID());
              goto label_22;
            }
            else
              ++num;
          }
          if (currentRecipe.UnlockBirthNum > 0)
          {
            if (index == num)
            {
              data = MonoSingleton<GameManager>.Instance.GetItemParam(this.mCurrentUnit.GetUnlockTobiraBirthID());
              goto label_22;
            }
            else
              ++num;
          }
          int index1 = index - num;
          if (0 <= index1 && index1 < currentRecipe.Materials.Length)
            data = MonoSingleton<GameManager>.Instance.GetItemParam(currentRecipe.Materials[index1].Iname);
label_22:
          if (data == null)
          {
            DebugUtility.LogError("UnitTobiraEnhanceWindow.cs => RefreshSubPanel():itemParam is Null References!");
          }
          else
          {
            this.SubPanel.SetActive(true);
            DataSource.Bind<ItemParam>(this.SubPanel, data);
            GameParameter.UpdateAll(this.SubPanel.gameObject);
            if (this.mLastSelectItemIname != data.iname)
            {
              this.ResetScrollPosition();
              this.mLastSelectItemIname = data.iname;
            }
            if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) QuestDropParam.Instance, (UnityEngine.Object) null))
              return;
            QuestParam[] availableQuests = MonoSingleton<GameManager>.Instance.Player.AvailableQuests;
            foreach (QuestParam itemDropQuest in QuestDropParam.Instance.GetItemDropQuestList(data, GlobalVars.GetDropTableGeneratedDateTime()))
            {
              QuestParam qp = itemDropQuest;
              bool isActive = Array.Find<QuestParam>(availableQuests, (Predicate<QuestParam>) (p => p.iname == qp.iname)) != null;
              this.AddList(qp, isActive);
            }
          }
        }
      }
    }

    private void AddList(QuestParam qparam, bool isActive = false)
    {
      if (qparam == null || qparam.IsMulti)
        DebugUtility.LogWarning("UnitTobiraEnhanceWindow.cs => AddList():qparam is Null Reference!");
      else if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.QuestListItemTemplate, (UnityEngine.Object) null))
        DebugUtility.LogWarning("UnitTobiraEnhanceWindow.cs => AddList():QuestListItemTemplate is Null Reference!");
      else if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.QuestListParent, (UnityEngine.Object) null))
      {
        DebugUtility.LogWarning("UnitTobiraEnhanceWindow.cs => AddList():QuestListParent is Null Reference!");
      }
      else
      {
        GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.QuestListItemTemplate);
        gameObject.transform.SetParent(this.QuestListParent, false);
        gameObject.SetActive(true);
        this.mGainedQuests.Add(gameObject);
        Button component1 = gameObject.GetComponent<Button>();
        bool flag = qparam.IsDateUnlock() && LevelLock.IsPlayableQuest(qparam);
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component1, (UnityEngine.Object) null))
          ((Selectable) component1).interactable = flag && isActive;
        UnitEquipmentQuestItem component2 = gameObject.GetComponent<UnitEquipmentQuestItem>();
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component2, (UnityEngine.Object) null) && UnityEngine.Object.op_Inequality((UnityEngine.Object) component2.QuestDetail, (UnityEngine.Object) null))
        {
          Button component3 = component2.QuestDetail.GetComponent<Button>();
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component3, (UnityEngine.Object) null))
            ((Selectable) component3).interactable = flag && isActive;
        }
        DataSource.Bind<QuestParam>(gameObject, qparam);
      }
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
      return (IEnumerator) new UnitTobiraEnhanceWindow.\u003CRefreshScrollRect\u003Ec__Iterator0()
      {
        \u0024this = this
      };
    }

    private void Refresh()
    {
      if (!this._CheckNullReference())
        return;
      ((Component) this.MainPanelCloseBtn).gameObject.SetActive(true);
      this.SubPanel.SetActive(false);
      GameManager instanceDirect = MonoSingleton<GameManager>.GetInstanceDirect();
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) instanceDirect, (UnityEngine.Object) null))
        return;
      GameUtility.DestroyGameObjects(this.mItems);
      GameUtility.DestroyGameObjects(this.mBoxs);
      this.mItems.Clear();
      this.mBoxs.Clear();
      DataSource.Bind<UnitData>(((Component) this).gameObject, this.mCurrentUnit);
      GameParameter.UpdateAll(((Component) this).gameObject);
      string key = (string) null;
      bool flag1 = true;
      TobiraRecipeParam currentRecipe = this.GetCurrentRecipe();
      if (currentRecipe == null)
        return;
      DataSource.Bind<TobiraRecipeParam>(((Component) this).gameObject, currentRecipe);
      if (currentRecipe.Cost > instanceDirect.Player.Gold)
      {
        key = "sys.GOLD_NOT_ENOUGH";
        flag1 = false;
      }
      int length = currentRecipe.Materials.Length;
      if (currentRecipe.UnitPieceNum > 0)
        ++length;
      if (currentRecipe.ElementNum > 0)
        ++length;
      if (currentRecipe.UnlockElementNum > 0)
        ++length;
      if (currentRecipe.UnlockBirthNum > 0)
        ++length;
      GridLayoutGroup component1 = this.ItemSlotBox.GetComponent<GridLayoutGroup>();
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) component1, (UnityEngine.Object) null))
      {
        DebugUtility.LogWarning("UnitTobiraEnhanceWindow.cs => Refresh2():gridlayout is Not Component [GridLayoutGroup]!");
      }
      else
      {
        int num1 = length / component1.constraintCount + 1;
        for (int index = 0; index < num1; ++index)
        {
          GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.ItemSlotBox);
          gameObject.transform.SetParent(this.ItemSlotRoot.transform, false);
          gameObject.SetActive(true);
          this.mBoxs.Add(gameObject);
        }
        int num2 = 0;
        int index1 = 0;
        if (currentRecipe.UnitPieceNum > 0)
        {
          GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.ItemSlotTemplate);
          gameObject.transform.SetParent(this.mBoxs[index1].transform, false);
          gameObject.SetActive(true);
          this.mItems.Add(gameObject.gameObject);
          ItemParam itemParam = instanceDirect.GetItemParam(this.mCurrentUnit.UnitParam.piece);
          if (itemParam == null)
          {
            DebugUtility.LogWarning("UnitTobiraEnhanceWindow.cs => Refresh():item_param is Null References!");
            return;
          }
          DataSource.Bind<ItemParam>(gameObject.gameObject, itemParam);
          DataSource.Bind<TobiraEnhanceRecipe>(gameObject.gameObject, new TobiraEnhanceRecipe()
          {
            Amount = instanceDirect.Player.GetItemAmount(itemParam.iname),
            RequiredAmount = currentRecipe.UnitPieceNum,
            Index = num2
          });
          if (flag1 && currentRecipe.UnitPieceNum > instanceDirect.Player.GetItemAmount(itemParam.iname))
          {
            if (this.isSubstitutionUnitPiece())
            {
              UnitTobiraEnhanceWindow.isUseSubPiece = true;
              TobiraEnhanceIcon component2 = gameObject.GetComponent<TobiraEnhanceIcon>();
              if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component2, (UnityEngine.Object) null))
                component2.ShowUseSubPieceIcon();
            }
            else
            {
              flag1 = false;
              key = "sys.ITEM_NOT_ENOUGH";
            }
          }
          ++num2;
        }
        int index2 = num2 / component1.constraintCount;
        if (currentRecipe.ElementNum > 0)
        {
          GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.ItemSlotTemplate);
          gameObject.transform.SetParent(this.mBoxs[index2].transform, false);
          gameObject.SetActive(true);
          this.mItems.Add(gameObject.gameObject);
          ItemParam elementPieceParam = this.mCurrentUnit.GetElementPieceParam();
          if (elementPieceParam == null)
          {
            DebugUtility.LogWarning("UnitTobiraEnhanceWindow.cs => Refresh():item_param is Null References!");
            return;
          }
          DataSource.Bind<ItemParam>(gameObject.gameObject, elementPieceParam);
          DataSource.Bind<TobiraEnhanceRecipe>(gameObject.gameObject, new TobiraEnhanceRecipe()
          {
            Amount = instanceDirect.Player.GetItemAmount(elementPieceParam.iname),
            RequiredAmount = currentRecipe.ElementNum,
            Index = num2
          });
          if (flag1 && currentRecipe.ElementNum > instanceDirect.Player.GetItemAmount(elementPieceParam.iname))
          {
            flag1 = false;
            key = "sys.ITEM_NOT_ENOUGH";
          }
          ++num2;
        }
        int index3 = num2 / component1.constraintCount;
        if (currentRecipe.UnlockElementNum > 0)
        {
          GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.ItemSlotTemplate);
          gameObject.transform.SetParent(this.mBoxs[index3].transform, false);
          gameObject.SetActive(true);
          this.mItems.Add(gameObject.gameObject);
          ItemParam itemParam = instanceDirect.GetItemParam(this.mCurrentUnit.GetUnlockTobiraElementID());
          if (itemParam == null)
          {
            DebugUtility.LogWarning("UnitTobiraEnhanceWindow.cs => Refresh():item_param is Null References!");
            return;
          }
          DataSource.Bind<ItemParam>(gameObject.gameObject, itemParam);
          DataSource.Bind<TobiraEnhanceRecipe>(gameObject.gameObject, new TobiraEnhanceRecipe()
          {
            Amount = instanceDirect.Player.GetItemAmount(itemParam.iname),
            RequiredAmount = currentRecipe.UnlockElementNum,
            Index = num2
          });
          if (flag1 && currentRecipe.UnlockElementNum > instanceDirect.Player.GetItemAmount(this.mCurrentUnit.GetUnlockTobiraElementID()))
          {
            flag1 = false;
            key = "sys.ITEM_NOT_ENOUGH";
          }
          ++num2;
        }
        int index4 = num2 / component1.constraintCount;
        if (currentRecipe.UnlockBirthNum > 0)
        {
          GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.ItemSlotTemplate);
          gameObject.transform.SetParent(this.mBoxs[index4].transform, false);
          gameObject.SetActive(true);
          this.mItems.Add(gameObject.gameObject);
          ItemParam itemParam = instanceDirect.GetItemParam(this.mCurrentUnit.GetUnlockTobiraBirthID());
          if (itemParam == null)
          {
            DebugUtility.LogWarning("UnitTobiraEnhanceWindow.cs => Refresh():item_param is Null References!");
            return;
          }
          DataSource.Bind<ItemParam>(gameObject.gameObject, itemParam);
          DataSource.Bind<TobiraEnhanceRecipe>(gameObject.gameObject, new TobiraEnhanceRecipe()
          {
            Amount = instanceDirect.Player.GetItemAmount(itemParam.iname),
            RequiredAmount = currentRecipe.UnlockBirthNum,
            Index = num2
          });
          if (flag1 && currentRecipe.UnlockBirthNum > instanceDirect.Player.GetItemAmount(this.mCurrentUnit.GetUnlockTobiraBirthID()))
          {
            flag1 = false;
            key = "sys.ITEM_NOT_ENOUGH";
          }
          ++num2;
        }
        foreach (TobiraRecipeMaterialParam material in currentRecipe.Materials)
        {
          if (material != null && !string.IsNullOrEmpty(material.Iname))
          {
            int index5 = num2 / component1.constraintCount;
            GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.ItemSlotTemplate);
            gameObject.transform.SetParent(this.mBoxs[index5].transform, false);
            gameObject.SetActive(true);
            this.mItems.Add(gameObject.gameObject);
            ItemParam itemParam = instanceDirect.GetItemParam(material.Iname);
            if (itemParam == null)
            {
              DebugUtility.LogWarning("UnitTobiraEnhanceWindow.cs => Refresh():item_param is Null References!");
              return;
            }
            DataSource.Bind<ItemParam>(gameObject.gameObject, itemParam);
            DataSource.Bind<TobiraEnhanceRecipe>(gameObject.gameObject, new TobiraEnhanceRecipe()
            {
              Amount = instanceDirect.Player.GetItemAmount(itemParam.iname),
              RequiredAmount = material.Num,
              Index = num2
            });
            if (flag1 && material.Num > instanceDirect.Player.GetItemAmount(material.Iname))
            {
              flag1 = false;
              key = "sys.ITEM_NOT_ENOUGH";
            }
            ++num2;
          }
        }
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.HelpText, (UnityEngine.Object) null))
        {
          bool flag2 = !string.IsNullOrEmpty(key);
          ((Component) this.HelpText).gameObject.SetActive(flag2);
          if (flag2)
            this.HelpText.text = LocalizedText.Get(key);
        }
        if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.EnhanceTobiraButton, (UnityEngine.Object) null))
          return;
        ((Selectable) this.EnhanceTobiraButton).interactable = flag1;
      }
    }

    private bool isSubstitutionUnitPiece()
    {
      if (string.IsNullOrEmpty(this.mCurrentUnit.UnitParam.subPiece))
        return false;
      int itemAmount1 = MonoSingleton<GameManager>.Instance.Player.GetItemAmount(this.mCurrentUnit.UnitParam.subPiece);
      if (itemAmount1 == 0)
        return false;
      TobiraRecipeParam currentRecipe = this.GetCurrentRecipe();
      if (currentRecipe == null)
        return false;
      int unitPieceNum = currentRecipe.UnitPieceNum;
      int itemAmount2 = MonoSingleton<GameManager>.Instance.Player.GetItemAmount(this.mCurrentUnit.UnitParam.piece);
      return itemAmount2 < unitPieceNum && itemAmount2 + itemAmount1 >= unitPieceNum;
    }

    private bool _CheckNullReference()
    {
      if (this.mCurrentUnit == null || this.mCurrentTobira == null)
      {
        DebugUtility.LogWarning("UnitTobiraEnhanceWindow.cs => Refresh():CurrentUnit or CurrentTobira is Null or Empty!");
        return false;
      }
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.ItemSlotTemplate, (UnityEngine.Object) null))
      {
        DebugUtility.LogWarning("UnitTobiraEnhanceWindow.cs => Refresh():ItemSlotTemplate is Null or Empty!");
        return false;
      }
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.ItemSlotRoot, (UnityEngine.Object) null))
      {
        DebugUtility.LogWarning("UnitTobiraEnhanceWindow.cs => Refresh():ItemSlotRoot is Null or Empty!");
        return false;
      }
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.ItemSlotBox, (UnityEngine.Object) null))
      {
        DebugUtility.LogWarning("UnitTobiraEnhanceWindow.cs => Refresh():ItemSlotBox is Null or Empty!");
        return false;
      }
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.SubPanel, (UnityEngine.Object) null))
      {
        DebugUtility.LogWarning("UnitTobiraEnhanceWindow.cs => Refresh():SubPanel is Null References!");
        return false;
      }
      if (!UnityEngine.Object.op_Equality((UnityEngine.Object) this.MainPanelCloseBtn, (UnityEngine.Object) null))
        return true;
      DebugUtility.LogWarning("UnitTobiraEnhanceWindow.cs => Refresh():MainPanelCloseBtn is Null References!");
      return false;
    }

    private TobiraRecipeParam GetCurrentRecipe()
    {
      if (this.mCurrentUnit != null)
        return MonoSingleton<GameManager>.Instance.MasterParam.GetTobiraRecipe(this.mCurrentUnit.UnitID, this.mCurrentTobira.Param.TobiraCategory, this.mCurrentTobira.Lv);
      DebugUtility.LogError("UnitTobiraEnhanceWindow.cs => GetCurrentRecipe():unit and mCurrentUnit is Null References!");
      return (TobiraRecipeParam) null;
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

    public delegate void CallbackEvent();
  }
}
