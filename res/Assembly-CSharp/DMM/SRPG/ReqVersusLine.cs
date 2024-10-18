// Decompiled with JetBrains decompiler
// Type: SRPG.ReqVersusLine
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

#nullable disable
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
