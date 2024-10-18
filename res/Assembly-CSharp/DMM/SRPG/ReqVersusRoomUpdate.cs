// Decompiled with JetBrains decompiler
// Type: SRPG.ReqVersusRoomUpdate
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

#nullable disable
namespace SRPG
{
  public class ReqVersusRoomUpdate : WebAPI
  {
    public ReqVersusRoomUpdate(
      int roomID,
      string comment,
      string iname,
      Network.ResponseCallback response)
    {
      this.name = "vs/friendmatch/update";
      this.body = string.Empty;
      ReqVersusRoomUpdate versusRoomUpdate1 = this;
      versusRoomUpdate1.body = versusRoomUpdate1.body + "\"roomid\":" + (object) roomID;
      ReqVersusRoomUpdate versusRoomUpdate2 = this;
      versusRoomUpdate2.body = versusRoomUpdate2.body + ",\"comment\":\"" + JsonEscape.Escape(comment) + "\"";
      ReqVersusRoomUpdate versusRoomUpdate3 = this;
      versusRoomUpdate3.body = versusRoomUpdate3.body + ",\"quest\":\"" + iname + "\"";
      this.body = WebAPI.GetRequestString(this.body);
      this.callback = response;
    }
  }
}
