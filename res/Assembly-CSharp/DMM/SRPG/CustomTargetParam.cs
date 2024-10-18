﻿// Decompiled with JetBrains decompiler
// Type: SRPG.CustomTargetParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;

#nullable disable
namespace SRPG
{
  public class CustomTargetParam
  {
    public string iname;
    public string[] units;
    public string[] jobs;
    public string[] unit_groups;
    public string[] job_groups;
    public string[] concept_card_groups;
    public string first_job;
    public string second_job;
    public string third_job;
    public ESex sex;
    public int birth_id;
    public int element;

    public bool Deserialize(JSON_CustomTargetParam json)
    {
      this.iname = json.iname;
      if (json.units != null)
      {
        this.units = new string[json.units.Length];
        for (int index = 0; index < json.units.Length; ++index)
          this.units[index] = json.units[index];
      }
      if (json.jobs != null)
      {
        this.jobs = new string[json.jobs.Length];
        for (int index = 0; index < json.jobs.Length; ++index)
          this.jobs[index] = json.jobs[index];
      }
      if (json.unit_groups != null)
      {
        this.unit_groups = new string[json.unit_groups.Length];
        for (int index = 0; index < json.unit_groups.Length; ++index)
          this.unit_groups[index] = json.unit_groups[index];
      }
      if (json.job_groups != null)
      {
        this.job_groups = new string[json.job_groups.Length];
        for (int index = 0; index < json.job_groups.Length; ++index)
          this.job_groups[index] = json.job_groups[index];
      }
      if (json.concept_card_groups != null)
      {
        this.concept_card_groups = new string[json.concept_card_groups.Length];
        for (int index = 0; index < json.concept_card_groups.Length; ++index)
          this.concept_card_groups[index] = json.concept_card_groups[index];
      }
      this.first_job = json.first_job;
      this.second_job = json.second_job;
      this.third_job = json.third_job;
      this.sex = (ESex) json.sex;
      this.birth_id = json.birth_id;
      string[] strArray = new string[6]
      {
        json.dark.ToString(),
        json.shine.ToString(),
        json.thunder.ToString(),
        json.wind.ToString(),
        json.water.ToString(),
        json.fire.ToString()
      };
      string empty = string.Empty;
      foreach (string str in strArray)
        empty += str;
      this.element = Convert.ToInt32(empty, 2);
      return true;
    }
  }
}
