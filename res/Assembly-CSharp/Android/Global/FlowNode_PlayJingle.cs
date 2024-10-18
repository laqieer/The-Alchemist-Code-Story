// Decompiled with JetBrains decompiler
// Type: FlowNode_PlayJingle
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using GR;

[FlowNode.Pin(100, "OneShot", FlowNode.PinTypes.Input, 0)]
[FlowNode.Pin(1, "Output", FlowNode.PinTypes.Output, 1)]
[FlowNode.NodeType("Sound/PlayJingle", 32741)]
public class FlowNode_PlayJingle : FlowNode
{
  public string cueID;

  public override void OnActivate(int pinID)
  {
    MonoSingleton<MySound>.Instance.PlayJingle(this.cueID, 0.0f, (string) null);
    this.ActivateOutputLinks(1);
  }
}
