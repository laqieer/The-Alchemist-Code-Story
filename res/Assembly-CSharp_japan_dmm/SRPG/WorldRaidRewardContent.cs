// Decompiled with JetBrains decompiler
// Type: SRPG.WorldRaidRewardContent
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  public class WorldRaidRewardContent : MonoBehaviour
  {
    [SerializeField]
    private ImageArray mRankingImages;
    [SerializeField]
    private GameObject mRankingSingleObj;
    [SerializeField]
    private Text mRankingSingleTxt;
    [SerializeField]
    private GameObject mRankingMultiObj;
    [SerializeField]
    private Text mRankingMultiStartTxt;
    [SerializeField]
    private Text mRankingMultiEndTxt;
    [SerializeField]
    private Transform mRaidRewardIconRoot;
    [SerializeField]
    private RaidRewardIcon mRaidRewardIcon_ItemTemplate;
    private List<GameObject> mRewardObjs = new List<GameObject>();

    public void Setup(WorldRaidRankingRewardParam.Reward reward_data)
    {
      if (Object.op_Inequality((Object) this.mRankingImages, (Object) null))
      {
        this.mRankingImages.ImageIndex = Mathf.Clamp(reward_data.RankBegin - 1, 0, this.mRankingImages.Images.Length - 1);
        if (reward_data.RankBegin >= this.mRankingImages.Images.Length)
        {
          if (reward_data.RankBegin == reward_data.RankEnd)
          {
            if (Object.op_Inequality((Object) this.mRankingSingleTxt, (Object) null))
              this.mRankingSingleTxt.text = reward_data.RankBegin.ToString();
          }
          else
          {
            if (Object.op_Inequality((Object) this.mRankingMultiStartTxt, (Object) null))
              this.mRankingMultiStartTxt.text = reward_data.RankBegin.ToString();
            if (Object.op_Inequality((Object) this.mRankingMultiEndTxt, (Object) null))
              this.mRankingMultiEndTxt.text = reward_data.RankEnd.ToString();
          }
          if (Object.op_Inequality((Object) this.mRankingSingleObj, (Object) null))
            this.mRankingSingleObj.SetActive(reward_data.RankBegin == reward_data.RankEnd);
          if (Object.op_Inequality((Object) this.mRankingMultiObj, (Object) null))
            this.mRankingMultiObj.SetActive(reward_data.RankBegin != reward_data.RankEnd);
        }
      }
      foreach (Object mRewardObj in this.mRewardObjs)
        Object.Destroy(mRewardObj);
      this.mRewardObjs = new List<GameObject>();
      this.SetReward(reward_data.RewardParam.RewardList);
    }

    public void Setup(WorldRaidRewardParam param) => this.SetReward(param.RewardList);

    private void SetReward(List<WorldRaidRewardParam.Reward> list)
    {
      if (this.mRewardObjs != null && this.mRewardObjs.Count > 0)
      {
        foreach (Object mRewardObj in this.mRewardObjs)
          Object.Destroy(mRewardObj);
        this.mRewardObjs.Clear();
      }
      foreach (WorldRaidRewardParam.Reward reward in list)
      {
        RaidRewardIcon raidRewardIcon = Object.Instantiate<RaidRewardIcon>(this.mRaidRewardIcon_ItemTemplate, this.mRaidRewardIconRoot, false);
        if (!Object.op_Equality((Object) raidRewardIcon, (Object) null))
        {
          raidRewardIcon.Initialize(reward.ItemType, reward.ItemIname, reward.ItemNum);
          ((Component) raidRewardIcon).gameObject.SetActive(true);
          this.mRewardObjs.Add(((Component) raidRewardIcon).gameObject);
        }
      }
    }
  }
}
