// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_ReqGachaRate
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using UnityEngine;

#nullable disable
namespace SRPG
{
  [FlowNode.NodeType("Gacha/RequestGachaRate", 32741)]
  [FlowNode.Pin(100, "指定の召喚提供割合取得", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(110, "Success", FlowNode.PinTypes.Output, 10)]
  [FlowNode.Pin(120, "Failed", FlowNode.PinTypes.Output, 11)]
  public class FlowNode_ReqGachaRate : FlowNode_Network
  {
    public const int PIN_IN_REQUEST = 100;
    public const int PIN_OT_REQUEST_SUCCESS = 110;
    public const int PIN_OT_REQUEST_FAILED = 120;

    public override void OnActivate(int pinID)
    {
      if (pinID != 100)
        return;
      if (FlowNode_ButtonEvent.currentValue is SerializeValueList currentValue)
      {
        string gachaid = currentValue.GetString("select_gachaid");
        if (string.IsNullOrEmpty(gachaid))
          return;
        this.ExecRequest((WebAPI) new ReqGachaRate(gachaid, new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback)));
        ((Behaviour) this).enabled = true;
      }
      else
        this.Failure();
    }

    private void Success()
    {
      ((Behaviour) this).enabled = false;
      this.ActivateOutputLinks(110);
    }

    private void Failure()
    {
      ((Behaviour) this).enabled = false;
      this.ActivateOutputLinks(120);
    }

    public override void OnSuccess(WWWResult www)
    {
      if (Network.IsError)
      {
        if (Network.ErrCode == Network.EErrCode.NoGacha)
          this.OnFailed();
        else
          this.OnRetry();
      }
      else
      {
        WebAPI.JSON_BodyResponse<Json_GachaRateParam> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<Json_GachaRateParam>>(www.text);
        DebugUtility.Assert(jsonObject != null, "res == null");
        Network.RemoveAPI();
        if (jsonObject.body == null)
        {
          this.Failure();
        }
        else
        {
          if (jsonObject.body != null)
            GachaInfoRateWindow.instance?.DeserializeRateList(jsonObject.body);
          this.Success();
        }
      }
    }
  }
}
