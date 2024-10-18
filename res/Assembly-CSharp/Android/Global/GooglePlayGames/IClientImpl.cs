// Decompiled with JetBrains decompiler
// Type: GooglePlayGames.IClientImpl
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using GooglePlayGames.BasicApi;
using GooglePlayGames.Native.PInvoke;
using System;

namespace GooglePlayGames
{
  internal interface IClientImpl
  {
    PlatformConfiguration CreatePlatformConfiguration();

    TokenClient CreateTokenClient();

    void GetPlayerStats(IntPtr apiClient, Action<CommonStatusCodes, PlayGamesLocalUser.PlayerStats> callback);
  }
}
