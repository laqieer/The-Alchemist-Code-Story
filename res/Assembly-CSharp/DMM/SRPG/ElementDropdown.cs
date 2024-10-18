// Decompiled with JetBrains decompiler
// Type: SRPG.ElementDropdown
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  public class ElementDropdown : Pulldown
  {
    [SerializeField]
    private Image ElementIcon;

    protected override void UpdateSelection()
    {
      if (!Object.op_Inequality((Object) this.ElementIcon, (Object) null))
        return;
      ElementDropdownItem currentSelection = this.GetCurrentSelection() as ElementDropdownItem;
      if (!Object.op_Inequality((Object) currentSelection, (Object) null))
        return;
      this.ElementIcon.sprite = currentSelection.IconImage.sprite;
    }

    protected override PulldownItem SetupPulldownItem(GameObject itemObject)
    {
      ElementDropdownItem elementDropdownItem = itemObject.GetComponent<ElementDropdownItem>();
      if (Object.op_Equality((Object) elementDropdownItem, (Object) null))
        elementDropdownItem = itemObject.AddComponent<ElementDropdownItem>();
      return (PulldownItem) elementDropdownItem;
    }

    public PulldownItem AddItem(string label, Sprite sprite, int value)
    {
      PulldownItem pulldownItem = this.AddItem(label, value);
      ElementDropdownItem elementDropdownItem = pulldownItem as ElementDropdownItem;
      if (Object.op_Inequality((Object) elementDropdownItem, (Object) null))
        elementDropdownItem.IconImage.sprite = sprite;
      return pulldownItem;
    }
  }
}
