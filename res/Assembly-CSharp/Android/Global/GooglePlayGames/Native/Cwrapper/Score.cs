// Decompiled with JetBrains decompiler
// Type: GooglePlayGames.Native.Cwrapper.Score
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using System;
using System.Runtime.InteropServices;
using System.Text;

namespace GooglePlayGames.Native.Cwrapper
{
  internal static class Score
  {
    [DllImport("gpg")]
    internal static extern ulong Score_Value(HandleRef self);

    [DllImport("gpg")]
    [return: MarshalAs(UnmanagedType.I1)]
    internal static extern bool Score_Valid(HandleRef self);

    [DllImport("gpg")]
    internal static extern ulong Score_Rank(HandleRef self);

    [DllImport("gpg")]
    internal static extern void Score_Dispose(HandleRef self);

    [DllImport("gpg")]
    internal static extern UIntPtr Score_Metadata(HandleRef self, StringBuilder out_arg, UIntPtr out_size);
  }
}
