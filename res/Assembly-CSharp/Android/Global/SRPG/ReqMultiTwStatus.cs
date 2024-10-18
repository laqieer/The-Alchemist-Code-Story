// Decompiled with JetBrains decompiler
// Type: SRPG.ReqMultiTwStatus
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using System;

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
    }
  }
}
