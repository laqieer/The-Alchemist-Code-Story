// Decompiled with JetBrains decompiler
// Type: SRPG.Emission
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  public class Emission : MonoBehaviour
  {
    public AnimationCurve AnimationCurve = new AnimationCurve(new Keyframe[2]{ new Keyframe(0.0f, 0.0f, 0.0f, 1f), new Keyframe(1f, 1f, 1f, 0.0f) });
    public float Delay;
    public float Duration;
    public Image Image;
    private float m_Factor;
    private float m_Time;

    private void Update()
    {
      float deltaTime = Time.deltaTime;
      this.m_Time += deltaTime;
      if ((double) this.m_Time < (double) this.Delay)
      {
        this.m_Factor = 0.0f;
      }
      else
      {
        this.m_Factor += deltaTime;
        float num1 = (double) this.Duration > 0.0 ? this.m_Factor / this.Duration : 1f;
        if ((double) num1 >= 1.0)
          this.m_Factor = 0.0f;
        float num2 = this.AnimationCurve.Evaluate(Mathf.Clamp01(num1));
        if (!((UnityEngine.Object) this.Image != (UnityEngine.Object) null))
          return;
        Color color = this.Image.color;
        color.a = num2;
        this.Image.color = color;
        this.Image.enabled = true;
      }
    }
  }
}
