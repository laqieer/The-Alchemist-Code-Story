﻿// Decompiled with JetBrains decompiler
// Type: SRPG.MultiPlayResumeAbilChg
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
  public class MultiPlayResumeAbilChg
  {
    public MultiPlayResumeAbilChg.Data[] acd;

    [MessagePackObject(true)]
    [Serializable]
    public class Data
    {
      public string fid;
      public string tid;
      public int tur;
      public int irs;
      public int exp;
      public int iif;
    }
  }
}
