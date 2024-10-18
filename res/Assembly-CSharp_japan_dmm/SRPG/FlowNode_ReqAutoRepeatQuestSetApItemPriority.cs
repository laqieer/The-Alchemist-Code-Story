// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_ReqAutoRepeatQuestSetApItemPriority
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
  [FlowNode.NodeType("AutoRepeatQuest/ReqSetApItemPriority", 32741)]
  [FlowNode.Pin(10, "AP回復アイテム優先度設定開始", FlowNode.PinTypes.Input, 10)]
  [FlowNode.Pin(110, "AP回復アイテム優先度設定終了", FlowNode.PinTypes.Output, 110)]
  public class FlowNode_ReqAutoRepeatQuestSetApItemPriority : FlowNode_Network
  {
    private const int PIN_INPUT_SET_PRIORITY_START = 10;
    private const int PIN_OUTPUT_SET_PRIORITY_END = 110;

    public override void OnActivate(int pinID)
    {
      if (pinID != 10 || !Object.op_Inequality((Object) AutoRepeatQuestApItemPriority.Instance, (Object) null))
        return;
      string[] apItemPriority = AutoRepeatQuestApItemPriority.Instance.GetApItemPriority();
      ((Behaviour) this).enabled = true;
      this.SerializeCompressMethod = EncodingTypes.ESerializeCompressMethod.TYPED_MESSAGE_PACK;
      this.ExecRequest((WebAPI) new ReqAutoRepeatQuestSetApItemPriority(apItemPriority, new SRPG.Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback), this.SerializeCompressMethod));
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
        ReqAutoRepeatQuestSetApItemPriority.Response body;
        if (!EncodingTypes.IsJsonSerializeCompressSelected(this.SerializeCompressMethod))
        {
          FlowNode_ReqAutoRepeatQuestSetApItemPriority.MP_ReqAutoRepeatQuestSetApItemPriority setApItemPriority = SerializerCompressorHelper.Decode<FlowNode_ReqAutoRepeatQuestSetApItemPriority.MP_ReqAutoRepeatQuestSetApItemPriority>(www.rawResult, true, EncodingTypes.GetCompressModeFromSerializeCompressMethod(this.SerializeCompressMethod));
          DebugUtility.Assert(setApItemPriority != null, "mpRes == null");
          body = setApItemPriority.body;
        }
        else
        {
          WebAPI.JSON_BodyResponse<ReqAutoRepeatQuestSetApItemPriority.Response> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<ReqAutoRepeatQuestSetApItemPriority.Response>>(www.text);
          DebugUtility.Assert(jsonObject != null, "jsonRes == null");
          body = jsonObject.body;
        }
        SRPG.Network.RemoveAPI();
        if (body != null)
        {
          MonoSingleton<GameManager>.Instance.Player.SetAutoRepeatQuestApItemPriority(body.drip_priority);
          ((Behaviour) this).enabled = false;
          this.ActivateOutputLinks(110);
        }
        else
          FlowNode_Network.Failed();
      }
    }

    [MessagePackObject(true)]
    public class MP_ReqAutoRepeatQuestSetApItemPriority : WebAPI.JSON_BaseResponse
    {
      public ReqAutoRepeatQuestSetApItemPriority.Response body;
    }
  }
}
