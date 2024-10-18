// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_QuestSelector
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;

#nullable disable
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
