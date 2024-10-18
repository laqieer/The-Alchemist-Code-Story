// Decompiled with JetBrains decompiler
// Type: SRPG.ReqMixConceptCardAwakeItemData
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System.Collections.Generic;
using System.Text;

#nullable disable
namespace SRPG
{
  public class ReqMixConceptCardAwakeItemData : WebAPI
  {
    public ReqMixConceptCardAwakeItemData(
      long base_id,
      Dictionary<string, int> materials,
      Network.ResponseCallback response,
      string trophyProgs = null,
      string bingoProgs = null)
    {
      this.name = "unit/concept/plus";
      StringBuilder stringBuilder = WebAPI.GetStringBuilder();
      stringBuilder.Append("\"base_id\":");
      stringBuilder.Append(base_id);
      stringBuilder.Append(",");
      stringBuilder.Append("\"mats\":[");
      int num = 0;
      foreach (KeyValuePair<string, int> material in materials)
      {
        stringBuilder.Append("{");
        stringBuilder.Append("\"iname\":");
        stringBuilder.Append("\"");
        stringBuilder.Append(material.Key);
        stringBuilder.Append("\"");
        stringBuilder.Append(",");
        stringBuilder.Append("\"num\":");
        stringBuilder.Append(material.Value);
        if (num < materials.Count - 1)
          stringBuilder.Append("},");
        else
          stringBuilder.Append("}");
        ++num;
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
