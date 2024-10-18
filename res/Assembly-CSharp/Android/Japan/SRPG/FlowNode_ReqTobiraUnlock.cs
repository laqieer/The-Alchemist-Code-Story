// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_ReqTobiraUnlock
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;
using System;

namespace SRPG
{
  [FlowNode.NodeType("System/Unit/ReqTobira/Unlock")]
  [FlowNode.Pin(0, "扉機能を解放する", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(100, "扉機能を解放した", FlowNode.PinTypes.Output, 100)]
  public class FlowNode_ReqTobiraUnlock : FlowNode_Network
  {
    public const int INPUT_REQUEST = 0;
    public const int OUTPUT_REQUEST = 100;

    public override void OnActivate(int pinID)
    {
      if (pinID != 0)
        return;
      this.enabled = true;
      this.ExecRequest((WebAPI) new ReqTobiraUnlock((long) GlobalVars.SelectedUnitUniqueID, new Network.ResponseCallback(this.TobiraUnlockCallback)));
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
          MonoSingleton<GameManager>.Instance.Deserialize(jsonObject.body.items);
        }
        catch (Exception ex)
        {
          DebugUtility.LogException(ex);
          return;
        }
        this.enabled = false;
      }
    }

    private void TobiraUnlockCallback(WWWResult www)
    {
      if (FlowNode_Network.HasCommonError(www))
        return;
      this.OnSuccess(www);
      this.ActivateOutputLinks(100);
    }
  }
}
