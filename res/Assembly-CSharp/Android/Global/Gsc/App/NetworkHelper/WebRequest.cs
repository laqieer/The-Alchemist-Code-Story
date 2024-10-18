// Decompiled with JetBrains decompiler
// Type: Gsc.App.NetworkHelper.WebRequest
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

namespace Gsc.App.NetworkHelper
{
  public class WebRequest : ApiRequest<WebRequest, WebResponse>
  {
    private readonly string method;
    private readonly string path;
    private readonly byte[] payload;

    public WebRequest(string method, string path, byte[] payload)
    {
      this.method = method;
      this.path = path;
      this.payload = payload;
    }

    public override string GetMethod()
    {
      return this.method;
    }

    public override byte[] GetPayload()
    {
      return this.payload;
    }

    public override string GetPath()
    {
      return "/" + this.path;
    }
  }
}
