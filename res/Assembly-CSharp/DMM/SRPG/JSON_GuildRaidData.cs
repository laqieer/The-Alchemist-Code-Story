// Decompiled with JetBrains decompiler
// Type: SRPG.JSON_GuildRaidData
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using MessagePack;
using System;

#nullable disable
namespace SRPG
{
  [MessagePackObject(true)]
  [Serializable]
  public class JSON_GuildRaidData
  {
    public JSON_GuildRaidPrev prev;
    public JSON_GuildRaidCurrent current;
    public JSON_GuildRaidBattlePoint bp;
    public JSON_GuildRaidBossInfo bossinfo;
    public int can_open_area_id;
    public int refresh_wait_sec;
  }
}
