﻿// Decompiled with JetBrains decompiler
// Type: SRPG.JSON_GvGRuleParam
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
  public class JSON_GvGRuleParam : JSON_GvGMasterParam
  {
    public string iname;
    public string name;
    public int unit_cnt;
    public int def_unit_cnt;
    public int unit_cool_time;
    public int[] cnds_birth;
  }
}
