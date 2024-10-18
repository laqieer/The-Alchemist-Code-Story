// Decompiled with JetBrains decompiler
// Type: FlowNode_ToggleBlocksRaycasts
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

[FlowNode.NodeType("Toggle/BlocksRaycasts", 32741)]
[FlowNode.Pin(10, "Disable", FlowNode.PinTypes.Input, 0)]
[FlowNode.Pin(11, "Enable", FlowNode.PinTypes.Input, 0)]
[FlowNode.Pin(1, "Output", FlowNode.PinTypes.Output, 0)]
public class FlowNode_ToggleBlocksRaycasts : FlowNodePersistent
{
  [FlowNode.ShowInInfo]
  [FlowNode.DropTarget(typeof (GameObject), true)]
  public GameObject Target;

  public override void OnActivate(int pinID)
  {
    CanvasGroup canvasGroup = !((Object) this.Target != (Object) null) ? (CanvasGroup) null : this.Target.GetComponent<CanvasGroup>();
    switch (pinID)
    {
      case 10:
        if ((Object) canvasGroup != (Object) null)
        {
          canvasGroup.blocksRaycasts = false;
          break;
        }
        break;
      case 11:
        if ((Object) canvasGroup != (Object) null)
        {
          canvasGroup.blocksRaycasts = true;
          break;
        }
        break;
    }
    this.ActivateOutputLinks(1);
  }
}
