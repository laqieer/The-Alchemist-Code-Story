// Decompiled with JetBrains decompiler
// Type: SRPG.QuestCampaignTrust
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

namespace SRPG
{
  public class QuestCampaignTrust
  {
    public string iname;
    public string concept_card;
    public int card_trust_lottery_rate;
    public int card_trust_qe_bonus;

    public bool Deserialize(JSON_QuestCampaignTrust json)
    {
      this.iname = json.children_iname;
      this.concept_card = json.concept_card;
      this.card_trust_lottery_rate = json.card_trust_lottery_rate;
      this.card_trust_qe_bonus = json.card_trust_qe_bonus;
      return true;
    }
  }
}
