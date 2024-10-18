// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_ReqInspSkillEquip
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System;
using UnityEngine;

#nullable disable
namespace SRPG
{
  [FlowNode.NodeType("InspSkill/Req/Equip", 32741)]
  [FlowNode.Pin(1, "Request", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(101, "Success", FlowNode.PinTypes.Output, 101)]
  public class FlowNode_ReqInspSkillEquip : FlowNode_Network
  {
    protected const int PIN_INPUT_REQUEST = 1;
    protected const int PIN_OUTPUT_SUCCESS = 101;

    public override void OnActivate(int pinID)
    {
      if (pinID != 1)
        return;
      if (GlobalVars.TargetInspSkillArtifact == null)
        throw new Exception("Target artifact is not set.");
      if (GlobalVars.TargetInspSkill == null)
        throw new Exception("Target skill is not set.");
      ((Behaviour) this).enabled = true;
      this.ExecRequest((WebAPI) new ReqInspSkillEquip((long) GlobalVars.TargetInspSkillArtifact.UniqueID, (long) GlobalVars.TargetInspSkill.UniqueID, new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback)));
    }

    private void Success()
    {
      ((Behaviour) this).enabled = false;
      this.ActivateOutputLinks(101);
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
            this.OnBack();
            break;
          default:
            this.OnRetry();
            break;
        }
      }
      else
      {
        WebAPI.JSON_BodyResponse<ReqInspSkillEquip.Response> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<ReqInspSkillEquip.Response>>(www.text);
        DebugUtility.Assert(jsonObject != null, "res == null");
        if (jsonObject.body == null)
        {
          this.OnRetry();
        }
        else
        {
          try
          {
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
