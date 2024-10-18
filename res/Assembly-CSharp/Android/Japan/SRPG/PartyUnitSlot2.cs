﻿// Decompiled with JetBrains decompiler
// Type: SRPG.PartyUnitSlot2
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

namespace SRPG
{
  public class PartyUnitSlot2 : MonoBehaviour
  {
    public GameObject[] HideIfEmpty = new GameObject[0];
    public PartyUnitSlot2.SelectEvent OnSelect;
    public SRPG_Button SelectButton;
    public RectTransform Empty;
    public RectTransform NonEmpty;
    private UnitData mUnit;

    private void Awake()
    {
      if (!((UnityEngine.Object) this.SelectButton != (UnityEngine.Object) null))
        return;
      this.SelectButton.AddListener(new SRPG_Button.ButtonClickEvent(this.OnButtonClick));
    }

    public UnitData Unit
    {
      get
      {
        return this.mUnit;
      }
    }

    private void OnButtonClick(SRPG_Button button)
    {
      if (!button.IsInteractable() || this.OnSelect == null)
        return;
      this.OnSelect(this);
    }

    public void SetUnitData(UnitData unit)
    {
      this.mUnit = unit;
      DataSource.Bind<UnitData>(this.gameObject, unit, false);
      bool flag = unit != null;
      for (int index = 0; index < this.HideIfEmpty.Length; ++index)
      {
        if ((UnityEngine.Object) this.HideIfEmpty[index] != (UnityEngine.Object) null)
          this.HideIfEmpty[index].SetActive(flag);
      }
      if ((UnityEngine.Object) this.Empty != (UnityEngine.Object) null)
        this.Empty.gameObject.SetActive(!flag);
      if ((UnityEngine.Object) this.NonEmpty != (UnityEngine.Object) null)
        this.NonEmpty.gameObject.SetActive(flag);
      GameParameter.UpdateAll(this.gameObject);
    }

    public delegate void SelectEvent(PartyUnitSlot2 slot);
  }
}
