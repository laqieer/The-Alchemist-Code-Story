// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_ReqInspSkillReset
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System;
using UnityEngine;

#nullable disable
namespace SRPG
{
  [FlowNode.NodeType("InspSkill/Req/Reset", 32741)]
  [FlowNode.Pin(1, "Request", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(101, "Success", FlowNode.PinTypes.Output, 101)]
  [FlowNode.Pin(201, "コスト不足(GOLD)", FlowNode.PinTypes.Output, 201)]
  [FlowNode.Pin(202, "コスト不足(COIN)", FlowNode.PinTypes.Output, 202)]
  [FlowNode.Pin(203, "コスト不足(COIN_P)", FlowNode.PinTypes.Output, 203)]
  public class FlowNode_ReqInspSkillReset : FlowNode_Network
  {
    protected const int PIN_INPUT_REQUEST = 1;
    protected const int PIN_OUTPUT_SUCCESS = 101;
    protected const int PIN_OUTPUT_COSTSHORT_GOLD = 201;
    protected const int PIN_OUTPUT_COSTSHORT_COIN = 202;
    protected const int PIN_OUTPUT_COSTSHORT_COIN_P = 203;

    public override void OnActivate(int pinID)
    {
      if (pinID != 1)
        return;
      if (GlobalVars.TargetInspSkillArtifact == null)
        throw new Exception("Target artifact is not set.");
      if (GlobalVars.TargetInspSkill == null)
        throw new Exception("Target skill is not set.");
      ((Behaviour) this).enabled = true;
      this.ExecRequest((WebAPI) new ReqInspSkillReset((long) GlobalVars.TargetInspSkillArtifact.UniqueID, (long) GlobalVars.TargetInspSkill.UniqueID, new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback)));
    }

    private void Success()
    {
      ((Behaviour) this).enabled = false;
      this.ActivateOutputLinks(101);
    }

    private void CostShort()
    {
      ((Behaviour) this).enabled = false;
      Network.ResetError();
      Network.RemoveAPI();
      InspSkillCostParam resetCost = GlobalVars.TargetInspSkill.InspSkillParam.GetResetCost();
      if (resetCost == null)
        return;
      int pinID = -1;
      if (resetCost.Type == InspSkillCostType.GOLD)
        pinID = 201;
      else if (resetCost.Type == InspSkillCostType.COIN)
        pinID = 202;
      else if (resetCost.Type == InspSkillCostType.COIN)
        pinID = 203;
      this.ActivateOutputLinks(pinID);
    }

    public override void OnSuccess(WWWResult www)
    {
      if (Network.IsError)
      {
        switch (Network.ErrCode)
        {
          case Network.EErrCode.InspSkill_IncorrectParam:
          case Network.EErrCode.InspSkill_NotExistArtifact:
          case Network.EErrCode.InspSkill_NotExistInspirationSkill:
          case Network.EErrCode.InspSkill_CannotResetNotFound:
            this.OnBack();
            break;
          case Network.EErrCode.InspSkill_CostShort:
            this.CostShort();
            break;
          default:
            this.OnRetry();
            break;
        }
      }
      else
      {
        WebAPI.JSON_BodyResponse<ReqInspSkillReset.Response> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<ReqInspSkillReset.Response>>(www.text);
        DebugUtility.Assert(jsonObject != null, "res == null");
        if (jsonObject.body == null)
        {
          this.OnRetry();
        }
        else
        {
          try
          {
            MonoSingleton<GameManager>.Instance.Deserialize(jsonObject.body.player);
            ArtifactData artifactByUniqueId = MonoSingleton<GameManager>.Instance.Player.FindArtifactByUniqueID((long) GlobalVars.TargetInspSkillArtifact.UniqueID);
            if (jsonObject.body.inspiration_skill != null)
              artifactByUniqueId.DeserializeInspSkill(jsonObject.body.inspiration_skill);
            else
              artifactByUniqueId.RemoveInspSkill((long) GlobalVars.TargetInspSkill.UniqueID);
            MonoSingleton<GameManager>.Instance.Player.UpdateArtifactOwner();
          }
          catch (Exception ex)
          {
            DebugUtility.LogException(ex);
            this.OnRetry();
            return;
          }
          Network.RemoveAPI();
          this.Success();
        }
      }
    }
  }
}
