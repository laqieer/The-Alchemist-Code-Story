// Decompiled with JetBrains decompiler
// Type: SRPG.JobGroupParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using System;

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
  }
}
