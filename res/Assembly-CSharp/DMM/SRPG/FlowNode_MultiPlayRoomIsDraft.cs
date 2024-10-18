// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_MultiPlayRoomIsDraft
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using UnityEngine;

#nullable disable
namespace SRPG
{
  [FlowNode.NodeType("Multi/MultiPlayRoomIsDraft", 32741)]
  [FlowNode.Pin(0, "Check", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(1, "Yes", FlowNode.PinTypes.Output, 1)]
  [FlowNode.Pin(2, "No", FlowNode.PinTypes.Output, 2)]
  public class FlowNode_MultiPlayRoomIsDraft : FlowNode
  {
    private const int PIN_INPUT_CHECK = 0;
    private const int PIN_OUTPUT_YES = 1;
    private const int PIN_OUTPUT_NO = 2;

    public override void OnActivate(int pinID)
    {
      if (pinID != 0)
        return;
      if (FlowNode_MultiPlayRoomIsDraft.IsDraftMultiPlayRoom())
        this.ActivateOutputLinks(1);
      else
        this.ActivateOutputLinks(2);
    }

    public static bool IsDraftMultiPlayRoom()
    {
      GameManager instance1 = MonoSingleton<GameManager>.Instance;
      MyPhoton instance2 = PunMonoSingleton<MyPhoton>.Instance;
      if (Object.op_Equality((Object) instance2, (Object) null))
        return false;
      MyPhoton.MyRoom myRoom = !instance1.AudienceMode ? instance2.GetCurrentRoom() : instance1.AudienceRoom;
      if (myRoom == null)
        return false;
      JSON_MyPhotonRoomParam myPhotonRoomParam = myRoom != null ? JSON_MyPhotonRoomParam.Parse(myRoom.json) : (JSON_MyPhotonRoomParam) null;
      return myPhotonRoomParam != null && myPhotonRoomParam.draft_type != 0;
    }
  }
}
