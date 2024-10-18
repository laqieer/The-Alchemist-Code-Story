// Decompiled with JetBrains decompiler
// Type: SRPG.ConditionsResult_JobLv
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System;
using System.Collections.Generic;

#nullable disable
namespace SRPG
{
  public class ConditionsResult_JobLv : ConditionsResult_Unit
  {
    public List<ConditionsResult_JobLv.CondsJob> mCondJobs;
    private JobParam mCurrentJobParam;

    public ConditionsResult_JobLv(
      UnitData unitData,
      UnitParam unitParam,
      List<TobiraCondsUnitParam.CondJob> condsJobs)
      : base(unitData, unitParam)
    {
      this.mIsClear = false;
      this.mCondJobs = new List<ConditionsResult_JobLv.CondsJob>();
      this.mTargetValue = condsJobs.Count <= 0 ? 0 : condsJobs[0].JobLevel;
      this.mCurrentJobParam = condsJobs.Count <= 0 ? (JobParam) null : MonoSingleton<GameManager>.Instance.GetJobParam(condsJobs[0].JobIname);
      this.mCurrentValue = 0;
      if (condsJobs == null || condsJobs.Count <= 0)
        return;
      foreach (TobiraCondsUnitParam.CondJob condsJob1 in condsJobs)
      {
        TobiraCondsUnitParam.CondJob cond_job = condsJob1;
        ConditionsResult_JobLv.CondsJob condsJob2 = new ConditionsResult_JobLv.CondsJob();
        condsJob2.mCondsJobIname = cond_job.JobIname;
        condsJob2.mCondsJobLv = cond_job.JobLevel;
        condsJob2.mJobParam = MonoSingleton<GameManager>.Instance.GetJobParam(cond_job.JobIname);
        if (unitData != null)
        {
          condsJob2.mJobData = Array.Find<JobData>(unitData.Jobs, (Predicate<JobData>) (job_data => job_data.Param.iname == cond_job.JobIname));
          if (!this.mIsClear && condsJob2.mJobData != null)
          {
            this.mIsClear = condsJob2.mJobData.Rank >= condsJob2.mCondsJobLv;
            if (condsJob2.mJobData.IsActivated)
            {
              this.mTargetValue = cond_job.JobLevel;
              this.mCurrentJobParam = condsJob2.mJobParam;
              this.mCurrentValue = condsJob2.mJobData.Rank;
            }
          }
        }
        this.mCondJobs.Add(condsJob2);
      }
    }

    public override string text
    {
      get
      {
        if (this.mCondJobs == null || this.mCondJobs.Count <= 0)
          return string.Empty;
        if (this.mCondJobs.Count == 1)
          return LocalizedText.Get("sys.TOBIRA_CONDITIONS_JOB_LEVEL", (object) this.unitName, (object) this.mCondJobs[0].mJobParam.name, (object) this.mCondJobs[0].mCondsJobLv.ToString());
        string empty = string.Empty;
        for (int index = 0; index < this.mCondJobs.Count; ++index)
        {
          if (index == 0)
            empty += LocalizedText.Get("sys.TOBIRA_CONDITIONS_JOB_LEVEL_OR_START", (object) this.unitName, (object) this.mCondJobs[index].mJobParam.name, (object) this.mCondJobs[index].mCondsJobLv.ToString());
          else if (index == this.mCondJobs.Count - 1)
            empty += LocalizedText.Get("sys.TOBIRA_CONDITIONS_JOB_LEVEL_OR_END", (object) this.mCondJobs[index].mJobParam.name, (object) this.mCondJobs[index].mCondsJobLv.ToString());
          else
            empty += LocalizedText.Get("sys.TOBIRA_CONDITIONS_JOB_LEVEL_OR", (object) this.mCondJobs[index].mJobParam.name, (object) this.mCondJobs[index].mCondsJobLv.ToString());
        }
        return empty;
      }
    }

    public override string errorText => "エラー文";

    public JobParam CurrentJobParam => this.mCurrentJobParam;

    public class CondsJob
    {
      public string mCondsJobIname;
      public int mCondsJobLv;
      public JobData mJobData;
      public JobParam mJobParam;
    }
  }
}
