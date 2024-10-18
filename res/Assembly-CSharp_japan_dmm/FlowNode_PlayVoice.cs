// Decompiled with JetBrains decompiler
// Type: FlowNode_PlayVoice
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

#nullable disable
[FlowNode.NodeType("Sound/PlayVoice", 32741)]
[FlowNode.Pin(100, "OneShot", FlowNode.PinTypes.Input, 0)]
[FlowNode.Pin(1, "Output", FlowNode.PinTypes.Output, 1)]
public class FlowNode_PlayVoice : FlowNode
{
  public string charaName;
  public string cueID;

  public override void OnActivate(int pinID)
  {
    new MySound.Voice(this.charaName)?.Play(this.cueID);
    this.ActivateOutputLinks(1);
  }
}
