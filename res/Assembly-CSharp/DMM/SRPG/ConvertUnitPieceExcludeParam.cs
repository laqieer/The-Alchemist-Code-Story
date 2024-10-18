// Decompiled with JetBrains decompiler
// Type: SRPG.ConvertUnitPieceExcludeParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

#nullable disable
namespace SRPG
{
  public class ConvertUnitPieceExcludeParam
  {
    public int id;
    public string unit_piece_iname;

    public void Deserialize(JSON_ConvertUnitPieceExcludeParam json)
    {
      if (json == null)
        return;
      this.id = json.id;
      this.unit_piece_iname = json.unit_piece_iname;
    }
  }
}
