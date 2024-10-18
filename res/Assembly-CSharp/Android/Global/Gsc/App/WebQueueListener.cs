// Decompiled with JetBrains decompiler
// Type: Gsc.App.WebQueueListener
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using Gsc.App.NetworkHelper;
using Gsc.Network;
using SRPG;
using System.Collections.Generic;

namespace Gsc.App
{
  public class WebQueueListener : IWebQueueObserver
  {
    public static byte[] ErrorPayload { get; set; }

    public void OnStart()
    {
      CriticalSection.Enter(CriticalSections.Network);
    }

    public void OnFinish()
    {
      CriticalSection.Leave(CriticalSections.Network);
    }

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
          WebResponse webResponse = new WebResponse(webTask.error);
          SRPG.Network.SetServerMetaDataAsError(webResponse.ErrorCode, webResponse.ErrorMessage);
          SRPG.Network.SetServerMaintainenceMessage(webResponse.MultiLanguage_Message);
          if (FlowNode_Network.HasCommonError(webResponse.Result))
            return;
        }
      }
      if (taskBundle.HasResult(WebTaskResult.ServerError))
      {
        SRPG.Network.SetServerMetaDataAsError();
        FlowNode_Network.Retry();
      }
      else if (taskBundle.HasResult(WebTaskResult.Maintenance))
      {
        if (WebQueueListener.ErrorPayload != null)
        {
          WebResponse webResponse = new WebResponse(WebQueueListener.ErrorPayload);
          SRPG.Network.SetServerMetaDataAsError(SRPG.Network.EErrCode.Maintenance, webResponse.ErrorMessage);
          SRPG.Network.SetServerMaintainenceMessage(webResponse.MultiLanguage_Message);
        }
        else
        {
          SRPG.Network.SetServerMetaDataAsError(SRPG.Network.EErrCode.Maintenance, (string) null);
          SRPG.Network.SetServerMaintainenceMessage((SRPG.Network.MultiLanguage_Message) null);
        }
        FlowNode_Network.Maintenance();
      }
      else if (taskBundle.HasResult(WebTaskResult.ExpiredSessionError))
      {
        SRPG.Network.SetServerMetaDataAsError();
        FlowNode_Network.Failed();
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
          WebResponse webResponse = new WebResponse(WebQueueListener.ErrorPayload);
          SRPG.Network.SetServerMetaDataAsError(webResponse.ErrorCode, webResponse.ErrorMessage);
          SRPG.Network.SetServerMaintainenceMessage(webResponse.MultiLanguage_Message);
          WebQueueListener.ErrorPayload = (byte[]) null;
          if (FlowNode_Network.HasCommonError(webResponse.Result))
            return;
        }
        else
          SRPG.Network.SetServerMetaDataAsError();
        FlowNode_Network.Failed();
      }
    }

    public static void UnityErrorLogCallback(CustomHeaders customHeaders, Dictionary<string, object> user, Dictionary<string, object> tags, Dictionary<string, object> extra)
    {
    }
  }
}
