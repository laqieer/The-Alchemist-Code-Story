// Decompiled with JetBrains decompiler
// Type: SRPG.ReqMPRoomJoin
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

#nullable disable
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
      public string btl_ver;
    }
  }
}
