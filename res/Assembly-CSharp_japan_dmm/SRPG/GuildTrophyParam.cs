// Decompiled with JetBrains decompiler
// Type: SRPG.GuildTrophyParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System.Collections.Generic;

#nullable disable
namespace SRPG
{
  public class GuildTrophyParam : TrophyParam
  {
    public override void SetTrophyObjectives(
      ref TrophyObjective[] trophy_objectives,
      JSON_TrophyParam json,
      int objective_length)
    {
      trophy_objectives = (TrophyObjective[]) new GuildTrophyObjective[objective_length];
      if (json == null)
        return;
      for (int index = 0; index < trophy_objectives.Length; ++index)
      {
        GuildTrophyObjective guildTrophyObjective = new GuildTrophyObjective();
        guildTrophyObjective.Param = (TrophyParam) this;
        guildTrophyObjective.index = index;
        guildTrophyObjective.type = (GuildTrophyConditionTypes) json.type;
        guildTrophyObjective.ival = json.ival;
        if (json.sval != null)
          guildTrophyObjective.sval = new List<string>((IEnumerable<string>) json.sval);
        trophy_objectives[index] = (TrophyObjective) guildTrophyObjective;
      }
    }

    public override TrophyState GetTrophyCounter()
    {
      return MonoSingleton<GameManager>.Instance.Player.GuildTrophyData.GetTrophyCounter((TrophyParam) this);
    }
  }
}
