// Decompiled with JetBrains decompiler
// Type: SRPG.JSON_ReqTowerResuponse
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

namespace SRPG
{
  public class JSON_ReqTowerResuponse
  {
    public long rtime;
    public JSON_ReqTowerResuponse.Json_TowerStatus stats;
    public JSON_ReqTowerResuponse.Json_TowerPlayerUnit[] pdeck;
    public JSON_ReqTowerResuponse.Json_TowerEnemyUnit[] edeck;
    public RandDeckResult[] lot_enemies;
    public short reset_cost;
    public byte round;
    public byte is_reset;
    public int turn_num;
    public int died_num;
    public int retire_num;
    public int recover_num;
    public JSON_ReqTowerResuponse.Json_RankStatus rank;

    public class Json_TowerStatus
    {
      public string fname;
      public string state;

      public QuestStates questStates
      {
        get
        {
          switch (this.state)
          {
            case "win":
              return QuestStates.Cleared;
            case "lose":
            case "ritire":
            case "cancel":
              return QuestStates.Challenged;
            default:
              return QuestStates.New;
          }
        }
        set
        {
          switch (value)
          {
            case QuestStates.New:
            case QuestStates.Challenged:
              this.state = "ritire";
              break;
            case QuestStates.Cleared:
              this.state = "win";
              break;
          }
        }
      }
    }

    public class Json_TowerProg
    {
      public string iname;
      public int is_open;
    }

    public class Json_TowerPlayerUnit
    {
      public string uname;
      public int damage;
      public int is_died;
    }

    public class Json_TowerEnemyUnit
    {
      public int eid;
      public int hp;
      public int jewel;
    }

    public class Json_RankStatus
    {
      public int turn_num;
      public int died_num;
      public int retire_num;
      public int recovery_num;
      public int spd_rank;
      public int tec_rank;
      public int spd_score;
      public int tec_score;
      public int ret_score;
      public int rcv_score;
      public int challenge_num;
      public int lose_num;
      public int reset_num;
      public int challenge_score;
      public int lose_score;
      public int reset_score;
    }

    public class Json_UserCoin
    {
      public int free;
      public int paid;
      public int com;
    }
  }
}
