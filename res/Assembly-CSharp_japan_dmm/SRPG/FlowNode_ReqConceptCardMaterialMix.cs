// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_ReqConceptCardMaterialMix
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
namespace SRPG
{
  [FlowNode.NodeType("ConceptCard/Req/ReqConceptCardMaterialDataMix", 32741)]
  [FlowNode.Pin(10, "真理念装用素材を使った合成開始", FlowNode.PinTypes.Input, 10)]
  [FlowNode.Pin(1000, "真理念装用素材を使った合成完了", FlowNode.PinTypes.Output, 1000)]
  public class FlowNode_ReqConceptCardMaterialMix : FlowNode_Network
  {
    private const int INPUT_CONCEPT_CARD_MATERIAL_DATA_MIX_START = 10;
    private const int OUTPUT_CONCEPT_CARD_MATERIAL_DATA_MIX_END = 1000;
    private long mBaseCardId;
    private List<SelecteConceptCardMaterial> mMaterials;
    private int totalMixZeny;

    public void SetMixParam()
    {
      ConceptCardManager instance = ConceptCardManager.Instance;
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) instance, (UnityEngine.Object) null))
        return;
      ConceptCardManager.GalcTotalMixZenyMaterialData(out this.totalMixZeny);
      instance.SetupBulkLevelupAnimation();
      this.mBaseCardId = (long) instance.SelectedConceptCardData.UniqueID;
      this.mMaterials = new List<SelecteConceptCardMaterial>((IEnumerable<SelecteConceptCardMaterial>) instance.BulkSelectedMaterialList);
      instance.BulkSelectedMaterialList.Clear();
    }

    public override void OnActivate(int pinID)
    {
      if (pinID != 10)
        return;
      this.SetMixParam();
      ((Behaviour) this).enabled = true;
      this.ExecRequest((WebAPI) new ReqMixConceptCardMaterialData(this.mBaseCardId, this.mMaterials, new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback)));
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
        WebAPI.JSON_BodyResponse<FlowNode_ReqConceptCardMaterialMix.Json_ConceptCardMaterialDataMix> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<FlowNode_ReqConceptCardMaterialMix.Json_ConceptCardMaterialDataMix>>(www.text);
        DebugUtility.Assert(jsonObject != null, "res == null");
        Network.RemoveAPI();
        long iid = -1;
        int beforeLevel = -1;
        int beforeAwakeCount = -1;
        int beforeTrust = -1;
        int num = -1;
        if (jsonObject.body.concept_card != null)
        {
          iid = jsonObject.body.concept_card.iid;
          ConceptCardData conceptCardByUniqueId = MonoSingleton<GameManager>.Instance.Player.FindConceptCardByUniqueID(iid);
          if (conceptCardByUniqueId != null)
          {
            beforeLevel = (int) conceptCardByUniqueId.Lv;
            beforeAwakeCount = (int) conceptCardByUniqueId.AwakeCount;
            beforeTrust = (int) conceptCardByUniqueId.Trust;
            num = conceptCardByUniqueId.TrustBonus;
          }
        }
        try
        {
          MonoSingleton<GameManager>.Instance.Deserialize(jsonObject.body.player);
          MonoSingleton<GameManager>.Instance.Player.Deserialize(jsonObject.body.concept_card);
          MonoSingleton<GameManager>.Instance.Player.OverWriteConceptCardMaterials(jsonObject.body.materials);
        }
        catch (Exception ex)
        {
          DebugUtility.LogException(ex);
          return;
        }
        MonoSingleton<GameManager>.Instance.Player.OnGoldChange(this.totalMixZeny);
        MonoSingleton<GameManager>.Instance.ServerSyncTrophyExecEnd(www);
        ConceptCardData conceptCardByUniqueId1 = MonoSingleton<GameManager>.Instance.Player.FindConceptCardByUniqueID(iid);
        MonoSingleton<GameManager>.Instance.Player.OnMixedConceptCard(jsonObject.body.concept_card.iname, beforeLevel, (int) conceptCardByUniqueId1.Lv, beforeAwakeCount, (int) conceptCardByUniqueId1.AwakeCount, beforeTrust, (int) conceptCardByUniqueId1.Trust);
        conceptCardByUniqueId1.SetBonusCount(conceptCardByUniqueId1.TrustBonus - num);
        this.ActivateOutputLinks(1000);
        ((Behaviour) this).enabled = false;
      }
    }

    public class UseItem
    {
      private long unique_id;
      private int use_num;
    }

    public class Json_ConceptCardMaterialDataMix
    {
      public Json_PlayerData player;
      public JSON_ConceptCard concept_card;
      public FlowNode_ReqConceptCardMaterialMix.UseItem[] use_item;
      public JSON_ConceptCardMaterial[] materials;
    }
  }
}
