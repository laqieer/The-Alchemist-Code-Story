// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_UpdateParameter
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

namespace SRPG
{
  [AddComponentMenu("")]
  [FlowNode.NodeType("UI/UpdateParameter", 32741)]
  [FlowNode.Pin(100, "Update", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(101, "UpdateAll", FlowNode.PinTypes.Input, 2)]
  [FlowNode.Pin(1, "Updated", FlowNode.PinTypes.Output, 1)]
  public class FlowNode_UpdateParameter : FlowNode
  {
    [FlowNode.ShowInInfo]
    [FlowNode.DropTarget(typeof (GameObject), false)]
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
