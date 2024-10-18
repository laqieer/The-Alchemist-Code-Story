// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_DebugReqReset
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using UnityEngine;

#nullable disable
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
        Network.RequestAPI((WebAPI) new ReqDebugDataReset(new Network.ResponseCallback(this.ResDebugDataReset)));
        this.mStateMachine.GotoState<FlowNode_DebugReqReset.State_WaitForConnect>();
        ((Behaviour) this).enabled = true;
      }
      else
      {
        ((Behaviour) this).enabled = false;
        this.ActivateOutputLinks(1);
      }
    }

    private void Success()
    {
      ((Behaviour) this).enabled = false;
      this.ActivateOutputLinks(2);
    }

    private void Failure()
    {
      ((Behaviour) this).enabled = false;
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
        bool flag1 = GameUtility.Config_UseAssetBundles.Value;
        bool flag2 = GameUtility.Config_UseEncryption.Value;
        bool flag3 = GameUtility.Config_UseSerializedParams.Value;
        GameUtility.ClearPreferences();
        GameUtility.Config_UseAssetBundles.Value = flag1;
        GameUtility.Config_UseEncryption.Value = flag2;
        GameUtility.Config_UseSerializedParams.Value = flag3;
        DebugUtility.Assert(www.text != null, "res == null");
        MonoSingleton<GameManager>.Instance.ResetAuth();
        GameUtility.Config_NewGame.Value = false;
        Network.RemoveAPI();
        this.Success();
      }
    }

    private class State_WaitForConnect : State<FlowNode_DebugReqReset>
    {
      public override void Update(FlowNode_DebugReqReset self)
      {
        if (!Network.IsConnecting)
          ;
      }
    }
  }
}
