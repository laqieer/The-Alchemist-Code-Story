// Decompiled with JetBrains decompiler
// Type: SRPG.GachaButtonParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

#nullable disable
namespace SRPG
{
  public class GachaButtonParam
  {
    private int m_Cost;
    private int m_StepIndex;
    private int m_StepMax;
    private int m_TicketNum;
    private int m_ExecNum;
    private int m_AppealType;
    private string m_ButtonText = string.Empty;
    private string m_AppealText = string.Empty;
    private bool m_IsShowStepup = true;
    private bool m_IsNoUseFree;
    private GachaCostType m_CostType;
    private GachaCategory m_Category;
    private int m_CostDiscount = -1;
    private string m_DiscountItem = string.Empty;
    private bool m_DailyFree;
    private bool m_IsUsePickupSelect;
    private bool m_IsSelectedPickup;

    public GachaButtonParam()
    {
      this.m_Cost = 0;
      this.m_StepIndex = 0;
      this.m_StepMax = 0;
      this.m_TicketNum = 0;
      this.m_ExecNum = 0;
      this.m_ButtonText = string.Empty;
      this.m_AppealText = string.Empty;
      this.m_IsShowStepup = true;
      this.m_IsNoUseFree = false;
      this.m_CostType = GachaCostType.NONE;
      this.m_Category = GachaCategory.NONE;
      this.m_CostDiscount = -1;
      this.m_DiscountItem = string.Empty;
      this.m_DailyFree = false;
      this.m_IsUsePickupSelect = false;
      this.m_IsSelectedPickup = false;
    }

    public GachaButtonParam(GachaTopParamNew param)
    {
      this.m_CostType = param.CostType;
      this.m_Cost = param.Cost;
      this.m_StepIndex = param.step_index;
      this.m_StepMax = param.step_num;
      this.m_TicketNum = param.ticket_num;
      this.m_ExecNum = param.num;
      this.m_AppealType = param.appeal_type;
      this.m_AppealText = param.appeal_message;
      this.m_ButtonText = param.btext;
      this.m_IsShowStepup = !param.IsOptionUIHide;
      this.m_IsNoUseFree = param.IsFreePause;
      this.m_Category = param.Category;
      this.m_CostDiscount = param.DiscountCost;
      this.m_DiscountItem = param.discount_item;
      this.m_DailyFree = param.is_daily_free;
      this.m_IsUsePickupSelect = param.IsUsePickupSelect;
      this.m_IsSelectedPickup = param.IsSelectedPickup;
    }

    public int Cost => this.m_Cost;

    public int StepIndex => this.m_StepIndex;

    public int StepMax => this.m_StepMax;

    public int TicketNum => this.m_TicketNum;

    public int ExecNum => this.m_ExecNum;

    public int AppealType => this.m_AppealType;

    public string ButtonText => this.m_ButtonText;

    public string AppealText => this.m_AppealText;

    public bool IsShowStepup => this.m_IsShowStepup;

    public GachaCostType CostType => this.m_CostType;

    public GachaCategory Category => this.m_Category;

    public bool IsNoUseFree => this.m_Category != GachaCategory.NONE && this.m_IsNoUseFree;

    public int CostDiscount => this.m_CostDiscount;

    public int FixCost => this.CostDiscount >= 0 ? this.CostDiscount : this.Cost;

    public string DiscountItem => this.m_DiscountItem;

    public bool IsDailyFree => this.m_DailyFree;

    public bool IsUsePickupSelect => this.m_IsUsePickupSelect;

    public bool IsSelectedPickup => this.m_IsSelectedPickup;

    public bool IsStepUp() => this.m_StepMax > 0;

    public bool IsDiscount() => this.m_CostDiscount >= 0;

    public bool IsDiscountCostFree() => this.IsDiscount() && this.m_CostDiscount == 0;
  }
}
