﻿// Decompiled with JetBrains decompiler
// Type: SRPG.VersusStreakWinScheduleParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using System;

namespace SRPG
{
  public class VersusStreakWinScheduleParam
  {
    public int id;
    public STREAK_JUDGE judge;
    public DateTime begin_at;
    public DateTime end_at;

    public bool Deserialize(JSON_VersusStreakWinSchedule json)
    {
      if (json == null)
        return false;
      this.id = json.id;
      try
      {
        this.judge = (STREAK_JUDGE) Enum.ToObject(typeof (STREAK_JUDGE), json.judge);
        if (!string.IsNullOrEmpty(json.begin_at))
          this.begin_at = DateTime.Parse(json.begin_at);
        if (!string.IsNullOrEmpty(json.end_at))
          this.end_at = DateTime.Parse(json.end_at);
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
