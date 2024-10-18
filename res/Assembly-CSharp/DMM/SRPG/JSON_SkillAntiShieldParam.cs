// Decompiled with JetBrains decompiler
// Type: SRPG.JSON_SkillAntiShieldParam
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
  public class JSON_SkillAntiShieldParam
  {
    public string iname;
    public int is_ignore;
    public short ignore_rate_ini;
    public short ignore_rate_max;
    public int is_destroy;
    public short destroy_rate_ini;
    public short destroy_rate_max;
  }
}
