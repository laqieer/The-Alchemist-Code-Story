// Decompiled with JetBrains decompiler
// Type: SRPG.OrdealTeamPanel
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  public class OrdealTeamPanel : MonoBehaviour
  {
    public OrdealUnitSlot[] UnitSlots = new OrdealUnitSlot[1];
    public GameObject UnitSlotContainer;
    public OrdealUnitSlot SupportSlot;
    public Text TotalAtack;
    public Text TeamName;
    public Button Button;
    private int mUnitCount;

    private void Awake()
    {
    }

    private void Reset()
    {
      if (this.UnitSlots != null)
      {
        foreach (OrdealUnitSlot unitSlot in this.UnitSlots)
          unitSlot.Unit.SetActive(false);
      }
      if ((UnityEngine.Object) this.SupportSlot != (UnityEngine.Object) null)
        this.SupportSlot.Unit.SetActive(false);
      this.mUnitCount = 0;
    }

    public void Add(UnitData unitData)
    {
      if (this.mUnitCount < this.UnitSlots.Length)
      {
        OrdealUnitSlot unitSlot = this.UnitSlots[this.mUnitCount];
        unitSlot.Unit.SetActive(true);
        DataSource.Bind<UnitData>(unitSlot.Unit.gameObject, unitData, false);
        GameParameter.UpdateAll(unitSlot.Unit.gameObject);
      }
      ++this.mUnitCount;
    }

    public void SetSupport(SupportData supportData)
    {
      DataSource.Bind<SupportData>(this.SupportSlot.Unit.gameObject, supportData, false);
      this.SupportSlot.Unit.SetActive(true);
    }
  }
}
