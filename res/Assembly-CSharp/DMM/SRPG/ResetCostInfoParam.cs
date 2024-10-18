// Decompiled with JetBrains decompiler
// Type: SRPG.ResetCostInfoParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System.Collections.Generic;
using UnityEngine;

#nullable disable
namespace SRPG
{
  public class ResetCostInfoParam
  {
    private eResetCostType type;
    private string item;
    private List<int> num = new List<int>();

    public eResetCostType Type => this.type;

    public string Item => this.item;

    public List<int> Num => this.num;

    public void Deserialize(JSON_ResetCostInfoParam json)
    {
      if (json == null)
        return;
      this.type = (eResetCostType) json.type;
      this.item = json.item_iname;
      this.num.Clear();
      if (json.costs == null)
        return;
      for (int index = 0; index < json.costs.Length; ++index)
        this.num.Add(json.costs[index]);
    }

    public int GetNeedNum(int current_reset_count)
    {
      if (this.num.Count <= 0)
      {
        DebugUtility.LogError("MasterParam.ResetCostの消費数データの入力が不十分な可能性があります");
        return 0;
      }
      int num = this.num.Count - 1;
      return this.num[Mathf.Min(current_reset_count, num)];
    }
  }
}
