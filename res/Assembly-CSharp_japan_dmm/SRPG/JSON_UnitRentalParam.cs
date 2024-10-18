// Decompiled with JetBrains decompiler
// Type: SRPG.JSON_UnitRentalParam
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
  public class JSON_UnitRentalParam
  {
    public string iname;
    public string unit;
    public string begin_at;
    public string end_at;
    public int pt_max;
    public int ptup_lv;
    public int ptup_evol;
    public int ptup_awake;
    public int ptup_job_lv;
    public int ptup_ability_lv;
    public int ptup_quest_main;
    public int ptup_quest_sub;
    public string notification;
    public JSON_UnitRentalParam.QuestInfo[] quest_infos;

    [MessagePackObject(true)]
    [Serializable]
    public class QuestInfo
    {
      public int point;
      public string quest_id;
    }
  }
}
