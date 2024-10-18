// Decompiled with JetBrains decompiler
// Type: SRPG.ReqMultiTwRoom
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

namespace SRPG
{
  public class ReqMultiTwRoom : WebAPI
  {
    public ReqMultiTwRoom(string fuid, string iname, int floor, Network.ResponseCallback response)
    {
      this.name = "btl/multi/tower/room";
      this.body = string.Empty;
      if (!string.IsNullOrEmpty(fuid))
      {
        ReqMultiTwRoom reqMultiTwRoom = this;
        reqMultiTwRoom.body = reqMultiTwRoom.body + "\"fuid\":\"" + JsonEscape.Escape(fuid) + "\"";
      }
      if (!string.IsNullOrEmpty(iname))
      {
        if (!string.IsNullOrEmpty(this.body))
          this.body += ",";
        ReqMultiTwRoom reqMultiTwRoom = this;
        reqMultiTwRoom.body = reqMultiTwRoom.body + "\"iname\":\"" + JsonEscape.Escape(iname) + "\"";
      }
      if (!string.IsNullOrEmpty(iname))
      {
        if (!string.IsNullOrEmpty(this.body))
          this.body += ",";
        ReqMultiTwRoom reqMultiTwRoom = this;
        reqMultiTwRoom.body = reqMultiTwRoom.body + "\"floor\":" + (object) floor;
      }
      this.body = WebAPI.GetRequestString(this.body);
      this.callback = response;
    }

    public class Response
    {
      public MultiPlayAPIRoom[] rooms;
    }
  }
}
