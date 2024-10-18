// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_ReqChatMessageWorld
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using GR;

namespace SRPG
{
  [FlowNode.NodeType("System/ReqChatLogWorld", 32741)]
  public class FlowNode_ReqChatMessageWorld : FlowNode_ReqChatMessage
  {
    private int mChannel;
    private long mStartID;
    private int mLimit;
    private long mExcludeID;

    private void ResetParam()
    {
      this.mChannel = 0;
      this.mStartID = 0L;
      this.mLimit = 0;
      this.mExcludeID = 0L;
      this.mSetup = false;
    }

    public override void SetChatMessageInfo(int channel, long start_id, int limit, long exclude_id)
    {
      this.ResetParam();
      this.mChannel = channel;
      this.mStartID = start_id;
      this.mLimit = limit;
      this.mExcludeID = exclude_id;
      this.mSetup = true;
    }

    public override void OnActivate(int pinID)
    {
      this.enabled = true;
      if (!this.mSetup || pinID != 0)
        return;
      Network.IsIndicator = false;
      bool isMultiPush = false;
      if ((UnityEngine.Object) MonoSingleton<GameManager>.Instance != (UnityEngine.Object) null && MonoSingleton<GameManager>.Instance.Player != null)
        isMultiPush = MonoSingleton<GameManager>.Instance.Player.MultiInvitaionFlag;
      this.ExecRequest((WebAPI) new ReqChatMessage(this.mStartID, this.mChannel, this.mLimit, this.mExcludeID, isMultiPush, new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback)));
    }

    protected override void Success(ChatLog log)
    {
      ChatWindow component = this.gameObject.GetComponent<ChatWindow>();
      if ((UnityEngine.Object) component != (UnityEngine.Object) null)
        component.SetChatLog(log, ChatWindow.eChatType.World);
      base.Success(log);
    }
  }
}
