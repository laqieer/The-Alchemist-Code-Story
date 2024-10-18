// Decompiled with JetBrains decompiler
// Type: GooglePlayGames.Native.Cwrapper.SnapshotMetadataChangeBuilder
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using System;
using System.Runtime.InteropServices;

namespace GooglePlayGames.Native.Cwrapper
{
  internal static class SnapshotMetadataChangeBuilder
  {
    [DllImport("gpg")]
    internal static extern void SnapshotMetadataChange_Builder_SetDescription(HandleRef self, string description);

    [DllImport("gpg")]
    internal static extern IntPtr SnapshotMetadataChange_Builder_Construct();

    [DllImport("gpg")]
    internal static extern void SnapshotMetadataChange_Builder_SetPlayedTime(HandleRef self, ulong played_time);

    [DllImport("gpg")]
    internal static extern void SnapshotMetadataChange_Builder_SetCoverImageFromPngData(HandleRef self, byte[] png_data, UIntPtr png_data_size);

    [DllImport("gpg")]
    internal static extern IntPtr SnapshotMetadataChange_Builder_Create(HandleRef self);

    [DllImport("gpg")]
    internal static extern void SnapshotMetadataChange_Builder_Dispose(HandleRef self);
  }
}
