// Decompiled with JetBrains decompiler
// Type: SRPG.EventBanner2
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  public class EventBanner2 : MonoBehaviour
  {
    private Image mTarget;
    private LoadRequest mLoadRequest;

    private void Start() => this.Refresh();

    private void Update()
    {
      if (this.mLoadRequest == null || Object.op_Equality((Object) this.mTarget, (Object) null))
      {
        ((Behaviour) this).enabled = false;
      }
      else
      {
        if (!this.mLoadRequest.isDone)
          return;
        BannerParam dataOfClass = DataSource.FindDataOfClass<BannerParam>(((Component) this).gameObject, (BannerParam) null);
        if (dataOfClass == null)
          return;
        GachaTabSprites asset = this.mLoadRequest.asset as GachaTabSprites;
        if (Object.op_Inequality((Object) asset, (Object) null) && asset.Sprites != null && asset.Sprites.Length > 0)
        {
          Sprite[] sprites = asset.Sprites;
          for (int index = 0; index < sprites.Length; ++index)
          {
            if (Object.op_Inequality((Object) sprites[index], (Object) null) && ((Object) sprites[index]).name == dataOfClass.banr_sprite)
              this.mTarget.sprite = sprites[index];
          }
        }
        ((Behaviour) this).enabled = false;
      }
    }

    public void Refresh()
    {
      if (this.mLoadRequest != null)
        return;
      this.mTarget = ((Component) this).GetComponent<Image>();
      BannerParam dataOfClass = DataSource.FindDataOfClass<BannerParam>(((Component) this).gameObject, (BannerParam) null);
      if (dataOfClass == null)
        return;
      this.mLoadRequest = AssetManager.LoadAsync<GachaTabSprites>(dataOfClass.banner);
    }
  }
}
