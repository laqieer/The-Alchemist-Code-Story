// Decompiled with JetBrains decompiler
// Type: DeviceKit.Sys
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using SRPG;
using System;
using System.Runtime.InteropServices;
using System.Text;

namespace DeviceKit
{
  public static class Sys
  {
    private const uint LOCALE_USER_DEFAULT = 1024;
    private const uint LOCALE_SISO639LANGNAME = 89;
    private const uint LOCALE_SISO3166CTRYNAME = 90;

    public static string GetUserAgent()
    {
      return Sys.devicekit_getUserAgent();
    }

    public static string GetSystemProxyURL()
    {
      return Sys.devicekit_getSystemProxyURL();
    }

    public static string GetLanguageLocale()
    {
      return Sys.devicekit_getLanguageLocale();
    }

    public static ulong GetAvailableMemoryBytes()
    {
      return Sys.devicekit_getAvailableMemoryBytes();
    }

    public static ulong GetAvailableStorageBytes()
    {
      return Sys.GetAvailableStorageBytes(AppPath.assetCachePath);
    }

    public static ulong GetAvailableStorageBytes(string localPath)
    {
      ulong lpFreeBytesAvailable;
      ulong lpTotalNumberOfBytes;
      ulong lpTotalNumberOfFreeBytes;
      Sys.GetDiskFreeSpaceEx(localPath, out lpFreeBytesAvailable, out lpTotalNumberOfBytes, out lpTotalNumberOfFreeBytes);
      return lpFreeBytesAvailable;
    }

    [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool GetDiskFreeSpaceEx(string lpDirectoryName, out ulong lpFreeBytesAvailable, out ulong lpTotalNumberOfBytes, out ulong lpTotalNumberOfFreeBytes);

    [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool GlobalMemoryStatusEx(ref Sys.MemoryStatusEx lpBuffer);

    [DllImport("Kernel32.dll", CharSet = CharSet.Auto)]
    private static extern int GetLocaleInfoA(uint Locale, uint LCType, IntPtr lpLCData, int cchData);

    private static string devicekit_getUserAgent()
    {
      return (string) null;
    }

    private static string devicekit_getSystemProxyURL()
    {
      return (string) null;
    }

    private static string devicekit_getLanguageLocale()
    {
      byte[] numArray = new byte[16];
      IntPtr num = Marshal.AllocCoTaskMem(numArray.Length);
      int localeInfoA1 = Sys.GetLocaleInfoA(1024U, 89U, num, numArray.Length);
      Marshal.Copy(num, numArray, 0, localeInfoA1 - 1);
      string str1 = Encoding.ASCII.GetString(numArray, 0, localeInfoA1 - 1);
      int localeInfoA2 = Sys.GetLocaleInfoA(1024U, 90U, num, numArray.Length);
      Marshal.Copy(num, numArray, 0, localeInfoA2 - 1);
      string str2 = Encoding.ASCII.GetString(numArray, 0, localeInfoA2 - 1);
      Marshal.FreeCoTaskMem(num);
      return string.Format("{0}.{1}-{2}", (object) str1, (object) str1, (object) str2);
    }

    private static ulong devicekit_getAvailableStorageBytes()
    {
      return 0;
    }

    private static ulong devicekit_getAvailableMemoryBytes()
    {
      Sys.MemoryStatusEx lpBuffer = new Sys.MemoryStatusEx();
      lpBuffer.dwLength = (uint) Marshal.SizeOf(typeof (Sys.MemoryStatusEx));
      Sys.GlobalMemoryStatusEx(ref lpBuffer);
      return lpBuffer.ullAvailPhys;
    }

    private struct MemoryStatusEx
    {
      public uint dwLength;
      public uint dwMemoryLoad;
      public ulong ullTotalPhys;
      public ulong ullAvailPhys;
      public ulong ullTotalPageFile;
      public ulong ullAvailPageFile;
      public ulong ullTotalVirtual;
      public ulong ullAvailVirtual;
      public ulong ullAvailExtendedVirtual;
    }
  }
}
