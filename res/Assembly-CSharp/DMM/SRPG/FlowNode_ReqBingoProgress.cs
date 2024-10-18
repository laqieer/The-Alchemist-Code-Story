// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_ReqBingoProgress
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
  [FlowNode.NodeType("System/ReqBingo/ReqBingoProgress", 32741)]
  [FlowNode.Pin(0, "Request", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(1, "Success", FlowNode.PinTypes.Output, 1)]
  public class FlowNode_ReqBingoProgress : FlowNode_Network
  {
    public override void OnActivate(int pinID)
    {
      if (pinID != 0)
        return;
      if (SRPG.Network.Mode == SRPG.Network.EConnectMode.Offline)
      {
        ((Behaviour) this).enabled = false;
        this.Success();
      }
      else
      {
        this.SerializeCompressMethod = EncodingTypes.ESerializeCompressMethod.TYPED_MESSAGE_PACK;
        this.ExecRequest((WebAPI) new ReqBingoProgress(new SRPG.Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback), this.SerializeCompressMethod));
        ((Behaviour) this).enabled = true;
      }
    }

    private void Success()
    {
      ((Behaviour) this).enabled = false;
      this.ActivateOutputLinks(1);
    }

    public override void OnSuccess(WWWResult www)
    {
      FlowNode_ReqBingoProgress.JSON_BingoResponse body;
      if (EncodingTypes.IsJsonSerializeCompressSelected(this.SerializeCompressMethod))
      {
        if (SRPG.Network.IsError)
        {
          this.OnRetry();
          return;
        }
        WebAPI.JSON_BodyResponse<FlowNode_ReqBingoProgress.JSON_BingoResponse> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<FlowNode_ReqBingoProgress.JSON_BingoResponse>>(www.text);
        if (jsonObject == null || jsonObject.body == null)
        {
          this.OnRetry();
          return;
        }
        body = jsonObject.body;
      }
      else
      {
        FlowNode_ReqBingoProgress.MP_BingoResponse mpBingoResponse = SerializerCompressorHelper.Decode<FlowNode_ReqBingoProgress.MP_BingoResponse>(www.rawResult, true, EncodingTypes.GetCompressModeFromSerializeCompressMethod(this.SerializeCompressMethod));
        SRPG.Network.EErrCode stat = (SRPG.Network.EErrCode) mpBingoResponse.stat;
        string statMsg = mpBingoResponse.stat_msg;
        if (stat != SRPG.Network.EErrCode.Success)
          SRPG.Network.SetServerMetaDataAsError(stat, statMsg);
        if (SRPG.Network.IsError)
        {
          this.OnRetry();
          return;
        }
        body = mpBingoResponse.body;
      }
      DebugUtility.Assert(body != null, "res == null");
      if (body == null)
        this.OnRetry();
      else if (body.bingoprogs == null)
      {
        SRPG.Network.RemoveAPI();
        this.Success();
      }
      else
      {
        GameManager instance = MonoSingleton<GameManager>.Instance;
        for (int index1 = 0; index1 < body.bingoprogs.Length; ++index1)
        {
          JSON_TrophyProgress bingoprog = body.bingoprogs[index1];
          if (bingoprog != null)
          {
            if (instance.MasterParam.GetTrophy(bingoprog.iname) == null)
            {
              DebugUtility.LogError("存在しないミッション:" + bingoprog.iname);
            }
            else
            {
              TrophyState trophyCounter = instance.Player.TrophyData.GetTrophyCounter(instance.MasterParam.GetTrophy(bingoprog.iname));
              for (int index2 = 0; index2 < bingoprog.pts.Length && index2 < trophyCounter.Count.Length; ++index2)
                trophyCounter.Count[index2] = bingoprog.pts[index2];
              trophyCounter.StartYMD = bingoprog.ymd;
              trophyCounter.IsEnded = bingoprog.rewarded_at != 0;
            }
          }
        }
        SRPG.Network.RemoveAPI();
        this.Success();
      }
    }

    [MessagePackObject(true)]
    public class JSON_BingoResponse
    {
      public JSON_TrophyProgress[] bingoprogs;
    }

    [MessagePackObject(true)]
    public class MP_BingoResponse : WebAPI.JSON_BaseResponse
    {
      public FlowNode_ReqBingoProgress.JSON_BingoResponse body;
    }
  }
}
