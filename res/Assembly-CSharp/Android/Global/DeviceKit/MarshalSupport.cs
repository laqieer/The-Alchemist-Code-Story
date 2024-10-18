// Decompiled with JetBrains decompiler
// Type: DeviceKit.MarshalSupport
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using System;
using System.Runtime.InteropServices;
using System.Text;

namespace DeviceKit
{
  internal static class MarshalSupport
  {
    [DllImport("devicekit")]
    private static extern void devicekit_get_rawdata_from_string(IntPtr intptr, out IntPtr data, out int size);

    [DllImport("devicekit")]
    private static extern void devicekit_purge_string(ref IntPtr intptr);

    public static string ToString(IntPtr intptr)
    {
      if (!(intptr != IntPtr.Zero))
        return (string) null;
      string str = (string) null;
      IntPtr data;
      int size;
      MarshalSupport.devicekit_get_rawdata_from_string(intptr, out data, out size);
      if (data != IntPtr.Zero && size > 0)
      {
        byte[] numArray = new byte[size];
        Marshal.Copy(data, numArray, 0, size);
        str = Encoding.UTF8.GetString(numArray);
      }
      MarshalSupport.devicekit_purge_string(ref intptr);
      return str;
    }
  }
}
