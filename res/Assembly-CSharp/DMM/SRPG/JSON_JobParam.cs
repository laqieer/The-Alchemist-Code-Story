// Decompiled with JetBrains decompiler
// Type: SRPG.JSON_JobParam
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
  public class JSON_JobParam
  {
    public string iname;
    public string name;
    public string expr;
    public string mdl;
    public string ac2d;
    public string mdlp;
    public string pet;
    public string buki;
    public string origin;
    public int type;
    public int role;
    public int jmov;
    public int jjmp;
    public string wepmdl;
    public string atkskl;
    public string atkfi;
    public string atkwa;
    public string atkwi;
    public string atkth;
    public string atksh;
    public string atkda;
    public string fixabl;
    public string artifact;
    public string ai;
    public string master;
    public string me_abl;
    public int is_me_rr;
    public string desc_ch;
    public string desc_ot;
    public int hp;
    public int mp;
    public int atk;
    public int def;
    public int mag;
    public int mnd;
    public int dex;
    public int spd;
    public int cri;
    public int luk;
    public int avoid;
    public int inimp;
    public JSON_JobRankParam[] ranks;
    public string unit_image;
    public int mov_type;
    public int is_riding;
    public int no_pass;
    public string[] tags;
    public string buff;
  }
}
