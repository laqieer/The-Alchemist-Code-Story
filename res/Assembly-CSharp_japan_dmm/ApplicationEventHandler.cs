// Decompiled with JetBrains decompiler
// Type: ApplicationEventHandler
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using SRPG;
using System;
using UnityEngine;
using UnityEngine.Events;

#nullable disable
public class ApplicationEventHandler : MonoBehaviour
{
  private bool m_IsQuiting;
  private EmbedWindowYesNo m_QuitWindow;
  private Rect m_WindowRect;
  private Vector2 m_FrameSize;
  private Rect m_PrimaryWindowRect = new Rect(0.0f, 0.0f, 0.0f, 0.0f);
  private EnumBitArray<ApplicationEventHandler.State> m_EventFlag = new EnumBitArray<ApplicationEventHandler.State>();

  public void OpenQuitWindow()
  {
    if (!UnityEngine.Object.op_Equality((UnityEngine.Object) this.m_QuitWindow, (UnityEngine.Object) null) || this.m_IsQuiting)
      return;
    this.m_QuitWindow = EmbedWindowYesNo.Create(LocalizedText.Get("embed.APP_QUIT"), new EmbedWindowYesNo.YesNoWindowEvent(this.OnApplicationQuitWindowResult));
  }

  public void OnApplicationQuitWindowResult(bool yes)
  {
    if (yes)
      this.OnDecide();
    else
      this.OnCancel();
  }

  private void OnCancel()
  {
    this.m_QuitWindow = (EmbedWindowYesNo) null;
    this.m_IsQuiting = false;
  }

  private void OnDecide()
  {
    this.m_QuitWindow = (EmbedWindowYesNo) null;
    this.m_IsQuiting = true;
    Application.Quit();
  }

  private void Awake()
  {
    UnityEngine.Object.DontDestroyOnLoad((UnityEngine.Object) this);
    IntPtr window = WindowsAPI.FindWindow((string) null, Application.productName);
    Rect clientRect = WindowsAPI.GetClientRect(window);
    this.m_WindowRect = WindowsAPI.GetWindowRect(window);
    this.m_FrameSize.x = Mathf.Floor(((Rect) ref this.m_WindowRect).size.x - ((Rect) ref clientRect).size.x);
    this.m_FrameSize.y = Mathf.Floor(((Rect) ref this.m_WindowRect).size.y - ((Rect) ref clientRect).size.y);
    // ISSUE: method pointer
    MonoSingleton<WindowProc>.Instance.AddWM_CloseListener(new UnityAction<WindowProc.WM_CloseEvent.Param>((object) this, __methodptr(OnWM_Close)));
    // ISSUE: method pointer
    MonoSingleton<WindowProc>.Instance.AddWM_MoveListener(new UnityAction<WindowProc.WM_MoveEvent.Param>((object) this, __methodptr(OnWM_Move)));
    // ISSUE: method pointer
    MonoSingleton<WindowProc>.Instance.AddWM_SizeListener(new UnityAction<WindowProc.WM_SizeEvent.Param>((object) this, __methodptr(OnWM_Size)));
    // ISSUE: method pointer
    MonoSingleton<WindowProc>.Instance.AddWM_SizingListener(new UnityAction<WindowProc.WM_SizingEvent.Param>((object) this, __methodptr(OnWM_Sizing)));
    this.InitializeWindowSize(window);
    WindowsAPI.MonitorInfoEx monitorInfo = WindowsAPI.GetMonitorInfo(window);
    if (monitorInfo == null)
      return;
    this.m_PrimaryWindowRect = this.ToScreenRect(monitorInfo.workAreaRect);
  }

