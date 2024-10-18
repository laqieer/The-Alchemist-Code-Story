// Decompiled with JetBrains decompiler
// Type: FlowNode_ExecuteScript
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

[FlowNode.NodeType("Common/Script", 32741)]
[FlowNode.Pin(10, "Input", FlowNode.PinTypes.Input, 0)]
[FlowNode.Pin(1, "Output", FlowNode.PinTypes.Output, 1)]
public class FlowNode_ExecuteScript : FlowNode
{
  public string ScriptName = string.Empty;

  public override string GetCaption()
  {
    return base.GetCaption() + ":" + this.ScriptName;
  }

  public override void OnActivate(int pinID)
  {
    this.ActivateOutputLinks(1);
  }
}
