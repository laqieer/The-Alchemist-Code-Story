// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_MultiPlayUpdatePlayerParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

namespace SRPG
{
  [FlowNode.Pin(101, "Update", FlowNode.PinTypes.Input, 0)]
  [FlowNode.NodeType("Multi/MultiPlayUpdatePlayerParam", 32741)]
  [FlowNode.Pin(2, "Failure", FlowNode.PinTypes.Output, 0)]
  [FlowNode.Pin(1, "Success", FlowNode.PinTypes.Output, 0)]
  public class FlowNode_MultiPlayUpdatePlayerParam : FlowNode
  {
    public override void OnActivate(int pinID)
    {
      if (pinID != 101)
        return;
      MyPhoton instance = PunMonoSingleton<MyPhoton>.Instance;
      MyPhoton.MyPlayer myPlayer = instance.GetMyPlayer();
      if (myPlayer == null)
      {
        this.ActivateOutputLinks(2);
      }
      else
      {
        JSON_MyPhotonPlayerParam photonPlayerParam = JSON_MyPhotonPlayerParam.Create(myPlayer.playerID, instance.MyPlayerIndex);
        photonPlayerParam.UpdateMultiTowerPlacement(false);
        instance.SetMyPlayerParam(photonPlayerParam.Serialize());
        this.ActivateOutputLinks(1);
      }
    }
  }
}
