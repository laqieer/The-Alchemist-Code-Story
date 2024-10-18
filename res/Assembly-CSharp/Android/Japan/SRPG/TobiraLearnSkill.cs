// Decompiled with JetBrains decompiler
// Type: SRPG.TobiraLearnSkill
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.UI;

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
