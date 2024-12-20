﻿// Decompiled with JetBrains decompiler
// Type: SRPG.SortFilterMode
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

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
