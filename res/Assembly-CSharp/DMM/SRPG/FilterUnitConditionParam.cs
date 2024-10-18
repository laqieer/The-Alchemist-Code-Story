// Decompiled with JetBrains decompiler
// Type: SRPG.FilterUnitConditionParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System;

#nullable disable
namespace SRPG
{
  public class FilterUnitConditionParam
  {
    public FilterUnitParam parent;
    public string cnds_iname;
    public string name;
    public int rarity_ini;
    public EBirth birth;
    public ESex sex;
    public string[] af_tags;
    public string[] unit_groups;
    public string[] job_groups;
    public UnitGroupParam[] unit_group_params;
    public JobGroupParam[] job_group_params;

    public FilterUnitConditionParam(FilterUnitParam parentParam) => this.parent = parentParam;

    public string PrefsKey => FilterUtility.FilterPrefs.MakeKey(this.parent.iname, this.cnds_iname);

    public void Deserialize(JSON_FilterUnitConditionParam json)
    {
      this.cnds_iname = json.cnds_iname;
      this.name = json.name;
      this.rarity_ini = json.rarity_ini;
      this.birth = (EBirth) json.birth;
      this.sex = (ESex) json.sex;
      if (json.af_tags != null && json.af_tags.Length > 0)
      {
        this.af_tags = new string[json.af_tags.Length];
        Array.Copy((Array) json.af_tags, (Array) this.af_tags, json.af_tags.Length);
      }
      if (json.un_groups != null && json.un_groups.Length > 0)
      {
        this.unit_groups = new string[json.un_groups.Length];
        Array.Copy((Array) json.un_groups, (Array) this.unit_groups, json.un_groups.Length);
      }
      if (json.job_groups != null && json.job_groups.Length > 0)
      {
        this.job_groups = new string[json.job_groups.Length];
        Array.Copy((Array) json.job_groups, (Array) this.job_groups, json.job_groups.Length);
      }
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) MonoSingleton<GameManager>.GetInstanceDirect(), (UnityEngine.Object) null))
        return;
      MasterParam masterParam = MonoSingleton<GameManager>.Instance.MasterParam;
      if (this.unit_groups != null)
      {
        this.unit_group_params = new UnitGroupParam[this.unit_groups.Length];
        for (int index = 0; index < this.unit_groups.Length; ++index)
          this.unit_group_params[index] = masterParam.GetUnitGroup(this.unit_groups[index]);
      }
      if (this.job_groups == null)
        return;
      this.job_group_params = new JobGroupParam[this.job_groups.Length];
      for (int index = 0; index < this.job_groups.Length; ++index)
        this.job_group_params[index] = masterParam.GetJobGroup(this.job_groups[index]);
    }
  }
}
