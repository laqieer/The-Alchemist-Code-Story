﻿// Decompiled with JetBrains decompiler
// Type: GooglePlayGames.Native.Cwrapper.TurnBasedMatchConfigBuilder
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using System;
using System.Runtime.InteropServices;

namespace GooglePlayGames.Native.Cwrapper
{
  internal static class TurnBasedMatchConfigBuilder
  {
    [DllImport("gpg")]
    internal static extern void TurnBasedMatchConfig_Builder_PopulateFromPlayerSelectUIResponse(HandleRef self, IntPtr response);

    [DllImport("gpg")]
    internal static extern void TurnBasedMatchConfig_Builder_SetVariant(HandleRef self, uint variant);

    [DllImport("gpg")]
    internal static extern void TurnBasedMatchConfig_Builder_AddPlayerToInvite(HandleRef self, string player_id);

    [DllImport("gpg")]
    internal static extern IntPtr TurnBasedMatchConfig_Builder_Construct();

    [DllImport("gpg")]
    internal static extern void TurnBasedMatchConfig_Builder_SetExclusiveBitMask(HandleRef self, ulong exclusive_bit_mask);

    [DllImport("gpg")]
    internal static extern void TurnBasedMatchConfig_Builder_SetMaximumAutomatchingPlayers(HandleRef self, uint maximum_automatching_players);

    [DllImport("gpg")]
    internal static extern IntPtr TurnBasedMatchConfig_Builder_Create(HandleRef self);

    [DllImport("gpg")]
    internal static extern void TurnBasedMatchConfig_Builder_SetMinimumAutomatchingPlayers(HandleRef self, uint minimum_automatching_players);

    [DllImport("gpg")]
    internal static extern void TurnBasedMatchConfig_Builder_Dispose(HandleRef self);
  }
}
