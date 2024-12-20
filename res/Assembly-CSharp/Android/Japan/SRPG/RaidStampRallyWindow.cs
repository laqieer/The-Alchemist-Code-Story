﻿// Decompiled with JetBrains decompiler
// Type: SRPG.RaidStampRallyWindow
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
  [FlowNode.Pin(1, "初期化", FlowNode.PinTypes.Input, 1)]
  public class RaidStampRallyWindow : MonoBehaviour, IFlowInterface
  {
    private static readonly string RAID_BOSS_ALIVE_OBJECT_KEY = "RaidBossAlive";
    private static readonly string RAID_BOSS_DEAD_OBJECT_KEY = "RaidBossDead";
    private List<GameObject> mCreatedRaidBossIconList = new List<GameObject>();
    private List<GameObject> mCreatedRewardIconList = new List<GameObject>();
    private const int PIN_INPUT_INIT = 1;
    private const int PIN_OUTPUT_END_RAID_BEAT_LIST = 101;
    [SerializeField]
    private GameObject mRaidIconTemplate;
    [SerializeField]
    private RaidRewardIcon mRaidRewardIconTemplate;
    [SerializeField]
    private Transform mRaidRewardIconParent;
    [SerializeField]
    private SRPG_Button mRewardButton;
    [SerializeField]
    private Text mRoundText;
    private static RaidStampRallyWindow mInstance;
    private ReqRaidBeatList.Response mRaidBeatListData;
    private bool mIsAllRaidBossBeat;
    private bool mIsExistReward;

    public static RaidStampRallyWindow Instance
    {
      get
      {
        return RaidStampRallyWindow.mInstance;
      }
    }

    private void Awake()
    {
      RaidStampRallyWindow.mInstance = this;
    }

    public void Activated(int pinID)
    {
      if (pinID != 1)
        return;
      this.Init();
    }

    public void SetParam(ReqRaidBeatList.Response beat_data)
    {
      this.mRaidBeatListData = beat_data;
    }

    private void Init()
    {
      this.Init_RaidBossList();
      this.Init_Reward();
      this.mRewardButton.interactable = this.mIsAllRaidBossBeat && this.mIsExistReward;
      if (!((UnityEngine.Object) this.mRoundText != (UnityEngine.Object) null))
        return;
      this.mRoundText.text = this.mRaidBeatListData.round.ToString();
    }

    private void Init_RaidBossList()
    {
      for (int index = 0; index < this.mCreatedRaidBossIconList.Count; ++index)
        UnityEngine.Object.Destroy((UnityEngine.Object) this.mCreatedRaidBossIconList[index]);
      this.mCreatedRaidBossIconList.Clear();
      this.mIsAllRaidBossBeat = false;
      if ((UnityEngine.Object) RaidManager.Instance == (UnityEngine.Object) null)
        return;
      List<RaidBossParam> raid_boss_list = RaidStampRallyWindow.GetRaidBossAll(RaidManager.Instance.RaidPeriodId);
      this.mIsAllRaidBossBeat = true;
      for (int i = 0; i < raid_boss_list.Count; ++i)
      {
        this.mRaidIconTemplate.SetActive(false);
        UnitParam unitParam = MonoSingleton<GameManager>.Instance.MasterParam.GetUnitParam(raid_boss_list[i].UnitIName);
        if (unitParam != null)
        {
          GameObject gameObject1 = UnityEngine.Object.Instantiate<GameObject>(this.mRaidIconTemplate);
          gameObject1.transform.SetParent(this.mRaidIconTemplate.transform.parent, false);
          gameObject1.SetActive(true);
          this.mCreatedRaidBossIconList.Add(gameObject1);
          if (this.mRaidBeatListData != null && this.mRaidBeatListData.beat_stamp_index != null)
          {
            SerializeValueBehaviour component = gameObject1.GetComponent<SerializeValueBehaviour>();
            if ((UnityEngine.Object) component != (UnityEngine.Object) null)
            {
              bool active = Array.FindIndex<int>(this.mRaidBeatListData.beat_stamp_index, (Predicate<int>) (iname => iname == raid_boss_list[i].StampIndex)) >= 0;
              GameObject gameObject2 = component.list.GetGameObject(RaidStampRallyWindow.RAID_BOSS_ALIVE_OBJECT_KEY);
              GameObject gameObject3 = component.list.GetGameObject(RaidStampRallyWindow.RAID_BOSS_DEAD_OBJECT_KEY);
              GameUtility.SetGameObjectActive(gameObject2, !active);
              GameUtility.SetGameObjectActive(gameObject3, active);
              if (!active)
                this.mIsAllRaidBossBeat = false;
            }
          }
          else
            this.mIsAllRaidBossBeat = false;
          DataSource.Bind<UnitParam>(gameObject1, unitParam, false);
        }
      }
    }

    private void Init_Reward()
    {
      for (int index = 0; index < this.mCreatedRewardIconList.Count; ++index)
        UnityEngine.Object.Destroy((UnityEngine.Object) this.mCreatedRewardIconList[index]);
      this.mCreatedRewardIconList.Clear();
      this.mIsExistReward = false;
      if ((UnityEngine.Object) this.mRaidRewardIconTemplate == (UnityEngine.Object) null)
        return;
      this.mRaidRewardIconTemplate.gameObject.SetActive(false);
      RaidPeriodParam raidPeriod = MonoSingleton<GameManager>.Instance.MasterParam.GetRaidPeriod(RaidManager.Instance.RaidPeriodId);
      if (raidPeriod == null)
        return;
      RaidCompleteRewardParam raidCompleteReward = MonoSingleton<GameManager>.Instance.MasterParam.GetRaidCompleteReward(raidPeriod.CompleteRewardId);
      if (raidCompleteReward == null)
        return;
      string reward_id = string.Empty;
      int round = 0;
      raidCompleteReward.Rewards.ForEach((Action<RaidCompleteRewardDataParam>) (reward =>
      {
        if (round >= reward.Round || reward.Round > this.mRaidBeatListData.round)
          return;
        round = reward.Round;
        reward_id = reward.RewardId;
      }));
      List<RaidReward> raidRewardList = MonoSingleton<GameManager>.Instance.MasterParam.GetRaidRewardList(reward_id);
      for (int index = 0; index < raidRewardList.Count; ++index)
      {
        RaidRewardIcon raidRewardIcon = UnityEngine.Object.Instantiate<RaidRewardIcon>(this.mRaidRewardIconTemplate);
        raidRewardIcon.transform.SetParent(this.mRaidRewardIconParent, false);
        raidRewardIcon.gameObject.SetActive(true);
        raidRewardIcon.Initialize(raidRewardList[index]);
        this.mCreatedRewardIconList.Add(raidRewardIcon.gameObject);
      }
      if (raidRewardList.Count <= 0)
        return;
      this.mIsExistReward = true;
    }

    public static List<RaidBossParam> GetRaidBossAll(int period_id)
    {
      List<RaidBossParam> raidBossParamList = new List<RaidBossParam>();
      List<RaidBossParam> all_raid_boss_list = MonoSingleton<GameManager>.Instance.MasterParam.GetRaidBossAll(period_id);
      for (int i = 0; i < all_raid_boss_list.Count; ++i)
      {
        if (raidBossParamList.FindIndex((Predicate<RaidBossParam>) (boss => boss.StampIndex == all_raid_boss_list[i].StampIndex)) < 0)
          raidBossParamList.Add(all_raid_boss_list[i]);
      }
      return raidBossParamList;
    }
  }
}
