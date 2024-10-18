﻿// Decompiled with JetBrains decompiler
// Type: SRPG.TowerRewardItem
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

namespace SRPG
{
  public class TowerRewardItem
  {
    public string iname;
    public int num;
    public TowerRewardItem.RewardType type;
    public bool visible;
    public bool is_new;

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
