// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_ReqJukeBox
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
  [FlowNode.NodeType("JukeBox/Req", 32741)]
  [FlowNode.Pin(1, "Request", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(101, "Success", FlowNode.PinTypes.Output, 101)]
  public class FlowNode_ReqJukeBox : FlowNode_Network
  {
    protected const int PIN_IN_REQUEST = 1;
    protected const int PIN_OUT_SUCCESS = 101;

    public override void OnActivate(int pinID)
    {
      if (pinID != 1)
        return;
      this.ExecRequest((WebAPI) new ReqJukeBox(new SRPG.Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback), EncodingTypes.ESerializeCompressMethod.TYPED_MESSAGE_PACK));
    }

    private void Success()
    {
      ((Behaviour) this).enabled = false;
      this.ActivateOutputLinks(101);
    }

    public override void OnSuccess(WWWResult www)
    {
      ReqJukeBox.Response body;
      if (EncodingTypes.IsJsonSerializeCompressSelected(!GlobalVars.SelectedSerializeCompressMethodWasNodeSet ? EncodingTypes.ESerializeCompressMethod.TYPED_MESSAGE_PACK : GlobalVars.SelectedSerializeCompressMethod))
      {
        WebAPI.JSON_BodyResponse<ReqJukeBox.Response> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<ReqJukeBox.Response>>(www.text);
        DebugUtility.Assert(jsonObject != null, "jsonRes == null");
        body = jsonObject.body;
      }
      else
      {
        FlowNode_ReqJukeBox.MP_Response mpResponse = SerializerCompressorHelper.Decode<FlowNode_ReqJukeBox.MP_Response>(www.rawResult, true, EncodingTypes.GetCompressModeFromSerializeCompressMethod(EncodingTypes.ESerializeCompressMethod.TYPED_MESSAGE_PACK));
        DebugUtility.Assert(mpResponse != null, "mpRes == null");
        body = mpResponse.body;
      }
      if (SRPG.Network.IsError)
      {
        int errCode = (int) SRPG.Network.ErrCode;
        this.OnRetry();
      }
      else if (body == null)
      {
        this.OnFailed();
      }
      else
      {
        try
        {
          if (!JukeBoxWindow.ReflectJukeboxUnlockData(body.bgms))
            throw new Exception("ReqJukeBox: illegal UnlockData!");
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
    public class MP_Response : WebAPI.JSON_BaseResponse
    {
      public ReqJukeBox.Response body;
    }
  }
}
