// Decompiled with JetBrains decompiler
// Type: SRPG.NetworkIndicator
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  public class NetworkIndicator : MonoBehaviour
  {
    public float FadeTime = 1f;
    public float KeepUp = 0.5f;
    public GameObject Body;
    private CanvasGroup mCanvasGroup;
    private float mRemainingTime;
    private string lang;
    [SerializeField]
    private Image header;
    [SerializeField]
    private Image fotter;
    [SerializeField]
    private Sprite[] locHeaderSprites;
    [SerializeField]
    private Sprite[] locFotterSprites;

    private void Start()
    {
      if (!((UnityEngine.Object) this.Body != (UnityEngine.Object) null))
        return;
      this.mCanvasGroup = this.Body.GetComponent<CanvasGroup>();
      this.Body.SetActive(false);
    }

    private void Update()
    {
      if (!Network.IsIndicator)
      {
        this.Body.SetActive(false);
      }
      else
      {
        if (Network.IsBusy || !AssetDownloader.isDone || (FlowNode_NetworkIndicator.NeedDisplay() || EventAction.IsLoading))
          this.mRemainingTime = this.KeepUp + this.FadeTime;
        if ((double) this.mRemainingTime > 0.0)
        {
          this.mRemainingTime -= Time.unscaledDeltaTime;
          if ((UnityEngine.Object) this.mCanvasGroup != (UnityEngine.Object) null && (double) this.FadeTime > 0.0)
            this.mCanvasGroup.alpha = Mathf.Clamp01(this.mRemainingTime / this.FadeTime);
          if ((UnityEngine.Object) this.Body != (UnityEngine.Object) null)
            this.Body.SetActive((double) this.mRemainingTime > 0.0);
        }
        this.SetLocalizedSprite();
      }
    }

    private void SetLocalizedSprite()
    {
      if (!PlayerPrefs.HasKey("Selected_Language"))
        return;
      string configLanguage = GameUtility.Config_Language;
      if (!(this.lang != configLanguage))
        return;
      this.lang = configLanguage;
      int index = 0;
      string lang = this.lang;
      if (lang != null)
      {
        // ISSUE: reference to a compiler-generated field
        if (NetworkIndicator.\u003C\u003Ef__switch\u0024mapF == null)
        {
          // ISSUE: reference to a compiler-generated field
          NetworkIndicator.\u003C\u003Ef__switch\u0024mapF = new Dictionary<string, int>(3)
          {
            {
              "french",
              0
            },
            {
              "german",
              1
            },
            {
              "spanish",
              2
            }
          };
        }
        int num;
        // ISSUE: reference to a compiler-generated field
        if (NetworkIndicator.\u003C\u003Ef__switch\u0024mapF.TryGetValue(lang, out num))
        {
          switch (num)
          {
            case 0:
              index = 1;
              break;
            case 1:
              index = 2;
              break;
            case 2:
              index = 3;
              break;
          }
        }
      }
      if (this.locHeaderSprites != null && (UnityEngine.Object) this.header != (UnityEngine.Object) null && index < this.locHeaderSprites.Length)
        this.header.overrideSprite = this.locHeaderSprites[index];
      if (this.locFotterSprites == null || !((UnityEngine.Object) this.fotter != (UnityEngine.Object) null) || index >= this.locFotterSprites.Length)
        return;
      this.fotter.overrideSprite = this.locFotterSprites[index];
    }
  }
}
