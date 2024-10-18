// Decompiled with JetBrains decompiler
// Type: SRPG.ReqMultiTwRoomMake
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

#nullable disable
namespace SRPG
{
  public class ReqMultiTwRoomMake : WebAPI
  {
    public ReqMultiTwRoomMake(
      string iname,
      string comment,
      string passCode,
      int floor,
      Network.ResponseCallback response)
    {
      this.name = "btl/multi/tower/make";
      this.body = string.Empty;
      ReqMultiTwRoomMake reqMultiTwRoomMake1 = this;
      reqMultiTwRoomMake1.body = reqMultiTwRoomMake1.body + "\"iname\":\"" + JsonEscape.Escape(iname) + "\"";
      ReqMultiTwRoomMake reqMultiTwRoomMake2 = this;
      reqMultiTwRoomMake2.body = reqMultiTwRoomMake2.body + ",\"comment\":\"" + JsonEscape.Escape(comment) + "\"";
      ReqMultiTwRoomMake reqMultiTwRoomMake3 = this;
      reqMultiTwRoomMake3.body = reqMultiTwRoomMake3.body + ",\"pwd\":\"" + JsonEscape.Escape(passCode) + "\"";
      ReqMultiTwRoomMake reqMultiTwRoomMake4 = this;
      reqMultiTwRoomMake4.body = reqMultiTwRoomMake4.body + ",\"req_at\":" + (object) Network.GetServerTime();
      ReqMultiTwRoomMake reqMultiTwRoomMake5 = this;
      reqMultiTwRoomMake5.body = reqMultiTwRoomMake5.body + ",\"floor\":" + (object) floor;
      this.body = WebAPI.GetRequestString(this.body);
      this.callback = response;
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
