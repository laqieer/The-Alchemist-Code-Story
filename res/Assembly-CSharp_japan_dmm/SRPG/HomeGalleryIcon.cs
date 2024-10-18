// Decompiled with JetBrains decompiler
// Type: SRPG.HomeGalleryIcon
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using UnityEngine;

#nullable disable
namespace SRPG
{
  public class HomeGalleryIcon : MonoBehaviour
  {
    [SerializeField]
    private GameObject Banner;
    private float mRefreshInterval = 1f;
    private HighlightParam[] mHilights;

    private void Awake()
    {
      this.Banner.SetActive(false);
      HighlightParam[] highlightParam = MonoSingleton<GameManager>.Instance.MasterParam.HighlightParam;
      if (highlightParam != null && highlightParam.Length > 0)
        this.mHilights = MonoSingleton<GameManager>.Instance.MasterParam.HighlightParam;
      if (this.mHilights == null || this.mHilights.Length < 1 || Object.op_Equality((Object) this.Banner, (Object) null))
        ((Behaviour) this).enabled = false;
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
