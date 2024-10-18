// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_ReqArtifactSell
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;
using System;

namespace SRPG
{
  [FlowNode.NodeType("System/ReqArtifact/ReqArtifactSell", 32741)]
  [FlowNode.Pin(0, "Request", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(1, "Success", FlowNode.PinTypes.Output, 1)]
  public class FlowNode_ReqArtifactSell : FlowNode_Network
  {
    public override void OnActivate(int pinID)
    {
      if (pinID != 0)
        return;
      if (Network.Mode == Network.EConnectMode.Online)
      {
        long[] array = GlobalVars.SellArtifactsList.ToArray();
        if (array.Length < 1)
        {
          this.enabled = false;
          this.Success();
        }
        else
        {
          this.enabled = true;
          this.ExecRequest((WebAPI) new ReqArtifactSell(array, new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback)));
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
        WebAPI.JSON_BodyResponse<ReqArtifactSell.Response> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<ReqArtifactSell.Response>>(www.text);
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
            MonoSingleton<GameManager>.Instance.Deserialize(jsonObject.body.units);
            if (jsonObject.body.sells != null)
            {
              for (int index = 0; index < jsonObject.body.sells.Length; ++index)
                MonoSingleton<GameManager>.Instance.Player.RemoveArtifactByUniqueID(jsonObject.body.sells[index]);
            }
            else if (jsonObject.body.artifacts != null)
              MonoSingleton<GameManager>.Instance.Deserialize(jsonObject.body.artifacts, false);
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
