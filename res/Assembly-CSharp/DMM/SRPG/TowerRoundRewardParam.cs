// Decompiled with JetBrains decompiler
// Type: SRPG.TowerRoundRewardParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System.Collections.Generic;

#nullable disable
namespace SRPG
{
  public class TowerRoundRewardParam : TowerRewardParam
  {
    public List<byte> mRoundList;

    public void Deserialize(JSON_TowerRoundRewardParam json)
    {
      this.iname = json != null ? json.iname : throw new InvalidJSONException();
      this.mRoundList = new List<byte>();
      if (json.rewards == null)
        return;
      this.mTowerRewardItems = new List<TowerRewardItem>();
      for (int index = 0; index < json.rewards.Length; ++index)
      {
        TowerRewardItem towerRewardItem = new TowerRewardItem();
        towerRewardItem.Deserialize((JSON_TowerRewardItem) json.rewards[index]);
        this.mTowerRewardItems.Add(towerRewardItem);
        this.mRoundList.Add(json.rewards[index].round_num);
      }
    }

    public override List<TowerRewardItem> GetTowerRewardItem()
    {
      TowerResuponse towerResuponse = MonoSingleton<GameManager>.Instance.TowerResuponse;
      byte round = towerResuponse.round;
      List<TowerRewardItem> towerRewardItem = new List<TowerRewardItem>();
      for (int index = 0; index < this.mRoundList.Count; ++index)
      {
        if ((int) towerResuponse.round >= (int) round)
          towerRewardItem.Add(this.mTowerRewardItems[index]);
      }
      return towerRewardItem;
    }
  }
}
