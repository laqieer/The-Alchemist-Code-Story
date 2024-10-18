// Decompiled with JetBrains decompiler
// Type: SRPG.ReqSetConceptCard
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

#nullable disable
namespace SRPG
{
  public class ReqSetConceptCard : WebAPI
  {
    private ReqSetConceptCard(long[] card_iids, long unit_iid, Network.ResponseCallback response)
    {
      this.name = "unit/concept/set";
      string[] array = ((IEnumerable<long>) card_iids).Select<long, string>((Func<long, string>) (iid => iid.ToString())).ToArray<string>();
      StringBuilder stringBuilder = WebAPI.GetStringBuilder();
      stringBuilder.Append("\"concept_iids\":[");
      stringBuilder.Append(string.Join(",", array));
      stringBuilder.Append("],");
      stringBuilder.Append("\"unit_iid\":");
      stringBuilder.Append(unit_iid);
      this.body = WebAPI.GetRequestString(stringBuilder.ToString());
      this.callback = response;
    }

    public static ReqSetConceptCard CreateSet(
      long[] card_iids,
      long unit_iid,
      Network.ResponseCallback response)
    {
      return new ReqSetConceptCard(card_iids, unit_iid, response);
    }
  }
}
