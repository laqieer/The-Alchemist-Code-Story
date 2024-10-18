// Decompiled with JetBrains decompiler
// Type: SRPG.GuildFacilityParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;

#nullable disable
namespace SRPG
{
  public class GuildFacilityParam
  {
    private string iname;
    private string name;
    private string image;
    private GuildFacilityParam.eFacilityType type;
    private GuildFacilityParam.eEnhanceType enhance_type;
    private int increment;
    private long day_limit_invest;
    private int release_cnds_type;
    private string release_cnds_val1;
    private int release_cnds_val2;
    private GuildFacilityEffectParam[] effects;

    public string Iname => this.iname;

    public string Name => this.name;

    public string Image => this.image;

    public GuildFacilityParam.eFacilityType Type => this.type;

    public GuildFacilityParam.eEnhanceType EnhanceType => this.enhance_type;

    public int Increment => this.increment;

    public long DayLimitInvest => this.day_limit_invest;

    public int ReleaseCndsType => this.release_cnds_type;

    public string ReleaseCndsVal1 => this.release_cnds_val1;

    public int ReleaseCndsVal2 => this.release_cnds_val2;

    public GuildFacilityEffectParam[] Effects => this.effects;

    public bool Deserialize(JSON_GuildFacilityParam json)
    {
      this.iname = json.iname;
      this.name = json.name;
      this.image = json.image;
      this.type = (GuildFacilityParam.eFacilityType) json.type;
      this.enhance_type = (GuildFacilityParam.eEnhanceType) json.enhance;
      this.increment = json.increment;
      this.day_limit_invest = json.day_limit;
      this.release_cnds_type = json.rel_cnds_type;
      this.release_cnds_val1 = json.rel_cnds_val1;
      this.release_cnds_val2 = json.rel_cnds_val2;
      this.effects = new GuildFacilityEffectParam[json.effects.Length];
      for (int index = 0; index < json.effects.Length; ++index)
      {
        this.effects[index] = new GuildFacilityEffectParam();
        this.effects[index].Deserialize(json.effects[index]);
      }
      return true;
    }

    public GuildFacilityEffectParam GetEffect(int level)
    {
      GuildFacilityEffectParam effect = new GuildFacilityEffectParam();
      if (this.type == GuildFacilityParam.eFacilityType.BASE_CAMP)
      {
        effect.member_count = MonoSingleton<GameManager>.Instance.MasterParam.FixParam.GuildDefaultMemberMax;
        effect.sub_master = MonoSingleton<GameManager>.Instance.MasterParam.FixParam.GuildDefaultSubMasterMax;
      }
      for (int index = 0; index < this.Effects.Length; ++index)
      {
        if (level >= this.Effects[index].lv)
        {
          if (this.Effects[index].member_count > 0)
            effect.member_count += this.Effects[index].member_count;
          if (this.Effects[index].sub_master > 0)
            effect.sub_master += this.Effects[index].sub_master;
          if (this.Effects[index].shop_count > 0)
            effect.shop_count += this.Effects[index].shop_count;
        }
      }
      return effect;
    }

    public int GetGuildShopLevelRequiredUnlockShop(int no)
    {
      if (this.Effects == null)
        return 0;
      int num = 0;
      for (int index = 0; index < this.Effects.Length; ++index)
      {
        num += this.Effects[index].shop_count;
        if (num >= no)
          return this.Effects[index].lv;
      }
      return 0;
    }

    public enum eFacilityType
    {
      NONE,
      BASE_CAMP,
      GUILD_SHOP,
    }

    public enum eEnhanceType
    {
      NONE,
      ITEM,
      GOLD,
    }
  }
}
