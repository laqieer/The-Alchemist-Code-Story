// Decompiled with JetBrains decompiler
// Type: SRPG.ReqMPRoomMake
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

namespace SRPG
{
  public class ReqMPRoomMake : WebAPI
  {
    public ReqMPRoomMake(string iname, string comment, string passCode, bool isPrivate, bool isHiSpeed, bool limit, int unitlv, bool clear, Network.ResponseCallback response)
    {
      this.name = "btl/room/make";
      this.body = string.Empty;
      ReqMPRoomMake reqMpRoomMake1 = this;
      reqMpRoomMake1.body = reqMpRoomMake1.body + "\"iname\":\"" + JsonEscape.Escape(iname) + "\"";
      ReqMPRoomMake reqMpRoomMake2 = this;
      reqMpRoomMake2.body = reqMpRoomMake2.body + ",\"comment\":\"" + JsonEscape.Escape(comment) + "\"";
      ReqMPRoomMake reqMpRoomMake3 = this;
      reqMpRoomMake3.body = reqMpRoomMake3.body + ",\"pwd\":\"" + JsonEscape.Escape(passCode) + "\"";
      ReqMPRoomMake reqMpRoomMake4 = this;
      reqMpRoomMake4.body = reqMpRoomMake4.body + ",\"private\":" + (object) (!isPrivate ? 0 : 1);
      ReqMPRoomMake reqMpRoomMake5 = this;
      reqMpRoomMake5.body = reqMpRoomMake5.body + ",\"btl_speed\":" + (object) (!isHiSpeed ? 1 : 2);
      ReqMPRoomMake reqMpRoomMake6 = this;
      reqMpRoomMake6.body = reqMpRoomMake6.body + ",\"req_at\":" + (object) Network.GetServerTime();
      ReqMPRoomMake reqMpRoomMake7 = this;
      reqMpRoomMake7.body = reqMpRoomMake7.body + ",\"limit\":" + (object) (!limit ? 0 : 1);
      ReqMPRoomMake reqMpRoomMake8 = this;
      reqMpRoomMake8.body = reqMpRoomMake8.body + ",\"unitlv\":" + (object) unitlv;
      ReqMPRoomMake reqMpRoomMake9 = this;
      reqMpRoomMake9.body = reqMpRoomMake9.body + ",\"clear\":" + (object) (!clear ? 0 : 1);
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
