// Decompiled with JetBrains decompiler
// Type: SRPG.ReqFriendReq
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

namespace SRPG
{
  public class ReqFriendReq : WebAPI
  {
    public ReqFriendReq(string fuid, Network.ResponseCallback response)
    {
      this.name = "friend/req";
      this.body = WebAPI.GetRequestString("\"fuid\":\"" + fuid + "\"");
      this.callback = response;
    }
  }
}
