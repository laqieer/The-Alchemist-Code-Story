// Decompiled with JetBrains decompiler
// Type: SRPG.EventBanner2
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  public class EventBanner2 : MonoBehaviour
  {
    private Image mTarget;
    private LoadRequest mLoadRequest;

    private void Start()
    {
      this.Refresh();
    }

    private void Update()
    {
      if (this.mLoadRequest == null || (UnityEngine.Object) this.mTarget == (UnityEngine.Object) null)
      {
        this.enabled = false;
      }
      else
      {
        if (!this.mLoadRequest.isDone)
          return;
        BannerParam dataOfClass = DataSource.FindDataOfClass<BannerParam>(this.gameObject, (BannerParam) null);
        if (dataOfClass == null)
          return;
        GachaTabSprites asset = this.mLoadRequest.asset as GachaTabSprites;
        if ((UnityEngine.Object) asset != (UnityEngine.Object) null && asset.Sprites != null && asset.Sprites.Length > 0)
        {
          Sprite[] sprites = asset.Sprites;
          for (int index = 0; index < sprites.Length; ++index)
          {
            if ((UnityEngine.Object) sprites[index] != (UnityEngine.Object) null && sprites[index].name == dataOfClass.banr_sprite)
              this.mTarget.sprite = sprites[index];
          }
        }
        this.enabled = false;
      }
    }

    public void Refresh()
    {
      if (this.mLoadRequest != null)
        return;
      this.mTarget = this.GetComponent<Image>();
      BannerParam dataOfClass = DataSource.FindDataOfClass<BannerParam>(this.gameObject, (BannerParam) null);
      if (dataOfClass == null)
        return;
      this.mLoadRequest = AssetManager.LoadAsync<GachaTabSprites>(dataOfClass.banner);
    }
  }
}
