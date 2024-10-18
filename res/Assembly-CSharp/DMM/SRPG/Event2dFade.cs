// Decompiled with JetBrains decompiler
// Type: SRPG.Event2dFade
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;
using UnityEngine.UI;

#nullable disable
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

    public static Event2dFade Find() => Event2dFade.Instance;

    private void Awake()
    {
      if (Object.op_Inequality((Object) null, (Object) Event2dFade.Instance))
        Object.Destroy((Object) this);
      Event2dFade.Instance = this;
    }

    private void OnDestroy()
    {
      if (!Object.op_Equality((Object) this, (Object) Event2dFade.Instance))
        return;
      Event2dFade.Instance = (Event2dFade) null;
    }

    public bool IsFading => (double) this.mCurrentTime < (double) this.mDuration;

    public void FadeTo(Color dest, float time)
    {
      if (!this.mInitialized)
      {
        this.mCurrentColor = dest;
        this.mCurrentColor.a = 1f - this.mCurrentColor.a;
        this.mInitialized = true;
        ((Graphic) this.mImage).color = this.mCurrentColor;
      }
      if ((double) time > 0.0)
      {
        this.mStartColor = this.mCurrentColor;
        this.mEndColor = dest;
        this.mCurrentTime = 0.0f;
        this.mDuration = time;
        ((Component) this).gameObject.SetActive(true);
      }
      else
      {
        this.mCurrentColor = dest;
        this.mCurrentTime = 0.0f;
        this.mDuration = 0.0f;
        ((Graphic) this.mImage).color = this.mCurrentColor;
        ((Component) this).gameObject.SetActive((double) this.mCurrentColor.a > 0.0);
      }
    }

    private void Update()
    {
      if ((double) this.mCurrentTime >= (double) this.mDuration)
      {
        if ((double) this.mCurrentColor.a > 0.0 || !((Component) this).gameObject.GetActive())
          return;
        ((Component) this).gameObject.SetActive(false);
      }
      else
      {
        this.mCurrentTime += Time.unscaledDeltaTime;
        this.mCurrentColor = Color.Lerp(this.mStartColor, this.mEndColor, Mathf.Clamp01(this.mCurrentTime / this.mDuration));
        ((Graphic) this.mImage).color = this.mCurrentColor;
      }
    }
  }
}
