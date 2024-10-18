// Decompiled with JetBrains decompiler
// Type: SRPG.ReqRaidBtlResume
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using System;

namespace SRPG
{
  public class ReqRaidBtlResume : WebAPI
  {
    public ReqRaidBtlResume(long btlid, Network.ResponseCallback response)
    {
      this.name = "raidboss/btl/resume";
      this.body = WebAPI.GetRequestString<ReqRaidBtlResume.RequestParam>(new ReqRaidBtlResume.RequestParam()
      {
        btlid = btlid
      });
      this.callback = response;
    }

    [Serializable]
    public class RequestParam
    {
      public long btlid;
    }
  }
}
