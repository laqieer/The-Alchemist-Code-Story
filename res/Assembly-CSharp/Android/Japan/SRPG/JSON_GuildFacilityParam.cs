// Decompiled with JetBrains decompiler
// Type: SRPG.JSON_GuildFacilityParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using System;

namespace SRPG
{
  [Serializable]
  public class JSON_GuildFacilityParam
  {
    public string iname;
    public string name;
    public string image;
    public int type;
    public int rel_cnds_type;
    public string rel_cnds_val1;
    public int rel_cnds_val2;
    public JSON_GuildFacilityEffectParam[] effects;
  }
}
