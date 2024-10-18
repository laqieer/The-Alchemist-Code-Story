// Decompiled with JetBrains decompiler
// Type: SRPG.ReqRankMatchStatus
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

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
        get
        {
          return (ReqRankMatchStatus.RankingStatus) this.ranking_status;
        }
      }
    }
  }
}
