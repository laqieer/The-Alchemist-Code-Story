// Decompiled with JetBrains decompiler
// Type: SRPG.TrophyObjectiveList
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;

#nullable disable
namespace SRPG
{
  public class TrophyObjectiveList : MonoBehaviour
  {
    public GameObject Item_Complete;
    public GameObject Item_Incomplete;

    public virtual void Start()
    {
      if (Object.op_Inequality((Object) this.Item_Complete, (Object) null))
        this.Item_Complete.SetActive(false);
      if (Object.op_Inequality((Object) this.Item_Incomplete, (Object) null))
        this.Item_Incomplete.SetActive(false);
      TrophyParam dataOfClass = DataSource.FindDataOfClass<TrophyParam>(((Component) this).gameObject, (TrophyParam) null);
      if (dataOfClass == null)
        return;
      TrophyState trophyCounter = dataOfClass.GetTrophyCounter();
      Transform transform = ((Component) this).transform;
      for (int index = 0; index < dataOfClass.Objectives.Length; ++index)
      {
        TrophyObjective objective = dataOfClass.Objectives[index];
        TrophyObjectiveData data = new TrophyObjectiveData();
        data.CountMax = objective.RequiredCount;
        data.Description = objective.GetDescription();
        data.Objective = objective;
        bool flag;
        if (index < trophyCounter.Count.Length)
        {
          data.Count = trophyCounter.Count[index];
          flag = objective.RequiredCount <= trophyCounter.Count[index];
        }
        else
          flag = false;
        GameObject gameObject1 = !flag ? this.Item_Incomplete : this.Item_Complete;
        if (!Object.op_Equality((Object) gameObject1, (Object) null))
        {
          GameObject gameObject2 = Object.Instantiate<GameObject>(gameObject1);
          DataSource.Bind<TrophyObjectiveData>(gameObject2, data);
          gameObject2.transform.SetParent(transform, false);
          gameObject2.SetActive(true);
        }
      }
    }
  }
}
