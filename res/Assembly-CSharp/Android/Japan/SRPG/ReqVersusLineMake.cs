// Decompiled with JetBrains decompiler
// Type: SRPG.ReqVersusLineMake
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

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
