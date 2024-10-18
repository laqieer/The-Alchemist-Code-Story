// Decompiled with JetBrains decompiler
// Type: GooglePlayGames.Native.AppStateClient
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using GooglePlayGames.BasicApi;

namespace GooglePlayGames.Native
{
  internal interface AppStateClient
  {
    void LoadState(int slot, OnStateLoadedListener listener);

    void UpdateState(int slot, byte[] data, OnStateLoadedListener listener);
  }
}
