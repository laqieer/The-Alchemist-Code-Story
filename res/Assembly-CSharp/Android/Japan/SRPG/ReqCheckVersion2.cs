// Decompiled with JetBrains decompiler
// Type: SRPG.ReqCheckVersion2
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

namespace SRPG
{
  public class ReqCheckVersion2 : WebAPI
  {
    public ReqCheckVersion2(string ver, Network.ResponseCallback response)
    {
      this.name = "chkver2";
      this.body = "{\"ver\":\"";
      this.body += ver;
      this.body += "\"}";
      this.callback = response;
    }
  }
}
