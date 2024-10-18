// Decompiled with JetBrains decompiler
// Type: SRPG.InventorySlot
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;
using UnityEngine;

namespace SRPG
{
  public class InventorySlot : MonoBehaviour
  {
    public string AnimatorBoolName = "active";
    public GameObject[] HideIfEmpty = new GameObject[0];
    public static InventorySlot Active;
    public Animator StateAnimator;
    public GameObject Empty;
    public GameObject NonEmpty;
    public int Index;
    public SRPG_Button Button;

    public void SetItem(ItemData item)
    {
      DataSource.Bind<ItemData>(this.gameObject, item, false);
      bool flag = item == null;
      if ((UnityEngine.Object) this.Empty != (UnityEngine.Object) null)
        this.Empty.SetActive(flag);
      if ((UnityEngine.Object) this.NonEmpty != (UnityEngine.Object) null)
        this.NonEmpty.SetActive(!flag);
      GameParameter.UpdateAll(this.gameObject);
    }

    public void Select()
    {
      InventorySlot.Active = this;
    }

    public void Update()
    {
      if ((UnityEngine.Object) this.StateAnimator != (UnityEngine.Object) null && !string.IsNullOrEmpty(this.AnimatorBoolName))
        this.StateAnimator.SetBool(this.AnimatorBoolName, (UnityEngine.Object) InventorySlot.Active == (UnityEngine.Object) this);
      PlayerData player = MonoSingleton<GameManager>.Instance.Player;
      bool flag = false;
      if (0 <= this.Index && this.Index < player.Inventory.Length)
        flag = player.Inventory[this.Index] != null && player.Inventory[this.Index].Param != null;
      for (int index = 0; index < this.HideIfEmpty.Length; ++index)
      {
        if ((UnityEngine.Object) this.HideIfEmpty[index] != (UnityEngine.Object) null)
          this.HideIfEmpty[index].SetActive(flag);
      }
    }
  }
}
