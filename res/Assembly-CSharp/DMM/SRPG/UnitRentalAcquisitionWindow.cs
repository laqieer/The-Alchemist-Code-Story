// Decompiled with JetBrains decompiler
// Type: SRPG.UnitRentalAcquisitionWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;

#nullable disable
namespace SRPG
{
  public class UnitRentalAcquisitionWindow : MonoBehaviour
  {
    private void Start()
    {
      UnitData getUnitData = UnitRentalParam.GetUnitData;
      if (getUnitData == null)
        return;
      DataSource.Bind<UnitData>(((Component) this).gameObject, getUnitData);
      GameParameter.UpdateAll(((Component) this).gameObject);
    }
  }
}
