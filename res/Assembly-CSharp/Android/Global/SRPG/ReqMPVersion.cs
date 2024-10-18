// Decompiled with JetBrains decompiler
// Type: SRPG.ReqMPVersion
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

namespace SRPG
{
  public class ReqMPVersion : WebAPI
  {
    public ReqMPVersion(Network.ResponseCallback response)
    {
      this.name = "btl/multi/check";
      this.body = string.Empty;
      this.body = WebAPI.GetRequestString(this.body);
      this.callback = response;
    }

    public class Response
    {
      public string device_id;
    }
  }
}
