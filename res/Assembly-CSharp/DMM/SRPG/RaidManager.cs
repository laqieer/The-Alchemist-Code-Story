// Decompiled with JetBrains decompiler
// Type: SRPG.RaidManager
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
  [FlowNode.Pin(1, "Initialize", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(11, "Start Challenge", FlowNode.PinTypes.Input, 11)]
  [FlowNode.Pin(12, "Selected Raid", FlowNode.PinTypes.Input, 12)]
  [FlowNode.Pin(13, "Selected List", FlowNode.PinTypes.Input, 13)]
  [FlowNode.Pin(14, "Selected Rescue", FlowNode.PinTypes.Input, 14)]
  [FlowNode.Pin(201, "Finish Initialize", FlowNode.PinTypes.Output, 201)]
  [FlowNode.Pin(301, "Select Raid", FlowNode.PinTypes.Output, 301)]
  [FlowNode.Pin(302, "Raid Stage Info", FlowNode.PinTypes.Output, 302)]
  [FlowNode.Pin(303, "Area Clear Reward", FlowNode.PinTypes.Output, 303)]
  [FlowNode.Pin(311, "Selected", FlowNode.PinTypes.Output, 311)]
  [FlowNode.Pin(901, "Error", FlowNode.PinTypes.Output, 901)]
  public class RaidManager : MonoBehaviour, IFlowInterface
  {
    public const int PIN_INPUT_INIT = 1;
    public const int PIN_INPUT_CHALLENGE = 11;
    public const int PIN_INPUT_SELECTED = 12;
    public const int PIN_INPUT_SELECTED_LIST = 13;
    public const int PIN_INPUT_SELECTED_RESCUE = 14;
    public const int PIN_OUTPUT_FINISH_INIT = 201;
    public const int PIN_OUTPUT_SELECT_RAID = 301;
    public const int PIN_OUTPUT_TO_DETAIL = 302;
    public const int PIN_OUTPUT_AREA_CLEAR_REWARD = 303;
    public const int PIN_OUTPUT_SELECTED = 311;
    public const int PIN_OUTPUT_ERROR = 901;
    [SerializeField]
    private Transform mAreaTransform;
    [SerializeField]
    private RaidArea mRaidAreaPrefab;
    [SerializeField]
    private GameObject mPeriodParent;
    [SerializeField]
    private Text mPeriodTitle;
    [SerializeField]
    private Text mPeriodText;
    [SerializeField]
    private GameObject mPeriodPeriodTimeParent;
    [SerializeField]
    private Text mPeriodTimeTitle;
    [SerializeField]
    private Text mPeriodTimeText;
    [SerializeField]
    private GameObject mPeriodReceiveParent;
    [SerializeField]
    private Text mPeriodReceiveText;
    [SerializeField]
    private Text mRoundText;
    [SerializeField]
    private GameObject mRescueListButton;
    [SerializeField]
    private GameObject mRescueDetailButton;
    [SerializeField]
    private GameObject mBPParent;
    [SerializeField]
    private List<GameObject> mBPList;
    [SerializeField]
    private GameObject mBPCoolDownGO;
    [SerializeField]
    private Text mBPCoolDownText;
    [SerializeField]
    private GameObject mChallengeBadge;
    [SerializeField]
    private GameObject mRescueBadge;
    [SerializeField]
    private GameObject mCompleteBadge;
    [SerializeField]
    private GameObject mChallengeButton;
    [SerializeField]
    private GameObject mNextAreaButton;
    [SerializeField]
    private GameObject mResetButton;
    [SerializeField]
    private GameObject mCheckButton;
    [SerializeField]
    private GameObject mRescueButton;
    [SerializeField]
    private GameObject mScheduleCloseButton;
    [SerializeField]
    private GameObject mTimeScheduleButton;
    private static RaidManager mInstance;
    private int mRaidPeriodId;
    private int mCurrentRound;
    private int mCurrentRaidAreaId;
    private bool mIsAreaReward;
    private bool mIsRaidCompleteReward;
    private RaidBP mRaidBP;
    private RaidArea mCurrentRaidArea;
    private RaidBossData mCurrentRaidBossData;
    private RaidBossData mRescueRaidBossData;
    private List<RaidBossInfo> mBeatedRaidBossList;
    private List<RaidRescueMember> mRaidRescueMemberList;
    private int mSelectedRaidRescueIndex = -1;
    private RaidManager.RaidOwnerType mSelectedRaidOwnerType;
    private List<RaidRewardData> mRaidRewards = new List<RaidRewardData>();
    private RaidBossInfo mSelectedClearedRaidBossInfo;
    private RaidBossData mSelectedClearedRaidBossData;
    private int mAreaClearAnnounceSkipCount;
    private DateTime mPeriodEndAt;
    private int mRescueListRefreshWaitSeconds;
    private DateTime mRescueListRefreshWaitStarted = DateTime.MinValue;
    private const int MULTITIMESET = 2;
    private const int TIME_SCHEDULEBASE = 0;
    public const int DAY_HOUR = 24;
    private const int SCHEDULE_DEFAULT = 0;

    public static RaidManager Instance => RaidManager.mInstance;

    public int RaidPeriodId => this.mRaidPeriodId;

    public int CurrentRound => this.mCurrentRound;

    public int CurrentRaidAreaId => this.mCurrentRaidAreaId;

    public RaidBossData CurrentRaidBossData => this.mCurrentRaidBossData;

    public RaidBossData RescueRaidBossData => this.mRescueRaidBossData;

    public List<RaidBossInfo> BeatedRaidBossList => this.mBeatedRaidBossList;

    public RaidManager.RaidOwnerType SelectedRaidOwnerType => this.mSelectedRaidOwnerType;

    public List<RaidRescueMember> RaidRescueMemberList => this.mRaidRescueMemberList;

    public RaidRescueMember SelectedRaidRescueMember
    {
      get
      {
        return 0 <= this.mSelectedRaidRescueIndex && this.mSelectedRaidRescueIndex < this.mRaidRescueMemberList.Count ? this.mRaidRescueMemberList[this.mSelectedRaidRescueIndex] : (RaidRescueMember) null;
      }
    }

    public int RaidBp => this.mRaidBP.Current;

    public RaidBossInfo SelectedClearedRaidBossInfo => this.mSelectedClearedRaidBossInfo;

    public RaidBossData SelectedClearedRaidBossData => this.mSelectedClearedRaidBossData;

    public bool RescueListIsRefreshable
    {
      get
      {
        return (TimeManager.ServerTime - this.mRescueListRefreshWaitStarted).TotalSeconds > (double) this.mRescueListRefreshWaitSeconds;
      }
    }

    public bool RescueReqOptionGuild { get; set; }

    public bool RescueReqOptionFriend { get; set; }

    public static RaidManager.RaidOwnerType SelectedLastRaidOwnerType { get; set; }

    public static RaidRescueMemberType SelectedLastRaidRescueMemberType { get; set; }

    private void Awake()
    {
      RaidManager.mInstance = this;
      this.mAreaClearAnnounceSkipCount = 0;
      this.mRescueListRefreshWaitSeconds = 0;
      this.mRescueListRefreshWaitStarted = DateTime.MinValue;
      RaidManager.SelectedLastRaidOwnerType = RaidManager.RaidOwnerType.Self;
      RaidManager.SelectedLastRaidRescueMemberType = RaidRescueMemberType.None;
    }

    private void OnDestroy() => RaidManager.mInstance = (RaidManager) null;

    private void Update()
    {
      if (TimeManager.ServerTime < this.mPeriodEndAt)
      {
        this.UpdateBP();
        this.UpdatePeriodScheduleTime();
        this.UpdateScheduleCloseButton();
      }
      else
        this.UpdateReceivePeriod();
    }

    public void Activated(int pinID)
    {
      switch (pinID)
      {
        case 1:
          this.Init();
          break;
        case 11:
          this.StartChallenge();
          break;
        case 12:
          this.mCurrentRaidArea.FinishSelectingRandomRaid();
          break;
        case 13:
          this.mSelectedRaidOwnerType = RaidManager.RaidOwnerType.Rescue_Temp;
          FlowNode_GameObject.ActivateOutputLinks((Component) this, 302);
          break;
        case 14:
          this.mSelectedRaidOwnerType = RaidManager.RaidOwnerType.Rescue;
          FlowNode_GameObject.ActivateOutputLinks((Component) this, 302);
          break;
      }
    }

    private void Init()
    {
      if (this.mSelectedRaidOwnerType == RaidManager.RaidOwnerType.Rescue_Temp)
      {
        this.mSelectedRaidOwnerType = RaidManager.RaidOwnerType.Self;
        this.mRescueRaidBossData = (RaidBossData) null;
      }
      this.mPeriodEndAt = MonoSingleton<GameManager>.Instance.MasterParam.GetRaidPeriod(this.RaidPeriodId).EndAt;
      RaidAreaParam raidArea = MonoSingleton<GameManager>.Instance.MasterParam.GetRaidArea(this.mCurrentRaidAreaId);
      if (raidArea == null)
      {
        DebugUtility.LogError("Areas is exist.");
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 901);
      }
      else
      {
        if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.mRescueListButton, (UnityEngine.Object) null) || UnityEngine.Object.op_Equality((UnityEngine.Object) this.mRescueDetailButton, (UnityEngine.Object) null))
          return;
        this.mRescueListButton.SetActive(this.mRescueRaidBossData == null);
        this.mRescueDetailButton.SetActive(this.mRescueRaidBossData != null);
        if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.mPeriodText, (UnityEngine.Object) null) || UnityEngine.Object.op_Equality((UnityEngine.Object) this.mPeriodParent, (UnityEngine.Object) null) || UnityEngine.Object.op_Equality((UnityEngine.Object) this.mPeriodReceiveText, (UnityEngine.Object) null) || UnityEngine.Object.op_Equality((UnityEngine.Object) this.mPeriodReceiveParent, (UnityEngine.Object) null))
          return;
        if (TimeManager.ServerTime < this.mPeriodEndAt)
        {
          this.mPeriodParent.SetActive(true);
          this.mPeriodReceiveParent.SetActive(false);
          this.mPeriodTitle.text = LocalizedText.Get("sys.RAID_PERIOD_CHALLENGETIME");
          this.mPeriodText.text = string.Format(LocalizedText.Get("sys.RAID_PERIOD_FINISH_DATETIME"), (object) this.mPeriodEndAt.Month, (object) this.mPeriodEndAt.Day, (object) this.mPeriodEndAt.Hour, (object) this.mPeriodEndAt.Minute);
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mTimeScheduleButton, (UnityEngine.Object) null))
          {
            switch (MonoSingleton<GameManager>.Instance.MasterParam.GetRaidScheduleStatus())
            {
              case RaidManager.RaidScheduleType.Open:
              case RaidManager.RaidScheduleType.Close:
                this.mTimeScheduleButton.SetActive(false);
                break;
              default:
                this.mTimeScheduleButton.SetActive(true);
                break;
            }
          }
          this.UpdateBP();
          this.UpdatePeriodScheduleTime();
        }
        else
        {
          this.mPeriodReceiveParent.SetActive(true);
          this.mPeriodParent.SetActive(false);
          this.UpdateReceivePeriod();
          this.mBPParent.SetActive(false);
          this.mRescueListButton.SetActive(false);
        }
        if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.mRoundText, (UnityEngine.Object) null))
          return;
        this.mRoundText.text = this.mCurrentRound.ToString();
        if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.mChallengeBadge, (UnityEngine.Object) null) || UnityEngine.Object.op_Equality((UnityEngine.Object) this.mRescueBadge, (UnityEngine.Object) null) || UnityEngine.Object.op_Equality((UnityEngine.Object) this.mCompleteBadge, (UnityEngine.Object) null))
          return;
        this.mChallengeBadge.SetActive(this.mCurrentRaidBossData != null && (this.mCurrentRaidBossData.RaidBossInfo.IsReward || this.mCurrentRaidBossData.RaidBossInfo.IsTimeOver));
        this.mRescueBadge.SetActive(this.mRescueRaidBossData != null && (this.mRescueRaidBossData.RaidBossInfo.IsReward || this.mRescueRaidBossData.RaidBossInfo.IsTimeOver));
        this.mCompleteBadge.SetActive(this.mIsRaidCompleteReward);
        this.SwitchRaidArea(raidArea.Order - 1);
        if (this.mAreaClearAnnounceSkipCount <= 0)
        {
          if (this.mIsAreaReward)
          {
            this.mIsAreaReward = false;
            FlowNode_GameObject.ActivateOutputLinks((Component) this, 303);
          }
        }
        else
          --this.mAreaClearAnnounceSkipCount;
        if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.mChallengeBadge, (UnityEngine.Object) null) || UnityEngine.Object.op_Equality((UnityEngine.Object) this.mNextAreaButton, (UnityEngine.Object) null) || UnityEngine.Object.op_Equality((UnityEngine.Object) this.mResetButton, (UnityEngine.Object) null) || UnityEngine.Object.op_Equality((UnityEngine.Object) this.mScheduleCloseButton, (UnityEngine.Object) null))
          return;
        this.mChallengeButton.SetActive(false);
        this.mNextAreaButton.SetActive(false);
        this.mResetButton.SetActive(false);
        this.mCheckButton.SetActive(false);
        this.mScheduleCloseButton.SetActive(false);
        if (!this.mCurrentRaidArea.IsAreaCleared)
        {
          if (TimeManager.ServerTime < this.mPeriodEndAt)
            this.mChallengeButton.SetActive(true);
          else
            this.mCheckButton.SetActive(this.mCurrentRaidBossData != null);
        }
        else if (raidArea.Order < MonoSingleton<GameManager>.Instance.MasterParam.GetRaidAreaCount(this.RaidPeriodId))
          this.mNextAreaButton.SetActive(true);
        else
          this.mResetButton.SetActive(true);
        if (HomeWindow.GetRestorePoint() == RestorePoints.Raid)
        {
          HomeWindow.SetRestorePoint(RestorePoints.Home);
          this.mSelectedRaidOwnerType = GlobalVars.RestoreOwnerType.Get();
          if (this.mSelectedRaidOwnerType == RaidManager.RaidOwnerType.Rescue_Temp)
            this.mSelectedRaidOwnerType = RaidManager.RaidOwnerType.Rescue;
          FlowNode_GameObject.ActivateOutputLinks((Component) this, 302);
        }
        this.StartCoroutine(this.WaitLoadArea());
      }
    }

    [DebuggerHidden]
    private IEnumerator WaitLoadArea()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new RaidManager.\u003CWaitLoadArea\u003Ec__Iterator0()
      {
        \u0024this = this
      };
    }

    public void StartChallenge()
    {
      if (this.mCurrentRaidBossData == null)
      {
        this.mCurrentRaidArea.StartSelectRaid();
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 301);
      }
      else
      {
        this.mSelectedRaidOwnerType = RaidManager.RaidOwnerType.Self;
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 302);
      }
    }

    public void SelectedBoss() => FlowNode_GameObject.ActivateOutputLinks((Component) this, 311);

    private void SwitchRaidArea(int area_index)
    {
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mCurrentRaidArea, (UnityEngine.Object) null))
      {
        UnityEngine.Object.Destroy((UnityEngine.Object) ((Component) this.mCurrentRaidArea).gameObject);
        this.mCurrentRaidArea = (RaidArea) null;
      }
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.mRaidAreaPrefab, (UnityEngine.Object) null))
        return;
      this.mCurrentRaidArea = UnityEngine.Object.Instantiate<RaidArea>(this.mRaidAreaPrefab, this.mAreaTransform);
      this.mCurrentRaidArea.Initialize(area_index);
    }

    public void SetRescueIndex(int index) => this.mSelectedRaidRescueIndex = index;

    public RaidRewardData GetRaidRewards()
    {
      if (this.mRaidRewards == null)
        return (RaidRewardData) null;
      if (this.mRaidRewards.Count == 0)
        return (RaidRewardData) null;
      RaidRewardData mRaidReward = this.mRaidRewards[0];
      this.mRaidRewards.Remove(mRaidReward);
      return mRaidReward;
    }

    public bool HasRaidRewards() => this.mRaidRewards != null && this.mRaidRewards.Count > 0;

    public void GeActiveRaidPeriodTimeString(out string title, out string timeCount)
    {
      title = string.Empty;
      timeCount = string.Empty;
      switch (MonoSingleton<GameManager>.Instance.MasterParam.GetRaidScheduleStatus())
      {
        case RaidManager.RaidScheduleType.Open:
          break;
        case RaidManager.RaidScheduleType.Close:
          break;
        default:
          bool nowCheck = false;
          RaidPeriodTimeScheduleParam raidScheduleTime = MonoSingleton<GameManager>.Instance.MasterParam.GetRaidScheduleTime(out nowCheck);
          if (raidScheduleTime == null)
            break;
          RaidPeriodParam activeRaidPeriod = MonoSingleton<GameManager>.Instance.MasterParam.GetActiveRaidPeriod();
          DateTime dateTime1 = DateTime.Parse(TimeManager.ServerTime.ToShortDateString() + " " + raidScheduleTime.Begin + ":00");
          TimeSpan timeSpan = RaidManager.GetTimeSpan(raidScheduleTime.Open);
          DateTime dateTime2 = dateTime1 + timeSpan;
          string format = LocalizedText.Get("sys.RAIDTIME_BETWEEN");
          title = !nowCheck ? LocalizedText.Get("sys.RAIDTIME_NEXT_TITLE") : LocalizedText.Get("sys.RAIDTIME_BETWEEN_TITLE");
          if (activeRaidPeriod.EndAt < dateTime2)
            dateTime2 = activeRaidPeriod.EndAt;
          if (dateTime1.Hour == dateTime2.Hour && dateTime1.Day != dateTime2.Day)
          {
            timeCount = string.Format(format, (object) dateTime1.Hour, (object) dateTime1.Minute, (object) (dateTime2.Hour + 24), (object) dateTime2.Minute);
            break;
          }
          timeCount = string.Format(format, (object) dateTime1.Hour, (object) dateTime1.Minute, (object) dateTime2.Hour, (object) dateTime2.Minute);
          break;
      }
    }

    public RaidBossData GetSelectedRaidBoss()
    {
      switch (this.SelectedRaidOwnerType)
      {
        case RaidManager.RaidOwnerType.Self:
          return this.mCurrentRaidBossData;
        case RaidManager.RaidOwnerType.Rescue:
        case RaidManager.RaidOwnerType.Rescue_Temp:
          return this.mRescueRaidBossData;
        case RaidManager.RaidOwnerType.Self_Cleared:
          return this.mSelectedClearedRaidBossData;
        default:
          return (RaidBossData) null;
      }
    }

    public void AddAnnounceSkipCount() => ++this.mAreaClearAnnounceSkipCount;

    public List<RaidSOSMember> GetSOSMembers()
    {
      RaidBossData selectedRaidBoss = this.GetSelectedRaidBoss();
      return selectedRaidBoss != null ? selectedRaidBoss.SOSMember : new List<RaidSOSMember>();
    }

    public void ShowDetail(RaidBossInfo info)
    {
      this.mSelectedClearedRaidBossInfo = info;
      this.mSelectedRaidOwnerType = RaidManager.RaidOwnerType.Self_Cleared;
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 302);
    }

    public void UpdateBP()
    {
      if (this.mRaidBP == null)
        return;
      if (this.mRaidBP.Max > this.mRaidBP.Current && this.mRaidBP.At <= TimeManager.ServerTime)
      {
        this.mRaidBP.AddPoint();
        if (this.mRaidBP.Max > this.mRaidBP.Current)
          this.mRaidBP.AddMinutes();
      }
      if (this.mBPList == null)
        return;
      for (int index = 0; index < this.mBPList.Count; ++index)
        this.mBPList[index].SetActive(index < this.mRaidBP.Current);
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.mBPCoolDownGO, (UnityEngine.Object) null) || UnityEngine.Object.op_Equality((UnityEngine.Object) this.mBPCoolDownText, (UnityEngine.Object) null))
        return;
      this.mBPCoolDownGO.SetActive(this.mRaidBP.At > TimeManager.ServerTime);
      if (!(this.mRaidBP.At > TimeManager.ServerTime))
        return;
      TimeSpan timeSpan = this.mRaidBP.At - TimeManager.ServerTime;
      string strB = string.Format("{0:00}:{1:00}:{2:00}", (object) timeSpan.Hours, (object) timeSpan.Minutes, (object) timeSpan.Seconds);
      if (string.Compare(this.mBPCoolDownText.text, strB) == 0)
        return;
      this.mBPCoolDownText.text = strB;
    }

    public void UpdateReceivePeriod()
    {
      TimeSpan timeSpan = this.mPeriodEndAt.AddHours(24.0) - TimeManager.ServerTime;
      if (timeSpan.TotalSeconds < 0.0)
        return;
      string strB = string.Format("{0:00}:{1:00}:{2:00}", (object) timeSpan.Hours, (object) timeSpan.Minutes, (object) timeSpan.Seconds);
      if (string.Compare(this.mPeriodReceiveText.text, strB) == 0)
        return;
      this.mPeriodReceiveText.text = strB;
    }

    public void UpdatePeriodScheduleTime()
    {
      switch (MonoSingleton<GameManager>.Instance.MasterParam.GetRaidScheduleStatus())
      {
        case RaidManager.RaidScheduleType.OpenSchedule:
        case RaidManager.RaidScheduleType.CloseSchedule:
          string title;
          string timeCount;
          this.GeActiveRaidPeriodTimeString(out title, out timeCount);
          this.mPeriodTimeTitle.text = title;
          this.mPeriodTimeText.text = timeCount;
          if (string.IsNullOrEmpty(title) && string.IsNullOrEmpty(timeCount))
          {
            this.mPeriodPeriodTimeParent.SetActive(false);
            break;
          }
          this.mPeriodPeriodTimeParent.SetActive(true);
          break;
        default:
          this.mPeriodPeriodTimeParent.SetActive(false);
          break;
      }
    }

    private void UpdateScheduleCloseButton()
    {
      if (MonoSingleton<GameManager>.Instance.MasterParam.GetRaidScheduleStatus() != RaidManager.RaidScheduleType.CloseSchedule || this.mChallengeBadge.GetActive() || this.mScheduleCloseButton.GetActive() || (this.mSelectedClearedRaidBossInfo == null || this.mSelectedClearedRaidBossInfo.IsReward) && this.mSelectedClearedRaidBossInfo != null)
        return;
      ((Selectable) this.mChallengeButton.GetComponentInChildren<Button>()).interactable = false;
      this.mNextAreaButton.SetActive(false);
      this.mResetButton.SetActive(false);
      this.mCheckButton.SetActive(false);
      this.mScheduleCloseButton.SetActive(true);
    }

    private List<DateTime> GetBeginScheduleTime()
    {
      List<DateTime> beginScheduleTime = new List<DateTime>();
      List<RaidPeriodTimeParam> raidPeriodTime = MonoSingleton<GameManager>.Instance.MasterParam.GetRaidPeriodTime();
      RaidPeriodParam raidPeriod = MonoSingleton<GameManager>.Instance.MasterParam.GetRaidPeriod(this.RaidPeriodId);
      int num = 0;
      for (int index = 0; index < raidPeriodTime.Count; ++index)
      {
        if (raidPeriodTime[index].PeriodId == this.RaidPeriodId)
        {
          DateTime beginAt = DateTime.Parse(raidPeriodTime[index].BeginAt.ToShortDateString() + " 00:00:00");
          if (num == 0 && beginAt < raidPeriodTime[index].BeginAt)
            beginAt = raidPeriodTime[index].BeginAt;
          if (!beginScheduleTime.Contains(beginAt))
            beginScheduleTime.Add(beginAt);
          if (num != 0)
          {
            if (!(raidPeriod.EndAt < raidPeriodTime[index].EndAt.AddDays(1.0)))
            {
              beginAt = DateTime.Parse(raidPeriodTime[index].EndAt.ToShortDateString() + " 00:00:00");
              if (!beginScheduleTime.Contains(beginAt.AddDays(1.0)))
                beginScheduleTime.Add(beginAt.AddDays(1.0));
            }
            else
              continue;
          }
          ++num;
        }
      }
      beginScheduleTime.Sort();
      return beginScheduleTime;
    }

    private void CreateScheduleMessaageBox()
    {
      RaidPeriodParam raidPeriod = MonoSingleton<GameManager>.Instance.MasterParam.GetRaidPeriod(this.RaidPeriodId);
      string empty = string.Empty;
      if (raidPeriod != null)
      {
        List<DateTime> beginScheduleTime = this.GetBeginScheduleTime();
        if (beginScheduleTime.Count == 0)
          return;
        for (int index = 0; index < beginScheduleTime.Count - 1; ++index)
          empty += this.SetTimeScheduleList(beginScheduleTime[index], beginScheduleTime[index + 1], -1);
        empty += this.SetTimeScheduleList(beginScheduleTime[beginScheduleTime.Count - 1], raidPeriod.EndAt);
      }
      if (string.IsNullOrEmpty(empty))
        return;
      UIUtility.SystemMessage(LocalizedText.Get("sys.RAID_SCHEDULETITLE"), empty, (UIUtility.DialogResultEvent) null);
    }

    private string SetTimeScheduleList(DateTime beginTime, DateTime endTime, int addDay = 0)
    {
      List<RaidPeriodTimeParam> raidPeriodTime = MonoSingleton<GameManager>.Instance.MasterParam.GetRaidPeriodTime();
      RaidPeriodParam raidPeriod = MonoSingleton<GameManager>.Instance.MasterParam.GetRaidPeriod(this.RaidPeriodId);
      string str = string.Empty + LocalizedText.Get("sys.RAID_ALLSCHEDULEDAY") + beginTime.Month.ToString() + "/" + beginTime.Day.ToString() + "～" + endTime.AddDays((double) addDay).Month.ToString() + "/" + endTime.AddDays((double) addDay).Day.ToString() + "\n";
      for (int index1 = raidPeriodTime.Count - 1; index1 >= 0; --index1)
      {
        if (raidPeriodTime[index1].PeriodId == this.RaidPeriodId && raidPeriodTime[index1].BeginAt <= beginTime && beginTime < raidPeriodTime[index1].EndAt)
        {
          for (int index2 = 0; index2 < raidPeriodTime[index1].Schedule.Count; ++index2)
          {
            DateTime dateTime1 = DateTime.Parse(beginTime.ToShortDateString() + " " + raidPeriodTime[index1].Schedule[index2].Begin + ":00");
            TimeSpan timeSpan = RaidManager.GetTimeSpan(raidPeriodTime[index1].Schedule[index2].Open);
            DateTime dateTime2 = dateTime1 + timeSpan;
            if (!(raidPeriod.EndAt < raidPeriodTime[index1].BeginAt + TimeSpan.Parse(raidPeriodTime[index1].Schedule[index2].Begin)))
            {
              if (dateTime1.Hour == dateTime2.Hour && dateTime1.Day != dateTime2.Day)
                str = str + raidPeriodTime[index1].Schedule[index2].Begin + "～" + (dateTime2.Hour + 24).ToString() + ":00\n";
              else
                str = str + raidPeriodTime[index1].Schedule[index2].Begin + "～" + dateTime2.Hour.ToString() + ":00\n";
            }
          }
          break;
        }
      }
      return str;
    }

    public void Setup(ReqRaid.Response response)
    {
      this.mRaidPeriodId = response.period_id;
      this.mCurrentRound = response.round;
      this.mCurrentRaidAreaId = response.area_id;
      this.mIsAreaReward = response.is_area_reward == 1;
      this.mIsRaidCompleteReward = response.is_raid_complete_reward == 1;
      DateTime dateTime = DateTime.Parse(MonoSingleton<GameManager>.Instance.MasterParam.GetRaidPeriod(this.mRaidPeriodId).AddBpTime);
      this.mRaidBP = new RaidBP(dateTime.Hour * 60 + dateTime.Minute);
      if (response.bp != null)
        this.mRaidBP.Deserialize(response.bp);
      this.mCurrentRaidBossData = (RaidBossData) null;
      if (response.raidboss_current != null && response.raidboss_current.boss_info != null)
      {
        this.mCurrentRaidBossData = new RaidBossData();
        this.mCurrentRaidBossData.Deserialize(response.raidboss_current);
      }
      this.mRescueRaidBossData = (RaidBossData) null;
      if (response.rescue_current != null && response.rescue_current.boss_info != null)
      {
        this.mRescueRaidBossData = new RaidBossData();
        this.mRescueRaidBossData.Deserialize(response.rescue_current);
      }
      this.mBeatedRaidBossList = new List<RaidBossInfo>();
      if (response.raidboss_knock_down == null || response.raidboss_knock_down.Length <= 0)
        return;
      for (int index = 0; index < response.raidboss_knock_down.Length; ++index)
      {
        RaidBossInfo raidBossInfo = new RaidBossInfo();
        if (raidBossInfo.Deserialize(response.raidboss_knock_down[index]))
          this.mBeatedRaidBossList.Add(raidBossInfo);
      }
    }

    public void Setup(ReqRaidSelect.Response response)
    {
      if (response.raidboss_current == null || response.raidboss_current.boss_info == null)
        return;
      this.mCurrentRaidBossData = new RaidBossData();
      this.mCurrentRaidBossData.Deserialize(response.raidboss_current);
    }

    public void Setup(ReqRaidInfo.Response response)
    {
      if (response.raidboss == null || response.raidboss.boss_info == null)
        return;
      switch (this.SelectedRaidOwnerType)
      {
        case RaidManager.RaidOwnerType.Self:
          this.mCurrentRaidBossData = new RaidBossData();
          this.mCurrentRaidBossData.Deserialize(response.raidboss);
          break;
        case RaidManager.RaidOwnerType.Rescue:
        case RaidManager.RaidOwnerType.Rescue_Temp:
          this.mRescueRaidBossData = new RaidBossData();
          this.mRescueRaidBossData.Deserialize(response.raidboss);
          break;
        case RaidManager.RaidOwnerType.Self_Cleared:
          this.mSelectedClearedRaidBossData = new RaidBossData();
          this.mSelectedClearedRaidBossData.Deserialize(response.raidboss);
          break;
      }
    }

    public void Setup(ReqRaidRescue.Response response)
    {
      if (response.sos != null)
      {
        this.mRaidRescueMemberList = new List<RaidRescueMember>();
        for (int index = 0; index < response.sos.Length; ++index)
        {
          RaidRescueMember raidRescueMember = new RaidRescueMember();
          if (raidRescueMember.Deserialize(response.sos[index]))
            this.mRaidRescueMemberList.Add(raidRescueMember);
        }
      }
      this.mRescueListRefreshWaitSeconds = response.refresh_wait_sec;
      this.mRescueListRefreshWaitStarted = TimeManager.ServerTime;
    }

    public void Setup(ReqRaidRewardAreaClear.Response response)
    {
      if (response.reward != null)
      {
        RaidRewardData raidRewardData = new RaidRewardData();
        if (raidRewardData.Deserialize(RaidRewardKind.AreaClear, response.reward))
          this.mRaidRewards.Add(raidRewardData);
      }
      if (response.player != null)
        MonoSingleton<GameManager>.Instance.Deserialize(response.player);
      if (response.items != null)
        MonoSingleton<GameManager>.Instance.Deserialize(response.items);
      if (response.units != null)
        MonoSingleton<GameManager>.Instance.Deserialize(response.units);
      if (response.cards != null)
        MonoSingleton<GameManager>.Instance.Player.OnDirtyConceptCardData();
      if (response.artifacts != null)
        MonoSingleton<GameManager>.Instance.Deserialize(response.artifacts, true);
      MonoSingleton<GameManager>.Instance.Player.TrophyData.OverwriteTrophyProgress(response.trophyprogs);
      MonoSingleton<GameManager>.Instance.Player.TrophyData.OverwriteTrophyProgress(response.bingoprogs);
    }

    public void Setup(ReqRaidRewardBeat.Response response)
    {
      if (response.raid_beat_reward != null)
      {
        RaidRewardData raidRewardData = new RaidRewardData();
        if (raidRewardData.Deserialize(RaidRewardKind.Beat, response.raid_beat_reward))
          this.mRaidRewards.Add(raidRewardData);
      }
      if (response.raid_damage_ratio_reward != null)
      {
        RaidRewardData raidRewardData = new RaidRewardData();
        if (raidRewardData.Deserialize(RaidRewardKind.DamageRatio, response.raid_damage_ratio_reward))
          this.mRaidRewards.Add(raidRewardData);
      }
      if (response.raid_damage_amount_reward != null)
      {
        RaidRewardData raidRewardData = new RaidRewardData();
        if (raidRewardData.Deserialize(RaidRewardKind.DamageAmount, response.raid_damage_amount_reward))
          this.mRaidRewards.Add(raidRewardData);
      }
      if (response.player != null)
        MonoSingleton<GameManager>.Instance.Deserialize(response.player);
      if (response.items != null)
        MonoSingleton<GameManager>.Instance.Deserialize(response.items);
      if (response.units != null)
        MonoSingleton<GameManager>.Instance.Deserialize(response.units);
      if (response.cards != null)
        MonoSingleton<GameManager>.Instance.Player.OnDirtyConceptCardData();
      if (response.artifacts != null)
        MonoSingleton<GameManager>.Instance.Deserialize(response.artifacts, true);
      MonoSingleton<GameManager>.Instance.Player.TrophyData.OverwriteTrophyProgress(response.trophyprogs);
      MonoSingleton<GameManager>.Instance.Player.TrophyData.OverwriteTrophyProgress(response.bingoprogs);
    }

    public void Setup(ReqRaidRewardRescueComplete.Response response)
    {
      if (response.reward != null)
      {
        RaidRewardData raidRewardData = new RaidRewardData();
        if (raidRewardData.Deserialize(RaidRewardKind.Complete, response.reward))
          this.mRaidRewards.Add(raidRewardData);
      }
      if (response.player != null)
        MonoSingleton<GameManager>.Instance.Deserialize(response.player);
      if (response.items != null)
        MonoSingleton<GameManager>.Instance.Deserialize(response.items);
      if (response.units != null)
        MonoSingleton<GameManager>.Instance.Deserialize(response.units);
      if (response.cards != null)
        MonoSingleton<GameManager>.Instance.Player.OnDirtyConceptCardData();
      if (response.artifacts != null)
        MonoSingleton<GameManager>.Instance.Deserialize(response.artifacts, true);
      MonoSingleton<GameManager>.Instance.Player.TrophyData.OverwriteTrophyProgress(response.trophyprogs);
      MonoSingleton<GameManager>.Instance.Player.TrophyData.OverwriteTrophyProgress(response.bingoprogs);
      this.mIsRaidCompleteReward = false;
      this.mCompleteBadge.SetActive(false);
    }

    public void Setup(ReqRaidResetBp.Response response)
    {
      if (response.bp != null)
        this.mRaidBP.Deserialize(response.bp);
      if (response.player != null)
        MonoSingleton<GameManager>.Instance.Deserialize(response.player);
      GameParameter.UpdateAll(((Component) this).gameObject);
    }

    public static TimeSpan GetTimeSpan(string time)
    {
      string[] strArray = time.Split(':');
      int[] numArray = new int[3];
      if (strArray.Length == 2)
        numArray[2] = 0;
      for (int index = 0; index < strArray.Length; ++index)
        numArray[index] = int.Parse(strArray[index]);
      int num1 = numArray[0];
      int num2 = numArray[1];
      int num3 = numArray[2];
      DateTime dateTime1 = new DateTime(0L);
      DateTime dateTime2 = new DateTime(0L);
      dateTime1 = dateTime1.AddHours((double) num1);
      dateTime1 = dateTime1.AddMinutes((double) num2);
      dateTime1 = dateTime1.AddSeconds((double) num3);
      return dateTime1.Day <= dateTime2.Day ? new TimeSpan(dateTime1.Hour, dateTime1.Minute, dateTime1.Second) : new TimeSpan(dateTime1.Hour + 24, dateTime1.Minute, dateTime1.Second);
    }

    public enum RaidOwnerType
    {
      Self,
      Rescue,
      Rescue_Temp,
      Self_Cleared,
    }

    public enum RaidScheduleType
    {
      Open,
      Close,
      OpenSchedule,
      CloseSchedule,
    }
  }
}
