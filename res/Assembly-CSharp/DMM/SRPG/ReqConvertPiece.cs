// Decompiled with JetBrains decompiler
// Type: SRPG.ReqConvertPiece
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System.Text;

#nullable disable
namespace SRPG
{
  public class ReqConvertPiece : WebAPI
  {
    public ReqConvertPiece(
      string consume,
      string unit,
      int pieceNum,
      Network.ResponseCallback response)
    {
      this.name = "shop/piece/convert";
      StringBuilder stringBuilder = WebAPI.GetStringBuilder();
      stringBuilder.Append("\"convert_piece\":{");
      stringBuilder.Append("\"consume_piece_iname\":\"");
      stringBuilder.Append(consume);
      stringBuilder.Append("\",");
      stringBuilder.Append("\"convert_piece_iname\":\"");
      stringBuilder.Append(unit);
      stringBuilder.Append("\",");
      stringBuilder.Append("\"convert_piece_num\":");
      stringBuilder.Append(pieceNum);
      stringBuilder.Append("}");
      this.body = WebAPI.GetRequestString(stringBuilder.ToString());
      this.callback = response;
    }
  }
}
