// Decompiled with JetBrains decompiler
// Type: SRPG.QuestCampaignParentParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;

#nullable disable
namespace SRPG
{
  public class QuestCampaignParentParam
  {
    public string iname;
    public DateTime beginAt;
    public DateTime endAt;
    public string[] children;

    public bool Deserialize(JSON_QuestCampaignParentParam json)
    {
      this.iname = json.iname;
      this.children = json.children;
      this.beginAt = DateTime.MinValue;
      this.endAt = DateTime.MaxValue;
      if (!string.IsNullOrEmpty(json.begin_at))
        DateTime.TryParse(json.begin_at, out this.beginAt);
      if (!string.IsNullOrEmpty(json.end_at))
        DateTime.TryParse(json.end_at, out this.endAt);
      return true;
    }

    public bool IsChild(string childId)
    {
      if (this.children != null)
      {
        foreach (string child in this.children)
        {
          if (child == childId)
            return true;
        }
      }
      return false;
    }

    public bool IsAvailablePeriod(DateTime now) => !(now < this.beginAt) && !(this.endAt < now);
  }
}
