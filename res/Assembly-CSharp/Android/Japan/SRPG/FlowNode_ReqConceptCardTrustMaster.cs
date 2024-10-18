// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_ReqConceptCardTrustMaster
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;
using System;

namespace SRPG
{
  [FlowNode.NodeType("ConceptCard/Req/ReqConceptCardTrustMaster", 32741)]
  [FlowNode.Pin(100, "報酬未受取のトラストマスター達成", FlowNode.PinTypes.Input, 100)]
  [FlowNode.Pin(1000, "報酬受取完了", FlowNode.PinTypes.Output, 1000)]
  public class FlowNode_ReqConceptCardTrustMaster : FlowNode_Network
  {
    private const int INPUT_TRUSTMASTER_ON = 100;
    private const int OUTPUT_TRUSTMASTER_ON = 1000;
    private int mOutPutPinId;

    public override void OnActivate(int pinID)
    {
      ConceptCardManager instance = ConceptCardManager.Instance;
      if ((UnityEngine.Object) instance == (UnityEngine.Object) null)
        return;
      if (pinID == 100)
      {
        this.ExecRequest((WebAPI) new ReqTrustMasterConceptCard((long) instance.SelectedConceptCardData.UniqueID, true, new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback)));
        this.mOutPutPinId = 1000;
      }
      this.enabled = true;
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
        WebAPI.JSON_BodyResponse<FlowNode_ReqConceptCardTrustMaster.Json_ConceptCardTrustMaster> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<FlowNode_ReqConceptCardTrustMaster.Json_ConceptCardTrustMaster>>(www.text);
        DebugUtility.Assert(jsonObject != null, "res == null");
        Network.RemoveAPI();
        long iid = -1;
        int num = -1;
        if (jsonObject.body.concept_card != null)
        {
          iid = jsonObject.body.concept_card.iid;
          ConceptCardData conceptCardByUniqueId = MonoSingleton<GameManager>.Instance.Player.FindConceptCardByUniqueID(iid);
          if (conceptCardByUniqueId != null)
            num = conceptCardByUniqueId.TrustBonus;
        }
        try
        {
          MonoSingleton<GameManager>.Instance.Player.Deserialize(jsonObject.body.concept_card);
        }
        catch (Exception ex)
        {
          DebugUtility.LogException(ex);
          return;
        }
        ConceptCardData conceptCardByUniqueId1 = MonoSingleton<GameManager>.Instance.Player.FindConceptCardByUniqueID(iid);
        conceptCardByUniqueId1.SetBonusCount(conceptCardByUniqueId1.TrustBonus - num);
        this.ActivateOutputLinks(this.mOutPutPinId);
        this.enabled = false;
      }
    }

    public class Json_ConceptCardTrustMaster
    {
      public JSON_ConceptCard concept_card;
    }
  }
}
