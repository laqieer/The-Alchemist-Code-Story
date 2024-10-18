// Decompiled with JetBrains decompiler
// Type: SRPG.RaidBossStageInfo
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
  [FlowNode.Pin(1, "Initialize", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(2, "Check Reward", FlowNode.PinTypes.Input, 2)]
  [FlowNode.Pin(101, "Next Reward", FlowNode.PinTypes.Output, 101)]
  [FlowNode.Pin(102, "Finish Reward", FlowNode.PinTypes.Output, 102)]
  public class RaidBossStageInfo : MonoBehaviour, IFlowInterface
  {
    public const int PIN_INPUT_INIT = 1;
    public const int PIN_INPUT_CHECK_REWARD = 2;
    public const int PIN_OUTPUT_GET_REWARD = 101;
    public const int PIN_OUTPUT_FINISH_REWARD = 102;
    [SerializeField]
    private GameObject mClearIcon;
    [SerializeField]
    private RawImage_Transparent mRaidImage;
    [SerializeField]
    private GameObject mRescueListGO;
    [SerializeField]
    private Transform mRescueListTransform;
    [SerializeField]
    private GameObject mRescueListItem;
    [SerializeField]
    private GameObject mRemainTimeGO;
    [SerializeField]
    private GameObject mChallengeButton;
    [SerializeField]
    private GameObject mScheduleClose;
    [SerializeField]
    private Text mRemainTimeText;
    [SerializeField]
    private Text mMemberCurrent;
    [SerializeField]
    private Text mMemberMax;
    private List<GameObject> mSOSMembers = new List<GameObject>();

    private void Update()
    {
      this.UpdateRemainTime();
      this.UpdateScheduleClose();
    }

    public void Activated(int pinID)
    {
      switch (pinID)
      {
        case 1:
          this.Init();
          break;
        case 2:
          if (RaidManager.Instance.HasRaidRewards())
          {
            FlowNode_GameObject.ActivateOutputLinks((Component) this, 101);
            break;
          }
          this.Init();
          RaidManager.Instance.AddAnnounceSkipCount();
          FlowNode_GameObject.ActivateOutputLinks((Component) this, 102);
          break;
      }
    }

    private void Init()
    {
      MasterParam masterParam = MonoSingleton<GameManager>.Instance.MasterParam;
      RaidBossData data = (RaidBossData) null;
      switch (RaidManager.Instance.SelectedRaidOwnerType)
      {
        case RaidManager.RaidOwnerType.Self:
          data = RaidManager.Instance.CurrentRaidBossData;
          break;
        case RaidManager.RaidOwnerType.Rescue:
        case RaidManager.RaidOwnerType.Rescue_Temp:
          data = RaidManager.Instance.RescueRaidBossData;
          break;
        case RaidManager.RaidOwnerType.Self_Cleared:
          data = RaidManager.Instance.SelectedClearedRaidBossData;
          break;
      }
      if (data == null)
        return;
      UnitParam unitParam = masterParam.GetUnitParam(data.RaidBossInfo.RaidBossParam.UnitIName);
      if (unitParam == null)
        return;
      DataSource.Bind<RaidBossData>(((Component) this).gameObject, data);
      DataSource.Bind<RaidBossInfo>(((Component) this).gameObject, data.RaidBossInfo);
      DataSource.Bind<RaidBossParam>(((Component) this).gameObject, data.RaidBossInfo.RaidBossParam);
      DataSource.Bind<UnitParam>(((Component) this).gameObject, unitParam);
      GlobalVars.SelectedQuestID = data.RaidBossInfo.RaidBossParam.QuestIName;
      if (data.RaidBossInfo.HP <= 0)
      {
        ((Graphic) this.mRaidImage).color = Color.cyan;
        this.mClearIcon.SetActive(true);
      }
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.mMemberCurrent, (UnityEngine.Object) null) || UnityEngine.Object.op_Equality((UnityEngine.Object) this.mMemberMax, (UnityEngine.Object) null))
        return;
      RaidPeriodParam raidPeriod = MonoSingleton<GameManager>.Instance.MasterParam.GetRaidPeriod(RaidManager.Instance.RaidPeriodId);
      if (raidPeriod == null)
        return;
      this.mMemberCurrent.text = data.SOSMember.Count.ToString();
      this.mMemberMax.text = raidPeriod.RescueMemberMax.ToString();
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.mRescueListGO, (UnityEngine.Object) null) || UnityEngine.Object.op_Equality((UnityEngine.Object) this.mRescueListTransform, (UnityEngine.Object) null) || UnityEngine.Object.op_Equality((UnityEngine.Object) this.mRescueListItem, (UnityEngine.Object) null))
        return;
      for (int index = 0; index < this.mSOSMembers.Count; ++index)
        UnityEngine.Object.Destroy((UnityEngine.Object) this.mSOSMembers[index]);
      this.mSOSMembers.Clear();
      if (data.SOSMember.Count > 0)
      {
        this.mRescueListGO.SetActive(true);
        this.mRescueListItem.SetActive(false);
        for (int index = 0; index < data.SOSMember.Count; ++index)
        {
          GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.mRescueListItem, this.mRescueListTransform);
          DataSource.Bind<RaidSOSMember>(gameObject, data.SOSMember[index]);
          gameObject.SetActive(true);
          this.mSOSMembers.Add(gameObject);
        }
      }
      else
        this.mRescueListGO.SetActive(false);
      GlobalVars.RestoreOwnerType.Set(RaidManager.Instance.SelectedRaidOwnerType);
      RaidManager.SelectedLastRaidOwnerType = RaidManager.Instance.SelectedRaidOwnerType;
      if (RaidManager.Instance.SelectedRaidOwnerType == RaidManager.RaidOwnerType.Rescue || RaidManager.Instance.SelectedRaidOwnerType == RaidManager.RaidOwnerType.Rescue_Temp)
      {
        RaidSOSMember raidSosMember = RaidManager.Instance.GetSOSMembers().Find((Predicate<RaidSOSMember>) (m => m.FUID == MonoSingleton<GameManager>.Instance.Player.FUID));
        if (raidSosMember != null)
          RaidManager.SelectedLastRaidRescueMemberType = raidSosMember.MemberType;
        else if (RaidManager.Instance.SelectedRaidRescueMember != null)
          RaidManager.SelectedLastRaidRescueMemberType = RaidManager.Instance.SelectedRaidRescueMember.MemberType;
      }
      GameParameter.UpdateAll(((Component) this).gameObject);
    }

    private void UpdateRemainTime()
    {
      RaidBossData raidBossData = (RaidBossData) null;
      switch (RaidManager.Instance.SelectedRaidOwnerType)
      {
        case RaidManager.RaidOwnerType.Self:
          raidBossData = RaidManager.Instance.CurrentRaidBossData;
          break;
        case RaidManager.RaidOwnerType.Rescue:
        case RaidManager.RaidOwnerType.Rescue_Temp:
          raidBossData = RaidManager.Instance.RescueRaidBossData;
          break;
        case RaidManager.RaidOwnerType.Self_Cleared:
          raidBossData = RaidManager.Instance.SelectedClearedRaidBossData;
          break;
      }
      if (raidBossData == null)
        return;
      if (raidBossData.RaidBossInfo.HP <= 0)
      {
        this.mRemainTimeGO.SetActive(false);
      }
      else
      {
        DateTime dateTime = TimeManager.FromUnixTime(raidBossData.RaidBossInfo.StartTime).AddDays((double) raidBossData.RaidBossInfo.RaidBossParam.TimeLimitSpan.Days).AddHours((double) raidBossData.RaidBossInfo.RaidBossParam.TimeLimitSpan.Hours).AddMinutes((double) raidBossData.RaidBossInfo.RaidBossParam.TimeLimitSpan.Minutes);
        if (dateTime < TimeManager.ServerTime)
        {
          this.mRemainTimeGO.SetActive(false);
        }
        else
        {
          TimeSpan timeSpan = dateTime - TimeManager.ServerTime;
          this.mRemainTimeText.text = string.Format(LocalizedText.Get("sys.RAID_RESCUE_REMAIN_TIME"), (object) (int) timeSpan.TotalHours, (object) timeSpan.Minutes);
          this.mRemainTimeGO.SetActive(true);
        }
      }
    }

    private void UpdateScheduleClose()
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.mScheduleClose, (UnityEngine.Object) null))
        return;
      RaidBossInfo dataOfClass = DataSource.FindDataOfClass<RaidBossInfo>(((Component) this).gameObject, (RaidBossInfo) null);
      if (dataOfClass != null && MonoSingleton<GameManager>.Instance.MasterParam.GetRaidScheduleStatus() == RaidManager.RaidScheduleType.CloseSchedule && !dataOfClass.IsReward && dataOfClass.HP > 0)
      {
        if (this.mScheduleClose.GetActive())
          return;
        ((Selectable) this.mChallengeButton.GetComponentInChildren<Button>()).interactable = false;
        this.mScheduleClose.SetActive(true);
      }
      else
      {
        if (!this.mScheduleClose.GetActive())
          return;
        this.mScheduleClose.SetActive(false);
      }
    }
  }
}
