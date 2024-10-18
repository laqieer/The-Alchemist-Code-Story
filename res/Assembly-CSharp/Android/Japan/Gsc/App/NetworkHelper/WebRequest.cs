// Decompiled with JetBrains decompiler
// Type: Gsc.App.NetworkHelper.WebRequest
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

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
