// Decompiled with JetBrains decompiler
// Type: ColorAnimation
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;
using UnityEngine.UI;

#nullable disable
public class ColorAnimation : MonoBehaviour
{
  public float Duration = 1f;
  public Color ColorStart = Color.black;
  public Color ColorEnd = Color.white;
  public ColorAnimation.AnimationTypes AnimationType;
  public Graphic Graphic;
  public AnimatorUpdateMode UpdateMode;
  private float mAnimPos;

  private void Update()
  {
    if ((double) this.Duration <= 0.0)
      return;
    float num;
    if (this.AnimationType == ColorAnimation.AnimationTypes.Once)
    {
      this.mAnimPos += this.UpdateMode != 2 ? Time.deltaTime : Time.unscaledDeltaTime;
      num = Mathf.Clamp01(this.mAnimPos / this.Duration);
      if ((double) num >= 1.0)
        ((Behaviour) this).enabled = false;
    }
    else
    {
      num = (this.UpdateMode != 2 ? Time.time : Time.unscaledTime) % this.Duration / this.Duration;
      if (this.AnimationType == ColorAnimation.AnimationTypes.PingPong)
      {
        num *= 2f;
        if ((double) num >= 1.0)
          num = (float) (1.0 - ((double) num - 1.0));
      }
    }
    if (!Object.op_Inequality((Object) this.Graphic, (Object) null))
      return;
    this.Graphic.color = Color.Lerp(this.ColorStart, this.ColorEnd, num);
  }

  public enum AnimationTypes
  {
    Loop,
    PingPong,
    Once,
  }
}
