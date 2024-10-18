// Decompiled with JetBrains decompiler
// Type: SRPG.UnitSelectListItemData
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

#nullable disable
namespace SRPG
{
  public class UnitSelectListItemData
  {
    public string iname;
    public UnitParam param;
    public int convert_piece_num;

    public void Deserialize(Json_UnitSelectItem json)
    {
      this.iname = json.iname;
      this.convert_piece_num = json.convert_piece_num;
    }
  }
}
