// Decompiled with JetBrains decompiler
// Type: SRPG.RuneSortToggleItem
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  public class RuneSortToggleItem : MonoBehaviour
  {
    [SerializeField]
    private Toggle m_Toggle;
    [SerializeField]
    private Text m_SortName;

    public bool isOn => this.m_Toggle.isOn;

    public void SetToggleSilient(bool value)
    {
      if (Object.op_Equality((Object) this.m_Toggle, (Object) null))
        return;
      GameUtility.SetToggle(this.m_Toggle, value);
    }

    public void SetToggleValueChangeListner(UnityAction<bool> onToggleValueChanged)
    {
      if (Object.op_Equality((Object) this.m_Toggle, (Object) null))
        return;
      ((UnityEvent<bool>) this.m_Toggle.onValueChanged).RemoveListener(onToggleValueChanged);
      ((UnityEvent<bool>) this.m_Toggle.onValueChanged).AddListener(onToggleValueChanged);
    }

    public void SetName(string name)
    {
      if (Object.op_Equality((Object) this.m_SortName, (Object) null))
        return;
      this.m_SortName.text = name;
    }
  }
}
