// Decompiled with JetBrains decompiler
// Type: Gsc.Network.CustomHeaders
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using Gsc.Auth;
using Gsc.Network.Encoding;
using SRPG;
using System;
using System.Collections.Generic;
using UnityEngine.Networking;

#nullable disable
namespace Gsc.Network
{
  public class CustomHeaders
  {
    private readonly string requestId;
    private readonly Dictionary<string, string> headers = new Dictionary<string, string>();
    private readonly List<Dictionary<string, string>> headersList = new List<Dictionary<string, string>>();

    public CustomHeaders(string requestId)
    {
      this.requestId = requestId;
      this.IsUseEncryption = GameUtility.Config_UseEncryption.Value;
    }

    public bool IsUseEncryption { get; set; }

    public void SetCustomHeader(string key, string value, Action<string, string> setter = null)
    {
      if (setter != null)
        setter(key, value);
      else if (this.headers.ContainsKey(key))
        DebugUtility.LogError("headers containskey. keyname = " + key);
      else
        this.headers.Add(key, value);
    }

    public void AddCustomHeaders(Dictionary<string, string> headers)
    {
      this.headersList.Add(headers);
    }

    public void SetSerializationCompressionEncryptionHeaders(
      string url,
      EncodingTypes.ESerializeCompressMethod method,
      Action<string, string> setter = null)
    {
      if (SDK.Configuration.Env != null && !string.IsNullOrEmpty(SDK.Configuration.Env.ServerUrl) && url.StartsWith("http") && !url.StartsWith(SDK.Configuration.Env.ServerUrl))
      {
        this.SetCustomHeader("Content-Type", "application/json; charset=utf-8", setter);
      }
      else
      {
        if (GlobalVars.SelectedSerializeCompressMethodWasNodeSet)
          method = GlobalVars.SelectedSerializeCompressMethod;
        string empty = string.Empty;
        if (SRPG.Network.MenteCheckFlag)
          this.SetCustomHeader("x-taitapu-check", "true", setter);
        if (SRPG.Network.DoChkver2InJson)
        {
          this.SetCustomHeader("Content-Type", "application/json; charset=utf-8", setter);
          SRPG.Network.DoChkver2InJson = false;
        }
        else
        {
          switch (method)
          {
            case EncodingTypes.ESerializeCompressMethod.TYPED_MESSAGE_PACK:
              this.SetCustomHeader("Content-Type", (!this.IsUseEncryption ? EncodingTypes.BCT_MESSAGEPACK : EncodingTypes.BCT_MESSAGEPACK_AES) + empty, setter);
              this.SetCustomHeader("Content-Encoding", "identity", setter);
              this.SetCustomHeader("Accept-Encoding", "identity", setter);
              break;
            case EncodingTypes.ESerializeCompressMethod.TYPED_MESSAGE_PACK_LZ4:
              this.SetCustomHeader("Content-Type", (!this.IsUseEncryption ? EncodingTypes.BCT_MESSAGEPACK : EncodingTypes.BCT_MESSAGEPACK_AES) + empty, setter);
              this.SetCustomHeader("Content-Encoding", "lz4", setter);
              this.SetCustomHeader("Accept-Encoding", "lz4", setter);
              break;
            default:
              if (url.Contains("/photon/"))
                this.SetCustomHeader("Content-Type", "application/json; charset=utf-8", setter);
              else
                this.SetCustomHeader("Content-Type", !this.IsUseEncryption ? "application/json; charset=utf-8" : EncodingTypes.BCT_JSON_AES + empty, setter);
              this.SetCustomHeader("Content-Encoding", "identity", setter);
              this.SetCustomHeader("Accept-Encoding", "identity", setter);
              break;
          }
        }
      }
    }

    public void Dispatch(UnityWebRequest request)
    {
      Action<string, string> setter = new Action<string, string>(request.SetRequestHeader);
      this.SetSerializationCompressionEncryptionHeaders(request.url, EncodingTypes.ESerializeCompressMethod.JSON, setter);
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
