// Decompiled with JetBrains decompiler
// Type: GooglePlayGames.Native.Cwrapper.GameServices
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using System;
using System.Runtime.InteropServices;

namespace GooglePlayGames.Native.Cwrapper
{
  internal static class GameServices
  {
    [DllImport("gpg")]
    internal static extern void GameServices_Flush(HandleRef self, GameServices.FlushCallback callback, IntPtr callback_arg);

    [DllImport("gpg")]
    [return: MarshalAs(UnmanagedType.I1)]
    internal static extern bool GameServices_IsAuthorized(HandleRef self);

    [DllImport("gpg")]
    internal static extern void GameServices_Dispose(HandleRef self);

    [DllImport("gpg")]
    internal static extern void GameServices_SignOut(HandleRef self);

    [DllImport("gpg")]
    internal static extern void GameServices_StartAuthorizationUI(HandleRef self);

    internal delegate void FlushCallback(CommonErrorStatus.FlushStatus arg0, IntPtr arg1);
  }
}
