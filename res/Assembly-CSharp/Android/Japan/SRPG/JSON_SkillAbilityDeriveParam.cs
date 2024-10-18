// Decompiled with JetBrains decompiler
// Type: SRPG.JSON_SkillAbilityDeriveParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using System;

namespace SRPG
{
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
