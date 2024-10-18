// Decompiled with JetBrains decompiler
// Type: SRPG.ReqRaidRescueSend
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;

#nullable disable
namespace SRPG
{
  public class ReqRaidRescueSend : WebAPI
  {
    public ReqRaidRescueSend(
      int area_id,
      int boss_id,
      int round,
      int is_send_guild,
      int is_send_friend,
      Network.ResponseCallback response)
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
