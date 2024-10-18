// Decompiled with JetBrains decompiler
// Type: WindowProc
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Events;

#nullable disable
public class WindowProc : MonoSingleton<WindowProc>
{
  public const int WM_CLOSE = 16;
  public const int WM_MOVE = 3;
  public const int WM_SIZE = 5;
  public const int WM_SIZING = 532;
  public const int WM_ENTERSIZEMOVE = 561;
  public const int WM_EXITSIZEMOVE = 562;
  private HandleRef hMainWindow;
  private IntPtr oldWndProcPtr;
  private IntPtr newWndProcPtr;
  private WindowsAPI.WndProcDelegate newWndProc;
  private WindowProc.WM_CloseEvent onWM_Close = new WindowProc.WM_CloseEvent();
  private WindowProc.WM_MoveEvent onWM_Move = new WindowProc.WM_MoveEvent();
  private WindowProc.WM_SizeEvent onWM_Size = new WindowProc.WM_SizeEvent();
  private WindowProc.WM_SizingEvent onWM_Sizing = new WindowProc.WM_SizingEvent();
  private WindowProc.WM_EnterSizeMoveEvent onWM_EnterSizeMove = new WindowProc.WM_EnterSizeMoveEvent();
  private WindowProc.WM_ExitSizeMoveEvent onWM_ExitSizeMove = new WindowProc.WM_ExitSizeMoveEvent();

  private void _RegisterWindowProc()
  {
    this.hMainWindow = new HandleRef((object) null, WindowsAPI.FindWindow((string) null, Application.productName));
    if (this.hMainWindow.Handle == IntPtr.Zero)
      return;
    this.newWndProc = new WindowsAPI.WndProcDelegate(this.wndProc);
    this.newWndProcPtr = Marshal.GetFunctionPointerForDelegate((Delegate) this.newWndProc);
    if (this.newWndProcPtr == IntPtr.Zero)
      return;
    this.oldWndProcPtr = WindowsAPI.SetWindowLongPtr(this.hMainWindow, -4, this.newWndProcPtr);
    if (!(this.oldWndProcPtr == IntPtr.Zero))
      ;
  }

  private void _ReleaseWindowProc()
  {
    if (this.hMainWindow.Handle != IntPtr.Zero)
      WindowsAPI.SetWindowLongPtr(this.hMainWindow, -4, this.oldWndProcPtr);
    this.hMainWindow = new HandleRef((object) null, IntPtr.Zero);
    this.oldWndProcPtr = IntPtr.Zero;
    this.newWndProcPtr = IntPtr.Zero;
    this.newWndProc = (WindowsAPI.WndProcDelegate) null;
  }

  protected override void Initialize()
  {
    base.Initialize();
    this._RegisterWindowProc();
    Object.DontDestroyOnLoad((Object) this);
  }

  protected override void Release()
  {
    base.Release();
    this._ReleaseWindowProc();
  }

