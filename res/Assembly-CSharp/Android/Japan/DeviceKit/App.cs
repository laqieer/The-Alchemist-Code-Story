// Decompiled with JetBrains decompiler
// Type: DeviceKit.App
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using UnityEngine;

namespace DeviceKit
{
  public static class App
  {
    private static readonly string SECRETID_PATH = Application.persistentDataPath + "/secretKey";
    private static readonly string DEVICEID_PATH = Application.persistentDataPath + "/deviceId";
    private const uint CF_TEXT = 1;

    public static void SetAutoSleep(bool active)
    {
      App.devicekit_setAutoSleep(active);
    }

    public static string GetBundleIdentifier()
    {
      return App.devicekit_getBundleIdentifier();
    }

    public static string GetBundleVersion()
    {
      return App.devicekit_getBundleVersion();
    }

    public static bool OpenUrl(string url)
    {
      return App.devicekit_openUrl(url);
    }

    public static bool OpenStore(string appId)
    {
      return App.devicekit_openStore(appId);
    }

    public static bool LaunchMailer(string mailto, string subject, string body)
    {
      return App.devicekit_launchMailer(mailto, subject, body);
    }

    public static string GetClientId()
    {
      return App.devicekit_getClientId();
    }

    public static string GetIdfa()
    {
      return App.devicekit_getIdfa();
    }

    public static void SetClipboard(string text)
    {
      App.devicekit_setClipboard(text);
    }

    public static string GetClipboard()
    {
      IntPtr intptr;
      App.devicekit_getClipboard(out intptr);
      return MarshalSupport.ToString(intptr);
    }

    private static void devicekit_setAutoSleep(bool active)
    {
      if (active)
        Screen.sleepTimeout = -1;
      else
        Screen.sleepTimeout = -2;
    }

    private static string devicekit_getBundleIdentifier()
    {
      return "jp.co.gu3.standalone.build";
    }

    private static string devicekit_getBundleVersion()
    {
      return "0.0.0";
    }

    private static bool devicekit_openUrl(string url)
    {
      Process.Start(url);
      return true;
    }

    private static bool devicekit_openStore(string url)
    {
      Process.Start(url);
      return true;
    }

    private static bool devicekit_launchMailer(string mailto, string subject, string body)
    {
      Process.Start(string.Format("mailto:{0}?subject={1}&body={2}", (object) mailto, (object) subject, (object) body));
      return true;
    }

    private static string devicekit_getClientId()
    {
      return "CLIENT_ID:unity_editor";
    }

    public static void GetAuthKeys(out string secretKey, out string deviceId, string suffix)
    {
      secretKey = deviceId = (string) null;
      string path1 = App.SECRETID_PATH + (suffix == null ? string.Empty : "_" + suffix);
      string path2 = App.DEVICEID_PATH + (suffix == null ? string.Empty : "_" + suffix);
      if (File.Exists(path1))
        secretKey = File.ReadAllText(path1, Encoding.UTF8);
      if (File.Exists(path2))
        deviceId = File.ReadAllText(path2, Encoding.UTF8);
      if (secretKey != null)
        return;
      secretKey = Guid.NewGuid().ToString();
    }

    public static void SetAuthKeys(string secretKey, string deviceId, string suffix)
    {
      string path1 = App.SECRETID_PATH + (suffix == null ? string.Empty : "_" + suffix);
      string path2 = App.DEVICEID_PATH + (suffix == null ? string.Empty : "_" + suffix);
      if (secretKey != null)
        File.WriteAllText(path1, secretKey, Encoding.UTF8);
      if (deviceId == null)
        return;
      File.WriteAllText(path2, deviceId, Encoding.UTF8);
    }

    public static void DeleteAuthKeys(string suffix)
    {
      string path1 = App.SECRETID_PATH + (suffix == null ? string.Empty : "_" + suffix);
      string path2 = App.DEVICEID_PATH + (suffix == null ? string.Empty : "_" + suffix);
      if (File.Exists(path1))
        File.Delete(path1);
      if (!File.Exists(path2))
        return;
      File.Delete(path2);
    }

    private static string devicekit_getIdfa()
    {
      return "IDFA:unity_editor";
    }

    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool OpenClipboard(IntPtr hWndNewOwner);

    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool CloseClipboard();

    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool EmptyClipboard();

    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    private static extern IntPtr SetClipboardData(uint uFormat, IntPtr hMem);

    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    private static extern IntPtr GetClipboardData(uint uFormat);

    private static void devicekit_setClipboard(string text)
    {
      if (string.IsNullOrEmpty(text))
        return;
      byte[] bytes = Encoding.Default.GetBytes(text);
      IntPtr num = Marshal.AllocHGlobal(bytes.Length + 1);
      Marshal.Copy(bytes, 0, num, bytes.Length);
      bytes[0] = (byte) 0;
      Marshal.Copy(bytes, 0, (IntPtr) ((long) num + (long) bytes.Length), 1);
      App.OpenClipboard(IntPtr.Zero);
      App.EmptyClipboard();
      if (App.SetClipboardData(1U, num) == IntPtr.Zero)
        Marshal.FreeHGlobal(num);
      App.CloseClipboard();
    }

    private static void devicekit_getClipboard(out IntPtr intptr)
    {
      intptr = IntPtr.Zero;
      App.OpenClipboard(IntPtr.Zero);
      IntPtr clipboardData = App.GetClipboardData(1U);
      if (clipboardData != IntPtr.Zero)
        intptr = clipboardData;
      App.CloseClipboard();
    }

    public static class Hardkey
    {
      public static void Init(GameObject serviceNode = null)
      {
        HardkeyHandler.Init(serviceNode);
      }

      public static void SetListener(IHardkeyListener listener)
      {
        HardkeyHandler.SetListener(listener);
      }
    }
  }
}
