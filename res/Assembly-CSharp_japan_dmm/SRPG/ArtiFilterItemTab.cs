// Decompiled with JetBrains decompiler
// Type: SRPG.ArtiFilterItemTab
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  public class ArtiFilterItemTab : MonoBehaviour
  {
    [SerializeField]
    private Text TextTab;
    [SerializeField]
    private Toggle ToggleTab;
    [SerializeField]
    private GameObject GoFiltered;
    [SerializeField]
    private GameObject GoDefault;
    private int mIndex;
    private FilterArtifactParam mParam;

    public int Index => this.mIndex;

    public FilterArtifactParam Param => this.mParam;

    public bool IsOn
    {
      get => Object.op_Implicit((Object) this.ToggleTab) && this.ToggleTab.isOn;
      set
      {
        if (!Object.op_Implicit((Object) this.ToggleTab))
          return;
        this.ToggleTab.isOn = value;
      }
    }

    public void SetToggleSilent(bool val) => GameUtility.SetToggle(this.ToggleTab, val);

    public void SetItem(int index, FilterArtifactParam param, UnityAction<bool> action = null)
    {
      if (index < 0 || param == null)
        return;
      this.mIndex = index;
      this.mParam = param;
      if (Object.op_Implicit((Object) this.TextTab))
        this.TextTab.text = param.TabName;
      if (action == null || !Object.op_Implicit((Object) this.ToggleTab))
        return;
      ((UnityEvent<bool>) this.ToggleTab.onValueChanged).AddListener(action);
    }

    public void SetIsFiltered(bool is_filtered)
    {
      GameUtility.SetGameObjectActive(this.GoFiltered, is_filtered);
      GameUtility.SetGameObjectActive(this.GoDefault, !is_filtered);
    }
  }
}
