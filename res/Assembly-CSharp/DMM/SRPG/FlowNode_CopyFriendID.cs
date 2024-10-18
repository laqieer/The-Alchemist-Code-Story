// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_CopyFriendID
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using DeviceKit;
using GR;
using UnityEngine;

#nullable disable
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
      ((Behaviour) this).enabled = false;
      this.ActivateOutputLinks(1);
    }
  }
}
