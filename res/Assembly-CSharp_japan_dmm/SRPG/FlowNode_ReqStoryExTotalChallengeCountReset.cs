// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_ReqStoryExTotalChallengeCountReset
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
  [FlowNode.NodeType("StoryExTotalChallengeCount/Reset", 32741)]
  [FlowNode.Pin(10, "トータル挑戦回数リセット開始", FlowNode.PinTypes.Input, 10)]
  [FlowNode.Pin(110, "トータル挑戦回数リセット終了", FlowNode.PinTypes.Output, 110)]
  [FlowNode.Pin(120, "幻晶石が不足", FlowNode.PinTypes.Output, 120)]
  public class FlowNode_ReqStoryExTotalChallengeCountReset : FlowNode_Network
  {
    private const int PIN_INPUT_START = 10;
    private const int PIN_OUTPUT_END = 110;
    private const int PIN_OUTPUT_ERR_NOT_ENOUGH_COIN = 120;
    [SerializeField]
    private eResetCostType mCostType;

    public override void OnActivate(int pinID)
    {
      if (pinID != 10)
        return;
      ((Behaviour) this).enabled = true;
      this.SerializeCompressMethod = EncodingTypes.ESerializeCompressMethod.TYPED_MESSAGE_PACK;
      this.ExecRequest((WebAPI) new ReqStoryExChallengeCountReset(this.mCostType, new SRPG.Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback), this.SerializeCompressMethod));
    }

    public override void OnSuccess(WWWResult www)
    {
      if (SRPG.Network.IsError)
      {
        switch (SRPG.Network.ErrCode)
        {
          case SRPG.Network.EErrCode.QuestResetNotEnoughCoin:
            this.ActivateOutputLinks(120);
            ((Behaviour) this).enabled = false;
            SRPG.Network.RemoveAPI();
            SRPG.Network.ResetError();
            break;
          default:
            FlowNode_Network.Failed();
            break;
        }
      }
      else
      {
        ReqStoryExChallengeCountReset.Response body;
        if (!EncodingTypes.IsJsonSerializeCompressSelected(this.SerializeCompressMethod))
        {
          FlowNode_ReqStoryExTotalChallengeCountReset.MP_ReqStoryExChallengeCountResetResponse countResetResponse = SerializerCompressorHelper.Decode<FlowNode_ReqStoryExTotalChallengeCountReset.MP_ReqStoryExChallengeCountResetResponse>(www.rawResult, true, EncodingTypes.GetCompressModeFromSerializeCompressMethod(this.SerializeCompressMethod));
          DebugUtility.Assert(countResetResponse != null, "mpRes == null");
          body = countResetResponse.body;
        }
        else
        {
          WebAPI.JSON_BodyResponse<ReqStoryExChallengeCountReset.Response> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<ReqStoryExChallengeCountReset.Response>>(www.text);
          DebugUtility.Assert(jsonObject != null, "jsonRes == null");
          body = jsonObject.body;
        }
        SRPG.Network.RemoveAPI();
        if (body != null)
        {
          MonoSingleton<GameManager>.Instance.Player.Deserialize(body.player);
          MonoSingleton<GameManager>.Instance.Player.Deserialize(body.story_ex_challenge);
          MonoSingleton<GameManager>.Instance.Deserialize(body.items);
          ((Behaviour) this).enabled = false;
          this.ActivateOutputLinks(110);
        }
        else
          FlowNode_Network.Failed();
      }
    }

    [MessagePackObject(true)]
    public class MP_ReqStoryExChallengeCountResetResponse : WebAPI.JSON_BaseResponse
    {
      public ReqStoryExChallengeCountReset.Response body;
    }
  }
}
