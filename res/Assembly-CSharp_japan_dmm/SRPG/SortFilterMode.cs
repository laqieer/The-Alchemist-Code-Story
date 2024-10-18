// Decompiled with JetBrains decompiler
// Type: SRPG.SortFilterMode
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;

#nullable disable
namespace SRPG
{
  public class SortFilterMode : MonoBehaviour, ISortableList
  {
    public GameObject Ascending;
    public GameObject Descending;
    public GameObject FilterOn;
    public GameObject FilterOff;

    public void SetSortMethod(string method, bool ascending, string[] filters)
    {
      bool active = false;
      GameUtility.SetGameObjectActive(this.Ascending, ascending);
      GameUtility.SetGameObjectActive(this.Descending, !ascending);
      if (filters != null && filters.Length != 0)
        active = true;
      GameUtility.SetGameObjectActive(this.FilterOn, active);
      GameUtility.SetGameObjectActive(this.FilterOff, !active);
    }
  }
}
