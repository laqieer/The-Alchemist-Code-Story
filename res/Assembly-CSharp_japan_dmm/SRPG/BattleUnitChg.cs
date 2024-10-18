// Decompiled with JetBrains decompiler
// Type: SRPG.BattleUnitChg
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  public class BattleUnitChg : MonoBehaviour
  {
    public BattleUnitChg.SelectEvent OnSelectUnit;
    public RectTransform ListParent;
    public ListItemEvents UnitTemplate;
    public ListItemEvents EmptyTemplate;
    private const int SUB_UNIT_MAX = 2;
    private List<Unit> mSubUnitLists = new List<Unit>(2);
    private List<ListItemEvents> mUnitEvents = new List<ListItemEvents>();

    private void Start()
    {
      if (Object.op_Inequality((Object) this.UnitTemplate, (Object) null))
      {
        ((Component) this.UnitTemplate).gameObject.SetActive(false);
        if (Object.op_Equality((Object) this.ListParent, (Object) null))
          this.ListParent = ((Component) this.UnitTemplate).transform.parent as RectTransform;
      }
      if (Object.op_Inequality((Object) this.EmptyTemplate, (Object) null))
        ((Component) this.EmptyTemplate).gameObject.SetActive(false);
      this.Refresh();
    }

    public void Refresh()
    {
      GameUtility.DestroyGameObjects<ListItemEvents>(this.mUnitEvents);
      this.mUnitEvents.Clear();
      if (Object.op_Equality((Object) this.UnitTemplate, (Object) null) || Object.op_Equality((Object) this.EmptyTemplate, (Object) null) || Object.op_Equality((Object) this.ListParent, (Object) null))
        return;
      SceneBattle instance = SceneBattle.Instance;
      BattleCore battleCore = (BattleCore) null;
      if (Object.op_Implicit((Object) instance))
        battleCore = instance.Battle;
      if (battleCore == null)
        return;
      this.mSubUnitLists.Clear();
      foreach (Unit unit in battleCore.Player)
      {
        if (unit != null && !unit.IsUnitFlag(EUnitFlag.CreatedBreakObj) && !unit.IsUnitFlag(EUnitFlag.IsDynamicTransform) && !battleCore.StartingMembers.Contains(unit))
          this.mSubUnitLists.Add(unit);
      }
      for (int index1 = 0; index1 < 2; ++index1)
      {
        if (index1 >= this.mSubUnitLists.Count)
        {
          ListItemEvents listItemEvents = Object.Instantiate<ListItemEvents>(this.EmptyTemplate);
          ((Component) listItemEvents).transform.SetParent((Transform) this.ListParent, false);
          this.mUnitEvents.Add(listItemEvents);
          ((Component) listItemEvents).gameObject.SetActive(true);
        }
        else
        {
          Unit mSubUnitList = this.mSubUnitLists[index1];
          if (mSubUnitList != null)
          {
            ListItemEvents listItemEvents = Object.Instantiate<ListItemEvents>(this.UnitTemplate);
            DataSource.Bind<Unit>(((Component) listItemEvents).gameObject, mSubUnitList);
            ((Component) listItemEvents).transform.SetParent((Transform) this.ListParent, false);
            this.mUnitEvents.Add(listItemEvents);
            ((Component) listItemEvents).gameObject.SetActive(true);
            bool flag = !mSubUnitList.IsDead && mSubUnitList.IsEntry && mSubUnitList.IsSub;
            Selectable[] componentsInChildren = ((Component) listItemEvents).gameObject.GetComponentsInChildren<Selectable>(true);
            if (componentsInChildren != null)
            {
              for (int index2 = componentsInChildren.Length - 1; index2 >= 0; --index2)
                componentsInChildren[index2].interactable = flag;
            }
            listItemEvents.OnSelect = (ListItemEvents.ListItemEvent) (go =>
            {
              if (this.OnSelectUnit == null)
                return;
              Unit dataOfClass = DataSource.FindDataOfClass<Unit>(go, (Unit) null);
              if (dataOfClass == null)
                return;
              this.OnSelectUnit(dataOfClass);
            });
          }
        }
      }
    }

    public delegate void SelectEvent(Unit unit);
  }
}
