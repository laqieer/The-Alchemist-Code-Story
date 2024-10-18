// Decompiled with JetBrains decompiler
// Type: SRPG.ReqFriendList
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

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
