// Decompiled with JetBrains decompiler
// Type: Gsc.Network.WebInternalResponse
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using Gsc.Network.Encoding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using UnityEngine.Networking;

#nullable disable
namespace Gsc.Network
{
  public class WebInternalResponse
  {
    public static readonly byte[] RETRY_FOR_CHECK_MENTE_FLAG = new byte[2]
    {
      (byte) 21,
      (byte) 23
    };
    private readonly WeakReference request;
    public readonly byte[] Payload;
    public readonly int StatusCode;
    public readonly ContentType ContentType;
    public readonly ContentEncoding ContentEncoding;

    public WebInternalResponse(UnityWebRequest request)
    {
      this.request = new WeakReference((object) request);
      this.StatusCode = WebInternalResponse.GetStatusCode(request);
      this.Payload = WebInternalResponse.GetResponsePayload(request);
      this.ContentType = WebInternalResponse.GetContentType(this);
      this.ContentEncoding = WebInternalResponse.GetContentEncoding(this);
      DebugUtility.Log(" <color=\"lightblue\">api:</color><color=\"yellow\">" + request.url + "</color> <color=\"lightblue\">p/g:</color><color=\"yellow\">" + request.method + "</color>");
    }

    public WebInternalResponse(int statusCode)
    {
      this.request = (WeakReference) null;
      this.StatusCode = statusCode;
      this.Payload = (byte[]) null;
      this.ContentType = ContentType.None;
      this.ContentEncoding = ContentEncoding.None;
    }

    public string GetResponseHeader(string name)
    {
      string responseHeader = (string) null;
      if (this.request != null && this.request.IsAlive)
        responseHeader = ((UnityWebRequest) this.request.Target).GetResponseHeader(name);
      return responseHeader;
    }

    private static int GetStatusCode(UnityWebRequest webRequest)
    {
      if (webRequest.GetResponseHeaders() != null)
      {
        string s;
        if (webRequest.GetResponseHeaders().TryGetValue("X-GUMI-STATUS-CODE", out s))
          return int.Parse(s);
        if (webRequest.GetResponseHeaders().TryGetValue("STATUS", out s) || webRequest.GetResponseHeaders().TryGetValue("NULL", out s))
        {
          if (s.ToLower().Contains("connection established"))
            return 503;
          string[] array = ((IEnumerable<string>) s.Split(' ')).Select<string, string>((Func<string, string>) (x => x.Trim())).Where<string>((Func<string, bool>) (x => !string.IsNullOrEmpty(x))).ToArray<string>();
          if (array.Length >= 3)
            return int.Parse(array[1]);
        }
      }
      return !webRequest.isNetworkError ? (int) webRequest.responseCode : 0;
    }

    private static byte[] GetResponsePayload(UnityWebRequest webRequest)
    {
      string str = webRequest.GetResponseHeader("Content-Type");
      string pathAndQuery = new Uri(webRequest.url).PathAndQuery;
      EncryptionHelper.DecryptOptions options = EncryptionHelper.DecryptOptions.ExtraKeySaltATDI;
      if (!string.IsNullOrEmpty(str) && str.Contains(EncodingTypes.BCT_NO_EXTRA_KEY_SALT))
      {
        str = str.Replace("+" + EncodingTypes.BCT_NO_EXTRA_KEY_SALT, string.Empty);
        options = EncryptionHelper.DecryptOptions.None;
      }
      else if (pathAndQuery.StartsWith("/login"))
        options = EncryptionHelper.DecryptOptions.ExtraKeySaltAT;
      if (!string.IsNullOrEmpty(str))
      {
        if (!str.StartsWith(EncodingTypes.BCT_JSON_AES))
        {
          if (!str.StartsWith(EncodingTypes.BCT_MESSAGEPACK_AES))
            goto label_11;
        }
        try
        {
          return EncryptionHelper.Decrypt(!EncryptionHelper.IsUseAPPSharedKey(pathAndQuery) ? EncryptionHelper.KeyType.DLC : EncryptionHelper.KeyType.APP, webRequest.downloadHandler.data, pathAndQuery, options);
        }
        catch (CryptographicException ex)
        {
          if (pathAndQuery == "/chkver2" || pathAndQuery.StartsWith("/gauth/"))
            return WebInternalResponse.RETRY_FOR_CHECK_MENTE_FLAG;
          throw ex;
        }
      }
label_11:
      return webRequest.downloadHandler.data;
    }

    private static ContentType GetContentType(WebInternalResponse response)
    {
      string responseHeader = response.GetResponseHeader("CONTENT-TYPE");
      if (responseHeader != null)
      {
        if (responseHeader.StartsWith(EncodingTypes.BCT_JSON_AES))
          return ContentType.ApplicationOctetStream_Json_AES;
        if (responseHeader.StartsWith(EncodingTypes.BCT_MESSAGEPACK_AES))
          return ContentType.ApplicationOctetStream_MessagePack_AES;
        if (responseHeader.StartsWith(EncodingTypes.BCT_MESSAGEPACK))
          return ContentType.ApplicationOctetStream_MessagePack;
        if (responseHeader.StartsWith("application/json"))
          return ContentType.ApplicationJson;
        if (responseHeader.StartsWith("application/octet-stream"))
          return ContentType.ApplicationOctetStream;
      }
      return ContentType.TextPlain;
    }

    private static ContentEncoding GetContentEncoding(WebInternalResponse response)
    {
      string responseHeader = response.GetResponseHeader("CONTENT-ENCODING");
      if (responseHeader != null)
      {
        if (responseHeader.StartsWith("lz4"))
          return ContentEncoding.Lz4;
        if (responseHeader.StartsWith("gzip"))
          return ContentEncoding.Gzip;
      }
      return ContentEncoding.None;
    }
  }
}
