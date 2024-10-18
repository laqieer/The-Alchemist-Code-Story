// Decompiled with JetBrains decompiler
// Type: WindowsAPI
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using System;
using System.Runtime.InteropServices;
using UnityEngine;

public class WindowsAPI
{
  public static readonly int MONITOR_DEFAULTTOPRIMARY = 1;
  public static readonly int MONITOR_DEFAULTTONEAREST = 2;
  public static readonly int MONITOR_DEFAULTTONULL;

  [DllImport("user32.dll", CharSet = CharSet.Auto)]
  public static extern IntPtr FindWindow(string className, string windowName);

  [DllImport("user32.dll", EntryPoint = "SetWindowLong")]
  public static extern int SetWindowLong32(HandleRef hWnd, int nIndex, int dwNewLong);

  [DllImport("user32.dll", EntryPoint = "SetWindowLongPtr")]
  public static extern IntPtr SetWindowLongPtr64(HandleRef hWnd, int nIndex, IntPtr dwNewLong);

  [DllImport("user32.dll", EntryPoint = "DefWindowProcA")]
  public static extern IntPtr DefWindowProc(IntPtr hWnd, uint wMsg, IntPtr wParam, IntPtr lParam);

  [DllImport("user32.dll", CharSet = CharSet.Auto)]
  public static extern IntPtr CallWindowProc(IntPtr lpPrevWndFunc, IntPtr hWnd, uint wMsg, IntPtr wParam, IntPtr lParam);

  [DllImport("user32.dll", CharSet = CharSet.Auto)]
  private static extern int GetWindowRect(IntPtr hWnd, out WindowsAPI.RECT rect);

  [DllImport("user32.dll", CharSet = CharSet.Auto)]
  private static extern bool GetClientRect(IntPtr hWnd, out WindowsAPI.RECT rect);

  [DllImport("user32.dll", CharSet = CharSet.Auto)]
  private static extern int MoveWindow(IntPtr hWnd, int x, int y, int nWidth, int nHeight, int bRepaint);

  [DllImport("user32.dll", CharSet = CharSet.Auto)]
  public static extern IntPtr MonitorFromWindow(IntPtr hwnd, IntPtr dwFlags);

  [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
  public static extern bool GetMonitorInfo(IntPtr hMonitor, ref WindowsAPI.MONITORINFOEX moniterInfoEX);

  public static int MoveWindow(IntPtr hWnd, Rect rect, bool repaint)
  {
    return WindowsAPI.MoveWindow(hWnd, (int) rect.x, (int) rect.y, (int) rect.width, (int) rect.height, !repaint ? 0 : 1);
  }

  public static WindowsAPI.MonitorInfoEx GetMonitorInfo(IntPtr hWnd)
  {
    IntPtr hMonitor = WindowsAPI.MonitorFromWindow(hWnd, new IntPtr(WindowsAPI.MONITOR_DEFAULTTONEAREST));
    WindowsAPI.MONITORINFOEX moniterInfoEX = new WindowsAPI.MONITORINFOEX();
    moniterInfoEX.cbSize = Marshal.SizeOf((object) moniterInfoEX);
    WindowsAPI.GetMonitorInfo(hMonitor, ref moniterInfoEX);
    WindowsAPI.MonitorInfoEx monitorInfoEx = new WindowsAPI.MonitorInfoEx()
    {
      workAreaRect = new Rect()
    };
    monitorInfoEx.workAreaRect.xMin = (float) moniterInfoEX.rcWork.left;
    monitorInfoEx.workAreaRect.xMax = (float) moniterInfoEX.rcWork.right;
    monitorInfoEx.workAreaRect.yMin = (float) moniterInfoEX.rcWork.top;
    monitorInfoEx.workAreaRect.yMax = (float) moniterInfoEX.rcWork.bottom;
    monitorInfoEx.monitorRect = new Rect();
    monitorInfoEx.monitorRect.xMin = (float) moniterInfoEX.rcMonitor.left;
    monitorInfoEx.monitorRect.xMax = (float) moniterInfoEX.rcMonitor.right;
    monitorInfoEx.monitorRect.yMin = (float) moniterInfoEX.rcMonitor.top;
    monitorInfoEx.monitorRect.yMax = (float) moniterInfoEX.rcMonitor.bottom;
    monitorInfoEx.Device = moniterInfoEX.szDevice;
    return monitorInfoEx;
  }

  public static Rect GetWindowRect(IntPtr hWnd)
  {
    WindowsAPI.RECT rect = new WindowsAPI.RECT();
    WindowsAPI.GetWindowRect(hWnd, out rect);
    return new Rect()
    {
      xMin = (float) rect.left,
      xMax = (float) rect.right,
      yMin = (float) rect.top,
      yMax = (float) rect.bottom
    };
  }

  public static Rect GetClientRect(IntPtr hWnd)
  {
    WindowsAPI.RECT rect = new WindowsAPI.RECT();
    WindowsAPI.GetClientRect(hWnd, out rect);
    return new Rect()
    {
      xMin = (float) rect.left,
      xMax = (float) rect.right,
      yMin = (float) rect.top,
      yMax = (float) rect.bottom
    };
  }

  public static IntPtr SetWindowLongPtr(HandleRef hWnd, int nIndex, IntPtr dwNewLong)
  {
    if (IntPtr.Size == 8)
      return WindowsAPI.SetWindowLongPtr64(hWnd, nIndex, dwNewLong);
    return new IntPtr(WindowsAPI.SetWindowLong32(hWnd, nIndex, dwNewLong.ToInt32()));
  }

  public delegate IntPtr WndProcDelegate(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam);

  public struct RECT
  {
    public int left;
    public int top;
    public int right;
    public int bottom;
  }

  [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
  public struct MONITORINFOEX
  {
    public int cbSize;
    public WindowsAPI.RECT rcMonitor;
    public WindowsAPI.RECT rcWork;
    public uint dwFlags;
    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
    public string szDevice;
  }

  public class MonitorInfoEx
  {
    public Rect monitorRect;
    public Rect workAreaRect;
    public int flag;
    public string Device;
  }
}
