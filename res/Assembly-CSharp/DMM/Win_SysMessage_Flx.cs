﻿// Decompiled with JetBrains decompiler
// Type: Win_SysMessage_Flx
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;
using UnityEngine.UI;

#nullable disable
[AddComponentMenu("UI/Drafts/Win_SysMessage_Flx")]
public class Win_SysMessage_Flx : UIDraft
{
  [UIDraft.AutoGenerated]
  public Text Text_Message;
  public Image Bg;
  public AnimationCurve AnimationCurve = new AnimationCurve(new Keyframe[2]
  {
    new Keyframe(0.0f, 0.0f, 0.0f, 1f),
    new Keyframe(1f, 1f, 1f, 0.0f)
  });
  public float Delay;
  public float Duration;
  private bool m_StartAnimation;
  private float m_Factor;
  private float m_Time;
  private bool m_AutoClose;
  private float m_AutoCloseTime;
  private UIWindow m_Window;

  public UIWindow window
  {
    get
    {
      if (Object.op_Equality((Object) this.m_Window, (Object) null))
        this.m_Window = ((Component) this).GetComponent<UIWindow>();
      return this.m_Window;
    }
  }

  private void Awake()
  {
  }

  private void Start()
  {
  }

  public void Initialize(bool isBg, float alpha = 0.0f)
  {
    if (Object.op_Inequality((Object) this.Text_Message, (Object) null))
    {
      Color color = ((Graphic) this.Text_Message).color;
      color.a = 1f;
      ((Graphic) this.Text_Message).color = color;
    }
    this.m_StartAnimation = false;
    this.m_Time = 0.0f;
    this.m_Factor = 0.0f;
    if (!Object.op_Inequality((Object) this.Bg, (Object) null))
      return;
    ((Component) this.Bg).gameObject.SetActive(isBg);
    Color color1 = ((Graphic) this.Bg).color;
    color1.a = alpha;
    ((Graphic) this.Bg).color = color1;
  }

  private void Update()
  {
    if (this.m_AutoClose)
    {
      this.m_AutoCloseTime -= Time.deltaTime;
      if ((double) this.m_AutoCloseTime < 0.0)
      {
        this.m_AutoClose = false;
        this.BeginClose();
      }
    }
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
      Color color = ((Graphic) this.Text_Message).color;
      color.a = num2;
      ((Graphic) this.Text_Message).color = color;
    }
  }

  public void StartAnim()
  {
    this.m_StartAnimation = true;
    this.m_Time = 0.0f;
    this.m_Factor = 0.0f;
    ((Behaviour) this).enabled = true;
  }

  public void AutoClose(float time)
  {
    this.m_AutoClose = true;
    this.m_AutoCloseTime = time;
  }

  public void BeginClose()
  {
    UIUtility.PopCanvas(true);
    this.window.OnWindowClose = new UIWindow.WindowEvent(this.OnWindowClose);
    this.window.Close();
  }

  private void OnWindowClose(UIWindow window)
  {
    Object.DestroyImmediate((Object) ((Component) ((Component) this).GetComponentInParent<Canvas>()).gameObject);
  }
}
