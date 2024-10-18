// Decompiled with JetBrains decompiler
// Type: FlowNode_OnPointerRelease
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

[FlowNode.NodeType("Event/OnPointerRelease", 58751)]
[FlowNode.Pin(0, "Released", FlowNode.PinTypes.Output, 0)]
public class FlowNode_OnPointerRelease : FlowNodePersistent
{
  private bool mPressed;

  private void OnDisable()
  {
    this.mPressed = false;
  }

  private void Update()
  {
    bool mPressed = this.mPressed;
    this.mPressed = Input.GetMouseButton(0);
    if (this.mPressed || !mPressed)
      return;
    this.ActivateOutputLinks(0);
  }
}
