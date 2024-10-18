// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_ExtraUnitOpenPopup
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System.Collections.Generic;

#nullable disable
namespace SRPG
{
  [FlowNode.NodeType("Quest/ExtraUnitOpenPopup", 32741)]
  [FlowNode.Pin(0, "Open", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(1, "Clear", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(100, "Closed", FlowNode.PinTypes.Output, 100)]
  public class FlowNode_ExtraUnitOpenPopup : FlowNode
  {
    private static List<string> s_QuestIds = new List<string>();

    public override void OnActivate(int pinID)
    {
      switch (pinID)
      {
        case 0:
          if (FlowNode_ExtraUnitOpenPopup.OpenPopup((UIUtility.DialogResultEvent) (_param1 => this.ActivateOutputLinks(100))))
            break;
          this.ActivateOutputLinks(100);
          break;
        case 1:
          FlowNode_ExtraUnitOpenPopup.s_QuestIds.Clear();
          break;
      }
    }

    public static void ReserveOpenExtraQuestPopup(string unit_id)
    {
      QuestParam openUnitQuestParam = MonoSingleton<GameManager>.Instance.GetOpenUnitQuestParam(unit_id);
      if (openUnitQuestParam == null || !openUnitQuestParam.IsAvailable() || FlowNode_ExtraUnitOpenPopup.s_QuestIds.Contains(openUnitQuestParam.iname))
        return;
      FlowNode_ExtraUnitOpenPopup.s_QuestIds.Add(openUnitQuestParam.iname);
    }

    public static bool OpenPopup(UIUtility.DialogResultEvent on_close)
    {
      List<QuestParam> questParamList = new List<QuestParam>();
      if (FlowNode_ExtraUnitOpenPopup.s_QuestIds != null && FlowNode_ExtraUnitOpenPopup.s_QuestIds.Count > 0)
      {
        foreach (string questId in FlowNode_ExtraUnitOpenPopup.s_QuestIds)
        {
          QuestParam quest = MonoSingleton<GameManager>.Instance.FindQuest(questId);
          if (quest != null)
            questParamList.Add(quest);
        }
      }
      if (questParamList.Count <= 0)
        return false;
      string[] strArray = new string[questParamList.Count];
      for (int index = 0; index < questParamList.Count; ++index)
        strArray[index] = string.Format(LocalizedText.Get("sys.EXTRAOPEN_MSG"), (object) questParamList[index].name);
      string msg = string.Join("\n", strArray);
      UIUtility.SystemMessage(LocalizedText.Get("sys.EXTRAOPEN_TITLE"), msg, on_close, systemModal: true);
      FlowNode_ExtraUnitOpenPopup.s_QuestIds.Clear();
      return true;
    }
  }
}
