// Decompiled with JetBrains decompiler
// Type: FlowNode_PlayVoice
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

[FlowNode.NodeType("Sound/PlayVoice", 32741)]
[FlowNode.Pin(100, "OneShot", FlowNode.PinTypes.Input, 0)]
[FlowNode.Pin(1, "Output", FlowNode.PinTypes.Output, 1)]
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
