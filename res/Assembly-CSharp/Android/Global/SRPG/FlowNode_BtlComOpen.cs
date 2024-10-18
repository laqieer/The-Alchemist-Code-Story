// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_BtlComOpen
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using GR;
using System;

namespace SRPG
{
  [FlowNode.Pin(1, "Request", FlowNode.PinTypes.Input, 0)]
  [FlowNode.NodeType("System/BtlComOpen", 32741)]
  [FlowNode.Pin(100, "Success", FlowNode.PinTypes.Output, 10)]
  [FlowNode.Pin(101, "Failed", FlowNode.PinTypes.Output, 11)]
  public class FlowNode_BtlComOpen : FlowNode_Network
  {
    public override void OnActivate(int pinID)
    {
      if (pinID != 1)
        return;
      this.enabled = false;
      string selectedChapter = (string) GlobalVars.SelectedChapter;
      if (Network.Mode == Network.EConnectMode.Offline)
      {
        this.Success();
      }
      else
      {
        this.ExecRequest((WebAPI) new ReqBtlComOpen(selectedChapter, new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback)));
        this.enabled = true;
      }
    }

    private void Success()
    {
      this.enabled = false;
      this.ActivateOutputLinks(100);
    }

    private void Failure()
    {
      this.enabled = false;
      this.ActivateOutputLinks(101);
    }

    public override void OnSuccess(WWWResult www)
    {
      if (Network.IsError)
      {
        Network.EErrCode errCode = Network.ErrCode;
        this.OnRetry();
      }
      else
      {
        WebAPI.JSON_BodyResponse<FlowNode_BtlComOpen.JSON_BtlComOpenResponse> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<FlowNode_BtlComOpen.JSON_BtlComOpenResponse>>(www.text);
        DebugUtility.Assert(jsonObject != null, "res == null");
        Network.RemoveAPI();
        GameManager instance = MonoSingleton<GameManager>.Instance;
        try
        {
          instance.Deserialize(jsonObject.body.items);
          instance.Deserialize(jsonObject.body.quests);
        }
        catch (Exception ex)
        {
          this.Failure();
          return;
        }
        this.Success();
      }
    }

    public class JSON_BtlComOpenResponse
    {
      public Json_Item[] items;
      public JSON_QuestProgress[] quests;
    }
  }
}
