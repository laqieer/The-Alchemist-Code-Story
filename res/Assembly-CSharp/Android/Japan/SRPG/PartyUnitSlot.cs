// Decompiled with JetBrains decompiler
// Type: SRPG.PartyUnitSlot
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  public class PartyUnitSlot : MonoBehaviour
  {
    public string AnimatorBoolName = "active";
    public GameObject[] HideIfEmpty = new GameObject[0];
    public static PartyUnitSlot Active;
    [HelpBox("パーティ編成画面のユニットを割り当てるスロット。 選択状態あわせて StateAnimator に指定された Animator の bool 値を切り替えます。イベントにSelect()を登録してください。")]
    public Animator StateAnimator;
    public int Index;
    public bool ToggleButtonIfEmpty;
    private Button mButton;

    private void ToggleButton()
    {
      if (!this.ToggleButtonIfEmpty || (UnityEngine.Object) this.mButton == (UnityEngine.Object) null)
        return;
      this.mButton.interactable = DataSource.FindDataOfClass<UnitData>(this.gameObject, (UnitData) null) != null;
    }

    public void Select()
    {
      PartyUnitSlot.Active = this;
    }

    private void Awake()
    {
      if (this.ToggleButtonIfEmpty)
        this.mButton = this.gameObject.GetComponent<Button>();
      this.ToggleButton();
    }

    public void Update()
    {
      if ((UnityEngine.Object) this.StateAnimator != (UnityEngine.Object) null && !string.IsNullOrEmpty(this.AnimatorBoolName))
        this.StateAnimator.SetBool(this.AnimatorBoolName, (UnityEngine.Object) PartyUnitSlot.Active == (UnityEngine.Object) this);
      bool flag = DataSource.FindDataOfClass<UnitData>(this.gameObject, (UnitData) null) != null;
      for (int index = 0; index < this.HideIfEmpty.Length; ++index)
      {
        if ((UnityEngine.Object) this.HideIfEmpty[index] != (UnityEngine.Object) null)
          this.HideIfEmpty[index].SetActive(flag);
      }
      this.ToggleButton();
    }
  }
}
