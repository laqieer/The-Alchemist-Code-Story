// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_ReqCombatPowerUpdate
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using Gsc.Network.Encoding;
using MessagePack;
using System;
using System.Collections;
using System.Diagnostics;
using UnityEngine;

#nullable disable
namespace SRPG
{
  [FlowNode.NodeType("CombatPower/Req/Update", 32741)]
  [FlowNode.Pin(1, "Request", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(101, "Success", FlowNode.PinTypes.Output, 101)]
  public class FlowNode_ReqCombatPowerUpdate : FlowNode_Network
  {
    protected const int PIN_IN_REQUEST = 1;
    protected const int PIN_OUT_SUCCESS = 101;

    [DebuggerHidden]
    private IEnumerator BeforeDestroyRequest()
    {
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: variable of a compiler-generated type
      FlowNode_ReqCombatPowerUpdate.\u003CBeforeDestroyRequest\u003Ec__Iterator0 requestCIterator0 = new FlowNode_ReqCombatPowerUpdate.\u003CBeforeDestroyRequest\u003Ec__Iterator0();
      return (IEnumerator) requestCIterator0;
    }

    public override void OnActivate(int pinID)
    {
      if (pinID != 1 || ((Behaviour) this).enabled)
        return;
      MonoSingleton<GameManager>.Instance.Player.UpdateTotalCombatPower();
      if (!MonoSingleton<GameManager>.Instance.Player.IsNeedToCombatPowerRequest)
        return;
      ((Behaviour) this).enabled = true;
      MonoSingleton<GameManager>.Instance.Player.UpdateCombatPowerTrophy();
      string trophy_progs = (string) null;
      string bingo_progs = (string) null;
      MonoSingleton<GameManager>.Instance.ServerSyncTrophyExecStart(out trophy_progs, out bingo_progs);
      MonoSingleton<GameManager>.Instance.RegisterImportantJob(this.StartCoroutine(this.BeforeDestroyRequest()));
      long totalCombatPower = (long) MonoSingleton<GameManager>.Instance.Player.combatPowerData.TotalCombatPower;
      this.SerializeCompressMethod = EncodingTypes.ESerializeCompressMethod.TYPED_MESSAGE_PACK;
      this.ExecRequest((WebAPI) new ReqCombatPowerUpdate(totalCombatPower, trophy_progs, bingo_progs, new SRPG.Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback), this.SerializeCompressMethod));
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
      bool flag = EncodingTypes.IsJsonSerializeCompressSelected(this.SerializeCompressMethod);
      if (!flag)
      {
        FlowNode_ReqCombatPowerUpdate.MP_Response mpResponse = SerializerCompressorHelper.Decode<FlowNode_ReqCombatPowerUpdate.MP_Response>(www.rawResult, true, EncodingTypes.GetCompressModeFromSerializeCompressMethod(this.SerializeCompressMethod));
        DebugUtility.Assert(mpResponse != null, "mp_res == null");
        SRPG.Network.EErrCode stat = (SRPG.Network.EErrCode) mpResponse.stat;
        string statMsg = mpResponse.stat_msg;
        if (stat != SRPG.Network.EErrCode.Success)
          SRPG.Network.SetServerMetaDataAsError(stat, statMsg);
      }
      if (SRPG.Network.IsError)
      {
        int errCode = (int) SRPG.Network.ErrCode;
        this.OnRetry();
      }
      else
      {
        if (flag)
        {
          WebAPI.JSON_BodyResponse<ReqCombatPowerUpdate.Response> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<ReqCombatPowerUpdate.Response>>(www.text);
          DebugUtility.Assert(jsonObject != null, "res == null");
          if (jsonObject.body == null)
          {
            this.OnRetry();
            return;
          }
        }
        SRPG.Network.RemoveAPI();
        try
        {
          MonoSingleton<GameManager>.Instance.ServerSyncTrophyExecEnd(www);
          MonoSingleton<GameManager>.Instance.Player.ClearTotalCombatPowerRequestFlag();
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
      public ReqCombatPowerUpdate.Response body;
    }
  }
}
