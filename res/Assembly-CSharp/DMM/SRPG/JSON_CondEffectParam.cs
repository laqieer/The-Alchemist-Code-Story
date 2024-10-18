// Decompiled with JetBrains decompiler
// Type: SRPG.JSON_CondEffectParam
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
  public class JSON_CondEffectParam
  {
    public string iname;
    public string job;
    public string buki;
    public string birth;
    public int sex;
    public int elem;
    public int cond;
    public int type;
    public int vini;
    public int vmax;
    public int rini;
    public int rmax;
    public int tini;
    public int tmax;
    public int chktgt;
    public int timing;
    public int[] conds;
    public int v_poi;
    public int v_poifix;
    public int v_par;
    public int v_blihit;
    public int v_bliavo;
    public int v_dea;
    public int v_beratk;
    public int v_berdef;
    public int v_fast;
    public int v_slow;
    public int v_don;
    public int v_ahp;
    public int v_ahpfix;
    public int v_amp;
    public int v_ampfix;
    public int curse;
    public string[] buffs;
    public int is_lb_dupli;
    public int is_lb_resist;
    public string tag;
    public string un_group;
    public string[] custom_targets;
  }
}
