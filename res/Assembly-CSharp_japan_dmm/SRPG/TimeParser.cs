// Decompiled with JetBrains decompiler
// Type: SRPG.TimeParser
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;

#nullable disable
namespace SRPG
{
  public class TimeParser
  {
    private string str_time;
    private DateTime date_time;

    public void Set(string str_time_at, DateTime base_time)
    {
      this.str_time = str_time_at;
      this.date_time = base_time;
      if (string.IsNullOrEmpty(this.str_time))
        return;
      try
      {
        this.date_time = DateTime.Parse(this.str_time);
      }
      catch (Exception ex)
      {
        DebugUtility.LogWarning("Failed to parse date! [" + this.str_time + "]");
      }
    }

    public string StrTime => this.str_time;

    public DateTime DateTimes => this.date_time;
  }
}
