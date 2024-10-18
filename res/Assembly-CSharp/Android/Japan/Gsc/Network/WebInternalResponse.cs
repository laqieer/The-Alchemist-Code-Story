// Decompiled with JetBrains decompiler
// Type: Gsc.Network.WebInternalResponse
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.Networking;

namespace Gsc.Network
{
  public class WebInternalResponse
  {
    private readonly WeakReference request;
    public readonly byte[] Payload;
    public readonly int StatusCode;
    public readonly ContentType ContentType;

    public WebInternalResponse(UnityWebRequest request)
    {
      DebugUtility.Log(" api:" + request.url + " p/g:" + request.method);
      this.request = new WeakReference((object) request);
      this.StatusCode = WebInternalResponse.GetStatusCode(request);
      this.Payload = WebInternalResponse.GetResponsePayload(request);
      this.ContentType = WebInternalResponse.GetContentType(this);
    }

    public WebInternalResponse(int statusCode)
    {
      this.request = (WeakReference) null;
      this.StatusCode = statusCode;
      this.Payload = (byte[]) null;
      this.ContentType = ContentType.None;
    }

    public string GetResponseHeader(string name)
    {
      string str = (string) null;
      if (this.request != null && this.request.IsAlive)
        str = ((UnityWebRequest) this.request.Target).GetResponseHeader(name);
      return str;
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
      if (!webRequest.isError)
        return (int) webRequest.responseCode;
      return 0;
    }

    private static byte[] GetResponsePayload(UnityWebRequest webRequest)
    {
      return webRequest.downloadHandler.data;
    }

    private static ContentType GetContentType(WebInternalResponse response)
    {
      string responseHeader = response.GetResponseHeader("CONTENT-TYPE");
      if (responseHeader != null)
      {
        if (responseHeader.StartsWith("application/json"))
          return ContentType.ApplicationJson;
        if (responseHeader.StartsWith("application/octet-stream"))
          return ContentType.ApplicationOctetStream;
      }
      return ContentType.TextPlain;
    }
  }
}
