// Decompiled with JetBrains decompiler
// Type: SRPG.UnitCursor
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;

#nullable disable
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
      set => this.mColor = value;
    }

    private void Update()
    {
      if ((double) this.mTime <= (double) this.mDuration)
      {
        this.mTime += Time.deltaTime;
        float num = Mathf.Sin((float) ((double) Mathf.Clamp01(this.mTime / this.mDuration) * 3.1415927410125732 * 0.5));
        this.mCurrentScale = Mathf.Lerp(this.mStartScale, this.mDesiredScale, num);
        this.mCurrentOpacity = Mathf.Lerp(this.mStartOpacity, this.mDesiredOpacity, num);
        Color mColor = this.mColor;
        mColor.a *= this.mCurrentOpacity;
        ((Component) this).transform.localScale = Vector3.op_Multiply(Vector3.one, this.mCurrentScale);
        ((Component) this).GetComponent<Renderer>().material.SetColor("_color", mColor);
      }
      else
      {
        if (!this.mDiscard)
          return;
        Object.DestroyImmediate((Object) ((Component) this).gameObject);
      }
    }

    public void FadeOut() => this.AnimateScale(this.EndScale, 0.0f, this.FadeTime, true);

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
