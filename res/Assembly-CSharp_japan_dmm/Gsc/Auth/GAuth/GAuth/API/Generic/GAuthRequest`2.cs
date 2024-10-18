// Decompiled with JetBrains decompiler
// Type: Gsc.Auth.GAuth.GAuth.API.Generic.GAuthRequest`2
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using Gsc.DOM.Json;
using Gsc.Network;
using Gsc.Network.Encoding;
using System.Collections.Generic;

#nullable disable
namespace Gsc.Auth.GAuth.GAuth.API.Generic
{
  public abstract class GAuthRequest<TRequest, TResponse> : Request<TRequest, TResponse>
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
      if (this.IsUseEncryption)
      {
        this.UnencryptedPayload = System.Text.Encoding.UTF8.GetBytes(MiniJSON.Json.Serialize((object) dictionary));
        return EncryptionHelper.Encrypt(!EncryptionHelper.IsUseAPPSharedKey(this.GetPath()) ? EncryptionHelper.KeyType.DLC : EncryptionHelper.KeyType.APP, this.UnencryptedPayload, this.GetPath());
      }
      this.UnencryptedPayload = System.Text.Encoding.UTF8.GetBytes(MiniJSON.Json.Serialize((object) dictionary));
      return this.UnencryptedPayload;
    }
  }
}
