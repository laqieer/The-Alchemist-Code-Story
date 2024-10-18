// Decompiled with JetBrains decompiler
// Type: SRPG.ChallengeCategoryParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using System;

namespace SRPG
{
  public class ChallengeCategoryParam
  {
    public string iname;
    public TimeParser begin_at;
    public TimeParser end_at;
    public int prio;

    public int Priority
    {
      get
      {
        return this.prio;
      }
    }

    public bool Deserialize(JSON_ChallengeCategoryParam json)
    {
      if (json == null)
        return false;
      this.iname = json.iname;
      if (json.begin_at != null)
      {
        this.begin_at = new TimeParser();
        this.begin_at.Set(json.begin_at, DateTime.MinValue);
      }
      if (json.end_at != null)
      {
        this.end_at = new TimeParser();
        this.end_at.Set(json.end_at, DateTime.MaxValue);
      }
      this.prio = json.prio;
      return true;
    }
  }
}
