﻿// Decompiled with JetBrains decompiler
// Type: SRPG.UnitLearnAbilityWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

namespace SRPG
{
  public class UnitLearnAbilityWindow : MonoBehaviour, IFlowInterface
  {
    public List<AbilityData> AbilityList;
    public Transform LearnAbilityParent;
    public GameObject LearnAbilityTemplate;
    public GameObject LearnAbilitySkillTemplate;
    public GameObject[] LearningSkills;

    public void Activated(int pinID)
    {
      this.Refresh();
    }

    private void Awake()
    {
      if ((UnityEngine.Object) this.LearnAbilityTemplate != (UnityEngine.Object) null)
        this.LearnAbilityTemplate.SetActive(false);
      if (!((UnityEngine.Object) this.LearnAbilitySkillTemplate != (UnityEngine.Object) null))
        return;
      this.LearnAbilitySkillTemplate.SetActive(false);
    }

    private void Start()
    {
      if (this.AbilityList == null)
      {
        this.AbilityList = GlobalVars.LearningAbilities;
        GlobalVars.LearningAbilities = (List<AbilityData>) null;
      }
      this.Refresh();
    }

    private void Refresh()
    {
      if (this.AbilityList != null)
      {
        for (int index = 0; index < this.AbilityList.Count; ++index)
        {
          AbilityData ability = this.AbilityList[index];
          GameObject gameObject = ability.LearningSkills == null || ability.LearningSkills.Length == 1 ? UnityEngine.Object.Instantiate<GameObject>(this.LearnAbilityTemplate) : UnityEngine.Object.Instantiate<GameObject>(this.LearnAbilitySkillTemplate);
          DataSource.Bind<AbilityData>(gameObject, ability, false);
          UnitLearnAbilityElement component = gameObject.GetComponent<UnitLearnAbilityElement>();
          if ((UnityEngine.Object) component != (UnityEngine.Object) null)
            component.Refresh();
          gameObject.transform.SetParent(this.LearnAbilityParent, false);
          gameObject.SetActive(true);
        }
      }
      this.enabled = true;
    }
  }
}
