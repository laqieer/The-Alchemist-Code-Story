// Decompiled with JetBrains decompiler
// Type: SRPG.BattleInventory
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  public class BattleInventory : MonoBehaviour
  {
    public bool DisplayEmptySlots = true;
    private List<ListItemEvents> mItems = new List<ListItemEvents>();
    public BattleInventory.SelectEvent OnSelectItem;
    public RectTransform ListParent;
    public ListItemEvents ItemTemplate;
    public ListItemEvents EmptySlotTemplate;
    public ItemData[] mInventory;

    private void Start()
    {
      if ((UnityEngine.Object) this.ItemTemplate != (UnityEngine.Object) null)
      {
        this.ItemTemplate.gameObject.SetActive(false);
        if ((UnityEngine.Object) this.ListParent == (UnityEngine.Object) null)
          this.ListParent = this.ItemTemplate.transform.parent as RectTransform;
      }
      if ((UnityEngine.Object) this.EmptySlotTemplate != (UnityEngine.Object) null)
        this.EmptySlotTemplate.gameObject.SetActive(false);
      this.Refresh();
    }

    public void Refresh()
    {
      GameUtility.DestroyGameObjects<ListItemEvents>(this.mItems);
      if ((UnityEngine.Object) this.ItemTemplate == (UnityEngine.Object) null || (UnityEngine.Object) this.ListParent == (UnityEngine.Object) null)
        return;
      this.mInventory = SceneBattle.Instance.Battle.mInventory;
      for (int index1 = 0; index1 < this.mInventory.Length; ++index1)
      {
        if (this.DisplayEmptySlots)
        {
          ListItemEvents original = !((UnityEngine.Object) SceneBattle.Instance != (UnityEngine.Object) null) || !SceneBattle.Instance.Battle.IsMultiPlay ? (!((UnityEngine.Object) this.EmptySlotTemplate != (UnityEngine.Object) null) || this.mInventory[index1] != null && this.mInventory[index1].Param != null ? this.ItemTemplate : this.EmptySlotTemplate) : this.EmptySlotTemplate;
          ListItemEvents listItemEvents = UnityEngine.Object.Instantiate<ListItemEvents>(original);
          listItemEvents.gameObject.SetActive(true);
          listItemEvents.transform.SetParent((Transform) this.ListParent, false);
          this.mItems.Add(listItemEvents);
          if (!((UnityEngine.Object) original == (UnityEngine.Object) this.EmptySlotTemplate))
          {
            DataSource.Bind<ItemData>(listItemEvents.gameObject, this.mInventory[index1], false);
            bool flag = false;
            if (this.mInventory[index1] != null && this.mInventory[index1].Param != null && this.mInventory[index1].Num > 0)
            {
              Unit currentUnit = SceneBattle.Instance.Battle.CurrentUnit;
              if (currentUnit != null)
                flag = currentUnit.CheckEnableUseSkill(this.mInventory[index1].Skill, false);
            }
            Selectable[] componentsInChildren = listItemEvents.gameObject.GetComponentsInChildren<Selectable>(true);
            if (componentsInChildren != null)
            {
              for (int index2 = componentsInChildren.Length - 1; index2 >= 0; --index2)
                componentsInChildren[index2].interactable = flag;
            }
            listItemEvents.OnSelect = (ListItemEvents.ListItemEvent) (go =>
            {
              ItemData dataOfClass = DataSource.FindDataOfClass<ItemData>(go, (ItemData) null);
              if (dataOfClass == null || dataOfClass.Param == null || this.OnSelectItem == null)
                return;
              this.OnSelectItem(dataOfClass);
            });
          }
        }
      }
    }

    public delegate void SelectEvent(ItemData item);
  }
}
