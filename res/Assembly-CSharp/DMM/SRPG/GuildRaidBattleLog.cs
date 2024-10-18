// Decompiled with JetBrains decompiler
// Type: SRPG.GuildRaidBattleLog
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;

#nullable disable
namespace SRPG
{
  public class GuildRaidBattleLog
  {
    public string Message { get; private set; }

    public int ReportId { get; private set; }

    public DateTime PostedAt { get; private set; }

    public bool Deserialize(JSON_GuildRaidBattleLog json)
    {
      this.Message = json.message;
      this.ReportId = json.report_id;
      this.PostedAt = TimeManager.FromUnixTime((long) json.posted_at);
      return true;
    }
  }
}
