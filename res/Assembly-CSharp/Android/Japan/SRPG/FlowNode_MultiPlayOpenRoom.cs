// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_MultiPlayOpenRoom
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

namespace SRPG
{
  [FlowNode.NodeType("Multi/OpenRoom", 32741)]
  [FlowNode.Pin(10, "部屋オープン", FlowNode.PinTypes.Input, 10)]
  [FlowNode.Pin(1010, "完了", FlowNode.PinTypes.Output, 1010)]
  public class FlowNode_MultiPlayOpenRoom : FlowNode
  {
    private const int PIN_INPUT_OPEN = 10;
    private const int PIN_OUTPUT_FINISH = 1010;

    public override void OnActivate(int pinID)
    {
      if (pinID != 10)
        return;
      this.OpenRoom();
    }

    private void OpenRoom()
    {
      MyPhoton instance = PunMonoSingleton<MyPhoton>.Instance;
      if ((UnityEngine.Object) instance != (UnityEngine.Object) null && instance.IsConnectedInRoom() && instance.IsCreatedRoom())
        instance.OpenRoom(true, false);
      this.ActivateOutputLinks(1010);
    }
  }
}
