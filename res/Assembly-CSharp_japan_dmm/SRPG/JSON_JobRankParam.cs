// Decompiled with JetBrains decompiler
// Type: SRPG.JSON_JobRankParam
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
  public class JSON_JobRankParam
  {
    public int chcost;
    public string chitm1;
    public string chitm2;
    public string chitm3;
    public int chnum1;
    public int chnum2;
    public int chnum3;
    public int cost;
    public string eqid1;
    public string eqid2;
    public string eqid3;
    public string eqid4;
    public string eqid5;
    public string eqid6;
    public string learn1;
    public string learn2;
    public string learn3;
  }
}
