// Decompiled with JetBrains decompiler
// Type: SRPG.ReqVersusLobby
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

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
        get
        {
          return (ReqRankMatchStatus.RankingStatus) this.rankmatch_ranking_status;
        }
      }
    }
  }
}
