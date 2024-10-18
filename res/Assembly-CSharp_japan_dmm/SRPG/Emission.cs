// Decompiled with JetBrains decompiler
// Type: SRPG.Emission
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  public class Emission : MonoBehaviour
  {
    public AnimationCurve AnimationCurve = new AnimationCurve(new Keyframe[2]
    {
      new Keyframe(0.0f, 0.0f, 0.0f, 1f),
      new Keyframe(1f, 1f, 1f, 0.0f)
    });
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
        if (!Object.op_Inequality((Object) this.Image, (Object) null))
          return;
        Color color = ((Graphic) this.Image).color;
        color.a = num2;
        ((Graphic) this.Image).color = color;
        ((Behaviour) this.Image).enabled = true;
      }
    }
  }
}
