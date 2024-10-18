// Decompiled with JetBrains decompiler
// Type: SRPG.JSON_SkillAbilityDeriveParam
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
  public class JSON_SkillAbilityDeriveParam
  {
    public string iname;
    public int trig_type_1;
    public string trig_iname_1;
    public int trig_type_2;
    public string trig_iname_2;
    public int trig_type_3;
    public string trig_iname_3;
    public string[] base_abils;
    public string[] derive_abils;
    public string[] base_skills;
    public string[] derive_skills;
  }
}
