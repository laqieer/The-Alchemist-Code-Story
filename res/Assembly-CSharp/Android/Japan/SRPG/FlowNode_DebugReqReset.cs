// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_DebugReqReset
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;

namespace SRPG
{
  [FlowNode.NodeType("Debug/サーバのデータを削除", 32741)]
  [FlowNode.Pin(0, "Reset", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(1, "Success_Offline", FlowNode.PinTypes.Output, 1)]
  [FlowNode.Pin(2, "Success_Online", FlowNode.PinTypes.Output, 2)]
  [FlowNode.Pin(2, "Error", FlowNode.PinTypes.Output, 3)]
  public class FlowNode_DebugReqReset : FlowNode
  {
    private StateMachine<FlowNode_DebugReqReset> mStateMachine;

    public override void OnActivate(int pinID)
    {
      if (pinID != 0)
        return;
      this.mStateMachine = new StateMachine<FlowNode_DebugReqReset>(this);
      if (Network.Mode == Network.EConnectMode.Online)
      {
        Network.RequestAPI((WebAPI) new ReqDebugDataReset(new Network.ResponseCallback(this.ResDebugDataReset)), false);
        this.mStateMachine.GotoState<FlowNode_DebugReqReset.State_WaitForConnect>();
        this.enabled = true;
      }
      else
      {
        this.enabled = false;
        this.ActivateOutputLinks(1);
      }
    }

    private void Success()
    {
      this.enabled = false;
      this.ActivateOutputLinks(2);
    }

    private void Failure()
    {
      this.enabled = false;
      this.ActivateOutputLinks(3);
    }

    private void Update()
    {
      if (this.mStateMachine == null)
        return;
      this.mStateMachine.Update();
    }

    public void ResDebugDataReset(WWWResult www)
    {
      if (Network.IsError)
      {
        Network.IsRetry = true;
      }
      else
      {
        bool flag = GameUtility.Config_UseAssetBundles.Value;
        GameUtility.ClearPreferences();
        GameUtility.Config_UseAssetBundles.Value = flag;
        DebugUtility.Assert(www.text != null, "res == null");
        MonoSingleton<GameManager>.Instance.ResetAuth();
        GameUtility.Config_NewGame.Value = false;
        MetapsAnalyticsScript.ReInitialize();
        Network.RemoveAPI();
        this.Success();
      }
    }

    private class State_WaitForConnect : State<FlowNode_DebugReqReset>
    {
      public override void Update(FlowNode_DebugReqReset self)
      {
        if (Network.IsConnecting)
          ;
      }
    }
  }
}
