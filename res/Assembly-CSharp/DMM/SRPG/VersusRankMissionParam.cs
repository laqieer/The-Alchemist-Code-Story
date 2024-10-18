// Decompiled with JetBrains decompiler
// Type: SRPG.VersusRankMissionParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

#nullable disable
namespace SRPG
{
  public class VersusRankMissionParam
  {
    private string mIName;
    private string mName;
    private string mExpire;
    private RankMatchMissionType mType;
    private string mSVal;
    private int mIVal;
    private string mRewardId;

    public string IName => this.mIName;

    public string Name => this.mName;

    public string Expire => this.mExpire;

    public RankMatchMissionType Type => this.mType;

    public string SVal => this.mSVal;

    public int IVal => this.mIVal;

    public string RewardId => this.mRewardId;

    public bool Deserialize(JSON_VersusRankMissionParam json)
    {
      if (json == null)
        return false;
      this.mIName = json.iname;
      this.mName = json.name;
      this.mExpire = json.expr;
      this.mType = (RankMatchMissionType) json.type;
      this.mSVal = json.sval;
      this.mIVal = json.ival;
      this.mRewardId = json.reward_id;
      return true;
    }
  }
}
