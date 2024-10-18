// Decompiled with JetBrains decompiler
// Type: SRPG.FilterDispatcher
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;

#nullable disable
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
        if (!Object.op_Equality((Object) target, (Object) null))
          target.GetComponent<ISortableList>()?.SetSortMethod(method, ascending, filters);
      }
    }
  }
}
