// Decompiled with JetBrains decompiler
// Type: SRPG.GuildRaidScoreParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;
using System.Collections.Generic;

#nullable disable
namespace SRPG
{
  public class GuildRaidScoreParam : GuildRaidMasterParam<JSON_GuildRaidScoreParam>
  {
    public int Id { get; private set; }

    public List<GuildRaidScoreDataParam> Score { get; private set; }

    public override bool Deserialize(JSON_GuildRaidScoreParam json)
    {
      if (json == null)
        return false;
      this.Id = json.id;
      if (json.score == null)
        return false;
      this.Score = new List<GuildRaidScoreDataParam>();
      for (int index = 0; index < json.score.Length; ++index)
      {
        GuildRaidScoreDataParam raidScoreDataParam = new GuildRaidScoreDataParam();
        if (raidScoreDataParam.Deserialize(json.score[index]))
          this.Score.Add(raidScoreDataParam);
      }
      return true;
    }

    public int GetScore(int round, int score)
    {
      int score1 = score;
      if (this.Score != null && this.Score.Count > 0)
      {
        GuildRaidScoreDataParam raidScoreDataParam = this.Score.Find((Predicate<GuildRaidScoreDataParam>) (data => data.Round == round));
        if (raidScoreDataParam != null)
        {
          long num = (long) (score * raidScoreDataParam.Scale) / 100L;
          if (num >= (long) int.MaxValue)
          {
            DebugUtility.LogWarning("GuildRaidScore Score Over int.MaxValue");
            num = (long) int.MaxValue;
          }
          score1 = (int) num;
        }
      }
      return score1;
    }
  }
}
