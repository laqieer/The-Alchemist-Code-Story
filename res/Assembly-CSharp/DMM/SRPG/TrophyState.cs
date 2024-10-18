// Decompiled with JetBrains decompiler
// Type: SRPG.TrophyState
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;

#nullable disable
namespace SRPG
{
  public class TrophyState
  {
    public string iname;
    public int[] Count = new int[0];
    public int StartYMD;
    public DateTime RewardedAt;
    public TrophyParam Param;
    public bool IsEnded;
    public bool IsDirty;
    public bool IsSending;

    public bool IsCompleted
    {
      get
      {
        if (this.Param == null || this.Count.Length < this.Param.Objectives.Length)
          return false;
        for (int index = 0; index < this.Param.Objectives.Length && index < this.Count.Length; ++index)
        {
          if (this.Count[index] < this.Param.Objectives[index].RequiredCount)
            return false;
        }
        return true;
      }
    }

    public void Setup(TrophyParam trophyParam, JSON_TrophyProgress trophyProgress)
    {
      this.iname = trophyParam.iname;
      this.Param = trophyParam;
      for (int index = 0; index < trophyProgress.pts.Length && index < this.Count.Length; ++index)
        this.Count[index] = trophyProgress.pts[index];
      if (trophyProgress.rewarded_at != 0)
      {
        try
        {
          this.RewardedAt = trophyProgress.rewarded_at.FromYMD();
        }
        catch
        {
          this.RewardedAt = DateTime.MinValue;
        }
      }
      this.IsEnded = trophyProgress.rewarded_at != 0;
      this.StartYMD = trophyProgress.ymd;
    }
  }
}
