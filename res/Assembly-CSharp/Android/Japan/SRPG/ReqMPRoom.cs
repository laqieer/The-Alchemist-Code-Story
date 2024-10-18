// Decompiled with JetBrains decompiler
// Type: SRPG.ReqMPRoom
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

namespace SRPG
{
  public class ReqMPRoom : WebAPI
  {
    public ReqMPRoom(string fuid, string iname, Network.ResponseCallback response)
    {
      this.name = "btl/room";
      this.body = string.Empty;
      if (!string.IsNullOrEmpty(fuid))
      {
        ReqMPRoom reqMpRoom = this;
        reqMpRoom.body = reqMpRoom.body + "\"fuid\":\"" + JsonEscape.Escape(fuid) + "\"";
      }
      if (!string.IsNullOrEmpty(iname))
      {
        if (!string.IsNullOrEmpty(this.body))
          this.body += ",";
        ReqMPRoom reqMpRoom = this;
        reqMpRoom.body = reqMpRoom.body + "\"iname\":\"" + JsonEscape.Escape(iname) + "\"";
      }
      this.body = WebAPI.GetRequestString(this.body);
      this.callback = response;
    }

    public class Response
    {
      public MultiPlayAPIRoom[] rooms;
    }
  }
}
