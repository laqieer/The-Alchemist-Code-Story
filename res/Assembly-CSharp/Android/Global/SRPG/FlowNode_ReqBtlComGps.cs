// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_ReqBtlComGps
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using GR;

namespace SRPG
{
  [FlowNode.NodeType("System/ReqBtlComGps", 32741)]
  [FlowNode.Pin(2, "Reset to Title", FlowNode.PinTypes.Output, 11)]
  [FlowNode.Pin(3, "エリアクエスト無し", FlowNode.PinTypes.Output, 12)]
  [FlowNode.Pin(1, "Success", FlowNode.PinTypes.Output, 10)]
  [FlowNode.Pin(100, "Start", FlowNode.PinTypes.Input, 0)]
  public class FlowNode_ReqBtlComGps : FlowNode_Network
  {
    public override void OnActivate(int pinID)
    {
      if (pinID != 100)
        return;
      if (Network.Mode == Network.EConnectMode.Online)
      {
        this.ExecRequest((WebAPI) new ReqBtlComGps(new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback), GlobalVars.Location));
        this.enabled = true;
      }
      else
        this.SuccessNotQuest();
    }

    private void Success()
    {
      this.enabled = false;
      this.ActivateOutputLinks(1);
    }

    private void SuccessNotQuest()
    {
      this.enabled = false;
      this.ActivateOutputLinks(3);
    }

    private void Failure()
    {
      this.enabled = false;
      this.ActivateOutputLinks(2);
    }

    public override void OnSuccess(WWWResult www)
    {
      if (Network.IsError)
      {
        if (Network.ErrCode == Network.EErrCode.NotGpsQuest)
        {
          Network.RemoveAPI();
          Network.ResetError();
          this.SuccessNotQuest();
        }
        else
          this.OnRetry();
      }
      else
      {
        WebAPI.JSON_BodyResponse<FlowNode_ReqBtlComGps.JSON_ReqBtlComGpsResponse> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<FlowNode_ReqBtlComGps.JSON_ReqBtlComGpsResponse>>(www.text);
        DebugUtility.Assert(jsonObject != null, "res == null");
        Network.RemoveAPI();
        GameManager instance = MonoSingleton<GameManager>.Instance;
        instance.ResetGpsQuests();
        if (jsonObject.body.quests == null || jsonObject.body.quests.Length == 0)
          this.SuccessNotQuest();
        else if (!instance.DeserializeGps(jsonObject.body.quests))
          this.Failure();
        else
          this.Success();
      }
    }

    public class JSON_ReqBtlComGpsResponse
    {
      public JSON_QuestProgress[] quests;
    }
  }
}
