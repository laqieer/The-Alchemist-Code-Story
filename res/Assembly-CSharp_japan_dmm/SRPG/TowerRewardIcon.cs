// Decompiled with JetBrains decompiler
// Type: SRPG.TowerRewardIcon
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;
using UnityEngine.UI;

#nullable disable
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
      ((Behaviour) this.ItemIcon).enabled = false;
      TowerRewardItem dataOfClass = DataSource.FindDataOfClass<TowerRewardItem>(((Component) this).gameObject, (TowerRewardItem) null);
      if (dataOfClass == null)
        return;
      ((Text) this.NumText).text = dataOfClass.num.ToString();
      switch (dataOfClass.type)
      {
        case TowerRewardItem.RewardType.Item:
          ((Behaviour) this.ItemIcon).enabled = true;
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
