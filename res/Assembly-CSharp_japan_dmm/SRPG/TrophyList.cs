// Decompiled with JetBrains decompiler
// Type: SRPG.TrophyList
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
  public class TrophyList : SRPG_ListBase, IFlowInterface
  {
    public TrophyWindow trophy_window;
    public TrophyList.TrophyTypes TrophyType;
    public TrophyCategorys TrophyCategory;
    public TrophyRecordPullView OriginalPullView;
    public ListItemEvents Item_Normal;
    public ListItemEvents Item_Completed;
    public ListItemEvents Item_Ended;
    public GameObject DetailWindow;
    public ListItemEvents Item_Review;
    public ListItemEvents Item_FollowTwitter;
    public RectTransform parent_panel_rect;
    [SerializeField]
    private LayoutElement view_port_handle;
    private RectTransform view_port_handle_rect;
    private RectTransform scroll_trans_rect;
    private const int CREATE_ENDED_PLATE_COUNT = 50;
    private const int CREATE_PULL_VIEW_COUNT_MAX = 50;
    private const int CREATE_CHILD_COUNT = 20;
    public bool RefreshOnStart;
    private bool mStarted;
    private CanvasGroup mCanvasGroup;
    private List<TrophyRecordPullView> child_pull_viewes = new List<TrophyRecordPullView>();
    private List<TrophyParam> child_plates = new List<TrophyParam>();
    private bool is_busy;
    private float FOCUS_EFFECT_SECOND = 0.2f;
    private Vector2 start_pos;
    private Vector2 target_pos;
    private List<TrophyRecordPullView> click_target = new List<TrophyRecordPullView>();
    [SerializeField]
    private ScrollRect scroll_rect;
    private float total_close_item_size;
    private float focus_delay_time;
    private float elapsed_time;
    private TrophyList.eState state;
    private List<TrophyRecordTab> mTabList = new List<TrophyRecordTab>();

    public TrophyWindow Trophy_Window
    {
      get
      {
        if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.trophy_window, (UnityEngine.Object) null))
          this.trophy_window = ((Component) this).GetComponentInParent<TrophyWindow>();
        return this.trophy_window;
      }
    }

    private RectTransform ScrollTransRect
    {
      get
      {
        if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.scroll_trans_rect, (UnityEngine.Object) null))
          this.scroll_trans_rect = this.GetRectTransform();
        return this.scroll_trans_rect;
      }
    }

    public virtual List<TrophyState> TrophyDailyData
    {
      get
      {
        return UnityEngine.Object.op_Equality((UnityEngine.Object) this.Trophy_Window, (UnityEngine.Object) null) ? (List<TrophyState>) null : this.Trophy_Window.TrophyDailyData;
      }
    }

    public virtual List<TrophyState> TrophyEndedData
    {
      get
      {
        return UnityEngine.Object.op_Equality((UnityEngine.Object) this.Trophy_Window, (UnityEngine.Object) null) ? (List<TrophyState>) null : this.Trophy_Window.TrophyEndedData;
      }
    }

    public void Activated(int pinID)
    {
    }

    private void Initialize()
    {
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.Item_Normal, (UnityEngine.Object) null) && ((Component) this.Item_Normal).gameObject.activeInHierarchy)
        ((Component) this.Item_Normal).gameObject.SetActive(false);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.Item_Review, (UnityEngine.Object) null) && ((Component) this.Item_Review).gameObject.activeInHierarchy)
        ((Component) this.Item_Review).gameObject.SetActive(false);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.Item_FollowTwitter, (UnityEngine.Object) null) && ((Component) this.Item_FollowTwitter).gameObject.activeInHierarchy)
        ((Component) this.Item_FollowTwitter).gameObject.SetActive(false);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.Item_Completed, (UnityEngine.Object) null) && ((Component) this.Item_Completed).gameObject.activeInHierarchy)
        ((Component) this.Item_Completed).gameObject.SetActive(false);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.Item_Ended, (UnityEngine.Object) null) && ((Component) this.Item_Ended).gameObject.activeInHierarchy)
        ((Component) this.Item_Ended).gameObject.SetActive(false);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.DetailWindow, (UnityEngine.Object) null) && this.DetailWindow.activeInHierarchy)
        this.DetailWindow.SetActive(false);
      this.mCanvasGroup = ((Component) this).GetComponent<CanvasGroup>();
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.mCanvasGroup, (UnityEngine.Object) null))
        this.mCanvasGroup = ((Component) this).gameObject.AddComponent<CanvasGroup>();
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.view_port_handle, (UnityEngine.Object) null))
        return;
      this.view_port_handle_rect = ((Component) this.view_port_handle).GetComponent<RectTransform>();
    }

    protected override void OnDestroy()
    {
      base.OnDestroy();
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) HomeWindow.Current, (UnityEngine.Object) null))
        return;
      HomeWindow.Current.UnlockContents();
    }

    protected override void Start()
    {
      this.Initialize();
      base.Start();
      this.mStarted = true;
      if (!this.RefreshOnStart)
        return;
      this.Refresh();
    }

    private void OnTrophyReset() => this.Refresh();

    private void OnEnable()
    {
      this.ScrollTransRect.anchoredPosition = Vector2.zero;
      this.Refresh();
      MonoSingleton<GameManager>.Instance.OnDayChange += new GameManager.DayChangeEvent(this.OnTrophyReset);
    }

    private void OnDisable()
    {
      this.ClearAllItems();
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) MonoSingleton<GameManager>.GetInstanceDirect(), (UnityEngine.Object) null))
        return;
      MonoSingleton<GameManager>.Instance.OnDayChange -= new GameManager.DayChangeEvent(this.OnTrophyReset);
    }

    private void Update()
    {
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mCanvasGroup, (UnityEngine.Object) null) || (double) this.mCanvasGroup.alpha >= 1.0)
        return;
      this.mCanvasGroup.alpha = Mathf.Clamp01(this.mCanvasGroup.alpha + Time.unscaledDeltaTime * 3.33333325f);
    }

    protected override void LateUpdate()
    {
      base.LateUpdate();
      this.Run();
    }

    public void CreateTrophies(List<TrophyState> trophies)
    {
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mCanvasGroup, (UnityEngine.Object) null))
        this.mCanvasGroup.alpha = 0.0f;
      this.ClearAllItems();
      int num = 20;
      for (int index = 0; index < trophies.Count && num != 0; ++index)
      {
        ListItemEvents listItemEvents = this.MakeTrophyPlate(trophies[index], trophies[index].IsCompleted);
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) listItemEvents, (UnityEngine.Object) null))
        {
          --num;
          this.AddItem(listItemEvents);
        }
      }
      this.scroll_rect.normalizedPosition = new Vector2(1f, 1f);
    }

    public void ClearAllItems()
    {
      for (int index = 0; index < this.child_pull_viewes.Count; ++index)
        this.child_pull_viewes[index].ClearItems();
      for (int index = 0; index < this.mTabList.Count; ++index)
        UnityEngine.Object.Destroy((UnityEngine.Object) this.mTabList[index]);
      this.ClearItems();
    }

    private void RefreshTrophyRecordLight()
    {
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mCanvasGroup, (UnityEngine.Object) null))
        this.mCanvasGroup.alpha = 0.0f;
      if (!this.Trophy_Window.TrophyRecordData.ContainsKey(this.TrophyCategory))
      {
        this.ClearAllItems();
      }
      else
      {
        List<TrophyCategoryData> trophyCategoryDataList = this.Trophy_Window.TrophyRecordData[this.TrophyCategory];
        if (trophyCategoryDataList == null || trophyCategoryDataList.Count <= 0)
        {
          this.ClearAllItems();
        }
        else
        {
          int count1 = this.child_pull_viewes.Count;
          for (int index1 = 0; index1 < trophyCategoryDataList.Count; ++index1)
          {
            if (!trophyCategoryDataList[index1].Param.IsNotPull)
            {
              for (int index2 = 0; index2 < this.child_pull_viewes.Count; ++index2)
              {
                if (trophyCategoryDataList[index1].Param.hash_code == this.child_pull_viewes[index2].HashCode)
                {
                  this.child_pull_viewes[index2].SetCategoryData(trophyCategoryDataList[index1]);
                  --count1;
                }
              }
            }
          }
          if (count1 != 0)
          {
            this.Refresh();
          }
          else
          {
            int count2 = this.child_plates.Count;
            for (int index3 = 0; index3 < trophyCategoryDataList.Count; ++index3)
            {
              if (trophyCategoryDataList[index3].Param.IsNotPull)
              {
                for (int index4 = 0; index4 < trophyCategoryDataList[index3].Trophies.Count; ++index4)
                {
                  for (int index5 = 0; index5 < this.child_plates.Count; ++index5)
                  {
                    if (!(trophyCategoryDataList[index3].Trophies[index4].Param.iname != this.child_plates[index5].iname))
                      --count2;
                  }
                }
              }
            }
            if (count2 != 0)
            {
              this.Refresh();
            }
            else
            {
              for (int index = 0; index < this.child_pull_viewes.Count; ++index)
                this.child_pull_viewes[index].Refresh(this.ScrollTransRect);
            }
          }
        }
      }
    }

    public void RefreshLight()
    {
      if (this.TrophyType == TrophyList.TrophyTypes.Normal)
        return;
      this.Refresh();
    }

    private void Refresh()
    {
      if (!this.mStarted || this.TrophyType == TrophyList.TrophyTypes.Normal)
        return;
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mCanvasGroup, (UnityEngine.Object) null))
        this.mCanvasGroup.alpha = 0.0f;
      this.ClearAllItems();
      switch (this.TrophyType)
      {
        case TrophyList.TrophyTypes.Daily:
          this.RefreshTrophySimple(this.TrophyDailyData, -1, false, true);
          break;
        case TrophyList.TrophyTypes.All:
          this.RefreshTrophySimple(this.TrophyEndedData, 50, true, false);
          break;
      }
      this.ScrollTransRect.anchoredPosition = Vector2.zero;
    }

    public void RefreshTrophySimple(
      List<TrophyState> _trophies,
      int _create_count,
      bool _need_sort,
      bool _daily_comp_check)
    {
      if (_trophies == null || _trophies.Count <= 0)
        return;
      int num = _create_count;
      int[] numArray = (int[]) null;
      if (_need_sort)
        numArray = this.GetSortedIndex(_trophies);
      if (_daily_comp_check)
        MonoSingleton<GameManager>.Instance.Player.TrophyData.DailyAllCompleteCheck();
      this.child_plates.Clear();
      for (int index1 = 0; index1 < _trophies.Count && num != 0; ++index1)
      {
        int index2 = !_need_sort ? index1 : numArray[index1];
        ListItemEvents listItemEvents = this.MakeTrophyPlate(_trophies[index2], _trophies[index2].IsCompleted);
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) listItemEvents, (UnityEngine.Object) null))
        {
          this.child_plates.Add(DataSource.FindDataOfClass<TrophyParam>(((Component) listItemEvents).gameObject, (TrophyParam) null));
          this.AddItem(listItemEvents);
          --num;
        }
      }
    }

    private int[] GetSortedIndex(List<TrophyState> _trophies)
    {
      int[] sortedIndex = new int[_trophies.Count];
      for (int index = 0; index < _trophies.Count; ++index)
        sortedIndex[index] = index;
      ulong[] numArray = new ulong[_trophies.Count];
      for (int index = 0; index < _trophies.Count; ++index)
      {
        DateTime dateTime = _trophies[index] == null ? DateTime.MinValue : _trophies[index].RewardedAt;
        numArray[index] = (ulong) ((long) ((ulong) dateTime.Year % 100UL) * 10000000000L + (long) ((ulong) dateTime.Month % 100UL) * 100000000L + (long) ((ulong) dateTime.Day % 100UL) * 1000000L + (long) ((ulong) dateTime.Hour % 100UL) * 10000L + (long) ((ulong) dateTime.Minute % 100UL) * 100L) + (ulong) dateTime.Second % 100UL;
      }
      Dictionary<int, ulong> dictionary = new Dictionary<int, ulong>();
      dictionary.Keys.CopyTo(sortedIndex, 0);
      dictionary.Values.CopyTo(numArray, 0);
      Array.Sort<ulong, int>(numArray, sortedIndex);
      Array.Reverse((Array) sortedIndex);
      return sortedIndex;
    }

    public ListItemEvents MakeTrophyPlate(TrophyState st, bool is_achievement)
    {
      ListItemEvents listItemEvents1 = !st.IsEnded ? (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.Item_FollowTwitter, (UnityEngine.Object) null) || !st.Param.ContainsCondition(TrophyConditionTypes.followtwitter) ? (!st.IsCompleted ? this.Item_Normal : this.Item_Completed) : this.Item_FollowTwitter) : this.Item_Ended;
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) listItemEvents1, (UnityEngine.Object) null))
        return (ListItemEvents) null;
      ListItemEvents listItemEvents2 = UnityEngine.Object.Instantiate<ListItemEvents>(listItemEvents1);
      DataSource.Bind<TrophyParam>(((Component) listItemEvents2).gameObject, st.Param);
      ((Component) listItemEvents2).transform.SetParent(((Component) this).transform, false);
      listItemEvents2.OnOpenDetail = new ListItemEvents.ListItemEvent(this.OnItemDetail);
      listItemEvents2.OnSelect = new ListItemEvents.ListItemEvent(this.OnItemSelect);
      SRPG_Button componentInChildren = ((Component) listItemEvents2).GetComponentInChildren<SRPG_Button>();
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) componentInChildren, (UnityEngine.Object) null) && !st.IsEnded)
      {
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.Item_FollowTwitter, (UnityEngine.Object) null) && st.Param.ContainsCondition(TrophyConditionTypes.followtwitter))
          this.FollowBtnSetting(componentInChildren);
        else if (st.IsCompleted)
          this.AchievementBtnSetting(componentInChildren);
        else
          this.ChallengeBtnSetting(componentInChildren, st.Param);
      }
      RewardData data = new RewardData(st.Param);
      DataSource.Bind<RewardData>(((Component) listItemEvents2).gameObject, data);
      ((Component) listItemEvents2).gameObject.SetActive(true);
      return listItemEvents2;
    }

    public virtual void OnItemSelect(GameObject go)
    {
      TrophyParam dataOfClass = DataSource.FindDataOfClass<TrophyParam>(go, (TrophyParam) null);
      if (dataOfClass == null)
        return;
      TrophyState trophyCounter = MonoSingleton<GameManager>.Instance.Player.TrophyData.GetTrophyCounter(dataOfClass, true);
      if (!trophyCounter.IsEnded && trophyCounter.IsCompleted)
        this.GotoReward(dataOfClass);
      else
        this.SelectGotoShortCut(dataOfClass);
    }

    protected void GotoReward(TrophyParam param)
    {
      if (param.IsInvisibleStamina() || !param.IsAvailablePeriod(TimeManager.ServerTime, true))
      {
        UIUtility.NegativeSystemMessage((string) null, LocalizedText.Get("sys.TROPHY_OUTDATED"), (UIUtility.DialogResultEvent) null);
        this.Refresh();
      }
      else
      {
        GlobalVars.SelectedTrophy.Set(param.iname);
        RewardData rewardData = new RewardData(param);
        GlobalVars.LastReward.Set(rewardData);
        GlobalVars.UnitGetReward = new UnitGetParam(rewardData.Items.ToArray());
        this.trophy_window.ActivateOutputLinks(2000);
      }
    }

    protected virtual void SelectGotoShortCut(TrophyParam param)
    {
      PlayerData player = MonoSingleton<GameManager>.Instance.Player;
      switch (param.Objectives[0].type)
      {
        case TrophyConditionTypes.winquest:
        case TrophyConditionTypes.playerlv:
        case TrophyConditionTypes.winquestsoldier:
        case TrophyConditionTypes.losequest:
        case TrophyConditionTypes.damage_over:
        case TrophyConditionTypes.complete_all_quest_mission:
        case TrophyConditionTypes.has_gold_over:
        case TrophyConditionTypes.complete_all_quest_mission_total:
        case TrophyConditionTypes.complete_all_mission_count:
        case TrophyConditionTypes.complete_story_mission_count:
          QuestParam quest = MonoSingleton<GameManager>.Instance.FindQuest(param.Objectives[0].sval_base);
          string chapterId = quest == null ? (string) null : quest.ChapterID;
          ArchiveParam archiveByArea = MonoSingleton<GameManager>.Instance.FindArchiveByArea(chapterId);
          if (archiveByArea != null && archiveByArea.IsAvailable() && MonoSingleton<GameManager>.Instance.Player.CheckUnlock(UnlockTargets.Archive))
          {
            switch (quest.type)
            {
              case QuestTypes.Multi:
                if (LevelLock.ShowLockMessage(player.Lv, player.VipRank, UnlockTargets.MultiPlay))
                  return;
                if (MonoSingleton<GameManager>.Instance.Player.IsQuestArchiveOpenByArea(chapterId))
                {
                  this.trophy_window.ActivateOutputLinks(2005);
                  return;
                }
                UIUtility.ConfirmBox(LocalizedText.Get("sys.ARCHIVE_CHANGE_SCENE_CONFIRM"), (UIUtility.DialogResultEvent) (yes_button => this.GotoEventQuestArchive()), (UIUtility.DialogResultEvent) null);
                return;
              case QuestTypes.Event:
                if (LevelLock.ShowLockMessage(player.Lv, player.VipRank, UnlockTargets.EventQuest))
                  return;
                if (MonoSingleton<GameManager>.Instance.Player.IsQuestArchiveOpenByArea(chapterId))
                {
                  GlobalVars.SelectedQuestID = quest.iname;
                  GlobalVars.SelectedChapter.Set(chapterId);
                  GlobalVars.SelectedSection.Set("WD_DAILY");
                  GlobalVars.ReqEventPageListType = GlobalVars.EventQuestListType.EventQuestArchive;
                  FlowNode_Variable.Set("SHOW_CHAPTER", "0");
                  this.trophy_window.ActivateOutputLinks(2006);
                  return;
                }
                UIUtility.ConfirmBox(LocalizedText.Get("sys.ARCHIVE_CHANGE_SCENE_CONFIRM"), (UIUtility.DialogResultEvent) (yes_button => this.GotoEventQuestArchive()), (UIUtility.DialogResultEvent) null);
                return;
              default:
                return;
            }
          }
          else
          {
            QuestTypes quest_type = QuestTypes.Story;
            if (!QuestParam.TransSectionGotoQuest(param.Objectives[0].sval_base, out quest_type, new UIUtility.DialogResultEvent(this.MsgBoxJumpToQuest)))
              break;
            switch (quest_type)
            {
              case QuestTypes.Event:
              case QuestTypes.Gps:
                if (LevelLock.ShowLockMessage(player.Lv, player.VipRank, UnlockTargets.EventQuest))
                  return;
                this.trophy_window.ActivateOutputLinks(2006);
                return;
              case QuestTypes.Character:
                if (LevelLock.ShowLockMessage(player.Lv, player.VipRank, UnlockTargets.CharacterQuest))
                  return;
                this.trophy_window.ActivateOutputLinks(2032);
                return;
              case QuestTypes.Tower:
                this.trophy_window.ActivateOutputLinks(2026);
                return;
              case QuestTypes.Beginner:
                this.trophy_window.ActivateOutputLinks(2033);
                return;
              case QuestTypes.MultiGps:
                if (LevelLock.ShowLockMessage(player.Lv, player.VipRank, UnlockTargets.MultiPlay))
                  return;
                this.trophy_window.ActivateOutputLinks(2005);
                return;
              case QuestTypes.GenesisStory:
              case QuestTypes.GenesisBoss:
                if (LevelLock.ShowLockMessage(player.Lv, player.VipRank, UnlockTargets.EventQuest))
                  return;
                this.trophy_window.ActivateOutputLinks(2040);
                return;
              case QuestTypes.AdvanceStory:
              case QuestTypes.AdvanceBoss:
                if (LevelLock.ShowLockMessage(player.Lv, player.VipRank, UnlockTargets.EventQuest))
                  return;
                this.trophy_window.ActivateOutputLinks(2041);
                return;
              default:
                if (quest_type != QuestTypes.Multi)
                {
                  this.trophy_window.ActivateOutputLinks(2002);
                  return;
                }
                goto case QuestTypes.MultiGps;
            }
          }
        case TrophyConditionTypes.winelite:
        case TrophyConditionTypes.loseelite:
          if (!QuestParam.TransSectionGotoElite(new UIUtility.DialogResultEvent(this.MsgBoxJumpToQuest)))
            break;
          this.trophy_window.ActivateOutputLinks(2002);
          break;
        case TrophyConditionTypes.winevent:
        case TrophyConditionTypes.loseevent:
        case TrophyConditionTypes.exclear_fire:
        case TrophyConditionTypes.exclear_water:
        case TrophyConditionTypes.exclear_wind:
        case TrophyConditionTypes.exclear_thunder:
        case TrophyConditionTypes.exclear_light:
        case TrophyConditionTypes.exclear_dark:
        case TrophyConditionTypes.exclear_fire_nocon:
        case TrophyConditionTypes.exclear_water_nocon:
        case TrophyConditionTypes.exclear_wind_nocon:
        case TrophyConditionTypes.exclear_thunder_nocon:
        case TrophyConditionTypes.exclear_light_nocon:
        case TrophyConditionTypes.exclear_dark_nocon:
          if (LevelLock.ShowLockMessage(player.Lv, player.VipRank, UnlockTargets.EventQuest))
            break;
          QuestParam.GotoEventListQuest((string) null);
          this.trophy_window.ActivateOutputLinks(2006);
          break;
        case TrophyConditionTypes.gacha:
        case TrophyConditionTypes.collectunits:
          this.trophy_window.ActivateOutputLinks(2001);
          break;
        case TrophyConditionTypes.multiplay:
        case TrophyConditionTypes.winmulti:
        case TrophyConditionTypes.winmultimore:
        case TrophyConditionTypes.winmultiless:
          if (LevelLock.ShowLockMessage(player.Lv, player.VipRank, UnlockTargets.MultiPlay))
            break;
          this.trophy_window.ActivateOutputLinks(2005);
          break;
        case TrophyConditionTypes.ability:
        case TrophyConditionTypes.changeability:
          ChallengeMissionDetail.SetGoToUnitListState(param);
          this.trophy_window.ActivateOutputLinks(2025);
          break;
        case TrophyConditionTypes.soubi:
          this.GotoEquip();
          break;
        case TrophyConditionTypes.buygold:
          this.trophy_window.ActivateOutputLinks(2003);
          break;
        case TrophyConditionTypes.arena:
        case TrophyConditionTypes.winarena:
        case TrophyConditionTypes.losearena:
        case TrophyConditionTypes.becomearenarank:
        case TrophyConditionTypes.overarenarank:
          this.GotoArena();
          break;
        case TrophyConditionTypes.review:
          this.trophy_window.ActivateOutputLinks(2029);
          break;
        case TrophyConditionTypes.fggid:
          this.trophy_window.ActivateOutputLinks(2008);
          break;
        case TrophyConditionTypes.unitlevel:
        case TrophyConditionTypes.evolutionnum:
        case TrophyConditionTypes.upunitlevel:
        case TrophyConditionTypes.makeunitlevel:
          ChallengeMissionDetail.SetGoToUnitListState(param);
          this.trophy_window.ActivateOutputLinks(2025);
          break;
        case TrophyConditionTypes.joblevel:
        case TrophyConditionTypes.makeunitandjoblevel:
          string[] strArray1 = ChallengeMissionDetail.SplitSvalByComma(param);
          if (strArray1 != null && strArray1.Length > 0)
            ChallengeMissionDetail.SetGoToUnitListState(strArray1[0]);
          this.trophy_window.ActivateOutputLinks(2025);
          break;
        case TrophyConditionTypes.unitequip:
        case TrophyConditionTypes.upjoblevel:
        case TrophyConditionTypes.makejoblevel:
          ChallengeMissionDetail.SetGoToUnitListState(param);
          this.trophy_window.ActivateOutputLinks(2025);
          break;
        case TrophyConditionTypes.limitbreak:
        case TrophyConditionTypes.evoltiontimes:
        case TrophyConditionTypes.changejob:
          ChallengeMissionDetail.SetGoToUnitListState(param);
          this.trophy_window.ActivateOutputLinks(2025);
          break;
        case TrophyConditionTypes.makeabilitylevel:
          string[] strArray2 = ChallengeMissionDetail.SplitSvalByComma(param);
          if (strArray2 != null && strArray2.Length > 0)
            ChallengeMissionDetail.SetGoToUnitListState(strArray2[0]);
          this.trophy_window.ActivateOutputLinks(2025);
          break;
        case TrophyConditionTypes.buyatshop:
          this.GotoShop(param);
          break;
        case TrophyConditionTypes.artifacttransmute:
        case TrophyConditionTypes.artifactstrength:
        case TrophyConditionTypes.artifactevolution:
        case TrophyConditionTypes.upartifactlevel:
        case TrophyConditionTypes.makeartifactlevel:
          this.trophy_window.ActivateOutputLinks(2021);
          break;
        case TrophyConditionTypes.totaljoblv11:
          this.trophy_window.ActivateOutputLinks(2025);
          break;
        case TrophyConditionTypes.totalunitlvs:
          this.trophy_window.ActivateOutputLinks(2025);
          break;
        case TrophyConditionTypes.wintower:
        case TrophyConditionTypes.losetower:
        case TrophyConditionTypes.tower:
        case TrophyConditionTypes.overtowerscore:
          QuestTypes quest_type1 = QuestTypes.Tower;
          if (!QuestParam.TransSectionGotoTower(param.Objectives[0].sval_base, out quest_type1))
            break;
          switch (quest_type1)
          {
            case QuestTypes.Gps:
              if (LevelLock.ShowLockMessage(player.Lv, player.VipRank, UnlockTargets.EventQuest))
                return;
              this.trophy_window.ActivateOutputLinks(2006);
              return;
            case QuestTypes.Beginner:
              this.trophy_window.ActivateOutputLinks(2033);
              return;
            default:
              if (quest_type1 != QuestTypes.Event)
              {
                this.trophy_window.ActivateOutputLinks(2026);
                return;
              }
              goto case QuestTypes.Gps;
          }
        case TrophyConditionTypes.vs:
        case TrophyConditionTypes.vswin:
        case TrophyConditionTypes.overvsrankfloor:
          this.GotoVersus();
          break;
        case TrophyConditionTypes.winstory_extra:
          if (!QuestParam.TransSectionGotoStoryExtra(new UIUtility.DialogResultEvent(this.MsgBoxJumpToQuest)))
            break;
          this.trophy_window.ActivateOutputLinks(2002);
          break;
        case TrophyConditionTypes.multitower_help:
        case TrophyConditionTypes.multitower:
          this.GotoMultiTower();
          break;
        case TrophyConditionTypes.read_tips:
        case TrophyConditionTypes.read_tips_count:
          this.GotoBeginnerTop();
          break;
        case TrophyConditionTypes.up_conceptcard_level:
        case TrophyConditionTypes.up_conceptcard_level_target:
        case TrophyConditionTypes.limitbreak_conceptcard:
        case TrophyConditionTypes.limitbreak_conceptcard_target:
        case TrophyConditionTypes.up_conceptcard_trust:
        case TrophyConditionTypes.up_conceptcard_trust_target:
        case TrophyConditionTypes.max_conceptcard_trust:
          this.trophy_window.ActivateOutputLinks(2035);
          break;
        case TrophyConditionTypes.unlock_tobira_total:
          this.trophy_window.ActivateOutputLinks(2025);
          break;
        case TrophyConditionTypes.unlock_tobira_unit:
        case TrophyConditionTypes.envy_unlock_unit:
        case TrophyConditionTypes.sloth_unlock_unit:
        case TrophyConditionTypes.lust_unlock_unit:
        case TrophyConditionTypes.greed_unlock_unit:
        case TrophyConditionTypes.wrath_unlock_unit:
        case TrophyConditionTypes.gluttonny_unlock_unit:
        case TrophyConditionTypes.pride_unlock_unit:
          ChallengeMissionDetail.SetGoToUnitListState(param);
          this.trophy_window.ActivateOutputLinks(2025);
          break;
        case TrophyConditionTypes.send_present:
        case TrophyConditionTypes.friend_count:
          this.trophy_window.ActivateOutputLinks(2039);
          break;
        case TrophyConditionTypes.complete_event_mission_count:
          if (LevelLock.ShowLockMessage(player.Lv, player.VipRank, UnlockTargets.EventQuest))
            break;
          ChapterParam area1 = MonoSingleton<GameManager>.Instance.FindArea(param.Objectives[0].sval_base);
          if (area1 == null)
          {
            QuestParam.GotoEventListQuest((string) null);
            this.trophy_window.ActivateOutputLinks(2006);
            break;
          }
          if (area1.IsArchiveQuest)
          {
            if (MonoSingleton<GameManager>.Instance.Player.IsQuestArchiveOpenByArea(area1.iname))
            {
              GlobalVars.SelectedQuestID = (string) null;
              GlobalVars.SelectedChapter.Set(area1.iname);
              GlobalVars.SelectedSection.Set("WD_DAILY");
              GlobalVars.ReqEventPageListType = GlobalVars.EventQuestListType.EventQuestArchive;
              FlowNode_Variable.Set("SHOW_CHAPTER", "0");
              this.trophy_window.ActivateOutputLinks(2006);
              break;
            }
            UIUtility.ConfirmBox(LocalizedText.Get("sys.ARCHIVE_CHANGE_SCENE_CONFIRM"), (UIUtility.DialogResultEvent) (yes_button => this.GotoEventQuestArchive()), (UIUtility.DialogResultEvent) null);
            break;
          }
          if (!EventQuestList.ChapterContainsPlayableQuest(area1, MonoSingleton<GameManager>.Instance.Chapters, MonoSingleton<GameManager>.Instance.Player.AvailableQuests, Network.GetServerTime()))
          {
            UIUtility.SystemMessage((string) null, LocalizedText.Get("sys.QUEST_UNAVAILABLE"), new UIUtility.DialogResultEvent(this.MsgBoxJumpToQuest));
            break;
          }
          QuestParam.GotoEventListQuest(area1);
          this.trophy_window.ActivateOutputLinks(2006);
          break;
        case TrophyConditionTypes.complete_ordeal_mission_count:
        case TrophyConditionTypes.clear_ordeal:
          this.trophy_window.ActivateOutputLinks(2036);
          break;
        case TrophyConditionTypes.view_news:
          this.trophy_window.ActivateOutputLinks(2038);
          break;
        case TrophyConditionTypes.consume_stamina:
        case TrophyConditionTypes.winquestunit:
        case TrophyConditionTypes.clear_questmission_count:
        case TrophyConditionTypes.winquestunit_support:
          if (!QuestParam.TransSectionGotoNormal())
            break;
          this.trophy_window.ActivateOutputLinks(2002);
          break;
        case TrophyConditionTypes.consume_stamina_by_quest:
        case TrophyConditionTypes.clear_questmission_count_by_quest:
          TrophyObjective objective1 = param.Objectives[0];
          if (objective1 == null || objective1.sval == null)
            break;
          int index1 = 0;
          while (index1 < objective1.SvalCount && !this.GotoQuests(objective1.sval[index1]))
            ++index1;
          break;
        case TrophyConditionTypes.consume_stamina_by_area:
        case TrophyConditionTypes.clear_questmission_count_by_area:
          TrophyObjective objective2 = param.Objectives[0];
          if (objective2 == null || objective2.sval == null)
            break;
          for (int index2 = 0; index2 < objective2.SvalCount; ++index2)
          {
            ChapterParam area2 = MonoSingleton<GameManager>.Instance.FindArea(objective2.sval[index2]);
            if (area2 != null && area2.quests != null && this.GotoQuests(area2.quests.ToArray()))
              break;
          }
          break;
        case TrophyConditionTypes.consume_stamina_by_mode:
        case TrophyConditionTypes.clear_questmission_count_by_mode:
          TrophyObjective objective3 = param.Objectives[0];
          if (objective3 == null)
            break;
          this.GotoQuestMode(QuestParam.GetQuestDifficulties(objective3.sval_base));
          break;
        case TrophyConditionTypes.winquestunit_by_quest:
        case TrophyConditionTypes.winquestunit_support_by_quest:
          TrophyObjective objective4 = param.Objectives[0];
          if (objective4 == null || objective4.sval == null)
            break;
          if (objective4.SvalCount <= 1)
          {
            DebugUtility.LogError("トロフィー[" + objective4.Param.iname + "]のsvalにクエスト指定がありません.");
            break;
          }
          int index3 = 1;
          while (index3 < objective4.SvalCount && !this.GotoQuests(objective4.sval[index3]))
            ++index3;
          break;
        case TrophyConditionTypes.winquestunit_by_area:
        case TrophyConditionTypes.winquestunit_support_by_area:
          TrophyObjective objective5 = param.Objectives[0];
          if (objective5 == null || objective5.sval == null)
            break;
          if (objective5.SvalCount <= 1)
          {
            DebugUtility.LogError("トロフィー[" + objective5.Param.iname + "]のsvalにエリア指定がありません.");
            break;
          }
          for (int index4 = 1; index4 < objective5.SvalCount; ++index4)
          {
            ChapterParam area3 = MonoSingleton<GameManager>.Instance.FindArea(objective5.sval[index4]);
            if (area3 != null && area3.quests != null && this.GotoQuests(area3.quests.ToArray()))
              break;
          }
          break;
        case TrophyConditionTypes.winquestunit_by_mode:
        case TrophyConditionTypes.winquestunit_support_by_mode:
          TrophyObjective objective6 = param.Objectives[0];
          if (objective6 == null)
            break;
          if (objective6.SvalCount <= 1)
          {
            DebugUtility.LogError("トロフィー[" + objective6.Param.iname + "]のsvalに難易度指定がありません.");
            break;
          }
          this.GotoQuestMode(QuestParam.GetQuestDifficulties(objective6.sval[1]));
          break;
        case TrophyConditionTypes.join_guild:
        case TrophyConditionTypes.gvg:
          if (LevelLock.ShowLockMessage(player.Lv, player.VipRank, UnlockTargets.Guild))
            break;
          this.trophy_window.ActivateOutputLinks(2042);
          break;
        case TrophyConditionTypes.raidboss:
        case TrophyConditionTypes.raidboss_rescue_for_guildmember:
          if (MonoSingleton<GameManager>.Instance.MasterParam.GetRaidScheduleStatus() != RaidManager.RaidScheduleType.Close)
          {
            if (LevelLock.ShowLockMessage(player.Lv, player.VipRank, UnlockTargets.Raid))
              break;
            this.trophy_window.ActivateOutputLinks(2043);
            break;
          }
          UIUtility.SystemMessage((string) null, LocalizedText.Get("sys.RAID_ERROR_OUT_OF_PERIOD_MSG"), (UIUtility.DialogResultEvent) null);
          break;
        case TrophyConditionTypes.guildraid:
          if (LevelLock.ShowLockMessage(player.Lv, player.VipRank, UnlockTargets.GuildRaid))
            break;
          if (!player.IsGuildAssign)
          {
            this.trophy_window.ActivateOutputLinks(2042);
            break;
          }
          this.trophy_window.ActivateOutputLinks(2044);
          break;
        case TrophyConditionTypes.over_total_combat_power:
        case TrophyConditionTypes.over_unit_combat_power:
        case TrophyConditionTypes.over_fire_combat_power:
        case TrophyConditionTypes.over_water_combat_power:
        case TrophyConditionTypes.over_thunder_combat_power:
        case TrophyConditionTypes.over_wind_combat_power:
        case TrophyConditionTypes.over_shine_combat_power:
        case TrophyConditionTypes.over_dark_combat_power:
          this.trophy_window.ActivateOutputLinks(2025);
          break;
      }
    }

    protected void OnItemDetail(GameObject go)
    {
      TrophyParam dataOfClass = DataSource.FindDataOfClass<TrophyParam>(go, (TrophyParam) null);
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.DetailWindow, (UnityEngine.Object) null) || dataOfClass == null)
        return;
      GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.DetailWindow);
      DataSource.Bind<TrophyParam>(gameObject, dataOfClass);
      RewardData data = new RewardData(dataOfClass);
      DataSource.Bind<RewardData>(gameObject, data);
      gameObject.SetActive(true);
    }

    protected void GotoArena()
    {
      PlayerData player = MonoSingleton<GameManager>.Instance.Player;
      if (player.CheckUnlock(UnlockTargets.Arena))
        this.trophy_window.ActivateOutputLinks(2007);
      else
        LevelLock.ShowLockMessage(player.Lv, player.VipRank, UnlockTargets.Arena);
    }

    private void GotoVersus()
    {
      PlayerData player = MonoSingleton<GameManager>.Instance.Player;
      if (player.CheckUnlock(UnlockTargets.MultiVS))
        this.trophy_window.ActivateOutputLinks(2030);
      else
        LevelLock.ShowLockMessage(player.Lv, player.VipRank, UnlockTargets.MultiVS);
    }

    private void GotoMultiTower()
    {
      GlobalVars.ReqEventPageListType = GlobalVars.EventQuestListType.Tower;
      this.trophy_window.ActivateOutputLinks(2031);
    }

    private void GotoEquip()
    {
      PlayerData player = MonoSingleton<GameManager>.Instance.Player;
      if (player.CheckUnlock(UnlockTargets.EnhanceEquip))
        this.trophy_window.ActivateOutputLinks(2004);
      else
        LevelLock.ShowLockMessage(player.Lv, player.VipRank, UnlockTargets.EnhanceEquip);
    }

    private void GotoShop(TrophyParam param)
    {
      PlayerData player = MonoSingleton<GameManager>.Instance.Player;
      EShopType type;
      if (string.IsNullOrEmpty(param.Objectives[0].sval_base))
      {
        type = EShopType.Normal;
      }
      else
      {
        char[] chArray = new char[1]{ ',' };
        string[] strArray = param.Objectives[0].sval_base.Split(chArray);
        type = !string.IsNullOrEmpty(strArray[0]) ? (EShopType) MonoSingleton<GameManager>.Instance.MasterParam.GetShopType(strArray[0]) : EShopType.Normal;
      }
      if (type >= EShopType.Normal && player.CheckUnlockShopType(type))
      {
        switch (type)
        {
          case EShopType.Normal:
          case EShopType.Tabi:
          case EShopType.Kimagure:
            this.trophy_window.ActivateOutputLinks(2013);
            break;
          case EShopType.Arena:
            this.GotoArena();
            break;
          case EShopType.Multi:
            this.trophy_window.ActivateOutputLinks(2005);
            break;
          case EShopType.AwakePiece:
            this.trophy_window.ActivateOutputLinks(2020);
            break;
          case EShopType.Artifact:
            this.trophy_window.ActivateOutputLinks(2021);
            break;
          case EShopType.Event:
            this.trophy_window.ActivateOutputLinks(2028);
            break;
          case EShopType.Limited:
            this.trophy_window.ActivateOutputLinks(2027);
            break;
          case EShopType.Port:
            this.trophy_window.ActivateOutputLinks(2045);
            break;
        }
      }
      if (type < EShopType.Normal)
        return;
      if (MonoSingleton<GameManager>.Instance.Player.CheckUnlockShopType(type))
        return;
      try
      {
        UnlockTargets unlockTargets = type.ToUnlockTargets();
        LevelLock.ShowLockMessage(player.Lv, player.VipRank, unlockTargets);
      }
      catch (Exception ex)
      {
      }
    }

    private void GotoBeginnerTop() => this.trophy_window.ActivateOutputLinks(2034);

    protected bool GotoQuests(QuestParam[] quests)
    {
      if (quests == null || quests.Length <= 0)
        return false;
      for (int index = 0; index < quests.Length; ++index)
      {
        if (this.GotoQuests(quests[index]))
          return true;
      }
      return false;
    }

    protected bool GotoQuests(string iname)
    {
      return this.GotoQuests(MonoSingleton<GameManager>.Instance.FindQuest(iname));
    }

    protected bool GotoQuests(QuestParam quest)
    {
      if (quest == null)
        return false;
      QuestTypes quest_type = QuestTypes.Story;
      if (!QuestParam.TransSectionGotoQuest(quest.iname, out quest_type, new UIUtility.DialogResultEvent(this.MsgBoxJumpToQuest)))
        return true;
      switch (quest_type)
      {
        case QuestTypes.Event:
        case QuestTypes.Gps:
          this.trophy_window.ActivateOutputLinks(2006);
          break;
        case QuestTypes.Character:
          this.trophy_window.ActivateOutputLinks(2032);
          break;
        case QuestTypes.Tower:
          this.trophy_window.ActivateOutputLinks(2026);
          break;
        case QuestTypes.Beginner:
          this.trophy_window.ActivateOutputLinks(2033);
          break;
        case QuestTypes.MultiGps:
          this.trophy_window.ActivateOutputLinks(2005);
          break;
        case QuestTypes.GenesisStory:
        case QuestTypes.GenesisBoss:
          this.trophy_window.ActivateOutputLinks(2040);
          break;
        case QuestTypes.AdvanceStory:
        case QuestTypes.AdvanceBoss:
          this.trophy_window.ActivateOutputLinks(2041);
          break;
        default:
          if (quest_type != QuestTypes.Multi)
          {
            this.trophy_window.ActivateOutputLinks(2002);
            break;
          }
          goto case QuestTypes.MultiGps;
      }
      return true;
    }

    protected void GotoQuestMode(QuestDifficulties difficulty)
    {
      switch (difficulty)
      {
        case QuestDifficulties.Normal:
          QuestParam.TransSectionGotoNormal();
          break;
        case QuestDifficulties.Elite:
          if (!QuestParam.TransSectionGotoElite(new UIUtility.DialogResultEvent(this.MsgBoxJumpToQuest)))
            return;
          break;
        case QuestDifficulties.Extra:
          if (!QuestParam.TransSectionGotoStoryExtra(new UIUtility.DialogResultEvent(this.MsgBoxJumpToQuest)))
            return;
          break;
      }
      this.trophy_window.ActivateOutputLinks(2002);
    }

    protected void ChallengeBtnSetting(SRPG_Button btn, TrophyParam trophy)
    {
      bool flag = true;
      switch (trophy.Objectives[0].type)
      {
        case TrophyConditionTypes.killenemy:
        case TrophyConditionTypes.getitem:
        case TrophyConditionTypes.vip:
        case TrophyConditionTypes.stamina:
        case TrophyConditionTypes.card:
        case TrophyConditionTypes.logincount:
        case TrophyConditionTypes.childrencomp:
        case TrophyConditionTypes.dailyall:
          flag = false;
          break;
      }
      if (trophy.DispType == TrophyDispType.HideChallenge)
        flag = false;
      ((Component) btn).gameObject.SetActive(flag);
      if (!flag)
      {
        VerticalLayoutGroup component = ((Component) ((Component) btn).transform.parent).GetComponent<VerticalLayoutGroup>();
        if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) component, (UnityEngine.Object) null))
          return;
        ((LayoutGroup) component).childAlignment = (TextAnchor) 7;
      }
      else
      {
        Text componentInChildren = ((Component) ((Component) btn).transform).GetComponentInChildren<Text>();
        if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) componentInChildren, (UnityEngine.Object) null))
          return;
        componentInChildren.text = LocalizedText.Get("sys.TROPHY_BTN_GO");
      }
    }

    protected void GotoEventQuestArchive()
    {
      GlobalVars.SelectedChapter.Set(string.Empty);
      GlobalVars.SelectedSection.Set("WD_DAILY");
      GlobalVars.ReqEventPageListType = GlobalVars.EventQuestListType.EventQuestArchive;
      FlowNode_Variable.Set("SHOW_CHAPTER", "1");
      this.trophy_window.ActivateOutputLinks(2006);
    }

    protected void AchievementBtnSetting(SRPG_Button btn)
    {
      Text componentInChildren = ((Component) ((Component) btn).transform).GetComponentInChildren<Text>();
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) componentInChildren, (UnityEngine.Object) null))
        return;
      componentInChildren.text = LocalizedText.Get("sys.TROPHY_BTN_CLEAR");
    }

    protected void FollowBtnSetting(SRPG_Button btn)
    {
      Text componentInChildren = ((Component) ((Component) btn).transform).GetComponentInChildren<Text>();
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) componentInChildren, (UnityEngine.Object) null))
        return;
      componentInChildren.text = LocalizedText.Get("sys.TROPHY_BTN_FOLLOWTWITTER");
    }

    protected void MsgBoxJumpToQuest(GameObject go)
    {
    }

    private void Run()
    {
      switch (this.state)
      {
        case TrophyList.eState.IDLE:
          this.UpdateIdle();
          break;
        case TrophyList.eState.CLOSE_OTHER_WAIT:
          this.UpdateCloseOtherWait();
          break;
        case TrophyList.eState.FOCUS:
          this.UpdateFocus();
          break;
        case TrophyList.eState.FOCUS_INTERVAL:
          this.UpdateFocusInterval();
          break;
        case TrophyList.eState.CREATE_PULL_ITEM:
          this.UpdateCreatePullItems();
          break;
        case TrophyList.eState.OPEN:
          this.UpdateOpen();
          break;
        case TrophyList.eState.CLOSE:
          this.UpdateClose();
          break;
      }
    }

    private void ChangeState(TrophyList.eState _new_state)
    {
      if (this.state == _new_state)
        return;
      switch (this.state)
      {
        case TrophyList.eState.IDLE:
          this.EndIdle();
          break;
        case TrophyList.eState.CLOSE_OTHER_WAIT:
          this.EndCloseOtherWait();
          break;
        case TrophyList.eState.FOCUS:
          this.EndFocus();
          break;
        case TrophyList.eState.OPEN:
          this.EndOpen();
          break;
        case TrophyList.eState.CLOSE:
          this.EndClose();
          break;
      }
      this.state = _new_state;
      switch (this.state)
      {
        case TrophyList.eState.IDLE:
          this.StartIdle();
          break;
        case TrophyList.eState.CLOSE_OTHER_WAIT:
          this.StartCloseOtherWait();
          break;
        case TrophyList.eState.FOCUS:
          this.StartFocus();
          break;
        case TrophyList.eState.CREATE_PULL_ITEM:
          this.StartCreatePullItems();
          break;
        case TrophyList.eState.OPEN:
          this.StartOpen();
          break;
        case TrophyList.eState.CLOSE:
          this.StartClose();
          break;
      }
    }

    public void SetClickTarget(TrophyRecordPullView _pull_view)
    {
      if (this.is_busy)
        return;
      this.is_busy = true;
      this.click_target.Add(_pull_view);
    }

    private void CheckTarget()
    {
      if (this.click_target.Count <= 0)
        return;
      if (this.click_target[0].IsStateClosed)
      {
        if (!this.OpenPullView())
          return;
        this.scroll_rect.StopMovement();
      }
      else
      {
        if (!this.click_target[0].IsStateOpened || !this.ClosePullView())
          return;
        this.scroll_rect.StopMovement();
      }
    }

    private bool OpenPullView()
    {
      if (this.child_pull_viewes == null || this.child_pull_viewes.Count <= 0)
      {
        this.ChangeState(TrophyList.eState.FOCUS);
        return true;
      }
      this.ChangeState(TrophyList.eState.CLOSE_OTHER_WAIT);
      return true;
    }

    private bool ClosePullView()
    {
      this.ChangeState(TrophyList.eState.CLOSE);
      return true;
    }

    private void StartCloseOtherWait()
    {
      this.total_close_item_size = 0.0f;
      for (int index = 0; index < this.child_pull_viewes.Count; ++index)
      {
        if (!UnityEngine.Object.op_Equality((UnityEngine.Object) this.child_pull_viewes[index], (UnityEngine.Object) this.click_target[0]) && this.child_pull_viewes[index].IsStateOpen)
        {
          this.child_pull_viewes[index].ChangeState(TrophyRecordPullView.eState.CLOSE_IMMEDIATE);
          if (this.child_pull_viewes[index].Index < this.click_target[0].Index)
            this.total_close_item_size += this.child_pull_viewes[index].RootLayoutElementMinHeightDef;
        }
      }
    }

    private void EndCloseOtherWait()
    {
      this.ScrollTransRect.anchoredPosition = new Vector2(this.ScrollTransRect.anchoredPosition.x, Mathf.Max(0.0f, this.ScrollTransRect.anchoredPosition.y - this.total_close_item_size));
    }

    private void UpdateCloseOtherWait()
    {
      for (int index = 0; index < this.child_pull_viewes.Count; ++index)
      {
        if (!UnityEngine.Object.op_Equality((UnityEngine.Object) this.child_pull_viewes[index], (UnityEngine.Object) this.click_target[0]) && !this.child_pull_viewes[index].IsStateClosed)
          return;
      }
      this.ChangeState(TrophyList.eState.FOCUS);
    }

    private void StartFocus()
    {
      this.focus_delay_time = 0.2f;
      this.elapsed_time = 0.0f;
      this.start_pos = this.ScrollTransRect.anchoredPosition;
      this.target_pos = new Vector2(this.ScrollTransRect.anchoredPosition.x, (float) this.click_target[0].Index * this.click_target[0].ItemDistance);
    }

    private void EndFocus()
    {
      this.ScrollTransRect.anchoredPosition = new Vector2(0.0f, this.target_pos.y);
    }

    private void UpdateFocus()
    {
      this.focus_delay_time -= Time.deltaTime;
      if ((double) this.focus_delay_time > 0.0)
      {
        this.ScrollTransRect.anchoredPosition = this.start_pos;
      }
      else
      {
        this.elapsed_time += Time.deltaTime;
        this.elapsed_time = Mathf.Min(this.elapsed_time, this.FOCUS_EFFECT_SECOND);
        this.ScrollTransRect.anchoredPosition = new Vector2(0.0f, Vector2.Lerp(this.start_pos, this.target_pos, this.elapsed_time / this.FOCUS_EFFECT_SECOND).y);
        if ((double) this.elapsed_time < (double) this.FOCUS_EFFECT_SECOND)
          return;
        this.ChangeState(TrophyList.eState.FOCUS_INTERVAL);
      }
    }

    private void UpdateFocusInterval() => this.ChangeState(TrophyList.eState.CREATE_PULL_ITEM);

    private void StartCreatePullItems()
    {
      this.click_target[0].CreateContents();
      float num1 = this.ScrollTransRect.anchoredPosition.y + this.view_port_handle_rect.anchoredPosition.y - this.click_target[0].TargetViewPortSize;
      Rect rect = this.parent_panel_rect.rect;
      float num2 = Mathf.Max(0.0f, ((Rect) ref rect).height + num1 - this.click_target[0].VerticalLayoutSpacing);
      this.view_port_handle.minHeight = (double) this.view_port_handle.minHeight < (double) num2 ? num2 : this.view_port_handle.minHeight;
    }

    private void UpdateCreatePullItems() => this.ChangeState(TrophyList.eState.OPEN);

    private void StartOpen() => this.click_target[0].StartOpen();

    private void EndOpen()
    {
    }

    private void UpdateOpen()
    {
      if (!this.click_target[0].IsStateOpened)
        return;
      this.ChangeState(TrophyList.eState.IDLE);
      this.click_target.Clear();
    }

    private void StartClose()
    {
      this.view_port_handle.minHeight += this.click_target[0].TargetViewPortSize;
      this.click_target[0].StartClose();
    }

    protected void EndClose()
    {
      this.scroll_rect.verticalNormalizedPosition = Mathf.Clamp(this.scroll_rect.verticalNormalizedPosition, 0.0f, 1f);
    }

    protected void UpdateClose()
    {
      this.scroll_rect.verticalNormalizedPosition = Mathf.Clamp(this.scroll_rect.verticalNormalizedPosition, 0.0f, 1f);
      if (!this.click_target[0].IsStateClosed)
        return;
      this.ChangeState(TrophyList.eState.IDLE);
      this.click_target.Clear();
    }

    private void UpdateHandleHeight()
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.view_port_handle, (UnityEngine.Object) null) || (double) this.view_port_handle.minHeight <= 0.0)
        return;
      float num = this.ScrollTransRect.anchoredPosition.y + this.view_port_handle_rect.anchoredPosition.y;
      Rect rect = this.parent_panel_rect.rect;
      this.view_port_handle.minHeight = Mathf.Clamp(((Rect) ref rect).height + num, 0.0f, this.view_port_handle.minHeight);
    }

    private void StartIdle()
    {
      this.is_busy = false;
      SRPG_TouchInputModule.UnlockInput();
    }

    private void EndIdle() => SRPG_TouchInputModule.LockInput();

    private void UpdateIdle()
    {
      this.UpdateHandleHeight();
      this.CheckTarget();
    }

    private enum eState
    {
      IDLE,
      CLOSE_OTHER_WAIT,
      FOCUS,
      FOCUS_INTERVAL,
      CREATE_PULL_ITEM,
      OPEN,
      CLOSE,
    }

    public enum TrophyTypes
    {
      Normal,
      Daily,
      All,
    }
  }
}
