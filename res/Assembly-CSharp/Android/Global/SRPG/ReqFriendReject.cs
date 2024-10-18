// Decompiled with JetBrains decompiler
// Type: SRPG.ReqFriendReject
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

namespace SRPG
{
  public class ReqFriendReject : WebAPI
  {
    public ReqFriendReject(string[] fuids, Network.ResponseCallback response)
    {
      this.name = "friend/reject";
      this.body = "\"fuids\":[";
      for (int index = 0; index < fuids.Length; ++index)
      {
        ReqFriendReject reqFriendReject = this;
        reqFriendReject.body = reqFriendReject.body + "\"" + fuids[index] + "\"";
        if (index != fuids.Length - 1)
          this.body += ",";
      }
      this.body += "]";
      this.body = WebAPI.GetRequestString(this.body);
      this.callback = response;
    }
  }
}
