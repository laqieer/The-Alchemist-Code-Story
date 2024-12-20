﻿// Decompiled with JetBrains decompiler
// Type: GooglePlayGames.Native.Cwrapper.Quest
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using System;
using System.Runtime.InteropServices;
using System.Text;

namespace GooglePlayGames.Native.Cwrapper
{
  internal static class Quest
  {
    [DllImport("gpg")]
    internal static extern UIntPtr Quest_Description(HandleRef self, StringBuilder out_arg, UIntPtr out_size);

    [DllImport("gpg")]
    internal static extern UIntPtr Quest_BannerUrl(HandleRef self, StringBuilder out_arg, UIntPtr out_size);

    [DllImport("gpg")]
    internal static extern long Quest_ExpirationTime(HandleRef self);

    [DllImport("gpg")]
    internal static extern UIntPtr Quest_IconUrl(HandleRef self, StringBuilder out_arg, UIntPtr out_size);

    [DllImport("gpg")]
    internal static extern Types.QuestState Quest_State(HandleRef self);

    [DllImport("gpg")]
    internal static extern IntPtr Quest_Copy(HandleRef self);

    [DllImport("gpg")]
    [return: MarshalAs(UnmanagedType.I1)]
    internal static extern bool Quest_Valid(HandleRef self);

    [DllImport("gpg")]
    internal static extern long Quest_StartTime(HandleRef self);

    [DllImport("gpg")]
    internal static extern void Quest_Dispose(HandleRef self);

    [DllImport("gpg")]
    internal static extern IntPtr Quest_CurrentMilestone(HandleRef self);

    [DllImport("gpg")]
    internal static extern long Quest_AcceptedTime(HandleRef self);

    [DllImport("gpg")]
    internal static extern UIntPtr Quest_Id(HandleRef self, StringBuilder out_arg, UIntPtr out_size);

    [DllImport("gpg")]
    internal static extern UIntPtr Quest_Name(HandleRef self, StringBuilder out_arg, UIntPtr out_size);
  }
}
