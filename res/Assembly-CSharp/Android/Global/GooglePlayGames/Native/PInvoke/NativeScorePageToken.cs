// Decompiled with JetBrains decompiler
// Type: GooglePlayGames.Native.PInvoke.NativeScorePageToken
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using GooglePlayGames.Native.Cwrapper;
using System;
using System.Runtime.InteropServices;

namespace GooglePlayGames.Native.PInvoke
{
  internal class NativeScorePageToken : BaseReferenceHolder
  {
    internal NativeScorePageToken(IntPtr selfPtr)
      : base(selfPtr)
    {
    }

    protected override void CallDispose(HandleRef selfPointer)
    {
      ScorePage.ScorePage_ScorePageToken_Dispose(selfPointer);
    }
  }
}
