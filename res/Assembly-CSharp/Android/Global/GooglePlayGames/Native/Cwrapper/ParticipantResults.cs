﻿// Decompiled with JetBrains decompiler
// Type: GooglePlayGames.Native.Cwrapper.ParticipantResults
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using System;
using System.Runtime.InteropServices;

namespace GooglePlayGames.Native.Cwrapper
{
  internal static class ParticipantResults
  {
    [DllImport("gpg")]
    internal static extern IntPtr ParticipantResults_WithResult(HandleRef self, string participant_id, uint placing, Types.MatchResult result);

    [DllImport("gpg")]
    [return: MarshalAs(UnmanagedType.I1)]
    internal static extern bool ParticipantResults_Valid(HandleRef self);

    [DllImport("gpg")]
    internal static extern Types.MatchResult ParticipantResults_MatchResultForParticipant(HandleRef self, string participant_id);

    [DllImport("gpg")]
    internal static extern uint ParticipantResults_PlaceForParticipant(HandleRef self, string participant_id);

    [DllImport("gpg")]
    [return: MarshalAs(UnmanagedType.I1)]
    internal static extern bool ParticipantResults_HasResultsForParticipant(HandleRef self, string participant_id);

    [DllImport("gpg")]
    internal static extern void ParticipantResults_Dispose(HandleRef self);
  }
}
