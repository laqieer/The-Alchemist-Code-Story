// Decompiled with JetBrains decompiler
// Type: SRPG.JSON_QuestCondParam
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
  public class JSON_QuestCondParam
  {
    public string iname;
    public int plvmax;
    public int plvmin;
    public int ulvmax;
    public int ulvmin;
    public int el_none;
    public int el_fire;
    public int el_watr;
    public int el_wind;
    public int el_thdr;
    public int el_lit;
    public int el_drk;
    public string[] job;
    public int party_type;
    public string[] unit;
    public int sex;
    public int rmax;
    public int rmin;
    public int rmax_ini;
    public int rmin_ini;
    public int hmax;
    public int hmin;
    public int wmax;
    public int wmin;
    public int jobset1;
    public int jobset2;
    public int jobset3;
    public string[] birth;
    public int not_solo;
  }
}
