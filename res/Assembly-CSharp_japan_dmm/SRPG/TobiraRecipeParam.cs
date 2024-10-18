// Decompiled with JetBrains decompiler
// Type: SRPG.TobiraRecipeParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System.Collections.Generic;

#nullable disable
namespace SRPG
{
  public class TobiraRecipeParam
  {
    private string mRecipeIname;
    private int mLevel;
    private int mCost;
    private int mUnitPieceNum;
    private int mElementNum;
    private int mUnlockElementNum;
    private int mUnlockBirthNum;
    private List<TobiraRecipeMaterialParam> mMaterials = new List<TobiraRecipeMaterialParam>();

    public string RecipeIname => this.mRecipeIname;

    public int Level => this.mLevel;

    public int Cost => this.mCost;

    public int UnitPieceNum => this.mUnitPieceNum;

    public int ElementNum => this.mElementNum;

    public int UnlockElementNum => this.mUnlockElementNum;

    public int UnlockBirthNum => this.mUnlockBirthNum;

    public TobiraRecipeMaterialParam[] Materials => this.mMaterials.ToArray();

    public void Deserialize(JSON_TobiraRecipeParam json)
    {
      if (json == null)
        return;
      this.mRecipeIname = json.recipe_iname;
      this.mLevel = json.tobira_lv;
      this.mCost = json.cost;
      this.mUnitPieceNum = json.unit_piece_num;
      this.mElementNum = json.piece_elem_num;
      this.mUnlockElementNum = json.unlock_elem_num;
      this.mUnlockBirthNum = json.unlock_birth_num;
      this.mMaterials.Clear();
      if (json.mats == null)
        return;
      for (int index = 0; index < json.mats.Length; ++index)
      {
        TobiraRecipeMaterialParam recipeMaterialParam = new TobiraRecipeMaterialParam();
        recipeMaterialParam.Deserialize(json.mats[index]);
        this.mMaterials.Add(recipeMaterialParam);
      }
    }
  }
}
