// Decompiled with JetBrains decompiler
// Type: SRPG.MapEffectQuest
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
namespace SRPG
{
  public class MapEffectQuest : MonoBehaviour
  {
    public GameObject GoMapEffectParent;
    public GameObject GoMapEffectBaseItem;

    public void Setup()
    {
      QuestParam dataOfClass = DataSource.FindDataOfClass<QuestParam>(((Component) this).gameObject, (QuestParam) null);
      if (dataOfClass == null)
        return;
      GameManager instanceDirect = MonoSingleton<GameManager>.GetInstanceDirect();
      if (!Object.op_Implicit((Object) instanceDirect))
        return;
      if (Object.op_Implicit((Object) this.GoMapEffectParent) && Object.op_Implicit((Object) this.GoMapEffectBaseItem))
      {
        this.GoMapEffectBaseItem.SetActive(false);
        BattleUnitDetail.DestroyChildGameObjects(this.GoMapEffectParent, new List<GameObject>((IEnumerable<GameObject>) new GameObject[1]
        {
          this.GoMapEffectBaseItem
        }));
      }
      MapEffectParam mapEffectParam = instanceDirect.GetMapEffectParam(dataOfClass.MapEffectId);
      if (mapEffectParam == null)
        return;
      DataSource component = ((Component) this).gameObject.GetComponent<DataSource>();
      if (Object.op_Implicit((Object) component))
        component.Clear();
      DataSource.Bind<MapEffectParam>(((Component) this).gameObject, mapEffectParam);
      if (!Object.op_Implicit((Object) this.GoMapEffectParent) || !Object.op_Implicit((Object) this.GoMapEffectBaseItem))
        return;
      for (int index = 0; index < mapEffectParam.ValidSkillLists.Count; ++index)
      {
        SkillParam skillParam = instanceDirect.GetSkillParam(mapEffectParam.ValidSkillLists[index]);
        if (skillParam != null)
        {
          foreach (JobParam haveJobList in MapEffectParam.GetHaveJobLists(skillParam.iname))
          {
            GameObject gameObject = Object.Instantiate<GameObject>(this.GoMapEffectBaseItem);
            if (Object.op_Implicit((Object) gameObject))
            {
              gameObject.transform.SetParent(this.GoMapEffectParent.transform);
              gameObject.transform.localScale = Vector3.one;
              DataSource.Bind<JobParam>(gameObject, haveJobList);
              DataSource.Bind<SkillParam>(gameObject, skillParam);
              gameObject.SetActive(true);
            }
          }
        }
      }
    }

    public static GameObject CreateInstance(GameObject go_target, Transform parent = null)
    {
      if (!Object.op_Implicit((Object) go_target))
        return (GameObject) null;
      GameObject instance = Object.Instantiate<GameObject>(go_target);
      if (!Object.op_Implicit((Object) instance))
        return (GameObject) null;
      if (Object.op_Implicit((Object) parent))
        instance.transform.SetParent(parent);
      RectTransform component1 = go_target.GetComponent<RectTransform>();
      if (Object.op_Inequality((Object) component1, (Object) null) && Object.op_Inequality((Object) go_target.GetComponent<Canvas>(), (Object) null))
      {
        RectTransform component2 = instance.GetComponent<RectTransform>();
        if (Object.op_Implicit((Object) component2))
        {
          component2.anchorMax = component1.anchorMax;
          component2.anchorMin = component1.anchorMin;
          component2.anchoredPosition = component1.anchoredPosition;
          component2.sizeDelta = component1.sizeDelta;
        }
      }
      instance.transform.localScale = Vector3.one;
      return instance;
    }
  }
}
