// Decompiled with JetBrains decompiler
// Type: SRPG.BannerParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;
using UnityEngine;

#nullable disable
namespace SRPG
{
  public class BannerParam
  {
    public string iname;
    public BannerType type;
    public string sval;
    public string banner;
    public string banr_sprite;
    public string begin_at;
    public string end_at;
    public int priority;
    public string message;
    private bool is_not_home;
    private BannerTabType tab_type;

    public bool IsHomeBanner => !this.is_not_home;

    public BannerTabType TabType => this.tab_type;

    public bool Deserialize(JSON_BannerParam json)
    {
      if (json == null)
        return false;
      try
      {
        this.iname = json.iname;
        this.type = (BannerType) Enum.Parse(typeof (BannerType), json.type);
        this.sval = json.sval;
        this.banner = json.banr;
        this.banr_sprite = json.banr_sprite;
        this.begin_at = json.begin_at;
        this.end_at = json.end_at;
        this.priority = json.priority > 0 ? json.priority : int.MaxValue;
        this.message = json.message;
        this.is_not_home = json.is_not_home == 1;
        this.tab_type = (BannerTabType) json.tab_type;
      }
      catch (Exception ex)
      {
        Debug.LogException(ex);
        return false;
      }
      return true;
    }

    public bool IsAvailablePeriod(DateTime now)
    {
      DateTime minValue = DateTime.MinValue;
      DateTime maxValue = DateTime.MaxValue;
      try
      {
        if (!string.IsNullOrEmpty(this.begin_at))
          minValue = DateTime.Parse(this.begin_at);
        if (!string.IsNullOrEmpty(this.end_at))
          maxValue = DateTime.Parse(this.end_at);
      }
      catch
      {
        return false;
      }
      return !(now < minValue) && !(maxValue < now);
    }
  }
}
