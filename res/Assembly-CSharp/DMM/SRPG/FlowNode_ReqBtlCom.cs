// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_ReqBtlCom
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using UnityEngine;

#nullable disable
namespace SRPG
{
  [FlowNode.NodeType("System/ReqBtlCom/ReqBtlCom", 32741)]
  [FlowNode.Pin(100, "Start", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(1, "Success", FlowNode.PinTypes.Output, 10)]
  [FlowNode.Pin(2, "Reset to Title", FlowNode.PinTypes.Output, 11)]
  [FlowNode.Pin(3, "GenesisOutOfPeriod", FlowNode.PinTypes.Output, 12)]
  [FlowNode.Pin(4, "AdvanceNotOpen", FlowNode.PinTypes.Output, 13)]
  public class FlowNode_ReqBtlCom : FlowNode_Network
  {
    private const int PIN_OUT_GENESIS_OUT_OF_PERIOD = 3;
    private const int PIN_OUT_ADVANCE_NOT_OPEN = 4;
    public bool FastRefresh;
    public bool GetTowerProgress;
    [SerializeField]
    private bool GetGenesisProgress;
    [SerializeField]
    private bool GetAdvanceProgress;

    public override void OnActivate(int pinID)
    {
      if (pinID != 100)
        return;
      if (Network.Mode == Network.EConnectMode.Online)
      {
        this.ExecRequest((WebAPI) new ReqBtlCom(new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback), this.FastRefresh, this.GetTowerProgress, this.GetGenesisProgress, this.GetAdvanceProgress));
        ((Behaviour) this).enabled = true;
      }
      else
        this.Success();
    }

    private void Success()
    {
      ((Behaviour) this).enabled = false;
      this.ActivateOutputLinks(1);
    }

    private void Failure()
    {
      ((Behaviour) this).enabled = false;
      this.ActivateOutputLinks(2);
    }

    public override void OnSuccess(WWWResult www)
    {
      if (Network.IsError)
      {
        switch (Network.ErrCode)
        {
          case Network.EErrCode.Advance_KeyClose:
          case Network.EErrCode.Advance_NotOpen:
            Network.RemoveAPI();
            Network.ResetError();
            this.ActivateOutputLinks(4);
            ((Behaviour) this).enabled = false;
            break;
          case Network.EErrCode.Genesis_OutOfPeriod:
            Network.RemoveAPI();
            Network.ResetError();
            this.ActivateOutputLinks(3);
            ((Behaviour) this).enabled = false;
            break;
          default:
            this.OnRetry();
            break;
        }
      }
      else
      {
        WebAPI.JSON_BodyResponse<FlowNode_ReqBtlCom.JSON_ReqBtlComResponse> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<FlowNode_ReqBtlCom.JSON_ReqBtlComResponse>>(www.text);
        DebugUtility.Assert(jsonObject != null, "res == null");
        Network.RemoveAPI();
        GameManager instance = MonoSingleton<GameManager>.Instance;
        instance.Player.SetQuestListDirty();
        instance.ResetJigenQuests(this.GetGenesisProgress, this.GetAdvanceProgress);
        if (!instance.Deserialize(jsonObject.body.quests))
        {
          this.Failure();
        }
        else
        {
          if (jsonObject.body.towers != null)
          {
            for (int index = 0; index < jsonObject.body.towers.Length; ++index)
            {
              JSON_ReqTowerResuponse.Json_TowerProg tower1 = jsonObject.body.towers[index];
              TowerParam tower2 = instance.FindTower(tower1.iname);
              if (tower2 != null)
                tower2.is_unlock = tower1.is_open == 1;
            }
          }
          if (jsonObject.body.genesis_stars != null)
            GenesisManager.SetStarMissionInfo(jsonObject.body.genesis_stars);
          if (jsonObject.body.advance_stars != null)
            AdvanceManager.SetStarMissionInfo(jsonObject.body.advance_stars);
          instance.Deserialize(jsonObject.body.areas);
          instance.Player.Deserialize(jsonObject.body.story_ex_challenge);
          this.Success();
        }
      }
    }

    public class JSON_ReqBtlComResponse
    {
      public JSON_QuestProgress[] quests;
      public JSON_ReqTowerResuponse.Json_TowerProg[] towers;
      public ReqBtlCom.GenesisStar[] genesis_stars;
      public ReqBtlCom.AdvanceStar[] advance_stars;
      public JSON_ChapterCount[] areas;
      public JSON_StoryExChallengeCount story_ex_challenge;
    }
  }
}
