// Decompiled with JetBrains decompiler
// Type: SRPG.ReqGuildFacilityEnhance
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using System.Text;

namespace SRPG
{
  public class ReqGuildFacilityEnhance : WebAPI
  {
    public ReqGuildFacilityEnhance(string facility_iname, EnhanceMaterial[] materials, Network.ResponseCallback response)
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
      this.body = WebAPI.GetRequestString(stringBuilder.ToString());
      this.callback = response;
    }
  }
}
