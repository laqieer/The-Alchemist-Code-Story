// Decompiled with JetBrains decompiler
// Type: GooglePlayGames.Native.UnsupportedAppStateClient
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using GooglePlayGames.BasicApi;
using GooglePlayGames.OurUtils;
using System;

namespace GooglePlayGames.Native
{
  internal class UnsupportedAppStateClient : AppStateClient
  {
    private readonly string mMessage;

    internal UnsupportedAppStateClient(string message)
    {
      this.mMessage = Misc.CheckNotNull<string>(message);
    }

    public void LoadState(int slot, OnStateLoadedListener listener)
    {
      throw new NotImplementedException(this.mMessage);
    }

    public void UpdateState(int slot, byte[] data, OnStateLoadedListener listener)
    {
      throw new NotImplementedException(this.mMessage);
    }
  }
}
