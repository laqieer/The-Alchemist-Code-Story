// Decompiled with JetBrains decompiler
// Type: SRPG.ReqRankMatchStatus
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

#nullable disable
namespace SRPG
{
  public class ReqRankMatchStatus : WebAPI
  {
    public ReqRankMatchStatus(Network.ResponseCallback response)
    {
      this.name = "vs/rankmatch/status";
      this.body = WebAPI.GetRequestString(string.Empty);
      this.callback = response;
    }

    public enum RankingStatus
    {
      Normal,
      Aggregating,
      Rewarding,
    }

    public class EnableTimeSchedule
    {
      public long expired;
      public long next;
      public string iname;
    }

    public class Response
    {
      public int schedule_id;
      public int ranking_status;
      public ReqRankMatchStatus.EnableTimeSchedule enabletime;

      public ReqRankMatchStatus.RankingStatus RankingStatus
      {
        get => (ReqRankMatchStatus.RankingStatus) this.ranking_status;
      }
    }
  }
}
