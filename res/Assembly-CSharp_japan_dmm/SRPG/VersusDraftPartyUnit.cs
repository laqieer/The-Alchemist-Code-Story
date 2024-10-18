// Decompiled with JetBrains decompiler
// Type: SRPG.VersusDraftPartyUnit
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;
using UnityEngine.UI;

#nullable disable
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
      get => VersusDraftPartyUnit.mVersusDraftPartyEdit;
      set => VersusDraftPartyUnit.mVersusDraftPartyEdit = value;
    }

    public UnitData UnitData => this.mUnitData;

    public bool IsSetSlot => Object.op_Inequality((Object) this.mTargetSlot, (Object) null);

    public void SetUp(UnitData unit)
    {
      this.mUnitData = unit;
      this.mSelected.SetActive(false);
      DataSource.Bind<UnitData>(((Component) this).gameObject, this.mUnitData);
      ((Component) this).gameObject.SetActive(true);
    }

    public void OnClick(Button button)
    {
      if (Object.op_Equality((Object) VersusDraftPartySlot.CurrentSelected, (Object) null))
        return;
      if (Object.op_Inequality((Object) this.mTargetSlot, (Object) null))
      {
        VersusDraftPartySlot mTargetSlot = this.mTargetSlot;
        this.mTargetSlot.PartyUnit.Reset();
        if (Object.op_Inequality((Object) VersusDraftPartySlot.CurrentSelected.PartyUnit, (Object) null))
          VersusDraftPartySlot.CurrentSelected.PartyUnit.Select(mTargetSlot);
      }
      else if (Object.op_Inequality((Object) VersusDraftPartySlot.CurrentSelected.PartyUnit, (Object) null))
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
      if (!Object.op_Inequality((Object) this.mTargetSlot, (Object) null))
        return;
      this.mTargetSlot.SetUnit((VersusDraftPartyUnit) null);
      this.mTargetSlot = (VersusDraftPartySlot) null;
    }
  }
}
