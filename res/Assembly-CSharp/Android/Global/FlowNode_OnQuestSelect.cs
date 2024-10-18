// Decompiled with JetBrains decompiler
// Type: FlowNode_OnQuestSelect
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using UnityEngine;

[AddComponentMenu("")]
[FlowNode.NodeType("Event/OnQuestSelect", 58751)]
[FlowNode.Pin(1, "Selected", FlowNode.PinTypes.Output, 0)]
public class FlowNode_OnQuestSelect : FlowNodePersistent
{
  public override void OnActivate(int pinID)
  {
  }

  public void Selected()
  {
    this.ActivateOutputLinks(1);
  }
}
