// Decompiled with JetBrains decompiler
// Type: SRPG.ReqPlayNew
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;

namespace SRPG
{
  public class ReqPlayNew : WebAPI
  {
    public ReqPlayNew(bool isDebug, Network.ResponseCallback response)
    {
      this.name = "playnew";
      this.body = string.Empty;
      string str = string.Empty;
      if (isDebug)
        str = "\"debug\":1,";
      this.body += WebAPI.GetRequestString(str + "\"permanent_id\":\"" + MonoSingleton<GameManager>.Instance.UdId + "\"");
      this.callback = response;
    }
  }
}
