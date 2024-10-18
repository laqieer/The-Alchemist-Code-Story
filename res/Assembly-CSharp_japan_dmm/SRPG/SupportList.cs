// Decompiled with JetBrains decompiler
// Type: SRPG.SupportList
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
namespace SRPG
{
  public class SupportList : UnitListV2
  {
    public RectTransform UnitListHilit;

    private List<UnitData> mOwnUnits => MonoSingleton<GameManager>.Instance.Player.Units;

    public override void SetData(object[] src, System.Type type)
    {
      if ((long) GlobalVars.SelectedSupportUnitUniqueID > 0L)
      {
        List<UnitData> collection = new List<UnitData>((IEnumerable<UnitData>) (src as UnitData[]));
        UnitData unitData1 = collection.Find((Predicate<UnitData>) (x => x.UniqueID == (long) GlobalVars.SelectedSupportUnitUniqueID));
        int index = 0;
        if (unitData1 == null)
        {
          UnitData unitData2 = this.mOwnUnits.Find((Predicate<UnitData>) (x => x.UniqueID == (long) GlobalVars.SelectedSupportUnitUniqueID));
          if (unitData2 != null)
          {
            collection.Add(unitData2);
            index = collection.Count - 1;
            GameUtility.SortUnits(new List<UnitData>((IEnumerable<UnitData>) collection), this.mUnitSortMode, false, out this.mSortValues);
          }
        }
        else
          index = collection.IndexOf(unitData1);
        UnitData unitData3 = collection[index];
        collection.RemoveAt(index);
        collection.Insert(0, unitData3);
        if (this.mSortValues != null)
        {
          List<int> intList = new List<int>((IEnumerable<int>) this.mSortValues);
          int num = intList[index];
          intList.RemoveAt(index);
          intList.Insert(0, num);
          this.mSortValues = intList.ToArray();
        }
        src = (object[]) collection.ToArray();
      }
      base.SetData(src, type);
      GameParameter.UpdateAll(((Component) this).gameObject);
      this.mPage = 0;
    }

    protected override void RefreshItems()
    {
      base.RefreshItems();
      if (this.mItems.Count > 0)
        ((Transform) this.UnitListHilit).SetParent(((Component) this.mItems[0].GetComponent<UnitIcon>().Frame).transform, false);
      ((Component) this.UnitListHilit).gameObject.SetActive(this.mPage == 0);
    }
  }
}
