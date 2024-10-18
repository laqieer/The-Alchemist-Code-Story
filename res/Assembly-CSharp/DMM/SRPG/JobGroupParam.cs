// Decompiled with JetBrains decompiler
// Type: SRPG.JobGroupParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;

#nullable disable
namespace SRPG
{
  public class JobGroupParam
  {
    public string iname;
    public string name;
    public string[] jobs;

    public bool Deserialize(JSON_JobGroupParam json)
    {
      this.iname = json.iname;
      this.jobs = json.jobs;
      this.name = json.name;
      return true;
    }

    public bool IsInGroup(string job_iname)
    {
      return Array.FindIndex<string>(this.jobs, (Predicate<string>) (j => j == job_iname)) >= 0;
    }

    public static bool IsInGroup(JobGroupParam[] group_param, UnitParam unit_param)
    {
      for (int index1 = 0; index1 < group_param.Length; ++index1)
      {
        JobGroupParam jobGroupParam = group_param[index1];
        for (int index2 = 0; index2 < jobGroupParam.jobs.Length; ++index2)
        {
          if (unit_param.HasJobParam(jobGroupParam.jobs[index2]))
            return true;
        }
      }
      return false;
    }
  }
}
