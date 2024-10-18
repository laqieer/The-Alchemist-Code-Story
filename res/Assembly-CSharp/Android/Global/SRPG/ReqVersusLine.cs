// Decompiled with JetBrains decompiler
// Type: SRPG.ReqVersusLine
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

namespace SRPG
{
  public class ReqVersusLine : WebAPI
  {
    public ReqVersusLine(string roomname, Network.ResponseCallback response)
    {
      this.name = "vs/friendmatch/line/recruitment";
      this.body = string.Empty;
      ReqVersusLine reqVersusLine = this;
      reqVersusLine.body = reqVersusLine.body + "\"token\":\"" + JsonEscape.Escape(roomname) + "\"";
      this.body = WebAPI.GetRequestString(this.body);
      this.callback = response;
    }
  }
}
