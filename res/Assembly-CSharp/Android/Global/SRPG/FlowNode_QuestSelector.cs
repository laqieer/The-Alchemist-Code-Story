﻿// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_QuestSelector
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using UnityEngine;

namespace SRPG
{
  [FlowNode.NodeType("System/Quest/Selector", 32741)]
  [FlowNode.Pin(100, "Out", FlowNode.PinTypes.Output, 100)]
  [FlowNode.Pin(0, "In", FlowNode.PinTypes.Input, 0)]
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
