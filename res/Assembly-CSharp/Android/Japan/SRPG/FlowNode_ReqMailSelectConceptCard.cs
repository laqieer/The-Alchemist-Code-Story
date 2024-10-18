// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_ReqMailSelectConceptCard
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;

namespace SRPG
{
  [FlowNode.NodeType("Mail/ReqMailSelectConceptCard", 32741)]
  [FlowNode.Pin(0, "Request", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(1, "Success", FlowNode.PinTypes.Output, 1)]
  public class FlowNode_ReqMailSelectConceptCard : FlowNode_Network
  {
    public GetConceptCardListWindow m_GetConceptCardListWindow;

    public override void OnActivate(int pinID)
    {
      if (pinID != 0)
        return;
      MailData mail = MonoSingleton<GameManager>.Instance.FindMail((long) GlobalVars.SelectedMailUniqueID);
      if (mail == null)
      {
        this.enabled = false;
      }
      else
      {
        this.enabled = true;
        this.ExecRequest((WebAPI) new ReqMailSelect(mail.Find(GiftTypes.SelectConceptCardItem).iname, ReqMailSelect.type.conceptcard, new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback)));
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
        WebAPI.JSON_BodyResponse<FlowNode_ReqMailSelectConceptCard.Json> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<FlowNode_ReqMailSelectConceptCard.Json>>(www.text);
        DebugUtility.Assert(jsonObject != null, "res == null");
        if (jsonObject.body == null)
        {
          this.OnRetry();
        }
        else
        {
          Network.RemoveAPI();
          if (jsonObject.body.select == null || jsonObject.body.select.Length <= 0)
            return;
          ConceptCardData[] data = new ConceptCardData[jsonObject.body.select.Length];
          for (int index = 0; index < jsonObject.body.select.Length; ++index)
          {
            FlowNode_ReqMailSelectConceptCard.Json_SelectConceptCard selectConceptCard = jsonObject.body.select[index];
            data[index] = ConceptCardData.CreateConceptCardDataForDisplay(selectConceptCard.iname);
            MonoSingleton<GameManager>.Instance.Player.SetConceptCardNum(selectConceptCard.iname, selectConceptCard.has_count);
          }
          this.m_GetConceptCardListWindow.Setup(data);
        }
      }
    }

    public class Json
    {
      public FlowNode_ReqMailSelectConceptCard.Json_SelectConceptCard[] select;
    }

    public class Json_SelectConceptCard
    {
      public long id;
      public string iname;
      public int has_count;
    }
  }
}