  private IntPtr wndProc(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam)
  {
    if (!(this.hMainWindow.Handle == hWnd) || !(this.oldWndProcPtr != IntPtr.Zero))
      return WindowsAPI.DefWindowProc(hWnd, msg, wParam, lParam);
    WindowProc.WindowEventParamBase windowEventParamBase = (WindowProc.WindowEventParamBase) null;
    switch (msg)
    {
      case 3:
        short num1 = this.LoWord(lParam.ToInt64());
        short num2 = this.HiWord(lParam.ToInt64());
        windowEventParamBase = (WindowProc.WindowEventParamBase) new WindowProc.WM_MoveEvent.Param(hWnd);
        WindowProc.WM_MoveEvent.Param obj1 = (WindowProc.WM_MoveEvent.Param) windowEventParamBase;
        obj1.pos = new Vector2((float) num1, (float) num2);
        this.onWM_Move.Invoke(obj1);
        break;
      case 5:
        short num3 = this.LoWord(lParam.ToInt64());
        short num4 = this.HiWord(lParam.ToInt64());
        windowEventParamBase = (WindowProc.WindowEventParamBase) new WindowProc.WM_SizeEvent.Param(hWnd);
        WindowProc.WM_SizeEvent.Param obj2 = (WindowProc.WM_SizeEvent.Param) windowEventParamBase;
        obj2.hWnd = hWnd;
        obj2.eventType = (WindowProc.WM_SizeEvent.EventType) wParam.ToInt64();
        obj2.size = new Vector2((float) num3, (float) num4);
        this.onWM_Size.Invoke(obj2);
        break;
      case 16:
        windowEventParamBase = (WindowProc.WindowEventParamBase) new WindowProc.WM_CloseEvent.Param(hWnd);
        this.onWM_Close.Invoke((WindowProc.WM_CloseEvent.Param) windowEventParamBase);
        break;
      case 532:
        WindowsAPI.RECT structure = (WindowsAPI.RECT) Marshal.PtrToStructure(lParam, typeof (WindowsAPI.RECT));
        windowEventParamBase = (WindowProc.WindowEventParamBase) new WindowProc.WM_SizingEvent.Param(hWnd);
        WindowProc.WM_SizingEvent.Param obj3 = (WindowProc.WM_SizingEvent.Param) windowEventParamBase;
        obj3.eventType = (WindowProc.WM_SizingEvent.EventType) wParam.ToInt32();
        obj3.rect = new Rect();
        ((Rect) ref obj3.rect).xMin = (float) structure.left;
        ((Rect) ref obj3.rect).yMin = (float) structure.top;
        ((Rect) ref obj3.rect).xMax = (float) structure.right;
        ((Rect) ref obj3.rect).yMax = (float) structure.bottom;
        this.onWM_Sizing.Invoke(obj3);
        structure.left = (int) ((Rect) ref obj3.rect).xMin;
        structure.top = (int) ((Rect) ref obj3.rect).yMin;
        structure.right = (int) ((Rect) ref obj3.rect).xMax;
        structure.bottom = (int) ((Rect) ref obj3.rect).yMax;
        Marshal.StructureToPtr((object) structure, lParam, false);
        break;
      case 561:
        windowEventParamBase = (WindowProc.WindowEventParamBase) new WindowProc.WM_EnterSizeMoveEvent.Param(hWnd);
        this.onWM_EnterSizeMove.Invoke((WindowProc.WM_EnterSizeMoveEvent.Param) windowEventParamBase);
        break;
      case 562:
        windowEventParamBase = (WindowProc.WindowEventParamBase) new WindowProc.WM_ExitSizeMoveEvent.Param(hWnd);
        this.onWM_ExitSizeMove.Invoke((WindowProc.WM_ExitSizeMoveEvent.Param) windowEventParamBase);
        break;
    }
    return windowEventParamBase != null && windowEventParamBase.skipProc ? IntPtr.Zero : WindowsAPI.CallWindowProc(this.oldWndProcPtr, hWnd, msg, wParam, lParam);
  }

  protected short LoWord(long input) => (short) ((int) input & (int) ushort.MaxValue);

  protected short HiWord(long input) => (short) ((int) input >> 16);

  public void AddWM_CloseListener(
    UnityAction<WindowProc.WM_CloseEvent.Param> callback)
  {
    this.onWM_Close.AddListener(callback);
  }

  public void RemoveWM_CloseListener(
    UnityAction<WindowProc.WM_CloseEvent.Param> callback)
  {
    this.onWM_Close.RemoveListener(callback);
  }

  public void AddWM_MoveListener(
    UnityAction<WindowProc.WM_MoveEvent.Param> callback)
  {
    this.onWM_Move.AddListener(callback);
  }

  public void RemoveWM_MoveListener(
    UnityAction<WindowProc.WM_MoveEvent.Param> callback)
  {
    this.onWM_Move.RemoveListener(callback);
  }

  public void AddWM_SizeListener(
    UnityAction<WindowProc.WM_SizeEvent.Param> callback)
  {
    this.onWM_Size.AddListener(callback);
  }

