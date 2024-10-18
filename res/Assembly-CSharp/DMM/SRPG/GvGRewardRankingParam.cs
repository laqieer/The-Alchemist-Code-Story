// Decompiled with JetBrains decompiler
// Type: SRPG.GvGRewardRankingParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System;
using System.Collections.Generic;

#nullable disable
namespace SRPG
{
  public class GvGRewardRankingParam : GvGMasterParam<JSON_GvGRewardRankingParam>
  {
    public string Id { get; private set; }

    public List<GvGRewardRankingDetailParam> RewardDetail { get; private set; }

    public override bool Deserialize(JSON_GvGRewardRankingParam json)
    {
      if (json == null)
        return false;
      this.Id = json.id;
      this.RewardDetail = new List<GvGRewardRankingDetailParam>();
      if (json.reward_detail != null)
      {
        for (int index = 0; index < json.reward_detail.Length; ++index)
        {
          GvGRewardRankingDetailParam rankingDetailParam = new GvGRewardRankingDetailParam();
          if (rankingDetailParam.Deserialize(json.reward_detail[index]))
            this.RewardDetail.Add(rankingDetailParam);
        }
      }
      return true;
    }

    public static GvGRewardRankingParam GetRewardRanking(string id)
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) MonoSingleton<GameManager>.Instance, (UnityEngine.Object) null) || string.IsNullOrEmpty(id))
        return (GvGRewardRankingParam) null;
      List<GvGRewardRankingParam> grewardRankingParam = MonoSingleton<GameManager>.Instance.mGvGRewardRankingParam;
      if (grewardRankingParam != null)
        return grewardRankingParam.Find((Predicate<GvGRewardRankingParam>) (r => r != null && r.Id == id));
      DebugUtility.Log("<color=yellow>QuestParam/mGvGRewardRankingParam no data!</color>");
      return (GvGRewardRankingParam) null;
    }
  }
}
