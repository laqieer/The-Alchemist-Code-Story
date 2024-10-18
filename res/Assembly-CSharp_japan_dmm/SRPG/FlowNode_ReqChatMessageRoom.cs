// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_ReqChatMessageRoom
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using UnityEngine;

#nullable disable
namespace SRPG
{
  [FlowNode.NodeType("System/ReqChat/ReqChatLogRoom", 32741)]
  public class FlowNode_ReqChatMessageRoom : FlowNode_ReqChatMessage
  {
    private string mRoomToken;
    private long mStartID;
    private int mLimit;
    private long mExcludeID;
    private bool mIsSystemMessageMerge;

    private void ResetParam()
    {
      this.mRoomToken = (string) null;
      this.mStartID = 0L;
      this.mLimit = 0;
      this.mExcludeID = 0L;
      this.mIsSystemMessageMerge = false;
      this.mSetup = false;
    }

    public override void SetChatMessageInfo(
      string room_token,
      long start_id,
      int limit,
      long exclude_id,
      bool is_sys_msg_merge)
    {
      this.ResetParam();
      this.mRoomToken = room_token;
      this.mStartID = start_id;
      this.mLimit = limit;
      this.mExcludeID = exclude_id;
      this.mIsSystemMessageMerge = is_sys_msg_merge;
      this.mSetup = true;
    }

    public override void OnActivate(int pinID)
    {
      ((Behaviour) this).enabled = true;
      if (!this.mSetup || pinID != 0)
        return;
      Network.IsIndicator = false;
      bool isMultiPush = false;
      if (Object.op_Inequality((Object) MonoSingleton<GameManager>.Instance, (Object) null) && MonoSingleton<GameManager>.Instance.Player != null)
        isMultiPush = MonoSingleton<GameManager>.Instance.Player.MultiInvitaionFlag;
      this.ExecRequest((WebAPI) new ReqChatMessageRoom(this.mStartID, this.mRoomToken, this.mLimit, this.mExcludeID, isMultiPush, new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback)));
    }

    protected override void Success(ChatLog log)
    {
      ChatWindow component = ((Component) this).gameObject.GetComponent<ChatWindow>();
      if (Object.op_Inequality((Object) component, (Object) null))
      {
        if (this.mIsSystemMessageMerge)
          component.SetChatLogAndSystemMessageMerge(log, this.mExcludeID);
        else
          component.SetChatLog(log, ChatWindow.eChatType.Room);
        component.SetChatLog(log, ChatWindow.eChatType.Official);
      }
      base.Success(log);
    }
  }
}
