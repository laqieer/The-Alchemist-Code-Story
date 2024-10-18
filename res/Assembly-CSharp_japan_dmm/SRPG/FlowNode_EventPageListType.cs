// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_EventPageListType
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;

#nullable disable
namespace SRPG
{
  [FlowNode.NodeType("System/EventPageList/EventPageListType")]
  [FlowNode.Pin(0, "設定", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(100, "完了", FlowNode.PinTypes.Output, 100)]
  public class FlowNode_EventPageListType : FlowNode
  {
    [SerializeField]
    private GlobalVars.EventQuestListType m_TargetEventQuestListType;

    public override void OnActivate(int pinID)
    {
      base.OnActivate(pinID);
      if (pinID != 0)
        return;
      GlobalVars.ReqEventPageListType = this.m_TargetEventQuestListType;
      this.ActivateOutputLinks(100);
    }
  }
}
