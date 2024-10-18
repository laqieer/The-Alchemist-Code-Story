// Decompiled with JetBrains decompiler
// Type: SRPG.TrophyState
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using System;

namespace SRPG
{
  public class TrophyState
  {
    public int[] Count = new int[0];
    public string iname;
    public bool IsEnded;
    public int StartYMD;
    public DateTime RewardedAt;
    public bool IsDirty;
    public TrophyParam Param;
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
