// Decompiled with JetBrains decompiler
// Type: SRPG.ReqBtlOrdealReq
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System.Collections.Generic;
using System.Text;

#nullable disable
namespace SRPG
{
  public class ReqBtlOrdealReq : WebAPI
  {
    public ReqBtlOrdealReq(
      string iname,
      List<SupportData> supports,
      Network.ResponseCallback response)
    {
      StringBuilder stringBuilder = WebAPI.GetStringBuilder();
      this.name = "btl/ordeal/req";
      stringBuilder.Append("\"iname\":\"");
      stringBuilder.Append(iname);
      stringBuilder.Append("\",");
      stringBuilder.Append("\"req_at\":");
      stringBuilder.Append(Network.GetServerTime());
      stringBuilder.Append(",");
      stringBuilder.Append("\"btlparam\":{\"helps\":[");
      for (int index = 0; index < supports.Count; ++index)
      {
        if (supports != null)
        {
          if (index > 0)
            stringBuilder.Append(",");
          SupportData support = supports[index];
          if (support == null)
          {
            stringBuilder.Append("{}");
          }
          else
          {
            stringBuilder.Append("{");
            stringBuilder.Append("\"fuid\":");
            stringBuilder.Append("\"" + support.FUID + "\"");
            stringBuilder.Append(",\"elem\":" + (object) (int) support.Unit.SupportElement);
            stringBuilder.Append(",\"iname\":\"" + support.Unit.UnitID + "\"");
            stringBuilder.Append("}");
          }
        }
      }
      stringBuilder.Append("]");
      stringBuilder.Append("}");
      this.body = WebAPI.GetRequestString(stringBuilder.ToString());
      this.callback = response;
    }
  }
}
