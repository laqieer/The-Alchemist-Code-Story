// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_ReqInspSkillLvUp
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System;
using System.Linq;
using UnityEngine;

#nullable disable
namespace SRPG
{
  [FlowNode.NodeType("InspSkill/Req/LevelUp", 32741)]
  [FlowNode.Pin(1, "Request", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(101, "Success", FlowNode.PinTypes.Output, 101)]
  [FlowNode.Pin(201, "コスト不足", FlowNode.PinTypes.Output, 201)]
  public class FlowNode_ReqInspSkillLvUp : FlowNode_Network
  {
    protected const int PIN_INPUT_REQUEST = 1;
    protected const int PIN_OUTPUT_SUCCESS = 101;
    protected const int PIN_OUTPUT_COSTSHORT = 201;

    public override void OnActivate(int pinID)
    {
      if (pinID != 1)
        return;
      if (GlobalVars.TargetInspSkillArtifact == null)
        throw new Exception("Target artifact is not set.");
      if (GlobalVars.TargetInspSkill == null)
        throw new Exception("Target skill is not set.");
      if (GlobalVars.MixInspSkillArtifactList == null || GlobalVars.MixInspSkillArtifactList.Count <= 0)
        throw new Exception("Target skill is not set.");
      ((Behaviour) this).enabled = true;
      this.ExecRequest((WebAPI) new ReqInspSkillLvUp((long) GlobalVars.TargetInspSkillArtifact.UniqueID, (long) GlobalVars.TargetInspSkill.UniqueID, GlobalVars.MixInspSkillArtifactList.Select<ArtifactData, long>((Func<ArtifactData, long>) (art => (long) art.UniqueID)).ToArray<long>(), new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback)));
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
      this.ActivateOutputLinks(201);
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
          case Network.EErrCode.InspSkill_ArtifactFavorite:
          case Network.EErrCode.InspSkill_IncorrectMixArtifact:
          case Network.EErrCode.InspSkill_CannotLevelUpLevelMax:
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
        WebAPI.JSON_BodyResponse<ReqInspSkillLvUp.Response> res = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<ReqInspSkillLvUp.Response>>(www.text);
        DebugUtility.Assert(res != null, "res == null");
        if (res.body == null)
        {
          this.OnRetry();
        }
        else
        {
          try
          {
            MonoSingleton<GameManager>.Instance.Deserialize(res.body.player);
            ArtifactData artifactByUniqueId = MonoSingleton<GameManager>.Instance.Player.FindArtifactByUniqueID((long) GlobalVars.TargetInspSkillArtifact.UniqueID);
            if (artifactByUniqueId != null)
            {
              artifactByUniqueId.DeserializeInspSkill(res.body.inspiration_skill);
              if (res.body.mix_iids != null && res.body.mix_iids.Length > 0)
                MonoSingleton<GameManager>.Instance.Player.Artifacts.RemoveAll((Predicate<ArtifactData>) (part => Array.FindIndex<long>(res.body.mix_iids, (Predicate<long>) (iid => iid == (long) part.UniqueID)) >= 0));
            }
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
