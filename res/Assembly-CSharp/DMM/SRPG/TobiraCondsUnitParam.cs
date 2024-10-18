// Decompiled with JetBrains decompiler
// Type: SRPG.TobiraCondsUnitParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;
using System.Collections.Generic;

#nullable disable
namespace SRPG
{
  public class TobiraCondsUnitParam
  {
    private string mId;
    private string mUnitIname;
    private int mLevel;
    private int mAwakeLevel;
    private List<TobiraCondsUnitParam.CondJob> mJobs;
    private TobiraParam.Category mCategory;
    private int mTobiraLv;
    private TobiraCondsUnitParam.ConditionsDetail mConditionsDetail;

    public string Id => this.mId;

    public string UnitIname => this.mUnitIname;

    public int Level => this.mLevel;

    public int AwakeLevel => this.mAwakeLevel;

    public TobiraParam.Category TobiraCategory => this.mCategory;

    public int TobiraLv => this.mTobiraLv;

    public List<TobiraCondsUnitParam.CondJob> Jobs => this.mJobs;

    public bool IsSelfUnit => string.IsNullOrEmpty(this.mUnitIname);

    public void Deserialize(JSON_TobiraCondsUnitParam json)
    {
      if (json == null)
        return;
      this.mId = json.id;
      this.mUnitIname = json.unit_iname;
      this.mLevel = json.lv;
      this.mAwakeLevel = json.awake_lv;
      this.mJobs = new List<TobiraCondsUnitParam.CondJob>();
      if (json.jobs != null && json.jobs.Length > 0)
      {
        foreach (JSON_TobiraCondsUnitParam.JobCond job in json.jobs)
        {
          if (job != null && !string.IsNullOrEmpty(job.job_iname) && job.job_lv > 0)
            this.mJobs.Add(new TobiraCondsUnitParam.CondJob(job.job_iname, job.job_lv));
        }
      }
      this.mCategory = (TobiraParam.Category) json.category;
      this.mTobiraLv = json.tobira_lv;
      this.UpdateConditionsFlag();
    }

    private void UpdateConditionsFlag()
    {
      if (this.IsSelfUnit)
        this.mConditionsDetail |= TobiraCondsUnitParam.ConditionsDetail.IsSelf;
      if (this.Level > 0)
        this.mConditionsDetail |= TobiraCondsUnitParam.ConditionsDetail.IsUnitLv;
      if (this.mJobs != null && this.mJobs.Count > 0)
        this.mConditionsDetail |= TobiraCondsUnitParam.ConditionsDetail.IsJobLv;
      if (this.mAwakeLevel > 0)
        this.mConditionsDetail |= TobiraCondsUnitParam.ConditionsDetail.IsAwake;
      if (this.mTobiraLv <= 0)
        return;
      this.mConditionsDetail |= TobiraCondsUnitParam.ConditionsDetail.IsTobiraLv;
    }

    public bool HasFlag(TobiraCondsUnitParam.ConditionsDetail flag)
    {
      return (this.mConditionsDetail & flag) != (TobiraCondsUnitParam.ConditionsDetail) 0;
    }

    public bool IsJobLvClear(UnitData unit)
    {
      if (this.Jobs != null && this.Jobs.Count > 0 && unit != null)
      {
        foreach (TobiraCondsUnitParam.CondJob job in this.Jobs)
        {
          if (unit.GetJobLevelByJobID(job.JobIname) >= job.JobLevel)
            return true;
        }
      }
      return false;
    }

    public class CondJob
    {
      private string mJobIname;
      private int mJobLevel;

      public CondJob(string job_iname, int job_level)
      {
        this.mJobIname = job_iname;
        this.mJobLevel = job_level;
      }

      public string JobIname => this.mJobIname;

      public int JobLevel => this.mJobLevel;
    }

    [Flags]
    public enum ConditionsDetail : long
    {
      IsSelf = 1,
      IsUnitLv = 2,
      IsAwake = 4,
      IsJobLv = 8,
      IsTobiraLv = 16, // 0x0000000000000010
    }
  }
}
