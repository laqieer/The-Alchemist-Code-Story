// Decompiled with JetBrains decompiler
// Type: SRPG.GachaParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
namespace SRPG
{
  public class GachaParam
  {
    public string iname = string.Empty;
    public string category = string.Empty;
    public long startat;
    public long endat;
    public int gold = -1;
    public int coin = -1;
    public int coin_p = -1;
    public string ticket_iname = string.Empty;
    public int ticket_num;
    public int num;
    public string msg = string.Empty;
    public string name = string.Empty;
    public string movie = string.Empty;
    public string bg = string.Empty;
    public string asset_bg = string.Empty;
    public string asset_title = string.Empty;
    public string detail_url = string.Empty;
    public string[] ext_type;
    public List<UnitData> units2;
    public List<UnitParam> units;
    public bool step;
    public int step_num;
    public int step_index;
    public bool limit;
    public int limit_num;
    public int limit_stock;
    public bool limit_cnt;
    public int limit_cnt_rest;
    public int limit_cnt_num;
    public string group = string.Empty;
    public string btext = string.Empty;
    public string confirm = string.Empty;
    public List<GachaBonusParam> bonus_items = new List<GachaBonusParam>();
    public List<ArtifactParam> artifacts;
    public long reset_at;
    public bool disabled;
    public string bonus_msg = string.Empty;
    public int appeal_type;
    public string appeal_message = string.Empty;
    public bool is_hide;
    public bool is_loop;
    public bool is_free_pause;
    public bool redraw;
    public int redraw_rest;
    public int redraw_num;
    public bool is_discount;
    public int gold_discount = -1;
    public int coin_discount = -1;
    public int coin_p_discount = -1;
    public string discount_item = string.Empty;
    public bool is_rate_view = true;
    public bool is_daily_free;
    public bool is_simple_anim;
    public int pickup_select_type;
    public int pickup_select_num;
    public bool is_pickup_view = true;
    public List<GachaSelectedPickup> pickup_selected = new List<GachaSelectedPickup>();

