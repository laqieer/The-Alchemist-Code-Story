// Decompiled with JetBrains decompiler
// Type: SRPG.UnitCharacterQuestWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  [FlowNode.Pin(10, "リスト切り替え", FlowNode.PinTypes.Input, 10)]
  [FlowNode.Pin(100, "クエストが選択された", FlowNode.PinTypes.Output, 100)]
  public class UnitCharacterQuestWindow : MonoBehaviour, IFlowInterface
  {
    public string DisableFlagName = "is_disable";
    private List<QuestParam> mQuestList = new List<QuestParam>();
    private List<GameObject> mStoryQuestListItems = new List<GameObject>();
    private List<GameObject> mPieceQuestListItems = new List<GameObject>();
    private bool mIsStoryList = true;
    public UnitData CurrentUnit;
    public Transform QuestList;
    public GameObject StoryQuestItemTemplate;
    public GameObject StoryQuestDisableItemTemplate;
    public GameObject PieceQuestItemTemplate;
    public GameObject PieceQuestDisableItemTemplate;
    public GameObject QuestDetailTemplate;
    public GameObject CharacterImage;
    private GameObject mQuestDetail;
    public string PieceQuestWorldId;
    public Image ListToggleButton;
    public Sprite StoryListSprite;
    public Sprite PieceListSprite;
    private bool mListRefreshing;
    private bool mIsRestore;

    public bool IsRestore
    {
      get
      {
        return this.mIsRestore;
      }
      set
      {
        this.mIsRestore = value;
      }
    }

    private void Start()
    {
      if ((UnityEngine.Object) this.StoryQuestItemTemplate != (UnityEngine.Object) null)
        this.StoryQuestItemTemplate.gameObject.SetActive(false);
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
        bool flag1 = this.mQuestList[i].IsDateUnlock(-1L);
        bool flag2 = Array.Find<QuestParam>(availableQuests, (Predicate<QuestParam>) (p => p == this.mQuestList[i])) != null;
        bool flag3 = this.mQuestList[i].state == QuestStates.Cleared;
        bool flag4 = charaEpisodeList[i] != null && charaEpisodeList[i].IsAvailable && this.CurrentUnit.IsChQuestParentUnlocked(this.mQuestList[i]);
        bool flag5 = flag1 && flag2 && !flag3;
        GameObject gameObject;
        if (flag4 || flag3)
        {
          gameObject = UnityEngine.Object.Instantiate<GameObject>(this.StoryQuestItemTemplate);
          Button component = gameObject.GetComponent<Button>();
          if (!((UnityEngine.Object) component == (UnityEngine.Object) null))
            component.interactable = flag5;
          else
            continue;
        }
        else
          gameObject = UnityEngine.Object.Instantiate<GameObject>(this.StoryQuestDisableItemTemplate);
        if (!((UnityEngine.Object) gameObject == (UnityEngine.Object) null))
        {
          gameObject.SetActive(true);
          gameObject.transform.SetParent(this.QuestList, false);
          DataSource.Bind<QuestParam>(gameObject, this.mQuestList[i], false);
          DataSource.Bind<UnitData>(gameObject, this.CurrentUnit, false);
          DataSource.Bind<UnitParam>(gameObject, this.CurrentUnit.UnitParam, false);
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
      if ((UnityEngine.Object) this.PieceQuestItemTemplate == (UnityEngine.Object) null)
        return;
      GameManager instance = MonoSingleton<GameManager>.Instance;
      List<QuestParam> questParamList = new List<QuestParam>();
      QuestParam[] quests = instance.Quests;
      for (int index = 0; index < quests.Length; ++index)
      {
        if (!string.IsNullOrEmpty(quests[index].world) && quests[index].world == this.PieceQuestWorldId && (!string.IsNullOrEmpty(quests[index].ChapterID) && quests[index].ChapterID == this.CurrentUnit.UnitID))
          questParamList.Add(quests[index]);
      }
      if (questParamList.Count <= 1)
        return;
      for (int index = 0; index < questParamList.Count; ++index)
      {
        GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.PieceQuestItemTemplate);
        gameObject.SetActive(true);
        gameObject.transform.SetParent(this.QuestList, false);
        DataSource.Bind<QuestParam>(gameObject, questParamList[index], false);
        DataSource.Bind<UnitData>(gameObject, this.CurrentUnit, false);
        ListItemEvents component = gameObject.GetComponent<ListItemEvents>();
        component.OnSelect = new ListItemEvents.ListItemEvent(this.OnQuestSelect);
        component.OnOpenDetail = new ListItemEvents.ListItemEvent(this.OnOpenItemDetail);
        component.OnCloseDetail = new ListItemEvents.ListItemEvent(this.OnCloseItemDetail);
        this.mPieceQuestListItems.Add(gameObject);
      }
    }

    private void RefreshQuestList()
    {
      if (this.mListRefreshing || (UnityEngine.Object) this.StoryQuestItemTemplate == (UnityEngine.Object) null || ((UnityEngine.Object) this.StoryQuestDisableItemTemplate == (UnityEngine.Object) null || (UnityEngine.Object) this.QuestList == (UnityEngine.Object) null))
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
      DataSource.Bind<UnitData>(this.CharacterImage, data, false);
      this.mListRefreshing = false;
    }

    private void OnQuestSelect(GameObject button)
    {
      List<GameObject> gameObjectList = !this.mIsStoryList ? this.mPieceQuestListItems : this.mStoryQuestListItems;
      int index = gameObjectList.IndexOf(button.gameObject);
      QuestParam quest = DataSource.FindDataOfClass<QuestParam>(gameObjectList[index], (QuestParam) null);
      if (quest == null)
        return;
      if (!quest.IsDateUnlock(-1L))
        UIUtility.NegativeSystemMessage((string) null, LocalizedText.Get("sys.DISABLE_QUEST_DATE_UNLOCK"), (UIUtility.DialogResultEvent) null, (GameObject) null, false, -1);
      else if (Array.Find<QuestParam>(MonoSingleton<GameManager>.Instance.Player.AvailableQuests, (Predicate<QuestParam>) (p => p == quest)) == null)
      {
        UIUtility.NegativeSystemMessage((string) null, LocalizedText.Get("sys.DISABLE_QUEST_CHALLENGE"), (UIUtility.DialogResultEvent) null, (GameObject) null, false, -1);
      }
      else
      {
        GlobalVars.SelectedQuestID = quest.iname;
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 100);
      }
    }

    private void OnCloseItemDetail(GameObject go)
    {
      if (!((UnityEngine.Object) this.mQuestDetail != (UnityEngine.Object) null))
        return;
      UnityEngine.Object.DestroyImmediate((UnityEngine.Object) this.mQuestDetail.gameObject);
      this.mQuestDetail = (GameObject) null;
    }

    private void OnOpenItemDetail(GameObject go)
    {
      QuestParam dataOfClass = DataSource.FindDataOfClass<QuestParam>(go, (QuestParam) null);
      if (!((UnityEngine.Object) this.mQuestDetail == (UnityEngine.Object) null) || dataOfClass == null)
        return;
      this.mQuestDetail = UnityEngine.Object.Instantiate<GameObject>(this.QuestDetailTemplate);
      DataSource.Bind<QuestParam>(this.mQuestDetail, dataOfClass, false);
      DataSource.Bind<UnitData>(this.mQuestDetail, this.CurrentUnit, false);
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
      if (!((UnityEngine.Object) this.ListToggleButton != (UnityEngine.Object) null))
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
