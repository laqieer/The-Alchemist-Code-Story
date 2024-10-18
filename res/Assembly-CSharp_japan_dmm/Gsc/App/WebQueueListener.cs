// Decompiled with JetBrains decompiler
// Type: Gsc.App.WebQueueListener
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using Gsc.App.NetworkHelper;
using Gsc.Network;
using SRPG;
using System.Collections.Generic;

#nullable disable
namespace Gsc.App
{
  public class WebQueueListener : IWebQueueObserver
  {
    public static byte[] ErrorPayload { get; set; }

    public void OnStart() => CriticalSection.Enter(CriticalSections.Network);

    public void OnFinish() => CriticalSection.Leave(CriticalSections.Network);

    public void Reset()
    {
    }

    public void OnReceiveUnhandledTasks(WebTaskBundle taskBundle)
    {
      GsccBridge.OnReceiveUnhandledTasks(taskBundle);
      foreach (IWebTask webTask in taskBundle)
      {
        if (!(webTask is WebRequest) && webTask.error != null)
        {
          WebResponse webResponse = new WebResponse(webTask.error, Gsc.Network.ContentType.ApplicationJson, ContentEncoding.None);
          SRPG.Network.SetServerMetaDataAsError(webResponse.ErrorCode, webResponse.ErrorMessage);
          if (FlowNode_Network.HasCommonError(webResponse.Result))
            return;
        }
      }
      if (taskBundle.HasResult(WebTaskResult.InternalCheckMaintenance))
      {
        SRPG.Network.ResetError();
        SRPG.Network.MenteCheckFlag = true;
        SRPG.Network.SetRetry();
      }
      else if (taskBundle.HasResult(WebTaskResult.InvalidChkver2Response))
      {
        SRPG.Network.DoChkver2InJson = true;
        SRPG.Network.SetRetry();
      }
      else if (taskBundle.HasResult(WebTaskResult.ServerError))
      {
        SRPG.Network.SetServerMetaDataAsError();
        FlowNode_Network.Retry();
      }
      else if (taskBundle.HasResult(WebTaskResult.Maintenance))
      {
        SRPG.Network.SetServerMetaDataAsError(SRPG.Network.EErrCode.Maintenance, (string) null);
        FlowNode_Network.Maintenance();
      }
      else if (taskBundle.HasResult(WebTaskResult.ExpiredSessionError))
      {
        SRPG.Network.SetServerSessionExpired();
        FlowNode_Network.ErrorAppQuit();
      }
      else if (taskBundle.HasResult(WebTaskResult.InvalidDeviceError))
      {
        SRPG.Network.SetServerInvalidDeviceError();
        FlowNode_Network.Failed();
      }
      else
      {
        if (WebQueueListener.ErrorPayload != null)
        {
          WebResponse webResponse = new WebResponse(WebQueueListener.ErrorPayload, Gsc.Network.ContentType.ApplicationJson, ContentEncoding.None);
          SRPG.Network.SetServerMetaDataAsError(webResponse.ErrorCode, webResponse.ErrorMessage);
          WebQueueListener.ErrorPayload = (byte[]) null;
          if (FlowNode_Network.HasCommonError(webResponse.Result))
            return;
        }
        else
          SRPG.Network.SetServerMetaDataAsError();
        FlowNode_Network.Failed();
      }
    }

    public static void UnityErrorLogCallback(
      CustomHeaders customHeaders,
      Dictionary<string, object> user,
      Dictionary<string, object> tags,
      Dictionary<string, object> extra)
    {
    }
  }
}
