﻿// Decompiled with JetBrains decompiler
// Type: GooglePlayGames.Native.Cwrapper.Leaderboard
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using System;
using System.Runtime.InteropServices;
using System.Text;

namespace GooglePlayGames.Native.Cwrapper
{
  internal static class Leaderboard
  {
    [DllImport("gpg")]
    internal static extern UIntPtr Leaderboard_Name(HandleRef self, StringBuilder out_arg, UIntPtr out_size);

    [DllImport("gpg")]
    internal static extern UIntPtr Leaderboard_Id(HandleRef self, StringBuilder out_arg, UIntPtr out_size);

    [DllImport("gpg")]
    internal static extern UIntPtr Leaderboard_IconUrl(HandleRef self, StringBuilder out_arg, UIntPtr out_size);

    [DllImport("gpg")]
    internal static extern void Leaderboard_Dispose(HandleRef self);

    [DllImport("gpg")]
    [return: MarshalAs(UnmanagedType.I1)]
    internal static extern bool Leaderboard_Valid(HandleRef self);

    [DllImport("gpg")]
    internal static extern Types.LeaderboardOrder Leaderboard_Order(HandleRef self);
  }
}
