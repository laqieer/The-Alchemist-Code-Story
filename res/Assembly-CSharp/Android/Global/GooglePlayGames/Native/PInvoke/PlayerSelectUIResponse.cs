﻿// Decompiled with JetBrains decompiler
// Type: GooglePlayGames.Native.PInvoke.PlayerSelectUIResponse
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using GooglePlayGames.Native.Cwrapper;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace GooglePlayGames.Native.PInvoke
{
  internal class PlayerSelectUIResponse : BaseReferenceHolder, IEnumerable, IEnumerable<string>
  {
    internal PlayerSelectUIResponse(IntPtr selfPointer)
      : base(selfPointer)
    {
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
      return (IEnumerator) this.GetEnumerator();
    }

    internal CommonErrorStatus.UIStatus Status()
    {
      return TurnBasedMultiplayerManager.TurnBasedMultiplayerManager_PlayerSelectUIResponse_GetStatus(this.SelfPtr());
    }

    private string PlayerIdAtIndex(UIntPtr index)
    {
      return PInvokeUtilities.OutParamsToString((PInvokeUtilities.OutStringMethod) ((out_string, size) => TurnBasedMultiplayerManager.TurnBasedMultiplayerManager_PlayerSelectUIResponse_GetPlayerIds_GetElement(this.SelfPtr(), index, out_string, size)));
    }

    public IEnumerator<string> GetEnumerator()
    {
      return PInvokeUtilities.ToEnumerator<string>(TurnBasedMultiplayerManager.TurnBasedMultiplayerManager_PlayerSelectUIResponse_GetPlayerIds_Length(this.SelfPtr()), new Func<UIntPtr, string>(this.PlayerIdAtIndex));
    }

    internal uint MinimumAutomatchingPlayers()
    {
      return TurnBasedMultiplayerManager.TurnBasedMultiplayerManager_PlayerSelectUIResponse_GetMinimumAutomatchingPlayers(this.SelfPtr());
    }

    internal uint MaximumAutomatchingPlayers()
    {
      return TurnBasedMultiplayerManager.TurnBasedMultiplayerManager_PlayerSelectUIResponse_GetMaximumAutomatchingPlayers(this.SelfPtr());
    }

    protected override void CallDispose(HandleRef selfPointer)
    {
      TurnBasedMultiplayerManager.TurnBasedMultiplayerManager_PlayerSelectUIResponse_Dispose(selfPointer);
    }

    internal static PlayerSelectUIResponse FromPointer(IntPtr pointer)
    {
      if (PInvokeUtilities.IsNull(pointer))
        return (PlayerSelectUIResponse) null;
      return new PlayerSelectUIResponse(pointer);
    }
  }
}
