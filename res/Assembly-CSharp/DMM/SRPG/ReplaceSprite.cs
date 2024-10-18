// Decompiled with JetBrains decompiler
// Type: SRPG.ReplaceSprite
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using MessagePack;
using System.Collections.Generic;

#nullable disable
namespace SRPG
{
  [MessagePackObject(true)]
  public class ReplaceSprite
  {
    public string mIname;
    public List<ReplacePeriod> mPeriod = new List<ReplacePeriod>();

    public static void Deserialize(ref List<ReplaceSprite> ref_params, JSON_ReplaceSprite[] json)
    {
      if (ref_params == null)
        ref_params = new List<ReplaceSprite>();
      ref_params.Clear();
      if (json == null)
        return;
      foreach (JSON_ReplaceSprite json1 in json)
      {
        ReplaceSprite replaceSprite = new ReplaceSprite();
        replaceSprite.Deserialize(json1);
        ref_params.Add(replaceSprite);
      }
    }

    public void Deserialize(JSON_ReplaceSprite json)
    {
      if (json == null || json.periods == null)
        return;
      this.mIname = json.iname;
      foreach (JSON_ReplacePeriod period in json.periods)
      {
        ReplacePeriod replacePeriod = new ReplacePeriod();
        replacePeriod.Deserialize(period);
        this.mPeriod.Add(replacePeriod);
      }
    }
  }
}
