// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_ReqRanking
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using UnityEngine;

#nullable disable
namespace SRPG
{
  [FlowNode.NodeType("System/ReqRanking", 32741)]
  [FlowNode.Pin(0, "Request", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(1, "Quest", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(2, "Arena", FlowNode.PinTypes.Input, 2)]
  [FlowNode.Pin(3, "TowerMuch", FlowNode.PinTypes.Input, 3)]
  [FlowNode.Pin(100, "Success", FlowNode.PinTypes.Output, 100)]
  public class FlowNode_ReqRanking : FlowNode_Network
  {
    public override void OnActivate(int pinID)
    {
      UsageRateRanking component = ((Component) this).gameObject.GetComponent<UsageRateRanking>();
      switch (pinID)
      {
        case 1:
          component.OnChangedToggle(UsageRateRanking.ViewInfoType.Quest);
          break;
        case 2:
          component.OnChangedToggle(UsageRateRanking.ViewInfoType.Arena);
          break;
        case 3:
          component.OnChangedToggle(UsageRateRanking.ViewInfoType.TowerMatch);
          break;
      }
      pinID = pinID <= 0 || pinID > 3 ? pinID : 0;
      if (pinID != 0)
        return;
      ((Behaviour) this).enabled = true;
      this.ExecRequest((WebAPI) new ReqRanking(UsageRateRanking.ViewInfo, new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback)));
    }

    public override void OnSuccess(WWWResult www)
    {
      WebAPI.JSON_BodyResponse<RankingData[]> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<RankingData[]>>(www.text);
      DebugUtility.Assert(jsonObject != null, "res == null");
      Network.RemoveAPI();
      MonoSingleton<GameManager>.Instance.Deserialize(jsonObject.body);
      this.ActivateOutputLinks(100);
    }
  }
}
