// Decompiled with JetBrains decompiler
// Type: SRPG.ReqSellConceptCard
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System.Text;

#nullable disable
namespace SRPG
{
  public class ReqSellConceptCard : WebAPI
  {
    public ReqSellConceptCard(long[] sell_ids, Network.ResponseCallback response)
    {
      this.name = "unit/concept/sell";
      StringBuilder stringBuilder = WebAPI.GetStringBuilder();
      stringBuilder.Append("\"sell_ids\":[");
      for (int index = 0; index < sell_ids.Length; ++index)
      {
        stringBuilder.Append(sell_ids[index]);
        if (index != sell_ids.Length - 1)
          stringBuilder.Append(",");
      }
      stringBuilder.Append("]");
      this.body = WebAPI.GetRequestString(stringBuilder.ToString());
      this.callback = response;
    }
  }
}
