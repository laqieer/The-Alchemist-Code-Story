// Decompiled with JetBrains decompiler
// Type: SRPG.UnitCursor
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

namespace SRPG
{
  public class UnitCursor : MonoBehaviour
  {
    public float StartScale = 2f;
    public float LoopScale = 1f;
    public float EndScale = 2f;
    public float Opacity = 1f;
    public float FadeTime = 1f;
    private float mCurrentScale;
    private float mTime;
    private float mDuration;
    private float mDesiredScale;
    private float mStartScale;
    private bool mDiscard;
    private Color mColor;
    private float mCurrentOpacity;
    private float mStartOpacity;
    private float mDesiredOpacity;

    private void Start()
    {
      this.mCurrentScale = this.StartScale;
      this.AnimateScale(this.LoopScale, this.Opacity, this.FadeTime, false);
      this.Update();
    }

    public Color Color
    {
      set
      {
        this.mColor = value;
      }
    }

    private void Update()
    {
      if ((double) this.mTime <= (double) this.mDuration)
      {
        this.mTime += Time.deltaTime;
        float t = Mathf.Sin((float) ((double) Mathf.Clamp01(this.mTime / this.mDuration) * 3.14159274101257 * 0.5));
        this.mCurrentScale = Mathf.Lerp(this.mStartScale, this.mDesiredScale, t);
        this.mCurrentOpacity = Mathf.Lerp(this.mStartOpacity, this.mDesiredOpacity, t);
        Color mColor = this.mColor;
        mColor.a *= this.mCurrentOpacity;
        this.transform.localScale = Vector3.one * this.mCurrentScale;
        this.GetComponent<Renderer>().material.SetColor("_color", mColor);
      }
      else
      {
        if (!this.mDiscard)
          return;
        UnityEngine.Object.DestroyImmediate((UnityEngine.Object) this.gameObject);
      }
    }

    public void FadeOut()
    {
      this.AnimateScale(this.EndScale, 0.0f, this.FadeTime, true);
    }

    private void AnimateScale(float endScale, float opacity, float time, bool destroyLater)
    {
      this.mTime = 0.0f;
      this.mDuration = time;
      this.mStartScale = this.mCurrentScale;
      this.mDesiredScale = endScale;
      this.mDiscard = destroyLater;
      this.mStartOpacity = this.mCurrentOpacity;
      this.mDesiredOpacity = opacity;
    }
  }
}
