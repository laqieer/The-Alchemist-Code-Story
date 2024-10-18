// Decompiled with JetBrains decompiler
// Type: SRPG.HomeGalleryIcon
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;
using UnityEngine;

namespace SRPG
{
  public class HomeGalleryIcon : MonoBehaviour
  {
    private float mRefreshInterval = 1f;
    [SerializeField]
    private GameObject Banner;
    private HighlightParam[] mHilights;

    private void Awake()
    {
      this.Banner.SetActive(false);
      HighlightParam[] highlightParam = MonoSingleton<GameManager>.Instance.MasterParam.HighlightParam;
      if (highlightParam != null && highlightParam.Length > 0)
        this.mHilights = MonoSingleton<GameManager>.Instance.MasterParam.HighlightParam;
      if (this.mHilights == null || this.mHilights.Length < 1 || (UnityEngine.Object) this.Banner == (UnityEngine.Object) null)
        this.enabled = false;
      else
        this.CheckAvailable();
    }

    private void Update()
    {
      this.mRefreshInterval -= Time.unscaledDeltaTime;
      if ((double) this.mRefreshInterval > 0.0)
        return;
      this.CheckAvailable();
      this.mRefreshInterval = 1f;
    }

    private void CheckAvailable()
    {
      bool flag = false;
      for (int index = 0; index < this.mHilights.Length; ++index)
      {
        if (this.mHilights[index].IsAvailable())
        {
          flag = true;
          break;
        }
      }
      this.Banner.SetActive(flag);
    }
  }
}
