// Decompiled with JetBrains decompiler
// Type: SRPG.InventorySlot
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using UnityEngine;

#nullable disable
namespace SRPG
{
  public class InventorySlot : MonoBehaviour
  {
    public static InventorySlot Active;
    public Animator StateAnimator;
    public string AnimatorBoolName = "active";
    public GameObject Empty;
    public GameObject NonEmpty;
    public GameObject[] HideIfEmpty = new GameObject[0];
    public int Index;
    public SRPG_Button Button;

    public void SetItem(ItemData item)
    {
      DataSource.Bind<ItemData>(((Component) this).gameObject, item);
      bool flag = item == null;
      if (Object.op_Inequality((Object) this.Empty, (Object) null))
        this.Empty.SetActive(flag);
      if (Object.op_Inequality((Object) this.NonEmpty, (Object) null))
        this.NonEmpty.SetActive(!flag);
      GameParameter.UpdateAll(((Component) this).gameObject);
    }

    public void Select() => InventorySlot.Active = this;

    public void Update()
    {
      if (Object.op_Inequality((Object) this.StateAnimator, (Object) null) && !string.IsNullOrEmpty(this.AnimatorBoolName))
        this.StateAnimator.SetBool(this.AnimatorBoolName, Object.op_Equality((Object) InventorySlot.Active, (Object) this));
      PlayerData player = MonoSingleton<GameManager>.Instance.Player;
      bool flag = false;
      if (0 <= this.Index && this.Index < player.Inventory.Length)
        flag = player.Inventory[this.Index] != null && player.Inventory[this.Index].Param != null;
      for (int index = 0; index < this.HideIfEmpty.Length; ++index)
      {
        if (Object.op_Inequality((Object) this.HideIfEmpty[index], (Object) null))
          this.HideIfEmpty[index].SetActive(flag);
      }
    }
  }
}
