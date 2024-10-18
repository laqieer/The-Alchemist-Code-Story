// Decompiled with JetBrains decompiler
// Type: SRPG.UnitOverWriteData
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

#nullable disable
namespace SRPG
{
  public class UnitOverWriteData
  {
    private long mUniqueID;
    private long mJobUniqueID;
    private long[] mAbilities;
    private long[] mArtifacts;
    private ConceptCardData[] mConceptCards;
    private bool mLeaderSkillIsConceptCard;

    public long UniqueID => this.mUniqueID;

    public long JobUniqueID => this.mJobUniqueID;

    public long[] Abilities => this.mAbilities;

    public long[] Artifacts => this.mArtifacts;

    public ConceptCardData[] ConceptCards => this.mConceptCards;

    public bool LeaderSkillIsConceptCard => this.mLeaderSkillIsConceptCard;

    public void Deserialize(JSON_UnitOverWriteData json)
    {
      if (json == null)
        return;
      this.mUniqueID = json.unit_iid;
      this.mJobUniqueID = json.job_iid;
      this.mAbilities = json.abils;
      this.mArtifacts = json.artifacts;
      this.mLeaderSkillIsConceptCard = false;
      if (json.concept_cards == null)
        return;
      this.mConceptCards = new ConceptCardData[json.concept_cards.Length];
      for (int index = 0; index < this.mConceptCards.Length; ++index)
      {
        if (!json.concept_cards[index].IsEmptyDummyData)
        {
          this.mLeaderSkillIsConceptCard |= json.concept_cards[index].leaderskill != 0;
          this.mConceptCards[index] = new ConceptCardData();
          this.mConceptCards[index].Deserialize(json.concept_cards[index]);
        }
      }
    }

    public bool IsEquipConceptCard(long iid)
    {
      if (this.mConceptCards == null || iid == 0L)
        return false;
      for (int index = 0; index < this.mConceptCards.Length; ++index)
      {
        if (this.mConceptCards[index] != null && (long) this.mConceptCards[index].UniqueID == iid)
          return true;
      }
      return false;
    }
  }
}
