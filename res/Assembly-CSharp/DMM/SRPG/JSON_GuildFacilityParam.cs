// Decompiled with JetBrains decompiler
// Type: SRPG.JSON_GuildFacilityParam
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
  public class JSON_GuildFacilityParam
  {
    public string iname;
    public string name;
    public string image;
    public int type;
    public int enhance;
    public int increment;
    public long day_limit;
    public int rel_cnds_type;
    public string rel_cnds_val1;
    public int rel_cnds_val2;
    public JSON_GuildFacilityEffectParam[] effects;
  }
}
