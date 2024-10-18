// Decompiled with JetBrains decompiler
// Type: SRPG.ConceptCardEffectsParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using MessagePack;

#nullable disable
namespace SRPG
{
  [MessagePackObject(true)]
  public class ConceptCardEffectsParam
  {
    public string cnds_iname;
    public string card_skill;
    public string add_card_skill_buff_awake;
    public string add_card_skill_buff_lvmax;
    public string abil_iname;
    public string abil_iname_lvmax;
    public string statusup_skill;
    public string skin;
    public bool is_decrease_eff;

    public bool Deserialize(JSON_ConceptCardEquipParam json)
    {
      this.cnds_iname = json.cnds_iname;
      this.card_skill = json.card_skill;
      this.add_card_skill_buff_awake = json.add_card_skill_buff_awake;
      this.add_card_skill_buff_lvmax = json.add_card_skill_buff_lvmax;
      this.abil_iname = json.abil_iname;
      this.abil_iname_lvmax = json.abil_iname_lvmax;
      this.statusup_skill = json.statusup_skill;
      this.skin = json.skin;
      this.is_decrease_eff = json.is_decrease_eff == 1;
      return true;
    }

    public BuffEffect CreateAddCardSkillBuffEffectAwake(int awake, int awake_cap)
    {
      if (string.IsNullOrEmpty(this.add_card_skill_buff_awake) || awake <= 0)
        return (BuffEffect) null;
      BuffEffectParam buffEffectParam = MonoSingleton<GameManager>.Instance.MasterParam.GetBuffEffectParam(this.add_card_skill_buff_awake);
      return buffEffectParam == null ? (BuffEffect) null : BuffEffect.CreateBuffEffect(buffEffectParam, awake, awake_cap);
    }

    public BuffEffect CreateAddCardSkillBuffEffectLvMax(int lv, int lv_cap, int awake)
    {
      if (lv < lv_cap)
        return (BuffEffect) null;
      if (string.IsNullOrEmpty(this.add_card_skill_buff_lvmax) || awake <= 0)
        return (BuffEffect) null;
      BuffEffectParam buffEffectParam = MonoSingleton<GameManager>.Instance.MasterParam.GetBuffEffectParam(this.add_card_skill_buff_lvmax);
      return buffEffectParam == null ? (BuffEffect) null : BuffEffect.CreateBuffEffect(buffEffectParam, 1, 1);
    }

    public void GetAddCardSkillBuffStatusAwake(
      int awake,
      int awake_cap,
      ref BaseStatus total_add,
      ref BaseStatus total_scale)
    {
      total_add.Clear();
      total_scale.Clear();
      this.CreateAddCardSkillBuffEffectAwake(awake, awake_cap)?.GetBaseStatus(ref total_add, ref total_scale);
    }

    public void GetAddCardSkillBuffStatusLvMax(
      int lv,
      int lv_cap,
      int awake,
      ref BaseStatus total_add,
      ref BaseStatus total_scale)
    {
      total_add.Clear();
      total_scale.Clear();
      this.CreateAddCardSkillBuffEffectLvMax(lv, lv_cap, awake)?.GetBaseStatus(ref total_add, ref total_scale);
    }
  }
}
