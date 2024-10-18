// Decompiled with JetBrains decompiler
// Type: FlowNode_DestroyObject
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

[FlowNode.NodeType("Common/DestroyGameObject", 32741)]
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
