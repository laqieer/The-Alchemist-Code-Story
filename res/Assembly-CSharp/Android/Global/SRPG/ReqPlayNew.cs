// Decompiled with JetBrains decompiler
// Type: SRPG.ReqPlayNew
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

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
