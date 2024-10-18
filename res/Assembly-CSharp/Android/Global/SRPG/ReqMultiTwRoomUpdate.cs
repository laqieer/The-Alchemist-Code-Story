﻿// Decompiled with JetBrains decompiler
// Type: SRPG.ReqMultiTwRoomUpdate
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

namespace SRPG
{
  public class ReqMultiTwRoomUpdate : WebAPI
  {
    public ReqMultiTwRoomUpdate(int roomID, string comment, string passCode, string iname, int floor, Network.ResponseCallback response)
    {
      this.name = "btl/multi/tower/update";
      this.body = string.Empty;
      ReqMultiTwRoomUpdate multiTwRoomUpdate1 = this;
      multiTwRoomUpdate1.body = multiTwRoomUpdate1.body + "\"roomid\":" + (object) roomID;
      ReqMultiTwRoomUpdate multiTwRoomUpdate2 = this;
      multiTwRoomUpdate2.body = multiTwRoomUpdate2.body + ",\"iname\":\"" + JsonEscape.Escape(iname) + "\"";
      ReqMultiTwRoomUpdate multiTwRoomUpdate3 = this;
      multiTwRoomUpdate3.body = multiTwRoomUpdate3.body + ",\"floor\":" + (object) floor;
      ReqMultiTwRoomUpdate multiTwRoomUpdate4 = this;
      multiTwRoomUpdate4.body = multiTwRoomUpdate4.body + ",\"pwd\":\"" + JsonEscape.Escape(passCode) + "\"";
      this.body = WebAPI.GetRequestString(this.body);
      this.callback = response;
    }
  }
}
