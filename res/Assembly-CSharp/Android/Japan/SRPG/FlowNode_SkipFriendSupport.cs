﻿// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_SkipFriendSupport
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

namespace SRPG
{
  [FlowNode.NodeType("SRPG/SkipFriendSupport", 32741)]
  [FlowNode.Pin(100, "In", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(1, "Out", FlowNode.PinTypes.Output, 1)]
  public class FlowNode_SkipFriendSupport : FlowNode
  {
    public override void OnActivate(int pinID)
    {
      if (pinID != 100)
        return;
      GlobalVars.SelectedSupport.Set((SupportData) null);
      this.ActivateOutputLinks(1);
    }
  }
}