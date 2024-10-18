// Decompiled with JetBrains decompiler
// Type: SRPG.ReqSellConceptCard
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using System.Text;

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
