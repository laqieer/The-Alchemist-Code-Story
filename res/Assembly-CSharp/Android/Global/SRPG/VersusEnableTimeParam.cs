// Decompiled with JetBrains decompiler
// Type: SRPG.VersusEnableTimeParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using System;
using System.Collections.Generic;

namespace SRPG
{
  public class VersusEnableTimeParam
  {
    private int mScheduleId;
    private VERSUS_TYPE mVersusType;
    private DateTime mBeginAt;
    private DateTime mEndAt;
    private List<VersusEnableTimeScheduleParam> mSchedule;

    public int ScheduleId
    {
      get
      {
        return this.mScheduleId;
      }
    }

    public VERSUS_TYPE VersusType
    {
      get
      {
        return this.mVersusType;
      }
    }

    public DateTime BeginAt
    {
      get
      {
        return this.mBeginAt;
      }
    }

    public DateTime EndAt
    {
      get
      {
        return this.mEndAt;
      }
    }

    public List<VersusEnableTimeScheduleParam> Schedule
    {
      get
      {
        return this.mSchedule;
      }
    }

    public bool Deserialize(JSON_VersusEnableTimeParam json)
    {
      if (json == null)
        return false;
      this.mScheduleId = json.id;
      this.mVersusType = (VERSUS_TYPE) json.mode;
      try
      {
        if (!string.IsNullOrEmpty(json.begin_at))
          this.mBeginAt = DateTime.Parse(json.begin_at);
        if (!string.IsNullOrEmpty(json.end_at))
          this.mEndAt = DateTime.Parse(json.end_at);
      }
      catch (Exception ex)
      {
        DebugUtility.LogError(ex.Message);
        return false;
      }
      this.mSchedule = new List<VersusEnableTimeScheduleParam>();
      for (int index = 0; index < json.schedule.Length; ++index)
      {
        VersusEnableTimeScheduleParam timeScheduleParam = new VersusEnableTimeScheduleParam();
        if (timeScheduleParam.Deserialize(json.schedule[index]))
          this.mSchedule.Add(timeScheduleParam);
      }
      return true;
    }
  }
}
