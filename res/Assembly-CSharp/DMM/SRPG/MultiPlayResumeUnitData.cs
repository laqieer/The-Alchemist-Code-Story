﻿// Decompiled with JetBrains decompiler
// Type: SRPG.MultiPlayResumeUnitData
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using MessagePack;
using System;
using System.Collections.Generic;

#nullable disable
namespace SRPG
{
  [MessagePackObject(true)]
  [Serializable]
  public class MultiPlayResumeUnitData
  {
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
    public int ist = -1;
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
    public MultiPlayResumeFtgt[] tfl;
    public MultiPlayResumeFtgt[] ffl;
    public MultiPlayResumeAbilChg[] abilchgs;
    public MultiPlayResumeAddedAbil[] addedabils;
    public List<MultiPlayResumeProtect> protects = new List<MultiPlayResumeProtect>();
    public List<MultiPlayResumeProtect> guards = new List<MultiPlayResumeProtect>();
    public string[] skillname;
    public int[] skillcnt;
  }
}
