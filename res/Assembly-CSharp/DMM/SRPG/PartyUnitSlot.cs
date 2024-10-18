// Decompiled with JetBrains decompiler
// Type: SRPG.PartyUnitSlot
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  public class PartyUnitSlot : MonoBehaviour
  {
    public static PartyUnitSlot Active;
    [HelpBox("パーティ編成画面のユニットを割り当てるスロット。 選択状態あわせて StateAnimator に指定された Animator の bool 値を切り替えます。イベントにSelect()を登録してください。")]
    public Animator StateAnimator;
    public string AnimatorBoolName = "active";
    public GameObject[] HideIfEmpty = new GameObject[0];
    public int Index;
    public bool ToggleButtonIfEmpty;
    private Button mButton;

    private void ToggleButton()
    {
      if (!this.ToggleButtonIfEmpty || Object.op_Equality((Object) this.mButton, (Object) null))
        return;
      ((Selectable) this.mButton).interactable = DataSource.FindDataOfClass<UnitData>(((Component) this).gameObject, (UnitData) null) != null;
    }

    public void Select() => PartyUnitSlot.Active = this;

    private void Awake()
    {
      if (this.ToggleButtonIfEmpty)
        this.mButton = ((Component) this).gameObject.GetComponent<Button>();
      this.ToggleButton();
    }

    public void Update()
    {
      if (Object.op_Inequality((Object) this.StateAnimator, (Object) null) && !string.IsNullOrEmpty(this.AnimatorBoolName))
        this.StateAnimator.SetBool(this.AnimatorBoolName, Object.op_Equality((Object) PartyUnitSlot.Active, (Object) this));
      bool flag = DataSource.FindDataOfClass<UnitData>(((Component) this).gameObject, (UnitData) null) != null;
      for (int index = 0; index < this.HideIfEmpty.Length; ++index)
      {
        if (Object.op_Inequality((Object) this.HideIfEmpty[index], (Object) null))
          this.HideIfEmpty[index].SetActive(flag);
      }
      this.ToggleButton();
    }
  }
}
