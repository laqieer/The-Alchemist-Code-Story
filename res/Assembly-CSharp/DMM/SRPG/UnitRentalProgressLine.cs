// Decompiled with JetBrains decompiler
// Type: SRPG.UnitRentalProgressLine
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  public class UnitRentalProgressLine : MonoBehaviour
  {
    private Scrollbar mScrollbar;

    private void SetValue(float value)
    {
      this.mScrollbar = ((Component) this).GetComponent<Scrollbar>();
      if (!Object.op_Inequality((Object) this.mScrollbar, (Object) null))
        return;
      this.mScrollbar.value = value;
    }

    public void UpdateValue()
    {
      RentalQuestInfo dataOfClass1 = DataSource.FindDataOfClass<RentalQuestInfo>(((Component) this).gameObject, (RentalQuestInfo) null);
      UnitRentalParam dataOfClass2 = DataSource.FindDataOfClass<UnitRentalParam>(((Component) this).gameObject, (UnitRentalParam) null);
      if (dataOfClass1 == null || dataOfClass2 == null)
        return;
      this.SetValue((float) (int) dataOfClass1.Point / (float) (int) dataOfClass2.PtMax);
    }
  }
}
