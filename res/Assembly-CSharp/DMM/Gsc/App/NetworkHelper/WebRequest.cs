// Decompiled with JetBrains decompiler
// Type: Gsc.App.NetworkHelper.WebRequest
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

#nullable disable
namespace Gsc.App.NetworkHelper
{
  public class WebRequest : ApiRequest<WebRequest, WebResponse>
  {
    private readonly string method;
    private readonly string path;
    private readonly byte[] payload;

    public WebRequest(string method, string path, byte[] unencryptedPayload, byte[] payload)
    {
      this.method = method;
      this.path = path;
      this.UnencryptedPayload = unencryptedPayload;
      this.payload = payload;
    }

    public override string GetMethod() => this.method;

    public override byte[] GetPayload() => this.payload;

    public override string GetPath() => "/" + this.path;
  }
}
