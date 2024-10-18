// Decompiled with JetBrains decompiler
// Type: SRPG.ArtiFilterItemFilter
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  public class ArtiFilterItemFilter : MonoBehaviour
  {
    [SerializeField]
    private Toggle ToggleFilter;
    private int mIndex;
    private FilterArtifactParam.Condition mCondition;

    public int Index => this.mIndex;

    public FilterArtifactParam.Condition Condition => this.mCondition;

    public bool IsOn
    {
      get => Object.op_Implicit((Object) this.ToggleFilter) && this.ToggleFilter.isOn;
      set
      {
        if (!Object.op_Implicit((Object) this.ToggleFilter))
          return;
        this.ToggleFilter.isOn = value;
      }
    }

    public void SetToggleSilent(bool val) => GameUtility.SetToggle(this.ToggleFilter, val);

    public void SetItem(
      int index,
      FilterArtifactParam.Condition condition,
      UnityAction<bool> action = null)
    {
      if (index < 0 || condition == null)
        return;
      this.mIndex = index;
      this.mCondition = condition;
      DataSource.Bind<FilterUtility.FilterBindData>(((Component) this).gameObject, new FilterUtility.FilterBindData(condition.Rarity, condition.Name, condition.EquipType));
      if (action == null || !Object.op_Implicit((Object) this.ToggleFilter))
        return;
      ((UnityEvent<bool>) this.ToggleFilter.onValueChanged).AddListener(action);
    }
  }
}
