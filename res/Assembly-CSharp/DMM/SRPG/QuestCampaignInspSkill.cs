// Decompiled with JetBrains decompiler
// Type: SRPG.QuestCampaignInspSkill
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

#nullable disable
namespace SRPG
{
  public class QuestCampaignInspSkill
  {
    private string mIname;
    private string mInspirationSkillIname;
    private int mRate;

    public string Iname => this.mIname;

    public string InspirationSkillIname => this.mInspirationSkillIname;

    public int Rate => this.mRate;

    public bool Deserialize(JSON_QuestCampaignInspSkill json)
    {
      this.mIname = json.children_iname;
      this.mInspirationSkillIname = json.insp;
      this.mRate = json.rate;
      return true;
    }
  }
}
