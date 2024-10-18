// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_SetTrophyType
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

namespace SRPG
{
  [FlowNode.Pin(100, "Daily", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(1, "Success", FlowNode.PinTypes.Output, 10)]
  [FlowNode.Pin(101, "Record", FlowNode.PinTypes.Input, 1)]
  [FlowNode.NodeType("System/SetTrophyType", 32741)]
  public class FlowNode_SetTrophyType : FlowNode
  {
    public override void OnActivate(int pinID)
    {
      switch (pinID)
      {
        case 100:
          GlobalVars.SelectedTrophyType = TrophyType.Daily;
          break;
        case 101:
          GlobalVars.SelectedTrophyType = TrophyType.Record;
          break;
      }
      this.enabled = false;
      this.ActivateOutputLinks(1);
    }
  }
}
