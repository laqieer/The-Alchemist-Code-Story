// Decompiled with JetBrains decompiler
// Type: SRPG.ReqVersusAudience
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using UnityEngine.Experimental.Networking;

namespace SRPG
{
  public class ReqVersusAudience : WebAPI
  {
    public ReqVersusAudience(string appid, string version, string roomid, Network.ResponseCallback response, DownloadHandler handler)
    {
      this.name = "photon/watching/view";
      this.body = string.Empty;
      ReqVersusAudience reqVersusAudience1 = this;
      reqVersusAudience1.body = reqVersusAudience1.body + "\"appid\":\"" + JsonEscape.Escape(appid) + "\",";
      ReqVersusAudience reqVersusAudience2 = this;
      reqVersusAudience2.body = reqVersusAudience2.body + "\"appversion\":\"" + JsonEscape.Escape(version) + "\",";
      ReqVersusAudience reqVersusAudience3 = this;
      reqVersusAudience3.body = reqVersusAudience3.body + "\"roomname\":\"" + JsonEscape.Escape(roomid) + "\"";
      this.body = WebAPI.GetRequestString(this.body);
      this.callback = response;
      this.dlHandler = handler;
      this.reqtype = WebAPI.ReqeustType.REQ_STREAM;
    }
  }
}
