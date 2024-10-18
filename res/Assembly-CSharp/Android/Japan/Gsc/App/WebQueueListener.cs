﻿// Decompiled with JetBrains decompiler
// Type: Gsc.App.WebQueueListener
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

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
          WebResponse webResponse = new WebResponse(WebQueueListener.ErrorPayload);
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

    public static void UnityErrorLogCallback(CustomHeaders customHeaders, Dictionary<string, object> user, Dictionary<string, object> tags, Dictionary<string, object> extra)
    {
    }
  }
}
