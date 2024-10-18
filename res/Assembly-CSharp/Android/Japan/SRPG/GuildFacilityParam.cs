// Decompiled with JetBrains decompiler
// Type: SRPG.GuildFacilityParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;

namespace SRPG
{
  public class GuildFacilityParam
  {
    private string iname;
    private string name;
    private string image;
    private GuildFacilityParam.eFacilityType type;
    private int release_cnds_type;
    private string release_cnds_val1;
    private int release_cnds_val2;
    private GuildFacilityEffectParam[] effects;

    public string Iname
    {
      get
      {
        return this.iname;
      }
    }

    public string Name
    {
      get
      {
        return this.name;
      }
    }

    public string Image
    {
      get
      {
        return this.image;
      }
    }

    public GuildFacilityParam.eFacilityType Type
    {
      get
      {
        return this.type;
      }
    }

    public int ReleaseCndsType
    {
      get
      {
        return this.release_cnds_type;
      }
    }

    public string ReleaseCndsVal1
    {
      get
      {
        return this.release_cnds_val1;
      }
    }

    public int ReleaseCndsVal2
    {
      get
      {
        return this.release_cnds_val2;
      }
    }

    public GuildFacilityEffectParam[] Effects
    {
      get
      {
        return this.effects;
      }
    }

    public bool Deserialize(JSON_GuildFacilityParam json)
    {
      this.iname = json.iname;
      this.name = json.name;
      this.image = json.image;
      this.type = (GuildFacilityParam.eFacilityType) json.type;
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
      GuildFacilityEffectParam facilityEffectParam = new GuildFacilityEffectParam();
      if (this.type == GuildFacilityParam.eFacilityType.BASE_CAMP)
      {
        facilityEffectParam.member_count = MonoSingleton<GameManager>.Instance.MasterParam.FixParam.GuildDefaultMemberMax;
        facilityEffectParam.sub_master = MonoSingleton<GameManager>.Instance.MasterParam.FixParam.GuildDefaultSubMasterMax;
      }
      for (int index = 0; index < this.Effects.Length; ++index)
      {
        if (level >= this.Effects[index].lv)
        {
          if (this.Effects[index].member_count > 0)
            facilityEffectParam.member_count += this.Effects[index].member_count;
          if (this.Effects[index].sub_master > 0)
            facilityEffectParam.sub_master += this.Effects[index].sub_master;
        }
      }
      return facilityEffectParam;
    }

    public enum eFacilityType
    {
      NONE,
      BASE_CAMP,
    }
  }
}
