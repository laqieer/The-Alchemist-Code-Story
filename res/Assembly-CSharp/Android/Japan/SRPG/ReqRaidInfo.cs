// Decompiled with JetBrains decompiler
// Type: SRPG.ReqRaidInfo
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using System;

namespace SRPG
{
  public class ReqRaidInfo : WebAPI
  {
    public ReqRaidInfo(int area_id, int boss_id, int round, string uid, Network.ResponseCallback response)
    {
      this.name = "raidboss/info";
      this.body = WebAPI.GetRequestString<ReqRaidInfo.RequestParam>(new ReqRaidInfo.RequestParam()
      {
        area_id = area_id,
        boss_id = boss_id,
        round = round,
        uid = uid
      });
      this.callback = response;
    }

    [Serializable]
    public class RequestParam
    {
      public int area_id;
      public int boss_id;
      public int round;
      public string uid;
    }

    [Serializable]
    public class Response
    {
      public int able_challenge_count;
      public JSON_RaidBossData raidboss;
    }
  }
}
