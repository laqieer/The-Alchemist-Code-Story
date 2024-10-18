// Decompiled with JetBrains decompiler
// Type: SRPG.ReqMultiAreaRoomMake
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;
using UnityEngine;

#nullable disable
namespace SRPG
{
  public class ReqMultiAreaRoomMake : WebAPI
  {
    public ReqMultiAreaRoomMake(
      string iname,
      string comment,
      string passCode,
      bool isPrivate,
      int btlSpeed,
      bool limit,
      int unitlv,
      bool clear,
      bool enable_auto,
      Vector2 location,
      Network.ResponseCallback response)
    {
      this.name = "btl/room/areaquest/make";
      this.body = WebAPI.GetRequestString<ReqMultiAreaRoomMake.RequestParam>(new ReqMultiAreaRoomMake.RequestParam()
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
        location = new ReqMultiAreaRoomMake.RequestParam.Location()
        {
          lat = location.x,
          lng = location.y
        },
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
      public ReqMultiAreaRoomMake.RequestParam.Location location;
      public int enable_auto;

      [Serializable]
      public class Location
      {
        public float lat;
        public float lng;
      }
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
