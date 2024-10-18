// Decompiled with JetBrains decompiler
// Type: Gsc.Network.CustomHeaders
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using Gsc.Auth;
using System;
using System.Collections.Generic;

namespace Gsc.Network
{
  public class CustomHeaders
  {
    private readonly Dictionary<string, string> headers = new Dictionary<string, string>();
    private readonly List<Dictionary<string, string>> headersList = new List<Dictionary<string, string>>();
    private readonly string requestId;

    public CustomHeaders(string requestId)
    {
      this.requestId = requestId;
    }

    public void SetCustomHeader(string key, string value)
    {
      if (this.headers.ContainsKey(key))
        DebugUtility.LogError("headers containskey. keyname = " + key);
      else
        this.headers.Add(key, value);
    }

    public void AddCustomHeaders(Dictionary<string, string> headers)
    {
      this.headersList.Add(headers);
    }

    public void Dispatch(Action<string, string> setter)
    {
      setter("Content-Type", "application/json; charset=utf-8");
      if (!SDK.Initialized)
        return;
      if (Session.DefaultSession.AccessToken != null)
        setter("Authorization", "gauth " + Session.DefaultSession.AccessToken);
      setter("X-Gumi-User-Agent", Session.DefaultSession.UserAgent);
      setter("X-GUMI-CLIENT", "gscc ver.0.1");
      setter("X-GUMI-DEVICE-OS", "windows");
      setter("X-GUMI-TRANSACTION", this.requestId);
      CustomHeaders.SetXGumiDeviceStorePlatform(setter);
      if (SDK.Configuration.EnvName != null)
        setter("X-Gumi-Game-Environment", SDK.Configuration.EnvName);
      setter("X-GUMI-REQUEST-ID", this.requestId);
      for (int index = 0; index < this.headersList.Count; ++index)
        this.Dispatch(setter, this.headersList[index]);
      this.Dispatch(setter, this.headers);
    }

    private void Dispatch(Action<string, string> setter, Dictionary<string, string> headers)
    {
      string empty = string.Empty;
      foreach (KeyValuePair<string, string> header in headers)
      {
        string str = header.Value;
        if (!string.IsNullOrEmpty(str))
          str = str.Replace("(", string.Empty).Replace(")", string.Empty);
        setter(header.Key, str);
      }
    }

    public static void SetXGumiDeviceStorePlatform(Action<string, string> setter)
    {
      setter("X-GUMI-STORE-PLATFORM", Device.Platform);
    }
  }
}