  public void RemoveWM_SizeListener(
    UnityAction<WindowProc.WM_SizeEvent.Param> callback)
  {
    this.onWM_Size.RemoveListener(callback);
  }

  public void AddWM_SizingListener(
    UnityAction<WindowProc.WM_SizingEvent.Param> callback)
  {
    this.onWM_Sizing.AddListener(callback);
  }

  public void RemoveWM_SizingListener(
    UnityAction<WindowProc.WM_SizingEvent.Param> callback)
  {
    this.onWM_Sizing.RemoveListener(callback);
  }

  public void AddWM_EnterSizeMoveListener(
    UnityAction<WindowProc.WM_EnterSizeMoveEvent.Param> callback)
  {
    this.onWM_EnterSizeMove.AddListener(callback);
  }

  public void RemoveWM_EnterSizeMoveListener(
    UnityAction<WindowProc.WM_EnterSizeMoveEvent.Param> callback)
  {
    this.onWM_EnterSizeMove.RemoveListener(callback);
  }

  public void AddWM_ExitSizeMoveListener(
    UnityAction<WindowProc.WM_ExitSizeMoveEvent.Param> callback)
  {
    this.onWM_ExitSizeMove.AddListener(callback);
  }

  public void RemoveWM_ExitSizeMoveListener(
    UnityAction<WindowProc.WM_ExitSizeMoveEvent.Param> callback)
  {
    this.onWM_ExitSizeMove.RemoveListener(callback);
  }

  public abstract class WindowEventParamBase
  {
    public IntPtr hWnd;
    public bool skipProc;

    public WindowEventParamBase(IntPtr hWnd) => this.hWnd = hWnd;

    public void EnableSkipProc() => this.skipProc = true;

    public void DisableSkipProc() => this.skipProc = false;
  }

  public class WM_CloseEvent : UnityEvent<WindowProc.WM_CloseEvent.Param>
  {
    public class Param : WindowProc.WindowEventParamBase
    {
      public Param(IntPtr hWnd)
        : base(hWnd)
      {
      }
    }
  }

  public class WM_MoveEvent : UnityEvent<WindowProc.WM_MoveEvent.Param>
  {
    public class Param : WindowProc.WindowEventParamBase
    {
      public Vector2 pos;

      public Param(IntPtr hWnd)
        : base(hWnd)
      {
      }
    }
  }

  public class WM_SizeEvent : UnityEvent<WindowProc.WM_SizeEvent.Param>
  {
    public enum EventType
    {
      Restored,
      Minimized,
      Maximized,
      Maxshow,
      Maxhide,
    }

    public class Param : WindowProc.WindowEventParamBase
    {
      public WindowProc.WM_SizeEvent.EventType eventType;
      public Vector2 size;

      public Param(IntPtr hWnd)
        : base(hWnd)
      {
      }
    }
  }

  public class WM_SizingEvent : UnityEvent<WindowProc.WM_SizingEvent.Param>
  {
    public enum EventType
    {
      Left = 1,
      Right = 2,
      Top = 3,
      TopLeft = 4,
      TopRight = 5,
      Bottom = 6,
      BottomLeft = 7,
      BottomRight = 8,
    }

    public class Param : WindowProc.WindowEventParamBase
    {
      public WindowProc.WM_SizingEvent.EventType eventType;
      public Rect rect;

      public Param(IntPtr hWnd)
        : base(hWnd)
      {
      }
    }
  }

  public class WM_EnterSizeMoveEvent : UnityEvent<WindowProc.WM_EnterSizeMoveEvent.Param>
  {
    public class Param : WindowProc.WindowEventParamBase
    {
      public Param(IntPtr hWnd)
        : base(hWnd)
      {
      }
    }
  }

  public class WM_ExitSizeMoveEvent : UnityEvent<WindowProc.WM_ExitSizeMoveEvent.Param>
  {
    public class Param : WindowProc.WindowEventParamBase
    {
      public Param(IntPtr hWnd)
        : base(hWnd)
      {
      }
    }
  }
}
