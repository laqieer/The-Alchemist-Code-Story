// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_ReqChatBlackList
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;

namespace SRPG
{
  [FlowNode.NodeType("Request/ReqBlackList(ブロックリスト取得)", 32741)]
  [FlowNode.Pin(0, "Request", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(1, "Success", FlowNode.PinTypes.Output, 1)]
  [FlowNode.Pin(2, "Maintenance", FlowNode.PinTypes.Output, 2)]
  public class FlowNode_ReqChatBlackList : FlowNode_Network
  {
    public int GetLimit = 10;
    public bool IsGetOnly;

    public override void OnActivate(int pinID)
    {
      if (pinID != 0)
        return;
      string s = FlowNode_Variable.Get("BLACKLIST_OFFSET");
      this.ExecRequest((WebAPI) new ReqChatBlackList(!string.IsNullOrEmpty(s) ? int.Parse(s) : 1, this.GetLimit, new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback)));
      if ((UnityEngine.Object) this == (UnityEngine.Object) null)
        return;
      this.enabled = false;
    }

    private void Success()
    {
      if ((UnityEngine.Object) this == (UnityEngine.Object) null)
        return;
      this.enabled = false;
      this.ActivateOutputLinks(1);
    }

    private void ChatMaintenance()
    {
      if ((UnityEngine.Object) this == (UnityEngine.Object) null)
        return;
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
        GlobalVars.BlockList.Clear();
        if (this.IsGetOnly)
        {
          foreach (ChatBlackListParam list in chatBlackList.lists)
            GlobalVars.BlockList.Add(list.uid);
        }
        else
        {
          BlackList component = this.gameObject.GetComponent<BlackList>();
          if ((UnityEngine.Object) component != (UnityEngine.Object) null)
            component.BList = chatBlackList;
        }
        this.Success();
      }
    }
  }
}
