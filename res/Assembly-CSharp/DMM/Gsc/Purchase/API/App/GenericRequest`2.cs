// Decompiled with JetBrains decompiler
// Type: Gsc.Purchase.API.App.GenericRequest`2
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using Gsc.DOM.Json;
using Gsc.Network;

#nullable disable
namespace Gsc.Purchase.API.App
{
  public abstract class GenericRequest<TRequest, TResponse> : Request<TRequest, TResponse>
    where TRequest : IRequest<TRequest, TResponse>
    where TResponse : IResponse<TResponse>
  {
    public override WebTaskResult InquireResult(WebTaskResult result, WebInternalResponse response)
    {
      if (response.StatusCode == 200 && (response.ContentType == ContentType.ApplicationJson || response.ContentType == ContentType.ApplicationOctetStream_Json_AES))
      {
        using (Document document = Document.Parse(response.Payload))
        {
          if (document.Root.GetValueByPointer("/is_error", false))
            return WebTaskResult.MustErrorHandle;
        }
      }
      return base.InquireResult(result, response);
    }
  }
}
