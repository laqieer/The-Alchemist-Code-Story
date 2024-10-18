// Decompiled with JetBrains decompiler
// Type: SRPG.ReqFriendList
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

namespace SRPG
{
  public class ReqFriendList : WebAPI
  {
    public ReqFriendList(bool is_follow, Network.ResponseCallback response)
    {
      this.name = "friend";
      this.body = WebAPI.GetRequestString((string) null);
      if (is_follow)
        this.body = WebAPI.GetRequestString("\"is_follower\":" + (object) 1);
      this.callback = response;
    }
  }
}