  private void InitializeWindowSize(IntPtr hWnd)
  {
    Rect windowRect = WindowsAPI.GetWindowRect(hWnd);
    Rect screenRect1 = this.ToScreenRect(windowRect);
    this.ToScreenRect(this.m_WindowRect);
    WindowsAPI.MonitorInfoEx monitorInfo = WindowsAPI.GetMonitorInfo(hWnd);
    if (monitorInfo == null || monitorInfo.workAreaRect.Contains(windowRect))
      return;
    if ((double) ((Rect) ref monitorInfo.workAreaRect).width < (double) ((Rect) ref windowRect).width || (double) ((Rect) ref monitorInfo.workAreaRect).height < (double) ((Rect) ref windowRect).height)
    {
      Rect screenRect2 = this.ToScreenRect(monitorInfo.workAreaRect);
      float num1 = Mathf.Clamp(((Rect) ref screenRect1).width, 480f, ((Rect) ref screenRect2).width);
      float num2 = Mathf.Clamp(((Rect) ref screenRect1).height, 270f, ((Rect) ref screenRect2).height);
      float num3 = Mathf.Floor(num2 * 1.77777779f);
      float num4 = Mathf.Floor(num1 / 1.77777779f);
      if ((double) num1 <= (double) ((Rect) ref screenRect2).width && (double) num4 <= (double) ((Rect) ref screenRect2).height)
        num3 = num1;
      else
        num4 = num2;
      ((Rect) ref windowRect).width = num3;
      ((Rect) ref windowRect).height = num4;
    }
    ((Rect) ref windowRect).center = ((Rect) ref monitorInfo.workAreaRect).center;
    WindowsAPI.MoveWindow(hWnd, windowRect, true);
  }

  private Rect ToScreenRect(Rect value)
  {
    ((Rect) ref value).size = Vector2.op_Subtraction(((Rect) ref value).size, this.m_FrameSize);
    return value;
  }

  private Rect ToWindowRect(Rect value)
  {
    ((Rect) ref value).size = Vector2.op_Addition(((Rect) ref value).size, this.m_FrameSize);
    return value;
  }

  private void OnWM_Close(WindowProc.WM_CloseEvent.Param param)
  {
    param.EnableSkipProc();
    this.OpenQuitWindow();
  }

  public void OnWM_Move(WindowProc.WM_MoveEvent.Param param)
  {
    ((Rect) ref this.m_WindowRect).x = param.pos.x;
    ((Rect) ref this.m_WindowRect).y = param.pos.y;
  }

  public void OnWM_Size(WindowProc.WM_SizeEvent.Param param)
  {
    if (param.eventType == WindowProc.WM_SizeEvent.EventType.Minimized || param.eventType == WindowProc.WM_SizeEvent.EventType.Maxshow || param.eventType == WindowProc.WM_SizeEvent.EventType.Maxhide)
      return;
    if (Vector2.op_Equality(((Rect) ref this.m_WindowRect).size, Vector2.op_Addition(param.size, this.m_FrameSize)))
    {
      this.m_EventFlag.Set(ApplicationEventHandler.State.Resize, false);
    }
    else
    {
      if (this.m_EventFlag.Get(ApplicationEventHandler.State.Resize))
        return;
      Rect windowRect1 = WindowsAPI.GetWindowRect(param.hWnd);
      ((Rect) ref windowRect1).size = Vector2.op_Addition(param.size, this.m_FrameSize);
      Rect screenRect1 = this.ToScreenRect(windowRect1);
      Rect screenRect2 = this.ToScreenRect(this.m_WindowRect);
      WindowsAPI.MonitorInfoEx monitorInfo = WindowsAPI.GetMonitorInfo(param.hWnd);
      if (monitorInfo == null)
        return;
      Rect screenRect3 = this.ToScreenRect(monitorInfo.workAreaRect);
      if ((double) ((Rect) ref this.m_PrimaryWindowRect).width > 0.0 && (double) ((Rect) ref screenRect3).width > (double) ((Rect) ref this.m_PrimaryWindowRect).width)
        ((Rect) ref screenRect3).width = ((Rect) ref this.m_PrimaryWindowRect).width;
      if ((double) ((Rect) ref this.m_PrimaryWindowRect).height > 0.0 && (double) ((Rect) ref screenRect3).height > (double) ((Rect) ref this.m_PrimaryWindowRect).height)
        ((Rect) ref screenRect3).height = ((Rect) ref this.m_PrimaryWindowRect).height;
      float num1 = Mathf.Clamp(((Rect) ref screenRect1).width, 480f, ((Rect) ref screenRect3).width);
      float num2 = Mathf.Clamp(((Rect) ref screenRect1).height, 270f, ((Rect) ref screenRect3).height);
      float num3 = Mathf.Floor(num2 * 1.77777779f);
      float num4 = Mathf.Floor(num1 / 1.77777779f);
      if ((double) num1 <= (double) ((Rect) ref screenRect3).width && (double) num4 <= (double) ((Rect) ref screenRect3).height)
        num3 = num1;
      else
        num4 = num2;
      ((Rect) ref screenRect2).width = num3;
      ((Rect) ref screenRect2).height = num4;
      ((Rect) ref screenRect1).width = ((Rect) ref screenRect2).width;
      ((Rect) ref screenRect1).height = ((Rect) ref screenRect2).height;
      this.m_WindowRect = this.ToWindowRect(screenRect2);
      Rect windowRect2 = this.ToWindowRect(screenRect1);
      if (param.eventType == WindowProc.WM_SizeEvent.EventType.Maximized)
        ((Rect) ref windowRect2).center = ((Rect) ref monitorInfo.workAreaRect).center;
      if (Vector2.op_Equality(((Rect) ref screenRect1).size, param.size))
      {
        param.DisableSkipProc();
      }
      else
      {
        param.EnableSkipProc();
        this.m_EventFlag.Set(ApplicationEventHandler.State.Resize, true);
        WindowsAPI.MoveWindow(param.hWnd, windowRect2, true);
      }
    }
  }

