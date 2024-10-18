// Decompiled with JetBrains decompiler
// Type: GooglePlayGames.BasicApi.Nearby.NearbyConnectionConfiguration
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using GooglePlayGames.OurUtils;
using System;

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

    public long LocalClientId
    {
      get
      {
        return this.mLocalClientId;
      }
    }

    public Action<InitializationStatus> InitializationCallback
    {
      get
      {
        return this.mInitializationCallback;
      }
    }
  }
}
