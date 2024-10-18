// Decompiled with JetBrains decompiler
// Type: SRPG.JSON_WorldRaidLogData
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
  public class JSON_WorldRaidLogData
  {
    public int log_id;
    public string boss_iname;
    public string player_name;
    public long damage;
  }
}
