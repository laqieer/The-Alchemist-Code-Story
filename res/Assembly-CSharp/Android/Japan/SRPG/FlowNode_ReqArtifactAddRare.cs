﻿// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_ReqArtifactAddRare
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;
using System;

namespace SRPG
{
  [FlowNode.NodeType("System/ReqArtifact/ReqArtifactAddRare", 32741)]
  [FlowNode.Pin(0, "Request", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(1, "Success", FlowNode.PinTypes.Output, 1)]
  public class FlowNode_ReqArtifactAddRare : FlowNode_Network
  {
    public override void OnActivate(int pinID)
    {
      if (pinID != 0)
        return;
      if (Network.Mode == Network.EConnectMode.Online)
      {
        long artifactUniqueId = (long) GlobalVars.SelectedArtifactUniqueID;
        if (artifactUniqueId < 1L)
        {
          this.enabled = false;
          this.Success();
        }
        else
        {
          this.enabled = true;
          this.ExecRequest((WebAPI) new ReqArtifactAddRare(artifactUniqueId, new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback), (string) null, (string) null));
        }
      }
      else
        this.Success();
    }

    private void Success()
    {
      this.enabled = false;
      this.ActivateOutputLinks(1);
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
        if (jsonObject.body == null)
        {
          this.OnRetry();
        }
        else
        {
          try
          {
            MonoSingleton<GameManager>.Instance.Deserialize(jsonObject.body.player);
            MonoSingleton<GameManager>.Instance.Deserialize(jsonObject.body.items);
            if (jsonObject.body.artifacts != null)
              MonoSingleton<GameManager>.Instance.Deserialize(jsonObject.body.artifacts, true);
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
