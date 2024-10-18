// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_ReqChatMessageGuild
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;

#nullable disable
namespace SRPG
{
  [FlowNode.NodeType("System/ReqChat/ReqChatLogGuild", 32741)]
  public class FlowNode_ReqChatMessageGuild : FlowNode_ReqChatMessage
  {
    private long mStartID;
    private int mLimit;
    private long mExcludeID;

    private void ResetParam()
    {
      this.mStartID = 0L;
      this.mLimit = 0;
      this.mExcludeID = 0L;
      this.mSetup = false;
    }

    public override void SetChatMessageInfo(long start_id, int limit, long exclude_id)
    {
      this.ResetParam();
      this.mStartID = start_id;
      this.mLimit = limit;
      this.mExcludeID = exclude_id;
      this.mSetup = true;
    }

    public override void OnActivate(int pinID)
    {
      ((Behaviour) this).enabled = true;
      if (!this.mSetup || pinID != 0)
        return;
      Network.IsIndicator = false;
      this.ExecRequest((WebAPI) new ReqChatMessageGuild(this.mStartID, this.mLimit, this.mExcludeID, new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback)));
    }

    protected override void Success(ChatLog log)
    {
      ChatWindow component = ((Component) this).gameObject.GetComponent<ChatWindow>();
      if (Object.op_Inequality((Object) component, (Object) null))
      {
        component.SetChatLog(log, ChatWindow.eChatType.Guild);
        component.SetChatLog(log, ChatWindow.eChatType.Official);
      }
      base.Success(log);
    }
  }
}
