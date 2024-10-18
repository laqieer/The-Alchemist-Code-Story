// Decompiled with JetBrains decompiler
// Type: SRPG.RaidReward
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

#nullable disable
namespace SRPG
{
  public class RaidReward
  {
    private RaidRewardType mType;
    private string mIName;
    private int mNum;

    public RaidRewardType Type => this.mType;

    public string IName => this.mIName;

    public int Num => this.mNum;

    public bool Deserialize(JSON_RaidReward json)
    {
      this.mType = (RaidRewardType) json.item_type;
      this.mIName = json.item_iname;
      this.mNum = json.item_num;
      return true;
    }
  }
}
