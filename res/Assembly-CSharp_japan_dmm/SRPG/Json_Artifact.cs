// Decompiled with JetBrains decompiler
// Type: SRPG.Json_Artifact
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
  public class Json_Artifact
  {
    public string iname;
    public long iid;
    public int rare;
    public int exp;
    public int fav;
    public int inspiration_skill_slots;
    public Json_InspirationSkill[] inspiration_skills;
  }
}
