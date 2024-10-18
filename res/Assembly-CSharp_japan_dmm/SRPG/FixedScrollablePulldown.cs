// Decompiled with JetBrains decompiler
// Type: SRPG.FixedScrollablePulldown
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;

#nullable disable
namespace SRPG
{
  public class FixedScrollablePulldown : ScrollablePulldownBase
  {
    public void ResetAllItems()
    {
      foreach (PulldownItem pulldownItem in this.Items)
      {
        GameUtility.RemoveComponent<SRPG_Button>(((Component) pulldownItem).gameObject);
        ((Component) pulldownItem).gameObject.SetActive(false);
      }
      this.ResetAllStatus();
    }

    public PulldownItem SetItem(string label, int index, int value)
    {
      if (index < 0 || index >= this.Items.Count)
        return (PulldownItem) null;
      PulldownItem pulldownItem = this.Items[index];
      if (Object.op_Inequality((Object) pulldownItem.Text, (Object) null))
        pulldownItem.Text.text = label;
      pulldownItem.Value = value;
      GameObject gameObject = ((Component) pulldownItem).gameObject;
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
