// Decompiled with JetBrains decompiler
// Type: Gsc.Purchase.API.App.GenericRequest`2
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using Gsc.DOM.Json;
using Gsc.Network;

namespace Gsc.Purchase.API.App
{
  public abstract class GenericRequest<TRequest, TResponse> : Request<TRequest, TResponse> where TRequest : IRequest<TRequest, TResponse> where TResponse : IResponse<TResponse>
  {
    public override WebTaskResult InquireResult(WebTaskResult result, WebInternalResponse response)
    {
      if (response.StatusCode == 200 && response.ContentType == ContentType.ApplicationJson)
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
