// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_ReqAutoRepeatQuestStart
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using Gsc.Network.Encoding;
using MessagePack;
using System;
using UnityEngine;

#nullable disable
namespace SRPG
{
  [FlowNode.NodeType("AutoRepeatQuest/ReqStart", 32741)]
  [FlowNode.Pin(10, "Request(自動周回開始)", FlowNode.PinTypes.Input, 10)]
  [FlowNode.Pin(110, "Success(自動周回開始)", FlowNode.PinTypes.Output, 110)]
  [FlowNode.Pin(120, "Failure(自動周回開始)", FlowNode.PinTypes.Output, 120)]
  public class FlowNode_ReqAutoRepeatQuestStart : FlowNode_Network
  {
    private const int PIN_INPUT_REQUEST = 10;
    private const int PIN_OUTPUT_SUCCESS = 110;
    private const int PIN_OUTPUT_FAILURE = 120;
    [SerializeField]
    private AutoRepeatQuestStart Target;

    public override void OnActivate(int pinID)
    {
      if (pinID != 10)
        return;
      this.Request();
      ((Behaviour) this).enabled = true;
    }

    private void Request()
    {
      QuestParam quest = MonoSingleton<GameManager>.Instance.FindQuest(GlobalVars.SelectedQuestID);
      if (quest == null)
      {
        DebugUtility.LogError("該当のQuestParamが存在しません:" + SceneBattle.Instance.Battle.QuestID);
      }
      else
      {
        string iname = quest.iname;
        int lap_num = 1;
        bool use_drip = false;
        bool is_full_box = false;
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.Target, (UnityEngine.Object) null))
        {
          lap_num = this.Target.LapNum;
          use_drip = this.Target.IsUseAPHealItem;
          is_full_box = this.Target.IsSelectBoxFull;
        }
        this.ExecRequest((WebAPI) new ReqAutoRepeatQuestStart(iname, lap_num, is_full_box, use_drip, this.Target.IsUseNotification, GlobalVars.Location, new SRPG.Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback), EncodingTypes.ESerializeCompressMethod.TYPED_MESSAGE_PACK));
      }
    }

    private void Success() => this.ActivateOutputLinks(110);

    public override void OnSuccess(WWWResult www)
    {
      if (SRPG.Network.IsError)
      {
        SRPG.Network.EErrCode errCode = SRPG.Network.ErrCode;
        switch (errCode)
        {
          case SRPG.Network.EErrCode.AutoRepeatQuest_NotQuest:
          case SRPG.Network.EErrCode.AutoRepeatQuest_NotQuestMissionComplete:
          case SRPG.Network.EErrCode.AutoRepeatQuest_AlreadyLap:
          case SRPG.Network.EErrCode.AutoRepeatQuest_AutoRepeatLocked:
          case SRPG.Network.EErrCode.AutoRepeatQuest_InvalidTurnNum:
            this.OnFailed();
            break;
          case SRPG.Network.EErrCode.AutoRepeatQuest_NotStart:
            SRPG.Network.RemoveAPI();
            SRPG.Network.IsIndicator = true;
            SRPG.Network.ResetError();
            ((Behaviour) this).enabled = false;
            UIUtility.SystemMessage(SRPG.Network.ErrMsg, (UIUtility.DialogResultEvent) null, systemModal: true);
            this.ActivateOutputLinks(this.OnFailedPinIndex);
            break;
          default:
            if (errCode == SRPG.Network.EErrCode.ChallengeLimit || errCode == SRPG.Network.EErrCode.QuestArchive_ArchiveNotOpened)
              goto case SRPG.Network.EErrCode.AutoRepeatQuest_NotQuest;
            else
              goto case SRPG.Network.EErrCode.AutoRepeatQuest_NotQuest;
        }
      }
      else
      {
        ReqAutoRepeatQuestStart.Response body;
        if (EncodingTypes.IsJsonSerializeCompressSelected(!GlobalVars.SelectedSerializeCompressMethodWasNodeSet ? EncodingTypes.ESerializeCompressMethod.TYPED_MESSAGE_PACK : GlobalVars.SelectedSerializeCompressMethod))
        {
          WebAPI.JSON_BodyResponse<ReqAutoRepeatQuestStart.Response> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<ReqAutoRepeatQuestStart.Response>>(www.text);
          DebugUtility.Assert(jsonObject != null, "jsonRes == null");
          body = jsonObject.body;
        }
        else
        {
          FlowNode_ReqAutoRepeatQuestStart.MP_AutoRepeatQuestStartResponse questStartResponse = SerializerCompressorHelper.Decode<FlowNode_ReqAutoRepeatQuestStart.MP_AutoRepeatQuestStartResponse>(www.rawResult, true, EncodingTypes.GetCompressModeFromSerializeCompressMethod(EncodingTypes.ESerializeCompressMethod.TYPED_MESSAGE_PACK));
          DebugUtility.Assert(questStartResponse != null, "mpRes == null");
          body = questStartResponse.body;
        }
        SRPG.Network.RemoveAPI();
        try
        {
          MonoSingleton<GameManager>.Instance.Player.Deserialize(body.auto_repeat);
          MonoSingleton<GameManager>.Instance.Player.Deserialize(body.player);
          MonoSingleton<GameManager>.Instance.Player.Deserialize(body.items);
        }
        catch (Exception ex)
        {
          DebugUtility.LogException(ex);
          FlowNode_Network.Failed();
          return;
        }
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.Target, (UnityEngine.Object) null) && this.Target.IsUseNotification)
          AutoRepeatQuestData.SetLocalNotification(MonoSingleton<GameManager>.Instance.Player.AutoRepeatQuestProgress);
        this.Success();
        ((Behaviour) this).enabled = false;
      }
    }

    [MessagePackObject(true)]
    public class MP_AutoRepeatQuestStartResponse : WebAPI.JSON_BaseResponse
    {
      public ReqAutoRepeatQuestStart.Response body;
    }
  }
}
