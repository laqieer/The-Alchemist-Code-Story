// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_MoveGameObject
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using UnityEngine;

namespace SRPG
{
  [FlowNode.NodeType("MoveGameObject", 32741)]
  [FlowNode.Pin(0, "In", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(1, "Out", FlowNode.PinTypes.Output, 1)]
  public class FlowNode_MoveGameObject : FlowNode
  {
    public float Time = 1f;
    public GameObject Target;
    public GameObject Destination;
    public ObjectAnimator.CurveType InterpolationMode;

    public override void OnActivate(int pinID)
    {
      if (pinID != 0)
        return;
      if ((UnityEngine.Object) this.Target != (UnityEngine.Object) null && (UnityEngine.Object) this.Destination != (UnityEngine.Object) null)
      {
        Transform transform = this.Destination.transform;
        ObjectAnimator.Get(this.Target).AnimateTo(transform.position, transform.rotation, this.Time, this.InterpolationMode);
      }
      this.ActivateOutputLinks(1);
    }
  }
}
