// Decompiled with JetBrains decompiler
// Type: SRPG.ReqRaid
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;

#nullable disable
namespace SRPG
{
  public class ReqRaid : WebAPI
  {
    public ReqRaid(Network.ResponseCallback response)
    {
      this.name = "raidboss";
      this.body = WebAPI.GetRequestString(string.Empty);
      this.callback = response;
    }

    [Serializable]
    public class Response
    {
      public int period_id;
      public int round;
      public int area_id;
      public int is_area_reward;
      public int is_raid_complete_reward;
      public Json_RaidBP bp;
      public JSON_RaidBossData raidboss_current;
      public JSON_RaidBossData rescue_current;
      public JSON_RaidBossInfo[] raidboss_knock_down;
    }
  }
}
