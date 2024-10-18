// Decompiled with JetBrains decompiler
// Type: SRPG.GenesisRewardParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;

namespace SRPG
{
  public class GenesisRewardParam
  {
    private string mIname;
    private GenesisRewardDataParam[] mRewards;

    public string Iname
    {
      get
      {
        return this.mIname;
      }
    }

    public List<GenesisRewardDataParam> RewardList
    {
      get
      {
        if (this.mRewards != null)
          return new List<GenesisRewardDataParam>((IEnumerable<GenesisRewardDataParam>) this.mRewards);
        return new List<GenesisRewardDataParam>();
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

    public static void Deserialize(ref List<GenesisRewardParam> list, JSON_GenesisRewardParam[] json)
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
