// Decompiled with JetBrains decompiler
// Type: SRPG.SortFilterMode
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

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
      GameUtility.SetGameObjectActive(this.Ascending, ascending);
      GameUtility.SetGameObjectActive(this.Descending, !ascending);
      GameUtility.SetGameObjectActive(this.FilterOn, filters != null);
      GameUtility.SetGameObjectActive(this.FilterOff, filters == null);
    }
  }
}
