// Decompiled with JetBrains decompiler
// Type: SRPG.RaidRewardReceiveWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  public class RaidRewardReceiveWindow : MonoBehaviour
  {
    [SerializeField]
    private bool mRewardAllDraw;
    [SerializeField]
    private Text mTitleText;
    [SerializeField]
    private Text mMessageText;
    [SerializeField]
    private Transform mBaseRewardTransform;
    [SerializeField]
    private GameObject mBeatReward;
    [SerializeField]
    private GameObject mRatioReward;
    [SerializeField]
    private GameObject mAmountReward;
    [SerializeField]
    private RaidRewardIcon mRewardItem;

    private void Start()
    {
      RaidRewardData raidRewards = RaidManager.Instance.GetRaidRewards();
      if (raidRewards == null)
        return;
      Transform baseRewardTransform = this.mBaseRewardTransform;
      if (this.mRewardAllDraw)
      {
        this.mBeatReward.SetActive(false);
        this.mRatioReward.SetActive(false);
        this.mAmountReward.SetActive(false);
        while (raidRewards != null)
        {
          Transform component;
          if (raidRewards.Kind == RaidRewardKind.Beat && Object.op_Inequality((Object) this.mBeatReward, (Object) null))
          {
            component = this.mBeatReward.GetComponent<Transform>();
            if (raidRewards.Rewards.Length != 0)
              this.mBeatReward.SetActive(true);
          }
          else if (raidRewards.Kind == RaidRewardKind.DamageRatio && Object.op_Inequality((Object) this.mRatioReward, (Object) null))
          {
            component = this.mRatioReward.GetComponent<Transform>();
            if (raidRewards.Rewards.Length != 0)
              this.mRatioReward.SetActive(true);
          }
          else if (raidRewards.Kind == RaidRewardKind.DamageAmount && Object.op_Inequality((Object) this.mAmountReward, (Object) null))
          {
            component = this.mAmountReward.GetComponent<Transform>();
            if (raidRewards.Rewards.Length != 0)
              this.mAmountReward.SetActive(true);
          }
          else
          {
            raidRewards = RaidManager.Instance.GetRaidRewards();
            continue;
          }
          for (int index = 0; index < raidRewards.Rewards.Length; ++index)
          {
            RaidRewardIcon raidRewardIcon = Object.Instantiate<RaidRewardIcon>(this.mRewardItem, component);
            raidRewardIcon.Initialize(raidRewards.Rewards[index]);
            ((Component) raidRewardIcon).gameObject.SetActive(true);
          }
          raidRewards = RaidManager.Instance.GetRaidRewards();
        }
        if (!Object.op_Inequality((Object) this.mTitleText, (Object) null) || !Object.op_Inequality((Object) this.mMessageText, (Object) null))
          return;
        this.mTitleText.text = LocalizedText.Get("sys.RAID_REWARD_ALLRECEIVE_WINDOW_TITLE");
        this.mMessageText.text = LocalizedText.Get("sys.RAID_REWARD_ALLRECEIVE_WINDOW_MESSAGE");
      }
      else
      {
        for (int index = 0; index < raidRewards.Rewards.Length; ++index)
        {
          RaidRewardIcon raidRewardIcon = Object.Instantiate<RaidRewardIcon>(this.mRewardItem, baseRewardTransform);
          raidRewardIcon.Initialize(raidRewards.Rewards[index]);
          ((Component) raidRewardIcon).gameObject.SetActive(true);
        }
        if (!Object.op_Inequality((Object) this.mTitleText, (Object) null) || !Object.op_Inequality((Object) this.mMessageText, (Object) null))
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
          case RaidRewardKind.DamageAmount:
            str1 = "RAID_REWARD_RECEIVE_WINDOW_DAMAGE_AMOUNT_TITLE";
            str2 = "RAID_REWARD_RECEIVE_WINDOW_DAMAGE_AMOUNT_MESSAGE";
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
}
