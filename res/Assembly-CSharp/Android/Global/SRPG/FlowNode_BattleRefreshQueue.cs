// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_BattleRefreshQueue
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

namespace SRPG
{
  [FlowNode.Pin(1, "Out", FlowNode.PinTypes.Output, 1)]
  [FlowNode.NodeType("Battle/RefreshQueue", 32741)]
  [FlowNode.Pin(0, "行動順更新", FlowNode.PinTypes.Input, 0)]
  public class FlowNode_BattleRefreshQueue : FlowNode
  {
    public override void OnActivate(int pinID)
    {
      if (pinID != 0 || !((UnityEngine.Object) UnitQueue.Instance != (UnityEngine.Object) null))
        return;
      UnitQueue.Instance.Refresh(0);
      this.ActivateOutputLinks(1);
    }
  }
}
