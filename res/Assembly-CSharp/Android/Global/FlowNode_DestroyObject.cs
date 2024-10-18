// Decompiled with JetBrains decompiler
// Type: FlowNode_DestroyObject
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using UnityEngine;

[FlowNode.NodeType("Destroy", 32741)]
[FlowNode.Pin(10, "Destroy", FlowNode.PinTypes.Input, 0)]
public class FlowNode_DestroyObject : FlowNode
{
  [FlowNode.ShowInInfo]
  [FlowNode.DropTarget(typeof (GameObject), false)]
  public GameObject Target;

  public override void OnActivate(int pinID)
  {
    if (!((Object) this.Target != (Object) null))
      return;
    Object.Destroy((Object) this.Target.gameObject);
  }
}
