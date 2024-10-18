// Decompiled with JetBrains decompiler
// Type: SRPG.UnitLearnAbilityWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System.Collections.Generic;
using UnityEngine;

#nullable disable
namespace SRPG
{
  public class UnitLearnAbilityWindow : MonoBehaviour, IFlowInterface
  {
    public List<AbilityData> AbilityList;
    public Transform LearnAbilityParent;
    public GameObject LearnAbilityTemplate;
    public GameObject LearnAbilitySkillTemplate;
    public GameObject[] LearningSkills;

    public void Activated(int pinID) => this.Refresh();

    private void Awake()
    {
      if (Object.op_Inequality((Object) this.LearnAbilityTemplate, (Object) null))
        this.LearnAbilityTemplate.SetActive(false);
      if (!Object.op_Inequality((Object) this.LearnAbilitySkillTemplate, (Object) null))
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
          GameObject gameObject = ability.LearningSkills == null || ability.LearningSkills.Length == 1 ? Object.Instantiate<GameObject>(this.LearnAbilityTemplate) : Object.Instantiate<GameObject>(this.LearnAbilitySkillTemplate);
          DataSource.Bind<AbilityData>(gameObject, ability);
          UnitLearnAbilityElement component = gameObject.GetComponent<UnitLearnAbilityElement>();
          if (Object.op_Inequality((Object) component, (Object) null))
            component.Refresh();
          gameObject.transform.SetParent(this.LearnAbilityParent, false);
          gameObject.SetActive(true);
        }
      }
      ((Behaviour) this).enabled = true;
    }
  }
}
