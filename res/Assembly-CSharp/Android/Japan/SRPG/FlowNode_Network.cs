﻿// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_Network
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;
using System;
using UnityEngine;

namespace SRPG
{
  [FlowNode.Pin(10001, "タイムアウト", FlowNode.PinTypes.Output, 10001)]
  [FlowNode.Pin(10002, "通信エラー", FlowNode.PinTypes.Output, 10002)]
  [FlowNode.Pin(10003, "メンテナンス中", FlowNode.PinTypes.Output, 10003)]
  [FlowNode.Pin(10004, "バージョン不一致", FlowNode.PinTypes.Output, 10004)]
  [FlowNode.Pin(10005, "セッションID無効", FlowNode.PinTypes.Output, 10005)]
  [FlowNode.Pin(10006, "API呼び出しパラメータ不正", FlowNode.PinTypes.Output, 10006)]
  [FlowNode.Pin(10007, "リトライ", FlowNode.PinTypes.Output, 10007)]
  [FlowNode.Pin(10008, "API呼び出し前の状態に戻る", FlowNode.PinTypes.Output, 10008)]
  public abstract class FlowNode_Network : FlowNode
  {
    public const string RetryWindowPrefabPath = "e/UI/NetworkRetryWindow";
    private StateMachine<FlowNode_Network> mStateMachine;

    public void ExecRequest(WebAPI api)
    {
      Network.RequestAPI(api, false);
      this.mStateMachine = new StateMachine<FlowNode_Network>(this);
      this.mStateMachine.GotoState<FlowNode_Network.State_WaitForConnect>();
    }

    private void Update()
    {
      if (this.mStateMachine == null)
        return;
      this.mStateMachine.Update();
    }

    public static bool HasCommonError(WWWResult www)
    {
      if (!Network.IsError)
        return false;
      if (Network.ErrCode != Network.EErrCode.Failed && Network.ErrCode != Network.EErrCode.TimeOut)
        DebugUtility.LogError("NetworkError: " + www.text);
      else
        DebugUtility.LogError("NetworkError: " + (object) Network.ErrCode + " : " + Network.ErrMsg);
      SRPG_InputField.ResetInput();
      Network.EErrCode errCode = Network.ErrCode;
      switch (errCode + 2)
      {
        case Network.EErrCode.Success:
          FlowNode_Network.Retry();
          break;
        case Network.EErrCode.Unknown:
          FlowNode_Network.Retry();
          break;
        case Network.EErrCode.AssetVersion:
          FlowNode_Network.Failed();
          break;
        case Network.EErrCode.NoVersionDbg:
          FlowNode_Network.Version();
          break;
        case Network.EErrCode.Unknown | Network.EErrCode.NoVersionDbg:
          FlowNode_Network.Failed();
          break;
        case Network.EErrCode.Version | Network.EErrCode.NoVersionDbg:
label_16:
          Network.IsNoVersion = true;
          FlowNode_Network.Version();
          break;
        default:
          switch (errCode)
          {
            case Network.EErrCode.NoSID:
            case Network.EErrCode.GauthNoSid:
              FlowNode_Network.SessionID();
              break;
            case Network.EErrCode.Maintenance:
              FlowNode_Network.Maintenance();
              break;
            case Network.EErrCode.IllegalParam:
              FlowNode_Network.Retry();
              break;
            case Network.EErrCode.NoVersion:
              goto label_16;
            case Network.EErrCode.ReturnForceTitle:
              FlowNode_Network.Relogin();
              break;
            default:
              return false;
          }
      }
      return true;
    }

    public void ResponseCallback(WWWResult www)
    {
      if (FlowNode_Network.HasCommonError(www))
        return;
      this.OnSuccess(www);
    }

    public int OnTimeOutPinIndex
    {
      get
      {
        return 10001;
      }
    }

    public int OnFailedPinIndex
    {
      get
      {
        return 10002;
      }
    }

    public int OnMaintenancePinIndex
    {
      get
      {
        return 10003;
      }
    }

    public int OnVersionPinIndex
    {
      get
      {
        return 10004;
      }
    }

    public int OnSessionIDPinIndex
    {
      get
      {
        return 10005;
      }
    }

    public int OnIllegalParamPinIndex
    {
      get
      {
        return 10006;
      }
    }

    public int OnRetryPinIndex
    {
      get
      {
        return 10007;
      }
    }

    public int OnBackPinIndex
    {
      get
      {
        return 10008;
      }
    }

    public static void TimeOut()
    {
      Network.RequestResult = Network.RequestResults.Timeout;
      if (Network.IsImmediateMode)
        return;
      Network.RemoveAPI();
      Network.ResetError();
    }

    public virtual void OnTimeOut()
    {
      FlowNode_Network.TimeOut();
      this.enabled = false;
      this.ActivateOutputLinks(this.OnTimeOutPinIndex);
    }

    public static void Failed()
    {
      Network.RequestResult = Network.RequestResults.Timeout;
      if (Network.IsImmediateMode)
        return;
      Network.RemoveAPI();
      Network.ResetError();
      GlobalEvent.Invoke(PredefinedGlobalEvents.ERROR_NETWORK.ToString(), (object) null);
    }

    public virtual void OnFailed()
    {
      FlowNode_Network.Failed();
      this.enabled = false;
      this.ActivateOutputLinks(this.OnFailedPinIndex);
    }

