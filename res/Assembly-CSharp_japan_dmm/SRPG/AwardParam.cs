﻿// Decompiled with JetBrains decompiler
// Type: SRPG.AwardParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;

#nullable disable
namespace SRPG
{
  public class AwardParam
  {
    public int id;
    public string iname;
    public string name;
    public string expr;
    public string icon;
    public string bg;
    public string txt_img;
    public DateTime start_at;
    public int grade;
    public int hash;
    public int tab;

    public bool Deserialize(JSON_AwardParam json)
    {
      if (json == null)
        return false;
      this.id = json.id;
      this.iname = json.iname;
      this.name = json.name;
      this.expr = json.expr;
      this.icon = json.icon;
      this.bg = json.bg;
      this.txt_img = json.txt_img;
      this.start_at = DateTime.MinValue;
      if (!string.IsNullOrEmpty(json.start_at))
        DateTime.TryParse(json.start_at, out this.start_at);
      this.grade = json.grade;
      this.hash = json.iname.GetHashCode();
      this.tab = json.tab;
      return true;
    }

    public bool IsAvailableStart(DateTime now) => this.start_at < now;

    public ItemParam ToItemParam()
    {
      JSON_ItemParam json = new JSON_ItemParam();
      json.iname = this.iname;
      json.name = this.name;
      json.icon = this.icon;
      json.rare = this.grade - 1;
      json.type = 19;
      ItemParam itemParam = new ItemParam();
      itemParam.Deserialize(json);
      return itemParam;
    }

    public enum AwardGrade
    {
      None,
      Grade1,
      Grade2,
      Grade3,
      Grade4,
      Grade5,
      GradeEx,
    }

    public enum Tab
    {
      None = -1, // 0xFFFFFFFF
      Normal = 0,
      Extra = 1,
    }
  }
}
