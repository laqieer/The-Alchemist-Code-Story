// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_ReqConceptCardSet
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;
using System;

namespace SRPG
{
  [FlowNode.NodeType("ConceptCard/Req/ReqConceptCardSet", 32741)]
  [FlowNode.Pin(0, "装備", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(10, "外す", FlowNode.PinTypes.Input, 10)]
  [FlowNode.Pin(100, "装備した", FlowNode.PinTypes.Output, 100)]
  [FlowNode.Pin(110, "外した", FlowNode.PinTypes.Output, 110)]
  public class FlowNode_ReqConceptCardSet : FlowNode_Network
  {
    private const int INPUT_CONCEPT_CARD_SET = 0;
    private const int INPUT_CONCEPT_CARD_UNSET = 10;
    private const int OUTPUT_CONCEPT_CARD_SET = 100;
    private const int OUTPUT_CONCEPT_CARD_UNSET = 110;
    private long mEquipCardId;
    private long mTargetUnitId;

    public void SetEquipParam(long equip_card_iid, long unit_iid)
    {
      this.ResetParam();
      this.mEquipCardId = equip_card_iid;
      this.mTargetUnitId = unit_iid;
    }

    public void SetReleaseParam(long equip_card_iid)
    {
      this.ResetParam();
      this.mEquipCardId = equip_card_iid;
    }

    private void ResetParam()
    {
      this.mEquipCardId = 0L;
      this.mTargetUnitId = 0L;
    }

    public override void OnActivate(int pinID)
    {
      if (pinID == 0)
      {
        this.enabled = true;
        this.ExecRequest((WebAPI) ReqSetConceptCard.CreateSet(this.mEquipCardId, this.mTargetUnitId, new Network.ResponseCallback(this.ConceptCardSetResponseCallback)));
      }
      else
      {
        if (pinID != 10)
          return;
        long card_iid = 0;
        this.enabled = true;
        this.ExecRequest((WebAPI) ReqSetConceptCard.CreateUnset(card_iid, new Network.ResponseCallback(this.ConceptCardUnsetResponseCallback)));
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
        WebAPI.JSON_BodyResponse<Json_PlayerDataAll> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<Json_PlayerDataAll>>(www.text);
        DebugUtility.Assert(jsonObject != null, "res == null");
        Network.RemoveAPI();
        try
        {
          MonoSingleton<GameManager>.Instance.Deserialize(jsonObject.body.player);
          MonoSingleton<GameManager>.Instance.Deserialize(jsonObject.body.units);
          if ((UnityEngine.Object) UnitEnhanceV3.Instance != (UnityEngine.Object) null)
            UnitEnhanceV3.Instance.OnEquipConceptCardSelect();
        }
        catch (Exception ex)
        {
          DebugUtility.LogException(ex);
          return;
        }
        this.enabled = false;
      }
    }

    private void ConceptCardSetResponseCallback(WWWResult www)
    {
      if (FlowNode_Network.HasCommonError(www))
        return;
      this.OnSuccess(www);
      this.ActivateOutputLinks(100);
    }

    private void ConceptCardUnsetResponseCallback(WWWResult www)
    {
      if (FlowNode_Network.HasCommonError(www))
        return;
      this.OnSuccess(www);
      this.ActivateOutputLinks(110);
    }
  }
}
