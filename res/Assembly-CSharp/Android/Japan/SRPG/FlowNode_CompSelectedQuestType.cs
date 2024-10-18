// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_CompSelectedQuestType
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;
using UnityEngine;

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
