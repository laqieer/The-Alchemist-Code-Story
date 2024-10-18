// Decompiled with JetBrains decompiler
// Type: FlowNode_Sequence
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

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

  public override FlowNode.Pin[] GetDynamicPins()
  {
    return this.m_Pins;
  }
}
