﻿// Decompiled with JetBrains decompiler
// Type: SRPG.RankMatchMissionState
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using System;

namespace SRPG
{
  public class RankMatchMissionState
  {
    private string mIName;
    private int mProgress;
    private DateTime mRewardedAt;
    private bool mIsRewarded;

    public string IName
    {
      get
      {
        return this.mIName;
      }
    }

    public int Progress
    {
      get
      {
        return this.mProgress;
      }
    }

    public DateTime RewardedAt
    {
      get
      {
        return this.mRewardedAt;
      }
    }

    public bool IsRewarded
    {
      get
      {
        return this.mIsRewarded;
      }
    }

    public void Deserialize(string iname, int prog, string rewarded_at)
    {
      this.mIName = iname;
      this.mProgress = prog;
      if (string.IsNullOrEmpty(rewarded_at))
        return;
      this.mRewardedAt = DateTime.Parse(rewarded_at);
      this.mIsRewarded = true;
    }

    public void Increment()
    {
      ++this.mProgress;
    }

    public void SetProgress(int prog)
    {
      this.mProgress = prog;
    }

    public void Rewarded()
    {
      this.mRewardedAt = TimeManager.ServerTime;
    }
  }
}