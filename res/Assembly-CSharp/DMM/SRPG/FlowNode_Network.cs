// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_Network
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using Gsc.Network.Encoding;
using System;
using UnityEngine;

#nullable disable
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
    private EncodingTypes.ESerializeCompressMethod __serializeCompressMethod;
    private StateMachine<FlowNode_Network> mStateMachine;

    protected EncodingTypes.ESerializeCompressMethod SerializeCompressMethod
    {
      get
      {
        return GlobalVars.SelectedSerializeCompressMethodWasNodeSet ? GlobalVars.SelectedSerializeCompressMethod : this.__serializeCompressMethod;
      }
      set => this.__serializeCompressMethod = value;
    }

    public void ExecRequest(WebAPI api)
    {
      SRPG.Network.RequestAPI(api);
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
      if (!SRPG.Network.IsError)
        return false;
      if (SRPG.Network.ErrCode != SRPG.Network.EErrCode.Failed && SRPG.Network.ErrCode != SRPG.Network.EErrCode.TimeOut)
        DebugUtility.LogError("NetworkError: " + www.text);
      else
        DebugUtility.LogError("NetworkError: " + (object) SRPG.Network.ErrCode + " : " + SRPG.Network.ErrMsg);
      SRPG_InputField.ResetInput();
      SRPG.Network.EErrCode errCode = SRPG.Network.ErrCode;
      switch (errCode + 2)
      {
        case SRPG.Network.EErrCode.Success:
          FlowNode_Network.Retry();
          break;
        case SRPG.Network.EErrCode.Unknown:
          FlowNode_Network.Retry();
          break;
        case SRPG.Network.EErrCode.AssetVersion:
          FlowNode_Network.Failed();
          break;
        case SRPG.Network.EErrCode.NoVersionDbg:
          FlowNode_Network.Version();
          break;
        case SRPG.Network.EErrCode.Unknown | SRPG.Network.EErrCode.NoVersionDbg:
          FlowNode_Network.Failed();
          break;
        case SRPG.Network.EErrCode.Version | SRPG.Network.EErrCode.NoVersionDbg:
label_16:
          SRPG.Network.IsNoVersion = true;
          FlowNode_Network.Version();
          break;
        default:
          switch (errCode)
          {
            case SRPG.Network.EErrCode.NoSID:
            case SRPG.Network.EErrCode.GauthNoSid:
              FlowNode_Network.SessionID();
              break;
            case SRPG.Network.EErrCode.Maintenance:
              FlowNode_Network.Maintenance();
              break;
            case SRPG.Network.EErrCode.IllegalParam:
              FlowNode_Network.Retry();
              break;
            case SRPG.Network.EErrCode.ServerNotify:
            case SRPG.Network.EErrCode.ServerNotifyAndGoToHome:
            case SRPG.Network.EErrCode.ServerNotifyAndReloadScene:
              FlowNode_Network.ServerNotify(SRPG.Network.ErrCode);
              break;
            case SRPG.Network.EErrCode.NoVersion:
              goto label_16;
            case SRPG.Network.EErrCode.ReturnForceTitle:
              FlowNode_Network.Relogin();
              break;
            default:
              return false;
          }
          break;
      }
      return true;
    }

    public void ResponseCallback(WWWResult www)
    {
      if (FlowNode_Network.HasCommonError(www))
        return;
      this.OnSuccess(www);
    }

    public int OnTimeOutPinIndex => 10001;

    public int OnFailedPinIndex => 10002;

    public int OnMaintenancePinIndex => 10003;

    public int OnVersionPinIndex => 10004;

    public int OnSessionIDPinIndex => 10005;

    public int OnIllegalParamPinIndex => 10006;

    public int OnRetryPinIndex => 10007;

    public int OnBackPinIndex => 10008;

    public static void TimeOut()
    {
      SRPG.Network.RequestResult = SRPG.Network.RequestResults.Timeout;
      if (SRPG.Network.IsImmediateMode)
        return;
      SRPG.Network.RemoveAPI();
      SRPG.Network.ResetError();
    }

    public virtual void OnTimeOut()
    {
      FlowNode_Network.TimeOut();
      ((Behaviour) this).enabled = false;
      this.ActivateOutputLinks(this.OnTimeOutPinIndex);
    }

    public static void Failed()
    {
      SRPG.Network.RequestResult = SRPG.Network.RequestResults.Timeout;
      if (SRPG.Network.IsImmediateMode)
        return;
      SRPG.Network.RemoveAPI();
      SRPG.Network.ResetError();
      GlobalEvent.Invoke(PredefinedGlobalEvents.ERROR_NETWORK.ToString(), (object) null);
    }

    public virtual void OnFailed()
    {
      FlowNode_Network.Failed();
      ((Behaviour) this).enabled = false;
      this.ActivateOutputLinks(this.OnFailedPinIndex);
    }

    public static void Maintenance()
    {
      SRPG.Network.RequestResult = SRPG.Network.RequestResults.Maintenance;
      if (SRPG.Network.IsImmediateMode)
        return;
      SRPG.Network.RemoveAPI();
      SRPG.Network.ResetError();
      GlobalEvent.Invoke(PredefinedGlobalEvents.MAINTENANCE_NETWORK.ToString(), (object) null);
    }

    public virtual void OnMaintenance()
    {
      FlowNode_Network.Maintenance();
      ((Behaviour) this).enabled = false;
      this.ActivateOutputLinks(this.OnMaintenancePinIndex);
    }

    public static void Version()
    {
      SRPG.Network.RequestResult = SRPG.Network.RequestResults.VersionMismatch;
      if (SRPG.Network.IsImmediateMode)
        return;
      SRPG.Network.RemoveAPI();
      SRPG.Network.ResetError();
      GlobalEvent.Invoke(PredefinedGlobalEvents.VERSION_MISMATCH_NETWORK.ToString(), (object) null);
    }

    public virtual void OnVersion()
    {
      FlowNode_Network.Version();
      ((Behaviour) this).enabled = false;
      this.ActivateOutputLinks(this.OnVersionPinIndex);
    }

    public static void SessionID()
    {
      SRPG.Network.RequestResult = SRPG.Network.RequestResults.InvalidSession;
      SRPG.Network.RemoveAPI();
      SRPG.Network.ResetError();
      GlobalEvent.Invoke(PredefinedGlobalEvents.ERROR_NETWORK.ToString(), (object) null);
    }

    public static void Relogin()
    {
      if (MonoSingleton<GameManager>.Instance.IsLogin)
        MonoSingleton<GameManager>.Instance.IsRelogin = true;
      SRPG.Network.RequestResult = SRPG.Network.RequestResults.InvalidSession;
      SRPG.Network.RemoveAPI();
      SRPG.Network.ResetError();
      GlobalEvent.Invoke(PredefinedGlobalEvents.ERROR_NETWORK.ToString(), (object) null);
    }

    public virtual void OnSessionID()
    {
      FlowNode_Network.SessionID();
      ((Behaviour) this).enabled = false;
      this.ActivateOutputLinks(this.OnSessionIDPinIndex);
    }

    public static void IllegalParam()
    {
      SRPG.Network.RequestResult = SRPG.Network.RequestResults.IllegalParam;
      SRPG.Network.RemoveAPI();
      SRPG.Network.ResetError();
    }

    public virtual void OnIllegalParam()
    {
      ((Behaviour) this).enabled = false;
      this.ActivateOutputLinks(this.OnIllegalParamPinIndex);
    }

    protected void OnRetry(Exception reason)
    {
      DebugUtility.LogException(reason);
      this.OnRetry();
    }

    public static void Retry()
    {
      SRPG.Network.RequestResult = SRPG.Network.RequestResults.Retry;
      if (SRPG.Network.IsImmediateMode)
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
      networkRetryWindow1.Body = SRPG.Network.ErrMsg;
    }

    public virtual void OnRetry()
    {
      FlowNode_Network.Retry();
      ((Behaviour) this).enabled = false;
      this.ActivateOutputLinks(this.OnRetryPinIndex);
    }

    private static void RetryEvent(bool retry)
    {
      if (retry)
      {
        SRPG.Network.ResetError();
        SRPG.Network.SetRetry();
      }
      else
      {
        SRPG.Network.RemoveAPI();
        SRPG.Network.ResetError();
        FlowNode_LoadScene.LoadBootScene();
      }
    }

    public static void Back()
    {
      SRPG.Network.RequestResult = SRPG.Network.RequestResults.Back;
      if (SRPG.Network.IsImmediateMode)
        return;
      SRPG.Network.RemoveAPI();
      GlobalEvent.Invoke(PredefinedGlobalEvents.BACK_NETWORK.ToString(), (object) null);
      SRPG.Network.ResetError();
    }

    public virtual void OnBack()
    {
      FlowNode_Network.Back();
      ((Behaviour) this).enabled = false;
      this.ActivateOutputLinks(this.OnBackPinIndex);
    }

    public static void CloseWebView() => GlobalEvent.Invoke("WEBVIEW_DELETE", (object) 1);

    public abstract void OnSuccess(WWWResult www);

    public static void ErrorAppQuit()
    {
      SRPG.Network.RequestResult = SRPG.Network.RequestResults.InvalidSession;
      SRPG.Network.RemoveAPI();
      SRPG.Network.ResetError();
      GlobalEvent.Invoke(PredefinedGlobalEvents.ERROR_APP_QUIT.ToString(), (object) null);
    }

    public virtual void OnErrorAppQuit()
    {
      FlowNode_Network.SessionID();
      ((Behaviour) this).enabled = false;
      this.ActivateOutputLinks(this.OnSessionIDPinIndex);
    }

    public static void ServerNotify(SRPG.Network.EErrCode error)
    {
      switch (error)
      {
        case SRPG.Network.EErrCode.ServerNotifyAndGoToHome:
          SRPG.Network.RequestResult = SRPG.Network.RequestResults.ServerNotifyAndGoToHome;
          break;
        case SRPG.Network.EErrCode.ServerNotifyAndReloadScene:
          SRPG.Network.RequestResult = SRPG.Network.RequestResults.ServerNotifyAndReloadScene;
          break;
        default:
          SRPG.Network.RequestResult = SRPG.Network.RequestResults.ServerNotify;
          break;
      }
      SRPG.Network.RemoveAPI();
      SRPG.Network.ResetError();
      GlobalEvent.Invoke(PredefinedGlobalEvents.SERVER_NOTIFY.ToString(), (object) null);
    }

    private class State_WaitForConnect : State<FlowNode_Network>
    {
      public override void Update(FlowNode_Network self)
      {
        if (!SRPG.Network.IsConnecting)
          ;
      }
    }
  }
}
