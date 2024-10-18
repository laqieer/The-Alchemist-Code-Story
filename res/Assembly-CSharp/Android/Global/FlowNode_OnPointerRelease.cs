// Decompiled with JetBrains decompiler
// Type: FlowNode_OnPointerRelease
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using UnityEngine;

[FlowNode.NodeType("Event/OnPointerRelease", 58751)]
[FlowNode.Pin(0, "Released", FlowNode.PinTypes.Output, 0)]
public class FlowNode_OnPointerRelease : FlowNodePersistent
{
  private bool mPressed;

  public override void OnActivate(int pinID)
  {
  }

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
