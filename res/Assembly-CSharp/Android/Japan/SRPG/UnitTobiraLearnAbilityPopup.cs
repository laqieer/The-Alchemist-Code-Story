// Decompiled with JetBrains decompiler
// Type: SRPG.UnitTobiraLearnAbilityPopup
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  public class UnitTobiraLearnAbilityPopup : MonoBehaviour
  {
    [SerializeField]
    private Text mTitleText;
    [SerializeField]
    private Text mNameText;
    [SerializeField]
    private Text mDescText;

    public void Setup(UnitData unit, AbilityParam new_ability, AbilityParam old_ability)
    {
      if (unit == null || new_ability == null)
        return;
      this.mTitleText.text = old_ability == null ? LocalizedText.Get("sys.TOBIRA_LEARN_NEW_ABILITY_TEXT") : LocalizedText.Get("sys.TOBIRA_LEARN_OVERRIDE_NEW_ABILITY_TEXT");
      this.mNameText.text = new_ability.name;
      this.mDescText.text = new_ability.expr;
      DataSource.Bind<UnitData>(this.gameObject, unit, false);
      GameParameter.UpdateAll(this.gameObject);
    }

    public void Setup(UnitData unit, SkillParam skill)
    {
      if (unit == null || skill == null)
        return;
      this.mTitleText.text = LocalizedText.Get("sys.TOBIRA_LEARN_NEW_LEADER_SKILL_TEXT");
      this.mNameText.text = skill.name;
      this.mDescText.text = skill.expr;
      DataSource.Bind<UnitData>(this.gameObject, unit, false);
      GameParameter.UpdateAll(this.gameObject);
    }
  }
}
