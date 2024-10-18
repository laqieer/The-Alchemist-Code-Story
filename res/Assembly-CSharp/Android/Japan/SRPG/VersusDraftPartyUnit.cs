// Decompiled with JetBrains decompiler
// Type: SRPG.VersusDraftPartyUnit
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  public class VersusDraftPartyUnit : MonoBehaviour
  {
    private static VersusDraftPartyEdit mVersusDraftPartyEdit;
    private UnitData mUnitData;
    private VersusDraftPartySlot mTargetSlot;
    [SerializeField]
    private GameObject mSelected;

    public static VersusDraftPartyEdit VersusDraftPartyEdit
    {
      get
      {
        return VersusDraftPartyUnit.mVersusDraftPartyEdit;
      }
      set
      {
        VersusDraftPartyUnit.mVersusDraftPartyEdit = value;
      }
    }

    public UnitData UnitData
    {
      get
      {
        return this.mUnitData;
      }
    }

    public bool IsSetSlot
    {
      get
      {
        return (UnityEngine.Object) this.mTargetSlot != (UnityEngine.Object) null;
      }
    }

    public void SetUp(UnitData unit)
    {
      this.mUnitData = unit;
      this.mSelected.SetActive(false);
      DataSource.Bind<UnitData>(this.gameObject, this.mUnitData, false);
      this.gameObject.SetActive(true);
    }

    public void OnClick(Button button)
    {
      if ((UnityEngine.Object) VersusDraftPartySlot.CurrentSelected == (UnityEngine.Object) null)
        return;
      if ((UnityEngine.Object) this.mTargetSlot != (UnityEngine.Object) null)
      {
        VersusDraftPartySlot mTargetSlot = this.mTargetSlot;
        this.mTargetSlot.PartyUnit.Reset();
        if ((UnityEngine.Object) VersusDraftPartySlot.CurrentSelected.PartyUnit != (UnityEngine.Object) null)
          VersusDraftPartySlot.CurrentSelected.PartyUnit.Select(mTargetSlot);
      }
      else if ((UnityEngine.Object) VersusDraftPartySlot.CurrentSelected.PartyUnit != (UnityEngine.Object) null)
        VersusDraftPartySlot.CurrentSelected.PartyUnit.Reset();
      this.Select(VersusDraftPartySlot.CurrentSelected);
      VersusDraftPartyUnit.mVersusDraftPartyEdit.SelectNextSlot();
    }

    public void Select(VersusDraftPartySlot slot)
    {
      this.mSelected.SetActive(true);
      this.mTargetSlot = slot;
      this.mTargetSlot.SetUnit(this);
    }

    public void Reset()
    {
      this.mSelected.SetActive(false);
      if (!((UnityEngine.Object) this.mTargetSlot != (UnityEngine.Object) null))
        return;
      this.mTargetSlot.SetUnit((VersusDraftPartyUnit) null);
      this.mTargetSlot = (VersusDraftPartySlot) null;
    }
  }
}
