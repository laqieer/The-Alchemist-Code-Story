// Decompiled with JetBrains decompiler
// Type: SRPG.ReqGuildRaidBtlResume
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;

#nullable disable
namespace SRPG
{
  public class ReqGuildRaidBtlResume : WebAPI
  {
    public ReqGuildRaidBtlResume(int gid, long btlid, Network.ResponseCallback response)
    {
      this.name = "guildraid/btl/resume";
      this.body = WebAPI.GetRequestString<ReqGuildRaidBtlResume.RequestParam>(new ReqGuildRaidBtlResume.RequestParam()
      {
        gid = gid,
        btlid = btlid
      });
      this.callback = response;
    }

    [Serializable]
    public class RequestParam
    {
      public int gid;
      public long btlid;
    }
  }
}
