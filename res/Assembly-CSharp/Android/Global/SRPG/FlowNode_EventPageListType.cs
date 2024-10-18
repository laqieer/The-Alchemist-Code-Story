// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_EventPageListType
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using UnityEngine;

namespace SRPG
{
  [FlowNode.Pin(100, "完了", FlowNode.PinTypes.Output, 100)]
  [FlowNode.Pin(0, "設定", FlowNode.PinTypes.Input, 0)]
  [FlowNode.NodeType("System/EventPageListType")]
  public class FlowNode_EventPageListType : FlowNode
  {
    [SerializeField]
    private GlobalVars.EventQuestListType m_TargetEventQuestListType;

    public override void OnActivate(int pinID)
    {
      if (pinID != 0)
        return;
      GlobalVars.ReqEventPageListType = this.m_TargetEventQuestListType;
      this.ActivateOutputLinks(100);
    }
  }
}
