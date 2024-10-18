// Decompiled with JetBrains decompiler
// Type: SliderAnimator
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;
using UnityEngine.UI;

#nullable disable
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
    this.mSlider = ((Component) this).GetComponent<Slider>();
    ((Behaviour) this).enabled = false;
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
      ((Behaviour) this).enabled = true;
    }
  }

  private void Update()
  {
    this.mTime += Time.deltaTime;
    float num = Mathf.Clamp01(this.mTime / this.mTransitionTime);
    this.mSlider.value = Mathf.Lerp(this.mStartValue, this.mTargetValue, num);
    if ((double) num < 1.0)
      return;
    ((Behaviour) this).enabled = false;
  }

  public bool IsAnimating => ((Behaviour) this).enabled;
}
