// Decompiled with JetBrains decompiler
// Type: SRPG.ReqRaidBtlResume
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;

#nullable disable
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
