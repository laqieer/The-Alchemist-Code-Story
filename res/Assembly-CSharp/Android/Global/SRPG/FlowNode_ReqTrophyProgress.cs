﻿// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_ReqTrophyProgress
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using GR;
using System;

namespace SRPG
{
  [FlowNode.Pin(1, "Success", FlowNode.PinTypes.Output, 1)]
  [FlowNode.NodeType("System/ReqTrophyProgress", 32741)]
  [FlowNode.Pin(0, "Request", FlowNode.PinTypes.Input, 0)]
  public class FlowNode_ReqTrophyProgress : FlowNode_Network
  {
    public override void OnActivate(int pinID)
    {
      if (pinID != 0)
        return;
      if (Network.Mode == Network.EConnectMode.Offline)
      {
        this.enabled = false;
        this.Success();
      }
      else
      {
        this.ExecRequest((WebAPI) new ReqTrophyProgress(new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback)));
        this.enabled = true;
      }
    }

    private void Success()
    {
      this.enabled = false;
      this.ActivateOutputLinks(1);
    }

    public override void OnSuccess(WWWResult www)
    {
      if (Network.IsError)
      {
        Network.EErrCode errCode = Network.ErrCode;
        this.OnRetry();
      }
      else
      {
        WebAPI.JSON_BodyResponse<JSON_TrophyResponse> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<JSON_TrophyResponse>>(www.text);
        DebugUtility.Assert(jsonObject != null, "res == null");
        if (jsonObject.body == null)
          this.OnRetry();
        else if (jsonObject.body.trophyprogs == null)
        {
          Network.RemoveAPI();
          this.Success();
        }
        else
        {
          GameManager instance = MonoSingleton<GameManager>.Instance;
          for (int index1 = 0; index1 < jsonObject.body.trophyprogs.Length; ++index1)
          {
            JSON_TrophyProgress trophyprog = jsonObject.body.trophyprogs[index1];
            if (trophyprog != null)
            {
              if (instance.MasterParam.GetTrophy(trophyprog.iname) == null)
              {
                DebugUtility.LogError("存在しないミッション:" + trophyprog.iname);
              }
              else
              {
                TrophyState trophyCounter = instance.Player.GetTrophyCounter(instance.MasterParam.GetTrophy(trophyprog.iname), false);
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
          Network.RemoveAPI();
          this.Success();
        }
      }
    }
  }
}
