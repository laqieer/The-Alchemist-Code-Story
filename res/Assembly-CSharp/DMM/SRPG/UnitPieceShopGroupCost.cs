// Decompiled with JetBrains decompiler
// Type: SRPG.UnitPieceShopGroupCost
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

#nullable disable
namespace SRPG
{
  public class UnitPieceShopGroupCost
  {
    private int mCost;
    private int mNum;

    public int Cost => this.mCost;

    public int Num => this.mNum;

    public bool Deserialize(JSON_UnitPieceShopGroupCost json)
    {
      if (json == null)
        return false;
      this.mCost = json.cost;
      this.mNum = json.num;
      return true;
    }
  }
}
