// Decompiled with JetBrains decompiler
// Type: SRPG.UnitPieceShopItem
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;

#nullable disable
namespace SRPG
{
  public class UnitPieceShopItem
  {
    public string IName { get; private set; }

    public bool IsSoldOut { get; private set; }

    public int MaxBuyNum { get; private set; }

    public int TotalBuyNum => this.MaxBuyNum + this.BoughtNum;

    public int BoughtNum { get; private set; }

    public int RemainCount => this.MaxBuyNum;

    public int CostNum { get; private set; }

    public bool IsExpired { get; private set; }

    public DateTime EndAt { get; private set; }

    public bool HasNextStep { get; private set; }

    public bool Deserialize(Json_UnitPieceShopItem json)
    {
      if (json == null)
        return false;
      this.IName = json.iname;
      this.IsSoldOut = json.sold > 0;
      this.MaxBuyNum = json.maxnum;
      this.BoughtNum = json.boughtnum;
      this.CostNum = json.cost_num;
      if (json.expired_at > 0)
      {
        this.IsExpired = true;
        this.EndAt = TimeManager.ServerTime + TimeSpan.FromSeconds((double) json.expired_at);
      }
      this.HasNextStep = json.has_next_step == 1;
      return true;
    }
  }
}
