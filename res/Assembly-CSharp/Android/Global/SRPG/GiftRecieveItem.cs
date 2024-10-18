// Decompiled with JetBrains decompiler
// Type: SRPG.GiftRecieveItem
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using GR;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  public class GiftRecieveItem : MonoBehaviour
  {
    public RawImage Icon;
    public Image Frame;
    public Text NameText;
    public Text AddText;
    public Text NumText;

    private bool HideNumText
    {
      set
      {
        GameUtility.SetGameObjectActive((Component) this.AddText, value);
        GameUtility.SetGameObjectActive((Component) this.NumText, value);
      }
    }

    private void Start()
    {
      this.UpdateValue();
    }

    private void OnDisable()
    {
      GameManager instanceDirect = MonoSingleton<GameManager>.GetInstanceDirect();
      if (!((UnityEngine.Object) instanceDirect != (UnityEngine.Object) null) || !((UnityEngine.Object) this.Icon != (UnityEngine.Object) null))
        return;
      instanceDirect.CancelTextureLoadRequest(this.Icon);
    }

    private string GetIconPath(ArtifactParam param)
    {
      if (param == null)
        return (string) null;
      return AssetPath.ArtifactIcon(param);
    }

    private string GetIconPath(ItemParam param)
    {
      if (param == null)
        return (string) null;
      return AssetPath.ItemIcon(param);
    }

    private Sprite GetFrameSprite(ArtifactParam param, int rarity)
    {
      Sprite sprite = this.Frame.sprite;
      if (param == null)
        return sprite;
      return GameSettings.Instance.ArtifactIcon_Frames[rarity];
    }

    private Sprite GetFrameSprite(ItemParam param, int rarity)
    {
      Sprite sprite = this.Frame.sprite;
      if (param == null)
        return sprite;
      return GameSettings.Instance.GetItemFrame(param);
    }

    private string GetName(ArtifactParam param)
    {
      return param?.name;
    }

    private string GetName(ItemParam param)
    {
      return param?.name;
    }

    public void UpdateValue()
    {
      GiftRecieveItemData dataOfClass = DataSource.FindDataOfClass<GiftRecieveItemData>(this.gameObject, (GiftRecieveItemData) null);
      if (dataOfClass == null)
        return;
      string path = (string) null;
      Sprite sprite = (Sprite) null;
      string str1 = (string) null;
      string str2 = (string) null;
      switch (dataOfClass.type)
      {
        case GiftTypes.Item:
        case GiftTypes.Unit:
          ItemParam itemParam1 = MonoSingleton<GameManager>.Instance.GetItemParam(dataOfClass.iname);
          path = this.GetIconPath(itemParam1);
          sprite = this.GetFrameSprite(itemParam1, dataOfClass.rarity);
          str1 = this.GetName(itemParam1);
          str2 = dataOfClass.num.ToString();
          break;
        case GiftTypes.Artifact:
          ArtifactParam artifactParam = MonoSingleton<GameManager>.Instance.MasterParam.GetArtifactParam(dataOfClass.iname);
          path = this.GetIconPath(artifactParam);
          sprite = this.GetFrameSprite(artifactParam, dataOfClass.rarity);
          str1 = this.GetName(artifactParam);
          str2 = dataOfClass.num.ToString();
          break;
        case GiftTypes.Award:
          ItemParam itemParam2 = MonoSingleton<GameManager>.Instance.GetAwardParam(dataOfClass.iname).ToItemParam();
          path = this.GetIconPath(itemParam2);
          sprite = this.GetFrameSprite(itemParam2, dataOfClass.rarity);
          str1 = LocalizedText.Get("sys.MAILBOX_ITEM_AWARD_RECEIVE") + this.GetName(itemParam2);
          this.HideNumText = false;
          break;
      }
      if ((UnityEngine.Object) this.Icon != (UnityEngine.Object) null)
        MonoSingleton<GameManager>.Instance.ApplyTextureAsync(this.Icon, path);
      if ((UnityEngine.Object) this.Frame != (UnityEngine.Object) null)
        this.Frame.sprite = sprite;
      if ((UnityEngine.Object) this.NameText != (UnityEngine.Object) null)
        this.NameText.text = str1;
      if (!((UnityEngine.Object) this.NumText != (UnityEngine.Object) null))
        return;
      this.NumText.text = str2;
    }
  }
}
