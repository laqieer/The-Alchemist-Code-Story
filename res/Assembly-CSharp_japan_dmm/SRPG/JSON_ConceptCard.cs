// Decompiled with JetBrains decompiler
// Type: SRPG.JSON_ConceptCard
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
  public class JSON_ConceptCard
  {
    public long iid;
    public string iname;
    public int exp;
    public int trust;
    public int fav;
    public int trust_bonus;
    public int plus;
    public int leaderskill;

    [IgnoreMember]
    public bool IsEmptyDummyData => this.iid == 0L;

    [IgnoreMember]
    public bool EnableLeaderSkill => this.leaderskill == 1;
  }
}
