// Decompiled with JetBrains decompiler
// Type: SRPG.ReqVersusAudience
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine.Networking;

#nullable disable
namespace SRPG
{
  public class ReqVersusAudience : WebAPI
  {
    public ReqVersusAudience(
      string appid,
      string version,
      string roomid,
      Network.ResponseCallback response,
      DownloadHandler handler)
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
      this.reqtype = WebAPI.RequestType.REQ_STREAM;
    }
  }
}
