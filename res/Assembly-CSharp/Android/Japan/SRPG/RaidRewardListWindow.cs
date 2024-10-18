// Decompiled with JetBrains decompiler
// Type: SRPG.RaidRewardListWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;
using System.Collections.Generic;
using UnityEngine;

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
      if ((UnityEngine.Object) this.mRaidRewardIconTemplate == (UnityEngine.Object) null)
        return;
      this.mRaidRewardIconTemplate.gameObject.SetActive(false);
      if ((UnityEngine.Object) RaidManager.Instance == (UnityEngine.Object) null || RaidManager.Instance.GetSelectedRaidBoss() == null)
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
        RaidRewardIcon raidRewardIcon = UnityEngine.Object.Instantiate<RaidRewardIcon>(this.mRaidRewardIconTemplate);
        raidRewardIcon.transform.SetParent(this.mRaidRewardIconParent, false);
        raidRewardIcon.gameObject.SetActive(true);
        raidRewardIcon.Initialize(raidRewardList[index]);
      }
    }
  }
}
