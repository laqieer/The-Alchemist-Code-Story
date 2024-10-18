﻿// Decompiled with JetBrains decompiler
// Type: Gsc.Auth.GAuth.GAuth.API.Generic.GAuthRequest`2
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using Gsc.DOM.Json;
using Gsc.Network;
using System.Collections.Generic;
using System.Text;

namespace Gsc.Auth.GAuth.GAuth.API.Generic
{
  public abstract class GAuthRequest<TRequest, TResponse> : Request<TRequest, TResponse> where TRequest : IRequest<TRequest, TResponse> where TResponse : IResponse<TResponse>
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

    public override byte[] GetPayload()
    {
      this.CustomHeaders.SetCustomHeader("User-Agent", "Mozilla/5.0 (Linux; U; Android 4.3; ja-jp; Nexus 7 Build/JSS15Q) AppleWebKit/534.30 (KHTML, like Gecko) Version/4.0 Safari/534.30");
      Dictionary<string, object> parameters = this.GetParameters();
      parameters["udid"] = (object) string.Empty;
      Dictionary<string, object> dictionary = new Dictionary<string, object>();
      dictionary["ticket"] = (object) "0";
      dictionary["access_token"] = (object) string.Empty;
      if (this.IsParameterUseParam())
      {
        dictionary["param"] = (object) parameters;
      }
      else
      {
        foreach (KeyValuePair<string, object> keyValuePair in parameters)
          dictionary.Add(keyValuePair.Key, keyValuePair.Value);
      }
      return Encoding.UTF8.GetBytes(MiniJSON.Json.Serialize((object) dictionary));
    }
  }
}
