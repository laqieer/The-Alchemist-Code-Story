// Decompiled with JetBrains decompiler
// Type: TabGroup
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using SRPG;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[AddComponentMenu("UI/Tab Group")]
public class TabGroup : MonoBehaviour
{
  [FlexibleArray]
  public Toggle[] Tabs = new Toggle[1];

  private void Start()
  {
    for (int index = 0; index < this.Tabs.Length; ++index)
    {
      if ((UnityEngine.Object) this.Tabs[index] != (UnityEngine.Object) null)
        this.Tabs[index].onValueChanged.AddListener(new UnityAction<bool>(this.OnValueChange));
    }
    this.OnValueChange(true);
  }

  private void OnValueChange(bool value)
  {
    if (!value)
      return;
    Transform transform = this.transform;
    for (int index = 0; index < this.Tabs.Length; ++index)
    {
      if (!this.Tabs[index].isOn && (UnityEngine.Object) transform.GetChild(index) != (UnityEngine.Object) null)
        transform.GetChild(index).gameObject.SetActive(false);
    }
    for (int index = 0; index < this.Tabs.Length; ++index)
    {
      if (this.Tabs[index].isOn && (UnityEngine.Object) transform.GetChild(index) != (UnityEngine.Object) null)
        transform.GetChild(index).gameObject.SetActive(true);
    }
  }
}
