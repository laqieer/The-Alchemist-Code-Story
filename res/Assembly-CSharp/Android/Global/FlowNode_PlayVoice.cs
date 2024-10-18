// Decompiled with JetBrains decompiler
// Type: FlowNode_PlayVoice
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

[FlowNode.NodeType("Sound/PlayVoice", 32741)]
[FlowNode.Pin(1, "Output", FlowNode.PinTypes.Output, 1)]
[FlowNode.Pin(100, "OneShot", FlowNode.PinTypes.Input, 0)]
public class FlowNode_PlayVoice : FlowNode
{
  public string charaName;
  public string cueID;

  public override void OnActivate(int pinID)
  {
    new MySound.Voice(this.charaName)?.Play(this.cueID, 0.0f, false);
    this.ActivateOutputLinks(1);
  }
}
