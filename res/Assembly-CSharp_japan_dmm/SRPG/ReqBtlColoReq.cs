// Decompiled with JetBrains decompiler
// Type: SRPG.ReqBtlColoReq
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System.Text;

#nullable disable
namespace SRPG
{
  public class ReqBtlColoReq : WebAPI
  {
    public ReqBtlColoReq(
      string questID,
      string fuid,
      ArenaPlayer ap,
      Network.ResponseCallback response,
      int partyIndex)
    {
      this.name = "btl/colo/req";
      StringBuilder stringBuilder = WebAPI.GetStringBuilder();
      if (partyIndex >= 0)
      {
        stringBuilder.Append("\"partyid\":");
        stringBuilder.Append(partyIndex);
        stringBuilder.Append(",");
      }
      stringBuilder.Append("\"btlparam\":{},");
      stringBuilder.Append("\"fuid\":\"");
      stringBuilder.Append(fuid);
      stringBuilder.Append("\"");
      stringBuilder.Append(",");
      stringBuilder.Append("\"opp_rank\":");
      stringBuilder.Append(ap.ArenaRank);
      this.body = WebAPI.GetRequestString(stringBuilder.ToString());
      this.callback = response;
    }
  }
}
