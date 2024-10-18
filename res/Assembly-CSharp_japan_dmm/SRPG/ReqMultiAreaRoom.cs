// Decompiled with JetBrains decompiler
// Type: SRPG.ReqMultiAreaRoom
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;

#nullable disable
namespace SRPG
{
  public class ReqMultiAreaRoom : WebAPI
  {
    public ReqMultiAreaRoom(
      string fuid,
      string[] iname,
      Vector2 location,
      Network.ResponseCallback response)
    {
      this.name = "btl/room/areaquest";
      this.body = string.Empty;
      if (!string.IsNullOrEmpty(fuid))
      {
        ReqMultiAreaRoom reqMultiAreaRoom = this;
        reqMultiAreaRoom.body = reqMultiAreaRoom.body + "\"fuid\":\"" + JsonEscape.Escape(fuid) + "\"";
      }
      if (iname != null && iname.Length > 0)
      {
        if (!string.IsNullOrEmpty(this.body))
          this.body += ",";
        this.body += "\"iname\":[";
        for (int index = 0; index < iname.Length; ++index)
        {
          if (index != 0)
            this.body += ",";
          ReqMultiAreaRoom reqMultiAreaRoom = this;
          reqMultiAreaRoom.body = reqMultiAreaRoom.body + "\"" + JsonEscape.Escape(iname[index]) + "\"";
        }
        this.body += "]";
      }
      if (!string.IsNullOrEmpty(this.body))
        this.body += ",";
      this.body += "\"location\":{";
      ReqMultiAreaRoom reqMultiAreaRoom1 = this;
      reqMultiAreaRoom1.body = reqMultiAreaRoom1.body + "\"lat\":" + (object) location.x + ",";
      ReqMultiAreaRoom reqMultiAreaRoom2 = this;
      reqMultiAreaRoom2.body = reqMultiAreaRoom2.body + "\"lng\":" + (object) location.y + "}";
      this.body = WebAPI.GetRequestString(this.body);
      this.callback = response;
    }

    public class Response
    {
      public MultiPlayAPIRoom[] rooms;
    }
  }
}
