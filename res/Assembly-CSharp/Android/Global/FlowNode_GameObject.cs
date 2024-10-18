// Decompiled with JetBrains decompiler
// Type: FlowNode_GameObject
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using UnityEngine;

[FlowNode.NodeType("GameObject", 32741)]
[AddComponentMenu("")]
public class FlowNode_GameObject : FlowNode
{
  [FlowNode.ShowInInfo(true)]
  public Component Target;

  public override FlowNode.Pin[] GetDynamicPins()
  {
    if ((Object) this.Target != (Object) null)
      return (FlowNode.Pin[]) ((object) this.Target).GetType().GetCustomAttributes(typeof (FlowNode.Pin), true);
    return base.GetDynamicPins();
  }

  public override bool OnDragUpdate(object[] objectReferences)
  {
    return false;
  }

  public override bool OnDragPerform(object[] objectReferences)
  {
    return false;
  }

  public override void OnActivate(int pinID)
  {
    if (!((Object) this.Target != (Object) null))
      return;
    ((IFlowInterface) this.Target)?.Activated(pinID);
  }

  public static void ActivateOutputLinks(Component caller, int pinID)
  {
    FlowNode_GameObject[] componentsInParent = caller.GetComponentsInParent<FlowNode_GameObject>();
    for (int index = 0; index < componentsInParent.Length; ++index)
    {
      if ((Object) componentsInParent[index].Target == (Object) caller)
        componentsInParent[index].ActivateOutputLinks(pinID);
    }
  }
}
