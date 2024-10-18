// Decompiled with JetBrains decompiler
// Type: SRPG.ReqGuildRaidReportSelfShare
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using Gsc.Network.Encoding;
using System;

#nullable disable
namespace SRPG
{
  public class ReqGuildRaidReportSelfShare : WebAPI
  {
    public ReqGuildRaidReportSelfShare(
      int gid,
      int report_id,
      SRPG.Network.ResponseCallback response,
      EncodingTypes.ESerializeCompressMethod serializeCompressMethod)
    {
      this.name = "guildraid/report/self/share";
      this.body = WebAPI.GetRequestString<ReqGuildRaidReportSelfShare.RequestParam>(new ReqGuildRaidReportSelfShare.RequestParam()
      {
        gid = gid,
        report_id = report_id
      });
      this.callback = response;
      this.serializeCompressMethod = serializeCompressMethod;
    }

    [Serializable]
    public class RequestParam
    {
      public int gid;
      public int report_id;
    }
  }
}
