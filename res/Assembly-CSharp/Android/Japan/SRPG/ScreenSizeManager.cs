﻿// Decompiled with JetBrains decompiler
// Type: SRPG.ScreenSizeManager
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using System;
using System.Runtime.InteropServices;
using UnityEngine;

namespace SRPG
{
  public class ScreenSizeManager : MonoBehaviour
  {
    private IntPtr window_handle;
    private ScreenSizeManager.MYRECT window_rect;
    private ScreenSizeManager.MYRECT client_rect;
    private int current_client_width;
    private int current_client_height;
    private int next_client_width;
    private int next_client_height;
    private int frame_width;
    private int frame_height;

    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    private static extern IntPtr FindWindow(string className, string windowName);

    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    private static extern bool IsZoomed(IntPtr hWnd);

    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    private static extern int MoveWindow(IntPtr hWnd, int x, int y, int nWidth, int nHeight, int bRepaint);

    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    private static extern int GetWindowRect(IntPtr hWnd, out ScreenSizeManager.MYRECT rect);

    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    private static extern bool GetClientRect(IntPtr hWnd, out ScreenSizeManager.MYRECT rect);

    private void Awake()
    {
      this.window_handle = ScreenSizeManager.FindWindow((string) null, Application.productName);
      ScreenSizeManager.GetClientRect(this.window_handle, out this.client_rect);
      this.next_client_width = this.current_client_width = this.client_rect.right - this.client_rect.left;
      this.next_client_height = this.current_client_height = this.client_rect.bottom - this.client_rect.top;
      ScreenSizeManager.GetWindowRect(this.window_handle, out this.window_rect);
      int num1 = this.window_rect.right - this.window_rect.left;
      int num2 = this.window_rect.bottom - this.window_rect.top;
      this.frame_width = num1 - this.current_client_width;
      this.frame_height = num2 - this.current_client_height;
    }

    private void LateUpdate()
    {
      ScreenSizeManager.GetClientRect(this.window_handle, out this.client_rect);
      this.next_client_width = this.client_rect.right - this.client_rect.left;
      this.next_client_height = this.client_rect.bottom - this.client_rect.top;
      if (this.current_client_width != this.next_client_width && this.current_client_height != this.next_client_height)
      {
        if (Mathf.Abs(this.next_client_width - this.current_client_width) >= Mathf.Abs(this.next_client_height - this.current_client_height))
          this.EditWindowsSizeW(this.next_client_width, true);
        else
          this.EditWindowSizeH(this.next_client_height, true);
      }
      else if (this.current_client_width != this.next_client_width)
      {
        this.EditWindowsSizeW(this.next_client_width, true);
      }
      else
      {
        if (this.current_client_height == this.next_client_height)
          return;
        this.EditWindowSizeH(this.next_client_height, true);
      }
    }

    private void EditWindowsSizeW(int _new_window_width, bool _size_check = true)
    {
      if (ScreenSizeManager.IsZoomed(this.window_handle))
      {
        int _width = Mathf.Max(_new_window_width, 480);
        int _height = (int) ((double) _width / 480.0 * 270.0);
        this.Resize(_width, _height);
      }
      else
      {
        int _width = Mathf.Clamp(_new_window_width, 480, Screen.currentResolution.width - this.frame_width);
        int num = (int) ((double) _width / 480.0 * 270.0);
        if (_size_check && num > Screen.currentResolution.height - this.frame_height)
          this.EditWindowSizeH(num, false);
        else
          this.Resize(_width, num);
      }
    }

    private void EditWindowSizeH(int _new_window_height, bool _size_check = true)
    {
      if (ScreenSizeManager.IsZoomed(this.window_handle))
      {
        int _height = Mathf.Max(_new_window_height, 270);
        this.Resize((int) ((double) _height / 270.0 * 480.0), _height);
      }
      else
      {
        int _height = Mathf.Clamp(_new_window_height, 270, Screen.currentResolution.height - this.frame_height);
        int num = (int) ((double) _height / 270.0 * 480.0);
        if (_size_check && num > Screen.currentResolution.width - this.frame_width)
          this.EditWindowsSizeW(num, false);
        else
          this.Resize(num, _height);
      }
    }

    private void Resize(int _width, int _height)
    {
      this.current_client_width = _width;
      this.current_client_height = _height;
      ScreenSizeManager.GetWindowRect(this.window_handle, out this.window_rect);
      ScreenSizeManager.MoveWindow(this.window_handle, this.window_rect.left, this.window_rect.top, _width + this.frame_width, _height + this.frame_height, 1);
    }

    private struct MYRECT
    {
      public int left;
      public int top;
      public int right;
      public int bottom;
    }
  }
}
