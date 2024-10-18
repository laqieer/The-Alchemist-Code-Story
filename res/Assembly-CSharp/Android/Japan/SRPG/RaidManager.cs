// Decompiled with JetBrains decompiler
// Type: SRPG.RaidManager
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

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
    private int mSelectedRaidRescueIndex = -1;
    private List<RaidRewardData> mRaidRewards = new List<RaidRewardData>();
    private DateTime mRescueListRefreshWaitStarted = DateTime.MinValue;
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
    private Text mPeriodText;
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
    private RaidManager.RaidOwnerType mSelectedRaidOwnerType;
    private RaidBossInfo mSelectedClearedRaidBossInfo;
    private RaidBossData mSelectedClearedRaidBossData;
    private int mAreaClearAnnounceSkipCount;
    private DateTime mPeriodEndAt;
    private int mRescueListRefreshWaitSeconds;

    public static RaidManager Instance
    {
      get
      {
        return RaidManager.mInstance;
      }
    }

    public int RaidPeriodId
    {
      get
      {
        return this.mRaidPeriodId;
      }
    }

    public int CurrentRound
    {
      get
      {
        return this.mCurrentRound;
      }
    }

    public int CurrentRaidAreaId
    {
      get
      {
        return this.mCurrentRaidAreaId;
      }
    }

    public RaidBossData CurrentRaidBossData
    {
      get
      {
        return this.mCurrentRaidBossData;
      }
    }

    public RaidBossData RescueRaidBossData
    {
      get
      {
        return this.mRescueRaidBossData;
      }
    }

    public List<RaidBossInfo> BeatedRaidBossList
    {
      get
      {
        return this.mBeatedRaidBossList;
      }
    }

    public RaidManager.RaidOwnerType SelectedRaidOwnerType
    {
      get
      {
        return this.mSelectedRaidOwnerType;
      }
    }

    public List<RaidRescueMember> RaidRescueMemberList
    {
      get
      {
        return this.mRaidRescueMemberList;
      }
    }

    public RaidRescueMember SelectedRaidRescueMember
    {
      get
      {
        if (0 <= this.mSelectedRaidRescueIndex && this.mSelectedRaidRescueIndex < this.mRaidRescueMemberList.Count)
          return this.mRaidRescueMemberList[this.mSelectedRaidRescueIndex];
        return (RaidRescueMember) null;
      }
    }

    public int RaidBp
    {
      get
      {
        return this.mRaidBP.Current;
      }
    }

    public RaidBossInfo SelectedClearedRaidBossInfo
    {
      get
      {
        return this.mSelectedClearedRaidBossInfo;
      }
    }

    public RaidBossData SelectedClearedRaidBossData
    {
      get
      {
        return this.mSelectedClearedRaidBossData;
      }
    }

    public bool RescueListIsRefreshable
    {
      get
      {
        return (TimeManager.ServerTime - this.mRescueListRefreshWaitStarted).TotalSeconds > (double) this.mRescueListRefreshWaitSeconds;
      }
    }

    public bool RescueReqOptionGuild { get; set; }

    public bool RescueReqOptionFriend { get; set; }

    private void Awake()
    {
      RaidManager.mInstance = this;
      this.mAreaClearAnnounceSkipCount = 0;
      this.mRescueListRefreshWaitSeconds = 0;
      this.mRescueListRefreshWaitStarted = DateTime.MinValue;
    }

    private void OnDestroy()
    {
      RaidManager.mInstance = (RaidManager) null;
    }

    private void Update()
    {
      if (TimeManager.ServerTime < this.mPeriodEndAt)
        this.UpdateBP();
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
      this.mPeriodEndAt = MonoSingleton<GameManager>.Instance.MasterParam.GetRaidPeriod(RaidManager.Instance.RaidPeriodId).EndAt;
      RaidAreaParam raidArea = MonoSingleton<GameManager>.Instance.MasterParam.GetRaidArea(this.mCurrentRaidAreaId);
      if (raidArea == null)
      {
        DebugUtility.LogError("Areas is exist.");
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 901);
      }
      else
      {
        if ((UnityEngine.Object) this.mRescueListButton == (UnityEngine.Object) null || (UnityEngine.Object) this.mRescueDetailButton == (UnityEngine.Object) null)
          return;
        this.mRescueListButton.SetActive(this.mRescueRaidBossData == null);
        this.mRescueDetailButton.SetActive(this.mRescueRaidBossData != null);
        if ((UnityEngine.Object) this.mPeriodText == (UnityEngine.Object) null || (UnityEngine.Object) this.mPeriodParent == (UnityEngine.Object) null || ((UnityEngine.Object) this.mPeriodReceiveText == (UnityEngine.Object) null || (UnityEngine.Object) this.mPeriodReceiveParent == (UnityEngine.Object) null))
          return;
        if (TimeManager.ServerTime < this.mPeriodEndAt)
        {
          this.mPeriodParent.SetActive(true);
          this.mPeriodReceiveParent.SetActive(false);
          this.mPeriodText.text = string.Format(LocalizedText.Get("sys.RAID_PERIOD_FINISH_DATETIME"), (object) this.mPeriodEndAt.Month, (object) this.mPeriodEndAt.Day, (object) this.mPeriodEndAt.Hour, (object) this.mPeriodEndAt.Minute);
          this.UpdateBP();
        }
        else
        {
          this.mPeriodReceiveParent.SetActive(true);
          this.mPeriodParent.SetActive(false);
          this.UpdateReceivePeriod();
          this.mBPParent.SetActive(false);
          this.mRescueListButton.SetActive(false);
        }
        if ((UnityEngine.Object) this.mRoundText == (UnityEngine.Object) null)
          return;
        this.mRoundText.text = this.mCurrentRound.ToString();
        if ((UnityEngine.Object) this.mChallengeBadge == (UnityEngine.Object) null || (UnityEngine.Object) this.mRescueBadge == (UnityEngine.Object) null || (UnityEngine.Object) this.mCompleteBadge == (UnityEngine.Object) null)
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
        if ((UnityEngine.Object) this.mChallengeBadge == (UnityEngine.Object) null || (UnityEngine.Object) this.mNextAreaButton == (UnityEngine.Object) null || (UnityEngine.Object) this.mResetButton == (UnityEngine.Object) null)
          return;
        this.mChallengeButton.SetActive(false);
        this.mNextAreaButton.SetActive(false);
        this.mResetButton.SetActive(false);
        this.mCheckButton.SetActive(false);
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
      return (IEnumerator) new RaidManager.\u003CWaitLoadArea\u003Ec__Iterator0() { \u0024this = this };
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

    public void SelectedBoss()
    {
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 311);
    }

    private void SwitchRaidArea(int area_index)
    {
      if ((UnityEngine.Object) this.mCurrentRaidArea != (UnityEngine.Object) null)
      {
        UnityEngine.Object.Destroy((UnityEngine.Object) this.mCurrentRaidArea.gameObject);
        this.mCurrentRaidArea = (RaidArea) null;
      }
      if ((UnityEngine.Object) this.mRaidAreaPrefab == (UnityEngine.Object) null)
        return;
      this.mCurrentRaidArea = UnityEngine.Object.Instantiate<RaidArea>(this.mRaidAreaPrefab, this.mAreaTransform);
      this.mCurrentRaidArea.Initialize(area_index);
    }

    public void SetRescueIndex(int index)
    {
      this.mSelectedRaidRescueIndex = index;
    }

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

    public bool HasRaidRewards()
    {
      if (this.mRaidRewards == null)
        return false;
      return this.mRaidRewards.Count > 0;
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

    public void AddAnnounceSkipCount()
    {
      ++this.mAreaClearAnnounceSkipCount;
    }

    public List<RaidSOSMember> GetSOSMembers()
    {
      RaidBossData selectedRaidBoss = this.GetSelectedRaidBoss();
      if (selectedRaidBoss != null)
        return selectedRaidBoss.SOSMember;
      return new List<RaidSOSMember>();
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
      if ((UnityEngine.Object) this.mBPCoolDownGO == (UnityEngine.Object) null || (UnityEngine.Object) this.mBPCoolDownText == (UnityEngine.Object) null)
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
      if (response.player != null)
        MonoSingleton<GameManager>.Instance.Deserialize(response.player);
      if (response.cards != null)
        MonoSingleton<GameManager>.Instance.Player.OnDirtyConceptCardData();
      MonoSingleton<GameManager>.Instance.Player.OverwiteTrophyProgress(response.trophyprogs);
      MonoSingleton<GameManager>.Instance.Player.OverwiteTrophyProgress(response.bingoprogs);
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
      if (response.player != null)
        MonoSingleton<GameManager>.Instance.Deserialize(response.player);
      if (response.items != null)
        MonoSingleton<GameManager>.Instance.Deserialize(response.items);
      if (response.player != null)
        MonoSingleton<GameManager>.Instance.Deserialize(response.player);
      if (response.cards != null)
        MonoSingleton<GameManager>.Instance.Player.OnDirtyConceptCardData();
      MonoSingleton<GameManager>.Instance.Player.OverwiteTrophyProgress(response.trophyprogs);
      MonoSingleton<GameManager>.Instance.Player.OverwiteTrophyProgress(response.bingoprogs);
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
      if (response.player != null)
        MonoSingleton<GameManager>.Instance.Deserialize(response.player);
      if (response.cards != null)
        MonoSingleton<GameManager>.Instance.Player.OnDirtyConceptCardData();
      MonoSingleton<GameManager>.Instance.Player.OverwiteTrophyProgress(response.trophyprogs);
      MonoSingleton<GameManager>.Instance.Player.OverwiteTrophyProgress(response.bingoprogs);
      this.mIsRaidCompleteReward = false;
      this.mCompleteBadge.SetActive(false);
    }

    public void Setup(ReqRaidResetBp.Response response)
    {
      if (response.bp != null)
        this.mRaidBP.Deserialize(response.bp);
      if (response.player != null)
        MonoSingleton<GameManager>.Instance.Deserialize(response.player);
      GameParameter.UpdateAll(this.gameObject);
    }

    public enum RaidOwnerType
    {
      Self,
      Rescue,
      Rescue_Temp,
      Self_Cleared,
    }
  }
}
