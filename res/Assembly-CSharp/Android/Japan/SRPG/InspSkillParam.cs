// Decompiled with JetBrains decompiler
// Type: SRPG.InspSkillParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;
using System.Collections.Generic;

namespace SRPG
{
  public class InspSkillParam
  {
    private string iname;
    private string ability;
    private List<InspSkillTriggerParam> triggers;
    private int gen_id;
    private int ctr_min;
    private int ctr_max;
    private List<InspSkillParam> derivation;
    private bool enable_passive;
    private InspSkillParam parent;
    private AbilityParam mAbilityParam;
    private SkillParam mSkillParam;

    public AbilityParam Ability
    {
      get
      {
        if (this.mAbilityParam == null)
        {
          if (string.IsNullOrEmpty(this.ability))
            return (AbilityParam) null;
          this.mAbilityParam = MonoSingleton<GameManager>.Instance.MasterParam.GetAbilityParam(this.ability);
        }
        return this.mAbilityParam;
      }
    }

    public SkillParam Skill
    {
      get
      {
        if (this.mSkillParam == null)
        {
          if (string.IsNullOrEmpty(this.SkillID))
            return (SkillParam) null;
          this.mSkillParam = MonoSingleton<GameManager>.Instance.MasterParam.GetSkillParam(this.SkillID);
        }
        return this.mSkillParam;
      }
    }

    public string Iname
    {
      get
      {
        return this.iname;
      }
    }

    public string SkillID
    {
      get
      {
        if (this.Ability == null || this.Ability.skills == null || (this.Ability.skills == null || this.Ability.skills.Length <= 0))
          return string.Empty;
        return this.Ability.skills[0].iname;
      }
    }

    public string AbilityID
    {
      get
      {
        return this.ability;
      }
    }

    public List<InspSkillTriggerParam> Triggers
    {
      get
      {
        return this.triggers;
      }
    }

    public int CtrMin
    {
      get
      {
        return this.ctr_min;
      }
    }

    public int CtrMax
    {
      get
      {
        return this.ctr_max;
      }
    }

    public int LvCap
    {
      get
      {
        if (this.Ability == null)
          return 0;
        return this.Ability.GetRankCap();
      }
    }

    public int GenId
    {
      get
      {
        return this.gen_id;
      }
    }

    public List<InspSkillParam> Derivation
    {
      get
      {
        return this.derivation;
      }
    }

    public bool EnablePassive
    {
      get
      {
        return this.enable_passive;
      }
    }

    public InspSkillParam Parent
    {
      get
      {
        return this.parent;
      }
    }

    public bool IsBase
    {
      get
      {
        return this.parent == null;
      }
    }

    public static bool Deserialize(JSON_InspSkillParam[] insp_skill_jsons, JSON_InspSkillTriggerParam[] insp_skill_trigger_jsons, ref Dictionary<string, InspSkillParam> insp_skill_dic)
    {
      insp_skill_dic = new Dictionary<string, InspSkillParam>();
      Dictionary<string, InspSkillTriggerParam> insp_skill_trigger_dic = new Dictionary<string, InspSkillTriggerParam>();
      InspSkillTriggerParam.Deserialize(insp_skill_trigger_jsons, ref insp_skill_trigger_dic);
      if (insp_skill_jsons == null || insp_skill_jsons.Length <= 0)
        return false;
      for (int index = 0; index < insp_skill_jsons.Length; ++index)
      {
        JSON_InspSkillParam inspSkillJson = insp_skill_jsons[index];
        if (!insp_skill_dic.ContainsKey(inspSkillJson.iname))
        {
          InspSkillParam inspSkillParam = new InspSkillParam();
          if (inspSkillParam.Deserialize(inspSkillJson, insp_skill_trigger_dic))
            insp_skill_dic.Add(inspSkillJson.iname, inspSkillParam);
        }
      }
      for (int index = 0; index < insp_skill_jsons.Length; ++index)
      {
        if (insp_skill_jsons[index] != null && insp_skill_jsons[index].derivation != null)
        {
          insp_skill_dic[insp_skill_jsons[index].iname].derivation = new List<InspSkillParam>();
          foreach (JSON_InspSkillDerivation inspSkillDerivation in insp_skill_jsons[index].derivation)
          {
            if (!string.IsNullOrEmpty(inspSkillDerivation.iname) && insp_skill_dic.ContainsKey(inspSkillDerivation.iname))
            {
              insp_skill_dic[insp_skill_jsons[index].iname].derivation.Add(insp_skill_dic[inspSkillDerivation.iname]);
              insp_skill_dic[inspSkillDerivation.iname].parent = insp_skill_dic[insp_skill_jsons[index].iname];
            }
          }
        }
      }
      return true;
    }

