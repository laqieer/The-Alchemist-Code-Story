// Decompiled with JetBrains decompiler
// Type: SRPG.ReqGenesisBossBtlResume
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;

#nullable disable
namespace SRPG
{
  public class ReqGenesisBossBtlResume : WebAPI
  {
    public ReqGenesisBossBtlResume(long btlid, Network.ResponseCallback response)
    {
      this.name = "genesis/raidboss/btl/resume";
      this.body = WebAPI.GetRequestString<ReqGenesisBossBtlResume.RequestParam>(new ReqGenesisBossBtlResume.RequestParam()
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
