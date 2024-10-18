// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_ReqAutoRepeatQuestBox
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
  [FlowNode.NodeType("AutoRepeatQuest/ReqBox", 32741)]
  [FlowNode.Pin(10, "BOX枠の拡張回数取得", FlowNode.PinTypes.Input, 10)]
  [FlowNode.Pin(110, "BOX枠の拡張回数取得終了", FlowNode.PinTypes.Output, 110)]
  public class FlowNode_ReqAutoRepeatQuestBox : FlowNode_Network
  {
    private const int PIN_INPUT_START = 10;
    private const int PIN_OUTPUT_END = 110;
    [SerializeField]
    private bool mRequestForce;

    public override void OnActivate(int pinID)
    {
      if (pinID != 10)
        return;
      if (this.mRequestForce || !MonoSingleton<GameManager>.Instance.Player.AutoRepeatQuestBox.IsInitialized)
      {
        ((Behaviour) this).enabled = true;
        this.SerializeCompressMethod = EncodingTypes.ESerializeCompressMethod.TYPED_MESSAGE_PACK;
        this.ExecRequest((WebAPI) new ReqAutoRepeatQuestBox(new SRPG.Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback), this.SerializeCompressMethod));
      }
      else
        this.ActivateOutputLinks(110);
    }

    public override void OnSuccess(WWWResult www)
    {
      if (SRPG.Network.IsError)
      {
        int errCode = (int) SRPG.Network.ErrCode;
        FlowNode_Network.Failed();
      }
      else
      {
        ReqAutoRepeatQuestBox.Response body;
        if (!EncodingTypes.IsJsonSerializeCompressSelected(this.SerializeCompressMethod))
        {
          FlowNode_ReqAutoRepeatQuestBox.MP_ReqAutoRepeatQuestBoxResponse questBoxResponse = SerializerCompressorHelper.Decode<FlowNode_ReqAutoRepeatQuestBox.MP_ReqAutoRepeatQuestBoxResponse>(www.rawResult, true, EncodingTypes.GetCompressModeFromSerializeCompressMethod(this.SerializeCompressMethod));
          DebugUtility.Assert(questBoxResponse != null, "mpRes == null");
          body = questBoxResponse.body;
        }
        else
        {
          WebAPI.JSON_BodyResponse<ReqAutoRepeatQuestBox.Response> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<ReqAutoRepeatQuestBox.Response>>(www.text);
          DebugUtility.Assert(jsonObject != null, "jsonRes == null");
          body = jsonObject.body;
        }
        SRPG.Network.RemoveAPI();
        if (body != null)
        {
          MonoSingleton<GameManager>.Instance.Player.SetAutoRepeatQuestBox(body.box_extension_count);
          MonoSingleton<GameManager>.Instance.Player.SetAutoRepeatQuestApItemPriority(body.drip_priority);
          MonoSingleton<GameManager>.Instance.Player.SetAutoRepeatQuestBoxExpansion(body.box_expansion_purchase_count);
          ((Behaviour) this).enabled = false;
          this.ActivateOutputLinks(110);
        }
        else
          FlowNode_Network.Failed();
      }
    }

    [MessagePackObject(true)]
    public class MP_ReqAutoRepeatQuestBoxResponse : WebAPI.JSON_BaseResponse
    {
      public ReqAutoRepeatQuestBox.Response body;
    }
  }
}
