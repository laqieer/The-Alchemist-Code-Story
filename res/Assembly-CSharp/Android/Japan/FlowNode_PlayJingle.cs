// Decompiled with JetBrains decompiler
// Type: FlowNode_PlayJingle
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;

[FlowNode.NodeType("Sound/PlayJingle", 32741)]
[FlowNode.Pin(100, "OneShot", FlowNode.PinTypes.Input, 0)]
[FlowNode.Pin(1, "Output", FlowNode.PinTypes.Output, 1)]
public class FlowNode_PlayJingle : FlowNode
{
  public string cueID;

  public override void OnActivate(int pinID)
  {
    MonoSingleton<MySound>.Instance.PlayJingle(this.cueID, 0.0f, (string) null);
    this.ActivateOutputLinks(1);
  }
}
