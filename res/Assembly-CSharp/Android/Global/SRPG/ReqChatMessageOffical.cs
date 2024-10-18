// Decompiled with JetBrains decompiler
// Type: SRPG.ReqChatMessageOffical
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

namespace SRPG
{
  public class ReqChatMessageOffical : WebAPI
  {
    public ReqChatMessageOffical(Network.ResponseCallback response)
    {
      this.name = "chat/message/offical";
      this.body = WebAPI.GetRequestString((string) null);
      this.callback = response;
    }
  }
}
