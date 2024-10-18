// Decompiled with JetBrains decompiler
// Type: SRPG.MapEffectJob
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
namespace SRPG
{
  public class MapEffectJob : MonoBehaviour
  {
    public GameObject GoMapEffectParent;
    public GameObject GoMapEffectBaseItem;

    public void Setup()
    {
      JobParam dataOfClass = DataSource.FindDataOfClass<JobParam>(((Component) this).gameObject, (JobParam) null);
      if (dataOfClass == null)
        return;
      GameManager instanceDirect = MonoSingleton<GameManager>.GetInstanceDirect();
      if (!UnityEngine.Object.op_Implicit((UnityEngine.Object) instanceDirect))
        return;
      if (UnityEngine.Object.op_Implicit((UnityEngine.Object) this.GoMapEffectParent) && UnityEngine.Object.op_Implicit((UnityEngine.Object) this.GoMapEffectBaseItem))
      {
        this.GoMapEffectBaseItem.SetActive(false);
        BattleUnitDetail.DestroyChildGameObjects(this.GoMapEffectParent, new List<GameObject>((IEnumerable<GameObject>) new GameObject[1]
        {
          this.GoMapEffectBaseItem
        }));
      }
      DataSource component = ((Component) this).gameObject.GetComponent<DataSource>();
      if (UnityEngine.Object.op_Implicit((UnityEngine.Object) component))
        component.Clear();
      DataSource.Bind<JobParam>(((Component) this).gameObject, dataOfClass);
      if (!UnityEngine.Object.op_Implicit((UnityEngine.Object) this.GoMapEffectParent) || !UnityEngine.Object.op_Implicit((UnityEngine.Object) this.GoMapEffectBaseItem))
        return;
      List<MapEffectJob.MeSkill> l = new List<MapEffectJob.MeSkill>();
      if (!string.IsNullOrEmpty(dataOfClass.MapEffectAbility))
      {
        AbilityParam abilityParam = instanceDirect.GetAbilityParam(dataOfClass.MapEffectAbility);
        if (abilityParam != null)
        {
          foreach (LearningSkill skill in abilityParam.skills)
          {
            SkillParam skillParam = instanceDirect.GetSkillParam(skill.iname);
            if (skillParam != null)
            {
              foreach (MapEffectParam haveMapEffectList in MapEffectParam.GetHaveMapEffectLists(skill.iname))
              {
                bool flag = false;
                foreach (MapEffectJob.MeSkill meSkill in l)
                {
                  if (meSkill.Equals(haveMapEffectList, skillParam))
                  {
                    flag = true;
                    break;
                  }
                }
                if (!flag)
                  l.Add(new MapEffectJob.MeSkill(haveMapEffectList, skillParam));
              }
            }
          }
        }
      }
      if (l.Count == 0)
        return;
      MySort<MapEffectJob.MeSkill>.Sort(l, (Comparison<MapEffectJob.MeSkill>) ((src, dsc) => src == dsc ? 0 : dsc.mMapEffectParam.Index - src.mMapEffectParam.Index));
      foreach (MapEffectJob.MeSkill meSkill in l)
      {
        GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.GoMapEffectBaseItem);
        if (UnityEngine.Object.op_Implicit((UnityEngine.Object) gameObject))
        {
          gameObject.transform.SetParent(this.GoMapEffectParent.transform);
          gameObject.transform.localScale = Vector3.one;
          DataSource.Bind<MapEffectParam>(gameObject, meSkill.mMapEffectParam);
          DataSource.Bind<SkillParam>(gameObject, meSkill.mSkillParam);
          gameObject.SetActive(true);
        }
      }
    }

    private class MeSkill
    {
      public MapEffectParam mMapEffectParam;
      public SkillParam mSkillParam;

      public MeSkill(MapEffectParam me_param, SkillParam skill_param)
      {
        this.mMapEffectParam = me_param;
        this.mSkillParam = skill_param;
      }

      public bool Equals(MapEffectParam me_param, SkillParam skill_param)
      {
        return this.mMapEffectParam == me_param && this.mSkillParam == skill_param;
      }
    }
  }
}
