// Decompiled with JetBrains decompiler
// Type: SRPG.RaidGuildMemberData
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

namespace SRPG
{
  public class RaidGuildMemberData : GuildMemberData
  {
    private int mBeatScore;
    private int mRescueScore;
    private int mRound;

    public int BeatScore
    {
      get
      {
        return this.mBeatScore;
      }
    }

    public int RescueScore
    {
      get
      {
        return this.mRescueScore;
      }
    }

    public int Round
    {
      get
      {
        return this.mRound;
      }
    }

    public bool Deserialize(JSON_RaidGuildMember json)
    {
      json.units = json.unit;
      json.award_id = json.selected_award;
      this.Deserialize((JSON_GuildMember) json);
      this.mBeatScore = json.beat_score;
      this.mRescueScore = json.rescue_score;
      this.mRound = json.round;
      return true;
    }
  }
}
