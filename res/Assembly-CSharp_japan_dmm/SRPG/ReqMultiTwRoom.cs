// Decompiled with JetBrains decompiler
// Type: SRPG.ReqMultiTwRoom
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

#nullable disable
namespace SRPG
{
  public class ReqMultiTwRoom : WebAPI
  {
    public ReqMultiTwRoom(string fuid, string iname, int floor, Network.ResponseCallback response)
    {
      this.name = "btl/multi/tower/room";
      this.body = string.Empty;
      ReqMultiTwRoom reqMultiTwRoom = this;
      reqMultiTwRoom.body = reqMultiTwRoom.body + "\"iname\":\"" + JsonEscape.Escape(iname) + "\"";
      this.body = WebAPI.GetRequestString(this.body);
      this.callback = response;
    }

    public class Response
    {
      public MultiPlayAPIRoom[] rooms;
    }
  }
}
