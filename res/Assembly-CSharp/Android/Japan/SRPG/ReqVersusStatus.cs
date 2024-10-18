﻿// Decompiled with JetBrains decompiler
// Type: SRPG.ReqVersusStatus
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

namespace SRPG
{
  public class ReqVersusStatus : WebAPI
  {
    public ReqVersusStatus(Network.ResponseCallback response)
    {
      this.name = "vs/status";
      this.body = string.Empty;
      this.body = WebAPI.GetRequestString(this.body);
      this.callback = response;
    }

    public class StreakStatus
    {
      public int schedule_id;
      public int num;
      public int best;
    }

    public class EnableTimeSchedule
    {
      public long expired;
      public long next;
      public int draft_type;
      public long schedule_id;
      public long draft_id;
      public string iname;
    }

    public class Response
    {
      public int floor;
      public int key;
      public int wincnt;
      public int is_season_gift;
      public int is_give_season_gift;
      public long end_at;
      public string vstower_id;
      public string tower_iname;
      public string last_enemyuid;
      public int daycnt;
      public ReqVersusStatus.StreakStatus[] streakwins;
      public int is_firstwin;
      public ReqVersusStatus.EnableTimeSchedule enabletime;
    }
  }
}
