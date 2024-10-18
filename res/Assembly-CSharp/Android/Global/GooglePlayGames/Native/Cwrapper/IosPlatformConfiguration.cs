// Decompiled with JetBrains decompiler
// Type: GooglePlayGames.Native.Cwrapper.IosPlatformConfiguration
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using System;
using System.Runtime.InteropServices;

namespace GooglePlayGames.Native.Cwrapper
{
  internal static class IosPlatformConfiguration
  {
    [DllImport("gpg")]
    internal static extern IntPtr IosPlatformConfiguration_Construct();

    [DllImport("gpg")]
    internal static extern void IosPlatformConfiguration_Dispose(HandleRef self);

    [DllImport("gpg")]
    [return: MarshalAs(UnmanagedType.I1)]
    internal static extern bool IosPlatformConfiguration_Valid(HandleRef self);

    [DllImport("gpg")]
    internal static extern void IosPlatformConfiguration_SetClientID(HandleRef self, string client_id);
  }
}
