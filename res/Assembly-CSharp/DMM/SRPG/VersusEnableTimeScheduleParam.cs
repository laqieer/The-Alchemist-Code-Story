// Decompiled with JetBrains decompiler
// Type: SRPG.VersusEnableTimeScheduleParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;
using System.Collections.Generic;

#nullable disable
namespace SRPG
{
  public class VersusEnableTimeScheduleParam
  {
    private string mBegin;
    private string mOpen;
    private string mQuestIname;
    private List<DateTime> mAddDateList;

    public string Begin => this.mBegin;

    public string Open => this.mOpen;

    public string QuestIname => this.mQuestIname;

    public List<DateTime> AddDateList => this.mAddDateList;

    public bool Deserialize(JSON_VersusEnableTimeScheduleParam json)
    {
      this.mBegin = json.begin_time;
      this.mOpen = json.open_time;
      this.mQuestIname = json.quest_iname;
      try
      {
        if (json.add_date != null)
        {
          this.mAddDateList = new List<DateTime>();
          for (int index = 0; index < json.add_date.Length; ++index)
          {
            if (!string.IsNullOrEmpty(json.add_date[index]))
              this.mAddDateList.Add(DateTime.Parse(json.add_date[index]));
          }
        }
      }
      catch (Exception ex)
      {
        DebugUtility.LogError(ex.Message);
        return false;
      }
      return true;
    }
  }
}
