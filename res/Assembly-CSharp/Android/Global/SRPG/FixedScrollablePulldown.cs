// Decompiled with JetBrains decompiler
// Type: SRPG.FixedScrollablePulldown
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

namespace SRPG
{
  public class FixedScrollablePulldown : ScrollablePulldownBase
  {
    public void ResetAllItems()
    {
      using (List<PulldownItem>.Enumerator enumerator = this.Items.GetEnumerator())
      {
        while (enumerator.MoveNext())
        {
          PulldownItem current = enumerator.Current;
          GameUtility.RemoveComponent<SRPG_Button>(current.gameObject);
          current.gameObject.SetActive(false);
        }
      }
      this.ResetAllStatus();
    }

    public PulldownItem SetItem(string label, int index, int value)
    {
      if (index < 0 || index >= this.Items.Count)
        return (PulldownItem) null;
      PulldownItem pulldownItem = this.Items[index];
      if ((UnityEngine.Object) pulldownItem.Text != (UnityEngine.Object) null)
        pulldownItem.Text.text = label;
      pulldownItem.Value = value;
      GameObject gameObject = pulldownItem.gameObject;
      GameUtility.RequireComponent<SRPG_Button>(gameObject).AddListener((SRPG_Button.ButtonClickEvent) (g =>
      {
        this.Selection = value;
        this.ClosePulldown(false);
        this.TriggerItemChange();
      }));
      gameObject.transform.SetParent((Transform) this.ItemHolder, false);
      gameObject.SetActive(true);
      return pulldownItem;
    }
  }
}
