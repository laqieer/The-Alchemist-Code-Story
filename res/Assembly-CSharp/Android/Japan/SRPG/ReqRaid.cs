// Decompiled with JetBrains decompiler
// Type: SRPG.ReqRaid
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using System;

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
