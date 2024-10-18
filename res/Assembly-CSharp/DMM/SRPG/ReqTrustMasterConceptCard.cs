// Decompiled with JetBrains decompiler
// Type: SRPG.ReqTrustMasterConceptCard
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System.Text;

#nullable disable
namespace SRPG
{
  public class ReqTrustMasterConceptCard : WebAPI
  {
    public ReqTrustMasterConceptCard(
      long card_iid,
      bool is_favorite,
      Network.ResponseCallback response)
    {
      this.name = "unit/concept/trust/reward";
      StringBuilder stringBuilder = WebAPI.GetStringBuilder();
      stringBuilder.Append("\"concept_iid\":");
      stringBuilder.Append(card_iid);
      this.body = WebAPI.GetRequestString(stringBuilder.ToString());
      this.callback = response;
    }
  }
}
