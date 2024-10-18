// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_MultiPlayAutoEntryRoom
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

namespace SRPG
{
  [FlowNode.NodeType("Multi/AutoEntryRoom", 32741)]
  [FlowNode.Pin(1, "開始", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(101, "自動入室に成功", FlowNode.PinTypes.Output, 101)]
  [FlowNode.Pin(102, "自動入室に失敗", FlowNode.PinTypes.Output, 102)]
  public class FlowNode_MultiPlayAutoEntryRoom : FlowNode
  {
    private const int PIN_INPUT_START = 1;
    private const int PIN_OUTPUT_SUCCESS = 101;
    private const int PIN_OUTPUT_FAILED = 102;

    public override void OnActivate(int pinID)
    {
      if (pinID != 1)
        return;
      this.AutoEntry();
    }

    private void AutoEntry()
    {
      MyPhoton instance = PunMonoSingleton<MyPhoton>.Instance;
      if ((UnityEngine.Object) instance != (UnityEngine.Object) null && instance.IsConnectedInRoom())
        this.ActivateOutputLinks(101);
      else
        this.ActivateOutputLinks(102);
    }
  }
}
