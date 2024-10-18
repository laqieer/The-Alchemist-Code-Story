// Decompiled with JetBrains decompiler
// Type: SRPG.JSON_ConceptCardConditionsParam
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
  public class JSON_ConceptCardConditionsParam
  {
    public string iname;
    public int el_fire;
    public int el_watr;
    public int el_wind;
    public int el_thdr;
    public int el_lit;
    public int el_drk;
    public string un_group;
    public int units_cnds_type;
    public string job_group;
    public int jobs_cnds_type;
    public int sex;
    public int[] birth_id;
  }
}
