// Decompiled with JetBrains decompiler
// Type: SRPG.GachaTopParamNew
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;
using System.Collections.Generic;

#nullable disable
namespace SRPG
{
  public class GachaTopParamNew
  {
    public string iname = string.Empty;
    public string category = string.Empty;
    public long startat;
    public long endat;
    public int coin = -1;
    public int coin_p = -1;
    public int gold = -1;
    public List<UnitParam> units;
    public int num;
    public string ticket_iname = string.Empty;
    public int ticket_num;
    public bool step;
    public int step_num;
    public int step_index;
    public bool limit;
    public int limit_num;
    public int limit_stock;
    public bool limit_cnt;
    public int limit_cnt_rest;
    public int limit_cnt_num;
    public string type = string.Empty;
    public string asset_title = string.Empty;
    public string asset_bg = string.Empty;
    public string group = string.Empty;
    public string btext = string.Empty;
    public string confirm = string.Empty;
    public List<GachaBonusParam> bonus_items = new List<GachaBonusParam>();
    public List<ArtifactParam> artifacts;
    public string detail_url = string.Empty;
    public long reset_at;
    public bool disabled;
    public string bonus_msg = string.Empty;
    public int appeal_type;
    public string appeal_message = string.Empty;
    public bool is_hide;
    public bool is_stepup_loop;
    public bool is_free_pause;
    public bool redraw;
    public int redraw_rest;
    public int redraw_num;
    public bool is_discount;
    public int coin_discount = -1;
    public int coin_p_discount = -1;
    public int gold_discount = -1;
    public string discount_item = string.Empty;
    public bool is_rate_view = true;
    public bool is_daily_free;
    public bool is_simple_anim;
    private bool is_pickup_view;
    private GachaPickupSelectType pickup_select_type;
    private int pickup_select_num;
    private List<GachaDropData> pickup_selected = new List<GachaDropData>();

    public void Deserialize(GachaParam param)
    {
      this.iname = param != null ? param.iname : throw new InvalidCastException();
      this.category = param.category;
      this.startat = param.startat;
      this.endat = param.endat;
      this.coin = param.coin;
      this.gold = param.gold;
      this.coin_p = param.coin_p;
      this.units = param.units;
      this.num = param.num;
      this.ticket_iname = param.ticket_iname;
      this.ticket_num = param.ticket_num;
      this.step = param.step;
      this.step_num = param.step_num;
      this.step_index = param.step_index;
      this.limit = param.limit;
      this.limit_num = param.limit_num;
      this.limit_stock = param.limit_stock;
      this.limit_cnt = param.limit_cnt;
      this.limit_cnt_rest = param.limit_cnt_rest;
      this.limit_cnt_num = param.limit_cnt_num;
      this.type = string.Empty;
      this.asset_title = param.asset_title;
      this.asset_bg = param.asset_bg;
      this.group = param.group;
      this.btext = param.btext;
      this.confirm = param.confirm;
      this.bonus_items = param.bonus_items;
      this.artifacts = param.artifacts;
      this.detail_url = param.detail_url;
      this.reset_at = param.reset_at;
      this.disabled = param.disabled;
      this.bonus_msg = param.bonus_msg;
      this.appeal_type = param.appeal_type;
      this.appeal_message = param.appeal_message;
      this.is_hide = param.is_hide;
      this.is_stepup_loop = param.is_loop;
      this.is_free_pause = param.is_free_pause;
      this.redraw = param.redraw;
      this.redraw_rest = param.redraw_rest;
      this.redraw_num = param.redraw_num;
      this.is_discount = param.is_discount;
      this.coin_discount = param.coin_discount;
      this.coin_p_discount = param.coin_p_discount;
      this.gold_discount = param.gold_discount;
      this.discount_item = param.discount_item;
      this.is_rate_view = param.is_rate_view;
      this.is_daily_free = param.is_daily_free;
      this.is_simple_anim = param.is_simple_anim;
      this.is_pickup_view = param.is_pickup_view;
      this.pickup_select_type = (GachaPickupSelectType) param.pickup_select_type;
      this.pickup_select_num = param.pickup_select_num;
      this.pickup_selected.Clear();
      if (param.pickup_selected == null)
        return;
      for (int index = 0; index < param.pickup_selected.Count; ++index)
      {
        GachaDropData gachaDropData = new GachaDropData();
        gachaDropData.Deserialize(param.pickup_selected[index]);
        this.pickup_selected.Add(gachaDropData);
      }
    }

