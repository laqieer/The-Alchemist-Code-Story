﻿// Decompiled with JetBrains decompiler
// Type: SRPG.JSON_TrickParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using MessagePack;
using System;

#nullable disable
namespace SRPG
{
  [MessagePackObject(true)]
  [Serializable]
  public class JSON_TrickParam
  {
    public string iname;
    public string name;
    public string expr;
    public int dmg_type;
    public int dmg_val;
    public int calc;
    public int elem;
    public int atk_det;
    public string buff;
    public string cond;
    public int kb_rate;
    public int kb_val;
    public int target;
    public int visual;
    public int count;
    public int clock;
    public int is_no_ow;
    public string marker;
    public string effect;
    public int eff_target;
    public int eff_shape;
    public int eff_scope;
    public int eff_height;
    public int ig_mt_num;
    public int[] ig_mts;
    public int is_rein;
  }
}
