// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_MultiPlaySupport
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
  [FlowNode.NodeType("Multi/MultiPlaySupport", 32741)]
  [FlowNode.Pin(1, "Get List", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(2, "Set Support", FlowNode.PinTypes.Input, 2)]
  [FlowNode.Pin(101, "Success", FlowNode.PinTypes.Output, 101)]
  public class FlowNode_MultiPlaySupport : FlowNode_Network
  {
    private const int PIN_IN_GET_LIST = 1;
    private const int PIN_IN_SET_SUPPORT = 2;
    private const int PIN_OUT_SUCCESS = 101;
    [SerializeField]
    private bool GetSupportIsUpdate;
    private FlowNode_MultiPlaySupport.API SelectedAPI;

    public override void OnActivate(int pinID)
    {
      if (pinID != 1)
      {
        if (pinID != 2)
          return;
        this.StartCoroutine(this.Request_SetSupport());
      }
      else
        this.StartCoroutine(this.Request_GetList());
    }

    [DebuggerHidden]
    private IEnumerator Request_GetList()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new FlowNode_MultiPlaySupport.\u003CRequest_GetList\u003Ec__Iterator0()
      {
        \u0024this = this
      };
    }

    [DebuggerHidden]
    private IEnumerator Request_SetSupport()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new FlowNode_MultiPlaySupport.\u003CRequest_SetSupport\u003Ec__Iterator1()
      {
        \u0024this = this
      };
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
      switch (this.SelectedAPI)
      {
        case FlowNode_MultiPlaySupport.API.GET_LIST:
          this.Response_GetList(www);
          break;
        case FlowNode_MultiPlaySupport.API.SET_SUPPORT:
          this.Response_SetSuuport(www);
          break;
        default:
          this.OnFailed();
          break;
      }
    }

    private void Response_GetList(WWWResult www)
    {
      ReqMultiSupportList.Response response = (ReqMultiSupportList.Response) null;
      bool flag = EncodingTypes.IsJsonSerializeCompressSelected(this.SerializeCompressMethod);
      if (!flag)
      {
        FlowNode_MultiPlaySupport.MP_Response_GetList mpResponseGetList = SerializerCompressorHelper.Decode<FlowNode_MultiPlaySupport.MP_Response_GetList>(www.rawResult, true, EncodingTypes.GetCompressModeFromSerializeCompressMethod(this.SerializeCompressMethod));
        DebugUtility.Assert(mpResponseGetList != null, "mp_res == null");
        SRPG.Network.EErrCode stat = (SRPG.Network.EErrCode) mpResponseGetList.stat;
        string statMsg = mpResponseGetList.stat_msg;
        if (stat != SRPG.Network.EErrCode.Success)
          SRPG.Network.SetServerMetaDataAsError(stat, statMsg);
        response = mpResponseGetList.body;
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
          WebAPI.JSON_BodyResponse<ReqMultiSupportList.Response> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<ReqMultiSupportList.Response>>(www.text);
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
          if (UnityEngine.Object.op_Equality((UnityEngine.Object) HomeMultiPlay_FriendUnitSelect.Instance, (UnityEngine.Object) null))
            throw new Exception("HomeMultiPlay_FriendUnitSelect instance is null");
          HomeMultiPlay_FriendUnitSelect.Instance.SetupSupportList(response.supports);
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

    private void Response_SetSuuport(WWWResult www)
    {
      if (!EncodingTypes.IsJsonSerializeCompressSelected(this.SerializeCompressMethod))
      {
        WebAPI.JSON_BaseResponse jsonBaseResponse = SerializerCompressorHelper.Decode<WebAPI.JSON_BaseResponse>(www.rawResult, true, EncodingTypes.GetCompressModeFromSerializeCompressMethod(this.SerializeCompressMethod));
        DebugUtility.Assert(jsonBaseResponse != null, "mp_res == null");
        SRPG.Network.EErrCode stat = (SRPG.Network.EErrCode) jsonBaseResponse.stat;
        string statMsg = jsonBaseResponse.stat_msg;
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
        SRPG.Network.RemoveAPI();
        this.Success();
      }
    }

    private enum API
    {
      NONE,
      GET_LIST,
      SET_SUPPORT,
    }

    [MessagePackObject(true)]
    public class MP_Response_GetList : WebAPI.JSON_BaseResponse
    {
      public ReqMultiSupportList.Response body;
    }
  }
}
