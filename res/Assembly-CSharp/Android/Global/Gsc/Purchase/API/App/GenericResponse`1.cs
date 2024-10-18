// Decompiled with JetBrains decompiler
// Type: Gsc.Purchase.API.App.GenericResponse`1
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using Gsc.DOM;
using Gsc.DOM.Json;
using Gsc.Network;

namespace Gsc.Purchase.API.App
{
  public abstract class GenericResponse<TResponse> : Response<TResponse> where TResponse : IResponse<TResponse>
  {
    public IDocument Parse(WebInternalResponse response)
    {
      return (IDocument) Document.Parse(response.Payload);
    }
  }
}
