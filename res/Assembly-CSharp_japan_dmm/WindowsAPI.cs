// Decompiled with JetBrains decompiler
// Type: WindowsAPI
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;
using System.Runtime.InteropServices;
using UnityEngine;

#nullable disable
public class WindowsAPI
{
  public static readonly int MONITOR_DEFAULTTONULL;
  public static readonly int MONITOR_DEFAULTTOPRIMARY = 1;
  public static readonly int MONITOR_DEFAULTTONEAREST = 2;

  [DllImport("user32.dll", CharSet = CharSet.Auto)]
  public static extern IntPtr FindWindow(string className, string windowName);

  [DllImport("user32.dll", EntryPoint = "SetWindowLong")]
  public static extern int SetWindowLong32(HandleRef hWnd, int nIndex, int dwNewLong);

  [DllImport("user32.dll", EntryPoint = "SetWindowLongPtr")]
  public static extern IntPtr SetWindowLongPtr64(HandleRef hWnd, int nIndex, IntPtr dwNewLong);

  [DllImport("user32.dll", EntryPoint = "DefWindowProcA")]
  public static extern IntPtr DefWindowProc(IntPtr hWnd, uint wMsg, IntPtr wParam, IntPtr lParam);

  [DllImport("user32.dll", CharSet = CharSet.Auto)]
  public static extern IntPtr CallWindowProc(
    IntPtr lpPrevWndFunc,
    IntPtr hWnd,
    uint wMsg,
    IntPtr wParam,
    IntPtr lParam);

  [DllImport("user32.dll", CharSet = CharSet.Auto)]
  private static extern int GetWindowRect(IntPtr hWnd, out WindowsAPI.RECT rect);

  [DllImport("user32.dll", CharSet = CharSet.Auto)]
  private static extern bool GetClientRect(IntPtr hWnd, out WindowsAPI.RECT rect);

  [DllImport("user32.dll", CharSet = CharSet.Auto)]
  private static extern int MoveWindow(
    IntPtr hWnd,
    int x,
    int y,
    int nWidth,
    int nHeight,
    int bRepaint);

  [DllImport("user32.dll", CharSet = CharSet.Auto)]
  public static extern IntPtr MonitorFromWindow(IntPtr hwnd, IntPtr dwFlags);

  [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
  public static extern bool GetMonitorInfo(
    IntPtr hMonitor,
    ref WindowsAPI.MONITORINFOEX moniterInfoEX);

  public static int MoveWindow(IntPtr hWnd, Rect rect, bool repaint)
  {
    return WindowsAPI.MoveWindow(hWnd, (int) ((Rect) ref rect).x, (int) ((Rect) ref rect).y, (int) ((Rect) ref rect).width, (int) ((Rect) ref rect).height, !repaint ? 0 : 1);
  }

  public static WindowsAPI.MonitorInfoEx GetMonitorInfo(IntPtr hWnd)
  {
    IntPtr hMonitor = WindowsAPI.MonitorFromWindow(hWnd, new IntPtr(WindowsAPI.MONITOR_DEFAULTTONEAREST));
    WindowsAPI.MONITORINFOEX moniterInfoEX = new WindowsAPI.MONITORINFOEX();
    moniterInfoEX.cbSize = Marshal.SizeOf((object) moniterInfoEX);
    WindowsAPI.GetMonitorInfo(hMonitor, ref moniterInfoEX);
    WindowsAPI.MonitorInfoEx monitorInfo = new WindowsAPI.MonitorInfoEx();
    monitorInfo.workAreaRect = new Rect();
    ((Rect) ref monitorInfo.workAreaRect).xMin = (float) moniterInfoEX.rcWork.left;
    ((Rect) ref monitorInfo.workAreaRect).xMax = (float) moniterInfoEX.rcWork.right;
    ((Rect) ref monitorInfo.workAreaRect).yMin = (float) moniterInfoEX.rcWork.top;
    ((Rect) ref monitorInfo.workAreaRect).yMax = (float) moniterInfoEX.rcWork.bottom;
    monitorInfo.monitorRect = new Rect();
    ((Rect) ref monitorInfo.monitorRect).xMin = (float) moniterInfoEX.rcMonitor.left;
    ((Rect) ref monitorInfo.monitorRect).xMax = (float) moniterInfoEX.rcMonitor.right;
    ((Rect) ref monitorInfo.monitorRect).yMin = (float) moniterInfoEX.rcMonitor.top;
    ((Rect) ref monitorInfo.monitorRect).yMax = (float) moniterInfoEX.rcMonitor.bottom;
    monitorInfo.Device = moniterInfoEX.szDevice;
    return monitorInfo;
  }

  public static Rect GetWindowRect(IntPtr hWnd)
  {
    WindowsAPI.RECT rect = new WindowsAPI.RECT();
    WindowsAPI.GetWindowRect(hWnd, out rect);
    Rect windowRect = new Rect();
    ((Rect) ref windowRect).xMin = (float) rect.left;
    ((Rect) ref windowRect).xMax = (float) rect.right;
    ((Rect) ref windowRect).yMin = (float) rect.top;
    ((Rect) ref windowRect).yMax = (float) rect.bottom;
    return windowRect;
  }

  public static Rect GetClientRect(IntPtr hWnd)
  {
    WindowsAPI.RECT rect = new WindowsAPI.RECT();
    WindowsAPI.GetClientRect(hWnd, out rect);
    Rect clientRect = new Rect();
    ((Rect) ref clientRect).xMin = (float) rect.left;
    ((Rect) ref clientRect).xMax = (float) rect.right;
    ((Rect) ref clientRect).yMin = (float) rect.top;
    ((Rect) ref clientRect).yMax = (float) rect.bottom;
    return clientRect;
  }

  public static IntPtr SetWindowLongPtr(HandleRef hWnd, int nIndex, IntPtr dwNewLong)
  {
    return IntPtr.Size == 8 ? WindowsAPI.SetWindowLongPtr64(hWnd, nIndex, dwNewLong) : new IntPtr(WindowsAPI.SetWindowLong32(hWnd, nIndex, dwNewLong.ToInt32()));
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
