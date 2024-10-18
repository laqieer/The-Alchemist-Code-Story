// Decompiled with JetBrains decompiler
// Type: SRPG.AdvanceRewardParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System.Collections.Generic;

#nullable disable
namespace SRPG
{
  public class AdvanceRewardParam
  {
    private string mIname;
    private AdvanceRewardDataParam[] mRewards;

    public string Iname => this.mIname;

    public List<AdvanceRewardDataParam> RewardList
    {
      get
      {
        return this.mRewards != null ? new List<AdvanceRewardDataParam>((IEnumerable<AdvanceRewardDataParam>) this.mRewards) : new List<AdvanceRewardDataParam>();
      }
    }

    public void Deserialize(JSON_AdvanceRewardParam json)
    {
      if (json == null)
        return;
      this.mIname = json.iname;
      this.mRewards = (AdvanceRewardDataParam[]) null;
      if (json.rewards == null || json.rewards.Length == 0)
        return;
      this.mRewards = new AdvanceRewardDataParam[json.rewards.Length];
      for (int index = 0; index < json.rewards.Length; ++index)
      {
        this.mRewards[index] = new AdvanceRewardDataParam();
        this.mRewards[index].Deserialize(json.rewards[index]);
      }
    }

    public static void Deserialize(
      ref List<AdvanceRewardParam> list,
      JSON_AdvanceRewardParam[] json)
    {
      if (json == null)
        return;
      if (list == null)
        list = new List<AdvanceRewardParam>(json.Length);
      list.Clear();
      for (int index = 0; index < json.Length; ++index)
      {
        AdvanceRewardParam advanceRewardParam = new AdvanceRewardParam();
        advanceRewardParam.Deserialize(json[index]);
        list.Add(advanceRewardParam);
      }
    }
  }
}
