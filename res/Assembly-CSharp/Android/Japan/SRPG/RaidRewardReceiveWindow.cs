// Decompiled with JetBrains decompiler
// Type: SRPG.RaidRewardReceiveWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  public class RaidRewardReceiveWindow : MonoBehaviour
  {
    [SerializeField]
    private Text mTitleText;
    [SerializeField]
    private Text mMessageText;
    [SerializeField]
    private Transform mRewardTransform;
    [SerializeField]
    private RaidRewardIcon mRewardItem;

    private void Start()
    {
      RaidRewardData raidRewards = RaidManager.Instance.GetRaidRewards();
      for (int index = 0; index < raidRewards.Rewards.Length; ++index)
      {
        RaidRewardIcon raidRewardIcon = UnityEngine.Object.Instantiate<RaidRewardIcon>(this.mRewardItem, this.mRewardTransform);
        raidRewardIcon.Initialize(raidRewards.Rewards[index]);
        raidRewardIcon.gameObject.SetActive(true);
      }
      if (!((UnityEngine.Object) this.mTitleText != (UnityEngine.Object) null) || !((UnityEngine.Object) this.mMessageText != (UnityEngine.Object) null))
        return;
      string str1 = string.Empty;
      string str2 = string.Empty;
      switch (raidRewards.Kind)
      {
        case RaidRewardKind.Beat:
          str1 = "RAID_REWARD_RECEIVE_WINDOW_BEAT_TITLE";
          str2 = "RAID_REWARD_RECEIVE_WINDOW_BEAT_MESSAGE";
          break;
        case RaidRewardKind.DamageRatio:
          str1 = "RAID_REWARD_RECEIVE_WINDOW_DAMAGE_RATIO_TITLE";
          str2 = "RAID_REWARD_RECEIVE_WINDOW_DAMAGE_RATIO_MESSAGE";
          break;
        case RaidRewardKind.AreaClear:
          str1 = "RAID_REWARD_RECEIVE_WINDOW_AREA_CLEAR_TITLE";
          str2 = "RAID_REWARD_RECEIVE_WINDOW_AREA_CLEAR_MESSAGE";
          break;
        case RaidRewardKind.Complete:
          str1 = "RAID_REWARD_RECEIVE_WINDOW_COMPLETE_TITLE";
          str2 = "RAID_REWARD_RECEIVE_WINDOW_COMPLETE_MESSAGE";
          break;
      }
      this.mTitleText.text = LocalizedText.Get("sys." + str1);
      this.mMessageText.text = LocalizedText.Get("sys." + str2);
    }
  }
}
