// Decompiled with JetBrains decompiler
// Type: SRPG.ReqMixConceptCardMaterialData
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using System.Text;

namespace SRPG
{
  public class ReqMixConceptCardMaterialData : WebAPI
  {
    public ReqMixConceptCardMaterialData(long base_id, List<SelecteConceptCardMaterial> materials, Network.ResponseCallback response, string trophyProgs = null, string bingoProgs = null)
    {
      this.name = "unit/concept/mixall";
      StringBuilder stringBuilder = WebAPI.GetStringBuilder();
      stringBuilder.Append("\"base_id\":");
      stringBuilder.Append(base_id);
      stringBuilder.Append(",");
      stringBuilder.Append("\"use_item\":[");
      for (int index = 0; index < materials.Count; ++index)
      {
        stringBuilder.Append("{");
        stringBuilder.Append("\"unique_id\":");
        stringBuilder.Append((long) materials[index].mUniqueID);
        stringBuilder.Append(",");
        stringBuilder.Append("\"use_num\":");
        stringBuilder.Append(materials[index].mSelectNum);
        if (index < materials.Count - 1)
          stringBuilder.Append("},");
        else
          stringBuilder.Append("}");
      }
      stringBuilder.Append("]");
      if (!string.IsNullOrEmpty(trophyProgs))
      {
        stringBuilder.Append(",");
        stringBuilder.Append(trophyProgs);
      }
      if (!string.IsNullOrEmpty(bingoProgs))
      {
        stringBuilder.Append(",");
        stringBuilder.Append(bingoProgs);
      }
      this.body = WebAPI.GetRequestString(stringBuilder.ToString());
      this.callback = response;
    }
  }
}
