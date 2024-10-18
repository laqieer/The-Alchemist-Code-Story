// Decompiled with JetBrains decompiler
// Type: SRPG.ReqLoginPack
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

namespace SRPG
{
  public class ReqLoginPack : WebAPI
  {
    public ReqLoginPack(Network.ResponseCallback response, bool relogin = false)
    {
      this.name = "login/param";
      this.body = "\"relogin\":";
      this.body += (string) (object) (!relogin ? 0 : 1);
      this.body += ",\"req_uid\":1";
      this.body = WebAPI.GetRequestString(this.body);
      this.callback = response;
    }
  }
}
