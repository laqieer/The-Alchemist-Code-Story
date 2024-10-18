// Decompiled with JetBrains decompiler
// Type: SRPG.JobSetParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;

#nullable disable
namespace SRPG
{
  public class JobSetParam
  {
    public string iname;
    public string job;
    public int lock_rarity;
    public int lock_awakelv;
    public JobSetParam.JobLock[] lock_jobs;
    public string jobchange;
    public string target_unit;
    private int joblv_opened;

    public int JobLvOpened => this.joblv_opened;

    public bool Deserialize(JSON_JobSetParam json)
    {
      if (json == null)
        return false;
      this.iname = json.iname;
      this.job = json.job;
      this.jobchange = json.cjob;
      this.target_unit = json.target_unit;
      this.lock_rarity = json.lrare;
      this.lock_awakelv = json.lplus;
      this.lock_jobs = (JobSetParam.JobLock[]) null;
      int length = 0;
      if (!string.IsNullOrEmpty(json.ljob1))
        ++length;
      if (!string.IsNullOrEmpty(json.ljob2))
        ++length;
      if (!string.IsNullOrEmpty(json.ljob3))
        ++length;
      if (length > 0)
      {
        this.lock_jobs = new JobSetParam.JobLock[length];
        int index = 0;
        if (!string.IsNullOrEmpty(json.ljob1))
        {
          this.lock_jobs[index] = new JobSetParam.JobLock();
          this.lock_jobs[index].iname = json.ljob1;
          this.lock_jobs[index].lv = json.llv1;
          ++index;
        }
        if (!string.IsNullOrEmpty(json.ljob2))
        {
          this.lock_jobs[index] = new JobSetParam.JobLock();
          this.lock_jobs[index].iname = json.ljob2;
          this.lock_jobs[index].lv = json.llv2;
          ++index;
        }
        if (!string.IsNullOrEmpty(json.ljob3))
        {
          this.lock_jobs[index] = new JobSetParam.JobLock();
          this.lock_jobs[index].iname = json.ljob3;
          this.lock_jobs[index].lv = json.llv3;
          int num = index + 1;
        }
      }
      this.joblv_opened = json.joblv_opened;
      return true;
    }

    public bool ContainsJob(string iname)
    {
      if (string.IsNullOrEmpty(iname))
        return false;
      if (iname == this.job)
        return true;
      if (this.jobchange == null)
        return false;
      return iname == this.jobchange || MonoSingleton<GameManager>.GetInstanceDirect().MasterParam.GetJobSetParam(this.jobchange).ContainsJob(iname);
    }

    public class JobLock
    {
      public string iname;
      public int lv;
    }
  }
}
