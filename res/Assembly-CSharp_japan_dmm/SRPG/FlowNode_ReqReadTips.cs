// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_ReqReadTips
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System.Collections.Generic;

#nullable disable
namespace SRPG
{
  [FlowNode.NodeType("System/ReqReadTips", 32741)]
  [FlowNode.Pin(0, "TIPS既読", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(10, "成功", FlowNode.PinTypes.Output, 10)]
  public class FlowNode_ReqReadTips : FlowNode_Network
  {
    private const int PIN_ID_REQUEST = 0;
    private const int PIN_ID_SUCCESS = 10;

    public override void OnActivate(int pinID)
    {
      if (pinID != 0)
        return;
      string requestTips = GlobalVars.RequestTips;
      if (MonoSingleton<GameManager>.Instance.Tips.Contains(requestTips))
      {
        this.ActivateOutputLinks(10);
      }
      else
      {
        MonoSingleton<GameManager>.Instance.Player.OnReadTips(requestTips);
        string trophy_progs;
        string bingo_progs;
        MonoSingleton<GameManager>.Instance.ServerSyncTrophyExecStart(out trophy_progs, out bingo_progs);
        this.ExecRequest((WebAPI) new ReqReadTips(requestTips, trophy_progs, bingo_progs, new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback)));
      }
    }

    public override void OnSuccess(WWWResult www)
    {
      WebAPI.JSON_BodyResponse<Json_ReturnTips> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<Json_ReturnTips>>(www.text);
      if (jsonObject.body.tips != null)
      {
        List<string> tips = MonoSingleton<GameManager>.Instance.Tips;
        if (!tips.Contains(jsonObject.body.tips))
          tips.Add(jsonObject.body.tips);
      }
      MonoSingleton<GameManager>.Instance.ServerSyncTrophyExecEnd(www);
      GlobalVars.RequestTips = (string) null;
      this.ActivateOutputLinks(10);
      Network.RemoveAPI();
    }
  }
}
