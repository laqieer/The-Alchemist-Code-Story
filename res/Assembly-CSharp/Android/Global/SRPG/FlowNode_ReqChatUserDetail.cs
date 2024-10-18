// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_ReqChatUserDetail
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using GR;
using UnityEngine;

namespace SRPG
{
  [FlowNode.Pin(1, "Success", FlowNode.PinTypes.Output, 1)]
  [FlowNode.Pin(0, "Request", FlowNode.PinTypes.Input, 0)]
  [FlowNode.NodeType("System/ReqChatUserDetail", 32741)]
  public class FlowNode_ReqChatUserDetail : FlowNode_Network
  {
    [SerializeField]
    private ChatPlayerWindow window;
    [SerializeField]
    private FriendDetailWindow detail;

    public override void OnActivate(int pinID)
    {
      if (pinID != 0)
        return;
      this.ExecRequest((WebAPI) new ReqChatUserProfile(FlowNode_Variable.Get("SelectUserID"), new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback)));
      this.enabled = false;
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
        this.OnBack();
      }
      else
      {
        WebAPI.JSON_BodyResponse<JSON_ChatPlayerData> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<JSON_ChatPlayerData>>(www.text);
        DebugUtility.Assert(jsonObject != null, "res == null");
        Network.RemoveAPI();
        ChatPlayerData data = new ChatPlayerData();
        data.Deserialize(jsonObject.body);
        if ((UnityEngine.Object) this.window != (UnityEngine.Object) null)
          this.window.Player = data;
        if ((UnityEngine.Object) this.detail != (UnityEngine.Object) null)
          this.detail.SetChatPlayerData(data);
        this.Success();
      }
    }
  }
}
