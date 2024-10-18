// Decompiled with JetBrains decompiler
// Type: SRPG.GuildAttendRewardDetail
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

#nullable disable
namespace SRPG
{
  public class GuildAttendRewardDetail
  {
    private int mMemberCount;
    private string mRewardID;

    public int member_cnt => this.mMemberCount;

    public string reward_id => this.mRewardID;

    public bool Deserialize(JSON_GuildAttendRewardDetail json)
    {
      if (json == null)
        return false;
      this.mMemberCount = json.member_cnt;
      this.mRewardID = json.reward_id;
      return true;
    }
  }
}
