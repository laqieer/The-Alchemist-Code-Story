// Decompiled with JetBrains decompiler
// Type: SRPG.ReqHikkoshiExec
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

namespace SRPG
{
  public class ReqHikkoshiExec : WebAPI
  {
    public ReqHikkoshiExec(string token, Network.ResponseCallback response)
    {
      this.name = "hikkoshi/exec";
      this.body = WebAPI.GetRequestString("\"token\":\"" + token + "\"");
      this.callback = response;
    }
  }
}
