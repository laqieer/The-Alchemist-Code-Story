// Decompiled with JetBrains decompiler
// Type: SRPG.ReqMultiTwStatus
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;

#nullable disable
namespace SRPG
{
  public class ReqMultiTwStatus : WebAPI
  {
    public ReqMultiTwStatus(string tower_id, Network.ResponseCallback response)
    {
      this.name = "btl/multi/tower/status";
      this.body = string.Empty;
      ReqMultiTwStatus reqMultiTwStatus = this;
      reqMultiTwStatus.body = reqMultiTwStatus.body + "\"tower_id\":\"" + JsonEscape.Escape(tower_id) + "\"";
      this.body = WebAPI.GetRequestString(this.body);
      this.callback = response;
    }

    [Serializable]
    public class FloorParam
    {
      public int floor;
      public int clear_count;
    }

    [Serializable]
    public class Response
    {
      public ReqMultiTwStatus.FloorParam[] floors;
      public string appid;
      public int max_clear_floor;
      public string btl_ver;
    }
  }
}
