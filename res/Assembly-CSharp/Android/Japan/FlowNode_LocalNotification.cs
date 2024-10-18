// Decompiled with JetBrains decompiler
// Type: FlowNode_LocalNotification
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

[FlowNode.NodeType("System/Notify/LocalNotification", 65535)]
[FlowNode.Pin(0, "SetUp", FlowNode.PinTypes.Input, 0)]
[FlowNode.Pin(1, "SetUpEnd", FlowNode.PinTypes.Output, 1)]
public class FlowNode_LocalNotification : FlowNode
{
  public string path = "Data/Localnotification";

  private void Init()
  {
    MyLocalNotification.Setup(this.path);
  }

  public override void OnActivate(int pinID)
  {
    if (pinID != 0)
      return;
    this.Init();
    this.ActivateOutputLinks(1);
  }
}
