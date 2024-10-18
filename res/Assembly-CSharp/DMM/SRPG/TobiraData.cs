// Decompiled with JetBrains decompiler
// Type: SRPG.TobiraData
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using MessagePack;
using System.Text;

#nullable disable
namespace SRPG
{
  [MessagePackObject(true)]
  public class TobiraData
  {
    private int mLv;
    private SkillData mParameterBuffSkill;
    private TobiraParam mTobiraParam;
    private string mLearnedLeaderSkillIname;

    public int Lv
    {
      get => this.mLv;
      set => this.mLv = value;
    }

    public int ViewLv => this.mLv - 1;

    public SkillData ParameterBuffSkill => this.mParameterBuffSkill;

    public string LearnedLeaderSkillIname => this.mLearnedLeaderSkillIname;

    public bool IsUnlocked => this.mLv > 0;

    public TobiraParam Param => this.mTobiraParam;

    public bool IsLearnedLeaderSkill => !string.IsNullOrEmpty(this.mLearnedLeaderSkillIname);

    public bool IsMaxLv
    {
      get => this.Lv >= (int) MonoSingleton<GameManager>.Instance.MasterParam.FixParam.TobiraLvCap;
    }

    public bool Setup(string unit_iname, TobiraParam.Category category, int lv)
    {
      this.mLv = lv;
      this.mTobiraParam = MonoSingleton<GameManager>.Instance.MasterParam.GetTobiraParam(unit_iname, category);
      if (this.mTobiraParam == null)
        return false;
      this.mParameterBuffSkill = TobiraUtility.CreateParameterBuffSkill(this.mTobiraParam, this.mLv);
      if (this.mTobiraParam.HasLeaerSkill && lv >= this.mTobiraParam.OverwriteLeaderSkillLevel)
        this.mLearnedLeaderSkillIname = this.mTobiraParam.OverwriteLeaderSkillIname;
      return this.mParameterBuffSkill != null;
    }

    public Json_Tobira ToJson()
    {
      return new Json_Tobira()
      {
        category = (int) this.Param.TobiraCategory,
        lv = this.Lv
      };
    }

    public string ToJsonString()
    {
      StringBuilder stringBuilder = new StringBuilder(512);
      stringBuilder.Append("{\"lv\":");
      stringBuilder.Append(this.Lv);
      stringBuilder.Append(",");
      stringBuilder.Append("\"category\":");
      stringBuilder.Append((int) this.Param.TobiraCategory);
      stringBuilder.Append("}");
      return stringBuilder.ToString();
    }
  }
}
