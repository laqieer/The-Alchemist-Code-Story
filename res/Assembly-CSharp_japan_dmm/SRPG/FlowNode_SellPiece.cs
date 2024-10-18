﻿// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_SellPiece
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
namespace SRPG
{
  [FlowNode.NodeType("System/Shop/SellPiece", 32741)]
  [FlowNode.Pin(1, "Request", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(100, "Success", FlowNode.PinTypes.Output, 10)]
  public class FlowNode_SellPiece : FlowNode_Network
  {
    public override void OnActivate(int pinID)
    {
      if (pinID != 1 || ((Behaviour) this).enabled)
        return;
      PlayerData player = MonoSingleton<GameManager>.Instance.Player;
      if (Network.Mode == Network.EConnectMode.Offline)
      {
        for (int index = 0; index < GlobalVars.ConvertAwakePieceList.Count; ++index)
        {
          SellItem convertAwakePiece = GlobalVars.ConvertAwakePieceList[index];
          player.GainPiecePoint((int) convertAwakePiece.item.RarityParam.PieceToPoint * convertAwakePiece.num);
          player.GainItem(convertAwakePiece.item.Param.iname, -convertAwakePiece.num);
          convertAwakePiece.num = 0;
          convertAwakePiece.index = -1;
        }
        GlobalVars.ConvertAwakePieceList.Clear();
        this.Success();
      }
      else
      {
        Dictionary<long, int> sells = new Dictionary<long, int>();
        List<SellItem> convertAwakePieceList = GlobalVars.ConvertAwakePieceList;
        for (int index = 0; index < convertAwakePieceList.Count; ++index)
        {
          long uniqueId = convertAwakePieceList[index].item.UniqueID;
          int num = convertAwakePieceList[index].num;
          sells[uniqueId] = num;
        }
        this.ExecRequest((WebAPI) new ReqSellPiece(sells, new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback)));
        ((Behaviour) this).enabled = true;
      }
    }

    private void Success()
    {
      ((Behaviour) this).enabled = false;
      this.ActivateOutputLinks(100);
    }

    public override void OnSuccess(WWWResult www)
    {
      if (Network.IsError)
      {
        if (Network.ErrCode == Network.EErrCode.NoItemSell)
          this.OnBack();
        else
          this.OnRetry();
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
          MonoSingleton<GameManager>.Instance.Deserialize(jsonObject.body.items);
        }
        catch (Exception ex)
        {
          this.OnRetry(ex);
          return;
        }
        Network.RemoveAPI();
        GlobalVars.ConvertAwakePieceList.Clear();
        this.Success();
      }
    }
  }
}
