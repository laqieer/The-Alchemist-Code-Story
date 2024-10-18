// Decompiled with JetBrains decompiler
// Type: ColorAnimation
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.UI;

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
    float t;
    if (this.AnimationType == ColorAnimation.AnimationTypes.Once)
    {
      this.mAnimPos += this.UpdateMode != AnimatorUpdateMode.UnscaledTime ? Time.deltaTime : Time.unscaledDeltaTime;
      t = Mathf.Clamp01(this.mAnimPos / this.Duration);
      if ((double) t >= 1.0)
        this.enabled = false;
    }
    else
    {
      t = (this.UpdateMode != AnimatorUpdateMode.UnscaledTime ? Time.time : Time.unscaledTime) % this.Duration / this.Duration;
      if (this.AnimationType == ColorAnimation.AnimationTypes.PingPong)
      {
        t *= 2f;
        if ((double) t >= 1.0)
          t = (float) (1.0 - ((double) t - 1.0));
      }
    }
    if (!((Object) this.Graphic != (Object) null))
      return;
    this.Graphic.color = Color.Lerp(this.ColorStart, this.ColorEnd, t);
  }

  public enum AnimationTypes
  {
    Loop,
    PingPong,
    Once,
  }
}
