// Decompiled with JetBrains decompiler
// Type: SRPG.ItemParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using MessagePack;
using System;

#nullable disable
namespace SRPG
{
  [MessagePackObject(true)]
  public class ItemParam
  {
    public const string CoinID = "$COIN";
    private const string ITEM_EXPR_PREFIX = "_EXPR";
    private const string ITEM_FLAVOR_PREFIX = "_FLAVOR";
    public int no;
    public string iname;
    public string name;
    public EItemType type;
    public EItemTabType tabtype;
    public int rare;
    public int cap;
    public int invcap;
    public int equipLv;
    public int coin;
    public int tour_coin;
    public int arena_coin;
    public int multi_coin;
    public int piece_point;
    public int buy;
    public int sell;
    public int enhace_cost;
    public int enhace_point;
    public int facility_point;
    public int value;
    public string icon;
    public string skill;
    public string recipe;
    public bool is_valuables;
    public byte cmn_type;
    public GalleryVisibilityType gallery_view;
    public DateTime begin_at;
    public DateTime end_at;

    public string Expr => this.GetText("external_item", this.iname + "_EXPR");

    public string Flavor => this.GetText("external_item", this.iname + "_FLAVOR");

    public RecipeParam Recipe
    {
      get
      {
        return !string.IsNullOrEmpty(this.recipe) ? MonoSingleton<GameManager>.GetInstanceDirect().MasterParam.GetRecipeParam(this.recipe) : (RecipeParam) null;
      }
    }

    public bool IsCommon => this.cmn_type > (byte) 0;

    public bool Deserialize(JSON_ItemParam json)
    {
      if (json == null)
        return false;
      this.iname = json.iname;
      this.name = json.name;
      this.type = (EItemType) json.type;
      this.tabtype = (EItemTabType) json.tabtype;
      this.rare = json.rare;
      this.cap = json.cap;
      this.invcap = json.invcap;
      this.equipLv = Math.Max(json.eqlv, 1);
      this.coin = json.coin;
      this.tour_coin = json.tc;
      this.arena_coin = json.ac;
      this.multi_coin = json.mc;
      this.piece_point = json.pp;
      this.buy = json.buy;
      this.sell = json.sell;
      this.enhace_cost = json.encost;
      this.enhace_point = json.enpt;
      this.facility_point = json.facilitypt;
      this.value = json.val;
      this.icon = json.icon;
      this.skill = json.skill;
      this.recipe = json.recipe;
      this.is_valuables = json.is_valuables > 0;
      this.cmn_type = json.cmn_type;
      this.gallery_view = (GalleryVisibilityType) json.gallery_view;
      this.begin_at = DateTime.MinValue;
      if (!string.IsNullOrEmpty(json.begin_at))
        DateTime.TryParse(json.begin_at, out this.begin_at);
      this.end_at = DateTime.MaxValue;
      if (!string.IsNullOrEmpty(json.end_at))
        DateTime.TryParse(json.end_at, out this.end_at);
      return true;
    }

    public int GetPiercePoint()
    {
      return this.type != EItemType.UnitPiece ? 0 : (int) MonoSingleton<GameManager>.GetInstanceDirect().GetRarityParam(this.rare).PieceToPoint;
    }

    public int GetEnhanceRankCap()
    {
      return this.type != EItemType.Equip ? 1 : (int) MonoSingleton<GameManager>.GetInstanceDirect().GetRarityParam(this.rare).EquipEnhanceParam.rankcap;
    }

    public bool CheckEquipEnhanceMaterial()
    {
      EItemType type = this.type;
      switch (type)
      {
        case EItemType.ItemPiece:
        case EItemType.Equip:
        case EItemType.Material:
        case EItemType.ExpUpEquip:
        case EItemType.ItemPiecePiece:
          return true;
        default:
          if (type != EItemType.ArtifactPiece && type != EItemType.ExpGuildFacility)
            return false;
          goto case EItemType.ItemPiece;
      }
    }

    public bool CheckCanShowInList()
    {
      switch (this.type)
      {
        case EItemType.Other:
        case EItemType.UnitSkin:
        case EItemType.EventCoin:
        case EItemType.Award:
          return false;
        default:
          return true;
      }
    }

    public bool IsLimited
    {
      get
      {
        return (!(this.begin_at == DateTime.MinValue) ? 0 : (this.end_at == DateTime.MaxValue ? 1 : 0)) == 0;
      }
    }

    public bool IsExpire
    {
      get
      {
        return this.IsLimited && (!(this.begin_at <= TimeManager.ServerTime) ? 0 : (TimeManager.ServerTime <= this.end_at ? 1 : 0)) == 0;
      }
    }

    public int GetFacilityPoint(GuildFacilityParam.eFacilityType type)
    {
      return type == GuildFacilityParam.eFacilityType.NONE || type == GuildFacilityParam.eFacilityType.BASE_CAMP ? this.facility_point : this.facility_point;
    }

    public override string ToString() => string.Format("{0} [ItemParam]", (object) this.iname);

    public int GetBuyNum(ESaleType type)
    {
      switch (type)
      {
        case ESaleType.Gold:
          return this.buy;
        case ESaleType.Coin:
          return this.coin;
        case ESaleType.TourCoin:
          return this.tour_coin;
        case ESaleType.ArenaCoin:
          return this.arena_coin;
        case ESaleType.PiecePoint:
          return this.piece_point;
        case ESaleType.MultiCoin:
          return this.multi_coin;
        case ESaleType.EventCoin:
          return 0;
        case ESaleType.Coin_P:
          return this.coin;
        default:
          return 0;
      }
    }

    public string GetText(string table, string key)
    {
      string str = LocalizedText.Get(table + "." + key);
      return str.Equals(key) ? string.Empty : str;
    }
  }
}
