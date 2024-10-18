// Decompiled with JetBrains decompiler
// Type: SRPG.TowerRewardUI
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using GR;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  public class TowerRewardUI : MonoBehaviour
  {
    public GameParameter ItemIcon;
    public RawImage_Transparent BaseImage;
    public BitmapText NumText;
    public Texture GoldImage;
    public Texture CoinImage;
    public Texture ArenaCoinImage;
    public Texture MultiCoinImage;
    public Texture KakeraCoinImage;
    public Text ItemName;
    public Text ItemNameNumTex;
    public GameObject ItemFrameObj;

    public void Refresh()
    {
      this.ItemIcon.enabled = false;
      TowerRewardItem dataOfClass = DataSource.FindDataOfClass<TowerRewardItem>(this.gameObject, (TowerRewardItem) null);
      if (dataOfClass == null)
        return;
      this.NumText.text = dataOfClass.num.ToString();
      if ((UnityEngine.Object) this.ItemFrameObj != (UnityEngine.Object) null)
        this.ItemFrameObj.SetActive(true);
      switch (dataOfClass.type)
      {
        case TowerRewardItem.RewardType.Item:
          this.ItemIcon.enabled = true;
          this.ItemIcon.UpdateValue();
          ItemParam itemParam = MonoSingleton<GameManager>.Instance.GetItemParam(dataOfClass.iname);
          if (itemParam == null || !((UnityEngine.Object) this.ItemName != (UnityEngine.Object) null))
            break;
          this.ItemName.text = itemParam.name;
          break;
        case TowerRewardItem.RewardType.Gold:
          this.BaseImage.texture = this.GoldImage;
          if (!((UnityEngine.Object) this.ItemName != (UnityEngine.Object) null))
            break;
          this.ItemName.text = LocalizedText.Get("sys.GOLD");
          break;
        case TowerRewardItem.RewardType.Coin:
          this.BaseImage.texture = this.CoinImage;
          if (!((UnityEngine.Object) this.ItemName != (UnityEngine.Object) null))
            break;
          this.ItemName.text = LocalizedText.Get("sys.COIN");
          break;
        case TowerRewardItem.RewardType.ArenaCoin:
          this.BaseImage.texture = this.ArenaCoinImage;
          if (!((UnityEngine.Object) this.ItemName != (UnityEngine.Object) null))
            break;
          this.ItemName.text = LocalizedText.Get("sys.ARENA_COIN");
          break;
        case TowerRewardItem.RewardType.MultiCoin:
          this.BaseImage.texture = this.MultiCoinImage;
          if (!((UnityEngine.Object) this.ItemName != (UnityEngine.Object) null))
            break;
          this.ItemName.text = LocalizedText.Get("sys.MULTI_COIN");
          break;
        case TowerRewardItem.RewardType.KakeraCoin:
          this.BaseImage.texture = this.KakeraCoinImage;
          if (!((UnityEngine.Object) this.ItemName != (UnityEngine.Object) null))
            break;
          this.ItemName.text = LocalizedText.Get("sys.KakeraCoin");
          break;
        case TowerRewardItem.RewardType.Artifact:
          if ((UnityEngine.Object) this.ItemFrameObj != (UnityEngine.Object) null)
            this.ItemFrameObj.SetActive(false);
          ArtifactParam artifactParam = MonoSingleton<GameManager>.Instance.MasterParam.GetArtifactParam(dataOfClass.iname);
          if (artifactParam == null || !((UnityEngine.Object) this.ItemName != (UnityEngine.Object) null))
            break;
          this.ItemName.text = artifactParam.name;
          break;
      }
    }
  }
}
