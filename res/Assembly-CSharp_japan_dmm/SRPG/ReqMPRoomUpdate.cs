// Decompiled with JetBrains decompiler
// Type: SRPG.ReqMPRoomUpdate
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

#nullable disable
namespace SRPG
{
  public class ReqMPRoomUpdate : WebAPI
  {
    public ReqMPRoomUpdate(
      int roomID,
      string comment,
      string passCode,
      int btl_speed,
      int enable_auto,
      Network.ResponseCallback response)
    {
      this.name = "btl/room/update";
      this.body = string.Empty;
      ReqMPRoomUpdate reqMpRoomUpdate1 = this;
      reqMpRoomUpdate1.body = reqMpRoomUpdate1.body + "\"roomid\":" + (object) roomID;
      ReqMPRoomUpdate reqMpRoomUpdate2 = this;
      reqMpRoomUpdate2.body = reqMpRoomUpdate2.body + ",\"comment\":\"" + JsonEscape.Escape(comment) + "\"";
      ReqMPRoomUpdate reqMpRoomUpdate3 = this;
      reqMpRoomUpdate3.body = reqMpRoomUpdate3.body + ",\"pwd\":\"" + JsonEscape.Escape(passCode) + "\"";
      ReqMPRoomUpdate reqMpRoomUpdate4 = this;
      reqMpRoomUpdate4.body = reqMpRoomUpdate4.body + ",\"btl_speed\":" + (object) btl_speed;
      ReqMPRoomUpdate reqMpRoomUpdate5 = this;
      reqMpRoomUpdate5.body = reqMpRoomUpdate5.body + ",\"enable_auto\":" + (object) enable_auto;
      this.body = WebAPI.GetRequestString(this.body);
      this.callback = response;
    }
  }
}
