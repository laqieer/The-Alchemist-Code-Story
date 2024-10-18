// Decompiled with JetBrains decompiler
// Type: SRPG.ReqMPRoomMake
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;

#nullable disable
namespace SRPG
{
  public class ReqMPRoomMake : WebAPI
  {
    public ReqMPRoomMake(
      string iname,
      string comment,
      string passCode,
      bool isPrivate,
      int btlSpeed,
      bool limit,
      int unitlv,
      bool clear,
      bool enable_auto,
      Network.ResponseCallback response)
    {
      this.name = "btl/room/make";
      this.body = WebAPI.GetRequestString<ReqMPRoomMake.RequestParam>(new ReqMPRoomMake.RequestParam()
      {
        iname = iname,
        comment = comment,
        pwd = passCode,
        @private = !isPrivate ? 0 : 1,
        btl_speed = btlSpeed,
        req_at = Network.GetServerTime(),
        limit = !limit ? 0 : 1,
        unitlv = unitlv,
        clear = !clear ? 0 : 1,
        enable_auto = !enable_auto ? 0 : 1
      });
      this.callback = response;
    }

    [Serializable]
    public class RequestParam
    {
      public string iname;
      public string comment;
      public string pwd;
      public int @private;
      public int btl_speed;
      public long req_at;
      public int limit;
      public int unitlv;
      public int clear;
      public int enable_auto;
    }

    public class Response
    {
      public int roomid;
      public string app_id;
      public string token;
      public string btl_ver;
    }
  }
}
