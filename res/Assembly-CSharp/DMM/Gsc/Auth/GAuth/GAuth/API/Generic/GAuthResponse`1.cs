// Decompiled with JetBrains decompiler
// Type: Gsc.Auth.GAuth.GAuth.API.Generic.GAuthResponse`1
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using Gsc.DOM;
using Gsc.DOM.Json;
using Gsc.Network;

#nullable disable
namespace Gsc.Auth.GAuth.GAuth.API.Generic
{
  public abstract class GAuthResponse<TResponse> : Response<TResponse> where TResponse : IResponse<TResponse>
  {
    public IDocument Parse(WebInternalResponse response)
    {
      Document document = Document.Parse(response.Payload);
      Value root;
      if (document.Root.GetObject().TryGetValue("body", out root))
        document.SetRoot(root);
      return (IDocument) document;
    }
  }
}
