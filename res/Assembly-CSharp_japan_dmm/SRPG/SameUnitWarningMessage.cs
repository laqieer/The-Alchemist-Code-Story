// Decompiled with JetBrains decompiler
// Type: SRPG.SameUnitWarningMessage
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using UnityEngine;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  public class SameUnitWarningMessage : MonoBehaviour
  {
    [SerializeField]
    private Button button;

    private void Start()
    {
      if (!Object.op_Inequality((Object) this.button, (Object) null))
        return;
      this.button.AddClickListener(new ButtonExt.ButtonClickEvent(this.OnClick));
    }

    private void OnClick(GameObject go)
    {
      GameManager instance = MonoSingleton<GameManager>.Instance;
      if (Object.op_Equality((Object) instance, (Object) null) || instance.MasterParam == null)
        return;
      UnitData dataOfClass = DataSource.FindDataOfClass<UnitData>(((Component) this).gameObject, (UnitData) null);
      if (dataOfClass == null)
        return;
      SameUnitWarningMessage.SameUnitWarningMessageDialog(dataOfClass);
    }

    public static void SameUnitWarningMessageDialog(UnitData unit)
    {
      GameManager instance = MonoSingleton<GameManager>.Instance;
      if (Object.op_Equality((Object) instance, (Object) null) || instance.MasterParam == null || unit == null)
        return;
      UnitSameGroupParam unitSameGroup = instance.MasterParam.GetUnitSameGroup(unit.UnitID);
      if (unitSameGroup == null)
        return;
      string unitOtherNameText = unitSameGroup.GetGroupUnitOtherNameText(unit.UnitID);
      if (string.IsNullOrEmpty(unitOtherNameText))
        return;
      UIUtility.NegativeSystemMessage((string) null, string.Format(LocalizedText.Get("sys.PARTYEDITOR_SAMEUNIT_INPARTY"), (object) unitOtherNameText), (UIUtility.DialogResultEvent) (dialog => { }));
    }
  }
}
