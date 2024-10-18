// Decompiled with JetBrains decompiler
// Type: SRPG.RaidGuildMemberData
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

#nullable disable
namespace SRPG
{
  public class RaidGuildMemberData : GuildMemberData
  {
    private int mBeatScore;
    private int mRescueScore;
    private int mRound;

    public int BeatScore => this.mBeatScore;

    public int RescueScore => this.mRescueScore;

    public int Round => this.mRound;

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
