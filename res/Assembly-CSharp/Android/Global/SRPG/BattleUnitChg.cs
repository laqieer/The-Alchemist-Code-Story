// Decompiled with JetBrains decompiler
// Type: SRPG.BattleUnitChg
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  public class BattleUnitChg : MonoBehaviour
  {
    private List<Unit> mSubUnitLists = new List<Unit>(2);
    private List<ListItemEvents> mUnitEvents = new List<ListItemEvents>();
    private const int SUB_UNIT_MAX = 2;
    public BattleUnitChg.SelectEvent OnSelectUnit;
    public RectTransform ListParent;
    public ListItemEvents UnitTemplate;
    public ListItemEvents EmptyTemplate;

    private void Start()
    {
      if ((UnityEngine.Object) this.UnitTemplate != (UnityEngine.Object) null)
      {
        this.UnitTemplate.gameObject.SetActive(false);
        if ((UnityEngine.Object) this.ListParent == (UnityEngine.Object) null)
          this.ListParent = this.UnitTemplate.transform.parent as RectTransform;
      }
      if ((UnityEngine.Object) this.EmptyTemplate != (UnityEngine.Object) null)
        this.EmptyTemplate.gameObject.SetActive(false);
      this.Refresh();
    }

    public void Refresh()
    {
      GameUtility.DestroyGameObjects<ListItemEvents>(this.mUnitEvents);
      this.mUnitEvents.Clear();
      if ((UnityEngine.Object) this.UnitTemplate == (UnityEngine.Object) null || (UnityEngine.Object) this.EmptyTemplate == (UnityEngine.Object) null || (UnityEngine.Object) this.ListParent == (UnityEngine.Object) null)
        return;
      SceneBattle instance = SceneBattle.Instance;
      BattleCore battleCore = (BattleCore) null;
      if ((bool) ((UnityEngine.Object) instance))
        battleCore = instance.Battle;
      if (battleCore == null)
        return;
      this.mSubUnitLists.Clear();
      using (List<Unit>.Enumerator enumerator = battleCore.Player.GetEnumerator())
      {
        while (enumerator.MoveNext())
        {
          Unit current = enumerator.Current;
          if (!battleCore.StartingMembers.Contains(current))
            this.mSubUnitLists.Add(current);
        }
      }
      for (int index1 = 0; index1 < 2; ++index1)
      {
        if (index1 >= this.mSubUnitLists.Count)
        {
          ListItemEvents listItemEvents = UnityEngine.Object.Instantiate<ListItemEvents>(this.EmptyTemplate);
          listItemEvents.transform.SetParent((Transform) this.ListParent, false);
          this.mUnitEvents.Add(listItemEvents);
          listItemEvents.gameObject.SetActive(true);
        }
        else
        {
          Unit mSubUnitList = this.mSubUnitLists[index1];
          if (mSubUnitList != null)
          {
            ListItemEvents listItemEvents = UnityEngine.Object.Instantiate<ListItemEvents>(this.UnitTemplate);
            DataSource.Bind<Unit>(listItemEvents.gameObject, mSubUnitList);
            listItemEvents.transform.SetParent((Transform) this.ListParent, false);
            this.mUnitEvents.Add(listItemEvents);
            listItemEvents.gameObject.SetActive(true);
            bool flag = !mSubUnitList.IsDead && mSubUnitList.IsEntry && mSubUnitList.IsSub;
            Selectable[] componentsInChildren = listItemEvents.gameObject.GetComponentsInChildren<Selectable>(true);
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
