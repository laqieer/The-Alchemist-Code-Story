// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_PlayNew
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
  [FlowNode.NodeType("System/NewGame/PlayNew", 32741)]
  [FlowNode.Pin(10, "Start", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(1, "Success", FlowNode.PinTypes.Output, 10)]
  [FlowNode.Pin(2, "Reset to Title", FlowNode.PinTypes.Output, 11)]
  public class FlowNode_PlayNew : FlowNode_Network
  {
    public bool IsDebug;
    public int debugMode;

    public void SetDebug(bool check)
    {
    }

    public override void OnActivate(int pinID)
    {
      if (pinID != 10)
        return;
      if (SRPG.Network.Mode == SRPG.Network.EConnectMode.Online)
      {
        this.SerializeCompressMethod = EncodingTypes.ESerializeCompressMethod.TYPED_MESSAGE_PACK;
        this.ExecRequest((WebAPI) new ReqPlayNew(this.debugMode, new SRPG.Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback), this.SerializeCompressMethod));
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
      Json_PlayerDataAll body;
      if (EncodingTypes.IsJsonSerializeCompressSelected(this.SerializeCompressMethod))
      {
        if (SRPG.Network.IsError)
        {
          if (SRPG.Network.ErrCode == SRPG.Network.EErrCode.CreateStopped)
          {
            this.OnRetry();
            return;
          }
          this.OnFailed();
          return;
        }
        WebAPI.JSON_BodyResponse<Json_PlayerDataAll> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<Json_PlayerDataAll>>(www.text);
        DebugUtility.Assert(jsonObject != null, "jsonRes == null");
        body = jsonObject.body;
      }
      else
      {
        FlowNode_PlayNew.MP_PlayNew mpPlayNew = SerializerCompressorHelper.Decode<FlowNode_PlayNew.MP_PlayNew>(www.rawResult, true, EncodingTypes.GetCompressModeFromSerializeCompressMethod(this.SerializeCompressMethod));
        SRPG.Network.EErrCode stat = (SRPG.Network.EErrCode) mpPlayNew.stat;
        string statMsg = mpPlayNew.stat_msg;
        if (stat != SRPG.Network.EErrCode.Success)
          SRPG.Network.SetServerMetaDataAsError(stat, statMsg);
        if (SRPG.Network.IsError)
        {
          int errCode = (int) SRPG.Network.ErrCode;
          this.OnFailed();
          return;
        }
        DebugUtility.Assert(mpPlayNew != null, "mpRes == null");
        body = mpPlayNew.body;
      }
      SRPG.Network.RemoveAPI();
      if (body == null)
      {
        this.Failure();
      }
      else
      {
        GameManager instance = MonoSingleton<GameManager>.Instance;
        try
        {
          instance.Deserialize(body.player);
          instance.Deserialize(body.units);
          instance.Deserialize(body.items);
          if (!instance.Deserialize(body.mails))
          {
            this.Failure();
            return;
          }
          instance.Deserialize(body.parties);
          instance.Deserialize(body.friends);
          instance.Deserialize(body.notify);
          instance.Deserialize(body.skins);
          instance.Player.SetRuneStorageNum(instance.MasterParam.FixParam.RuneStorageInit);
        }
        catch (Exception ex)
        {
          DebugUtility.LogException(ex);
          this.Failure();
          return;
        }
        GameUtility.Config_OkyakusamaCode = instance.Player.OkyakusamaCode;
        GlobalVars.CustomerID = instance.Player.CUID;
        instance.PostLogin();
        this.Success();
      }
    }

    public enum DebugModeType
    {
      NORMAL,
      DEBUG_ALL,
      DEBUG_HALF,
    }

    [MessagePackObject(true)]
    public class MP_PlayNew : WebAPI.JSON_BaseResponse
    {
      public Json_PlayerDataAll body;
    }
  }
}
