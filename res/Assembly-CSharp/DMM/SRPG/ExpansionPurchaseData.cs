// Decompiled with JetBrains decompiler
// Type: SRPG.ExpansionPurchaseData
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System;

#nullable disable
namespace SRPG
{
  public class ExpansionPurchaseData
  {
    private string mIname;
    private long mExpiredAt;
    private ExpansionPurchaseParam mParam;

    public string iname => this.mIname;

    public long expired_at => this.mExpiredAt;

    public ExpansionPurchaseParam param => this.mParam;

    public void Deserialize(Json_ExpansionPurchase json)
    {
      if (json == null)
        return;
      this.mIname = json.iname;
      this.mExpiredAt = json.expired_at;
      if (MonoSingleton<GameManager>.Instance.MasterParam.ExpansionPurchaseParams == null)
        return;
      this.mParam = MonoSingleton<GameManager>.Instance.MasterParam.ExpansionPurchaseParams.Find((Predicate<ExpansionPurchaseParam>) (x => x.Iname == this.iname));
    }

    public ExpansionPurchaseParam.eExpansionType GetExpansionType()
    {
      return this.mParam != null ? this.mParam.ExpansionType : ExpansionPurchaseParam.eExpansionType.None;
    }
  }
}
