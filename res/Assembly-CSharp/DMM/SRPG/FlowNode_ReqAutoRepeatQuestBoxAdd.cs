﻿// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_ReqAutoRepeatQuestBoxAdd
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using Gsc.Network.Encoding;
using MessagePack;
using UnityEngine;

#nullable disable
namespace SRPG
{
  [FlowNode.NodeType("AutoRepeatQuest/ReqBoxAdd", 32741)]
  [FlowNode.Pin(10, "BOX枠の拡張開始", FlowNode.PinTypes.Input, 10)]
  [FlowNode.Pin(110, "BOX枠の拡張終了", FlowNode.PinTypes.Output, 110)]
  [FlowNode.Pin(120, "幻晶石が不足", FlowNode.PinTypes.Output, 120)]
  [FlowNode.Pin(130, "BOX枠が上限に達している", FlowNode.PinTypes.Output, 130)]
  public class FlowNode_ReqAutoRepeatQuestBoxAdd : FlowNode_Network
  {
    private const int PIN_INPUT_BOX_ADD_START = 10;
    private const int PIN_OUTPUT_BOX_ADD_END = 110;
    private const int PIN_OUTPUT_ERR_NOT_ENOUGH_COIN = 120;
    private const int PIN_OUTPUT_ERR_BOX_SIZE_LIMIT = 130;

    public override void OnActivate(int pinID)
    {
      if (pinID != 10)
        return;
      ((Behaviour) this).enabled = true;
      this.SerializeCompressMethod = EncodingTypes.ESerializeCompressMethod.TYPED_MESSAGE_PACK;
      this.ExecRequest((WebAPI) new ReqAutoRepeatQuestBoxAdd(new SRPG.Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback), this.SerializeCompressMethod));
    }

    public override void OnSuccess(WWWResult www)
    {
      if (SRPG.Network.IsError)
      {
        switch (SRPG.Network.ErrCode)
        {
          case SRPG.Network.EErrCode.AutoRepeatQuest_BoxSizeLimit:
            this.ActivateOutputLinks(130);
            ((Behaviour) this).enabled = false;
            SRPG.Network.RemoveAPI();
            SRPG.Network.ResetError();
            break;
          case SRPG.Network.EErrCode.AutoRepeatQuest_NotEnoughCoin:
            this.ActivateOutputLinks(120);
            this.BuyCoinSequence();
            ((Behaviour) this).enabled = false;
            SRPG.Network.RemoveAPI();
            SRPG.Network.ResetError();
            break;
          default:
            FlowNode_Network.Failed();
            break;
        }
      }
      else
      {
        ReqAutoRepeatQuestBoxAdd.Response body;
        if (!EncodingTypes.IsJsonSerializeCompressSelected(this.SerializeCompressMethod))
        {
          FlowNode_ReqAutoRepeatQuestBoxAdd.MP_ReqAutoRepeatQuestBoxAddResponse questBoxAddResponse = SerializerCompressorHelper.Decode<FlowNode_ReqAutoRepeatQuestBoxAdd.MP_ReqAutoRepeatQuestBoxAddResponse>(www.rawResult, true, EncodingTypes.GetCompressModeFromSerializeCompressMethod(this.SerializeCompressMethod));
          DebugUtility.Assert(questBoxAddResponse != null, "mpRes == null");
          body = questBoxAddResponse.body;
        }
        else
        {
          WebAPI.JSON_BodyResponse<ReqAutoRepeatQuestBoxAdd.Response> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<ReqAutoRepeatQuestBoxAdd.Response>>(www.text);
          DebugUtility.Assert(jsonObject != null, "jsonRes == null");
          body = jsonObject.body;
        }
        SRPG.Network.RemoveAPI();
        if (body != null)
        {
          MonoSingleton<GameManager>.Instance.Player.Deserialize(body.player);
          MonoSingleton<GameManager>.Instance.Player.SetAutoRepeatQuestBox(body.box_extension_count);
          MonoSingleton<GameManager>.Instance.Player.SetAutoRepeatQuestBoxExpansion(body.box_expansion_purchase_count);
          AutoRepeatQuestData repeatQuestProgress = MonoSingleton<GameManager>.Instance.Player.AutoRepeatQuestProgress;
          if (repeatQuestProgress.State == AutoRepeatQuestData.eState.AUTO_REPEAT_NOW)
          {
            repeatQuestProgress.SetLapMax(body.lap_num);
            if (body.is_push != 0)
            {
              MyLocalNotification.ResetAutoRepeatQuest();
              AutoRepeatQuestData.SetLocalNotification(repeatQuestProgress);
            }
          }
          ((Behaviour) this).enabled = false;
          this.ActivateOutputLinks(110);
        }
        else
          FlowNode_Network.Failed();
      }
    }

    private void BuyCoinSequence()
    {
      MonoSingleton<GameManager>.Instance.ConfirmBuyCoin((GameManager.BuyCoinEvent) null, (GameManager.BuyCoinEvent) null);
    }

    [MessagePackObject(true)]
    public class MP_ReqAutoRepeatQuestBoxAddResponse : WebAPI.JSON_BaseResponse
    {
      public ReqAutoRepeatQuestBoxAdd.Response body;
    }
  }
}