﻿// Decompiled with JetBrains decompiler
// Type: SRPG.AppealLimitedShopMaster
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using System;

namespace SRPG
{
  public class AppealLimitedShopMaster
  {
    public string appeal_id;
    public long start_at;
    public long end_at;
    public int priority;
    public float pos_x_chara;
    public float pos_x_text;

    public bool Deserialize(JSON_AppealLimitedShopMaster json)
    {
      if (json == null)
        return false;
      this.appeal_id = json.fields.appeal_id;
      this.start_at = TimeManager.FromDateTime(DateTime.Parse(json.fields.start_at));
      this.end_at = TimeManager.FromDateTime(DateTime.Parse(json.fields.end_at));
      this.priority = json.fields.priority;
      this.pos_x_chara = json.fields.position_chara;
      this.pos_x_text = json.fields.position_text;
      return true;
    }
  }
}
