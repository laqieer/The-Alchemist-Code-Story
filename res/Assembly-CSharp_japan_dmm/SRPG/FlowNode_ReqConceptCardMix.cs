// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_ReqConceptCardMix
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System;
using UnityEngine;

#nullable disable
namespace SRPG
{
  [FlowNode.NodeType("ConceptCard/Req/ReqConceptCardMix", 32741)]
  [FlowNode.Pin(10, "合成開始", FlowNode.PinTypes.Input, 10)]
  [FlowNode.Pin(1000, "合成完了", FlowNode.PinTypes.Output, 1000)]
  public class FlowNode_ReqConceptCardMix : FlowNode_Network
  {
    private const int INPUT_CONCEPT_CARD_MIX_START = 10;
    private const int OUTPUT_CONCEPT_CARD_MIX_END = 1000;
    private long mBaseCardId;
    private long[] mMixCardIds;
    private int totalMixZeny;
    private int trustBonusMix;

    public void SetMixParam()
    {
      ConceptCardManager instance = ConceptCardManager.Instance;
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) instance, (UnityEngine.Object) null))
        return;
      ConceptCardManager.GalcTotalMixZeny(instance.SelectedMaterials, out this.totalMixZeny);
      instance.SetupLevelupAnimation();
      this.mBaseCardId = (long) instance.SelectedConceptCardData.UniqueID;
      this.mMixCardIds = instance.SelectedMaterials.GetUniqueIDs().ToArray();
      this.trustBonusMix = ConceptCardManager.CalcTotalTrustBonusMixCount(instance.SelectedConceptCardData, instance.SelectedMaterials);
      if (this.trustBonusMix > instance.SelectedConceptCardData.AwakeCountCap + 1)
        this.trustBonusMix = instance.SelectedConceptCardData.AwakeCountCap + 1;
      instance.SelectedMaterials.Clear();
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
      this.ExecRequest((WebAPI) new ReqMixConceptCard(this.mBaseCardId, this.mMixCardIds, new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback), trophy_progs, bingo_progs));
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
        WebAPI.JSON_BodyResponse<FlowNode_ReqConceptCardMix.Json_ConceptCardMix> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<FlowNode_ReqConceptCardMix.Json_ConceptCardMix>>(www.text);
        DebugUtility.Assert(jsonObject != null, "res == null");
        Network.RemoveAPI();
        long iid = -1;
        int beforeLevel = -1;
        int beforeAwakeCount = -1;
        int beforeTrust = -1;
        if (jsonObject.body.concept_card != null)
        {
          iid = jsonObject.body.concept_card.iid;
          ConceptCardData conceptCardByUniqueId = MonoSingleton<GameManager>.Instance.Player.FindConceptCardByUniqueID(iid);
          if (conceptCardByUniqueId != null)
          {
            beforeLevel = (int) conceptCardByUniqueId.Lv;
            beforeAwakeCount = (int) conceptCardByUniqueId.AwakeCount;
            beforeTrust = (int) conceptCardByUniqueId.Trust;
          }
        }
        try
        {
          MonoSingleton<GameManager>.Instance.Deserialize(jsonObject.body.player);
          MonoSingleton<GameManager>.Instance.Player.Deserialize(jsonObject.body.concept_card);
          MonoSingleton<GameManager>.Instance.Player.RemoveConceptCardData(jsonObject.body.mix_ids);
        }
        catch (Exception ex)
        {
          DebugUtility.LogException(ex);
          return;
        }
        MonoSingleton<GameManager>.Instance.Player.OnGoldChange(this.totalMixZeny);
        MonoSingleton<GameManager>.Instance.ServerSyncTrophyExecEnd(www);
        ConceptCardData conceptCardByUniqueId1 = MonoSingleton<GameManager>.Instance.Player.FindConceptCardByUniqueID(iid);
        MonoSingleton<GameManager>.Instance.Player.OnMixedConceptCard(conceptCardByUniqueId1.Param.iname, beforeLevel, (int) conceptCardByUniqueId1.Lv, beforeAwakeCount, (int) conceptCardByUniqueId1.AwakeCount, beforeTrust, (int) conceptCardByUniqueId1.Trust);
        conceptCardByUniqueId1.SetBonusCount(conceptCardByUniqueId1.TrustBonus - this.trustBonusMix);
        this.ActivateOutputLinks(1000);
        ((Behaviour) this).enabled = false;
      }
    }

    public class Json_ConceptCardMix
    {
      public Json_PlayerData player;
      public JSON_ConceptCard concept_card;
      public long[] mix_ids;
    }
  }
}
