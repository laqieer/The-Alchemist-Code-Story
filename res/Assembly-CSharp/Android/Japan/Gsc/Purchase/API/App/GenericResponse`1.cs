// Decompiled with JetBrains decompiler
// Type: Gsc.Purchase.API.App.GenericResponse`1
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

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
