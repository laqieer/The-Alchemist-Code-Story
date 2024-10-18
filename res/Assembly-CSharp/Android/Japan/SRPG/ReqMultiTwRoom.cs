// Decompiled with JetBrains decompiler
// Type: SRPG.ReqMultiTwRoom
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

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
