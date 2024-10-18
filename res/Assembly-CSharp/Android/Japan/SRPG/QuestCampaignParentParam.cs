﻿// Decompiled with JetBrains decompiler
// Type: SRPG.QuestCampaignParentParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using System;

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

    public bool IsAvailablePeriod(DateTime now)
    {
      return !(now < this.beginAt) && !(this.endAt < now);
    }
  }
}
