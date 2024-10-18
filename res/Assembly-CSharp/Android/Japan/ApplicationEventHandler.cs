// Decompiled with JetBrains decompiler
// Type: ApplicationEventHandler
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;
using SRPG;
using System;
using UnityEngine;
using UnityEngine.Events;

public class ApplicationEventHandler : MonoBehaviour
{
  private Rect m_PrimaryWindowRect = new Rect(0.0f, 0.0f, 0.0f, 0.0f);
  private EnumBitArray<ApplicationEventHandler.State> m_EventFlag = new EnumBitArray<ApplicationEventHandler.State>();
  private bool m_IsQuiting;
  private EmbedWindowYesNo m_QuitWindow;
  private Rect m_WindowRect;
  private Vector2 m_FrameSize;

  public void OpenQuitWindow()
  {
    if (!((UnityEngine.Object) this.m_QuitWindow == (UnityEngine.Object) null) || this.m_IsQuiting)
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
    this.m_FrameSize.x = Mathf.Floor(this.m_WindowRect.size.x - clientRect.size.x);
    this.m_FrameSize.y = Mathf.Floor(this.m_WindowRect.size.y - clientRect.size.y);
    MonoSingleton<WindowProc>.Instance.AddWM_CloseListener(new UnityAction<WindowProc.WM_CloseEvent.Param>(this.OnWM_Close));
    MonoSingleton<WindowProc>.Instance.AddWM_MoveListener(new UnityAction<WindowProc.WM_MoveEvent.Param>(this.OnWM_Move));
    MonoSingleton<WindowProc>.Instance.AddWM_SizeListener(new UnityAction<WindowProc.WM_SizeEvent.Param>(this.OnWM_Size));
    MonoSingleton<WindowProc>.Instance.AddWM_SizingListener(new UnityAction<WindowProc.WM_SizingEvent.Param>(this.OnWM_Sizing));
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
    if ((double) monitorInfo.workAreaRect.width < (double) windowRect.width || (double) monitorInfo.workAreaRect.height < (double) windowRect.height)
    {
      Rect screenRect2 = this.ToScreenRect(monitorInfo.workAreaRect);
      float num1 = Mathf.Clamp(screenRect1.width, 480f, screenRect2.width);
      float num2 = Mathf.Clamp(screenRect1.height, 270f, screenRect2.height);
      float num3 = Mathf.Floor(num2 * 1.777778f);
      float num4 = Mathf.Floor(num1 / 1.777778f);
      if ((double) num1 <= (double) screenRect2.width && (double) num4 <= (double) screenRect2.height)
        num3 = num1;
      else
        num4 = num2;
      windowRect.width = num3;
      windowRect.height = num4;
    }
    windowRect.center = monitorInfo.workAreaRect.center;
    WindowsAPI.MoveWindow(hWnd, windowRect, true);
  }

  private Rect ToScreenRect(Rect value)
  {
    value.size -= this.m_FrameSize;
    return value;
  }

  private Rect ToWindowRect(Rect value)
  {
    value.size += this.m_FrameSize;
    return value;
  }

  private void OnWM_Close(WindowProc.WM_CloseEvent.Param param)
  {
    param.EnableSkipProc();
    this.OpenQuitWindow();
  }

  public void OnWM_Move(WindowProc.WM_MoveEvent.Param param)
  {
    this.m_WindowRect.x = param.pos.x;
    this.m_WindowRect.y = param.pos.y;
  }

