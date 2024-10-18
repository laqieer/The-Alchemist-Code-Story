// Decompiled with JetBrains decompiler
// Type: SRPG.ReqConvertPiece
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using System.Text;

namespace SRPG
{
  public class ReqConvertPiece : WebAPI
  {
    public ReqConvertPiece(string consume, string unit, int pieceNum, Network.ResponseCallback response)
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