    public void Deserialize(Json_GachaParam json)
    {
      if (json == null)
        throw new InvalidCastException();
      if (this.units == null && json.units != null)
      {
        this.units = new List<UnitParam>(json.units.Length);
        for (int index = 0; index < json.units.Length; ++index)
        {
          UnitParam unitParam = MonoSingleton<GameManager>.Instance.MasterParam.GetUnitParam(json.units[index]);
          if (unitParam != null)
            this.units.Add(unitParam);
        }
      }
      if (this.artifacts == null && json.pickup_art != null)
      {
        this.artifacts = new List<ArtifactParam>(json.pickup_art.Length);
        for (int index = 0; index < json.pickup_art.Length; ++index)
        {
          ArtifactParam artifactParam = MonoSingleton<GameManager>.Instance.MasterParam.GetArtifactParam(json.pickup_art[index]);
          if (artifactParam != null)
            this.artifacts.Add(artifactParam);
        }
      }
      this.iname = json.iname;
      this.category = json.cat;
      this.startat = json.startat;
      this.endat = json.endat;
      this.num = json.num;
      this.msg = json.msg;
      if (json.cost != null)
      {
        this.gold = (int) json.cost.gold;
        this.coin = (int) json.cost.coin;
        this.coin_p = (int) json.cost.coin_p;
        if (json.cost.ticket != null)
        {
          this.ticket_iname = json.cost.ticket.iname;
          this.ticket_num = json.cost.ticket.num;
        }
      }
      this.name = json.name;
      this.movie = json.movie;
      this.bg = json.bg;
      this.asset_bg = json.asset_bg;
      this.asset_title = json.asset_title;
      this.detail_url = json.detail_url;
      if (json.ext_type != null)
      {
        this.ext_type = new string[json.ext_type.Length];
        for (int index = 0; index < json.ext_type.Length; ++index)
          this.ext_type[index] = json.ext_type[index];
      }
      this.step = false;
      this.step_num = 0;
      this.step_index = 0;
      this.limit = false;
      this.limit_num = 0;
      this.limit_stock = 0;
      this.limit_cnt = false;
      this.limit_cnt_rest = 0;
      this.limit_cnt_num = 0;
      this.reset_at = 0L;
      this.disabled = false;
      this.redraw = false;
      this.redraw_rest = 0;
      this.redraw_num = 0;
      if (json.ext_param != null)
      {
        if (json.ext_param.step != null)
        {
          this.step = true;
          this.step_num = json.ext_param.step.num;
          this.step_index = json.ext_param.step.index;
        }
        if (json.ext_param.limit != null)
        {
          this.limit = true;
          this.limit_num = json.ext_param.limit.num;
          this.limit_stock = json.ext_param.limit.stock;
        }
        if (json.ext_param.limit_cnt != null)
        {
          this.limit_cnt = true;
          this.limit_cnt_rest = json.ext_param.limit_cnt.rest;
          this.limit_cnt_num = json.ext_param.limit_cnt.num;
        }
        if (json.ext_param.redraw != null)
        {
          this.redraw = true;
          this.redraw_rest = json.ext_param.redraw.rest;
          this.redraw_num = json.ext_param.redraw.num;
        }
        this.reset_at = json.ext_param.next_reset_time;
        this.disabled = json.disabled == 1;
      }
      this.group = json.group;
      this.btext = json.btext;
      this.confirm = json.confirm;
      if (json.bonus_items != null && json.bonus_items.Length > 0)
      {
        for (int index = 0; index < json.bonus_items.Length; ++index)
          this.bonus_items.Add(new GachaBonusParam()
          {
            iname = json.bonus_items[index].iname,
            num = json.bonus_items[index].num
          });
      }
      this.detail_url = json.detail_url;
      this.bonus_msg = json.bonus_msg;
      if (json.appeal != null)
      {
        this.appeal_type = json.appeal.type;
        this.appeal_message = json.appeal.message;
      }
      this.is_hide = json.isHide == 1;
      this.is_loop = json.isLoop == 1;
      this.is_free_pause = json.isFreePause == 1;
      this.is_discount = json.isDiscount == 1;
      if (json.cost_discount != null)
      {
        this.gold_discount = (int) json.cost_discount.gold;
        this.coin_discount = (int) json.cost_discount.coin;
        this.coin_p_discount = (int) json.cost_discount.coin_p;
        this.discount_item = json.cost_discount.discount_item;
        if (GameUtility.IsDebugBuild)
        {
          if ((double) json.cost_discount.gold >= 0.0 && (double) json.cost_discount.gold - (double) Mathf.Floor(json.cost_discount.gold) > 0.0)
            DebugUtility.LogError("[Error]iname:" + this.iname + "の割引後のコストに小数点が含まれています.");
          if ((double) json.cost_discount.coin >= 0.0 && (double) json.cost_discount.coin - (double) Mathf.Floor(json.cost_discount.coin) > 0.0)
            DebugUtility.LogError("[Error]iname:" + this.iname + "の割引後のコストに小数点が含まれています.");
          if ((double) json.cost_discount.coin_p >= 0.0 && (double) json.cost_discount.coin_p - (double) Mathf.Floor(json.cost_discount.coin_p) > 0.0)
            DebugUtility.LogError("[Error]iname:" + this.iname + "の割引後のコストに小数点が含まれています.");
        }
      }
      this.is_rate_view = json.isRateViewOff == 0;
      this.is_daily_free = json.isDailyFree == 1;
      this.is_simple_anim = json.isSimpleAnim == 1;
      this.is_pickup_view = json.isPickupViewOff == 0;
      this.pickup_select_type = json.pickup_select_type;
      this.pickup_select_num = json.pickup_select_num;
      this.pickup_selected.Clear();
      if (json.pickup_selected == null)
        return;
      for (int index = 0; index < json.pickup_selected.Length; ++index)
        this.pickup_selected.Add(new GachaSelectedPickup()
        {
          iname = json.pickup_selected[index].iname,
          itype = json.pickup_selected[index].itype,
          num = json.pickup_selected[index].num
        });
    }
  }
}
