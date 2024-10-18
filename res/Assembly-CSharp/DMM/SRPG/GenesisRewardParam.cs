// Decompiled with JetBrains decompiler
// Type: SRPG.GenesisRewardParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System.Collections.Generic;

#nullable disable
namespace SRPG
{
  public class GenesisRewardParam
  {
    private string mIname;
    private GenesisRewardDataParam[] mRewards;

    public string Iname => this.mIname;

    public List<GenesisRewardDataParam> RewardList
    {
      get
      {
        return this.mRewards != null ? new List<GenesisRewardDataParam>((IEnumerable<GenesisRewardDataParam>) this.mRewards) : new List<GenesisRewardDataParam>();
      }
    }

    public void Deserialize(JSON_GenesisRewardParam json)
    {
      if (json == null)
        return;
      this.mIname = json.iname;
      this.mRewards = (GenesisRewardDataParam[]) null;
      if (json.rewards == null || json.rewards.Length == 0)
        return;
      this.mRewards = new GenesisRewardDataParam[json.rewards.Length];
      for (int index = 0; index < json.rewards.Length; ++index)
      {
        this.mRewards[index] = new GenesisRewardDataParam();
        this.mRewards[index].Deserialize(json.rewards[index]);
      }
    }

    public static void Deserialize(
      ref List<GenesisRewardParam> list,
      JSON_GenesisRewardParam[] json)
    {
      if (json == null)
        return;
      if (list == null)
        list = new List<GenesisRewardParam>(json.Length);
      list.Clear();
      for (int index = 0; index < json.Length; ++index)
      {
        GenesisRewardParam genesisRewardParam = new GenesisRewardParam();
        genesisRewardParam.Deserialize(json[index]);
        list.Add(genesisRewardParam);
      }
    }
  }
}
