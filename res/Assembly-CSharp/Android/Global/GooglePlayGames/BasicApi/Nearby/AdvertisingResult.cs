﻿// Decompiled with JetBrains decompiler
// Type: GooglePlayGames.BasicApi.Nearby.AdvertisingResult
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using GooglePlayGames.OurUtils;

namespace GooglePlayGames.BasicApi.Nearby
{
  public struct AdvertisingResult
  {
    private readonly ResponseStatus mStatus;
    private readonly string mLocalEndpointName;

    public AdvertisingResult(ResponseStatus status, string localEndpointName)
    {
      this.mStatus = status;
      this.mLocalEndpointName = Misc.CheckNotNull<string>(localEndpointName);
    }

    public bool Succeeded
    {
      get
      {
        return this.mStatus == ResponseStatus.Success;
      }
    }

    public ResponseStatus Status
    {
      get
      {
        return this.mStatus;
      }
    }

    public string LocalEndpointName
    {
      get
      {
        return this.mLocalEndpointName;
      }
    }
  }
}
