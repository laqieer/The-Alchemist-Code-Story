// Decompiled with JetBrains decompiler
// Type: FlowNode_GameObject
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;

#nullable disable
[AddComponentMenu("")]
[FlowNode.NodeType("Common/GameObject", 32741)]
public class FlowNode_GameObject : FlowNode
{
  [FlowNode.ShowInInfo(true)]
  public Component Target;

  public override FlowNode.Pin[] GetDynamicPins()
  {
    return Object.op_Inequality((Object) this.Target, (Object) null) ? (FlowNode.Pin[]) this.Target.GetType().GetCustomAttributes(typeof (FlowNode.Pin), true) : base.GetDynamicPins();
  }

  public override bool OnDragUpdate(object[] objectReferences) => false;

  public override bool OnDragPerform(object[] objectReferences) => false;

  public override void OnActivate(int pinID)
  {
    if (!Object.op_Inequality((Object) this.Target, (Object) null))
      return;
    ((IFlowInterface) this.Target)?.Activated(pinID);
  }

  public static void ActivateOutputLinks(Component caller, int pinID)
  {
    FlowNode_GameObject[] componentsInParent = caller.GetComponentsInParent<FlowNode_GameObject>();
    for (int index = 0; index < componentsInParent.Length; ++index)
    {
      if (Object.op_Equality((Object) componentsInParent[index].Target, (Object) caller))
        componentsInParent[index].ActivateOutputLinks(pinID);
    }
  }
}
