﻿// Decompiled with JetBrains decompiler
// Type: SRPG.SupportList
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using GR;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace SRPG
{
  public class SupportList : UnitListV2
  {
    public RectTransform UnitListHilit;

    private List<UnitData> mOwnUnits
    {
      get
      {
        return MonoSingleton<GameManager>.Instance.Player.Units;
      }
    }

    public override void SetData(object[] src, System.Type type)
    {
      if ((long) GlobalVars.SelectedSupportUnitUniqueID > 0L)
      {
        List<UnitData> unitDataList = new List<UnitData>((IEnumerable<UnitData>) (src as UnitData[]));
        UnitData unitData1 = unitDataList.Find((Predicate<UnitData>) (x => x.UniqueID == (long) GlobalVars.SelectedSupportUnitUniqueID));
        int index = 0;
        if (unitData1 == null)
        {
          UnitData unitData2 = this.mOwnUnits.Find((Predicate<UnitData>) (x => x.UniqueID == (long) GlobalVars.SelectedSupportUnitUniqueID));
          if (unitData2 != null)
          {
            unitDataList.Add(unitData2);
            index = unitDataList.Count - 1;
            GameUtility.SortUnits(new List<UnitData>((IEnumerable<UnitData>) unitDataList), this.mUnitSortMode, false, out this.mSortValues, false);
          }
        }
        else
          index = unitDataList.IndexOf(unitData1);
        UnitData unitData3 = unitDataList[index];
        unitDataList.RemoveAt(index);
        unitDataList.Insert(0, unitData3);
        if (this.mSortValues != null)
        {
          List<int> intList = new List<int>((IEnumerable<int>) this.mSortValues);
          int num = intList[index];
          intList.RemoveAt(index);
          intList.Insert(0, num);
          this.mSortValues = intList.ToArray();
        }
        src = (object[]) unitDataList.ToArray();
      }
      base.SetData(src, type);
      GameParameter.UpdateAll(this.gameObject);
      this.mPage = 0;
    }

    protected override void RefreshItems()
    {
      base.RefreshItems();
      if (this.mItems.Count > 0)
        this.UnitListHilit.SetParent(this.mItems[0].GetComponent<UnitIcon>().Frame.transform, false);
      this.UnitListHilit.gameObject.SetActive(this.mPage == 0);
    }
  }
}
