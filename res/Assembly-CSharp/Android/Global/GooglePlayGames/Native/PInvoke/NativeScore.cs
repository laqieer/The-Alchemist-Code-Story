// Decompiled with JetBrains decompiler
// Type: GooglePlayGames.Native.PInvoke.NativeScore
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using GooglePlayGames.Native.Cwrapper;
using System;
using System.Runtime.InteropServices;

namespace GooglePlayGames.Native.PInvoke
{
  internal class NativeScore : BaseReferenceHolder
  {
    private const ulong MinusOne = 18446744073709551615;

    internal NativeScore(IntPtr selfPtr)
      : base(selfPtr)
    {
    }

    protected override void CallDispose(HandleRef selfPointer)
    {
      Score.Score_Dispose(this.SelfPtr());
    }

    internal ulong GetDate()
    {
      return ulong.MaxValue;
    }

    internal string GetMetadata()
    {
      return PInvokeUtilities.OutParamsToString((PInvokeUtilities.OutStringMethod) ((out_string, out_size) => Score.Score_Metadata(this.SelfPtr(), out_string, out_size)));
    }

    internal ulong GetRank()
    {
      return Score.Score_Rank(this.SelfPtr());
    }

    internal ulong GetValue()
    {
      return Score.Score_Value(this.SelfPtr());
    }

    internal PlayGamesScore AsScore(string leaderboardId, string selfPlayerId)
    {
      DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
      ulong num = this.GetDate();
      if (num == ulong.MaxValue)
        num = 0UL;
      return new PlayGamesScore(dateTime.AddMilliseconds((double) num), leaderboardId, this.GetRank(), selfPlayerId, this.GetValue(), this.GetMetadata());
    }

    internal static NativeScore FromPointer(IntPtr pointer)
    {
      if (pointer.Equals((object) IntPtr.Zero))
        return (NativeScore) null;
      return new NativeScore(pointer);
    }
  }
}
