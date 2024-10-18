// Decompiled with JetBrains decompiler
// Type: SRPG.QuestCampaignInspSkill
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

namespace SRPG
{
  public class QuestCampaignInspSkill
  {
    private string mIname;
    private string mInspirationSkillIname;
    private int mRate;

    public string Iname
    {
      get
      {
        return this.mIname;
      }
    }

    public string InspirationSkillIname
    {
      get
      {
        return this.mInspirationSkillIname;
      }
    }

    public int Rate
    {
      get
      {
        return this.mRate;
      }
    }

    public bool Deserialize(JSON_QuestCampaignInspSkill json)
    {
      this.mIname = json.children_iname;
      this.mInspirationSkillIname = json.insp;
      this.mRate = json.rate;
      return true;
    }
  }
}
