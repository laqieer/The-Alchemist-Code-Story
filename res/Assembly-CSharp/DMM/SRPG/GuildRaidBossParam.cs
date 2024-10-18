// Decompiled with JetBrains decompiler
// Type: SRPG.GuildRaidBossParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;

#nullable disable
namespace SRPG
{
  public class GuildRaidBossParam : GuildRaidMasterParam<JSON_GuildRaidBossParam>
  {
    public int Id { get; private set; }

    public int PeriodId { get; private set; }

    public int AreaNo { get; private set; }

    public string Name { get; private set; }

    public int HP { get; private set; }

    public int HPWarning { get; private set; }

    public string UnitIName { get; private set; }

    public int ScoreId { get; private set; }

    public string QuestIName { get; private set; }

    public string BuffId { get; private set; }

    public string MobBuffId { get; private set; }

    public string BeatRewardId { get; private set; }

    public string DamageRankingRewardId { get; private set; }

    public string DamageRatioRewardId { get; private set; }

    public string LastAttackRewardId { get; private set; }

    public override bool Deserialize(JSON_GuildRaidBossParam json)
    {
      if (json == null)
        return false;
      this.Id = json.id;
      this.PeriodId = json.period_id;
      this.AreaNo = json.area_no;
      this.Name = json.name;
      this.BuffId = json.buff_id;
      this.MobBuffId = json.mob_buff_id;
      this.HP = json.hp;
      this.HPWarning = json.hp_warning;
      this.UnitIName = json.unit_iname;
      this.ScoreId = json.score_id;
      this.QuestIName = json.quest_iname;
      this.BeatRewardId = json.beat_reward_id;
      this.DamageRankingRewardId = json.damage_ranking_reward_id;
      this.DamageRatioRewardId = json.damage_ratio_reward_id;
      this.LastAttackRewardId = json.lastatk_reward_id;
      return true;
    }

    public static int CalcMaxHP(GuildRaidBossParam param, int round)
    {
      int num = 0;
      if (round > 1)
      {
        BuffEffectParam buffEffectParam = MonoSingleton<GameManager>.Instance.MasterParam.GetBuffEffectParam(param.BuffId);
        GuildRaidPeriodParam guildRaidPeriodParam = MonoSingleton<GameManager>.Instance.GetGuildRaidPeriodParam(param.PeriodId);
        if (buffEffectParam != null)
        {
          for (int index = 0; index < buffEffectParam.buffs.Length; ++index)
          {
            if (buffEffectParam.buffs[index].type == ParamTypes.Hp || buffEffectParam.buffs[index].type == ParamTypes.HpMax)
            {
              int rankValue = GuildRaidBossParam.GetRankValue(round, guildRaidPeriodParam.RoundBuffMax, buffEffectParam.buffs[index].value_ini, buffEffectParam.buffs[index].value_max);
              switch (buffEffectParam.buffs[index].calc)
              {
                case SkillParamCalcTypes.Add:
                  num += rankValue;
                  continue;
                case SkillParamCalcTypes.Scale:
                  num += (int) ((long) (param.HP * rankValue) / 100L);
                  continue;
                default:
                  continue;
              }
            }
          }
        }
      }
      return param.HP + num;
    }

    private static int GetRankValue(int rank, int rankcap, int ini, int max)
    {
      int num1 = rankcap - 1;
      int num2 = rank - 1;
      if (ini == max || num2 < 1 || num1 < 1)
        return ini;
      if (num2 >= num1)
        return max;
      long num3 = (long) (max - ini) * 100L / (long) num1;
      return (int) ((long) ini + num3 * (long) num2 / 100L);
    }
  }
}
