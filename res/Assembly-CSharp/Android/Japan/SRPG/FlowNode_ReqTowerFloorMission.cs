// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_ReqTowerFloorMission
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;
using System;

namespace SRPG
{
  [FlowNode.NodeType("Request/Tower/Floor/Mission", 32741)]
  [FlowNode.Pin(1, "ミッション進捗を取得", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(100, "ミッション進捗を取得完了", FlowNode.PinTypes.Output, 100)]
  public class FlowNode_ReqTowerFloorMission : FlowNode_Network
  {
    private const int INPUT_REQUEST_MISSION = 1;
    private const int OUTPUT_REQUEST_MISSION = 100;

    public override void OnActivate(int pinID)
    {
      if (pinID != 1)
        return;
      this.enabled = true;
      this.ExecRequest((WebAPI) new ReqTowerFloorMission(GlobalVars.SelectedTowerID, GlobalVars.SelectedQuestID, new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback)));
    }

    public override void OnSuccess(WWWResult www)
    {
      if (TowerErrorHandle.Error((FlowNode_Network) this))
        return;
      WebAPI.JSON_BodyResponse<ReqTowerFloorMission.Json_Response> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<ReqTowerFloorMission.Json_Response>>(www.text);
      DebugUtility.Assert(jsonObject != null, "res == null");
      Network.RemoveAPI();
      try
      {
        QuestParam quest = MonoSingleton<GameManager>.Instance.FindQuest(GlobalVars.SelectedQuestID);
        if (quest != null)
        {
          ReqTowerFloorMission.Json_Response body = jsonObject.body;
          for (int index = 0; index < quest.MissionNum; ++index)
          {
            quest.SetMissionFlag(index, false);
            quest.SetMissionValue(index, 0);
          }
          if (body != null && body.missions != null)
          {
            for (int index = 0; index < body.missions.Length; ++index)
            {
              int num = body.missions_val[index];
              bool isClear = body.missions[index] > 0;
              quest.SetMissionFlag(index, isClear);
              quest.SetMissionValue(index, num);
            }
          }
        }
        this.ActivateOutputLinks(100);
      }
      catch (Exception ex)
      {
        DebugUtility.LogException(ex);
        return;
      }
      this.enabled = false;
    }
  }
}
