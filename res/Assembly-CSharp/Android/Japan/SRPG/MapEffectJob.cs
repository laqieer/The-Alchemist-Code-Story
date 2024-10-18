// Decompiled with JetBrains decompiler
// Type: SRPG.MapEffectJob
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace SRPG
{
  public class MapEffectJob : MonoBehaviour
  {
    public GameObject GoMapEffectParent;
    public GameObject GoMapEffectBaseItem;

    public void Setup()
    {
      JobParam dataOfClass = DataSource.FindDataOfClass<JobParam>(this.gameObject, (JobParam) null);
      if (dataOfClass == null)
        return;
      GameManager instanceDirect = MonoSingleton<GameManager>.GetInstanceDirect();
      if (!(bool) ((UnityEngine.Object) instanceDirect))
        return;
      if ((bool) ((UnityEngine.Object) this.GoMapEffectParent) && (bool) ((UnityEngine.Object) this.GoMapEffectBaseItem))
      {
        this.GoMapEffectBaseItem.SetActive(false);
        BattleUnitDetail.DestroyChildGameObjects(this.GoMapEffectParent, new List<GameObject>((IEnumerable<GameObject>) new GameObject[1]
        {
          this.GoMapEffectBaseItem
        }));
      }
      DataSource component = this.gameObject.GetComponent<DataSource>();
      if ((bool) ((UnityEngine.Object) component))
        component.Clear();
      DataSource.Bind<JobParam>(this.gameObject, dataOfClass, false);
      if (!(bool) ((UnityEngine.Object) this.GoMapEffectParent) || !(bool) ((UnityEngine.Object) this.GoMapEffectBaseItem))
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
      MySort<MapEffectJob.MeSkill>.Sort(l, (Comparison<MapEffectJob.MeSkill>) ((src, dsc) =>
      {
        if (src == dsc)
          return 0;
        return dsc.mMapEffectParam.Index - src.mMapEffectParam.Index;
      }));
      foreach (MapEffectJob.MeSkill meSkill in l)
      {
        GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.GoMapEffectBaseItem);
        if ((bool) ((UnityEngine.Object) gameObject))
        {
          gameObject.transform.SetParent(this.GoMapEffectParent.transform);
          gameObject.transform.localScale = Vector3.one;
          DataSource.Bind<MapEffectParam>(gameObject, meSkill.mMapEffectParam, false);
          DataSource.Bind<SkillParam>(gameObject, meSkill.mSkillParam, false);
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
        if (this.mMapEffectParam == me_param)
          return this.mSkillParam == skill_param;
        return false;
      }
    }
  }
}
