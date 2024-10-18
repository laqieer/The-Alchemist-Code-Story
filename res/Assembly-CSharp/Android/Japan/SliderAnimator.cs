// Decompiled with JetBrains decompiler
// Type: SliderAnimator
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof (Slider))]
public class SliderAnimator : MonoBehaviour
{
  private Slider mSlider;
  private float mStartValue;
  private float mTargetValue;
  private float mTime;
  private float mTransitionTime;

  private void Awake()
  {
    this.mSlider = this.GetComponent<Slider>();
    this.enabled = false;
  }

  public void AnimateValue(float value, float time)
  {
    if ((double) time <= 0.0)
    {
      this.mSlider.value = value;
    }
    else
    {
      this.mStartValue = this.mSlider.value;
      this.mTargetValue = value;
      this.mTime = 0.0f;
      this.mTransitionTime = time;
      this.enabled = true;
    }
  }

  private void Update()
  {
    this.mTime += Time.deltaTime;
    float t = Mathf.Clamp01(this.mTime / this.mTransitionTime);
    this.mSlider.value = Mathf.Lerp(this.mStartValue, this.mTargetValue, t);
    if ((double) t < 1.0)
      return;
    this.enabled = false;
  }

  public bool IsAnimating
  {
    get
    {
      return this.enabled;
    }
  }
}
