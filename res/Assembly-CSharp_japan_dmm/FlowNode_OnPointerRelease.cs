// Decompiled with JetBrains decompiler
// Type: FlowNode_OnPointerRelease
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;

#nullable disable
[FlowNode.NodeType("Event/OnPointerRelease", 58751)]
[FlowNode.Pin(0, "Released", FlowNode.PinTypes.Output, 0)]
public class FlowNode_OnPointerRelease : FlowNodePersistent
{
  private bool mPressed;

  private void OnDisable() => this.mPressed = false;

  private void Update()
  {
    bool mPressed = this.mPressed;
    this.mPressed = Input.GetMouseButton(0);
    if (this.mPressed || !mPressed)
      return;
    this.ActivateOutputLinks(0);
  }
}
