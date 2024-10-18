// Decompiled with JetBrains decompiler
// Type: Gsc.App.NetworkHelper.GsccBridge
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using Gsc.Auth;
using Gsc.Network;
using Gsc.Network.Encoding;
using SRPG;
using System;
using UnityEngine.Networking;

#nullable disable
namespace Gsc.App.NetworkHelper
{
  public static class GsccBridge
  {
    private static WebTaskBundle unhandledTasks;

    public static void Send(WebAPI api, bool silent)
    {
      WebTaskAttribute attributes = WebTaskAttribute.Reliable;
      if (silent)
        attributes |= WebTaskAttribute.Silent;
      bool flag1 = !string.IsNullOrEmpty(api.body);
      string method = !flag1 ? "GET" : "POST";
      bool flag2 = api.name.Equals("chkver2") || api.name.StartsWith("photon/");
      byte[] payload = (byte[]) null;
      byte[] unencryptedPayload = new byte[0];
      if (flag1)
      {
        unencryptedPayload = System.Text.Encoding.UTF8.GetBytes(api.body);
        payload = flag2 ? unencryptedPayload : GsccBridge.EncodePayload(api);
      }
      WebRequest webRequest = new WebRequest(method, api.name, unencryptedPayload, payload);
      GsccBridge.SetCustomHeaders(webRequest.CustomHeaders);
      if (!api.name.StartsWith("photon/"))
        webRequest.CustomHeaders.SetSerializationCompressionEncryptionHeaders(api.name, api.serializeCompressMethod);
      GsccBridge.Send(webRequest.ToWebTask(attributes), api.callback);
    }

    public static void SendImmediate(WebAPI api)
    {
      bool flag1 = !string.IsNullOrEmpty(api.body);
      string method = !flag1 ? "GET" : "POST";
      bool flag2 = api.name.Equals("chkver2") || api.name.StartsWith("photon/");
      byte[] payload = (byte[]) null;
      byte[] unencryptedPayload = new byte[0];
      if (flag1)
      {
        unencryptedPayload = System.Text.Encoding.UTF8.GetBytes(api.body);
        payload = flag2 ? unencryptedPayload : GsccBridge.EncodePayload(api);
      }
      WebRequest webRequest = new WebRequest(method, api.name, unencryptedPayload, payload);
      GsccBridge.SetCustomHeaders(webRequest.CustomHeaders);
      if (!api.name.StartsWith("photon/"))
        webRequest.CustomHeaders.SetSerializationCompressionEncryptionHeaders(api.name, api.serializeCompressMethod);
      BlockRequest<WebRequest, WebResponse> blockRequest = BlockRequest.Create<WebRequest, WebResponse>((IRequest<WebRequest, WebResponse>) webRequest);
      string result;
      if (blockRequest.GetResult() != WebTaskResult.Success)
      {
        SRPG.Network.SetServerMetaDataAsError();
        result = string.Empty;
      }
      else
      {
        WebResponse response = blockRequest.GetResponse();
        result = response == null || response.payload == null ? string.Empty : System.Text.Encoding.UTF8.GetString(response.payload);
      }
      api.callback(new WWWResult(result));
    }

    private static byte[] EncodePayload(WebAPI api)
    {
      bool flag = EncryptionHelper.IsUseAPPSharedKey("/" + api.name);
      byte[] input = (byte[]) null;
      switch (api.serializeCompressMethod)
      {
        case EncodingTypes.ESerializeCompressMethod.JSON:
          input = SerializerCompressorHelper.Encode<byte[]>(System.Text.Encoding.UTF8.GetBytes(api.body), compressMode: CompressMode.None);
          break;
        case EncodingTypes.ESerializeCompressMethod.TYPED_MESSAGE_PACK:
          input = SerializerCompressorHelper.Encode<string>(api.body, true, CompressMode.None, true);
          break;
        case EncodingTypes.ESerializeCompressMethod.TYPED_MESSAGE_PACK_LZ4:
          input = SerializerCompressorHelper.Encode<string>(api.body, true, useFromJson: true);
          break;
      }
      return GameUtility.Config_UseEncryption.Value ? EncryptionHelper.Encrypt(!flag ? EncryptionHelper.KeyType.DLC : EncryptionHelper.KeyType.APP, input, "/" + api.name) : input;
    }

    private static void SetCustomHeaders(CustomHeaders headers)
    {
      if (!string.IsNullOrEmpty(SRPG.Network.Version))
        headers.SetCustomHeader("x-app-ver", SRPG.Network.Version);
      if (string.IsNullOrEmpty(SRPG.Network.AssetVersion))
        return;
      headers.SetCustomHeader("x-asset-ver", SRPG.Network.AssetVersion);
    }

    public static void SetBaseCustomHeaders(
      UnityWebRequest webReq,
      Action<string, string> setter,
      string requestId)
    {
      if (SDK.Initialized)
      {
        CustomHeaders headers = new CustomHeaders(requestId);
        GsccBridge.SetCustomHeaders(headers);
        headers.Dispatch(webReq);
      }
      else
      {
        setter("Content-Type", "application/json; charset=utf-8");
        if (!string.IsNullOrEmpty(SRPG.Network.Version))
          setter("x-app-ver", SRPG.Network.Version);
        if (!string.IsNullOrEmpty(SRPG.Network.AssetVersion))
          setter("x-asset-ver", SRPG.Network.AssetVersion);
        setter("X-GUMI-DEVICE-OS", "windows");
        setter("X-GUMI-TRANSACTION", requestId);
        setter("X-GUMI-REQUEST-ID", requestId);
        if (Session.DefaultSession == null || Session.DefaultSession.AccessToken == null)
          return;
        setter("Authorization", "gauth " + Session.DefaultSession.AccessToken);
        setter("X-Gumi-User-Agent", Session.DefaultSession.UserAgent);
      }
    }

    public static void SetWebViewHeaders(Action<string, string> setter)
    {
      if (!SDK.Initialized)
        return;
      if (Session.DefaultSession != null && Session.DefaultSession.AccessToken != null)
        setter("Authorization", "gauth " + Session.DefaultSession.AccessToken);
      setter("X-GUMI-DEVICE-OS", "windows");
      CustomHeaders.SetXGumiDeviceStorePlatform(setter);
    }

    private static void Send(
      WebTask<WebRequest, WebResponse> task,
      SRPG.Network.ResponseCallback callback)
    {
      task.OnResponse((VoidCallback<WebResponse>) (r => SRPG.Network.ConnectingResponse(r, callback)));
    }

    public static bool HasUnhandledTasks => GsccBridge.unhandledTasks != null;

    public static void OnReceiveUnhandledTasks(WebTaskBundle taskBundle)
    {
      if (GsccBridge.unhandledTasks == null)
      {
        GsccBridge.unhandledTasks = taskBundle;
      }
      else
      {
        foreach (IWebTask task in taskBundle)
          GsccBridge.unhandledTasks.Add<IWebTask>(task);
      }
    }

    public static void Retry()
    {
      if (GsccBridge.unhandledTasks == null)
        return;
      GsccBridge.unhandledTasks.Retry();
      GsccBridge.unhandledTasks = (WebTaskBundle) null;
    }

    public static void Reset() => GsccBridge.unhandledTasks = (WebTaskBundle) null;
  }
}
