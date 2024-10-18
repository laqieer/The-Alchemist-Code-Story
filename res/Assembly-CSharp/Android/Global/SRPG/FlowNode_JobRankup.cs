﻿// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_JobRankup
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using GR;
using System;
using System.Collections.Generic;

namespace SRPG
{
  [FlowNode.Pin(3, "ClassChange", FlowNode.PinTypes.Output, 3)]
  [FlowNode.Pin(0, "Request", FlowNode.PinTypes.Input, 0)]
  [FlowNode.NodeType("System/JobRankup", 32741)]
  [FlowNode.Pin(2, "Unlock", FlowNode.PinTypes.Output, 2)]
  [FlowNode.Pin(1, "Success", FlowNode.PinTypes.Output, 1)]
  public class FlowNode_JobRankup : FlowNode_Network
  {
    private int mSuccessPinID;

    public override void OnActivate(int pinID)
    {
      this.mSuccessPinID = 1;
      if (pinID != 0)
        return;
      List<AbilityData> learningAbilities = GlobalVars.LearningAbilities;
      if (learningAbilities != null && learningAbilities.Count > 0)
        FlowNode_Variable.Set("LEARNING_ABILITY", "1");
      if (GlobalVars.ReturnItems != null && GlobalVars.ReturnItems.Count > 0)
        FlowNode_Variable.Set("RETURN_ITEMS", "1");
      if (Network.Mode == Network.EConnectMode.Offline)
      {
        this.Success();
      }
      else
      {
        this.ExecRequest((WebAPI) new ReqJobRankup((long) GlobalVars.SelectedJobUniqueID, new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback)));
        this.enabled = true;
      }
    }

    private void Success()
    {
      this.enabled = false;
      this.ActivateOutputLinks(this.mSuccessPinID);
    }

    public override void OnSuccess(WWWResult www)
    {
      if (Network.IsError)
      {
        switch (Network.ErrCode)
        {
          case Network.EErrCode.NoJobLvUpEquip:
            this.OnFailed();
            break;
          case Network.EErrCode.EquipNotComp:
            this.OnBack();
            break;
          default:
            this.OnRetry();
            break;
        }
      }
      else
      {
        WebAPI.JSON_BodyResponse<Json_PlayerDataAll> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<Json_PlayerDataAll>>(www.text);
        DebugUtility.Assert(jsonObject != null, "res == null");
        try
        {
          if (jsonObject.body == null)
            throw new InvalidJSONException();
          MonoSingleton<GameManager>.Instance.Deserialize(jsonObject.body.player);
          MonoSingleton<GameManager>.Instance.Deserialize(jsonObject.body.units);
          MonoSingleton<GameManager>.Instance.Deserialize(jsonObject.body.items);
        }
        catch (Exception ex)
        {
          DebugUtility.LogException(ex);
          this.OnRetry();
          return;
        }
        Network.RemoveAPI();
        MonoSingleton<GameManager>.Instance.Player.FindUnitDataByUniqueID((long) GlobalVars.SelectedUnitUniqueID).SetJobIndex((int) GlobalVars.SelectedUnitJobIndex);
        if ((GlobalVars.JobRankUpTypes) GlobalVars.JobRankUpType == GlobalVars.JobRankUpTypes.Unlock)
          this.mSuccessPinID = 2;
        else if ((GlobalVars.JobRankUpTypes) GlobalVars.JobRankUpType == GlobalVars.JobRankUpTypes.ClassChange)
          this.mSuccessPinID = 3;
        this.Success();
      }
    }
  }
}
