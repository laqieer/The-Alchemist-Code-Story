﻿// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_PlayNew
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using GR;
using System;

namespace SRPG
{
  [FlowNode.NodeType("System/PlayNew", 32741)]
  [FlowNode.Pin(10, "Start", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(1, "Success", FlowNode.PinTypes.Output, 10)]
  [FlowNode.Pin(2, "Reset to Title", FlowNode.PinTypes.Output, 11)]
  public class FlowNode_PlayNew : FlowNode_Network
  {
    public bool IsDebug;

    public void SetDebug(bool check)
    {
      this.IsDebug = check;
    }

    public override void OnActivate(int pinID)
    {
      if (pinID != 10)
        return;
      if (Network.Mode == Network.EConnectMode.Online)
      {
        this.ExecRequest((WebAPI) new ReqPlayNew(this.IsDebug, new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback)));
        this.enabled = true;
      }
      else
        this.Success();
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
        if (Network.ErrCode == Network.EErrCode.CreateStopped)
          this.OnRetry();
        else
          this.OnFailed();
      }
      else
      {
        WebAPI.JSON_BodyResponse<Json_PlayerDataAll> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<Json_PlayerDataAll>>(www.text);
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
            instance.Deserialize(jsonObject.body.player);
            instance.Deserialize(jsonObject.body.units);
            instance.Deserialize(jsonObject.body.items);
            if (!instance.Deserialize(jsonObject.body.mails))
            {
              this.Failure();
              return;
            }
            instance.Deserialize(jsonObject.body.parties);
            instance.Deserialize(jsonObject.body.friends);
            instance.Deserialize(jsonObject.body.notify);
            instance.Deserialize(jsonObject.body.skins);
          }
          catch (Exception ex)
          {
            DebugUtility.LogException(ex);
            this.Failure();
            return;
          }
          GameUtility.Config_OkyakusamaCode = instance.Player.OkyakusamaCode;
          GlobalVars.CustomerID = instance.Player.CUID;
          instance.PostLogin();
          this.Success();
        }
      }
    }
  }
}
