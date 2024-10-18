// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_ReqGenesisRaidboss
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System;
using UnityEngine;

#nullable disable
namespace SRPG
{
  [FlowNode.NodeType("Genesis/Req/Raidboss", 32741)]
  [FlowNode.Pin(1, "Request", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(101, "Success", FlowNode.PinTypes.Output, 101)]
  [FlowNode.Pin(102, "OutOfPeriod", FlowNode.PinTypes.Output, 102)]
  public class FlowNode_ReqGenesisRaidboss : FlowNode_Network
  {
    protected const int PIN_IN_REQUEST = 1;
    protected const int PIN_OUT_SUCCESS = 101;
    protected const int PIN_OUT_OUT_OF_PERIOD = 102;

    public override void OnActivate(int pinID)
    {
      if (pinID != 1)
        return;
      ((Behaviour) this).enabled = true;
      GenesisChapterManager instance = GenesisChapterManager.Instance;
      this.ExecRequest((WebAPI) new ReqGenesisRaidboss(instance.CurrentChapterParam.AreaId, instance.BossDifficulty, new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback)));
    }

    private void Success()
    {
      ((Behaviour) this).enabled = false;
      this.ActivateOutputLinks(101);
    }

    public override void OnSuccess(WWWResult www)
    {
      if (Network.IsError)
      {
        if (Network.ErrCode == Network.EErrCode.Genesis_OutOfPeriod)
        {
          Network.RemoveAPI();
          Network.ResetError();
          this.ActivateOutputLinks(102);
          ((Behaviour) this).enabled = false;
        }
        else
          this.OnRetry();
      }
      else
      {
        WebAPI.JSON_BodyResponse<ReqGenesisRaidboss.Response> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<ReqGenesisRaidboss.Response>>(www.text);
        DebugUtility.Assert(jsonObject != null, "res == null");
        if (jsonObject.body == null)
        {
          this.OnRetry();
        }
        else
        {
          try
          {
            GenesisBossInfo.Instance.SetBossInfo(jsonObject.body.hp, jsonObject.body.round);
          }
          catch (Exception ex)
          {
            DebugUtility.LogException(ex);
            this.OnRetry();
            return;
          }
          Network.RemoveAPI();
          this.Success();
        }
      }
    }
  }
}
