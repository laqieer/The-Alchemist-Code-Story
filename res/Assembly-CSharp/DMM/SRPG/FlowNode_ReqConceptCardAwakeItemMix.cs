// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_ReqConceptCardAwakeItemMix
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
  [FlowNode.NodeType("ConceptCard/Req/ReqConceptCardAwakeItemMix", 32741)]
  [FlowNode.Pin(10, "限界突破アイテムを使った合成開始", FlowNode.PinTypes.Input, 10)]
  [FlowNode.Pin(1000, "限界突破アイテムを使った合成完了", FlowNode.PinTypes.Output, 1000)]
  public class FlowNode_ReqConceptCardAwakeItemMix : FlowNode_Network
  {
    private const int INPUT_CONCEPT_CARD_AWAKE_ITEM_MIX_START = 10;
    private const int OUTPUT_CONCEPT_CARD_AWAKE_ITEM_MIX_END = 1000;
    private long mBaseCardId;
    private Dictionary<string, int> mMaterials;
    private int totalMixZeny;

    public void SetMixParam()
    {
      ConceptCardManager instance = ConceptCardManager.Instance;
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) instance, (UnityEngine.Object) null))
        return;
      ConceptCardManager.GalcTotalMixZenyMaterialData(out this.totalMixZeny);
      instance.SetupBulkLevelupAnimation();
      this.mBaseCardId = (long) instance.SelectedConceptCardData.UniqueID;
      this.mMaterials = new Dictionary<string, int>((IDictionary<string, int>) instance.SelectedAwakeMaterialList);
      instance.SelectedAwakeMaterialList.Clear();
    }

    public override void OnActivate(int pinID)
    {
      if (pinID != 10)
        return;
      this.SetMixParam();
      ((Behaviour) this).enabled = true;
      string trophy_progs;
      string bingo_progs;
      MonoSingleton<GameManager>.Instance.ServerSyncTrophyExecStart(out trophy_progs, out bingo_progs);
      this.ExecRequest((WebAPI) new ReqMixConceptCardAwakeItemData(this.mBaseCardId, this.mMaterials, new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback), trophy_progs, bingo_progs));
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
        WebAPI.JSON_BodyResponse<FlowNode_ReqConceptCardAwakeItemMix.Json_ConceptCardAwakeItemMix> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<FlowNode_ReqConceptCardAwakeItemMix.Json_ConceptCardAwakeItemMix>>(www.text);
        DebugUtility.Assert(jsonObject != null, "res == null");
        Network.RemoveAPI();
        long iid = -1;
        int beforeLevel = -1;
        int beforeAwakeCount = -1;
        int beforeTrust = -1;
        int num1 = -1;
        int num2 = -1;
        if (jsonObject.body.concept_card != null)
        {
          iid = jsonObject.body.concept_card.iid;
          ConceptCardData conceptCardByUniqueId = MonoSingleton<GameManager>.Instance.Player.FindConceptCardByUniqueID(iid);
          if (conceptCardByUniqueId != null)
          {
            beforeLevel = (int) conceptCardByUniqueId.Lv;
            beforeAwakeCount = (int) conceptCardByUniqueId.AwakeCount;
            beforeTrust = (int) conceptCardByUniqueId.Trust;
            num1 = conceptCardByUniqueId.TrustBonus;
            num2 = conceptCardByUniqueId.AwakeLevelCap;
          }
        }
        try
        {
          MonoSingleton<GameManager>.Instance.Player.Deserialize(jsonObject.body.concept_card);
          MonoSingleton<GameManager>.Instance.Player.Deserialize(jsonObject.body.items);
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
        conceptCardByUniqueId1.SetBonusCount(conceptCardByUniqueId1.TrustBonus - num1);
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) ConceptCardManager.Instance, (UnityEngine.Object) null) && UnityEngine.Object.op_Inequality((UnityEngine.Object) ConceptCardManager.Instance.ConceptCardDetail, (UnityEngine.Object) null) && UnityEngine.Object.op_Inequality((UnityEngine.Object) ConceptCardManager.Instance.ConceptCardDetail.Description, (UnityEngine.Object) null))
        {
          int addAwakeLv = ((int) conceptCardByUniqueId1.AwakeCount - beforeAwakeCount) * (int) MonoSingleton<GameManager>.Instance.MasterParam.FixParam.CardAwakeUnlockLevelCap;
          ConceptCardManager.Instance.ConceptCardDetail.Description.SetParam((ConceptCardDetailBase) ConceptCardManager.Instance.ConceptCardDetail.Description.Level, conceptCardByUniqueId1, 0, 0, addAwakeLv);
          ConceptCardManager.Instance.ConceptCardDetail.Description.SetParam((ConceptCardDetailBase) ConceptCardManager.Instance.ConceptCardDetail.Description.Status, conceptCardByUniqueId1, 0, 0, addAwakeLv);
        }
        this.ActivateOutputLinks(1000);
        ((Behaviour) this).enabled = false;
      }
    }

    public class ItemData
    {
      private long iid;
      private int iname;
    }

    public class Json_ConceptCardAwakeItemMix
    {
      public JSON_ConceptCard concept_card;
      public Json_Item[] items;
    }
  }
}
