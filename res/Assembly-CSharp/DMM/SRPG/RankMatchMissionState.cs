// Decompiled with JetBrains decompiler
// Type: SRPG.RankMatchMissionState
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;

#nullable disable
namespace SRPG
{
  public class RankMatchMissionState
  {
    private string mIName;
    private int mProgress;
    private DateTime mRewardedAt;
    private bool mIsRewarded;

    public string IName => this.mIName;

    public int Progress => this.mProgress;

    public DateTime RewardedAt => this.mRewardedAt;

    public bool IsRewarded => this.mIsRewarded;

    public void Deserialize(string iname, int prog, string rewarded_at)
    {
      this.mIName = iname;
      this.mProgress = prog;
      if (string.IsNullOrEmpty(rewarded_at))
        return;
      this.mRewardedAt = DateTime.Parse(rewarded_at);
      this.mIsRewarded = true;
    }

    public void Increment() => ++this.mProgress;

    public void SetProgress(int prog) => this.mProgress = prog;

    public void Rewarded() => this.mRewardedAt = TimeManager.ServerTime;
  }
}
