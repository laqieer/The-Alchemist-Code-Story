// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_ReqMailCheck
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;

#nullable disable
namespace SRPG
{
  [FlowNode.NodeType("Mail/ReqMailCheck", 32741)]
  [FlowNode.Pin(0, "ギフトがあるか", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(10, "true", FlowNode.PinTypes.Output, 10)]
  [FlowNode.Pin(11, "false", FlowNode.PinTypes.Output, 11)]
  public class FlowNode_ReqMailCheck : FlowNode_Network
  {
    private const int PIN_INPUT = 0;
    private const int PIN_TRUE = 10;
    private const int PIN_FALSE = 11;

    public override void OnActivate(int pinID)
    {
      if (pinID != 0)
        return;
      this.ExecRequest((WebAPI) new ReqMailCheck(new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback)));
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
        WebAPI.JSON_BodyResponse<Json_MailCheck> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<Json_MailCheck>>(www.text);
        Network.RemoveAPI();
        if (jsonObject.body.mail_reward == 1)
        {
          MonoSingleton<GameManager>.Instance.BadgeFlags |= GameManager.BadgeTypes.GiftBox;
          this.ActivateOutputLinks(10);
        }
        else
          this.ActivateOutputLinks(11);
      }
    }
  }
}
