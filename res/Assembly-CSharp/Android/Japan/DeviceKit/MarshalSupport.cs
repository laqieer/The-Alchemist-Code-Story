// Decompiled with JetBrains decompiler
// Type: DeviceKit.MarshalSupport
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using System;
using System.Runtime.InteropServices;
using System.Text;

namespace DeviceKit
{
  internal static class MarshalSupport
  {
    public static string ToString(IntPtr intptr)
    {
      if (!(intptr != IntPtr.Zero))
        return (string) null;
      int length = 0;
      while (Marshal.ReadByte(intptr, length) != (byte) 0)
        ++length;
      byte[] numArray = new byte[length];
      Marshal.Copy(intptr, numArray, 0, length);
      return Encoding.Default.GetString(numArray);
    }
  }
}
