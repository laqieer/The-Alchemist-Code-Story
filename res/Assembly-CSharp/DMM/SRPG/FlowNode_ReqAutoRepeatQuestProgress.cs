// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_ReqAutoRepeatQuestProgress
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
  [FlowNode.NodeType("AutoRepeatQuest/ReqProgress", 32741)]
  [FlowNode.Pin(10, "進捗取得開始", FlowNode.PinTypes.Input, 10)]
  [FlowNode.Pin(110, "進捗取得終了", FlowNode.PinTypes.Output, 110)]
  public class FlowNode_ReqAutoRepeatQuestProgress : FlowNode_Network
  {
    private const int PIN_INPUT_GET_PROGRESS_START = 10;
    private const int PIN_OUTPUT_GET_PROGRESS_END = 110;

    public override void OnActivate(int pinID)
    {
      if (pinID == 10)
        this.RequestProgress();
      ((Behaviour) this).enabled = true;
    }

    private void RequestProgress()
    {
      if (!MonoSingleton<GameManager>.Instance.Player.AutoRepeatQuestProgress.IsExistRecord)
        return;
      this.ExecRequest((WebAPI) new ReqAutoRepeatQuestProgress(MonoSingleton<GameManager>.Instance.Player.AutoRepeatQuestProgress.CurrentLap, new SRPG.Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback), EncodingTypes.ESerializeCompressMethod.TYPED_MESSAGE_PACK));
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
        ReqAutoRepeatQuestProgress.Response body;
        if (EncodingTypes.IsJsonSerializeCompressSelected(!GlobalVars.SelectedSerializeCompressMethodWasNodeSet ? EncodingTypes.ESerializeCompressMethod.TYPED_MESSAGE_PACK : GlobalVars.SelectedSerializeCompressMethod))
        {
          WebAPI.JSON_BodyResponse<ReqAutoRepeatQuestProgress.Response> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<ReqAutoRepeatQuestProgress.Response>>(www.text);
          DebugUtility.Assert(jsonObject != null, "jsonRes == null");
          body = jsonObject.body;
        }
        else
        {
          FlowNode_ReqAutoRepeatQuestProgress.MP_AutoRepeatQuestProgressResponse progressResponse = SerializerCompressorHelper.Decode<FlowNode_ReqAutoRepeatQuestProgress.MP_AutoRepeatQuestProgressResponse>(www.rawResult, true, EncodingTypes.GetCompressModeFromSerializeCompressMethod(EncodingTypes.ESerializeCompressMethod.TYPED_MESSAGE_PACK));
          DebugUtility.Assert(progressResponse != null, "mpRes == null");
          body = progressResponse.body;
        }
        SRPG.Network.RemoveAPI();
        if (body != null)
        {
          MonoSingleton<GameManager>.Instance.Player.Deserialize(body.auto_repeat, false);
          MonoSingleton<GameManager>.Instance.Player.SetAutoRepeatQuestBox(body.box_extension_count);
          MonoSingleton<GameManager>.Instance.Player.SetAutoRepeatQuestBoxExpansion(body.box_expansion_purchase_count);
          ((Behaviour) this).enabled = false;
          this.ActivateOutputLinks(110);
        }
        else
          FlowNode_Network.Failed();
      }
    }

    [MessagePackObject(true)]
    public class MP_AutoRepeatQuestProgressResponse : WebAPI.JSON_BaseResponse
    {
      public ReqAutoRepeatQuestProgress.Response body;
    }
  }
}
