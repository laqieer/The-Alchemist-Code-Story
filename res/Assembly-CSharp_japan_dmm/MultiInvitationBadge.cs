// Decompiled with JetBrains decompiler
// Type: MultiInvitationBadge
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;
using UnityEngine.UI;

#nullable disable
public class MultiInvitationBadge : MonoBehaviour
{
  public AnimationCurve AnimationCurve = new AnimationCurve(new Keyframe[2]
  {
    new Keyframe(0.0f, 0.0f, 0.0f, 1f),
    new Keyframe(1f, 1f, 1f, 0.0f)
  });
  public float Delay;
  public float Duration;
  private Image m_Image;
  private bool m_StartAnimation;
  private float m_Factor;
  private float m_Time;

  public static bool isValid { set; get; }

  private void Awake() => this.m_Image = ((Component) this).gameObject.GetComponent<Image>();

  private void Start()
  {
    if (Object.op_Inequality((Object) this.m_Image, (Object) null))
    {
      Color color = ((Graphic) this.m_Image).color;
      color.a = 1f;
      ((Graphic) this.m_Image).color = color;
      ((Behaviour) this.m_Image).enabled = false;
    }
    this.m_StartAnimation = false;
    this.m_Time = 0.0f;
    this.m_Factor = 0.0f;
  }

  private void Update()
  {
    if (MultiInvitationBadge.isValid)
      this.Play();
    else
      this.Stop();
    if (!this.m_StartAnimation)
      return;
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
      if (!Object.op_Inequality((Object) this.m_Image, (Object) null))
        return;
      Color color = ((Graphic) this.m_Image).color;
      color.a = num2;
      ((Graphic) this.m_Image).color = color;
      ((Behaviour) this.m_Image).enabled = true;
    }
  }

  public void Play()
  {
    if (this.m_StartAnimation)
      return;
    this.m_StartAnimation = true;
    this.m_Time = 0.0f;
    this.m_Factor = 0.0f;
  }

  public void Stop()
  {
    if (!this.m_StartAnimation)
      return;
    this.m_StartAnimation = false;
    this.m_Time = 0.0f;
    this.m_Factor = 0.0f;
    if (!Object.op_Inequality((Object) this.m_Image, (Object) null))
      return;
    ((Behaviour) this.m_Image).enabled = false;
  }
}
