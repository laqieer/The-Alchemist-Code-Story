// Decompiled with JetBrains decompiler
// Type: SRPG.ReqVersusLineMake
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

#nullable disable
namespace SRPG
{
  public class ReqVersusLineMake : WebAPI
  {
    public ReqVersusLineMake(string roomname, Network.ResponseCallback response)
    {
      this.name = "vs/friendmatch/line/make";
      this.body = string.Empty;
      ReqVersusLineMake reqVersusLineMake1 = this;
      reqVersusLineMake1.body = reqVersusLineMake1.body + "\"token\":\"" + JsonEscape.Escape(roomname) + "\"";
      ReqVersusLineMake reqVersusLineMake2 = this;
      reqVersusLineMake2.body = reqVersusLineMake2.body + ",\"is_draft\":" + (object) (!GlobalVars.IsVersusDraftMode ? 0 : 1);
      this.body = WebAPI.GetRequestString(this.body);
      this.callback = response;
    }
  }
}
