// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_UpdateAchievements
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using GR;

namespace SRPG
{
  [FlowNode.Pin(0, "Request", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(11, "Failure", FlowNode.PinTypes.Output, 11)]
  [FlowNode.Pin(10, "Success", FlowNode.PinTypes.Output, 10)]
  [FlowNode.NodeType("System/UpdateAchievements", 32741)]
  public class FlowNode_UpdateAchievements : FlowNode_Network
  {
    public override void OnActivate(int pinID)
    {
      if (pinID != 0)
        return;
      this.ExecRequest((WebAPI) new ReqAwardList(new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback)));
      this.enabled = true;
    }

    private void Success()
    {
      this.enabled = false;
      this.ActivateOutputLinks(10);
    }

    private void Failure()
    {
      this.enabled = false;
      this.ActivateOutputLinks(11);
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
        WebAPI.JSON_BodyResponse<Json_ResAwardList> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<Json_ResAwardList>>(www.text);
        DebugUtility.Assert(jsonObject != null, "res == null");
        Network.RemoveAPI();
        if (jsonObject.body == null)
        {
          this.Failure();
        }
        else
        {
          string[] awards = jsonObject.body.awards;
          if (awards != null)
          {
            for (int index = 0; index < awards.Length; ++index)
            {
              if (!string.IsNullOrEmpty(awards[index]))
                GameCenterManager.SendAchievementProgress(awards[index]);
            }
            this.Success();
          }
          else
            this.Failure();
        }
      }
    }
  }
}
