// Decompiled with JetBrains decompiler
// Type: SRPG.ConceptCardBonusWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System.Collections.Generic;
using UnityEngine;

#nullable disable
namespace SRPG
{
  public class ConceptCardBonusWindow : MonoBehaviour
  {
    [SerializeField]
    private Transform mAwakeBonusParent;
    [SerializeField]
    private Transform mLvMaxBonusParent;
    [SerializeField]
    private ConceptCardBonusContentAwake mAwakeBonusTemplate;
    [SerializeField]
    private ConceptCardBonusContentLvMax mLvMaxBonusSkillTemplate;
    [SerializeField]
    private ConceptCardBonusContentLvMax mLvMaxBonusAbilityTemplate;

    public void Initailize(ConceptCardParam param, int current_awake_count, bool is_level_max)
    {
      List<ConceptCardBonusContentAwake> bonusContentAwakeList = new List<ConceptCardBonusContentAwake>();
      if (Object.op_Inequality((Object) this.mAwakeBonusTemplate, (Object) null) && Object.op_Inequality((Object) this.mAwakeBonusParent, (Object) null))
      {
        for (int awake_count = 1; awake_count <= param.AwakeCountCap; ++awake_count)
        {
          bool is_enable = current_awake_count >= awake_count;
          ConceptCardBonusContentAwake bonusContentAwake = Object.Instantiate<ConceptCardBonusContentAwake>(this.mAwakeBonusTemplate);
          ((Component) bonusContentAwake).transform.SetParent(this.mAwakeBonusParent, false);
          bonusContentAwake.Setup(param.effects, awake_count, param.AwakeCountCap, is_enable);
          bonusContentAwakeList.Add(bonusContentAwake);
        }
        for (int index1 = 0; index1 < bonusContentAwakeList.Count; ++index1)
        {
          int index2 = index1 + 1;
          bool is_enable = false;
          bool is_active = true;
          if (bonusContentAwakeList.Count > index2)
            is_enable = bonusContentAwakeList[index2].IsEnable;
          else
            is_active = false;
          bonusContentAwakeList[index1].SetProgressLineImage(is_enable, is_active);
        }
        ((Component) this.mAwakeBonusTemplate).gameObject.SetActive(false);
      }
      if (Object.op_Inequality((Object) this.mLvMaxBonusSkillTemplate, (Object) null) && Object.op_Inequality((Object) this.mLvMaxBonusParent, (Object) null))
      {
        bool is_enable = is_level_max;
        ConceptCardBonusContentLvMax bonusContentLvMax = Object.Instantiate<ConceptCardBonusContentLvMax>(this.mLvMaxBonusSkillTemplate);
        ((Component) bonusContentLvMax).transform.SetParent(this.mLvMaxBonusParent, false);
        bonusContentLvMax.Setup(param.effects, param.lvcap, param.lvcap, param.AwakeCountCap, is_enable, ConceptCardBonusWindow.eViewType.CARD_SKILL);
        ((Component) this.mLvMaxBonusSkillTemplate).gameObject.SetActive(false);
      }
      if (!Object.op_Inequality((Object) this.mLvMaxBonusAbilityTemplate, (Object) null) || !Object.op_Inequality((Object) this.mLvMaxBonusParent, (Object) null))
        return;
      bool is_enable1 = is_level_max;
      ConceptCardBonusContentLvMax bonusContentLvMax1 = Object.Instantiate<ConceptCardBonusContentLvMax>(this.mLvMaxBonusAbilityTemplate);
      ((Component) bonusContentLvMax1).transform.SetParent(this.mLvMaxBonusParent, false);
      bonusContentLvMax1.Setup(param.effects, param.lvcap, param.lvcap, param.AwakeCountCap, is_enable1, ConceptCardBonusWindow.eViewType.ABILITY);
      ((Component) this.mLvMaxBonusAbilityTemplate).gameObject.SetActive(false);
    }

    public enum eViewType
    {
      CARD_SKILL,
      ABILITY,
    }
  }
}
