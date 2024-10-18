// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_ReqMail
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using UnityEngine;

#nullable disable
namespace SRPG
{
  [FlowNode.NodeType("Mail/ReqMail", 32741)]
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
      MailWindow.MailPageRequestData dataOfClass = DataSource.FindDataOfClass<MailWindow.MailPageRequestData>(((Component) this).gameObject, (MailWindow.MailPageRequestData) null);
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
        WebAPI.JSON_BodyResponse<Json_MailPage> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<Json_MailPage>>(www.text);
        MonoSingleton<GameManager>.Instance.Player.Deserialize(jsonObject.body.mails);
        if (jsonObject.body.mails != null)
          GlobalVars.ConceptCardNum.Set(jsonObject.body.mails.concept_count);
        this.ActivateOutputLinks(10);
        Network.RemoveAPI();
      }
    }
  }
}
