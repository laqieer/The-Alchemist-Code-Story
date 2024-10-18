// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_ReqTrophyProgress
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using Gsc.Network.Encoding;
using System;
using UnityEngine;

#nullable disable
namespace SRPG
{
  [FlowNode.NodeType("Trophy/ReqTrophyProgress", 32741)]
  [FlowNode.Pin(0, "Request", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(1, "Success", FlowNode.PinTypes.Output, 1)]
  public class FlowNode_ReqTrophyProgress : FlowNode_Network
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
        this.ExecRequest((WebAPI) new ReqTrophyProgress(new SRPG.Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback), this.SerializeCompressMethod));
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
      JSON_TrophyResponse body;
      if (EncodingTypes.IsJsonSerializeCompressSelected(this.SerializeCompressMethod))
      {
        if (SRPG.Network.IsError)
        {
          int errCode = (int) SRPG.Network.ErrCode;
          this.OnRetry();
          return;
        }
        WebAPI.JSON_BodyResponse<JSON_TrophyResponse> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<JSON_TrophyResponse>>(www.text);
        if (jsonObject == null || jsonObject.body == null)
        {
          this.OnRetry();
          return;
        }
        body = jsonObject.body;
      }
      else
      {
        MP_TrophyResponse mpTrophyResponse = SerializerCompressorHelper.Decode<MP_TrophyResponse>(www.rawResult, true, EncodingTypes.GetCompressModeFromSerializeCompressMethod(this.SerializeCompressMethod));
        SRPG.Network.EErrCode stat = (SRPG.Network.EErrCode) mpTrophyResponse.stat;
        string statMsg = mpTrophyResponse.stat_msg;
        if (stat != SRPG.Network.EErrCode.Success)
          SRPG.Network.SetServerMetaDataAsError(stat, statMsg);
        if (SRPG.Network.IsError)
        {
          int errCode = (int) SRPG.Network.ErrCode;
          this.OnRetry();
          return;
        }
        body = mpTrophyResponse.body;
      }
      DebugUtility.Assert(body != null, "trophyResponse == null");
      if (body == null)
        this.OnRetry();
      else if (body.trophyprogs == null)
      {
        SRPG.Network.RemoveAPI();
        this.Success();
      }
      else
      {
        GameManager instance = MonoSingleton<GameManager>.Instance;
        for (int index1 = 0; index1 < body.trophyprogs.Length; ++index1)
        {
          JSON_TrophyProgress trophyprog = body.trophyprogs[index1];
          if (trophyprog != null)
          {
            if (instance.MasterParam.GetTrophy(trophyprog.iname) == null)
            {
              DebugUtility.LogError("存在しないミッション:" + trophyprog.iname);
            }
            else
            {
              TrophyState trophyCounter = instance.Player.TrophyData.GetTrophyCounter(instance.MasterParam.GetTrophy(trophyprog.iname));
              for (int index2 = 0; index2 < trophyprog.pts.Length && index2 < trophyCounter.Count.Length; ++index2)
                trophyCounter.Count[index2] = trophyprog.pts[index2];
              trophyCounter.StartYMD = trophyprog.ymd;
              trophyCounter.IsEnded = trophyprog.rewarded_at != 0;
              if (trophyprog.rewarded_at != 0)
              {
                try
                {
                  trophyCounter.RewardedAt = trophyprog.rewarded_at.FromYMD();
                }
                catch
                {
                  trophyCounter.RewardedAt = DateTime.MinValue;
                }
              }
              else
                trophyCounter.RewardedAt = DateTime.MinValue;
            }
          }
        }
        SRPG.Network.RemoveAPI();
        this.Success();
      }
    }
  }
}
