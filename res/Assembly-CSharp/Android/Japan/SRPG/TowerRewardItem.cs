// Decompiled with JetBrains decompiler
// Type: SRPG.TowerRewardItem
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

namespace SRPG
{
  public class TowerRewardItem
  {
    public string iname;
    public int num;
    public TowerRewardItem.RewardType type;
    public bool visible;
    public bool is_new;

    public bool IsDisableReward
    {
      get
      {
        if (this.visible)
          return this.type == TowerRewardItem.RewardType.Gold;
        return true;
      }
    }

    public void Deserialize(JSON_TowerRewardItem json)
    {
      if (json == null)
        throw new InvalidJSONException();
      this.iname = json.iname;
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
