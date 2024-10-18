// Decompiled with JetBrains decompiler
// Type: SRPG.RaidRewardListWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
namespace SRPG
{
  public class RaidRewardListWindow : MonoBehaviour
  {
    [SerializeField]
    private RaidRewardIcon mRaidRewardIconTemplate;
    [SerializeField]
    private Transform mRaidRewardIconParent;

    private void Start()
    {
      if (Object.op_Equality((Object) this.mRaidRewardIconTemplate, (Object) null))
        return;
      ((Component) this.mRaidRewardIconTemplate).gameObject.SetActive(false);
      if (Object.op_Equality((Object) RaidManager.Instance, (Object) null) || RaidManager.Instance.GetSelectedRaidBoss() == null)
        return;
      int beat_reward_id = 0;
      int round = 0;
      switch (RaidManager.Instance.SelectedRaidOwnerType)
      {
        case RaidManager.RaidOwnerType.Self:
          beat_reward_id = RaidManager.Instance.CurrentRaidBossData.RaidBossInfo.RaidBossParam.BeatRewardId;
          round = RaidManager.Instance.CurrentRaidBossData.RaidBossInfo.Round;
          break;
        case RaidManager.RaidOwnerType.Rescue:
          beat_reward_id = RaidManager.Instance.RescueRaidBossData.RaidBossInfo.RaidBossParam.BeatRewardId;
          round = RaidManager.Instance.RescueRaidBossData.RaidBossInfo.Round;
          break;
        case RaidManager.RaidOwnerType.Rescue_Temp:
          beat_reward_id = MonoSingleton<GameManager>.Instance.MasterParam.GetRaidBoss(RaidManager.Instance.SelectedRaidRescueMember.BossId).BeatRewardId;
          round = RaidManager.Instance.SelectedRaidRescueMember.Round;
          break;
        case RaidManager.RaidOwnerType.Self_Cleared:
          beat_reward_id = RaidManager.Instance.SelectedClearedRaidBossInfo.RaidBossParam.BeatRewardId;
          round = RaidManager.Instance.CurrentRound;
          break;
      }
      string raidBeatReward = MonoSingleton<GameManager>.Instance.MasterParam.GetRaidBeatReward(beat_reward_id, round);
      if (string.IsNullOrEmpty(raidBeatReward))
        return;
      List<RaidReward> raidRewardList = MonoSingleton<GameManager>.Instance.MasterParam.GetRaidRewardList(raidBeatReward);
      for (int index = 0; index < raidRewardList.Count; ++index)
      {
        RaidRewardIcon raidRewardIcon = Object.Instantiate<RaidRewardIcon>(this.mRaidRewardIconTemplate);
        ((Component) raidRewardIcon).transform.SetParent(this.mRaidRewardIconParent, false);
        ((Component) raidRewardIcon).gameObject.SetActive(true);
        raidRewardIcon.Initialize(raidRewardList[index]);
      }
    }
  }
}
