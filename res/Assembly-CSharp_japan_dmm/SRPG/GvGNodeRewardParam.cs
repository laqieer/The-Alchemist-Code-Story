// Decompiled with JetBrains decompiler
// Type: SRPG.GvGNodeRewardParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System;
using System.Collections.Generic;

#nullable disable
namespace SRPG
{
  public class GvGNodeRewardParam : GvGMasterParam<JSON_GvGNodeRewardParam>
  {
    public string Id { get; private set; }

    public List<GvGNodeReward> Rewards { get; private set; }

    public override bool Deserialize(JSON_GvGNodeRewardParam json)
    {
      if (json == null)
        return false;
      this.Id = json.id;
      if (json.rewards == null)
        return false;
      this.Rewards = new List<GvGNodeReward>();
      for (int index = 0; index < json.rewards.Length; ++index)
        this.Rewards.Add(new GvGNodeReward()
        {
          league_id = json.rewards[index].league_id,
          reward = json.rewards[index].reward
        });
      return true;
    }

    public static GvGRewardParam GetGvGRewardParam(string id, string league)
    {
      GameManager instance = MonoSingleton<GameManager>.Instance;
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) instance, (UnityEngine.Object) null) || instance.mGvGNodeRewardParam == null)
        return (GvGRewardParam) null;
      GvGNodeRewardParam gnodeRewardParam = instance.mGvGNodeRewardParam.Find((Predicate<GvGNodeRewardParam>) (p => p.Id == id));
      if (gnodeRewardParam == null || gnodeRewardParam.Rewards == null)
        return (GvGRewardParam) null;
      GvGNodeReward gvGnodeReward = gnodeRewardParam.Rewards.Find((Predicate<GvGNodeReward>) (p => p.league_id == league));
      return gvGnodeReward == null ? (GvGRewardParam) null : GvGRewardParam.GetReward(gvGnodeReward.reward);
    }
  }
}
