// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_ReqMail
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using GR;

namespace SRPG
{
  [FlowNode.NodeType("System/ReqMail", 32741)]
  [FlowNode.Pin(0, "メール取得", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(10, "成功", FlowNode.PinTypes.Output, 10)]
  public class FlowNode_ReqMail : FlowNode_Network
  {
    private const int PIN_ID_REQUEST = 0;
    private const int PIN_ID_SUCCESS = 10;

    public override void OnActivate(int pinID)
    {
      if (pinID != 0)
        return;
      MailWindow.MailPageRequestData dataOfClass = DataSource.FindDataOfClass<MailWindow.MailPageRequestData>(this.gameObject, (MailWindow.MailPageRequestData) null);
      this.ExecRequest((WebAPI) new ReqMail(dataOfClass.page, dataOfClass.isPeriod, dataOfClass.isRead, new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback)));
    }

    public override void OnSuccess(WWWResult www)
    {
      if (Network.IsError)
      {
        switch (Network.ErrCode)
        {
          case Network.EErrCode.NoMail:
            this.OnBack();
            break;
          case Network.EErrCode.MailReadable:
            this.OnBack();
            break;
          default:
            this.OnRetry();
            break;
        }
      }
      else
      {
        MonoSingleton<GameManager>.Instance.Player.Deserialize(JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<Json_MailPage>>(www.text).body.mails);
        this.ActivateOutputLinks(10);
        Network.RemoveAPI();
      }
    }
  }
}
