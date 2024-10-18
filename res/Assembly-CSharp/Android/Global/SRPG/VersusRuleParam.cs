﻿// Decompiled with JetBrains decompiler
// Type: SRPG.VersusRuleParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using System;

namespace SRPG
{
  public class VersusRuleParam
  {
    public static readonly int THREE_ON_THREE = 3;
    public static readonly int FIVE_ON_FIVE = 5;
    public int id;
    public int coin;
    public int coinrate;
    public DateTime begin_at;
    public DateTime end_at;
    public VS_MODE mode;

    public bool Deserialize(JSON_VersusRule json)
    {
      if (json == null)
        return false;
      this.id = json.id;
      this.mode = (VS_MODE) json.vsmode;
      this.coin = json.getcoin;
      this.coinrate = json.rate <= 0 ? 1 : json.rate;
      try
      {
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
