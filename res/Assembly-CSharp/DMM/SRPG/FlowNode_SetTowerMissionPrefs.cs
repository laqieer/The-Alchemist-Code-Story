﻿// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_SetTowerMissionPrefs
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;

#nullable disable
namespace SRPG
{
  [FlowNode.NodeType("Tower/SetTowerMissionPrefs", 32741)]
  [FlowNode.Pin(0, "ミッションID設定", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(1, "設定完了", FlowNode.PinTypes.Output, 1)]
  public class FlowNode_SetTowerMissionPrefs : FlowNode
  {
    private const int INPUT_SET_MISSION_ID = 0;
    private const int OUTPUT_SET_MISSION_ID = 1;

    public override void OnActivate(int pinID)
    {
      bool flag = false;
      string str = (string) null;
      TowerFloorParam towerFloor = MonoSingleton<GameManager>.Instance.FindTowerFloor(GlobalVars.SelectedQuestID);
      if (towerFloor != null)
      {
        QuestParam questParam = towerFloor.GetQuestParam();
        if (questParam != null)
          str = questParam.iname;
      }
      if (pinID == 0 && str != null)
        flag = PlayerPrefsUtility.SetString(PlayerPrefsUtility.CONFIRM_TOWER_MISSION_QUEST_ID, str, true);
      if (!flag)
        DebugUtility.Log("PlayerPrefsの設定に失敗しました");
      this.ActivateOutputLinks(1);
    }
  }
}
