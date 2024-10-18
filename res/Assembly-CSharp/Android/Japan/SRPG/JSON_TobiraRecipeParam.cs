// Decompiled with JetBrains decompiler
// Type: SRPG.JSON_TobiraRecipeParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using System;

namespace SRPG
{
  [Serializable]
  public class JSON_TobiraRecipeParam
  {
    public string recipe_iname;
    public int tobira_lv;
    public int cost;
    public int unit_piece_num;
    public int piece_elem_num;
    public int unlock_elem_num;
    public int unlock_birth_num;
    public JSON_TobiraRecipeMaterialParam[] mats;
  }
}
