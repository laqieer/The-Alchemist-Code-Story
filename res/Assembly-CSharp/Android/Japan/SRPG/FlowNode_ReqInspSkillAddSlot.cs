// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_ReqInspSkillAddSlot
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;
using System;

namespace SRPG
{
  [FlowNode.NodeType("InspSkill/Req/AddSlot", 32741)]
  [FlowNode.Pin(1, "Request", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(101, "Success", FlowNode.PinTypes.Output, 101)]
  [FlowNode.Pin(201, "コスト不足(GOLD)", FlowNode.PinTypes.Output, 201)]
  [FlowNode.Pin(202, "コスト不足(COIN)", FlowNode.PinTypes.Output, 202)]
  [FlowNode.Pin(203, "コスト不足(COIN_P)", FlowNode.PinTypes.Output, 203)]
  public class FlowNode_ReqInspSkillAddSlot : FlowNode_Network
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
      this.enabled = true;
      this.ExecRequest((WebAPI) new ReqInspSkillAddSlot((long) GlobalVars.TargetInspSkillArtifact.UniqueID, new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback)));
    }

    private void Success()
    {
      this.enabled = false;
      this.ActivateOutputLinks(101);
    }

    private void CostShort()
    {
      this.enabled = false;
      Network.ResetError();
      Network.RemoveAPI();
      InspSkillCostParam inspSkillOpenCost = MonoSingleton<GameManager>.Instance.MasterParam.GetInspSkillOpenCost(GlobalVars.TargetInspSkillArtifact.GetNextOpenInspSlot());
      if (inspSkillOpenCost == null)
        return;
      int pinID = -1;
      if (inspSkillOpenCost.Type == InspSkillCostType.GOLD)
        pinID = 201;
      else if (inspSkillOpenCost.Type == InspSkillCostType.COIN)
        pinID = 202;
      else if (inspSkillOpenCost.Type == InspSkillCostType.COIN_P)
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
          case Network.EErrCode.InspSkill_CannotAddSlotArtifact:
          case Network.EErrCode.InspSkill_CannotAddSlotNumMax:
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
        WebAPI.JSON_BodyResponse<ReqInspSkillAddSlot.Response> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<ReqInspSkillAddSlot.Response>>(www.text);
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
            MonoSingleton<GameManager>.Instance.Player.Deserialize(new Json_Artifact[1]
            {
              jsonObject.body.artifact
            }, true);
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
