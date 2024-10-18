// Decompiled with JetBrains decompiler
// Type: SRPG.VersusRankReward
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

#nullable disable
namespace SRPG
{
  public class VersusRankReward
  {
    private RewardType mType;
    private string mIName;
    private int mNum;

    public RewardType Type => this.mType;

    public string IName => this.mIName;

    public int Num => this.mNum;

    public bool Deserialize(JSON_VersusRankRewardRewardParam json)
    {
      this.mType = (RewardType) json.item_type;
      this.mIName = json.item_iname;
      this.mNum = json.item_num;
      return true;
    }
  }
}
