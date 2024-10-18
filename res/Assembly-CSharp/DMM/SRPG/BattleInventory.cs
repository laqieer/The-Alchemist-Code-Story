// Decompiled with JetBrains decompiler
// Type: SRPG.BattleInventory
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  public class BattleInventory : MonoBehaviour
  {
    public BattleInventory.SelectEvent OnSelectItem;
    public RectTransform ListParent;
    public ListItemEvents ItemTemplate;
    public ListItemEvents EmptySlotTemplate;
    public bool DisplayEmptySlots = true;
    public GameObject EmptyLabel;
    private List<ListItemEvents> mItems = new List<ListItemEvents>();
    public ItemData[] mInventory;

    private void Start()
    {
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.ItemTemplate, (UnityEngine.Object) null))
      {
        ((Component) this.ItemTemplate).gameObject.SetActive(false);
        if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.ListParent, (UnityEngine.Object) null))
          this.ListParent = ((Component) this.ItemTemplate).transform.parent as RectTransform;
      }
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.EmptySlotTemplate, (UnityEngine.Object) null))
        ((Component) this.EmptySlotTemplate).gameObject.SetActive(false);
      this.Refresh();
    }

    public void Refresh()
    {
      GameUtility.DestroyGameObjects<ListItemEvents>(this.mItems);
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.ItemTemplate, (UnityEngine.Object) null) || UnityEngine.Object.op_Equality((UnityEngine.Object) this.ListParent, (UnityEngine.Object) null))
        return;
      this.mInventory = SceneBattle.Instance.Battle.mInventory;
      for (int index1 = 0; index1 < this.mInventory.Length; ++index1)
      {
        if (this.DisplayEmptySlots)
        {
          ListItemEvents listItemEvents1 = !UnityEngine.Object.op_Inequality((UnityEngine.Object) SceneBattle.Instance, (UnityEngine.Object) null) || !SceneBattle.Instance.Battle.IsMultiPlay ? (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.EmptySlotTemplate, (UnityEngine.Object) null) || this.mInventory[index1] != null && this.mInventory[index1].Param != null ? this.ItemTemplate : this.EmptySlotTemplate) : this.EmptySlotTemplate;
          ListItemEvents listItemEvents2 = UnityEngine.Object.Instantiate<ListItemEvents>(listItemEvents1);
          ((Component) listItemEvents2).gameObject.SetActive(true);
          ((Component) listItemEvents2).transform.SetParent((Transform) this.ListParent, false);
          this.mItems.Add(listItemEvents2);
          if (!UnityEngine.Object.op_Equality((UnityEngine.Object) listItemEvents1, (UnityEngine.Object) this.EmptySlotTemplate))
          {
            DataSource.Bind<ItemData>(((Component) listItemEvents2).gameObject, this.mInventory[index1]);
            bool flag = false;
            if (this.mInventory[index1] != null && this.mInventory[index1].Param != null && this.mInventory[index1].Num > 0)
            {
              Unit currentUnit = SceneBattle.Instance.Battle.CurrentUnit;
              if (currentUnit != null)
                flag = currentUnit.CheckEnableUseSkill(this.mInventory[index1].Skill);
            }
            Selectable[] componentsInChildren = ((Component) listItemEvents2).gameObject.GetComponentsInChildren<Selectable>(true);
            if (componentsInChildren != null)
            {
              for (int index2 = componentsInChildren.Length - 1; index2 >= 0; --index2)
                componentsInChildren[index2].interactable = flag;
            }
            listItemEvents2.OnSelect = (ListItemEvents.ListItemEvent) (go =>
            {
              ItemData dataOfClass = DataSource.FindDataOfClass<ItemData>(go, (ItemData) null);
              if (dataOfClass == null || dataOfClass.Param == null || this.OnSelectItem == null)
                return;
              this.OnSelectItem(dataOfClass);
            });
          }
        }
      }
      if (Array.Find<ItemData>(this.mInventory, (Predicate<ItemData>) (ivt => ivt != null && ivt.Param != null)) == null)
        GameUtility.SetGameObjectActive(this.EmptyLabel, true);
      else
        GameUtility.SetGameObjectActive(this.EmptyLabel, false);
    }

    public delegate void SelectEvent(ItemData item);
  }
}
