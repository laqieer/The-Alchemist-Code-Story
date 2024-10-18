// Decompiled with JetBrains decompiler
// Type: FlowNode_LocalNotification
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

[FlowNode.Pin(1, "SetUpEnd", FlowNode.PinTypes.Output, 1)]
[FlowNode.NodeType("System/LocalNotification", 65535)]
[FlowNode.Pin(0, "SetUp", FlowNode.PinTypes.Input, 0)]
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
