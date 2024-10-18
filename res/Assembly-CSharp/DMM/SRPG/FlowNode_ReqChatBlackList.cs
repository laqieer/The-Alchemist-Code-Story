// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_ReqChatBlackList
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using UnityEngine;

#nullable disable
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
      if (Object.op_Equality((Object) this, (Object) null))
        return;
      ((Behaviour) this).enabled = false;
    }

    private void Success()
    {
      if (Object.op_Equality((Object) this, (Object) null))
        return;
      ((Behaviour) this).enabled = false;
      this.ActivateOutputLinks(1);
    }

    private void ChatMaintenance()
    {
      if (Object.op_Equality((Object) this, (Object) null))
        return;
      ((Behaviour) this).enabled = false;
      BlackList component = ((Component) this).gameObject.GetComponent<BlackList>();
      if (Object.op_Inequality((Object) component, (Object) null))
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
          BlackList component = ((Component) this).gameObject.GetComponent<BlackList>();
          if (Object.op_Inequality((Object) component, (Object) null))
            component.BList = chatBlackList;
        }
        this.Success();
      }
    }
  }
}
