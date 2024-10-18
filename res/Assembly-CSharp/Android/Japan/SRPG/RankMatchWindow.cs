﻿// Decompiled with JetBrains decompiler
// Type: SRPG.RankMatchWindow
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
  [FlowNode.Pin(100, "Refresh Map", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(110, "Refresh Party", FlowNode.PinTypes.Input, 1)]
  public class RankMatchWindow : MonoBehaviour, IFlowInterface
  {
    private float mWaitTime = 1f;
    [SerializeField]
    private GameObject[] PartyUnitSlots = new GameObject[5];
    public const int PINID_REFRESH_MAP = 100;
    public const int PINID_REFRESH_PARTY = 110;
    private const float UPDATE_WAIT_TIME = 1f;
    private long mEndTime;
    [SerializeField]
    private GameObject PartyInfo;
    [SerializeField]
    private GameObject PartyUnitLeader;
    [SerializeField]
    private Text SeasonDateText;
    [SerializeField]
    private Text SeasonTimeText;
    [Space(10f)]
    [SerializeField]
    private GameObject GoMapInfo;
    [SerializeField]
    private Text TextMapInfoSchedule;
    [SerializeField]
    private Text NextOpenDate;
    [SerializeField]
    private Text NextOpenTime;
    [SerializeField]
    private Text RemainTime;
    [SerializeField]
    private Text StreakWin;
    [SerializeField]
    private GameObject AwardItem;
    private bool mIsUpdateMapInfoEndAt;
    private float mPassedTimeMapInfoEndAt;

    private void Start()
    {
      this.RefreshParty();
      this.mEndTime = MonoSingleton<GameManager>.Instance.RankMatchExpiredTime;
      this.CountDown();
    }

    private void Update()
    {
      this.mWaitTime -= Time.deltaTime;
      if ((double) this.mWaitTime < 0.0)
        this.CountDown();
      this.UpdateMapInfoEndAt();
    }

    private void CountDown()
    {
      if (this.mEndTime <= 0L)
        return;
      DateTime dateTime = TimeManager.FromUnixTime(this.mEndTime);
      DateTime serverTime = TimeManager.ServerTime;
      if (serverTime > dateTime)
        return;
      TimeSpan timeSpan = dateTime - serverTime;
      if ((UnityEngine.Object) this.RemainTime != (UnityEngine.Object) null)
      {
        if (timeSpan.TotalDays >= 1.0)
          this.RemainTime.text = LocalizedText.Get("sys.MULTI_VERSUS_REMAIN_AT_DAY", new object[1]
          {
            (object) timeSpan.Days
          });
        else if (timeSpan.TotalHours >= 1.0)
          this.RemainTime.text = LocalizedText.Get("sys.MULTI_VERSUS_REMAIN_AT_HOUR", new object[1]
          {
            (object) timeSpan.Hours
          });
        else
          this.RemainTime.text = LocalizedText.Get("sys.MULTI_VERSUS_REMAIN_AT_MINUTE", new object[1]
          {
            (object) Mathf.Max(timeSpan.Minutes, 0)
          });
      }
      this.mWaitTime = 1f;
    }

    public void Activated(int pinID)
    {
      if (pinID != 100)
      {
        if (pinID != 110)
          return;
        this.RefreshParty();
      }
      else
        this.RefreshMap();
    }

    private void RefreshParty()
    {
      int lastSelectionIndex;
      PartyEditData loadTeamPreset = PartyUtility.LoadTeamPresets(PlayerPartyTypes.RankMatch, out lastSelectionIndex, false)[lastSelectionIndex];
      VersusRankParam versusRankParam = MonoSingleton<GameManager>.Instance.GetVersusRankParam(MonoSingleton<GameManager>.Instance.RankMatchScheduleId);
      if (versusRankParam == null)
        return;
      for (int index = 0; index < this.PartyUnitSlots.Length && index < loadTeamPreset.PartyData.VSWAITMEMBER_START; ++index)
      {
        if (index + 1 <= loadTeamPreset.Units.Length && loadTeamPreset.Units[index] != null)
        {
          UnitData unitData1 = loadTeamPreset.Units[index];
          if (unitData1.GetJobFor(PlayerPartyTypes.RankMatch) != unitData1.CurrentJob)
          {
            UnitData unitData2 = new UnitData();
            unitData2.TempFlags |= UnitData.TemporaryFlags.TemporaryUnitData;
            unitData2.Setup(unitData1);
            unitData2.SetJob(PlayerPartyTypes.RankMatch);
            unitData1 = unitData2;
          }
          unitData1.TempFlags |= UnitData.TemporaryFlags.AllowJobChange;
          if (index == 0)
          {
            DataSource.Bind<UnitData>(this.PartyUnitLeader, unitData1, false);
            GameParameter.UpdateAll(this.PartyUnitLeader);
          }
          DataSource.Bind<UnitData>(this.PartyUnitSlots[index], unitData1, false);
          GameParameter.UpdateAll(this.PartyUnitSlots[index]);
        }
      }
      if ((UnityEngine.Object) this.PartyInfo != (UnityEngine.Object) null)
      {
        DataSource.Bind<PartyData>(this.PartyInfo, loadTeamPreset.PartyData, false);
        GameParameter.UpdateAll(this.PartyInfo);
      }
      DataSource.Bind<PlayerPartyTypes>(this.gameObject, PlayerPartyTypes.RankMatch, false);
      PlayerData player = MonoSingleton<GameManager>.Instance.Player;
      if ((UnityEngine.Object) this.AwardItem != (UnityEngine.Object) null)
        DataSource.Bind<PlayerData>(this.AwardItem, player, false);
      if ((UnityEngine.Object) this.StreakWin != (UnityEngine.Object) null)
      {
        if (player.RankMatchStreakWin > 1)
          this.StreakWin.text = player.RankMatchStreakWin.ToString();
        else
          this.StreakWin.transform.parent.gameObject.SetActive(false);
      }
      if ((UnityEngine.Object) this.NextOpenDate != (UnityEngine.Object) null)
      {
        if (MonoSingleton<GameManager>.Instance.RankMatchNextTime == 0L)
        {
          this.NextOpenDate.gameObject.SetActive(false);
        }
        else
        {
          DateTime dateTime = TimeManager.FromUnixTime(MonoSingleton<GameManager>.Instance.RankMatchNextTime);
          this.NextOpenDate.gameObject.SetActive(true);
          this.NextOpenDate.text = dateTime.ToString("MM/dd");
        }
      }
      if ((UnityEngine.Object) this.NextOpenTime != (UnityEngine.Object) null)
        this.NextOpenTime.text = MonoSingleton<GameManager>.Instance.RankMatchNextTime != 0L ? TimeManager.FromUnixTime(MonoSingleton<GameManager>.Instance.RankMatchNextTime).ToString("HH:mm") : "--";
      if ((UnityEngine.Object) this.SeasonDateText != (UnityEngine.Object) null)
        this.SeasonDateText.text = versusRankParam.EndAt.ToString("MM/dd");
      if ((UnityEngine.Object) this.SeasonTimeText != (UnityEngine.Object) null)
        this.SeasonTimeText.text = versusRankParam.EndAt.ToString("HH:mm");
      MultiPlayVersusEdit component = this.GetComponent<MultiPlayVersusEdit>();
      if (!((UnityEngine.Object) component != (UnityEngine.Object) null))
        return;
      component.Set();
    }

    private void RefreshMap()
    {
      if (!(bool) ((UnityEngine.Object) this.GoMapInfo))
        return;
      GameManager instance = MonoSingleton<GameManager>.Instance;
      PlayerData player = instance.Player;
      if ((bool) ((UnityEngine.Object) instance) && player != null)
      {
        DataSource component = this.GoMapInfo.GetComponent<DataSource>();
        if ((bool) ((UnityEngine.Object) component))
          component.Clear();
        DataSource.Bind<QuestParam>(this.GoMapInfo, instance.FindQuest(GlobalVars.SelectedQuestID), false);
        GameParameter.UpdateAll(this.GoMapInfo);
        this.mIsUpdateMapInfoEndAt = this.RefreshMapInfoEndAt();
      }
      List<VersusEnableTimeScheduleParam> versusRankMapSchedule = instance.GetVersusRankMapSchedule(instance.RankMatchScheduleId);
      if (versusRankMapSchedule == null)
        return;
      List<VersusEnableTimeScheduleParam> timeScheduleParamList = new List<VersusEnableTimeScheduleParam>();
      int num1 = TimeManager.ServerTime.Year * 10000 + TimeManager.ServerTime.Month * 100 + TimeManager.ServerTime.Day;
      foreach (VersusEnableTimeScheduleParam timeScheduleParam in versusRankMapSchedule)
      {
        if (timeScheduleParam.AddDateList == null || timeScheduleParam.AddDateList.Count == 0)
        {
          timeScheduleParamList.Add(timeScheduleParam);
        }
        else
        {
          foreach (DateTime addDate in timeScheduleParam.AddDateList)
          {
            int num2 = addDate.Year * 10000 + addDate.Month * 100 + addDate.Day;
            if (num1 == num2)
              timeScheduleParamList.Add(timeScheduleParam);
          }
        }
      }
      bool flag = false;
      int num3 = TimeManager.ServerTime.Hour * 100 + TimeManager.ServerTime.Minute;
      foreach (VersusEnableTimeScheduleParam timeScheduleParam in timeScheduleParamList)
      {
        DateTime dateTime1 = DateTime.Parse(TimeManager.ServerTime.ToShortDateString() + " " + timeScheduleParam.Begin + ":00");
        TimeSpan timeSpan = TimeSpan.Parse(timeScheduleParam.Open);
        DateTime dateTime2 = dateTime1 + timeSpan;
        int num2 = dateTime1.Hour * 100 + dateTime1.Minute;
        int num4 = dateTime2.Hour * 100 + dateTime2.Minute;
        if (num2 <= num3 && num3 < num4)
        {
          this.TextMapInfoSchedule.text = dateTime1.ToString("HH:mm") + "-" + dateTime2.ToString("HH:mm");
          flag = true;
          break;
        }
      }
      if (flag)
        return;
      VersusEnableTimeScheduleParam timeScheduleParam1 = (VersusEnableTimeScheduleParam) null;
      foreach (VersusEnableTimeScheduleParam timeScheduleParam2 in timeScheduleParamList)
      {
        DateTime dateTime = DateTime.Parse(TimeManager.ServerTime.ToShortDateString() + " " + timeScheduleParam2.Begin + ":00");
        if (dateTime.Hour * 100 + dateTime.Minute > num3)
        {
          timeScheduleParam1 = timeScheduleParam2;
          break;
        }
      }
      if (timeScheduleParam1 == null)
      {
        DateTime serverTime = TimeManager.ServerTime;
        serverTime.AddDays(1.0);
        int num2 = serverTime.Year * 10000 + serverTime.Month * 100 + serverTime.Day;
        foreach (VersusEnableTimeScheduleParam timeScheduleParam2 in versusRankMapSchedule)
        {
          if (timeScheduleParam2.AddDateList == null || timeScheduleParam2.AddDateList.Count == 0)
          {
            timeScheduleParam1 = timeScheduleParam2;
            break;
          }
          foreach (DateTime addDate in timeScheduleParam2.AddDateList)
          {
            int num4 = addDate.Year * 10000 + addDate.Month * 100 + addDate.Day;
            if (num2 == num4)
            {
              timeScheduleParam1 = timeScheduleParam2;
              break;
            }
          }
        }
      }
      if (timeScheduleParam1 == null)
        return;
      DateTime dateTime3 = DateTime.Parse(TimeManager.ServerTime.ToShortDateString() + " " + timeScheduleParam1.Begin + ":00");
      TimeSpan timeSpan1 = TimeSpan.Parse(timeScheduleParam1.Open);
      DateTime dateTime4 = dateTime3 + timeSpan1;
      this.TextMapInfoSchedule.text = dateTime3.ToString("HH:mm") + "-" + dateTime4.ToString("HH:mm");
    }

    private bool RefreshMapInfoEndAt()
    {
      GameManager instance = MonoSingleton<GameManager>.Instance;
      if (!(bool) ((UnityEngine.Object) instance))
        return false;
      PlayerData player = instance.Player;
      if (player == null)
        return false;
      bool flag1 = false;
      DateTime serverTime = TimeManager.ServerTime;
      TimeSpan timeSpan = player.ArenaEndAt - serverTime;
      bool flag2 = player.ArenaEndAt > GameUtility.UnixtimeToLocalTime(0L);
      if (flag2 && timeSpan.TotalSeconds < 0.0)
      {
        flag2 = false;
        flag1 = true;
      }
      if (!flag2)
      {
        if (flag1)
          FlowNode_TriggerLocalEvent.TriggerLocalEvent((Component) this, "REFRESH_ARENA_INFO");
        return false;
      }
      string str1 = "sys.ARENA_TIMELIMIT_";
      string empty = string.Empty;
      string str2;
      if (timeSpan.Days != 0)
        str2 = LocalizedText.Get(str1 + "D", new object[1]
        {
          (object) timeSpan.Days
        });
      else if (timeSpan.Hours != 0)
        str2 = LocalizedText.Get(str1 + "H", new object[1]
        {
          (object) timeSpan.Hours
        });
      else
        str2 = LocalizedText.Get(str1 + "M", new object[1]
        {
          (object) Mathf.Max(timeSpan.Minutes, 0)
        });
      if ((bool) ((UnityEngine.Object) this.TextMapInfoSchedule) && this.TextMapInfoSchedule.text != str2)
        this.TextMapInfoSchedule.text = str2;
      this.mPassedTimeMapInfoEndAt = 1f;
      return true;
    }

    private void UpdateMapInfoEndAt()
    {
      if (!this.mIsUpdateMapInfoEndAt)
        return;
      if ((double) this.mPassedTimeMapInfoEndAt > 0.0)
      {
        this.mPassedTimeMapInfoEndAt -= Time.fixedDeltaTime;
        if ((double) this.mPassedTimeMapInfoEndAt > 0.0)
          return;
      }
      this.mIsUpdateMapInfoEndAt = this.RefreshMapInfoEndAt();
    }
  }
}
