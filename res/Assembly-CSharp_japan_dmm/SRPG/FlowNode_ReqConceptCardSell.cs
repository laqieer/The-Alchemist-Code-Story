// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_ReqConceptCardSell
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System;
using UnityEngine;

#nullable disable
namespace SRPG
{
  [FlowNode.NodeType("ConceptCard/Req/ReqConceptCardSell", 32741)]
  [FlowNode.Pin(0, "売却する", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(100, "売却した", FlowNode.PinTypes.Output, 100)]
  public class FlowNode_ReqConceptCardSell : FlowNode_Network
  {
    private const int INPUT_CONCEPT_CARD_SELL = 0;
    private const int OUTPUT_CONCEPT_CARD_SOLD = 100;
    private long[] sellCardIDs;
    private int totalSellZeny;

    public void SetSellParam()
    {
      ConceptCardManager instance = ConceptCardManager.Instance;
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) instance, (UnityEngine.Object) null))
        return;
      ConceptCardManager.GalcTotalSellZeny(instance.SelectedMaterials, out this.totalSellZeny);
      this.sellCardIDs = instance.SelectedMaterials.GetUniqueIDs().ToArray();
      instance.SelectedMaterials.Clear();
    }

    public override void OnActivate(int pinID)
    {
      if (pinID != 0)
        return;
      this.SetSellParam();
      ((Behaviour) this).enabled = true;
      this.ExecRequest((WebAPI) new ReqSellConceptCard(this.sellCardIDs, new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback)));
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
        WebAPI.JSON_BodyResponse<FlowNode_ReqConceptCardSell.Json_ConceptCardSell> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<FlowNode_ReqConceptCardSell.Json_ConceptCardSell>>(www.text);
        DebugUtility.Assert(jsonObject != null, "res == null");
        Network.RemoveAPI();
        try
        {
          MonoSingleton<GameManager>.Instance.Deserialize(jsonObject.body.player);
          if (jsonObject.body.items != null)
            MonoSingleton<GameManager>.Instance.Deserialize(jsonObject.body.items);
          if (jsonObject.body.sell_ids != null)
            MonoSingleton<GameManager>.Instance.Player.RemoveConceptCardData(jsonObject.body.sell_ids);
        }
        catch (Exception ex)
        {
          DebugUtility.LogException(ex);
          return;
        }
        MonoSingleton<GameManager>.Instance.Player.OnGoldChange(this.totalSellZeny);
        this.ActivateOutputLinks(100);
        ((Behaviour) this).enabled = false;
      }
    }

    public class Json_ConceptCardSell
    {
      public Json_PlayerData player;
      public Json_Item[] items;
      public long[] sell_ids;
    }
  }
}
