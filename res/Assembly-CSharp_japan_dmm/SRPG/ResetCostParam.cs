// Decompiled with JetBrains decompiler
// Type: SRPG.ResetCostParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System;
using System.Collections.Generic;

#nullable disable
namespace SRPG
{
  public class ResetCostParam
  {
    private string iname;
    private List<ResetCostInfoParam> cost = new List<ResetCostInfoParam>();

    public string Iname => this.iname;

    public List<ResetCostInfoParam> Cost => this.cost;

    public void Deserialize(JSON_ResetCostParam json)
    {
      this.iname = json.iname;
      this.cost.Clear();
      for (int index = 0; index < json.cost.Length; ++index)
      {
        ResetCostInfoParam resetCostInfoParam = new ResetCostInfoParam();
        resetCostInfoParam.Deserialize(json.cost[index]);
        this.cost.Add(resetCostInfoParam);
      }
    }

    public static void Deserialize(
      ref Dictionary<string, ResetCostParam> param,
      JSON_ResetCostParam[] json)
    {
      if (json == null || param == null)
        return;
      param.Clear();
      for (int index = 0; index < json.Length; ++index)
      {
        ResetCostParam resetCostParam = new ResetCostParam();
        resetCostParam.Deserialize(json[index]);
        param.Add(resetCostParam.iname, resetCostParam);
      }
    }

    public ResetCostInfoParam GetResetCostInfo(eResetCostType cost_type)
    {
      return this.cost.Find((Predicate<ResetCostInfoParam>) (c => c.Type == cost_type));
    }

    public bool IsEnableCoinCost()
    {
      ResetCostInfoParam resetCostInfo = this.GetResetCostInfo(eResetCostType.Coin);
      return resetCostInfo != null && resetCostInfo.Num.Count > 0;
    }

    public bool IsEnableItemCost()
    {
      ResetCostInfoParam resetCostInfo = this.GetResetCostInfo(eResetCostType.Item);
      return resetCostInfo != null && MonoSingleton<GameManager>.Instance.MasterParam.GetItemParam(resetCostInfo.Item) != null && resetCostInfo.Num.Count > 0;
    }
  }
}
