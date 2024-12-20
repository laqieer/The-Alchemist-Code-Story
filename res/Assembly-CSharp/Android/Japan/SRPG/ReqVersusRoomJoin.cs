﻿// Decompiled with JetBrains decompiler
// Type: SRPG.ReqVersusRoomJoin
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

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
    }
  }
}
