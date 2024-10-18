// Decompiled with JetBrains decompiler
// Type: GooglePlayGames.Native.PInvoke.FetchScoreSummaryResponse
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using GooglePlayGames.Native.Cwrapper;
using System;
using System.Runtime.InteropServices;

namespace GooglePlayGames.Native.PInvoke
{
  internal class FetchScoreSummaryResponse : BaseReferenceHolder
  {
    internal FetchScoreSummaryResponse(IntPtr selfPointer)
      : base(selfPointer)
    {
    }

    protected override void CallDispose(HandleRef selfPointer)
    {
      GooglePlayGames.Native.Cwrapper.LeaderboardManager.LeaderboardManager_FetchScoreSummaryResponse_Dispose(selfPointer);
    }

    internal CommonErrorStatus.ResponseStatus GetStatus()
    {
      return GooglePlayGames.Native.Cwrapper.LeaderboardManager.LeaderboardManager_FetchScoreSummaryResponse_GetStatus(this.SelfPtr());
    }

    internal NativeScoreSummary GetScoreSummary()
    {
      return NativeScoreSummary.FromPointer(GooglePlayGames.Native.Cwrapper.LeaderboardManager.LeaderboardManager_FetchScoreSummaryResponse_GetData(this.SelfPtr()));
    }

    internal static FetchScoreSummaryResponse FromPointer(IntPtr pointer)
    {
      if (pointer.Equals((object) IntPtr.Zero))
        return (FetchScoreSummaryResponse) null;
      return new FetchScoreSummaryResponse(pointer);
    }
  }
}
