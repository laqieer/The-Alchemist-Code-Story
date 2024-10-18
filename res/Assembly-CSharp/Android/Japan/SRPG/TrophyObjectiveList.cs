// Decompiled with JetBrains decompiler
// Type: SRPG.TrophyObjectiveList
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;
using UnityEngine;

namespace SRPG
{
  public class TrophyObjectiveList : MonoBehaviour
  {
    public GameObject Item_Complete;
    public GameObject Item_Incomplete;

    public virtual void Start()
    {
      if ((UnityEngine.Object) this.Item_Complete != (UnityEngine.Object) null)
        this.Item_Complete.SetActive(false);
      if ((UnityEngine.Object) this.Item_Incomplete != (UnityEngine.Object) null)
        this.Item_Incomplete.SetActive(false);
      TrophyParam dataOfClass = DataSource.FindDataOfClass<TrophyParam>(this.gameObject, (TrophyParam) null);
      if (dataOfClass == null)
        return;
      TrophyState trophyCounter = MonoSingleton<GameManager>.Instance.Player.GetTrophyCounter(dataOfClass, false);
      Transform transform = this.transform;
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
        GameObject original = !flag ? this.Item_Incomplete : this.Item_Complete;
        if (!((UnityEngine.Object) original == (UnityEngine.Object) null))
        {
          GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(original);
          DataSource.Bind<TrophyObjectiveData>(gameObject, data, false);
          gameObject.transform.SetParent(transform, false);
          gameObject.SetActive(true);
        }
      }
    }
  }
}
