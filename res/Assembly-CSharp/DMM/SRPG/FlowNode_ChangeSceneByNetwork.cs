// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_ChangeSceneByNetwork
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

#nullable disable
namespace SRPG
{
  [FlowNode.NodeType("Network/ChangeSceneByNetwork", 32741)]
  [FlowNode.Pin(0, "Input", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(10, "なにもしない", FlowNode.PinTypes.Output, 10)]
  [FlowNode.Pin(11, "強制的にホームに遷移", FlowNode.PinTypes.Output, 11)]
  [FlowNode.Pin(12, "強制的にシーンを再読み込み", FlowNode.PinTypes.Output, 12)]
  public class FlowNode_ChangeSceneByNetwork : FlowNode
  {
    private const int PIN_IN_INPUT = 0;
    private const int PIN_OUT_DO_NOTHING = 10;
    private const int PIN_OUT_GO_TO_HOME = 11;
    private const int PIN_OUT_RELOAD_SCENE = 12;

    public override void OnActivate(int pinID)
    {
      if (pinID == 0)
      {
        switch (Network.RequestResult)
        {
          case Network.RequestResults.ServerNotify:
            this.ActivateOutputLinks(10);
            return;
          case Network.RequestResults.ServerNotifyAndGoToHome:
            this.ActivateOutputLinks(11);
            return;
          case Network.RequestResults.ServerNotifyAndReloadScene:
            this.ActivateOutputLinks(12);
            return;
        }
      }
      this.ActivateOutputLinks(10);
    }
  }
}
