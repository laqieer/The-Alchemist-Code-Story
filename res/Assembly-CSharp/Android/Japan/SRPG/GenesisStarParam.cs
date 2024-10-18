// Decompiled with JetBrains decompiler
// Type: SRPG.GenesisStarParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;

namespace SRPG
{
  public class GenesisStarParam
  {
    private string mIname;
    private GenesisStarRewardParam[] mStars;

    public string Iname
    {
      get
      {
        return this.mIname;
      }
    }

    public List<GenesisStarRewardParam> StarList
    {
      get
      {
        if (this.mStars != null)
          return new List<GenesisStarRewardParam>((IEnumerable<GenesisStarRewardParam>) this.mStars);
        return new List<GenesisStarRewardParam>();
      }
    }

    public void Deserialize(JSON_GenesisStarParam json)
    {
      if (json == null)
        return;
      this.mIname = json.iname;
      this.mStars = (GenesisStarRewardParam[]) null;
      if (json.stars == null || json.stars.Length == 0)
        return;
      this.mStars = new GenesisStarRewardParam[json.stars.Length];
      for (int index = 0; index < json.stars.Length; ++index)
      {
        this.mStars[index] = new GenesisStarRewardParam();
        this.mStars[index].Deserialize(json.stars[index]);
      }
    }

    public static void Deserialize(ref List<GenesisStarParam> list, JSON_GenesisStarParam[] json)
    {
      if (json == null)
        return;
      if (list == null)
        list = new List<GenesisStarParam>(json.Length);
      list.Clear();
      for (int index = 0; index < json.Length; ++index)
      {
        GenesisStarParam genesisStarParam = new GenesisStarParam();
        genesisStarParam.Deserialize(json[index]);
        list.Add(genesisStarParam);
      }
    }
  }
}
