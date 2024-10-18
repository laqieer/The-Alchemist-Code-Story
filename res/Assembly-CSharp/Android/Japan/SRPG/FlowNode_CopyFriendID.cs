// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_CopyFriendID
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using DeviceKit;
using GR;

namespace SRPG
{
  [FlowNode.NodeType("System/ClipBoard/CopyFriendID", 32741)]
  [FlowNode.Pin(0, "コピー", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(1, "成功", FlowNode.PinTypes.Output, 1)]
  public class FlowNode_CopyFriendID : FlowNode
  {
    public override void OnActivate(int pinID)
    {
      if (pinID != 0)
        return;
      App.SetClipboard(MonoSingleton<GameManager>.Instance.Player.FUID);
      this.enabled = false;
      this.ActivateOutputLinks(1);
    }
  }
}
