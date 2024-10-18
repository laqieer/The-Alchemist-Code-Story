// Decompiled with JetBrains decompiler
// Type: SRPG.ConceptCardDetailGetUnit
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  public class ConceptCardDetailGetUnit : ConceptCardDetailBase
  {
    [SerializeField]
    private Text UnitName;
    [SerializeField]
    private ButtonEvent UnitDetailBtn;
    [SerializeField]
    private GameObject UnitObj;

    public override void Refresh()
    {
      if (this.mConceptCardData == null)
        return;
      string firstGetUnit = this.mConceptCardData.Param.first_get_unit;
      if (string.IsNullOrEmpty(firstGetUnit))
        return;
      UnitParam unitParam = this.GM.GetUnitParam(firstGetUnit);
      if (unitParam == null)
        return;
      if (Object.op_Inequality((Object) this.UnitObj, (Object) null))
      {
        DataSource.Bind<UnitData>(this.UnitObj, UnitData.CreateUnitDataForDisplay(unitParam));
        GameParameter.UpdateAll(this.UnitObj);
      }
      if (Object.op_Inequality((Object) this.UnitName, (Object) null))
        this.UnitName.text = unitParam.name;
      if (!Object.op_Inequality((Object) this.UnitDetailBtn, (Object) null))
        return;
      this.UnitDetailBtn.GetEvent("CONCEPT_CARD_DETAIL_BTN_UNIT_DETAIL")?.valueList.SetField("select_unit", unitParam.iname);
    }
  }
}
