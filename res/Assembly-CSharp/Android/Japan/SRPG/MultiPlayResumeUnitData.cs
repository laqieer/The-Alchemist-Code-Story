﻿// Decompiled with JetBrains decompiler
// Type: SRPG.MultiPlayResumeUnitData
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using System;

namespace SRPG
{
  [Serializable]
  public class MultiPlayResumeUnitData
  {
    public int ist = -1;
    public string name;
    public int hp;
    public int chp;
    public int gem;
    public int dir;
    public int x;
    public int y;
    public int target;
    public int ragetarget;
    public string castskill;
    public int chargetime;
    public int casttime;
    public int[] castgrid;
    public int casttarget;
    public int castindex;
    public int grid_w;
    public int grid_h;
    public int isDead;
    public int deathcnt;
    public int autojewel;
    public int waitturn;
    public int moveturn;
    public int actcnt;
    public int turncnt;
    public int trgcnt;
    public int killcnt;
    public int[] etr;
    public int aiindex;
    public int aiturn;
    public int aipatrol;
    public int search;
    public int entry;
    public int to_dying;
    public int paralyse;
    public int flag;
    public int ctx;
    public int cty;
    public string boi;
    public int boc;
    public int own;
    public int isd;
    public string did;
    public int dfu;
    public int drt;
    public int okd;
    public MultiPlayResumeBuff[] buff;
    public MultiPlayResumeBuff[] cond;
    public MultiPlayResumeShield[] shields;
    public string[] hpis;
    public MultiPlayResumeMhmDmg[] mhm_dmgs;
    public MultiPlayResumeAbilChg[] abilchgs;
    public MultiPlayResumeAddedAbil[] addedabils;
    public string[] skillname;
    public int[] skillcnt;
  }
}
