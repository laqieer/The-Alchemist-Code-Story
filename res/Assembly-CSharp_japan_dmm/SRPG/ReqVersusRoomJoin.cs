// Decompiled with JetBrains decompiler
// Type: SRPG.ReqVersusRoomJoin
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

#nullable disable
namespace SRPG
{
  public class ReqVersusRoomJoin : WebAPI
  {
    public ReqVersusRoomJoin(int roomID, Network.ResponseCallback response)
    {
      this.name = "vs/friendmatch/join";
      this.body = string.Empty;
      ReqVersusRoomJoin reqVersusRoomJoin = this;
      reqVersusRoomJoin.body = reqVersusRoomJoin.body + "\"roomid\":" + (object) roomID;
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
      public ReqVersusRoomJoin.Quest quest;
      public string btl_ver;
    }
  }
}
