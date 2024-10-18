// Decompiled with JetBrains decompiler
// Type: SRPG.SimpleDropTableParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;

#nullable disable
namespace SRPG
{
  public class SimpleDropTableParam
  {
    public string iname;
    public DateTime beginAt;
    public DateTime endAt;
    public string[] dropList;
    public string[] dropcards;

    public bool Deserialize(JSON_SimpleDropTableParam json)
    {
      this.iname = json.iname;
      this.dropList = json.droplist;
      this.dropcards = json.dropcards;
      this.beginAt = DateTime.MinValue;
      this.endAt = DateTime.MaxValue;
      if (!string.IsNullOrEmpty(json.begin_at))
        DateTime.TryParse(json.begin_at, out this.beginAt);
      if (!string.IsNullOrEmpty(json.end_at))
        DateTime.TryParse(json.end_at, out this.endAt);
      return true;
    }

    public bool IsAvailablePeriod(DateTime now) => !(now < this.beginAt) && !(this.endAt < now);

    public string GetCommonName
    {
      get
      {
        if (string.IsNullOrEmpty(this.iname))
          return string.Empty;
        return this.iname.Split(':')[0];
      }
    }

    public bool IsSuffix => 2 <= this.iname.Split(':').Length;
  }
}
