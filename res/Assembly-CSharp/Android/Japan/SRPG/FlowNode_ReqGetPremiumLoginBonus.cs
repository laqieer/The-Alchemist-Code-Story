// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_ReqGetPremiumLoginBonus
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;
using System;

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
        this.enabled = true;
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
        this.enabled = false;
      }
    }
  }
}
