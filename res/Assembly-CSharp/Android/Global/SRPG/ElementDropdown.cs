// Decompiled with JetBrains decompiler
// Type: SRPG.ElementDropdown
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  public class ElementDropdown : Pulldown
  {
    [SerializeField]
    private Image ElementIcon;

    protected override void UpdateSelection()
    {
      if (!((UnityEngine.Object) this.ElementIcon != (UnityEngine.Object) null))
        return;
      ElementDropdownItem currentSelection = this.GetCurrentSelection() as ElementDropdownItem;
      if (!((UnityEngine.Object) currentSelection != (UnityEngine.Object) null))
        return;
      this.ElementIcon.sprite = currentSelection.IconImage.sprite;
    }

    protected override PulldownItem SetupPulldownItem(GameObject itemObject)
    {
      ElementDropdownItem elementDropdownItem = itemObject.GetComponent<ElementDropdownItem>();
      if ((UnityEngine.Object) elementDropdownItem == (UnityEngine.Object) null)
        elementDropdownItem = itemObject.AddComponent<ElementDropdownItem>();
      return (PulldownItem) elementDropdownItem;
    }

    public PulldownItem AddItem(string label, Sprite sprite, int value)
    {
      PulldownItem pulldownItem = this.AddItem(label, value);
      ElementDropdownItem elementDropdownItem = pulldownItem as ElementDropdownItem;
      if ((UnityEngine.Object) elementDropdownItem != (UnityEngine.Object) null)
        elementDropdownItem.IconImage.sprite = sprite;
      return pulldownItem;
    }
  }
}
