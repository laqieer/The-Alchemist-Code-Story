﻿// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_ReqTowerRecover
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System;
using UnityEngine;

#nullable disable
namespace SRPG
{
  [FlowNode.NodeType("Tower/Req/ReqTowerRecover", 32741)]
  [FlowNode.Pin(0, "Request", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(1, "Success", FlowNode.PinTypes.Output, 10)]
  public class FlowNode_ReqTowerRecover : FlowNode_Network
  {
    private GameObject mFlowRoot;
    private int usedCoin;

    public override void OnActivate(int pinID)
    {
      if (pinID != 0)
        return;
      TowerRecoverData dataOfClass = DataSource.FindDataOfClass<TowerRecoverData>(((Component) this).gameObject, (TowerRecoverData) null);
      TowerResuponse towerResuponse = MonoSingleton<GameManager>.Instance.TowerResuponse;
      if (Network.Mode != Network.EConnectMode.Online)
        return;
      if (dataOfClass != null)
      {
        byte floor = towerResuponse.GetCurrentFloor().floor;
        this.usedCoin = dataOfClass.useCoin;
        this.ExecRequest((WebAPI) new ReqTowerRecover(dataOfClass.towerID, dataOfClass.useCoin, (int) towerResuponse.round, floor, new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback)));
        ((Behaviour) this).enabled = true;
      }
      else
        ((Behaviour) this).enabled = false;
    }

    private void Success()
    {
      ((Behaviour) this).enabled = false;
      UIUtility.SystemMessage(LocalizedText.Get("sys.CAPTION_TOWER_RECOVERED"), LocalizedText.Get("sys.MSG_TOWER_RECOVERED", (object) this.usedCoin.ToString()), (UIUtility.DialogResultEvent) (go => this.ActivateOutputLinks(1)));
    }

    public override void OnSuccess(WWWResult www)
    {
      if (TowerErrorHandle.Error((FlowNode_Network) this))
        return;
      WebAPI.JSON_BodyResponse<FlowNode_ReqTowerRecover.JSON_ReqTowerRecoverResponse> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<FlowNode_ReqTowerRecover.JSON_ReqTowerRecoverResponse>>(www.text);
      DebugUtility.Assert(jsonObject != null, "res == null");
      Network.RemoveAPI();
      GameManager instance = MonoSingleton<GameManager>.Instance;
      try
      {
        instance.Deserialize(jsonObject.body.player);
        instance.TowerResuponse.Deserialize(jsonObject.body.pdeck);
        instance.TowerResuponse.rtime = (long) jsonObject.body.rtime;
        instance.TowerResuponse.recover_num = jsonObject.body.rcv_num;
      }
      catch (Exception ex)
      {
        DebugUtility.LogException(ex);
        return;
      }
      this.Success();
    }

    private class JSON_ReqTowerRecoverResponse
    {
      public Json_PlayerData player;
      public int rtime;
      public int rcv_num;
      public JSON_ReqTowerResuponse.Json_TowerPlayerUnit[] pdeck;
    }
  }
}
