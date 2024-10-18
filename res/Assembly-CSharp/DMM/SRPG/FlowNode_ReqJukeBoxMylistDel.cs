// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_ReqJukeBoxMylistDel
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
  [FlowNode.NodeType("JukeBox/Req/PlayListDel", 32741)]
  [FlowNode.Pin(1, "Del Request", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(101, "Success", FlowNode.PinTypes.Output, 101)]
  public class FlowNode_ReqJukeBoxMylistDel : FlowNode_Network
  {
    protected const int PIN_IN_DEL_REQUEST = 1;
    protected const int PIN_OUT_SUCCESS = 101;

    public override void OnActivate(int pinID)
    {
      JukeBoxWindow.JukeBoxData currentJukeBoxData = JukeBoxWindow.GetCurrentJukeBoxData();
      if (currentJukeBoxData != null)
        this.ExecRequest((WebAPI) new ReqJukeBoxPlaylistDel(new string[1]
        {
          currentJukeBoxData.param.Iname
        }, new SRPG.Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback), EncodingTypes.ESerializeCompressMethod.TYPED_MESSAGE_PACK));
      else
        this.OnFailed();
    }

    private void Success()
    {
      ((Behaviour) this).enabled = false;
      this.ActivateOutputLinks(101);
    }

    public override void OnSuccess(WWWResult www)
    {
      FlowNode_ReqJukeBoxMylistDel.MP_Del_Response mpDelResponse1 = SerializerCompressorHelper.Decode<FlowNode_ReqJukeBoxMylistDel.MP_Del_Response>(www.rawResult, true, EncodingTypes.GetCompressModeFromSerializeCompressMethod(this.SerializeCompressMethod));
      DebugUtility.Assert(mpDelResponse1 != null, "mp_res == null");
      ReqJukeBoxPlaylistDel.Response body;
      if (EncodingTypes.IsJsonSerializeCompressSelected(!GlobalVars.SelectedSerializeCompressMethodWasNodeSet ? EncodingTypes.ESerializeCompressMethod.TYPED_MESSAGE_PACK : GlobalVars.SelectedSerializeCompressMethod))
      {
        WebAPI.JSON_BodyResponse<ReqJukeBoxPlaylistDel.Response> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<ReqJukeBoxPlaylistDel.Response>>(www.text);
        DebugUtility.Assert(jsonObject != null, "jsonRes == null");
        body = jsonObject.body;
      }
      else
      {
        FlowNode_ReqJukeBoxMylistDel.MP_Del_Response mpDelResponse2 = SerializerCompressorHelper.Decode<FlowNode_ReqJukeBoxMylistDel.MP_Del_Response>(www.rawResult, true, EncodingTypes.GetCompressModeFromSerializeCompressMethod(EncodingTypes.ESerializeCompressMethod.TYPED_MESSAGE_PACK));
        DebugUtility.Assert(mpDelResponse2 != null, "mpRes == null");
        body = mpDelResponse2.body;
      }
      SRPG.Network.EErrCode stat = (SRPG.Network.EErrCode) mpDelResponse1.stat;
      string statMsg = mpDelResponse1.stat_msg;
      if (stat != SRPG.Network.EErrCode.Success)
        SRPG.Network.SetServerMetaDataAsError(stat, statMsg);
      if (SRPG.Network.IsError)
      {
        switch (SRPG.Network.ErrCode)
        {
          case SRPG.Network.EErrCode.Gallery_JukeBox_NotHaveBgm:
          case SRPG.Network.EErrCode.Gallery_JukeBox_NotCreatePlayList:
          case SRPG.Network.EErrCode.Gallery_JukeBox_NotInPlayList:
          case SRPG.Network.EErrCode.Gallery_JukeBox_AlreadyAddPlayList:
            this.OnFailed();
            break;
          default:
            this.OnRetry();
            break;
        }
      }
      else
      {
        try
        {
          JukeBoxWindow.SetJukeboxPlayListData(body.playlists);
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

    [MessagePackObject(true)]
    public class MP_Del_Response : WebAPI.JSON_BaseResponse
    {
      public ReqJukeBoxPlaylistDel.Response body;
    }
  }
}