    private bool Deserialize(JSON_InspSkillParam json, Dictionary<string, InspSkillTriggerParam> insp_skill_trigger_dic)
    {
      if (json == null)
        return false;
      this.iname = json.iname;
      this.ability = json.ability;
      this.gen_id = json.gen_id;
      if (json.triggers != null && json.triggers.Length >= 1 && (insp_skill_trigger_dic != null && insp_skill_trigger_dic.Count >= 1))
      {
        this.triggers = new List<InspSkillTriggerParam>();
        for (int index = 0; index < json.triggers.Length; ++index)
        {
          if (insp_skill_trigger_dic.ContainsKey(json.triggers[index]))
            this.triggers.Add(insp_skill_trigger_dic[json.triggers[index]]);
        }
      }
      this.ctr_min = json.ctr_min;
      this.ctr_max = json.ctr_max;
      this.enable_passive = json.enable_passive == 1;
      return true;
    }

    public int GetCheckNum()
    {
      if (this.ctr_min <= 0 || this.ctr_max <= 0)
        return 1;
      int num1 = 0;
      BattleCore battleCore = !(bool) ((UnityEngine.Object) SceneBattle.Instance) ? (BattleCore) null : SceneBattle.Instance.Battle;
      int num2 = this.ctr_max + 1 - this.ctr_min;
      if (battleCore != null && num2 > 0)
        num1 = (int) ((long) battleCore.GetRandom() % (long) num2);
      return num1 + this.ctr_min;
    }

    public static bool Check(UnitData unit, List<UnitData> targets, ArtifactData artifact, int slot_no, SkillData use)
    {
      if (unit == null || targets == null || (targets.Count <= 0 || artifact == null) || (artifact.ArtifactParam == null || use == null || (slot_no == 0 || !unit.UnitParam.IsInspiration())) || !artifact.IsInspiration())
        return false;
      InspSkillParam inspSkillParam = (InspSkillParam) null;
      if (artifact.InspirationSkillList != null && artifact.InspirationSkillList.Count > 0)
      {
        InspirationSkillData inspirationSkillDataBySlot = artifact.GetInspirationSkillDataBySlot(slot_no);
        if (inspirationSkillDataBySlot != null)
          inspSkillParam = inspirationSkillDataBySlot.InspSkillParam;
      }
      if (inspSkillParam == null)
        inspSkillParam = MonoSingleton<GameManager>.Instance.MasterParam.GetInspirationSkillParam(artifact.ArtifactParam.insp_skill);
      if (inspSkillParam == null || !inspSkillParam.EnablePassive && use.IsPassiveSkill())
        return false;
      if (inspSkillParam.Triggers == null || inspSkillParam.Triggers.Count <= 0)
      {
        DebugUtility.LogWarning(string.Format("ひらめきスキルに条件が設定されていません！iname : ", (object) inspSkillParam.iname));
        return true;
      }
      foreach (InspSkillTriggerParam trigger in inspSkillParam.Triggers)
      {
        if (trigger != null && trigger.Check(unit, targets, use))
          return true;
      }
      return false;
    }

    public static bool IsCanLevelUp(UnitData unit, ArtifactData artifact, SkillData skill)
    {
      if (unit == null || artifact == null || (skill == null || !unit.UnitParam.IsInspiration()) || !artifact.IsInspiration())
        return false;
      InspirationSkillData inspirationSkillData = artifact.GetCurrentInspirationSkillData();
      if (inspirationSkillData == null || !inspirationSkillData.InspSkillParam.EnablePassive && skill.IsPassiveSkill() || !inspirationSkillData.IsIncludeSkill(skill))
        return false;
      return (int) inspirationSkillData.Lv < inspirationSkillData.InspSkillParam.LvCap;
    }

    public InspSkillCostParam GetResetCost()
    {
      return MonoSingleton<GameManager>.Instance.MasterParam.GetInspSkillResetCost(this.GetInspSkillGen());
    }

    public int GetInspSkillGen()
    {
      int num = 0;
      InspSkillParam parent = this.Parent;
      while (parent != null)
      {
        parent = parent.Parent;
        ++num;
      }
      return num;
    }

    public static int GetInspLvUpCostTotal(string iname, int current_lv, int up)
    {
      MasterParam masterParam = MonoSingleton<GameManager>.Instance.MasterParam;
      InspSkillParam inspirationSkillParam = masterParam.GetInspirationSkillParam(iname);
      if (inspirationSkillParam == null)
        return -1;
      if (inspirationSkillParam.LvCap <= current_lv + up)
        up = inspirationSkillParam.LvCap - current_lv;
      if (up <= 0)
        return 0;
      return masterParam.GetInspLvUpCostTotal(inspirationSkillParam.GenId, current_lv, up);
    }
  }
}
