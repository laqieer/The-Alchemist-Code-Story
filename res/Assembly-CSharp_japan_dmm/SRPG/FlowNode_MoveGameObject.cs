// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_MoveGameObject
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;

#nullable disable
namespace SRPG
{
  [FlowNode.NodeType("Common/MoveGameObject", 32741)]
  [FlowNode.Pin(0, "In", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(1, "Out", FlowNode.PinTypes.Output, 1)]
  public class FlowNode_MoveGameObject : FlowNode
  {
    public GameObject Target;
    public GameObject Destination;
    public float Time = 1f;
    public ObjectAnimator.CurveType InterpolationMode;

    public override void OnActivate(int pinID)
    {
      if (pinID != 0)
        return;
      if (Object.op_Inequality((Object) this.Target, (Object) null) && Object.op_Inequality((Object) this.Destination, (Object) null))
      {
        Transform transform = this.Destination.transform;
        ObjectAnimator.Get(this.Target).AnimateTo(transform.position, transform.rotation, this.Time, this.InterpolationMode);
      }
      this.ActivateOutputLinks(1);
    }
  }
}
