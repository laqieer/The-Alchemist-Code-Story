// Decompiled with JetBrains decompiler
// Type: SRPG.UnitPieceShopGroupParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System.Collections.Generic;

#nullable disable
namespace SRPG
{
  public class UnitPieceShopGroupParam
  {
    private string mIname;
    private List<UnitPieceShopGroupCost> mCosts;

    public string Iname => this.mIname;

    public List<UnitPieceShopGroupCost> Costs => this.mCosts;

    public bool Deserialize(JSON_UnitPieceShopGroupParam json)
    {
      if (json == null)
        return false;
      this.mIname = json.iname;
      this.mCosts = new List<UnitPieceShopGroupCost>();
      if (json.costs != null)
      {
        for (int index = 0; index < json.costs.Length; ++index)
        {
          if (json.costs[index] != null)
          {
            UnitPieceShopGroupCost pieceShopGroupCost = new UnitPieceShopGroupCost();
            if (pieceShopGroupCost.Deserialize(json.costs[index]))
              this.mCosts.Add(pieceShopGroupCost);
          }
        }
      }
      return true;
    }

    public static void Deserialize(
      ref List<UnitPieceShopGroupParam> param,
      JSON_UnitPieceShopGroupParam[] json)
    {
      if (json == null)
        return;
      param = new List<UnitPieceShopGroupParam>(json.Length);
      for (int index = 0; index < json.Length; ++index)
      {
        if (json[index] != null)
        {
          UnitPieceShopGroupParam pieceShopGroupParam = new UnitPieceShopGroupParam();
          if (pieceShopGroupParam.Deserialize(json[index]))
            param.Add(pieceShopGroupParam);
        }
      }
    }
  }
}
