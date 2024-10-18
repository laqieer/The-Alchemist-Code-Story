// Decompiled with JetBrains decompiler
// Type: SRPG.QuestListV2
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
  [AddComponentMenu("UI/リスト/クエスト一覧")]
  [FlowNode.Pin(0, "通常クエストを表示", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(1, "エリートクエストを表示", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(2, "エクストラクエストを表示", FlowNode.PinTypes.Input, 2)]
  [FlowNode.Pin(10, "前回選択したクエストを表示", FlowNode.PinTypes.Input, 10)]
  [FlowNode.Pin(11, "チャプター情報更新", FlowNode.PinTypes.Input, 11)]
  [FlowNode.Pin(101, "シナリオクエストが選択された", FlowNode.PinTypes.Output, 101)]
  [FlowNode.Pin(102, "鍵クエストを開ける", FlowNode.PinTypes.Output, 102)]
  [FlowNode.Pin(103, "鍵クエストを閉じる", FlowNode.PinTypes.Output, 103)]
  [FlowNode.Pin(104, "上の階層に戻る", FlowNode.PinTypes.Output, 104)]
  [FlowNode.Pin(110, "ノーマルに切り替わった", FlowNode.PinTypes.Output, 110)]
  [FlowNode.Pin(111, "ハードに切り替わった", FlowNode.PinTypes.Output, 111)]
  [FlowNode.Pin(112, "エクストラに切り替わった", FlowNode.PinTypes.Output, 112)]
  public class QuestListV2 : SRPG_ListBase, IFlowInterface, IWebHelp
  {
    private const int PIN_IN_REFRESH = 11;
    private const int PIN_OUT_DIFF_CHANGE_NORMAL = 110;
    private const int PIN_OUT_DIFF_CHANGE_HARD = 111;
    private const int PIN_OUT_DIFF_CHANGE_EXTRA = 112;
    [Description("リストアイテムとして使用するゲームオブジェクト")]
    public GameObject ItemTemplate;
    public GameObject SpecialQuestItem;
    public GameObject ExtraQuestItem;
    [Description("シナリオクエスト用のアイテムとして使用するゲームオブジェクト")]
    public GameObject ScenarioQuestItem;
    [Description("シナリオクエスト用のアイテムとして使用するゲームオブジェクト")]
    public GameObject EliteQuestItem;
    [Description("挑戦回数を使い果たしたエリートクエストのアイテムとして使用するゲームオブジェクト")]
    public GameObject EliteQuestDisAbleItem;
    [Description("シナリオのエクストラクエスト用のアイテムとして使用するゲームオブジェクト")]
    public GameObject StoryExtraQuestItem;
    [Description("挑戦回数を使い果たしたシナリオのエクストラクエスト用のアイテムとして使用するゲームオブジェクト")]
    public GameObject StoryExtraQuestDisableItem;
    [Description("挑戦回数ありのイベントリストアイテムとして使用するゲームオブジェクト")]
    public GameObject EventTemplateLimit;
    [Description("詳細画面として使用するゲームオブジェクト")]
    public GameObject DetailTemplate;
    [Description("難易度選択ボタン (Normal)")]
    public GameObject BtnNormal;
    [Description("難易度選択ボタン (Elite)")]
    public GameObject BtnElite;
    [Description("難易度選択ボタン (Extra)")]
    public GameObject BtnExtra;
    [Description("ハードクエストのブックマーク表示用ボタン")]
    public GameObject BtnEliteBookmark;
    [Description("ユニットランキング表示ボタン")]
    public GameObject BtnUnitRanking;
    public Dictionary<string, GameObject> mListItemTemplates = new Dictionary<string, GameObject>();
    private static Dictionary<int, float> mScrollPosCache = new Dictionary<int, float>();
    private GameObject mDetailInfo;
    public bool Descending = true;
    public bool RefreshOnStart = true;
    public bool ShowAllQuests = true;
    public ScrollRect ScrollRect;
    public GameObject AreaInfo;
    [FourCC]
    public int ListID;
    public GameObject ChapterTimer;
    [SerializeField]
    private GameObject StoryExRestCount;
    public GameObject BackButton;
    [Space]
    [SerializeField]
    private GameObject ChapterCount;
    [SerializeField]
    private Text ChapterCountText;
    [SerializeField]
    private Text ChapterCountLimit;
    private ChapterParam mCurrentChapter;
    private List<QuestParam> mQuests = new List<QuestParam>();
    private QuestDifficulties mDifficultyFilter;
    private int mSetScrollPos;
    private float mNewScrollPos;
    private bool mIsQuestsRefreshed;
    private GameObject mExtraSystemMsg;

    public void Activated(int pinID)
    {
      bool flag = false;
      switch (pinID)
      {
        case 0:
          flag = this.Refresh(QuestDifficulties.Normal);
          break;
        case 1:
          flag = this.Refresh(QuestDifficulties.Elite);
          break;
        case 2:
          flag = this.Refresh(QuestDifficulties.Extra);
          break;
        case 10:
          flag = this.Refresh(this.PlayingDifficultiy());
          break;
        case 11:
          this.CheckChapterState();
          return;
      }
      if (flag)
        return;
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 104);
    }

    protected override void Start()
    {
      base.Start();
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.ItemTemplate, (UnityEngine.Object) null) && this.ItemTemplate.gameObject.activeInHierarchy)
        this.ItemTemplate.SetActive(false);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.SpecialQuestItem, (UnityEngine.Object) null) && this.SpecialQuestItem.gameObject.activeInHierarchy)
        this.SpecialQuestItem.SetActive(false);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.ExtraQuestItem, (UnityEngine.Object) null) && this.ExtraQuestItem.gameObject.activeInHierarchy)
        this.ExtraQuestItem.SetActive(false);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.DetailTemplate, (UnityEngine.Object) null) && this.DetailTemplate.gameObject.activeInHierarchy)
        this.DetailTemplate.SetActive(false);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.ScenarioQuestItem, (UnityEngine.Object) null) && this.ScenarioQuestItem.gameObject.activeInHierarchy)
        this.ScenarioQuestItem.SetActive(false);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.EliteQuestItem, (UnityEngine.Object) null) && this.EliteQuestItem.gameObject.activeInHierarchy)
        this.EliteQuestItem.SetActive(false);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.EliteQuestDisAbleItem, (UnityEngine.Object) null) && this.EliteQuestDisAbleItem.gameObject.activeInHierarchy)
        this.EliteQuestDisAbleItem.SetActive(false);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.StoryExtraQuestItem, (UnityEngine.Object) null) && this.StoryExtraQuestItem.gameObject.activeInHierarchy)
        this.StoryExtraQuestItem.SetActive(false);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.StoryExtraQuestDisableItem, (UnityEngine.Object) null) && this.StoryExtraQuestDisableItem.gameObject.activeInHierarchy)
        this.StoryExtraQuestDisableItem.SetActive(false);
      if (this.RefreshOnStart && !this.mIsQuestsRefreshed)
      {
        this.RefreshQuests();
        this.RefreshItems();
        ChapterParam mCurrentChapter = this.mCurrentChapter;
        if (mCurrentChapter != null && mCurrentChapter.IsKeyQuest() && !mCurrentChapter.IsKeyUnlock(Network.GetServerTime()))
        {
          if (mCurrentChapter.CheckHasKeyItem())
          {
            GlobalVars.KeyQuestTimeOver = true;
            FlowNode_GameObject.ActivateOutputLinks((Component) this, 102);
          }
          else
            UIUtility.SystemMessage(LocalizedText.Get("sys.KEYQUEST_UNLOCK"), LocalizedText.Get("sys.KEYQUEST_AVAILABLE_CAUTION"), new UIUtility.DialogResultEvent(this.OnCloseKeyQuest), systemModal: true);
        }
      }
      MonoSingleton<GameManager>.Instance.OnPlayerLvChange += new GameManager.PlayerLvChangeEvent(this.RefreshItems);
    }

    private GameObject LoadQuestListItem(QuestParam param)
    {
      if (string.IsNullOrEmpty(param.ItemLayout))
        return (GameObject) null;
      if (this.mListItemTemplates.ContainsKey(param.ItemLayout))
        return this.mListItemTemplates[param.ItemLayout];
      GameObject gameObject = AssetManager.Load<GameObject>("QuestListItems/" + param.ItemLayout);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) gameObject, (UnityEngine.Object) null))
        this.mListItemTemplates.Add(param.ItemLayout, gameObject);
      return gameObject;
    }

    protected override void OnDestroy()
    {
      base.OnDestroy();
      GameManager instanceDirect = MonoSingleton<GameManager>.GetInstanceDirect();
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) instanceDirect, (UnityEngine.Object) null))
        return;
      instanceDirect.OnPlayerLvChange -= new GameManager.PlayerLvChangeEvent(this.RefreshItems);
    }

    public bool ExistsQuest(QuestDifficulties difficulty)
    {
      return this.mQuests.Any<QuestParam>((Func<QuestParam, bool>) (q => q.difficulty == difficulty));
    }

    private bool Refresh(QuestDifficulties difficulty)
    {
      if (GlobalVars.RankingQuestSelected)
      {
        GlobalVars.RankingQuestSelected = false;
        this.mIsQuestsRefreshed = true;
        if (!this.RefreshRankingQuests())
          return false;
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.BackButton, (UnityEngine.Object) null))
          this.BackButton.SetActive(false);
      }
      else
      {
        if (!this.RefreshQuests())
          return false;
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.BackButton, (UnityEngine.Object) null))
          this.BackButton.SetActive(true);
      }
      if (difficulty == QuestDifficulties.Normal && !this.ExistsQuest(QuestDifficulties.Normal))
        difficulty = QuestDifficulties.Elite;
      if (difficulty == QuestDifficulties.Elite && !this.ExistsQuest(QuestDifficulties.Elite))
        difficulty = QuestDifficulties.Extra;
      if (difficulty == QuestDifficulties.Extra && !this.ExistsQuest(QuestDifficulties.Extra))
        difficulty = QuestDifficulties.Normal;
      this.mDifficultyFilter = difficulty;
      this.RefreshItems();
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.ScrollRect, (UnityEngine.Object) null))
      {
        this.ScrollRect.normalizedPosition = Vector2.one;
        if (HomeWindow.GetRestorePoint() == RestorePoints.QuestList && !string.IsNullOrEmpty(GlobalVars.LastPlayedQuest.Get()) && (QuestStates) GlobalVars.LastQuestState == QuestStates.Cleared && QuestListV2.mScrollPosCache.ContainsKey(this.ListID))
        {
          this.mSetScrollPos = 2;
          this.mNewScrollPos = QuestListV2.mScrollPosCache[this.ListID];
          QuestListV2.mScrollPosCache.Remove(this.ListID);
          HomeWindow.SetRestorePoint(RestorePoints.Home);
        }
      }
      this.RefreshButtons(difficulty);
      switch (difficulty)
      {
        case QuestDifficulties.Normal:
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.BtnNormal, (UnityEngine.Object) null) && !this.ExistsQuest(QuestDifficulties.Elite) && !this.ExistsQuest(QuestDifficulties.Extra))
          {
            this.BtnNormal.SetActive(false);
            break;
          }
          break;
        case QuestDifficulties.Elite:
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.BtnElite, (UnityEngine.Object) null) && !this.ExistsQuest(QuestDifficulties.Extra) && !this.ExistsQuest(QuestDifficulties.Normal))
          {
            this.BtnElite.SetActive(false);
            break;
          }
          break;
        default:
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.BtnExtra, (UnityEngine.Object) null) && !this.ExistsQuest(QuestDifficulties.Normal) && !this.ExistsQuest(QuestDifficulties.Elite))
          {
            this.BtnExtra.SetActive(false);
            break;
          }
          break;
      }
      GameUtility.SetGameObjectActive(this.StoryExRestCount, difficulty == QuestDifficulties.Extra);
      switch (this.mDifficultyFilter)
      {
        case QuestDifficulties.Normal:
          FlowNode_GameObject.ActivateOutputLinks((Component) this, 110);
          break;
        case QuestDifficulties.Elite:
          FlowNode_GameObject.ActivateOutputLinks((Component) this, 111);
          break;
        case QuestDifficulties.Extra:
          FlowNode_GameObject.ActivateOutputLinks((Component) this, 112);
          break;
      }
      return true;
    }

    private bool HasEliteQuest(QuestParam q)
    {
      QuestParam[] quests = MonoSingleton<GameManager>.Instance.Quests;
      for (int index = 0; index < quests.Length; ++index)
      {
        if (quests[index].difficulty == QuestDifficulties.Elite && quests[index].cond_quests != null && Array.IndexOf<string>(quests[index].cond_quests, q.iname) >= 0)
          return true;
      }
      return false;
    }

    private bool RefreshQuests()
    {
      this.mCurrentChapter = MonoSingleton<GameManager>.Instance.FindArea((string) GlobalVars.SelectedChapter);
      this.mQuests.Clear();
      QuestParam[] availableQuests = MonoSingleton<GameManager>.Instance.Player.AvailableQuests;
      for (int index = 0; index < availableQuests.Length; ++index)
      {
        QuestParam questParam = availableQuests[index];
        if (!availableQuests[index].IsMulti && (this.ShowAllQuests || this.mCurrentChapter != null && !(this.mCurrentChapter.iname != questParam.ChapterID)))
        {
          if (this.mCurrentChapter != null && this.mCurrentChapter.IsGpsQuest())
          {
            if (!questParam.gps_enable || questParam.type != QuestTypes.Gps)
              continue;
          }
          else if (questParam.type == QuestTypes.Gps)
            continue;
          if (questParam.Chapter != null && questParam.Chapter.IsArchiveQuest)
          {
            if (questParam.hidden || !MonoSingleton<GameManager>.Instance.Player.IsQuestArchiveOpenByArea(questParam.ChapterID))
              continue;
          }
          else if (!questParam.IsDateUnlock())
            continue;
          this.mQuests.Add(questParam);
        }
      }
      this.RefreshChapterTimer();
      if (this.mCurrentChapter != null && this.mCurrentChapter.IsKeyQuest() && this.mCurrentChapter.quests != null && this.mCurrentChapter.quests.Count > 1)
        DebugUtility.LogError("キークエストの設定に問題があります（キークエストを設定する際は1エリアに対して1クエストで設定してください）");
      return this.mQuests.Count > 0;
    }

    private bool RefreshRankingQuests()
    {
      List<RankingQuestParam> rankingQuesstParams = MonoSingleton<GameManager>.Instance.AvailableRankingQuesstParams;
      List<QuestParam> questParamList = new List<QuestParam>();
      for (int index = 0; index < rankingQuesstParams.Count; ++index)
      {
        QuestParam quest = MonoSingleton<GameManager>.Instance.FindQuest(rankingQuesstParams[index].iname);
        if (quest != null && questParamList.Find((Predicate<QuestParam>) (q => q.iname == quest.iname)) == null)
          questParamList.Add(quest);
      }
      this.mQuests.Clear();
      for (int index = 0; index < questParamList.Count; ++index)
      {
        QuestParam questParam = questParamList[index];
        if (!questParamList[index].IsMulti && questParam.type != QuestTypes.Gps && questParam.IsDateUnlock() && questParam.IsQuestCondition())
          this.mQuests.Add(questParam);
      }
      this.RefreshChapterTimer();
      return this.mQuests.Count > 0;
    }

    private void RefreshItems()
    {
      Transform transform = ((Component) this).transform;
      this.ClearItems();
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.ItemTemplate, (UnityEngine.Object) null))
        return;
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.AreaInfo, (UnityEngine.Object) null) && !string.IsNullOrEmpty((string) GlobalVars.SelectedSection))
        DataSource.Bind<ChapterParam>(this.AreaInfo, MonoSingleton<GameManager>.Instance.FindArea((string) GlobalVars.SelectedSection));
      QuestParam[] array = this.mQuests.ToArray();
      if (this.Descending)
        Array.Reverse((Array) array);
      for (int index = 0; index < array.Length; ++index)
      {
        QuestParam questParam = array[index];
        if (questParam.difficulty == this.mDifficultyFilter)
        {
          GameObject gameObject1 = (GameObject) null;
          if (!string.IsNullOrEmpty(questParam.ItemLayout))
            gameObject1 = this.LoadQuestListItem(questParam);
          if (UnityEngine.Object.op_Equality((UnityEngine.Object) gameObject1, (UnityEngine.Object) null))
            gameObject1 = questParam.difficulty != QuestDifficulties.Elite ? (questParam.difficulty != QuestDifficulties.Extra ? (!questParam.IsScenario ? (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.SpecialQuestItem, (UnityEngine.Object) null) || !this.HasEliteQuest(questParam) ? (questParam.GetChallangeLimit() <= 0 ? (!questParam.IsExtra ? this.ItemTemplate : this.ExtraQuestItem) : this.EventTemplateLimit) : this.SpecialQuestItem) : this.ScenarioQuestItem) : (!questParam.CheckEnableChallange() ? this.StoryExtraQuestDisableItem : this.StoryExtraQuestItem)) : (!questParam.CheckEnableChallange() ? this.EliteQuestDisAbleItem : this.EliteQuestItem);
          if (!UnityEngine.Object.op_Equality((UnityEngine.Object) gameObject1, (UnityEngine.Object) null))
          {
            GameObject gameObject2 = UnityEngine.Object.Instantiate<GameObject>(gameObject1);
            ((UnityEngine.Object) gameObject2).hideFlags = (HideFlags) 52;
            DataSource.Bind<QuestParam>(gameObject2, questParam);
            RankingQuestParam availableRankingQuest = MonoSingleton<GameManager>.Instance.FindAvailableRankingQuest(questParam.iname);
            DataSource.Bind<RankingQuestParam>(gameObject2, availableRankingQuest);
            DataSource.Bind<QuestParam>(gameObject2, questParam);
            QuestCampaignData[] questCampaigns = MonoSingleton<GameManager>.Instance.FindQuestCampaigns(questParam);
            DataSource.Bind<QuestCampaignData[]>(gameObject2, questCampaigns.Length != 0 ? questCampaigns : (QuestCampaignData[]) null);
            ListItemEvents component = gameObject2.GetComponent<ListItemEvents>();
            component.OnSelect = new ListItemEvents.ListItemEvent(this.OnSelectItem);
            component.OnOpenDetail = new ListItemEvents.ListItemEvent(this.OnOpenItemDetail);
            component.OnCloseDetail = new ListItemEvents.ListItemEvent(this.OnCloseItemDetail);
            gameObject2.transform.SetParent(transform, false);
            gameObject2.gameObject.SetActive(true);
            this.AddItem(component);
          }
        }
      }
    }

    private void RefreshChapterTimer()
    {
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.ChapterTimer, (UnityEngine.Object) null))
        return;
      bool flag = false;
      if (this.mCurrentChapter != null)
      {
        ChapterParam mCurrentChapter = this.mCurrentChapter;
        if (mCurrentChapter != null)
        {
          DataSource.Bind<ChapterParam>(this.ChapterTimer, mCurrentChapter);
          switch (mCurrentChapter.GetKeyQuestType())
          {
            case KeyQuestTypes.Timer:
              flag = mCurrentChapter.key_end > 0L;
              break;
            case KeyQuestTypes.Count:
              flag = false;
              break;
            default:
              flag = mCurrentChapter.end > 0L;
              break;
          }
        }
      }
      this.ChapterTimer.SetActive(flag);
      GameParameter.UpdateAll(this.ChapterTimer);
    }

    private void RefreshButtons(QuestDifficulties difficulty)
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) null, (UnityEngine.Object) this.BtnNormal) || UnityEngine.Object.op_Equality((UnityEngine.Object) null, (UnityEngine.Object) this.BtnElite) || UnityEngine.Object.op_Equality((UnityEngine.Object) null, (UnityEngine.Object) this.BtnExtra))
        return;
      switch (difficulty)
      {
        case QuestDifficulties.Normal:
          this.BtnNormal.SetActive(true);
          this.BtnUnitRanking.SetActive(true);
          this.BtnEliteBookmark.SetActive(true);
          this.BtnElite.SetActive(false);
          this.BtnExtra.SetActive(false);
          break;
        case QuestDifficulties.Elite:
          this.BtnUnitRanking.SetActive(true);
          this.BtnEliteBookmark.SetActive(true);
          this.BtnNormal.SetActive(false);
          this.BtnElite.SetActive(true);
          this.BtnExtra.SetActive(false);
          break;
        case QuestDifficulties.Extra:
          this.BtnUnitRanking.SetActive(true);
          this.BtnEliteBookmark.SetActive(true);
          this.BtnNormal.SetActive(false);
          this.BtnElite.SetActive(false);
          this.BtnExtra.SetActive(true);
          break;
      }
    }

    private void ChangeChapterCountState(ChapterParam chapterParam)
    {
      if (chapterParam == null)
      {
        if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.ChapterCount, (UnityEngine.Object) null))
          return;
        this.ChapterCount.SetActive(false);
      }
      else
      {
        ChapterParam chapter;
        chapterParam.CheckEnableChallange(out chapter);
        if (chapter == null)
        {
          if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.ChapterCount, (UnityEngine.Object) null))
            return;
          this.ChapterCount.SetActive(false);
        }
        else
        {
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.ChapterCount, (UnityEngine.Object) null))
            this.ChapterCount.SetActive(chapter.ChallengeLimitCount() > 0);
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.ChapterCountText, (UnityEngine.Object) null))
            this.ChapterCountText.text = chapter.challengeCount.ToString();
          if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.ChapterCountLimit, (UnityEngine.Object) null))
            return;
          this.ChapterCountLimit.text = chapter.ChallengeLimitCount().ToString();
        }
      }
    }

    private void CheckChapterState()
    {
      ChapterParam area = MonoSingleton<GameManager>.Instance.FindArea((string) GlobalVars.SelectedChapter);
      if (area == null)
        return;
      ChapterParam chapter;
      if (area.CheckEnableChallange(out chapter))
        this.ChangeChapterCountState(chapter);
      else
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 104);
    }

    private void OnSelectItem(GameObject go)
    {
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.ScrollRect, (UnityEngine.Object) null) && this.ListID != 0)
        QuestListV2.mScrollPosCache[this.ListID] = this.ScrollRect.verticalNormalizedPosition;
      GlobalVars.SelectedRankingQuestParam = DataSource.FindDataOfClass<RankingQuestParam>(go, (RankingQuestParam) null);
      QuestParam dataOfClass = DataSource.FindDataOfClass<QuestParam>(go, (QuestParam) null);
      if (dataOfClass == null)
        return;
      if (dataOfClass.IsKeyQuest && !dataOfClass.IsKeyUnlock())
      {
        if (dataOfClass.Chapter != null && dataOfClass.Chapter.CheckHasKeyItem() && dataOfClass.IsDateUnlock())
        {
          GlobalVars.KeyQuestTimeOver = true;
          FlowNode_GameObject.ActivateOutputLinks((Component) this, 102);
        }
        else
          UIUtility.SystemMessage(LocalizedText.Get("sys.KEYQUEST_UNLOCK"), LocalizedText.Get("sys.KEYQUEST_AVAILABLE_CAUTION"), new UIUtility.DialogResultEvent(this.OnCloseKeyQuest), systemModal: true);
      }
      else if (dataOfClass.type == QuestTypes.StoryExtra && !dataOfClass.IsOpenUnitHave())
      {
        UnitParam unitParam = MonoSingleton<GameManager>.Instance.MasterParam.GetUnitParam(dataOfClass.OpenUnit);
        if (unitParam == null)
        {
          DebugUtility.Log(string.Format("<color=yellow>QuestParam/Quest OpenUnit No Data!</color>"));
        }
        else
        {
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mExtraSystemMsg, (UnityEngine.Object) null))
            return;
          this.mExtraSystemMsg = UIUtility.SystemMessage(LocalizedText.Get("sys.EXTRA_NOT_OPEN_MSG", (object) unitParam.name), (UIUtility.DialogResultEvent) null, systemModal: true);
        }
      }
      else
      {
        GlobalVars.SelectedQuestID = dataOfClass.iname;
        GlobalVars.LastQuestState.Set(dataOfClass.state);
        if (dataOfClass.IsScenario)
        {
          FlowNode_GameObject.ActivateOutputLinks((Component) this, 101);
        }
        else
        {
          if ((int) dataOfClass.aplv > MonoSingleton<GameManager>.Instance.Player.Lv)
            NotifyList.Push(LocalizedText.Get("sys.QUEST_AP_CONDITION", (object) dataOfClass.aplv));
          FlowNode_OnQuestSelect objectOfType = UnityEngine.Object.FindObjectOfType<FlowNode_OnQuestSelect>();
          if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) objectOfType, (UnityEngine.Object) null))
            return;
          objectOfType.Selected();
        }
      }
    }

    private void OnCloseKeyQuest(GameObject go)
    {
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 103);
    }

    private void OnCloseItemDetail(GameObject go)
    {
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mDetailInfo, (UnityEngine.Object) null))
        return;
      UnityEngine.Object.DestroyImmediate((UnityEngine.Object) this.mDetailInfo.gameObject);
      this.mDetailInfo = (GameObject) null;
    }

    private void OnOpenItemDetail(GameObject go)
    {
      QuestParam dataOfClass = DataSource.FindDataOfClass<QuestParam>(go, (QuestParam) null);
      if (!UnityEngine.Object.op_Equality((UnityEngine.Object) this.mDetailInfo, (UnityEngine.Object) null) || dataOfClass == null)
        return;
      this.mDetailInfo = UnityEngine.Object.Instantiate<GameObject>(this.DetailTemplate);
      DataSource.Bind<QuestParam>(this.mDetailInfo, dataOfClass);
      QuestCampaignData[] questCampaigns = MonoSingleton<GameManager>.Instance.FindQuestCampaigns(dataOfClass);
      DataSource.Bind<QuestCampaignData[]>(this.mDetailInfo, questCampaigns.Length != 0 ? questCampaigns : (QuestCampaignData[]) null);
      this.mDetailInfo.SetActive(true);
    }

    private QuestDifficulties PlayingDifficultiy()
    {
      QuestParam quest = MonoSingleton<GameManager>.Instance.FindQuest(GlobalVars.SelectedQuestID);
      return quest != null ? quest.difficulty : QuestDifficulties.Normal;
    }

    protected override void LateUpdate()
    {
      if (this.mSetScrollPos > 0)
      {
        --this.mSetScrollPos;
        if (this.mSetScrollPos == 0)
          this.ScrollRect.verticalNormalizedPosition = this.mNewScrollPos;
      }
      base.LateUpdate();
    }

    public bool GetHelpURL(out string url, out string title)
    {
      if (this.mCurrentChapter != null && !string.IsNullOrEmpty(this.mCurrentChapter.helpURL))
      {
        title = this.mCurrentChapter.name;
        url = this.mCurrentChapter.helpURL;
        return true;
      }
      title = (string) null;
      url = (string) null;
      return false;
    }
  }
}
