// Decompiled with JetBrains decompiler
// Type: SRPG.GuildFacilityLvParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

namespace SRPG
{
  public class GuildFacilityLvParam
  {
    public int lv;
    public int base_camp;

    public bool Deserialize(JSON_GuildFacilityLvParam json)
    {
      this.lv = json.lv;
      this.base_camp = json.base_camp;
      return true;
    }
  }
}
