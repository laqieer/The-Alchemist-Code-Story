// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_UpdateParameter
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using UnityEngine;

namespace SRPG
{
  [FlowNode.Pin(101, "UpdateAll", FlowNode.PinTypes.Input, 2)]
  [FlowNode.NodeType("UI/UpdateParameter", 32741)]
  [FlowNode.Pin(100, "Update", FlowNode.PinTypes.Input, 0)]
  [AddComponentMenu("")]
  [FlowNode.Pin(1, "Updated", FlowNode.PinTypes.Output, 1)]
  public class FlowNode_UpdateParameter : FlowNode
  {
    [FlowNode.DropTarget(typeof (GameObject), false)]
    [FlowNode.ShowInInfo]
    public GameObject Target;

    public override void OnActivate(int pinID)
    {
      if (pinID != 100 && pinID != 101 || !((UnityEngine.Object) this.Target != (UnityEngine.Object) null))
        return;
      foreach (IGameParameter componentsInChild in this.Target.GetComponentsInChildren(typeof (IGameParameter), pinID == 101))
        componentsInChild.UpdateValue();
      this.ActivateOutputLinks(1);
    }
  }
}
