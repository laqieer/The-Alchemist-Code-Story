﻿// Decompiled with JetBrains decompiler
// Type: SRPG.SimpleDropTableParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using System;

namespace SRPG
{
  public class SimpleDropTableParam
  {
    public string iname;
    public DateTime beginAt;
    public DateTime endAt;
    public string[] dropList;

    public bool Deserialize(JSON_SimpleDropTableParam json)
    {
      this.iname = json.iname;
      this.dropList = json.droplist;
      this.beginAt = DateTime.MinValue;
      this.endAt = DateTime.MaxValue;
      if (!string.IsNullOrEmpty(json.begin_at))
        DateTime.TryParse(json.begin_at, out this.beginAt);
      if (!string.IsNullOrEmpty(json.end_at))
        DateTime.TryParse(json.end_at, out this.endAt);
      return true;
    }

    public bool IsAvailablePeriod(DateTime now)
    {
      return !(now < this.beginAt) && !(this.endAt < now);
    }

    public string GetCommonName
    {
      get
      {
        if (string.IsNullOrEmpty(this.iname))
          return string.Empty;
        return this.iname.Split(':')[0];
      }
    }

    public bool IsSuffix
    {
      get
      {
        return 2 <= this.iname.Split(':').Length;
      }
    }
  }
}
