// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_ReqMailCheck
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;

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
