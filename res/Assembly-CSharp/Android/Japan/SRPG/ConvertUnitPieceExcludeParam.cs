// Decompiled with JetBrains decompiler
// Type: SRPG.ConvertUnitPieceExcludeParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

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