  public void OnWM_Size(WindowProc.WM_SizeEvent.Param param)
  {
    if (param.eventType == WindowProc.WM_SizeEvent.EventType.Minimized || param.eventType == WindowProc.WM_SizeEvent.EventType.Maxshow || param.eventType == WindowProc.WM_SizeEvent.EventType.Maxhide)
      return;
    if (this.m_WindowRect.size == param.size + this.m_FrameSize)
    {
      this.m_EventFlag.Set(ApplicationEventHandler.State.Resize, false);
    }
    else
    {
      if (this.m_EventFlag.Get(ApplicationEventHandler.State.Resize))
        return;
      Rect windowRect1 = WindowsAPI.GetWindowRect(param.hWnd);
      windowRect1.size = param.size + this.m_FrameSize;
      Rect screenRect1 = this.ToScreenRect(windowRect1);
      Rect screenRect2 = this.ToScreenRect(this.m_WindowRect);
      WindowsAPI.MonitorInfoEx monitorInfo = WindowsAPI.GetMonitorInfo(param.hWnd);
      if (monitorInfo == null)
        return;
      Rect screenRect3 = this.ToScreenRect(monitorInfo.workAreaRect);
      if ((double) this.m_PrimaryWindowRect.width > 0.0 && (double) screenRect3.width > (double) this.m_PrimaryWindowRect.width)
        screenRect3.width = this.m_PrimaryWindowRect.width;
      if ((double) this.m_PrimaryWindowRect.height > 0.0 && (double) screenRect3.height > (double) this.m_PrimaryWindowRect.height)
        screenRect3.height = this.m_PrimaryWindowRect.height;
      float num1 = Mathf.Clamp(screenRect1.width, 480f, screenRect3.width);
      float num2 = Mathf.Clamp(screenRect1.height, 270f, screenRect3.height);
      float num3 = Mathf.Floor(num2 * 1.777778f);
      float num4 = Mathf.Floor(num1 / 1.777778f);
      if ((double) num1 <= (double) screenRect3.width && (double) num4 <= (double) screenRect3.height)
        num3 = num1;
      else
        num4 = num2;
      screenRect2.width = num3;
      screenRect2.height = num4;
      screenRect1.width = screenRect2.width;
      screenRect1.height = screenRect2.height;
      this.m_WindowRect = this.ToWindowRect(screenRect2);
      Rect windowRect2 = this.ToWindowRect(screenRect1);
      if (param.eventType == WindowProc.WM_SizeEvent.EventType.Maximized)
        windowRect2.center = monitorInfo.workAreaRect.center;
      if (screenRect1.size == param.size)
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
    if ((double) param.rect.width == 0.0 && (double) param.rect.height == 0.0)
      return;
    Rect screenRect1 = this.ToScreenRect(param.rect);
    Rect screenRect2 = this.ToScreenRect(this.m_WindowRect);
    WindowsAPI.MonitorInfoEx monitorInfo = WindowsAPI.GetMonitorInfo(param.hWnd);
    if (monitorInfo == null)
      return;
    Rect screenRect3 = this.ToScreenRect(monitorInfo.workAreaRect);
    if ((double) this.m_PrimaryWindowRect.width > 0.0 && (double) screenRect3.width > (double) this.m_PrimaryWindowRect.width)
      screenRect3.width = this.m_PrimaryWindowRect.width;
    if ((double) this.m_PrimaryWindowRect.height > 0.0 && (double) screenRect3.height > (double) this.m_PrimaryWindowRect.height)
      screenRect3.height = this.m_PrimaryWindowRect.height;
    float num1 = Mathf.Clamp(screenRect1.width, 480f, screenRect3.width);
    float num2 = Mathf.Clamp(screenRect1.height, 270f, screenRect3.height);
    float num3 = Mathf.Floor(num2 * 1.777778f);
    float num4 = Mathf.Floor(num1 / 1.777778f);
    if (param.eventType == WindowProc.WM_SizingEvent.EventType.TopLeft || param.eventType == WindowProc.WM_SizingEvent.EventType.TopRight || (param.eventType == WindowProc.WM_SizingEvent.EventType.BottomLeft || param.eventType == WindowProc.WM_SizingEvent.EventType.BottomRight))
    {
      if ((double) (Mathf.Max(this.m_WindowRect.width, param.rect.size.x) / Mathf.Min(this.m_WindowRect.width, param.rect.size.x)) < (double) (Mathf.Max(this.m_WindowRect.height, param.rect.size.y) / Mathf.Min(this.m_WindowRect.height, param.rect.size.y)))
        num3 = num1;
      else
        num4 = num2;
      if ((double) num3 > (double) screenRect3.width)
      {
        num3 = screenRect3.width;
        num4 = Mathf.Floor(num1 / 1.777778f);
      }
      else if ((double) num4 > (double) screenRect3.height)
      {
        num4 = screenRect3.height;
        num3 = Mathf.Floor(num4 * 1.777778f);
      }
    }
    else if (param.eventType == WindowProc.WM_SizingEvent.EventType.Top || param.eventType == WindowProc.WM_SizingEvent.EventType.Bottom)
    {
      if ((double) num3 > (double) screenRect3.width)
      {
        num3 = screenRect3.width;
        num4 = Mathf.Floor(num1 / 1.777778f);
      }
      else
        num4 = num2;
    }
    else if (param.eventType == WindowProc.WM_SizingEvent.EventType.Left || param.eventType == WindowProc.WM_SizingEvent.EventType.Right)
    {
      if ((double) num4 > (double) screenRect3.height)
      {
        num4 = screenRect3.height;
        num3 = Mathf.Floor(num4 * 1.777778f);
      }
      else
        num3 = num1;
    }
    switch (param.eventType)
    {
      case WindowProc.WM_SizingEvent.EventType.Left:
        screenRect2.xMin = screenRect1.xMax - num1;
        screenRect2.width = num3;
        screenRect2.height = num4;
        screenRect1.xMin = screenRect2.xMin;
        screenRect1.height = screenRect2.height;
        break;
      case WindowProc.WM_SizingEvent.EventType.Right:
        screenRect2.width = num3;
        screenRect2.height = num4;
        screenRect1.width = screenRect2.width;
        screenRect1.height = screenRect2.height;
        break;
      case WindowProc.WM_SizingEvent.EventType.Top:
        screenRect2.yMin = screenRect1.yMax - num2;
        screenRect2.width = num3;
        screenRect2.height = num4;
        screenRect1.width = screenRect2.width;
        screenRect1.yMin = screenRect2.yMin;
        break;
      case WindowProc.WM_SizingEvent.EventType.TopLeft:
        screenRect2.xMin = screenRect1.xMax - num3;
        screenRect2.yMin = screenRect1.yMax - num4;
        screenRect2.width = num3;
        screenRect2.height = num4;
        screenRect1.xMin = screenRect2.xMin;
        screenRect1.yMin = screenRect2.yMin;
        break;
      case WindowProc.WM_SizingEvent.EventType.TopRight:
        screenRect2.yMin = screenRect1.yMax - num4;
        screenRect2.width = num3;
        screenRect2.height = num4;
        screenRect1.yMin = screenRect2.yMin;
        screenRect1.width = screenRect2.width;
        break;
      case WindowProc.WM_SizingEvent.EventType.Bottom:
        screenRect2.width = num3;
        screenRect2.height = num4;
        screenRect1.width = screenRect2.width;
        screenRect1.height = screenRect2.height;
        break;
      case WindowProc.WM_SizingEvent.EventType.BottomLeft:
        screenRect2.xMin = screenRect1.xMax - num3;
        screenRect2.width = num3;
        screenRect2.height = num4;
        screenRect1.xMin = screenRect2.xMin;
        screenRect1.height = screenRect2.height;
        break;
      case WindowProc.WM_SizingEvent.EventType.BottomRight:
        screenRect2.width = num3;
        screenRect2.height = num4;
        screenRect1.width = screenRect2.width;
        screenRect1.height = screenRect2.height;
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
