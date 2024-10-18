// Decompiled with JetBrains decompiler
// Type: SRPG.ReqVersusLineMake
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

namespace SRPG
{
  public class ReqVersusLineMake : WebAPI
  {
    public ReqVersusLineMake(string roomname, Network.ResponseCallback response)
    {
      this.name = "vs/friendmatch/line/make";
      this.body = string.Empty;
      ReqVersusLineMake reqVersusLineMake = this;
      reqVersusLineMake.body = reqVersusLineMake.body + "\"token\":\"" + JsonEscape.Escape(roomname) + "\"";
      this.body = WebAPI.GetRequestString(this.body);
      this.callback = response;
    }
  }
}
