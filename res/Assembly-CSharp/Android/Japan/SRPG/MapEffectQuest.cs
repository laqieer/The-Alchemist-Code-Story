// Decompiled with JetBrains decompiler
// Type: SRPG.MapEffectQuest
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;
using System.Collections.Generic;
using UnityEngine;

namespace SRPG
{
  public class MapEffectQuest : MonoBehaviour
  {
    public GameObject GoMapEffectParent;
    public GameObject GoMapEffectBaseItem;

    public void Setup()
    {
      QuestParam dataOfClass = DataSource.FindDataOfClass<QuestParam>(this.gameObject, (QuestParam) null);
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
      MapEffectParam mapEffectParam = instanceDirect.GetMapEffectParam(dataOfClass.MapEffectId);
      if (mapEffectParam == null)
        return;
      DataSource component = this.gameObject.GetComponent<DataSource>();
      if ((bool) ((UnityEngine.Object) component))
        component.Clear();
      DataSource.Bind<MapEffectParam>(this.gameObject, mapEffectParam, false);
      if (!(bool) ((UnityEngine.Object) this.GoMapEffectParent) || !(bool) ((UnityEngine.Object) this.GoMapEffectBaseItem))
        return;
      for (int index = 0; index < mapEffectParam.ValidSkillLists.Count; ++index)
      {
        SkillParam skillParam = instanceDirect.GetSkillParam(mapEffectParam.ValidSkillLists[index]);
        if (skillParam != null)
        {
          foreach (JobParam haveJobList in MapEffectParam.GetHaveJobLists(skillParam.iname))
          {
            GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.GoMapEffectBaseItem);
            if ((bool) ((UnityEngine.Object) gameObject))
            {
              gameObject.transform.SetParent(this.GoMapEffectParent.transform);
              gameObject.transform.localScale = Vector3.one;
              DataSource.Bind<JobParam>(gameObject, haveJobList, false);
              DataSource.Bind<SkillParam>(gameObject, skillParam, false);
              gameObject.SetActive(true);
            }
          }
        }
      }
    }

    public static GameObject CreateInstance(GameObject go_target, Transform parent = null)
    {
      if (!(bool) ((UnityEngine.Object) go_target))
        return (GameObject) null;
      GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(go_target);
      if (!(bool) ((UnityEngine.Object) gameObject))
        return (GameObject) null;
      if ((bool) ((UnityEngine.Object) parent))
        gameObject.transform.SetParent(parent);
      RectTransform component1 = go_target.GetComponent<RectTransform>();
      if ((UnityEngine.Object) component1 != (UnityEngine.Object) null && (UnityEngine.Object) go_target.GetComponent<Canvas>() != (UnityEngine.Object) null)
      {
        RectTransform component2 = gameObject.GetComponent<RectTransform>();
        if ((bool) ((UnityEngine.Object) component2))
        {
          component2.anchorMax = component1.anchorMax;
          component2.anchorMin = component1.anchorMin;
          component2.anchoredPosition = component1.anchoredPosition;
          component2.sizeDelta = component1.sizeDelta;
        }
      }
      gameObject.transform.localScale = Vector3.one;
      return gameObject;
    }
  }
}
