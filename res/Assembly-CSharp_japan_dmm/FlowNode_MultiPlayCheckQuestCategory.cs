﻿// Decompiled with JetBrains decompiler
// Type: FlowNode_MultiPlayCheckQuestCategory
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using SRPG;

#nullable disable
[FlowNode.NodeType("Multi/MultiPlayCheckQuestCategory", 32741)]
[FlowNode.Pin(100, "TestExist", FlowNode.PinTypes.Input, 0)]
[FlowNode.Pin(0, "None", FlowNode.PinTypes.Output, 0)]
[FlowNode.Pin(1, "NormalOnly", FlowNode.PinTypes.Output, 0)]
[FlowNode.Pin(2, "EventOnly", FlowNode.PinTypes.Output, 0)]
[FlowNode.Pin(3, "Both", FlowNode.PinTypes.Output, 0)]
public class FlowNode_MultiPlayCheckQuestCategory : FlowNode
{
  public override void OnActivate(int pinID)
  {
    if (pinID != 100)
      return;
    bool flag1 = false;
    bool flag2 = false;
    for (int index = 0; index < MonoSingleton<GameManager>.Instance.Quests.Length; ++index)
    {
      QuestParam quest = MonoSingleton<GameManager>.Instance.Quests[index];
      if (quest.IsMulti)
      {
        if (quest.IsMultiEvent)
          flag2 = true;
        else
          flag1 = true;
        if (flag1 && flag2)
          break;
      }
    }
    if (flag1 && flag2)
    {
      DebugUtility.Log("BOTH");
      this.ActivateOutputLinks(3);
    }
    else if (!flag1 && flag2)
    {
      DebugUtility.Log("EVENT Only");
      this.ActivateOutputLinks(2);
    }
    else if (flag1 && !flag2)
    {
      DebugUtility.Log("NORMAL Only");
      this.ActivateOutputLinks(1);
    }
    else
    {
      DebugUtility.Log("NONE");
      this.ActivateOutputLinks(0);
    }
  }
}
