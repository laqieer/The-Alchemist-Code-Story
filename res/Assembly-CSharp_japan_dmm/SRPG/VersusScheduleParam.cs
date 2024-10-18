// Decompiled with JetBrains decompiler
// Type: SRPG.VersusScheduleParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;

#nullable disable
namespace SRPG
{
  public class VersusScheduleParam
  {
    public string tower_iname;
    public string iname;
    public string begin_at;
    public string end_at;
    public string gift_begin_at;
    public string gift_end_at;
    private DateTime BeginDate;
    private DateTime EndDate;
    private DateTime GiftBeginDate;
    private DateTime GiftEndDate;

    public bool IsOpen
    {
      get
      {
        DateTime serverTime = TimeManager.ServerTime;
        return this.BeginDate < serverTime && serverTime < this.EndDate;
      }
    }

    public bool IsGift
    {
      get
      {
        DateTime serverTime = TimeManager.ServerTime;
        return this.GiftBeginDate < serverTime && serverTime < this.GiftEndDate;
      }
    }

    public void Deserialize(JSON_VersusSchedule json)
    {
      if (json == null)
        return;
      this.tower_iname = json.tower_iname;
      this.iname = json.iname;
      this.begin_at = json.begin_at;
      this.end_at = json.end_at;
      this.gift_begin_at = json.gift_begin_at;
      this.gift_end_at = json.gift_end_at;
      try
      {
        this.BeginDate = DateTime.Parse(this.begin_at);
        this.EndDate = DateTime.Parse(this.end_at);
        this.GiftBeginDate = DateTime.Parse(this.gift_begin_at);
        this.GiftEndDate = DateTime.Parse(this.gift_end_at);
      }
      catch (Exception ex)
      {
        DebugUtility.Log(ex.ToString());
      }
    }
  }
}
