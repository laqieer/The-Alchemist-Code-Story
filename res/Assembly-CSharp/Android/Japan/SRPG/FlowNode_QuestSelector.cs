// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_QuestSelector
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

namespace SRPG
{
  [FlowNode.NodeType("System/Quest/Selector", 32741)]
  [FlowNode.Pin(0, "In", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(100, "Out", FlowNode.PinTypes.Output, 100)]
  public class FlowNode_QuestSelector : FlowNode
  {
    [SerializeField]
    private GlobalVars.EventQuestListType EventQuestListType;

    public override void OnActivate(int pinID)
    {
      if (pinID == 0)
        GlobalVars.ReqEventPageListType = this.EventQuestListType;
      this.ActivateOutputLinks(100);
    }
  }
}
