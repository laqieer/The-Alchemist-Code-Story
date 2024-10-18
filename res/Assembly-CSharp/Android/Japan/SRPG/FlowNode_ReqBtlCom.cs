// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_ReqBtlCom
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;
using UnityEngine;

namespace SRPG
{
  [FlowNode.NodeType("System/ReqBtlCom/ReqBtlCom", 32741)]
  [FlowNode.Pin(100, "Start", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(1, "Success", FlowNode.PinTypes.Output, 10)]
  [FlowNode.Pin(2, "Reset to Title", FlowNode.PinTypes.Output, 11)]
  [FlowNode.Pin(3, "GenesisOutOfPeriod", FlowNode.PinTypes.Output, 12)]
  public class FlowNode_ReqBtlCom : FlowNode_Network
  {
    private const int PIN_OUT_GENESIS_OUT_OF_PERIOD = 3;
    public bool FastRefresh;
    public bool GetTowerProgress;
    [SerializeField]
    private bool GetGenesisProgress;

    public override void OnActivate(int pinID)
    {
      if (pinID != 100)
        return;
      if (Network.Mode == Network.EConnectMode.Online)
      {
        this.ExecRequest((WebAPI) new ReqBtlCom(new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback), this.FastRefresh, this.GetTowerProgress, this.GetGenesisProgress));
        this.enabled = true;
      }
      else
        this.Success();
    }

    private void Success()
    {
      this.enabled = false;
      this.ActivateOutputLinks(1);
    }

    private void Failure()
    {
      this.enabled = false;
      this.ActivateOutputLinks(2);
    }

    public override void OnSuccess(WWWResult www)
    {
      if (Network.IsError)
      {
        if (Network.ErrCode == Network.EErrCode.Genesis_OutOfPeriod)
        {
          Network.RemoveAPI();
          Network.ResetError();
          this.ActivateOutputLinks(3);
          this.enabled = false;
        }
        else
          this.OnRetry();
      }
      else
      {
        WebAPI.JSON_BodyResponse<FlowNode_ReqBtlCom.JSON_ReqBtlComResponse> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<FlowNode_ReqBtlCom.JSON_ReqBtlComResponse>>(www.text);
        DebugUtility.Assert(jsonObject != null, "res == null");
        Network.RemoveAPI();
        GameManager instance = MonoSingleton<GameManager>.Instance;
        instance.Player.SetQuestListDirty();
        instance.ResetJigenQuests(this.GetGenesisProgress);
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
          this.Success();
        }
      }
    }

    public class JSON_ReqBtlComResponse
    {
      public JSON_QuestProgress[] quests;
      public JSON_ReqTowerResuponse.Json_TowerProg[] towers;
      public ReqBtlCom.GenesisStar[] genesis_stars;
    }
  }
}
