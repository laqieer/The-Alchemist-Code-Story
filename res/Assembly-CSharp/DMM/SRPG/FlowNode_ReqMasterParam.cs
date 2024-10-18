// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_ReqMasterParam
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
  [FlowNode.NodeType("System/Master/ReqMasterParam", 32741)]
  [FlowNode.Pin(0, "Request", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(1, "Success", FlowNode.PinTypes.Output, 1)]
  [FlowNode.Pin(2, "Failed", FlowNode.PinTypes.Output, 2)]
  public class FlowNode_ReqMasterParam : FlowNode_Network
  {
    public override void OnActivate(int pinID)
    {
      if (pinID != 0)
        return;
      if ((!AssetManager.UseDLC || GameUtility.Config_UseLocalData.Value) && !GameUtility.Config_UseServerData.Value)
      {
        GameManager.LoadMasterDataResult result = GameUtility.Config_UseSerializedParams.Value || GameUtility.Config_UseEncryption.Value ? MonoSingleton<GameManager>.Instance.ReloadMasterData(new GameManager.MasterDataInBinary().Load(GameUtility.Config_UseSerializedParams.Value, GameUtility.Config_UseEncryption.Value)) : MonoSingleton<GameManager>.Instance.ReloadMasterData();
        if (result.Result == GameManager.ELoadMasterDataResult.SUCCESS)
        {
          this.Success();
        }
        else
        {
          GameManager.HandleAnyLoadMasterDataErrors(result);
          this.Failure();
        }
      }
      else if (SRPG.Network.Mode == SRPG.Network.EConnectMode.Online && (!GameUtility.Config_UseAssetBundles.Value || GameUtility.Config_UseServerData.Value))
      {
        this.SerializeCompressMethod = EncodingTypes.ESerializeCompressMethod.TYPED_MESSAGE_PACK_LZ4;
        this.ExecRequest((WebAPI) new ReqMasterParam(new SRPG.Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback), this.SerializeCompressMethod));
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
      WebAPI.JSON_BodyResponse<JSON_MasterParam> jsonBodyResponse = (WebAPI.JSON_BodyResponse<JSON_MasterParam>) null;
      JSON_MasterParam body;
      if (EncodingTypes.IsJsonSerializeCompressSelected(this.SerializeCompressMethod))
      {
        if (SRPG.Network.IsError)
        {
          int errCode = (int) SRPG.Network.ErrCode;
          this.OnFailed();
          return;
        }
        WebAPI.JSON_BodyResponse<JSON_MasterParam> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<JSON_MasterParam>>(www.text);
        DebugUtility.Assert(jsonObject != null, "jsonRes == null");
        body = jsonObject.body;
      }
      else
      {
        FlowNode_ReqMasterParam.MP_MasterParam mpMasterParam = SerializerCompressorHelper.Decode<FlowNode_ReqMasterParam.MP_MasterParam>(www.rawResult, true, EncodingTypes.GetCompressModeFromSerializeCompressMethod(this.SerializeCompressMethod));
        DebugUtility.Assert(mpMasterParam != null, "mpRes == null");
        SRPG.Network.EErrCode stat = (SRPG.Network.EErrCode) mpMasterParam.stat;
        string statMsg = mpMasterParam.stat_msg;
        if (stat != SRPG.Network.EErrCode.Success)
          SRPG.Network.SetServerMetaDataAsError(stat, statMsg);
        if (SRPG.Network.IsError)
        {
          int errCode = (int) SRPG.Network.ErrCode;
          this.OnFailed();
          return;
        }
        body = mpMasterParam.body;
      }
      SRPG.Network.RemoveAPI();
      if (!MonoSingleton<GameManager>.Instance.Deserialize(body))
      {
        this.Failure();
      }
      else
      {
        MonoSingleton<GameManager>.Instance.MasterParam.DumpLoadedLog();
        this.Success();
        jsonBodyResponse = (WebAPI.JSON_BodyResponse<JSON_MasterParam>) null;
      }
    }

    [MessagePackObject(true)]
    public class MP_MasterParam : WebAPI.JSON_BaseResponse
    {
      public JSON_MasterParam body;
    }
  }
}
