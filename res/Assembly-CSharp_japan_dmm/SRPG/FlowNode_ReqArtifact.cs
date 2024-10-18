// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_ReqArtifact
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System;
using UnityEngine;

#nullable disable
namespace SRPG
{
  [FlowNode.NodeType("System/ReqArtifact/ReqArtifact", 32741)]
  [FlowNode.Pin(0, "一覧取得", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(100, "一覧取得した", FlowNode.PinTypes.Output, 100)]
  public class FlowNode_ReqArtifact : FlowNode_Network
  {
    private const int PIN_IN_REQUEST = 0;
    private const int PIN_OUT_DONE = 100;
    private bool mIsDifferenceUpdate;

    public override void OnActivate(int pinID)
    {
      if (pinID != 0)
        return;
      if (GlobalVars.IsDirtyArtifactData.Get())
      {
        ((Behaviour) this).enabled = true;
        long last_artifact_iid = 0;
        this.mIsDifferenceUpdate = false;
        this.ExecRequest((WebAPI) new ReqArtifact(last_artifact_iid, new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback)));
      }
      else
        this.ActivateOutputLinks(100);
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
        WebAPI.JSON_BodyResponse<ReqArtifact.Response> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<ReqArtifact.Response>>(www.text);
        DebugUtility.Assert(jsonObject != null, "res == null");
        Network.RemoveAPI();
        try
        {
          if (jsonObject.body.artifacts != null)
            MonoSingleton<GameManager>.Instance.Deserialize(jsonObject.body.artifacts, this.mIsDifferenceUpdate);
          GlobalVars.IsDirtyArtifactData.Set(false);
          MonoSingleton<GameManager>.Instance.Player.UpdateArtifactOwner();
        }
        catch (Exception ex)
        {
          DebugUtility.LogException(ex);
          return;
        }
        this.ActivateOutputLinks(100);
        ((Behaviour) this).enabled = false;
      }
    }
  }
}
