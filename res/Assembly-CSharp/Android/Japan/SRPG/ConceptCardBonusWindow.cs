// Decompiled with JetBrains decompiler
// Type: SRPG.ConceptCardBonusWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

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
      if ((UnityEngine.Object) this.mAwakeBonusTemplate != (UnityEngine.Object) null && (UnityEngine.Object) this.mAwakeBonusParent != (UnityEngine.Object) null)
      {
        for (int awake_count = 1; awake_count <= param.AwakeCountCap; ++awake_count)
        {
          bool is_enable = current_awake_count >= awake_count;
          ConceptCardBonusContentAwake bonusContentAwake = UnityEngine.Object.Instantiate<ConceptCardBonusContentAwake>(this.mAwakeBonusTemplate);
          bonusContentAwake.transform.SetParent(this.mAwakeBonusParent, false);
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
        this.mAwakeBonusTemplate.gameObject.SetActive(false);
      }
      if ((UnityEngine.Object) this.mLvMaxBonusSkillTemplate != (UnityEngine.Object) null && (UnityEngine.Object) this.mLvMaxBonusParent != (UnityEngine.Object) null)
      {
        bool is_enable = is_level_max;
        ConceptCardBonusContentLvMax bonusContentLvMax = UnityEngine.Object.Instantiate<ConceptCardBonusContentLvMax>(this.mLvMaxBonusSkillTemplate);
        bonusContentLvMax.transform.SetParent(this.mLvMaxBonusParent, false);
        bonusContentLvMax.Setup(param.effects, param.lvcap, param.lvcap, param.AwakeCountCap, is_enable, ConceptCardBonusWindow.eViewType.CARD_SKILL);
        this.mLvMaxBonusSkillTemplate.gameObject.SetActive(false);
      }
      if (!((UnityEngine.Object) this.mLvMaxBonusAbilityTemplate != (UnityEngine.Object) null) || !((UnityEngine.Object) this.mLvMaxBonusParent != (UnityEngine.Object) null))
        return;
      bool is_enable1 = is_level_max;
      ConceptCardBonusContentLvMax bonusContentLvMax1 = UnityEngine.Object.Instantiate<ConceptCardBonusContentLvMax>(this.mLvMaxBonusAbilityTemplate);
      bonusContentLvMax1.transform.SetParent(this.mLvMaxBonusParent, false);
      bonusContentLvMax1.Setup(param.effects, param.lvcap, param.lvcap, param.AwakeCountCap, is_enable1, ConceptCardBonusWindow.eViewType.ABILITY);
      this.mLvMaxBonusAbilityTemplate.gameObject.SetActive(false);
    }

    public enum eViewType
    {
      CARD_SKILL,
      ABILITY,
    }
  }
}
