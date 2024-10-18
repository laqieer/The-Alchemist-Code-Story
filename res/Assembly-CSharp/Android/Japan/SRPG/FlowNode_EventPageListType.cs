// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_EventPageListType
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

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
