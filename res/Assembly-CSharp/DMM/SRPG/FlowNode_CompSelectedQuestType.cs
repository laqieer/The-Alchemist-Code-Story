// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_CompSelectedQuestType
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using UnityEngine;

#nullable disable
namespace SRPG
{
  [FlowNode.NodeType("System/CompSelectedQuestType", 32741)]
  [FlowNode.Pin(1, "Comp", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(111, "Yes", FlowNode.PinTypes.Output, 111)]
  [FlowNode.Pin(112, "No", FlowNode.PinTypes.Output, 112)]
  public class FlowNode_CompSelectedQuestType : FlowNode
  {
    [SerializeField]
    private QuestTypes[] mCompQuestTypes;
    private const int PIN_IN_COMP = 1;
    private const int PIN_OUT_YES = 111;
    private const int PIN_OUT_NO = 112;

    public override void OnActivate(int pinID)
    {
      if (pinID != 1)
        return;
      QuestParam quest = MonoSingleton<GameManager>.Instance.FindQuest(GlobalVars.SelectedQuestID);
      if (quest != null && this.mCompQuestTypes != null && this.mCompQuestTypes.Length != 0)
      {
        for (int index = 0; index < this.mCompQuestTypes.Length; ++index)
        {
          if (quest.type == this.mCompQuestTypes[index])
          {
            this.ActivateOutputLinks(111);
            return;
          }
        }
      }
      this.ActivateOutputLinks(112);
    }
  }
}
