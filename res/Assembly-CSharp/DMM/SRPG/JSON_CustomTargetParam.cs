// Decompiled with JetBrains decompiler
// Type: SRPG.JSON_CustomTargetParam
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
  public class JSON_CustomTargetParam
  {
    public string iname;
    public string name;
    public string[] units;
    public string[] jobs;
    public string[] unit_groups;
    public string[] job_groups;
    public string[] concept_card_groups;
    public string first_job;
    public string second_job;
    public string third_job;
    public int sex;
    public int birth_id;
    public int fire;
    public int water;
    public int wind;
    public int thunder;
    public int shine;
    public int dark;
  }
}
