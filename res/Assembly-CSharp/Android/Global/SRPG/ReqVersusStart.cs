﻿// Decompiled with JetBrains decompiler
// Type: SRPG.ReqVersusStart
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

namespace SRPG
{
  public class ReqVersusStart : WebAPI
  {
    public ReqVersusStart(VERSUS_TYPE type, Network.ResponseCallback response)
    {
      this.name = "vs/start";
      this.body = string.Empty;
      ReqVersusStart reqVersusStart = this;
      reqVersusStart.body = reqVersusStart.body + "\"type\":\"" + type.ToString().ToLower() + "\"";
      this.body = WebAPI.GetRequestString(this.body);
      this.callback = response;
    }

    public class JSON_VersusMap
    {
      public string free;
      public string tower;
      public string friend;
    }

    public class Response
    {
      public string app_id;
      public ReqVersusStart.JSON_VersusMap maps;
    }
  }
}
