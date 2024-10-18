// Decompiled with JetBrains decompiler
// Type: SRPG.ReqFriendFavoriteAdd
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

namespace SRPG
{
  public class ReqFriendFavoriteAdd : WebAPI
  {
    public ReqFriendFavoriteAdd(string fuid, Network.ResponseCallback response)
    {
      this.name = "friend/favorite/add";
      this.body = WebAPI.GetRequestString("\"fuid\":\"" + fuid + "\"");
      this.callback = response;
    }
  }
}
