// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_ReqGetPremiumLoginBonus
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System;
using UnityEngine;

#nullable disable
namespace SRPG
{
  [FlowNode.NodeType("System/Login/ReqGetPremiumLoginBonus", 32741)]
  [FlowNode.Pin(1, "プレミアムログインボーナス情報をリクエスト", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(101, "すでに取得している", FlowNode.PinTypes.Output, 101)]
  [FlowNode.Pin(102, "リクエストを出して取得", FlowNode.PinTypes.Output, 102)]
  public class FlowNode_ReqGetPremiumLoginBonus : FlowNode_Network
  {
    private const int INPUT_GET_PREMIUM_LOGINBONUS = 1;
    private const int OUTPUT_AlREADY_GET_LOGINBONUS = 101;
    private const int OUTPUT_REQUEST_GET_LOGINBONUS = 102;

    public override void OnActivate(int pinID)
    {
      if (pinID != 1)
        return;
      if (MonoSingleton<GameManager>.Instance.Player.PremiumLoginBonus != null)
      {
        this.ActivateOutputLinks(101);
      }
      else
      {
        ((Behaviour) this).enabled = true;
        this.ExecRequest((WebAPI) new ReqGetPremiumLoginBonus(new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback)));
      }
    }

    public override void OnSuccess(WWWResult www)
    {
      if (Network.IsError)
      {
        int errCode = (int) Network.ErrCode;
        this.OnRetry();
      }
      else
      {
        WebAPI.JSON_BodyResponse<Json_LoginBonusTable> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<Json_LoginBonusTable>>(www.text);
        DebugUtility.Assert(jsonObject != null, "res == null");
        Network.RemoveAPI();
        try
        {
          MonoSingleton<GameManager>.Instance.Player.SetPremiumLoginBonus(jsonObject.body);
        }
        catch (Exception ex)
        {
          DebugUtility.LogException(ex);
          return;
        }
        this.ActivateOutputLinks(102);
        ((Behaviour) this).enabled = false;
      }
    }
  }
}
