// Decompiled with JetBrains decompiler
// Type: SRPG.TowerRewardParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System.Collections.Generic;

#nullable disable
namespace SRPG
{
  public class TowerRewardParam
  {
    public string iname;
    protected List<TowerRewardItem> mTowerRewardItems;

    public void Deserialize(JSON_TowerRewardParam json)
    {
      this.iname = json != null ? json.iname : throw new InvalidJSONException();
      if (json.rewards == null)
        return;
      this.mTowerRewardItems = new List<TowerRewardItem>();
      for (int index = 0; index < json.rewards.Length; ++index)
      {
        TowerRewardItem towerRewardItem = new TowerRewardItem();
        towerRewardItem.Deserialize(json.rewards[index]);
        this.mTowerRewardItems.Add(towerRewardItem);
      }
    }

    public virtual List<TowerRewardItem> GetTowerRewardItem() => this.mTowerRewardItems;
  }
}
