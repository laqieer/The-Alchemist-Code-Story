// Decompiled with JetBrains decompiler
// Type: SRPG.OrdealTeamPanel
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  public class OrdealTeamPanel : MonoBehaviour
  {
    public GameObject UnitSlotContainer;
    public OrdealUnitSlot[] UnitSlots = new OrdealUnitSlot[1];
    public OrdealUnitSlot SupportSlot;
    [FormerlySerializedAs("TotalAtk")]
    public Text TotalCombatPower;
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
      if (Object.op_Inequality((Object) this.SupportSlot, (Object) null))
        this.SupportSlot.Unit.SetActive(false);
      this.mUnitCount = 0;
    }

    public void Add(UnitData unitData)
    {
      if (this.mUnitCount < this.UnitSlots.Length)
      {
        OrdealUnitSlot unitSlot = this.UnitSlots[this.mUnitCount];
        unitSlot.Unit.SetActive(true);
        DataSource.Bind<UnitData>(unitSlot.Unit.gameObject, unitData);
        GameParameter.UpdateAll(unitSlot.Unit.gameObject);
      }
      ++this.mUnitCount;
    }

    public void SetSupport(SupportData supportData)
    {
      DataSource.Bind<SupportData>(this.SupportSlot.Unit.gameObject, supportData);
      this.SupportSlot.Unit.SetActive(true);
    }
  }
}
