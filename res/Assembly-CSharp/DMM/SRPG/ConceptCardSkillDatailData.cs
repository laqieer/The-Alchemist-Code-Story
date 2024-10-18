// Decompiled with JetBrains decompiler
// Type: SRPG.ConceptCardSkillDatailData
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

#nullable disable
namespace SRPG
{
  public class ConceptCardSkillDatailData
  {
    public ConceptCardEquipEffect effect;
    public SkillData skill_data;
    public LearningSkill learning_skill;
    public ConceptCardDetailAbility.ShowType type;

    public ConceptCardSkillDatailData(
      ConceptCardEquipEffect _effect,
      SkillData _data,
      ConceptCardDetailAbility.ShowType _type,
      LearningSkill _learning_skill = null)
    {
      this.skill_data = _data;
      this.effect = _effect;
      this.type = _type;
      this.learning_skill = _learning_skill;
    }
  }
}
