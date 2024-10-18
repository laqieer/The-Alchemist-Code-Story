// Decompiled with JetBrains decompiler
// Type: SRPG.ReqMPRoomJoin
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

namespace SRPG
{
  public class ReqMPRoomJoin : WebAPI
  {
    public ReqMPRoomJoin(int roomID, Network.ResponseCallback response, bool LockRoom = false)
    {
      this.name = "btl/room/join";
      this.body = string.Empty;
      ReqMPRoomJoin reqMpRoomJoin = this;
      reqMpRoomJoin.body = reqMpRoomJoin.body + "\"roomid\":" + (object) roomID + ",";
      this.body += "\"pwd\":";
      this.body += !LockRoom ? "\"0\"" : "\"1\"";
      this.body = WebAPI.GetRequestString(this.body);
      this.callback = response;
    }

    public class Quest
    {
      public string iname;
    }

    public class Response
    {
      public string app_id;
      public string token;
      public ReqMPRoomJoin.Quest quest;
    }
  }
}
