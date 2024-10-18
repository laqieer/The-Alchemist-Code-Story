// Decompiled with JetBrains decompiler
// Type: SRPG.PartyUnitSlot2
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  public class PartyUnitSlot2 : MonoBehaviour
  {
    public PartyUnitSlot2.SelectEvent OnSelect;
    public SRPG_Button SelectButton;
    public GameObject[] HideIfEmpty = new GameObject[0];
    public RectTransform Empty;
    public RectTransform NonEmpty;
    private UnitData mUnit;

    private void Awake()
    {
      if (!Object.op_Inequality((Object) this.SelectButton, (Object) null))
        return;
      this.SelectButton.AddListener(new SRPG_Button.ButtonClickEvent(this.OnButtonClick));
    }

    public UnitData Unit => this.mUnit;

    private void OnButtonClick(SRPG_Button button)
    {
      if (!((Selectable) button).IsInteractable() || this.OnSelect == null)
        return;
      this.OnSelect(this);
    }

    public void SetUnitData(UnitData unit)
    {
      this.mUnit = unit;
      DataSource.Bind<UnitData>(((Component) this).gameObject, unit);
      bool flag = unit != null;
      for (int index = 0; index < this.HideIfEmpty.Length; ++index)
      {
        if (Object.op_Inequality((Object) this.HideIfEmpty[index], (Object) null))
          this.HideIfEmpty[index].SetActive(flag);
      }
      if (Object.op_Inequality((Object) this.Empty, (Object) null))
        ((Component) this.Empty).gameObject.SetActive(!flag);
      if (Object.op_Inequality((Object) this.NonEmpty, (Object) null))
        ((Component) this.NonEmpty).gameObject.SetActive(flag);
      GameParameter.UpdateAll(((Component) this).gameObject);
    }

    public delegate void SelectEvent(PartyUnitSlot2 slot);
  }
}
