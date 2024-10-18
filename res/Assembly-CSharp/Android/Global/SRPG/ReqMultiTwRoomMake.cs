﻿// Decompiled with JetBrains decompiler
// Type: SRPG.ReqMultiTwRoomMake
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

namespace SRPG
{
  public class ReqMultiTwRoomMake : WebAPI
  {
    public ReqMultiTwRoomMake(string iname, string comment, string passCode, int floor, Network.ResponseCallback response)
    {
      this.name = "btl/multi/tower/make";
      this.body = string.Empty;
      ReqMultiTwRoomMake reqMultiTwRoomMake1 = this;
      reqMultiTwRoomMake1.body = reqMultiTwRoomMake1.body + "\"iname\":\"" + JsonEscape.Escape(iname) + "\"";
      ReqMultiTwRoomMake reqMultiTwRoomMake2 = this;
      reqMultiTwRoomMake2.body = reqMultiTwRoomMake2.body + ",\"pwd\":\"" + JsonEscape.Escape(passCode) + "\"";
      ReqMultiTwRoomMake reqMultiTwRoomMake3 = this;
      reqMultiTwRoomMake3.body = reqMultiTwRoomMake3.body + ",\"req_at\":" + (object) Network.GetServerTime();
      ReqMultiTwRoomMake reqMultiTwRoomMake4 = this;
      reqMultiTwRoomMake4.body = reqMultiTwRoomMake4.body + ",\"floor\":" + (object) floor;
      this.body = WebAPI.GetRequestString(this.body);
      this.callback = response;
    }

    public class Response
    {
      public int roomid;
      public string app_id;
      public string token;
    }
  }
}
