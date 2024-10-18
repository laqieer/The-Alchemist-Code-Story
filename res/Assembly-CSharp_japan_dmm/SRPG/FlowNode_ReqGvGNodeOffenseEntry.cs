// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_ReqGvGNodeOffenseEntry
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
  [FlowNode.NodeType("GvG/Req/GvGNodeOffenseEntry", 32741)]
  [FlowNode.Pin(1, "Request", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(101, "Success", FlowNode.PinTypes.Output, 101)]
  public class FlowNode_ReqGvGNodeOffenseEntry : FlowNode_Network
  {
    protected const int PIN_IN_REQUEST = 1;
    protected const int PIN_OUT_SUCCESS = 101;
    public bool IsSetGvGParty;
    public bool isForce;

    public override void OnActivate(int pinID)
    {
      if (pinID != 1 || ((Behaviour) this).enabled)
        return;
      ((Behaviour) this).enabled = true;
      this.SerializeCompressMethod = EncodingTypes.ESerializeCompressMethod.TYPED_MESSAGE_PACK;
      if (MonoSingleton<GameManager>.Instance.Player.PlayerGuild == null)
        this.OnFailed();
      else if (UnityEngine.Object.op_Equality((UnityEngine.Object) GvGManager.Instance, (UnityEngine.Object) null) || UnityEngine.Object.op_Equality((UnityEngine.Object) GvGBattleTop.Instance, (UnityEngine.Object) null))
        this.OnFailed();
      else
        this.ExecRequest((WebAPI) new ReqGvGNodeOffenseEntry(GvGManager.Instance.SelectNodeId, GvGBattleTop.Instance.EditPartyIds, MonoSingleton<GameManager>.Instance.Player.PlayerGuild.Gid, GvGManager.GvGGroupId, !this.isForce ? 0 : 1, new SRPG.Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback), this.SerializeCompressMethod));
    }

    private void Success()
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this, (UnityEngine.Object) null))
        return;
      ((Behaviour) this).enabled = false;
      this.ActivateOutputLinks(101);
    }

    public override void OnSuccess(WWWResult www)
    {
      ReqGvGNodeOffenseEntry.Response response = (ReqGvGNodeOffenseEntry.Response) null;
      bool flag = EncodingTypes.IsJsonSerializeCompressSelected(this.SerializeCompressMethod);
      if (!flag)
      {
        FlowNode_ReqGvGNodeOffenseEntry.MP_Response mpResponse = SerializerCompressorHelper.Decode<FlowNode_ReqGvGNodeOffenseEntry.MP_Response>(www.rawResult, true, EncodingTypes.GetCompressModeFromSerializeCompressMethod(this.SerializeCompressMethod));
        DebugUtility.Assert(mpResponse != null, "mp_res == null");
        SRPG.Network.EErrCode stat = (SRPG.Network.EErrCode) mpResponse.stat;
        string statMsg = mpResponse.stat_msg;
        if (stat != SRPG.Network.EErrCode.Success)
          SRPG.Network.SetServerMetaDataAsError(stat, statMsg);
        response = mpResponse.body;
      }
      if (SRPG.Network.IsError)
      {
        switch (SRPG.Network.ErrCode)
        {
          case SRPG.Network.EErrCode.Guild_NotJoined:
            SRPG.Network.RemoveAPI();
            SRPG.Network.ResetError();
            ((Behaviour) this).enabled = false;
            UIUtility.SystemMessage(SRPG.Network.ErrMsg, (UIUtility.DialogResultEvent) (go => GlobalEvent.Invoke("MENU_HOME", (object) null)), systemModal: true);
            break;
          case SRPG.Network.EErrCode.GvG_OutOfPeriod:
          case SRPG.Network.EErrCode.GvG_NotGvGEntry:
            SRPG.Network.RemoveAPI();
            SRPG.Network.ResetError();
            ((Behaviour) this).enabled = false;
            UIUtility.SystemMessage(SRPG.Network.ErrMsg, (UIUtility.DialogResultEvent) (go => GlobalEvent.Invoke("BACK_GUILD", (object) null)), systemModal: true);
            break;
          case SRPG.Network.EErrCode.GvG_NotUsedUnit:
          case SRPG.Network.EErrCode.GvG_NotMatchUnit:
          case SRPG.Network.EErrCode.GvG_ExceedUnitUsedCount:
          case SRPG.Network.EErrCode.GvG_HasNotDeclare:
          case SRPG.Network.EErrCode.GvG_CanNotModifiedOffenseParty:
          case SRPG.Network.EErrCode.GvG_NotOffensePartyEntryForOnTheDay:
          case SRPG.Network.EErrCode.GvG_BtlOutOfPeriod:
          case SRPG.Network.EErrCode.GvG_HasCapture:
          case SRPG.Network.EErrCode.GvG_BattleHalfTime:
            SRPG.Network.RemoveAPI();
            SRPG.Network.ResetError();
            ((Behaviour) this).enabled = false;
            UIUtility.SystemMessage(SRPG.Network.ErrMsg, (UIUtility.DialogResultEvent) (go => GlobalEvent.Invoke("RELOAD_GVG", (object) null)), systemModal: true);
            break;
          default:
            this.OnRetry();
            break;
        }
      }
      else
      {
        if (flag)
        {
          WebAPI.JSON_BodyResponse<ReqGvGNodeOffenseEntry.Response> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<ReqGvGNodeOffenseEntry.Response>>(www.text);
          DebugUtility.Assert(jsonObject != null, "res == null");
          if (jsonObject.body == null)
          {
            this.OnRetry();
            return;
          }
          response = jsonObject.body;
        }
        SRPG.Network.RemoveAPI();
        try
        {
          GvGBattleTop instance = GvGBattleTop.Instance;
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) instance, (UnityEngine.Object) null))
          {
            instance.SetupSelfParty(new JSON_GvGParty()
            {
              units = response.offense
            });
            if (this.IsSetGvGParty)
              GlobalVars.GvGOffenseParty.Set(instance.SelfParty);
          }
          GvGManager.Instance.SetupUsedMessage(response.unavailable_items);
        }
        catch (Exception ex)
        {
          DebugUtility.LogException(ex);
          this.OnFailed();
          return;
        }
        this.Success();
      }
    }

    [MessagePackObject(true)]
    public class MP_Response : WebAPI.JSON_BaseResponse
    {
      public ReqGvGNodeOffenseEntry.Response body;
    }
  }
}
