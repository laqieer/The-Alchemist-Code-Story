// Decompiled with JetBrains decompiler
// Type: SRPG.ReqFriendApprove
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

namespace SRPG
{
  public class ReqFriendApprove : WebAPI
  {
    public ReqFriendApprove(string fuid, Network.ResponseCallback response)
    {
      fuid = WebAPI.EscapeString(fuid);
      this.name = "friend/approve";
      this.body = WebAPI.GetRequestString("\"fuid\":\"" + fuid + "\"");
      this.callback = response;
    }
  }
}
