// Decompiled with JetBrains decompiler
// Type: SRPG.RuneSlotSelector
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
namespace SRPG
{
  public class RuneSlotSelector : MonoBehaviour
  {
    [SerializeField]
    private GameObject[] mRuneSlotParent;
    [SerializeField]
    private GameObject mRuneSlotTemplate;
    private RuneManager mRuneManager;
    private UnitData mCurrentUnit;
    private List<RuneSlot> mRuneSlotList = new List<RuneSlot>();

    public void Awake()
    {
      if (this.mRuneSlotParent == null)
        DebugUtility.LogError("RuneSlotParent is unable to attach.");
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.mRuneSlotTemplate, (UnityEngine.Object) null))
        DebugUtility.LogError("RuneSlotTemplate is unable to attach.");
      this.mRuneSlotTemplate.SetActive(false);
    }

    public void Init()
    {
    }

    public void Initialize(RuneManager manager, UnitData unit)
    {
      this.mRuneManager = manager;
      this.mCurrentUnit = unit;
      this.Refresh(false);
    }

    public void SetUnit(UnitData unit) => this.mCurrentUnit = unit;

    public void SelectedSlot(RuneSlotIndex slot)
    {
      for (int index = 0; index < this.mRuneSlotList.Count; ++index)
        this.mRuneSlotList[index].Selected((int) (byte) slot == index);
    }

    public void Refresh(bool is_play_equip_effect = true)
    {
      if (this.mCurrentUnit == null)
        return;
      List<RuneSlot> runeSlotList = new List<RuneSlot>((IEnumerable<RuneSlot>) this.mRuneSlotList.ToArray());
      this.mRuneSlotList.ForEach((Action<RuneSlot>) (rune => UnityEngine.Object.Destroy((UnityEngine.Object) ((Component) rune).gameObject)));
      this.mRuneSlotList.Clear();
      for (int index = 0; index < this.mRuneSlotParent.Length; ++index)
      {
        GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.mRuneSlotTemplate);
        gameObject.SetActive(true);
        gameObject.transform.SetParent(this.mRuneSlotParent[index].transform, false);
        RuneSlot component = gameObject.GetComponent<RuneSlot>();
        if (!UnityEngine.Object.op_Equality((UnityEngine.Object) component, (UnityEngine.Object) null))
        {
          BindRuneData bindRuneData = (BindRuneData) null;
          if (UnityEngine.Object.op_Implicit((UnityEngine.Object) this.mRuneManager))
            bindRuneData = this.mRuneManager.FindRune(this.mCurrentUnit.EquipRune(index));
          component.Initialize(bindRuneData, (RuneSlotIndex) (byte) index, is_play_equip_effect && this.IsChangeEquipState(bindRuneData, runeSlotList[index].RuneData));
          this.mRuneSlotList.Add(component);
        }
      }
    }

    private bool IsChangeEquipState(BindRuneData current, BindRuneData prev)
    {
      bool flag = false;
      if (current != null)
        flag = prev == null || current.iid != prev.iid;
      return flag;
    }
  }
}
