// Decompiled with JetBrains decompiler
// Type: SRPG.ExpansionPurchaseQuestParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System.Collections.Generic;

#nullable disable
namespace SRPG
{
  public class ExpansionPurchaseQuestParam
  {
    private string mExpansionName;
    private string mName;

    public string ExpansionName => this.mExpansionName;

    public string Name => this.mName;

    public bool Deserialize(JSON_ExpansionPurchaseQuestParam json)
    {
      this.mExpansionName = json.expansion_name;
      this.mName = json.name;
      return true;
    }

    public static void Deserialize(
      ref Dictionary<string, List<string>> ref_dict,
      JSON_ExpansionPurchaseQuestParam[] json)
    {
      if (ref_dict == null)
        ref_dict = new Dictionary<string, List<string>>();
      ref_dict.Clear();
      if (json == null)
        return;
      foreach (JSON_ExpansionPurchaseQuestParam json1 in json)
      {
        ExpansionPurchaseQuestParam purchaseQuestParam = new ExpansionPurchaseQuestParam();
        purchaseQuestParam.Deserialize(json1);
        if (ref_dict.ContainsKey(purchaseQuestParam.ExpansionName))
        {
          ref_dict[purchaseQuestParam.ExpansionName].Add(purchaseQuestParam.Name);
        }
        else
        {
          ref_dict.Add(purchaseQuestParam.ExpansionName, new List<string>());
          ref_dict[purchaseQuestParam.ExpansionName].Add(purchaseQuestParam.Name);
        }
      }
    }
  }
}
