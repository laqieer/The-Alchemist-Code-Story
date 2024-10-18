// Decompiled with JetBrains decompiler
// Type: SRPG.JSON_UnitJobOverwriteParam
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
  public class JSON_UnitJobOverwriteParam
  {
    public string unit_iname;
    public string job_iname;
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
  }
}
