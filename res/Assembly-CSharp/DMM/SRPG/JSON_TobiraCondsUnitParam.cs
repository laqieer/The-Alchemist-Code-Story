// Decompiled with JetBrains decompiler
// Type: SRPG.JSON_TobiraCondsUnitParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using MessagePack;
using System;

#nullable disable
namespace SRPG
{
  [MessagePackObject(true)]
  [Serializable]
  public class JSON_TobiraCondsUnitParam
  {
    public string id;
    public string unit_iname;
    public int lv;
    public int awake_lv;
    public JSON_TobiraCondsUnitParam.JobCond[] jobs;
    public int category;
    public int tobira_lv;

    [MessagePackObject(true)]
    [Serializable]
    public class JobCond
    {
      public string job_iname;
      public int job_lv;
    }
  }
}
