// Decompiled with JetBrains decompiler
// Type: SRPG.TowerRewardIcon
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using UnityEngine;

namespace SRPG
{
  public class TowerRewardIcon : MonoBehaviour
  {
    public GameParameter ItemIcon;
    public RawImage_Transparent BaseImage;
    public BitmapText NumText;
    public Texture GoldImage;
    public Texture CoinImage;
    public Texture ArenaCoinImage;
    public Texture MultiCoinImage;
    public Texture KakeraCoinImage;

    public void Refresh()
    {
      this.ItemIcon.enabled = false;
      TowerRewardItem dataOfClass = DataSource.FindDataOfClass<TowerRewardItem>(this.gameObject, (TowerRewardItem) null);
      if (dataOfClass == null)
        return;
      this.NumText.text = dataOfClass.num.ToString();
      switch (dataOfClass.type)
      {
        case TowerRewardItem.RewardType.Item:
          this.ItemIcon.enabled = true;
          this.ItemIcon.UpdateValue();
          break;
        case TowerRewardItem.RewardType.Gold:
          this.BaseImage.texture = this.GoldImage;
          break;
        case TowerRewardItem.RewardType.Coin:
          this.BaseImage.texture = this.CoinImage;
          break;
        case TowerRewardItem.RewardType.ArenaCoin:
          this.BaseImage.texture = this.ArenaCoinImage;
          break;
        case TowerRewardItem.RewardType.MultiCoin:
          this.BaseImage.texture = this.MultiCoinImage;
          break;
        case TowerRewardItem.RewardType.KakeraCoin:
          this.BaseImage.texture = this.KakeraCoinImage;
          break;
      }
    }
  }
}
