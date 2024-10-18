// Decompiled with JetBrains decompiler
// Type: SRPG.ReqFriendFavoriteAdd
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

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
