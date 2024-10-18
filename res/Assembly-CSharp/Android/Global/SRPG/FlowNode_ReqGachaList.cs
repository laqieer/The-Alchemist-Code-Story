﻿// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_ReqGachaList
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using GR;
using System;

namespace SRPG
{
  [FlowNode.Pin(2, "Failure", FlowNode.PinTypes.Output, 2)]
  [FlowNode.NodeType("Network/gacha", 32741)]
  [FlowNode.Pin(0, "Request", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(1, "Success", FlowNode.PinTypes.Output, 1)]
  public class FlowNode_ReqGachaList : FlowNode_Network
  {
    public override void OnActivate(int pinID)
    {
      if (pinID != 0)
        return;
      if (Network.Mode == Network.EConnectMode.Offline)
      {
        this.Success();
      }
      else
      {
        this.ExecRequest((WebAPI) new ReqGacha(new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback)));
        this.enabled = true;
      }
    }

    private void Success()
    {
      this.enabled = false;
      this.ActivateOutputLinks(1);
    }

    private void Failure()
    {
      this.enabled = false;
      this.ActivateOutputLinks(2);
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
        WebAPI.JSON_BodyResponse<Json_GachaList> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<Json_GachaList>>(www.text);
        DebugUtility.Assert(jsonObject != null, "res == null");
        Network.RemoveAPI();
        if (jsonObject.body == null)
        {
          this.Failure();
        }
        else
        {
          GameManager instance = MonoSingleton<GameManager>.Instance;
          try
          {
            if (!instance.Deserialize(jsonObject.body))
            {
              this.Failure();
              return;
            }
          }
          catch (Exception ex)
          {
            DebugUtility.LogException(ex);
            this.Failure();
            return;
          }
          this.Success();
        }
      }
    }
  }
}
