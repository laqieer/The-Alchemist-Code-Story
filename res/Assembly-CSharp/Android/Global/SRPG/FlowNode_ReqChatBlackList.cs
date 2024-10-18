// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_ReqChatBlackList
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using GR;

namespace SRPG
{
  [FlowNode.NodeType("System/ReqChatBlackList", 32741)]
  [FlowNode.Pin(2, "Maintenance", FlowNode.PinTypes.Output, 2)]
  [FlowNode.Pin(1, "Success", FlowNode.PinTypes.Output, 1)]
  [FlowNode.Pin(0, "Request", FlowNode.PinTypes.Input, 0)]
  public class FlowNode_ReqChatBlackList : FlowNode_Network
  {
    public int GetLimit = 10;

    public override void OnActivate(int pinID)
    {
      if (pinID != 0)
        return;
      string s = FlowNode_Variable.Get("BLACKLIST_OFFSET");
      this.ExecRequest((WebAPI) new ReqChatBlackList(!string.IsNullOrEmpty(s) ? int.Parse(s) : 1, this.GetLimit, new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback)));
      this.enabled = false;
    }

    private void Success()
    {
      this.enabled = false;
      this.ActivateOutputLinks(1);
    }

    private void ChatMaintenance()
    {
      this.enabled = false;
      BlackList component = this.gameObject.GetComponent<BlackList>();
      if ((UnityEngine.Object) component != (UnityEngine.Object) null)
        component.RefreshMaintenanceMessage(Network.ErrMsg);
      Network.RemoveAPI();
      Network.ResetError();
      this.ActivateOutputLinks(2);
    }

    public override void OnSuccess(WWWResult www)
    {
      if (Network.IsError)
      {
        if (Network.ErrCode != Network.EErrCode.ChatMaintenance)
          return;
        this.ChatMaintenance();
      }
      else
      {
        WebAPI.JSON_BodyResponse<JSON_ChatBlackList> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<JSON_ChatBlackList>>(www.text);
        DebugUtility.Assert(jsonObject != null, "res == null");
        Network.RemoveAPI();
        ChatBlackList chatBlackList = new ChatBlackList();
        chatBlackList.Deserialize(jsonObject.body);
        if (chatBlackList == null)
          return;
        BlackList componentInChildren = this.GetComponentInChildren<BlackList>();
        if ((UnityEngine.Object) componentInChildren != (UnityEngine.Object) null)
          componentInChildren.BList = chatBlackList;
        this.Success();
      }
    }
  }
}
