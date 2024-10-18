// Decompiled with JetBrains decompiler
// Type: SRPG.ReqVersusLobby
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

#nullable disable
namespace SRPG
{
  public class ReqVersusLobby : WebAPI
  {
    public ReqVersusLobby(Network.ResponseCallback response)
    {
      this.name = "vs/lobby";
      this.body = WebAPI.GetRequestString(string.Empty);
      this.callback = response;
    }

    public class Response
    {
      public int rankmatch_schedule_id;
      public int rankmatch_ranking_status;
      public ReqRankMatchStatus.EnableTimeSchedule rankmatch_enabletime;
      public int draft_schedule_id;
      public int draft_type;

      public ReqRankMatchStatus.RankingStatus RankMatchRankingStatus
      {
        get => (ReqRankMatchStatus.RankingStatus) this.rankmatch_ranking_status;
      }
    }
  }
}
