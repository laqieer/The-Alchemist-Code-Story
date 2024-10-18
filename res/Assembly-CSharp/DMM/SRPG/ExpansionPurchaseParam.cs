// Decompiled with JetBrains decompiler
// Type: SRPG.ExpansionPurchaseParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System.Collections.Generic;

#nullable disable
namespace SRPG
{
  public class ExpansionPurchaseParam
  {
    private string mIname;
    private ExpansionPurchaseParam.eExpansionType mExpansionType;
    private string mGroup;
    private string mBuyCoinProduct;
    private int mPeriod;
    private int mValue;

    public string Iname => this.mIname;

    public ExpansionPurchaseParam.eExpansionType ExpansionType => this.mExpansionType;

    public string Group => this.mGroup;

    public string BuyCoinProduct => this.mBuyCoinProduct;

    public int Period => this.mPeriod;

    public int Value => this.mValue;

    public bool Deserialize(JSON_ExpansionPurchaseParam json)
    {
      this.mIname = json.iname;
      this.mExpansionType = (ExpansionPurchaseParam.eExpansionType) json.expansion_type;
      this.mGroup = json.group;
      this.mBuyCoinProduct = json.buy_coin_product;
      this.mPeriod = json.period;
      this.mValue = json.value;
      return true;
    }

    public static void Deserialize(
      ref List<ExpansionPurchaseParam> ref_params,
      JSON_ExpansionPurchaseParam[] json)
    {
      if (ref_params == null)
        ref_params = new List<ExpansionPurchaseParam>();
      ref_params.Clear();
      if (json == null)
        return;
      foreach (JSON_ExpansionPurchaseParam json1 in json)
      {
        ExpansionPurchaseParam expansionPurchaseParam = new ExpansionPurchaseParam();
        expansionPurchaseParam.Deserialize(json1);
        ref_params.Add(expansionPurchaseParam);
      }
    }

    public enum eExpansionType
    {
      None,
      TripleSpeed,
      AutoBox,
      ExtraCount,
      ChallengeCount,
    }
  }
}
