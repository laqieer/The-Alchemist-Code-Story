// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_EventPageListTypeIs
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;

#nullable disable
namespace SRPG
{
  [FlowNode.NodeType("System/EventPageList/EventPageListTypeIs")]
  [FlowNode.Pin(0, "判定", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(100, "==", FlowNode.PinTypes.Output, 100)]
  [FlowNode.Pin(101, "!=", FlowNode.PinTypes.Output, 101)]
  public class FlowNode_EventPageListTypeIs : FlowNode
  {
    private const int INPUT_JUDGE = 0;
    private const int OUTPUT_EQUAL = 100;
    private const int OUTPUT_NOT_EQUAL = 101;
    [SerializeField]
    [FlowNode.ShowInInfo(true)]
    private GlobalVars.EventQuestListType m_TargetEventQuestListType;

    public override void OnActivate(int pinID)
    {
      if (pinID != 0)
        return;
      if (GlobalVars.ReqEventPageListType == this.m_TargetEventQuestListType)
        this.ActivateOutputLinks(100);
      else
        this.ActivateOutputLinks(101);
    }
  }
}
