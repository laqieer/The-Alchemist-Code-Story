// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_ReqQuestParam
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
  [FlowNode.NodeType("System/Master/ReqQuestParam", 32741)]
  [FlowNode.Pin(0, "Request", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(1, "Success", FlowNode.PinTypes.Output, 1)]
  [FlowNode.Pin(2, "Failed", FlowNode.PinTypes.Output, 2)]
  public class FlowNode_ReqQuestParam : FlowNode_Network
  {
    public override void OnActivate(int pinID)
    {
      if (pinID != 0)
        return;
      if (SRPG.Network.Mode == SRPG.Network.EConnectMode.Online && (!GameUtility.Config_UseAssetBundles.Value && !GameUtility.Config_UseLocalData.Value || GameUtility.Config_UseServerData.Value))
      {
        this.SerializeCompressMethod = EncodingTypes.ESerializeCompressMethod.TYPED_MESSAGE_PACK_LZ4;
        this.ExecRequest((WebAPI) new ReqQuestParam(new SRPG.Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback), this.SerializeCompressMethod));
        ((Behaviour) this).enabled = true;
      }
      else
        this.Success();
    }

    private void Success()
    {
      ((Behaviour) this).enabled = false;
      this.ActivateOutputLinks(1);
    }

    private void Failure()
    {
      ((Behaviour) this).enabled = false;
      this.ActivateOutputLinks(2);
    }

    public override void OnSuccess(WWWResult www)
    {
      Json_QuestList body;
      if (EncodingTypes.IsJsonSerializeCompressSelected(this.SerializeCompressMethod))
      {
        if (SRPG.Network.IsError)
        {
          int errCode = (int) SRPG.Network.ErrCode;
          this.OnFailed();
          return;
        }
        WebAPI.JSON_BodyResponse<Json_QuestList> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<Json_QuestList>>(www.text);
        DebugUtility.Assert(jsonObject != null, "jsonRes == null");
        body = jsonObject.body;
      }
      else
      {
        FlowNode_ReqQuestParam.MP_QuestParam mpQuestParam = SerializerCompressorHelper.Decode<FlowNode_ReqQuestParam.MP_QuestParam>(www.rawResult, true, EncodingTypes.GetCompressModeFromSerializeCompressMethod(this.SerializeCompressMethod));
        DebugUtility.Assert(mpQuestParam != null, "mpRes == null");
        SRPG.Network.EErrCode stat = (SRPG.Network.EErrCode) mpQuestParam.stat;
        string statMsg = mpQuestParam.stat_msg;
        if (stat != SRPG.Network.EErrCode.Success)
          SRPG.Network.SetServerMetaDataAsError(stat, statMsg);
        if (SRPG.Network.IsError)
        {
          int errCode = (int) SRPG.Network.ErrCode;
          this.OnFailed();
          return;
        }
        body = mpQuestParam.body;
      }
      SRPG.Network.RemoveAPI();
      try
      {
        MonoSingleton<GameManager>.Instance.Deserialize(body);
      }
      catch (Exception ex)
      {
        DebugUtility.LogException(ex);
        this.Failure();
        return;
      }
      this.Success();
    }

    [MessagePackObject(true)]
    public class MP_QuestParam : WebAPI.JSON_BaseResponse
    {
      public Json_QuestList body;
    }
  }
}
