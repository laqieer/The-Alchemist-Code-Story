// Decompiled with JetBrains decompiler
// Type: SRPG.ReqMixConceptCardMaterialData
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System.Collections.Generic;
using System.Text;

#nullable disable
namespace SRPG
{
  public class ReqMixConceptCardMaterialData : WebAPI
  {
    public ReqMixConceptCardMaterialData(
      long base_id,
      List<SelecteConceptCardMaterial> materials,
      Network.ResponseCallback response,
      string trophyProgs = null,
      string bingoProgs = null)
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
