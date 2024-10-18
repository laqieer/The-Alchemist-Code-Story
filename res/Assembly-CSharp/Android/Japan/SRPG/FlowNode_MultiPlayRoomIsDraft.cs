// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_MultiPlayRoomIsDraft
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;

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
      if ((UnityEngine.Object) instance2 == (UnityEngine.Object) null)
        return false;
      MyPhoton.MyRoom myRoom = !instance1.AudienceMode ? instance2.GetCurrentRoom() : instance1.AudienceRoom;
      if (myRoom == null)
        return false;
      JSON_MyPhotonRoomParam myPhotonRoomParam = myRoom != null ? JSON_MyPhotonRoomParam.Parse(myRoom.json) : (JSON_MyPhotonRoomParam) null;
      return myPhotonRoomParam != null && myPhotonRoomParam.draft_type != 0;
    }
  }
}
