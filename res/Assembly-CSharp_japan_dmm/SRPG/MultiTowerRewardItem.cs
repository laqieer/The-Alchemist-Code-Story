// Decompiled with JetBrains decompiler
// Type: SRPG.MultiTowerRewardItem
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

#nullable disable
namespace SRPG
{
  public class MultiTowerRewardItem
  {
    public int round_st;
    public int round_ed;
    public MultiTowerRewardItem.RewardType type;
    public string itemname;
    public int num;

    public void Deserialize(JSON_MultiTowerRewardItem json)
    {
      this.round_st = json != null ? json.round_st : throw new InvalidJSONException();
      this.round_ed = json.round_ed;
      this.itemname = json.itemname;
      this.num = json.num;
      this.type = (MultiTowerRewardItem.RewardType) json.type;
    }

    public enum RewardType : byte
    {
      None,
      Item,
      Coin,
      Artifact,
      Award,
      Unit,
      Gold,
    }
  }
}
