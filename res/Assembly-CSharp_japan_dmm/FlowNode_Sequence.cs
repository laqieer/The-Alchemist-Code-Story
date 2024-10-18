// Decompiled with JetBrains decompiler
// Type: FlowNode_Sequence
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;

#nullable disable
[FlowNode.NodeType("Common/Sequence", 32741)]
[FlowNode.Pin(0, "Input", FlowNode.PinTypes.Input, 0)]
public class FlowNode_Sequence : FlowNode
{
  [SerializeField]
  private int m_Num = 1;
  [SerializeField]
  [HideInInspector]
  private FlowNode.Pin[] m_Pins = new FlowNode.Pin[0];

  public override void OnActivate(int pinID)
  {
    for (int index = 0; index < this.OutputLinks.Length; ++index)
      this.ActivateOutputLinks(this.OutputLinks[index].SrcPinID);
  }

  public override FlowNode.Pin[] GetDynamicPins() => this.m_Pins;
}
