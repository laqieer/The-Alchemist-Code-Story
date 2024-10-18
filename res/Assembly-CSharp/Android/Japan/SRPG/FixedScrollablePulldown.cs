// Decompiled with JetBrains decompiler
// Type: SRPG.FixedScrollablePulldown
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

namespace SRPG
{
  public class FixedScrollablePulldown : ScrollablePulldownBase
  {
    public void ResetAllItems()
    {
      foreach (PulldownItem pulldownItem in this.Items)
      {
        GameUtility.RemoveComponent<SRPG_Button>(pulldownItem.gameObject);
        pulldownItem.gameObject.SetActive(false);
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
