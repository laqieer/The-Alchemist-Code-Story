// Decompiled with JetBrains decompiler
// Type: SRPG.GuildFacilityLvParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

#nullable disable
namespace SRPG
{
  public class GuildFacilityLvParam
  {
    public int lv;
    public int base_camp;
    public long guild_shop;

    public bool Deserialize(JSON_GuildFacilityLvParam json)
    {
      this.lv = json.lv;
      this.base_camp = json.base_camp;
      this.guild_shop = json.guild_shop;
      return true;
    }
  }
}