    public static void Maintenance()
    {
      Network.RequestResult = Network.RequestResults.Maintenance;
      if (Network.IsImmediateMode)
        return;
      Network.RemoveAPI();
      Network.ResetError();
      GlobalEvent.Invoke(PredefinedGlobalEvents.MAINTENANCE_NETWORK.ToString(), (object) null);
    }

    public virtual void OnMaintenance()
    {
      FlowNode_Network.Maintenance();
      this.enabled = false;
      this.ActivateOutputLinks(this.OnMaintenancePinIndex);
    }

    public static void Version()
    {
      Network.RequestResult = Network.RequestResults.VersionMismatch;
      if (Network.IsImmediateMode)
        return;
      Network.RemoveAPI();
      Network.ResetError();
      GlobalEvent.Invoke(PredefinedGlobalEvents.VERSION_MISMATCH_NETWORK.ToString(), (object) null);
    }

    public virtual void OnVersion()
    {
      FlowNode_Network.Version();
      this.enabled = false;
      this.ActivateOutputLinks(this.OnVersionPinIndex);
    }

    public static void SessionID()
    {
      Network.RequestResult = Network.RequestResults.InvalidSession;
      Network.RemoveAPI();
      Network.ResetError();
      GlobalEvent.Invoke(PredefinedGlobalEvents.ERROR_NETWORK.ToString(), (object) null);
    }

    public static void Relogin()
    {
      if (MonoSingleton<GameManager>.Instance.IsLogin)
        MonoSingleton<GameManager>.Instance.IsRelogin = true;
      Network.RequestResult = Network.RequestResults.InvalidSession;
      Network.RemoveAPI();
      Network.ResetError();
      GlobalEvent.Invoke(PredefinedGlobalEvents.ERROR_NETWORK.ToString(), (object) null);
    }

    public virtual void OnSessionID()
    {
      FlowNode_Network.SessionID();
      this.enabled = false;
      this.ActivateOutputLinks(this.OnSessionIDPinIndex);
    }

    public static void IllegalParam()
    {
      Network.RequestResult = Network.RequestResults.IllegalParam;
      Network.RemoveAPI();
      Network.ResetError();
    }

    public virtual void OnIllegalParam()
    {
      this.enabled = false;
      this.ActivateOutputLinks(this.OnIllegalParamPinIndex);
    }

    protected void OnRetry(Exception reason)
    {
      DebugUtility.LogException(reason);
      this.OnRetry();
    }

    public static void Retry()
    {
      Network.RequestResult = Network.RequestResults.Retry;
      if (Network.IsImmediateMode)
        return;
      FlowNode_Network.CloseWebView();
      NetworkRetryWindow networkRetryWindow1 = UnityEngine.Object.Instantiate<NetworkRetryWindow>(Resources.Load<NetworkRetryWindow>("e/UI/NetworkRetryWindow"));
      NetworkRetryWindow networkRetryWindow2 = networkRetryWindow1;
      // ISSUE: reference to a compiler-generated field
      if (FlowNode_Network.\u003C\u003Ef__mg\u0024cache0 == null)
      {
        // ISSUE: reference to a compiler-generated field
        FlowNode_Network.\u003C\u003Ef__mg\u0024cache0 = new NetworkRetryWindow.RetryWindowEvent(FlowNode_Network.RetryEvent);
      }
      // ISSUE: reference to a compiler-generated field
      NetworkRetryWindow.RetryWindowEvent fMgCache0 = FlowNode_Network.\u003C\u003Ef__mg\u0024cache0;
      networkRetryWindow2.Delegate = fMgCache0;
      networkRetryWindow1.Body = Network.ErrMsg;
    }

    public virtual void OnRetry()
    {
      FlowNode_Network.Retry();
      this.enabled = false;
      this.ActivateOutputLinks(this.OnRetryPinIndex);
    }

    private static void RetryEvent(bool retry)
    {
      if (retry)
      {
        Network.ResetError();
        Network.SetRetry();
      }
      else
      {
        Network.RemoveAPI();
        Network.ResetError();
        FlowNode_LoadScene.LoadBootScene();
      }
    }

    public static void Back()
    {
      Network.RequestResult = Network.RequestResults.Back;
      if (Network.IsImmediateMode)
        return;
      Network.RemoveAPI();
      GlobalEvent.Invoke(PredefinedGlobalEvents.BACK_NETWORK.ToString(), (object) null);
      Network.ResetError();
    }

    public virtual void OnBack()
    {
      FlowNode_Network.Back();
      this.enabled = false;
      this.ActivateOutputLinks(this.OnBackPinIndex);
    }

    public static void CloseWebView()
    {
      GlobalEvent.Invoke("WEBVIEW_DELETE", (object) 1);
    }

    public abstract void OnSuccess(WWWResult www);

    public static void ErrorAppQuit()
    {
      Network.RequestResult = Network.RequestResults.InvalidSession;
      Network.RemoveAPI();
      Network.ResetError();
      GlobalEvent.Invoke(PredefinedGlobalEvents.ERROR_APP_QUIT.ToString(), (object) null);
    }

    public virtual void OnErrorAppQuit()
    {
      FlowNode_Network.SessionID();
      this.enabled = false;
      this.ActivateOutputLinks(this.OnSessionIDPinIndex);
    }

    private class State_WaitForConnect : State<FlowNode_Network>
    {
      public override void Update(FlowNode_Network self)
      {
        if (Network.IsConnecting)
          ;
      }
    }
  }
}
