// Decompiled with JetBrains decompiler
// Type: Gsc.Network.CustomHeaders
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

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
        return;
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
      setter("X-GUMI-DEVICE-OS", "android");
      setter("X-GUMI-TRANSACTION", this.requestId);
      setter("X-GUMI-DEVICE-PLATFORM", "android");
      if (SDK.Configuration.EnvName != null)
        setter("X-Gumi-Game-Environment", SDK.Configuration.EnvName);
      setter("X-GUMI-REQUEST-ID", this.requestId);
      for (int index = 0; index < this.headersList.Count; ++index)
        this.Dispatch(setter, this.headersList[index]);
      this.Dispatch(setter, this.headers);
    }

    private void Dispatch(Action<string, string> setter, Dictionary<string, string> headers)
    {
      using (Dictionary<string, string>.Enumerator enumerator = headers.GetEnumerator())
      {
        while (enumerator.MoveNext())
        {
          KeyValuePair<string, string> current = enumerator.Current;
          setter(current.Key, current.Value);
        }
      }
    }
  }
}
