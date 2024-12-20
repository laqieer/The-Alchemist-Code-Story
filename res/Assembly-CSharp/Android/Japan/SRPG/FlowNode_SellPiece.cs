﻿// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_SellPiece
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;
using System;
using System.Collections.Generic;

namespace SRPG
{
  [FlowNode.NodeType("System/Shop/SellPiece", 32741)]
  [FlowNode.Pin(1, "Request", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(100, "Success", FlowNode.PinTypes.Output, 10)]
  public class FlowNode_SellPiece : FlowNode_Network
  {
    public override void OnActivate(int pinID)
    {
      if (pinID != 1 || this.enabled)
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
        this.enabled = true;
      }
    }

    private void Success()
    {
      this.enabled = false;
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
