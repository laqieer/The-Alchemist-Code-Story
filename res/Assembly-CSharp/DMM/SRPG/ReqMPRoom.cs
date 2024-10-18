// Decompiled with JetBrains decompiler
// Type: SRPG.ReqMPRoom
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

#nullable disable
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
