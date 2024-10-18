// Decompiled with JetBrains decompiler
// Type: SRPG.ReqRaidRescueSend
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using System;

namespace SRPG
{
  public class ReqRaidRescueSend : WebAPI
  {
    public ReqRaidRescueSend(int area_id, int boss_id, int round, int is_send_guild, int is_send_friend, Network.ResponseCallback response)
    {
      this.name = "raidboss/rescue/send";
      this.body = WebAPI.GetRequestString<ReqRaidRescueSend.RequestParam>(new ReqRaidRescueSend.RequestParam()
      {
        area_id = area_id,
        boss_id = boss_id,
        round = round,
        is_send_guild = is_send_guild,
        is_send_friend = is_send_friend
      });
      this.callback = response;
    }

    [Serializable]
    public class RequestParam
    {
      public int area_id;
      public int boss_id;
      public int round;
      public int is_send_guild;
      public int is_send_friend;
    }
  }
}
