// Decompiled with JetBrains decompiler
// Type: SRPG.JSON_AdvanceLapBossParam
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
  public class JSON_AdvanceLapBossParam
  {
    public string iname;
    public int round_buff_max;
    public string boss_bid;
    public string other_bid;
    public JSON_AdvanceLapBossParam.LapInfo[] lap_info;

    [MessagePackObject(true)]
    [Serializable]
    public class LapInfo
    {
      public int round;
      public string reward_id;
    }
  }
}
