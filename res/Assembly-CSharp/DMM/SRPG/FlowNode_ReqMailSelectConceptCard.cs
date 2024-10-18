// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_ReqMailSelectConceptCard
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using UnityEngine;

#nullable disable
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
        ((Behaviour) this).enabled = false;
      }
      else
      {
        ((Behaviour) this).enabled = true;
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
