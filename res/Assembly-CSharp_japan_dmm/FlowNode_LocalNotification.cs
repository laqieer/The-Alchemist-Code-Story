// Decompiled with JetBrains decompiler
// Type: FlowNode_LocalNotification
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

#nullable disable
[FlowNode.NodeType("System/Notify/LocalNotification", 65535)]
[FlowNode.Pin(0, "SetUp", FlowNode.PinTypes.Input, 0)]
[FlowNode.Pin(1, "SetUpEnd", FlowNode.PinTypes.Output, 1)]
public class FlowNode_LocalNotification : FlowNode
{
  public string path = "Data/Localnotification";

  private void Init() => MyLocalNotification.Setup(this.path);

  public override void OnActivate(int pinID)
  {
    if (pinID != 0)
      return;
    this.Init();
    this.ActivateOutputLinks(1);
  }
}
