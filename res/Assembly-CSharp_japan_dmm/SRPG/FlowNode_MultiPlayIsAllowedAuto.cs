// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_MultiPlayIsAllowedAuto
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

#nullable disable
namespace SRPG
{
  [FlowNode.NodeType("Multi/MultiPlayIsAllowedAuto", 32741)]
  [FlowNode.Pin(1, "Test", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(10, "Allowed", FlowNode.PinTypes.Output, 10)]
  [FlowNode.Pin(11, "Denied", FlowNode.PinTypes.Output, 11)]
  [FlowNode.Pin(100, "Error", FlowNode.PinTypes.Output, 100)]
  public class FlowNode_MultiPlayIsAllowedAuto : FlowNode
  {
    private const int PIN_IN_TEST = 1;
    private const int PIN_OUT_ALLOWED = 10;
    private const int PIN_OUT_DENIED = 11;
    private const int PIN_OUT_ERROR = 100;

    public override void OnActivate(int pinID)
    {
      if (pinID != 1)
        return;
      MyPhoton.MyRoom currentRoom = PunMonoSingleton<MyPhoton>.Instance.GetCurrentRoom();
      if (currentRoom == null)
      {
        this.ActivateOutputLinks(100);
      }
      else
      {
        JSON_MyPhotonRoomParam myPhotonRoomParam = JSON_MyPhotonRoomParam.Parse(currentRoom.json);
        if (myPhotonRoomParam == null)
          this.ActivateOutputLinks(100);
        else if (myPhotonRoomParam.autoAllowed != 0)
          this.ActivateOutputLinks(10);
        else
          this.ActivateOutputLinks(11);
      }
    }
  }
}
