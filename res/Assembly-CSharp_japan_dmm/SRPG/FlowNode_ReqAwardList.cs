// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_ReqAwardList
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using UnityEngine;

#nullable disable
namespace SRPG
{
  [FlowNode.NodeType("System/Award/ReqAwardList", 32741)]
  [FlowNode.Pin(0, "所持称号取得", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(10, "Success", FlowNode.PinTypes.Output, 10)]
  [FlowNode.Pin(11, "Failure", FlowNode.PinTypes.Output, 11)]
  public class FlowNode_ReqAwardList : FlowNode_Network
  {
    [FlowNode.ShowInInfo]
    [FlowNode.DropTarget(typeof (GameObject), true)]
    public GameObject Target;
    public FlowNode_ReqAwardList.MODE mMode;

    public override void OnActivate(int pinID)
    {
      if (pinID != 0)
        return;
      this.ExecRequest((WebAPI) new ReqAwardList(new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback)));
      ((Behaviour) this).enabled = true;
    }

    private void Success()
    {
      ((Behaviour) this).enabled = false;
      this.ActivateOutputLinks(10);
    }

    private void Failure()
    {
      ((Behaviour) this).enabled = false;
      this.ActivateOutputLinks(11);
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
        WebAPI.JSON_BodyResponse<Json_ResAwardList> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<Json_ResAwardList>>(www.text);
        DebugUtility.Assert(jsonObject != null, "res == null");
        Network.RemoveAPI();
        if (jsonObject.body == null)
        {
          this.Failure();
        }
        else
        {
          if (this.mMode == FlowNode_ReqAwardList.MODE.SetAwardList)
          {
            if (Object.op_Equality((Object) this.Target, (Object) null) || Object.op_Equality((Object) this.Target.GetComponent<AwardList>(), (Object) null))
            {
              this.Failure();
              return;
            }
            this.Target.GetComponent<AwardList>().SetOpenAwards(jsonObject.body.awards);
          }
          else if (this.mMode == FlowNode_ReqAwardList.MODE.SetPlayerAward)
            MonoSingleton<GameManager>.Instance.Player.SetHaveAward(jsonObject.body.awards);
          this.Success();
        }
      }
    }

    public enum MODE
    {
      SetAwardList,
      SetPlayerAward,
    }
  }
}
