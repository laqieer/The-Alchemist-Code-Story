// Decompiled with JetBrains decompiler
// Type: Gsc.Auth.GAuth.GAuth.API.Generic.GAuthResponse`1
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using Gsc.DOM;
using Gsc.DOM.Json;
using Gsc.Network;

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
