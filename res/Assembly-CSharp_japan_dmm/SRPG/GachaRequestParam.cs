// Decompiled with JetBrains decompiler
// Type: SRPG.GachaRequestParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

#nullable disable
namespace SRPG
{
  public class GachaRequestParam
  {
    private string m_iname;
    private int m_cost;
    private int m_free;
    private string m_ticket;
    private int m_num;
    private string m_confirm_text;
    private bool m_use_onemore;
    private bool m_no_use_free;
    private int m_redraw_rest;
    private int m_redraw_num;
    private GachaCategory m_category;
    private GachaCostType m_costtype;
    private int m_discount_cost = -1;
    private int m_daily_free;
    private bool m_simple_anim;

    public GachaRequestParam()
    {
      this.m_iname = (string) null;
      this.m_cost = 0;
      this.m_free = 0;
      this.m_ticket = (string) null;
      this.m_num = 0;
      this.m_confirm_text = (string) null;
      this.m_use_onemore = false;
      this.m_no_use_free = false;
      this.m_redraw_rest = 0;
      this.m_redraw_num = 0;
      this.m_category = GachaCategory.NONE;
      this.m_costtype = GachaCostType.NONE;
      this.m_discount_cost = -1;
      this.m_daily_free = 0;
    }

    public GachaRequestParam(
      string _iname,
      int _cost,
      string _confirm_text,
      GachaCostType _cost_type,
      GachaCategory _category,
      bool _use_onemore,
      bool _no_use_free = false)
    {
      this.m_iname = _iname;
      this.m_cost = _cost;
      this.m_confirm_text = _confirm_text;
      this.m_costtype = _cost_type;
      this.m_category = _category;
      this.m_use_onemore = _use_onemore;
      this.m_no_use_free = _no_use_free;
    }

    public GachaRequestParam(string _iname)
    {
      this.m_iname = _iname;
      this.m_cost = 0;
      this.m_confirm_text = string.Empty;
      this.m_costtype = GachaCostType.NONE;
      this.m_category = GachaCategory.NONE;
      this.m_use_onemore = false;
      this.m_no_use_free = false;
      this.m_discount_cost = -1;
    }

    public bool IsGold
    {
      get => this.m_costtype == GachaCostType.GOLD || this.m_costtype == GachaCostType.FREE_GOLD;
    }

    public bool IsSingle => this.m_num == 1;

    public string Iname => this.m_iname;

    public int Cost => this.m_cost;

    public int Free => this.m_free;

    public bool IsFree => this.m_free == 1;

    public string Ticket => this.m_ticket;

    public int Num => this.m_num;

    public bool IsPaid => this.m_costtype == GachaCostType.COIN_P;

    public string ConfirmText => this.m_confirm_text;

    public bool IsTicketGacha => !string.IsNullOrEmpty(this.Ticket);

    public bool IsUseOneMore => this.m_use_onemore;

    public GachaCategory Category => this.m_category;

    public GachaCostType CostType => this.m_costtype;

    public bool IsUseFree => !this.m_no_use_free;

    public bool IsRedrawGacha => this.m_redraw_num > 0;

    public int RedrawRest => this.m_redraw_rest;

    public int RedrawNum => this.m_redraw_num;

    public int ViewRedrawRest
    {
      get => this.m_redraw_rest == this.m_redraw_num ? this.m_redraw_rest - 1 : this.m_redraw_rest;
    }

    public bool IsRedrawConfirm => this.m_redraw_rest < this.m_redraw_num;

    public int DiscountCost => this.m_discount_cost;

    public int FixCost => this.m_discount_cost >= 0 ? this.m_discount_cost : this.m_cost;

    public bool IsDiscount => this.m_discount_cost >= 0;

    public int DailyFree => this.m_daily_free;

    public bool IsDailyFree => this.m_daily_free == 1;

    public bool IsFixFree => this.m_free == 1 || this.m_daily_free == 1;

    public bool IsSimpleAnim => this.m_simple_anim;

    public void SetFree(int _free) => this.m_free = _free;

    public void SetNum(int _num) => this.m_num = _num;

    public void SetTicketInfo(string _ticket_name, int _num = 0)
    {
      this.m_ticket = _ticket_name;
      this.m_num = _num;
    }

    public void SetConfirmText(string _confirm_text) => this.m_confirm_text = _confirm_text;

    public void SetUseOneMore(bool _use_onemore) => this.m_use_onemore = _use_onemore;

    public void SetNoUseFree(bool _no_use_free) => this.m_no_use_free = _no_use_free;

    public void SetRedraw(int _rest, int _num)
    {
      this.m_redraw_rest = _rest;
      this.m_redraw_num = _num;
    }

    public void SetDiscountCost(int _discount) => this.m_discount_cost = _discount;

    public void SetDailyFree(int _daily_free) => this.m_daily_free = _daily_free;

    public void SetSimpleAnim(bool _simple_anim) => this.m_simple_anim = _simple_anim;

    public void ResetExtraParam()
    {
      this.ResetDiscount();
      this.ResetDailyFree();
    }

    public void ResetDiscount()
    {
      if (this.IsDailyFree)
        return;
      this.m_discount_cost = -1;
    }

    public void ResetDailyFree() => this.m_daily_free = 0;
  }
}
