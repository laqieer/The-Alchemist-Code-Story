// Decompiled with JetBrains decompiler
// Type: SRPG.TowerRewardItem
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

#nullable disable
namespace SRPG
{
  public class TowerRewardItem
  {
    public string iname;
    public int num;
    public TowerRewardItem.RewardType type;
    public bool visible;
    public bool is_new;

    public bool IsDisableReward => !this.visible || this.type == TowerRewardItem.RewardType.Gold;

    public void Deserialize(JSON_TowerRewardItem json)
    {
      this.iname = json != null ? json.iname : throw new InvalidJSONException();
      this.type = (TowerRewardItem.RewardType) json.type;
      this.num = json.num;
      this.visible = json.visible == (byte) 1;
    }

    public enum RewardType : byte
    {
      Item,
      Gold,
      Coin,
      ArenaCoin,
      MultiCoin,
      KakeraCoin,
      Artifact,
    }
  }
}
