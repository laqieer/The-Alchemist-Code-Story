// Decompiled with JetBrains decompiler
// Type: SRPG.GvGRewardParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System;
using System.Collections.Generic;

#nullable disable
namespace SRPG
{
  public class GvGRewardParam : GvGMasterParam<JSON_GvGRewardParam>
  {
    public string Id { get; private set; }

    public List<GvGRewardDetailParam> Rewards { get; private set; }

    public override bool Deserialize(JSON_GvGRewardParam json)
    {
      if (json == null)
        return false;
      this.Id = json.id;
      this.Rewards = new List<GvGRewardDetailParam>();
      if (json.rewards != null)
      {
        for (int index = 0; index < json.rewards.Length; ++index)
        {
          GvGRewardDetailParam grewardDetailParam = new GvGRewardDetailParam();
          if (grewardDetailParam.Deserialize(json.rewards[index]))
            this.Rewards.Add(grewardDetailParam);
        }
      }
      return true;
    }

    public static GvGRewardParam GetReward(string id)
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) MonoSingleton<GameManager>.Instance, (UnityEngine.Object) null))
        return (GvGRewardParam) null;
      List<GvGRewardParam> mGvGrewardParam = MonoSingleton<GameManager>.Instance.mGvGRewardParam;
      if (mGvGrewardParam != null)
        return mGvGrewardParam.Find((Predicate<GvGRewardParam>) (r => r != null && r.Id == id));
      DebugUtility.Log("<color=yellow>QuestParam/mGvGRewardParam no data!</color>");
      return (GvGRewardParam) null;
    }
  }
}
