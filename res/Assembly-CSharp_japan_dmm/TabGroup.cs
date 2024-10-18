// Decompiled with JetBrains decompiler
// Type: TabGroup
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using SRPG;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

#nullable disable
[AddComponentMenu("UI/Tab Group")]
public class TabGroup : MonoBehaviour
{
  [FlexibleArray]
  public Toggle[] Tabs = new Toggle[1];

  private void Start()
  {
    for (int index = 0; index < this.Tabs.Length; ++index)
    {
      if (Object.op_Inequality((Object) this.Tabs[index], (Object) null))
      {
        // ISSUE: method pointer
        ((UnityEvent<bool>) this.Tabs[index].onValueChanged).AddListener(new UnityAction<bool>((object) this, __methodptr(OnValueChange)));
      }
    }
    this.OnValueChange(true);
  }

  private void OnValueChange(bool value)
  {
    if (!value)
      return;
    Transform transform = ((Component) this).transform;
    for (int index = 0; index < this.Tabs.Length; ++index)
    {
      if (!this.Tabs[index].isOn && Object.op_Inequality((Object) transform.GetChild(index), (Object) null))
        ((Component) transform.GetChild(index)).gameObject.SetActive(false);
    }
    for (int index = 0; index < this.Tabs.Length; ++index)
    {
      if (this.Tabs[index].isOn && Object.op_Inequality((Object) transform.GetChild(index), (Object) null))
        ((Component) transform.GetChild(index)).gameObject.SetActive(true);
    }
  }
}
