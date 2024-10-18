// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_MultiPlayTestRoomPassCode
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

#nullable disable
namespace SRPG
{
  [FlowNode.NodeType("Multi/MultiPlayTestRoomPassCode", 32741)]
  [FlowNode.Pin(0, "Test", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(1, "OK", FlowNode.PinTypes.Output, 0)]
  [FlowNode.Pin(2, "NG", FlowNode.PinTypes.Output, 0)]
  public class FlowNode_MultiPlayTestRoomPassCode : FlowNode
  {
    public override void OnActivate(int pinID)
    {
      if (pinID != 0)
        return;
      if (string.IsNullOrEmpty(GlobalVars.SelectedMultiPlayRoomPassCodeHash))
      {
        this.ActivateOutputLinks(1);
      }
      else
      {
        string str = MultiPlayAPIRoom.CalcHash(GlobalVars.EditMultiPlayRoomPassCode);
        DebugUtility.Log("CheckPass...:" + GlobalVars.EditMultiPlayRoomPassCode + " > " + str + " vs " + GlobalVars.SelectedMultiPlayRoomPassCodeHash);
        if (GlobalVars.SelectedMultiPlayRoomPassCodeHash.Equals(str))
          this.ActivateOutputLinks(1);
        else
          this.ActivateOutputLinks(2);
      }
    }
  }
}
