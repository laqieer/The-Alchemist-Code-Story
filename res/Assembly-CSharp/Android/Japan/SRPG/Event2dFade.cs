// Decompiled with JetBrains decompiler
// Type: SRPG.Event2dFade
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  public class Event2dFade : MonoBehaviour
  {
    public RawImage mImage;
    private Color mCurrentColor;
    private Color mStartColor;
    private Color mEndColor;
    private float mCurrentTime;
    private float mDuration;
    private bool mInitialized;

    private static Event2dFade Instance { get; set; }

    public static Event2dFade Find()
    {
      return Event2dFade.Instance;
    }

    private void Awake()
    {
      if ((UnityEngine.Object) null != (UnityEngine.Object) Event2dFade.Instance)
        UnityEngine.Object.Destroy((UnityEngine.Object) this);
      Event2dFade.Instance = this;
    }

    private void OnDestroy()
    {
      if (!((UnityEngine.Object) this == (UnityEngine.Object) Event2dFade.Instance))
        return;
      Event2dFade.Instance = (Event2dFade) null;
    }

    public bool IsFading
    {
      get
      {
        return (double) this.mCurrentTime < (double) this.mDuration;
      }
    }

    public void FadeTo(Color dest, float time)
    {
      if (!this.mInitialized)
      {
        this.mCurrentColor = dest;
        this.mCurrentColor.a = 1f - this.mCurrentColor.a;
        this.mInitialized = true;
        this.mImage.color = this.mCurrentColor;
      }
      if ((double) time > 0.0)
      {
        this.mStartColor = this.mCurrentColor;
        this.mEndColor = dest;
        this.mCurrentTime = 0.0f;
        this.mDuration = time;
        this.gameObject.SetActive(true);
      }
      else
      {
        this.mCurrentColor = dest;
        this.mCurrentTime = 0.0f;
        this.mDuration = 0.0f;
        this.mImage.color = this.mCurrentColor;
        this.gameObject.SetActive((double) this.mCurrentColor.a > 0.0);
      }
    }

    private void Update()
    {
      if ((double) this.mCurrentTime >= (double) this.mDuration)
      {
        if ((double) this.mCurrentColor.a > 0.0 || !this.gameObject.GetActive())
          return;
        this.gameObject.SetActive(false);
      }
      else
      {
        this.mCurrentTime += Time.unscaledDeltaTime;
        this.mCurrentColor = Color.Lerp(this.mStartColor, this.mEndColor, Mathf.Clamp01(this.mCurrentTime / this.mDuration));
        this.mImage.color = this.mCurrentColor;
      }
    }
  }
}
