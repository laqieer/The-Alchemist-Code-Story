// Decompiled with JetBrains decompiler
// Type: SRPG.UnitLearnAbilityElement
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;
using System.Collections.Generic;
using UnityEngine;

namespace SRPG
{
  public class UnitLearnAbilityElement : MonoBehaviour, IFlowInterface
  {
    public Transform SkillParent;
    public GameObject SkillTemplate;
    private List<GameObject> mSkills;

    public void Start()
    {
      if (!((UnityEngine.Object) this.SkillTemplate != (UnityEngine.Object) null))
        return;
      this.SkillTemplate.SetActive(false);
    }

    public void Activated(int pinID)
    {
    }

    public void Refresh()
    {
      AbilityData dataOfClass = DataSource.FindDataOfClass<AbilityData>(this.gameObject, (AbilityData) null);
      if (dataOfClass != null)
      {
        this.mSkills = new List<GameObject>(dataOfClass.LearningSkills.Length);
        for (int index = 0; index < dataOfClass.LearningSkills.Length; ++index)
        {
          if (dataOfClass.LearningSkills[index] != null && dataOfClass.Rank >= dataOfClass.LearningSkills[index].locklv)
          {
            GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.SkillTemplate);
            SkillParam skillParam = MonoSingleton<GameManager>.Instance.GetSkillParam(dataOfClass.LearningSkills[index].iname);
            DataSource.Bind<SkillParam>(gameObject, skillParam, false);
            gameObject.transform.SetParent(this.SkillParent, false);
            gameObject.SetActive(true);
            this.mSkills.Add(gameObject);
          }
        }
      }
      this.gameObject.SetActive(true);
      GameParameter.UpdateAll(this.gameObject);
    }
  }
}
