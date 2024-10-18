// Decompiled with JetBrains decompiler
// Type: GooglePlayGames.PlayGamesClientFactory
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using GooglePlayGames.Android;
using GooglePlayGames.BasicApi;
using GooglePlayGames.Native;
using UnityEngine;

namespace GooglePlayGames
{
  internal class PlayGamesClientFactory
  {
    internal static IPlayGamesClient GetPlatformPlayGamesClient(PlayGamesClientConfiguration config)
    {
      if (Application.isEditor)
      {
        GooglePlayGames.OurUtils.Logger.d("Creating IPlayGamesClient in editor, using DummyClient.");
        return (IPlayGamesClient) new DummyClient();
      }
      GooglePlayGames.OurUtils.Logger.d("Creating Android IPlayGamesClient Client");
      return (IPlayGamesClient) new NativeClient(config, (IClientImpl) new AndroidClient());
    }
  }
}
