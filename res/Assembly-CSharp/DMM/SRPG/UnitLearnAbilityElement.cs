// Decompiled with JetBrains decompiler
// Type: SRPG.UnitLearnAbilityElement
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
namespace SRPG
{
  public class UnitLearnAbilityElement : MonoBehaviour, IFlowInterface
  {
    public Transform SkillParent;
    public GameObject SkillTemplate;
    private List<GameObject> mSkills;

    public void Start()
    {
      if (!Object.op_Inequality((Object) this.SkillTemplate, (Object) null))
        return;
      this.SkillTemplate.SetActive(false);
    }

    public void Activated(int pinID)
    {
    }

    public void Refresh()
    {
      AbilityData dataOfClass = DataSource.FindDataOfClass<AbilityData>(((Component) this).gameObject, (AbilityData) null);
      if (dataOfClass != null)
      {
        this.mSkills = new List<GameObject>(dataOfClass.LearningSkills.Length);
        for (int index = 0; index < dataOfClass.LearningSkills.Length; ++index)
        {
          if (dataOfClass.LearningSkills[index] != null && dataOfClass.Rank >= dataOfClass.LearningSkills[index].locklv)
          {
            GameObject gameObject = Object.Instantiate<GameObject>(this.SkillTemplate);
            SkillParam skillParam = MonoSingleton<GameManager>.Instance.GetSkillParam(dataOfClass.LearningSkills[index].iname);
            DataSource.Bind<SkillParam>(gameObject, skillParam);
            gameObject.transform.SetParent(this.SkillParent, false);
            gameObject.SetActive(true);
            this.mSkills.Add(gameObject);
          }
        }
      }
      ((Component) this).gameObject.SetActive(true);
      GameParameter.UpdateAll(((Component) this).gameObject);
    }
  }
}
