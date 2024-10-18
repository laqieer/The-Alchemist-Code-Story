﻿// Decompiled with JetBrains decompiler
// Type: SRPG.ReqMultiRank
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

namespace SRPG
{
  public class ReqMultiRank : WebAPI
  {
    public ReqMultiRank(string iname, Network.ResponseCallback response)
    {
      this.name = "btl/usedunit";
      this.body = "\"iname\":\"" + JsonEscape.Escape(iname) + "\"";
      this.body = WebAPI.GetRequestString(this.body);
      this.callback = response;
    }

    public class Json_MultiRankParam
    {
      public string unit_iname;
      public string job_iname;
    }

    public class Json_MultiRank
    {
      public ReqMultiRank.Json_MultiRankParam[] ranking;
      public int isReady;
    }
  }
}