  public void OnWM_Sizing(WindowProc.WM_SizingEvent.Param param)
  {
    if ((double) ((Rect) ref param.rect).width == 0.0 && (double) ((Rect) ref param.rect).height == 0.0)
      return;
    Rect screenRect1 = this.ToScreenRect(param.rect);
    Rect screenRect2 = this.ToScreenRect(this.m_WindowRect);
    WindowsAPI.MonitorInfoEx monitorInfo = WindowsAPI.GetMonitorInfo(param.hWnd);
    if (monitorInfo == null)
      return;
    Rect screenRect3 = this.ToScreenRect(monitorInfo.workAreaRect);
    if ((double) ((Rect) ref this.m_PrimaryWindowRect).width > 0.0 && (double) ((Rect) ref screenRect3).width > (double) ((Rect) ref this.m_PrimaryWindowRect).width)
      ((Rect) ref screenRect3).width = ((Rect) ref this.m_PrimaryWindowRect).width;
    if ((double) ((Rect) ref this.m_PrimaryWindowRect).height > 0.0 && (double) ((Rect) ref screenRect3).height > (double) ((Rect) ref this.m_PrimaryWindowRect).height)
      ((Rect) ref screenRect3).height = ((Rect) ref this.m_PrimaryWindowRect).height;
    float num1 = Mathf.Clamp(((Rect) ref screenRect1).width, 480f, ((Rect) ref screenRect3).width);
    float num2 = Mathf.Clamp(((Rect) ref screenRect1).height, 270f, ((Rect) ref screenRect3).height);
    float num3 = Mathf.Floor(num2 * 1.77777779f);
    float num4 = Mathf.Floor(num1 / 1.77777779f);
    if (param.eventType == WindowProc.WM_SizingEvent.EventType.TopLeft || param.eventType == WindowProc.WM_SizingEvent.EventType.TopRight || param.eventType == WindowProc.WM_SizingEvent.EventType.BottomLeft || param.eventType == WindowProc.WM_SizingEvent.EventType.BottomRight)
    {
      if ((double) (Mathf.Max(((Rect) ref this.m_WindowRect).width, ((Rect) ref param.rect).size.x) / Mathf.Min(((Rect) ref this.m_WindowRect).width, ((Rect) ref param.rect).size.x)) < (double) (Mathf.Max(((Rect) ref this.m_WindowRect).height, ((Rect) ref param.rect).size.y) / Mathf.Min(((Rect) ref this.m_WindowRect).height, ((Rect) ref param.rect).size.y)))
        num3 = num1;
      else
        num4 = num2;
      if ((double) num3 > (double) ((Rect) ref screenRect3).width)
      {
        num3 = ((Rect) ref screenRect3).width;
        num4 = Mathf.Floor(num1 / 1.77777779f);
      }
      else if ((double) num4 > (double) ((Rect) ref screenRect3).height)
      {
        num4 = ((Rect) ref screenRect3).height;
        num3 = Mathf.Floor(num4 * 1.77777779f);
      }
    }
    else if (param.eventType == WindowProc.WM_SizingEvent.EventType.Top || param.eventType == WindowProc.WM_SizingEvent.EventType.Bottom)
    {
      if ((double) num3 > (double) ((Rect) ref screenRect3).width)
      {
        num3 = ((Rect) ref screenRect3).width;
        num4 = Mathf.Floor(num1 / 1.77777779f);
      }
      else
        num4 = num2;
    }
    else if (param.eventType == WindowProc.WM_SizingEvent.EventType.Left || param.eventType == WindowProc.WM_SizingEvent.EventType.Right)
    {
      if ((double) num4 > (double) ((Rect) ref screenRect3).height)
      {
        num4 = ((Rect) ref screenRect3).height;
        num3 = Mathf.Floor(num4 * 1.77777779f);
      }
      else
        num3 = num1;
    }
    switch (param.eventType)
    {
      case WindowProc.WM_SizingEvent.EventType.Left:
        ((Rect) ref screenRect2).xMin = ((Rect) ref screenRect1).xMax - num1;
        ((Rect) ref screenRect2).width = num3;
        ((Rect) ref screenRect2).height = num4;
        ((Rect) ref screenRect1).xMin = ((Rect) ref screenRect2).xMin;
        ((Rect) ref screenRect1).height = ((Rect) ref screenRect2).height;
        break;
      case WindowProc.WM_SizingEvent.EventType.Right:
        ((Rect) ref screenRect2).width = num3;
        ((Rect) ref screenRect2).height = num4;
        ((Rect) ref screenRect1).width = ((Rect) ref screenRect2).width;
        ((Rect) ref screenRect1).height = ((Rect) ref screenRect2).height;
        break;
      case WindowProc.WM_SizingEvent.EventType.Top:
        ((Rect) ref screenRect2).yMin = ((Rect) ref screenRect1).yMax - num2;
        ((Rect) ref screenRect2).width = num3;
        ((Rect) ref screenRect2).height = num4;
        ((Rect) ref screenRect1).width = ((Rect) ref screenRect2).width;
        ((Rect) ref screenRect1).yMin = ((Rect) ref screenRect2).yMin;
        break;
      case WindowProc.WM_SizingEvent.EventType.TopLeft:
        ((Rect) ref screenRect2).xMin = ((Rect) ref screenRect1).xMax - num3;
        ((Rect) ref screenRect2).yMin = ((Rect) ref screenRect1).yMax - num4;
        ((Rect) ref screenRect2).width = num3;
        ((Rect) ref screenRect2).height = num4;
        ((Rect) ref screenRect1).xMin = ((Rect) ref screenRect2).xMin;
        ((Rect) ref screenRect1).yMin = ((Rect) ref screenRect2).yMin;
        break;
      case WindowProc.WM_SizingEvent.EventType.TopRight:
        ((Rect) ref screenRect2).yMin = ((Rect) ref screenRect1).yMax - num4;
        ((Rect) ref screenRect2).width = num3;
        ((Rect) ref screenRect2).height = num4;
        ((Rect) ref screenRect1).yMin = ((Rect) ref screenRect2).yMin;
        ((Rect) ref screenRect1).width = ((Rect) ref screenRect2).width;
        break;
      case WindowProc.WM_SizingEvent.EventType.Bottom:
        ((Rect) ref screenRect2).width = num3;
        ((Rect) ref screenRect2).height = num4;
        ((Rect) ref screenRect1).width = ((Rect) ref screenRect2).width;
        ((Rect) ref screenRect1).height = ((Rect) ref screenRect2).height;
        break;
      case WindowProc.WM_SizingEvent.EventType.BottomLeft:
        ((Rect) ref screenRect2).xMin = ((Rect) ref screenRect1).xMax - num3;
        ((Rect) ref screenRect2).width = num3;
        ((Rect) ref screenRect2).height = num4;
        ((Rect) ref screenRect1).xMin = ((Rect) ref screenRect2).xMin;
        ((Rect) ref screenRect1).height = ((Rect) ref screenRect2).height;
        break;
      case WindowProc.WM_SizingEvent.EventType.BottomRight:
        ((Rect) ref screenRect2).width = num3;
        ((Rect) ref screenRect2).height = num4;
        ((Rect) ref screenRect1).width = ((Rect) ref screenRect2).width;
        ((Rect) ref screenRect1).height = ((Rect) ref screenRect2).height;
        break;
    }
    this.m_WindowRect = this.ToWindowRect(screenRect2);
    param.rect = this.ToWindowRect(screenRect1);
  }

  private enum State
  {
    Resize,
    SizeMove,
    Sizing,
  }
}
