// Decompiled with JetBrains decompiler
// Type: SRPG.FilterDispatcher
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

namespace SRPG
{
  public class FilterDispatcher : MonoBehaviour, ISortableList
  {
    public GameObject[] Targets;

    public void SetSortMethod(string method, bool ascending, string[] filters)
    {
      if (this.Targets == null)
        return;
      foreach (GameObject target in this.Targets)
      {
        if (!((UnityEngine.Object) target == (UnityEngine.Object) null))
          target.GetComponent<ISortableList>()?.SetSortMethod(method, ascending, filters);
      }
    }
  }
}
