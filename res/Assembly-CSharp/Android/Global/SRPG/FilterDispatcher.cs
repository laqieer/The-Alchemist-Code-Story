// Decompiled with JetBrains decompiler
// Type: SRPG.FilterDispatcher
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

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
