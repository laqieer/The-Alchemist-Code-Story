// Decompiled with JetBrains decompiler
// Type: SRPG.UnitCharacterQuestWindow
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
  [FlowNode.Pin(10, "リスト切り替え", FlowNode.PinTypes.Input, 10)]
  [FlowNode.Pin(100, "クエストが選択された", FlowNode.PinTypes.Output, 100)]
  public class UnitCharacterQuestWindow : MonoBehaviour, IFlowInterface
  {
    [SerializeField]
    private Transform QuestList;
    [SerializeField]
    [StringIsResourcePath(typeof (GameObject))]
    private string StoryQuestItemTemplatePath;
    [SerializeField]
    [StringIsResourcePath(typeof (GameObject))]
    private string StoryQuestDisableItemTemplatePath;
    [SerializeField]
    [StringIsResourcePath(typeof (GameObject))]
    private string PieceQuestItemTemplatePath;
    [SerializeField]
    [StringIsResourcePath(typeof (GameObject))]
    private string QuestDetailTemplatePath;
    [SerializeField]
    private GameObject CharacterImage;
    [SerializeField]
    private string PieceQuestWorldId;
    [SerializeField]
    private Image ListToggleButton;
    [SerializeField]
    private Sprite StoryListSprite;
    [SerializeField]
    private Sprite PieceListSprite;
    private List<QuestParam> mQuestList = new List<QuestParam>();
    private List<GameObject> mStoryQuestListItems = new List<GameObject>();
    private List<GameObject> mPieceQuestListItems = new List<GameObject>();
    private bool mIsStoryList = true;
    private bool mListRefreshing;
    private GameObject mQuestDetail;
    private UnitData mCurrentUnit;
    private bool mIsRestore;
    private GameObject mStoryQuestItemPrefab;
    private GameObject mStoryDisableQuestItemPrefab;
    private GameObject mPieceQuestItemPrefab;
    private GameObject mQuestDetailPrefab;

    public UnitData CurrentUnit
    {
      get => this.mCurrentUnit;
      set => this.mCurrentUnit = value;
    }

    public bool IsRestore
    {
      get => this.mIsRestore;
      set => this.mIsRestore = value;
    }

    private GameObject StoryQuestItemPrefab
    {
      get
      {
        if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.mStoryQuestItemPrefab, (UnityEngine.Object) null))
          this.mStoryQuestItemPrefab = AssetManager.Load<GameObject>(this.StoryQuestItemTemplatePath);
        return this.mStoryQuestItemPrefab;
      }
    }

    private GameObject StoryDisableQuestItemPrefab
    {
      get
      {
        if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.mStoryDisableQuestItemPrefab, (UnityEngine.Object) null))
          this.mStoryDisableQuestItemPrefab = AssetManager.Load<GameObject>(this.StoryQuestDisableItemTemplatePath);
        return this.mStoryDisableQuestItemPrefab;
      }
    }

    private GameObject PieceQuestItemPrefab
    {
      get
      {
        if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.mPieceQuestItemPrefab, (UnityEngine.Object) null))
          this.mPieceQuestItemPrefab = AssetManager.Load<GameObject>(this.PieceQuestItemTemplatePath);
        return this.mPieceQuestItemPrefab;
      }
    }

    private GameObject QuestDetailPrefab
    {
      get
      {
        if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.mQuestDetailPrefab, (UnityEngine.Object) null))
          this.mQuestDetailPrefab = AssetManager.Load<GameObject>(this.QuestDetailTemplatePath);
        return this.mQuestDetailPrefab;
      }
    }

    private void Start()
    {
      if (this.IsRestore)
      {
        string lastQuestName = GlobalVars.LastPlayedQuest.Get();
        if (lastQuestName != null && !string.IsNullOrEmpty(lastQuestName))
        {
          QuestParam questParam = Array.Find<QuestParam>(MonoSingleton<GameManager>.Instance.Quests, (Predicate<QuestParam>) (p => p.iname == lastQuestName));
          if (questParam != null && !string.IsNullOrEmpty(questParam.ChapterID))
            this.mIsStoryList = !(questParam.world == this.PieceQuestWorldId);
        }
      }
      this.UpdateToggleButton();
      this.RefreshQuestList();
    }

    private void CreateStoryList()
    {
      this.mQuestList.Clear();
      this.mQuestList = this.CurrentUnit.FindCondQuests();
      UnitData.CharacterQuestParam[] charaEpisodeList = this.CurrentUnit.GetCharaEpisodeList();
      QuestParam[] availableQuests = MonoSingleton<GameManager>.Instance.Player.AvailableQuests;
      for (int i = 0; i < this.mQuestList.Count; ++i)
      {
        bool flag1 = this.mQuestList[i].IsDateUnlock();
        bool flag2 = Array.Find<QuestParam>(availableQuests, (Predicate<QuestParam>) (p => p == this.mQuestList[i])) != null;
        bool flag3 = this.mQuestList[i].state == QuestStates.Cleared;
        bool flag4 = charaEpisodeList[i] != null && charaEpisodeList[i].IsAvailable && this.CurrentUnit.IsChQuestParentUnlocked(this.mQuestList[i]);
        bool flag5 = flag1 && flag2 && !flag3;
        GameObject gameObject;
        if (flag4 || flag3)
        {
          gameObject = UnityEngine.Object.Instantiate<GameObject>(this.StoryQuestItemPrefab);
          Button component = gameObject.GetComponent<Button>();
          if (!UnityEngine.Object.op_Equality((UnityEngine.Object) component, (UnityEngine.Object) null))
            ((Selectable) component).interactable = flag5;
          else
            continue;
        }
        else
          gameObject = UnityEngine.Object.Instantiate<GameObject>(this.StoryDisableQuestItemPrefab);
        if (!UnityEngine.Object.op_Equality((UnityEngine.Object) gameObject, (UnityEngine.Object) null))
        {
          gameObject.SetActive(true);
          gameObject.transform.SetParent(this.QuestList, false);
          DataSource.Bind<QuestParam>(gameObject, this.mQuestList[i]);
          DataSource.Bind<UnitData>(gameObject, this.CurrentUnit);
          DataSource.Bind<UnitParam>(gameObject, this.CurrentUnit.UnitParam);
          ListItemEvents component = gameObject.GetComponent<ListItemEvents>();
          component.OnSelect = new ListItemEvents.ListItemEvent(this.OnQuestSelect);
          component.OnOpenDetail = new ListItemEvents.ListItemEvent(this.OnOpenItemDetail);
          component.OnCloseDetail = new ListItemEvents.ListItemEvent(this.OnCloseItemDetail);
          this.mStoryQuestListItems.Add(gameObject);
        }
      }
    }

    private void CreatePieceQuest()
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.PieceQuestItemPrefab, (UnityEngine.Object) null))
        return;
      GameManager instance = MonoSingleton<GameManager>.Instance;
      List<QuestParam> questParamList = new List<QuestParam>();
      QuestParam[] quests = instance.Quests;
      for (int index = 0; index < quests.Length; ++index)
      {
        if (!string.IsNullOrEmpty(quests[index].world) && quests[index].world == this.PieceQuestWorldId && !string.IsNullOrEmpty(quests[index].ChapterID) && quests[index].ChapterID == this.CurrentUnit.UnitID)
          questParamList.Add(quests[index]);
      }
      if (questParamList.Count <= 1)
        return;
      for (int index = 0; index < questParamList.Count; ++index)
      {
        GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.PieceQuestItemPrefab);
        gameObject.SetActive(true);
        gameObject.transform.SetParent(this.QuestList, false);
        DataSource.Bind<QuestParam>(gameObject, questParamList[index]);
        DataSource.Bind<UnitData>(gameObject, this.CurrentUnit);
        ListItemEvents component = gameObject.GetComponent<ListItemEvents>();
        component.OnSelect = new ListItemEvents.ListItemEvent(this.OnQuestSelect);
        component.OnOpenDetail = new ListItemEvents.ListItemEvent(this.OnOpenItemDetail);
        component.OnCloseDetail = new ListItemEvents.ListItemEvent(this.OnCloseItemDetail);
        this.mPieceQuestListItems.Add(gameObject);
      }
    }

    private void RefreshQuestList()
    {
      if (this.mListRefreshing || UnityEngine.Object.op_Equality((UnityEngine.Object) this.StoryQuestItemPrefab, (UnityEngine.Object) null) || UnityEngine.Object.op_Equality((UnityEngine.Object) this.StoryDisableQuestItemPrefab, (UnityEngine.Object) null) || UnityEngine.Object.op_Equality((UnityEngine.Object) this.QuestList, (UnityEngine.Object) null))
        return;
      this.mListRefreshing = true;
      if (this.mStoryQuestListItems.Count <= 0)
        this.CreateStoryList();
      if (this.mPieceQuestListItems.Count <= 0)
        this.CreatePieceQuest();
      for (int index = 0; index < this.mStoryQuestListItems.Count; ++index)
        this.mStoryQuestListItems[index].SetActive(this.mIsStoryList);
      for (int index = 0; index < this.mPieceQuestListItems.Count; ++index)
        this.mPieceQuestListItems[index].SetActive(!this.mIsStoryList);
      UnitData data = new UnitData();
      data.Setup(this.CurrentUnit);
      data.SetJobSkinAll((string) null);
      DataSource.Bind<UnitData>(this.CharacterImage, data);
      this.mListRefreshing = false;
    }

    private void OnQuestSelect(GameObject button)
    {
      List<GameObject> gameObjectList = !this.mIsStoryList ? this.mPieceQuestListItems : this.mStoryQuestListItems;
      int index = gameObjectList.IndexOf(button.gameObject);
      QuestParam quest = DataSource.FindDataOfClass<QuestParam>(gameObjectList[index], (QuestParam) null);
      if (quest == null)
        return;
      if (!quest.IsDateUnlock())
        UIUtility.NegativeSystemMessage((string) null, LocalizedText.Get("sys.DISABLE_QUEST_DATE_UNLOCK"), (UIUtility.DialogResultEvent) null);
      else if (Array.Find<QuestParam>(MonoSingleton<GameManager>.Instance.Player.AvailableQuests, (Predicate<QuestParam>) (p => p == quest)) == null)
      {
        UIUtility.NegativeSystemMessage((string) null, LocalizedText.Get("sys.DISABLE_QUEST_CHALLENGE"), (UIUtility.DialogResultEvent) null);
      }
      else
      {
        GlobalVars.SelectedQuestID = quest.iname;
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 100);
      }
    }

    private void OnCloseItemDetail(GameObject go)
    {
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mQuestDetail, (UnityEngine.Object) null))
        return;
      UnityEngine.Object.DestroyImmediate((UnityEngine.Object) this.mQuestDetail.gameObject);
      this.mQuestDetail = (GameObject) null;
    }

    private void OnOpenItemDetail(GameObject go)
    {
      QuestParam dataOfClass = DataSource.FindDataOfClass<QuestParam>(go, (QuestParam) null);
      if (dataOfClass == null || !UnityEngine.Object.op_Equality((UnityEngine.Object) this.mQuestDetail, (UnityEngine.Object) null) || !UnityEngine.Object.op_Inequality((UnityEngine.Object) this.QuestDetailPrefab, (UnityEngine.Object) null))
        return;
      this.mQuestDetail = UnityEngine.Object.Instantiate<GameObject>(this.QuestDetailPrefab);
      DataSource.Bind<QuestParam>(this.mQuestDetail, dataOfClass);
      DataSource.Bind<UnitData>(this.mQuestDetail, this.CurrentUnit);
      this.mQuestDetail.SetActive(true);
    }

    private void OnToggleButton()
    {
      if (this.mListRefreshing)
        return;
      this.mIsStoryList = !this.mIsStoryList;
      this.UpdateToggleButton();
      this.RefreshQuestList();
    }

    private void UpdateToggleButton()
    {
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.ListToggleButton, (UnityEngine.Object) null))
        return;
      this.ListToggleButton.sprite = !this.mIsStoryList ? this.PieceListSprite : this.StoryListSprite;
    }

    public void Activated(int pinID)
    {
      if (pinID != 10)
        return;
      this.OnToggleButton();
    }
  }
}
