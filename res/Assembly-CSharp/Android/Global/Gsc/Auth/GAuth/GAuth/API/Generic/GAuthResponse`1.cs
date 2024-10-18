// Decompiled with JetBrains decompiler
// Type: Gsc.Auth.GAuth.GAuth.API.Generic.GAuthResponse`1
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

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