    public long GetTimerAt() => this.reset_at > 0L ? this.reset_at : this.endat;

    public GachaCostType CostType
    {
      get
      {
        GachaCostType costType = GachaCostType.NONE;
        if (this.coin >= 0)
          costType = GachaCostType.COIN;
        else if (this.coin_p >= 0)
          costType = GachaCostType.COIN_P;
        else if (this.gold >= 0)
          costType = GachaCostType.GOLD;
        else if (!string.IsNullOrEmpty(this.ticket_iname) && this.ticket_num > 0)
          costType = GachaCostType.TICKET;
        return costType;
      }
    }

    public GachaCategory Category
    {
      get
      {
        GachaCategory category = GachaCategory.NONE;
        if (this.category.Contains("gold"))
          category = GachaCategory.DEFAULT_NORMAL;
        else if (this.category.Contains("coin"))
          category = GachaCategory.DEFAULT_RARE;
        return category;
      }
    }

    public bool IsOptionUIHide => this.is_hide;

    public bool IsStepUpLoop => this.is_stepup_loop;

    public bool IsFreePause
    {
      get
      {
        bool isFreePause = false;
        if (this.Category != GachaCategory.NONE)
          isFreePause = this.is_free_pause;
        return isFreePause;
      }
    }

    public bool IsUseDiscount => this.is_discount;

    public bool IsDiscount
    {
      get
      {
        if (this.CostType == GachaCostType.COIN)
          return this.coin_discount > -1;
        if (this.CostType == GachaCostType.COIN_P)
          return this.coin_p_discount > -1;
        return this.CostType == GachaCostType.GOLD && this.gold_discount > -1;
      }
    }

    public int Cost
    {
      get
      {
        if (this.CostType == GachaCostType.COIN)
          return this.coin;
        if (this.CostType == GachaCostType.COIN_P)
          return this.coin_p;
        return this.CostType == GachaCostType.GOLD ? this.gold : 0;
      }
    }

    public int DiscountCost
    {
      get
      {
        if (this.CostType == GachaCostType.COIN)
          return this.coin_discount;
        if (this.CostType == GachaCostType.COIN_P)
          return this.coin_p_discount;
        return this.CostType == GachaCostType.GOLD ? this.gold_discount : -1;
      }
    }

    public string UseDiscountItem => this.discount_item;

    public bool IsDefaultRareFree
    {
      get
      {
        return this.Category == GachaCategory.DEFAULT_RARE && this.num == 1 && this.CostType == GachaCostType.COIN && !this.IsFreePause;
      }
    }

    public bool IsDefaultNormalFree
    {
      get => this.Category == GachaCategory.DEFAULT_NORMAL && this.num == 1;
    }

    public bool IsUseOneMore
    {
      get
      {
        if (!string.IsNullOrEmpty(this.ticket_iname))
          return true;
        return !this.limit && !this.step && !this.limit_cnt && !this.redraw;
      }
    }

    public int ShowCurrentStepNum => this.step_index + 1;

    public bool IsPickupView => this.is_pickup_view;

    public GachaPickupSelectType PickupSelectType => this.pickup_select_type;

    public int PickupSelectNum
    {
      get => this.pickup_select_num;
      set => this.pickup_select_num = value;
    }

    public bool IsUsePickupSelect => this.PickupSelectType != GachaPickupSelectType.None;

    public bool IsSelectedPickup
    {
      get
      {
        return this.IsUsePickupSelect && this.pickup_selected != null && this.pickup_selected.Count > 0;
      }
    }

    public List<GachaDropData> SelectedPickupList => this.pickup_selected;
  }
}
