// Decompiled with JetBrains decompiler
// Type: SRPG.TowerRewardUI
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using UnityEngine;
using UnityEngine.UI;

#nullable disable
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
      ((Behaviour) this.ItemIcon).enabled = false;
      TowerRewardItem dataOfClass = DataSource.FindDataOfClass<TowerRewardItem>(((Component) this).gameObject, (TowerRewardItem) null);
      if (dataOfClass == null)
        return;
      ((Text) this.NumText).text = dataOfClass.num.ToString();
      if (Object.op_Inequality((Object) this.ItemFrameObj, (Object) null))
        this.ItemFrameObj.SetActive(true);
      switch (dataOfClass.type)
      {
        case TowerRewardItem.RewardType.Item:
          ((Behaviour) this.ItemIcon).enabled = true;
          this.ItemIcon.UpdateValue();
          ItemParam itemParam = MonoSingleton<GameManager>.Instance.GetItemParam(dataOfClass.iname);
          if (itemParam == null || !Object.op_Inequality((Object) this.ItemName, (Object) null))
            break;
          this.ItemName.text = itemParam.name;
          break;
        case TowerRewardItem.RewardType.Gold:
          this.BaseImage.texture = this.GoldImage;
          if (!Object.op_Inequality((Object) this.ItemName, (Object) null))
            break;
          this.ItemName.text = LocalizedText.Get("sys.GOLD");
          break;
        case TowerRewardItem.RewardType.Coin:
          this.BaseImage.texture = this.CoinImage;
          if (!Object.op_Inequality((Object) this.ItemName, (Object) null))
            break;
          this.ItemName.text = LocalizedText.Get("sys.COIN");
          break;
        case TowerRewardItem.RewardType.ArenaCoin:
          this.BaseImage.texture = this.ArenaCoinImage;
          if (!Object.op_Inequality((Object) this.ItemName, (Object) null))
            break;
          this.ItemName.text = LocalizedText.Get("sys.ARENA_COIN");
          break;
        case TowerRewardItem.RewardType.MultiCoin:
          this.BaseImage.texture = this.MultiCoinImage;
          if (!Object.op_Inequality((Object) this.ItemName, (Object) null))
            break;
          this.ItemName.text = LocalizedText.Get("sys.MULTI_COIN");
          break;
        case TowerRewardItem.RewardType.KakeraCoin:
          this.BaseImage.texture = this.KakeraCoinImage;
          if (!Object.op_Inequality((Object) this.ItemName, (Object) null))
            break;
          this.ItemName.text = LocalizedText.Get("sys.KakeraCoin");
          break;
        case TowerRewardItem.RewardType.Artifact:
          if (Object.op_Inequality((Object) this.ItemFrameObj, (Object) null))
            this.ItemFrameObj.SetActive(false);
          ArtifactParam artifactParam = MonoSingleton<GameManager>.Instance.MasterParam.GetArtifactParam(dataOfClass.iname);
          if (artifactParam == null || !Object.op_Inequality((Object) this.ItemName, (Object) null))
            break;
          this.ItemName.text = artifactParam.name;
          break;
      }
    }
  }
}
