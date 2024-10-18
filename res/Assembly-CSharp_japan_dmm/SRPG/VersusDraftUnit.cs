// Decompiled with JetBrains decompiler
// Type: SRPG.VersusDraftUnit
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  public class VersusDraftUnit : MonoBehaviour
  {
    private static VersusDraftList mVersusDraftList;
    private static List<VersusDraftUnit> mCurrentSelected = new List<VersusDraftUnit>();
    private static int mCursorIndex = 0;
    [SerializeField]
    private UnitIcon mUnitIcon;
    [SerializeField]
    private GameObject mCursorPlayer;
    [SerializeField]
    private GameObject mCursorEnemy;
    [SerializeField]
    private GameObject mSecretIcon;
    [SerializeField]
    private GameObject mPickPlayer;
    [SerializeField]
    private GameObject mPickEnemy;
    private UnitData mUnitData;
    private bool mIsSelected;
    private bool mIsHidden;

    public static VersusDraftList VersusDraftList
    {
      get => VersusDraftUnit.mVersusDraftList;
      set => VersusDraftUnit.mVersusDraftList = value;
    }

    public static List<VersusDraftUnit> CurrentSelectCursors
    {
      get => VersusDraftUnit.mCurrentSelected;
      set => VersusDraftUnit.mCurrentSelected = value;
    }

    public UnitData UnitData => this.mUnitData;

    public bool IsSelected => this.mIsSelected;

    public bool IsHidden => this.mIsHidden;

    public void SetUp(UnitData unit, Transform parent, bool is_hidden)
    {
      this.mUnitData = unit;
      this.mIsHidden = is_hidden;
      if (!this.mIsHidden)
      {
        DataSource.Bind<UnitData>(((Component) this).gameObject, this.mUnitData);
      }
      else
      {
        this.mSecretIcon.SetActive(true);
        this.mUnitIcon.Tooltip = false;
      }
      ((Component) this).transform.SetParent(parent, false);
      ((Component) this).gameObject.SetActive(true);
    }

    public void OnClick(Button button)
    {
      if (MonoSingleton<GameManager>.Instance.AudienceMode || !VersusDraftList.VersusDraftTurnOwn || this.mIsSelected || VersusDraftUnit.mCurrentSelected.Contains(this))
        return;
      this.SelectUnit();
      if (!Object.op_Inequality((Object) VersusDraftUnit.mVersusDraftList, (Object) null))
        return;
      VersusDraftUnit.mVersusDraftList.SelectUnit();
    }

    public static void ResetSelectUnit()
    {
      for (int index = 0; index < VersusDraftUnit.mCurrentSelected.Count; ++index)
      {
        if (Object.op_Inequality((Object) VersusDraftUnit.mCurrentSelected[index], (Object) null))
        {
          VersusDraftUnit.mCurrentSelected[index].mCursorPlayer.SetActive(false);
          VersusDraftUnit.mCurrentSelected[index].mCursorEnemy.SetActive(false);
          VersusDraftUnit.mCurrentSelected[index] = (VersusDraftUnit) null;
        }
      }
      VersusDraftUnit.mCursorIndex = 0;
    }

    public void SelectUnit(bool isPlayer = true)
    {
      if (isPlayer)
      {
        if (Object.op_Inequality((Object) VersusDraftUnit.mCurrentSelected[VersusDraftUnit.mCursorIndex], (Object) null))
          VersusDraftUnit.mCurrentSelected[VersusDraftUnit.mCursorIndex].mCursorPlayer.SetActive(false);
        this.mCursorPlayer.SetActive(true);
        VersusDraftUnit.mCurrentSelected[VersusDraftUnit.mCursorIndex] = this;
        VersusDraftUnit.VersusDraftList.SetUnit(!VersusDraftUnit.mCurrentSelected[VersusDraftUnit.mCursorIndex].mIsHidden ? VersusDraftUnit.mCurrentSelected[VersusDraftUnit.mCursorIndex].UnitData : (UnitData) null, VersusDraftUnit.mCursorIndex);
      }
      else
      {
        if (Object.op_Inequality((Object) VersusDraftUnit.mCurrentSelected[VersusDraftUnit.mCursorIndex], (Object) null))
          VersusDraftUnit.mCurrentSelected[VersusDraftUnit.mCursorIndex].mCursorEnemy.SetActive(false);
        this.mCursorEnemy.SetActive(true);
        VersusDraftUnit.mCurrentSelected[VersusDraftUnit.mCursorIndex] = this;
      }
      ++VersusDraftUnit.mCursorIndex;
      if (VersusDraftUnit.VersusDraftList.SelectableUnitCount > VersusDraftUnit.mCursorIndex)
        return;
      VersusDraftUnit.mCursorIndex = 0;
    }

    public void DecideUnit(bool isPlayer = true)
    {
      if (isPlayer)
      {
        if (Object.op_Inequality((Object) this.mPickPlayer, (Object) null))
        {
          this.mPickPlayer.SetActive(true);
          VersusDraftList.VersusDraftUnitDataListPlayer.Add(this.mUnitData);
        }
      }
      else if (Object.op_Inequality((Object) this.mPickEnemy, (Object) null))
      {
        VersusDraftList.VersusDraftUnitDataListEnemy.Add(this.mUnitData);
        this.mPickEnemy.SetActive(true);
      }
      this.mIsSelected = true;
    }
  }
}
