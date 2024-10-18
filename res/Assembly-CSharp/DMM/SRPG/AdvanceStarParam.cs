// Decompiled with JetBrains decompiler
// Type: SRPG.AdvanceStarParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System.Collections.Generic;

#nullable disable
namespace SRPG
{
  public class AdvanceStarParam
  {
    private string mIname;
    private AdvanceStarRewardParam[] mStars;

    public string Iname => this.mIname;

    public List<AdvanceStarRewardParam> StarList
    {
      get
      {
        return this.mStars != null ? new List<AdvanceStarRewardParam>((IEnumerable<AdvanceStarRewardParam>) this.mStars) : new List<AdvanceStarRewardParam>();
      }
    }

    public void Deserialize(JSON_AdvanceStarParam json)
    {
      if (json == null)
        return;
      this.mIname = json.iname;
      this.mStars = (AdvanceStarRewardParam[]) null;
      if (json.stars == null || json.stars.Length == 0)
        return;
      this.mStars = new AdvanceStarRewardParam[json.stars.Length];
      for (int index = 0; index < json.stars.Length; ++index)
      {
        this.mStars[index] = new AdvanceStarRewardParam();
        this.mStars[index].Deserialize(json.stars[index]);
      }
    }

    public static void Deserialize(ref List<AdvanceStarParam> list, JSON_AdvanceStarParam[] json)
    {
      if (json == null)
        return;
      if (list == null)
        list = new List<AdvanceStarParam>(json.Length);
      list.Clear();
      for (int index = 0; index < json.Length; ++index)
      {
        AdvanceStarParam advanceStarParam = new AdvanceStarParam();
        advanceStarParam.Deserialize(json[index]);
        list.Add(advanceStarParam);
      }
    }
  }
}
