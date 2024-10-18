// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_ReqTobiraEnhance
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;
using System;

namespace SRPG
{
  [FlowNode.NodeType("System/Unit/ReqTobira/ReqTobiraEnhance", 32741)]
  [FlowNode.Pin(0, "扉を強化する", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(100, "扉を強化した", FlowNode.PinTypes.Output, 100)]
  public class FlowNode_ReqTobiraEnhance : FlowNode_Network
  {
    private const int INPUT_REQUEST = 0;
    private const int OUTPUT_REQUEST = 100;

    public override void OnActivate(int pinID)
    {
      if (pinID != 0)
        return;
      this.enabled = true;
      this.ExecRequest((WebAPI) new ReqTobiraEnhance((long) GlobalVars.SelectedUnitUniqueID, (TobiraParam.Category) GlobalVars.PreBattleUnitTobiraCategory, new Network.ResponseCallback(this.EnhanceTobiraCallback)));
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
        GameParameter.UpdateValuesOfType(GameParameter.ParameterTypes.GLOBAL_PLAYER_GOLD);
        this.enabled = false;
      }
    }

    private void EnhanceTobiraCallback(WWWResult www)
    {
      if (FlowNode_Network.HasCommonError(www))
        return;
      this.OnSuccess(www);
      this.ActivateOutputLinks(100);
    }
  }
}
