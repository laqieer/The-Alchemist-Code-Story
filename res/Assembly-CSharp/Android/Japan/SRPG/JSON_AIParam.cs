﻿// Decompiled with JetBrains decompiler
// Type: SRPG.JSON_AIParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using System;

namespace SRPG
{
  [Serializable]
  public class JSON_AIParam
  {
    public string iname;
    public int role;
    public int prm;
    public int prmprio;
    public int notprio;
    public int best;
    public int sneak;
    public int notmov;
    public int notact;
    public int notskl;
    public int notavo;
    public int notmpd;
    public int sos;
    public int heal;
    public int gems;
    public int notsup_hp;
    public int notsup_num;
    public int[] skill;
    public int csff;
    public int[] skil_prio;
    public int[] buff_prio;
    public int buff_self;
    public int buff_border;
    public int[] cond_prio;
    public int cond_border;
    public int safe_border;
    public int gosa_border;
    public int use_old_sort;
  }
}
