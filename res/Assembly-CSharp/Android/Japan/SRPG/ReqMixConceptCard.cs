// Decompiled with JetBrains decompiler
// Type: SRPG.ReqMixConceptCard
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using System.Text;

namespace SRPG
{
  public class ReqMixConceptCard : WebAPI
  {
    public ReqMixConceptCard(long base_id, long[] mix_ids, Network.ResponseCallback response, string trophyProgs = null, string bingoProgs = null)
    {
      this.name = "unit/concept/mix";
      StringBuilder stringBuilder = WebAPI.GetStringBuilder();
      stringBuilder.Append("\"base_id\":");
      stringBuilder.Append(base_id);
      stringBuilder.Append(",");
      stringBuilder.Append("\"mix_ids\":[");
      for (int index = 0; index < mix_ids.Length; ++index)
      {
        stringBuilder.Append(mix_ids[index]);
        if (index != mix_ids.Length - 1)
          stringBuilder.Append(",");
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
