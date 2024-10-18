// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_ReqRuneEquip
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using Gsc.Network.Encoding;
using MessagePack;
using System;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
namespace SRPG
{
  [FlowNode.NodeType("Rune/Req/Equip", 32741)]
  [FlowNode.Pin(1, "Request", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(101, "Success", FlowNode.PinTypes.Output, 101)]
  public class FlowNode_ReqRuneEquip : FlowNode_Network
  {
    protected const int PIN_IN_REQUEST = 1;
    protected const int PIN_OUT_SUCCESS = 101;
    private static UnitData mTargetUnit = (UnitData) null;
    private static List<BindRuneData> mTargetList = (List<BindRuneData>) null;
    private static BindRuneData[] mTargetEquipRune = new BindRuneData[6];

    public static void SetEquip(BindRuneData rune_data, List<BindRuneData> list, UnitData unit)
    {
      FlowNode_ReqRuneEquip.Clear();
      if (rune_data == null || unit == null || list == null)
        return;
      FlowNode_ReqRuneEquip.mTargetList = list;
      FlowNode_ReqRuneEquip.mTargetUnit = unit;
      FlowNode_ReqRuneEquip.CloneUnitsCurrentEquipment();
      if ((int) (byte) rune_data.RuneParam.slot_index >= FlowNode_ReqRuneEquip.mTargetEquipRune.Length)
        return;
      FlowNode_ReqRuneEquip.mTargetEquipRune[(int) (byte) rune_data.RuneParam.slot_index] = rune_data;
    }

    public static void SetUnequip(BindRuneData rune_data, List<BindRuneData> list, UnitData unit)
    {
      FlowNode_ReqRuneEquip.Clear();
      if (rune_data == null || unit == null || list == null)
        return;
      FlowNode_ReqRuneEquip.mTargetList = list;
      FlowNode_ReqRuneEquip.mTargetUnit = unit;
      FlowNode_ReqRuneEquip.CloneUnitsCurrentEquipment();
      FlowNode_ReqRuneEquip.mTargetEquipRune[(int) (byte) rune_data.RuneParam.slot_index] = (BindRuneData) null;
    }

    public static void SetUnequipAll(List<BindRuneData> list, UnitData unit)
    {
      FlowNode_ReqRuneEquip.Clear();
      if (unit == null || list == null)
        return;
      FlowNode_ReqRuneEquip.mTargetList = list;
      FlowNode_ReqRuneEquip.mTargetUnit = unit;
      FlowNode_ReqRuneEquip.CloneUnitsCurrentEquipment();
      for (int index = 0; index < FlowNode_ReqRuneEquip.mTargetEquipRune.Length; ++index)
        FlowNode_ReqRuneEquip.mTargetEquipRune[index] = (BindRuneData) null;
    }

    public static void Clear()
    {
      FlowNode_ReqRuneEquip.mTargetUnit = (UnitData) null;
      FlowNode_ReqRuneEquip.mTargetList = (List<BindRuneData>) null;
      for (int index = 0; index < FlowNode_ReqRuneEquip.mTargetEquipRune.Length; ++index)
        FlowNode_ReqRuneEquip.mTargetEquipRune[index] = (BindRuneData) null;
    }

    private static void CloneUnitsCurrentEquipment()
    {
      if (FlowNode_ReqRuneEquip.mTargetUnit == null || FlowNode_ReqRuneEquip.mTargetUnit.EquipRunes == null)
        return;
      for (int i = 0; i < 6; ++i)
      {
        if (FlowNode_ReqRuneEquip.mTargetUnit.EquipRunes[i] != null)
        {
          BindRuneData bindRuneData = FlowNode_ReqRuneEquip.mTargetList.Find((Predicate<BindRuneData>) (p => p.iid == (long) FlowNode_ReqRuneEquip.mTargetUnit.EquipRunes[i].UniqueID));
          if (bindRuneData == null)
            DebugUtility.LogError("装備したルーンが適正ではない");
          FlowNode_ReqRuneEquip.mTargetEquipRune[i] = bindRuneData;
        }
      }
    }

    public ReqRuneEquip.RequestParam CreateReqRuneEquip()
    {
      if (FlowNode_ReqRuneEquip.mTargetUnit == null || FlowNode_ReqRuneEquip.mTargetUnit.EquipRunes == null)
      {
        DebugUtility.LogError("Failed CreateReqRuneEquip.");
        return (ReqRuneEquip.RequestParam) null;
      }
      ReqRuneEquip.RequestParam reqRuneEquip = new ReqRuneEquip.RequestParam();
      reqRuneEquip.unit_id = FlowNode_ReqRuneEquip.mTargetUnit.UniqueID;
      reqRuneEquip.rune_ids = new long[6];
      for (int index = 0; index < 6; ++index)
        reqRuneEquip.rune_ids[index] = FlowNode_ReqRuneEquip.mTargetEquipRune[index] == null ? 0L : FlowNode_ReqRuneEquip.mTargetEquipRune[index].iid;
      return reqRuneEquip;
    }

    public override void OnActivate(int pinID)
    {
      ReqRuneEquip.RequestParam reqRuneEquip = this.CreateReqRuneEquip();
      if (reqRuneEquip == null || pinID != 1)
        return;
      ((Behaviour) this).enabled = true;
      this.SerializeCompressMethod = EncodingTypes.ESerializeCompressMethod.TYPED_MESSAGE_PACK;
      this.ExecRequest((WebAPI) new ReqRuneEquip(reqRuneEquip, new SRPG.Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback), this.SerializeCompressMethod));
    }

    private void Success()
    {
      ((Behaviour) this).enabled = false;
      this.ActivateOutputLinks(101);
    }

    public override void OnSuccess(WWWResult www)
    {
      ReqRuneEquip.Response response = (ReqRuneEquip.Response) null;
      bool flag = EncodingTypes.IsJsonSerializeCompressSelected(this.SerializeCompressMethod);
      if (!flag)
      {
        FlowNode_ReqRuneEquip.MP_Response mpResponse = SerializerCompressorHelper.Decode<FlowNode_ReqRuneEquip.MP_Response>(www.rawResult, true, EncodingTypes.GetCompressModeFromSerializeCompressMethod(this.SerializeCompressMethod));
        DebugUtility.Assert(mpResponse != null, "mp_res == null");
        SRPG.Network.EErrCode stat = (SRPG.Network.EErrCode) mpResponse.stat;
        string statMsg = mpResponse.stat_msg;
        if (stat != SRPG.Network.EErrCode.Success)
          SRPG.Network.SetServerMetaDataAsError(stat, statMsg);
        response = mpResponse.body;
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
          WebAPI.JSON_BodyResponse<ReqRuneEquip.Response> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<ReqRuneEquip.Response>>(www.text);
          DebugUtility.Assert(jsonObject != null, "res == null");
          response = jsonObject.body;
        }
        if (response == null)
        {
          this.OnFailed();
        }
        else
        {
          try
          {
            MonoSingleton<GameManager>.Instance.Deserialize(response.units);
            MonoSingleton<GameManager>.Instance.Player.SetRuneStorageUsedNum(response.rune_storage_used);
          }
          catch (Exception ex)
          {
            DebugUtility.LogException(ex);
            this.OnFailed();
            return;
          }
          SRPG.Network.RemoveAPI();
          this.Success();
        }
      }
    }

    [MessagePackObject(true)]
    public class MP_Response : WebAPI.JSON_BaseResponse
    {
      public ReqRuneEquip.Response body;
    }
  }
}
