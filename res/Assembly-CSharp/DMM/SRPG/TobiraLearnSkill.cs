// Decompiled with JetBrains decompiler
// Type: SRPG.TobiraLearnSkill
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  public class TobiraLearnSkill : MonoBehaviour
  {
    [SerializeField]
    private Text m_LearnSkillName;
    [SerializeField]
    private Text m_LearnSkillEffect;

    public void Setup(AbilityData newAbility)
    {
      this.m_LearnSkillName.text = "アビリティ：" + newAbility.AbilityName;
      this.m_LearnSkillEffect.text = newAbility.Param.expr;
    }

    public void Setup(SkillData skill)
    {
      this.m_LearnSkillName.text = "リーダースキル：" + skill.Name;
      this.m_LearnSkillEffect.text = skill.SkillParam.expr;
    }
  }
}
