// Decompiled with JetBrains decompiler
// Type: SRPG.ReqGetConceptCard
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System.Text;

#nullable disable
namespace SRPG
{
  public class ReqGetConceptCard : WebAPI
  {
    public ReqGetConceptCard(long last_card_iid, Network.ResponseCallback response)
    {
      this.name = "unit/concept";
      StringBuilder stringBuilder = WebAPI.GetStringBuilder();
      stringBuilder.Append("\"last_iid\":");
      stringBuilder.Append(last_card_iid);
      this.body = WebAPI.GetRequestString(stringBuilder.ToString());
      this.callback = response;
    }
  }
}
