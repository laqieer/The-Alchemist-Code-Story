// Decompiled with JetBrains decompiler
// Type: SRPG.ReqGuildFacilityEnhance
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System.Text;

#nullable disable
namespace SRPG
{
  public class ReqGuildFacilityEnhance : WebAPI
  {
    public ReqGuildFacilityEnhance(
      string facility_iname,
      EnhanceMaterial[] materials,
      long gold,
      Network.ResponseCallback response)
    {
      this.name = "guild/facility/invest";
      StringBuilder stringBuilder = WebAPI.GetStringBuilder();
      stringBuilder.Append("\"facility_iname\":\"");
      stringBuilder.Append(facility_iname);
      stringBuilder.Append("\"");
      stringBuilder.Append(",");
      stringBuilder.Append("\"mats\":[");
      for (int index = 0; index < materials.Length; ++index)
      {
        stringBuilder.Append("{");
        stringBuilder.Append("\"iname\":\"");
        stringBuilder.Append(materials[index].item.Param.iname);
        stringBuilder.Append("\"");
        stringBuilder.Append(",");
        stringBuilder.Append("\"num\":");
        stringBuilder.Append(materials[index].num);
        stringBuilder.Append("}");
        if (index < materials.Length - 1)
          stringBuilder.Append(",");
      }
      stringBuilder.Append("]");
      stringBuilder.Append(",");
      stringBuilder.Append("\"gold\":" + (object) gold);
      this.body = WebAPI.GetRequestString(stringBuilder.ToString());
      this.callback = response;
    }
  }
}
