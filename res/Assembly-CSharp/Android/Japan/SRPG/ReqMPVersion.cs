﻿// Decompiled with JetBrains decompiler
// Type: SRPG.ReqMPVersion
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

namespace SRPG
{
  public class ReqMPVersion : WebAPI
  {
    public ReqMPVersion(Network.ResponseCallback response)
    {
      this.name = "btl/multi/check";
      this.body = string.Empty;
      this.body = WebAPI.GetRequestString(this.body);
      this.callback = response;
    }

    public class Response
    {
      public string device_id;
    }
  }
}