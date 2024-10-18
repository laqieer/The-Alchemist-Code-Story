// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_ReqChatMessage
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;

namespace SRPG
{
  [FlowNode.NodeType("System/ReqChat/ReqChatLog", 32741)]
  [FlowNode.Pin(0, "Request", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(10, "Success", FlowNode.PinTypes.Output, 10)]
  [FlowNode.Pin(100, "ChatFailure", FlowNode.PinTypes.Output, 100)]
  public class FlowNode_ReqChatMessage : FlowNode_Network
  {
    protected bool mSetup;

    public virtual void SetChatMessageInfo(int channel, long start_id, int limit, long exclude_id)
    {
    }

    public virtual void SetChatMessageInfo(string room_token, long start_id, int limit, long exclude_id, bool is_sys_msg_merge)
    {
    }

    public virtual void SetChatMessageInfo(long start_id, int limit, long exclude_id)
    {
    }

    public override void OnActivate(int pinID)
    {
    }

    protected virtual void Success(ChatLog log)
    {
      this.enabled = false;
      this.mSetup = false;
      this.ActivateOutputLinks(10);
    }

    public override void OnSuccess(WWWResult www)
    {
      if ((UnityEngine.Object) this == (UnityEngine.Object) null)
      {
        Network.RemoveAPI();
        Network.IsIndicator = true;
      }
      else if (Network.IsError)
      {
        switch (Network.ErrCode)
        {
          case Network.EErrCode.Guild_NotJoined_First:
          case Network.EErrCode.Guild_NotJoined:
            Network.RemoveAPI();
            Network.IsIndicator = true;
            Network.ResetError();
            this.enabled = false;
            this.mSetup = false;
            this.Success((ChatLog) null);
            break;
          default:
            Network.RemoveAPI();
            Network.IsIndicator = true;
            Network.ResetError();
            this.enabled = false;
            this.mSetup = false;
            this.ActivateOutputLinks(100);
            break;
        }
      }
      else
      {
        DebugMenu.Log("API", "chat:message:{" + www.text + "}");
        WebAPI.JSON_BodyResponse<JSON_ChatLog> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<JSON_ChatLog>>(www.text);
        DebugUtility.Assert(jsonObject != null, "res == null");
        Network.RemoveAPI();
        Network.IsIndicator = true;
        ChatLog log = new ChatLog();
        if (jsonObject.body != null)
        {
          log.Deserialize(jsonObject.body);
          MultiInvitationReceiveWindow.SetBadge(jsonObject.body.player != null && jsonObject.body.player.multi_inv != 0);
        }
        else
          MultiInvitationReceiveWindow.SetBadge(false);
        this.Success(log);
      }
    }
  }
}
