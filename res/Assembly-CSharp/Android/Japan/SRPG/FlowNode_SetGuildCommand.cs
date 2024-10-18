// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_SetGuildCommand
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

namespace SRPG
{
  [FlowNode.NodeType("Guild/SetCommand", 32741)]
  [FlowNode.Pin(1, "開始", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(2, "完了", FlowNode.PinTypes.Output, 2)]
  public class FlowNode_SetGuildCommand : FlowNode
  {
    private const int PIN_INPUT_START = 1;
    private const int PIN_OUTPUT_END = 2;
    [SerializeField]
    private SerializeValueBehaviour Object;
    [SerializeField]
    private GuildCommand.eCommand Command;

    public override void OnActivate(int pinID)
    {
      if (pinID != 1)
        return;
      this.SetCommand();
      this.ActivateOutputLinks(2);
    }

    private void SetCommand()
    {
      if ((UnityEngine.Object) this.Object == (UnityEngine.Object) null)
        return;
      GuildCommand.SetCommand(this.Object.gameObject, this.Command);
    }
  }
}
