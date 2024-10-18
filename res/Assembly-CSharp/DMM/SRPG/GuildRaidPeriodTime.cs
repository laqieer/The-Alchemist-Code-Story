// Decompiled with JetBrains decompiler
// Type: SRPG.GuildRaidPeriodTime
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

#nullable disable
namespace SRPG
{
  public class GuildRaidPeriodTime
  {
    public string Begin { get; private set; }

    public string Open { get; private set; }

    public bool Deserialize(JSON_GuildRaidPeriodTime json)
    {
      this.Begin = json.begin_time;
      this.Open = json.open_time;
      return true;
    }
  }
}
