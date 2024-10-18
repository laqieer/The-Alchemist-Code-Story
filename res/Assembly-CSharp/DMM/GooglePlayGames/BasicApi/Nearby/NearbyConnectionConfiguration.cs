// Decompiled with JetBrains decompiler
// Type: GooglePlayGames.BasicApi.Nearby.NearbyConnectionConfiguration
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GooglePlayGames.OurUtils;
using System;

#nullable disable
namespace GooglePlayGames.BasicApi.Nearby
{
  public struct NearbyConnectionConfiguration
  {
    public const int MaxUnreliableMessagePayloadLength = 1168;
    public const int MaxReliableMessagePayloadLength = 4096;
    private readonly Action<InitializationStatus> mInitializationCallback;
    private readonly long mLocalClientId;

    public NearbyConnectionConfiguration(Action<InitializationStatus> callback, long localClientId)
    {
      this.mInitializationCallback = Misc.CheckNotNull<Action<InitializationStatus>>(callback);
      this.mLocalClientId = localClientId;
    }

    public long LocalClientId => this.mLocalClientId;

    public Action<InitializationStatus> InitializationCallback => this.mInitializationCallback;
  }
}
