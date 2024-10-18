// Decompiled with JetBrains decompiler
// Type: ButtonExt
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

#nullable disable
[RequireComponent(typeof (Button))]
public class ButtonExt : MonoBehaviour
{
  private ButtonExt.ButtonClickEvent mOnClick = (ButtonExt.ButtonClickEvent) (go => { });

  private void Start()
  {
    // ISSUE: method pointer
    ((UnityEvent) ((Component) this).GetComponent<Button>().onClick).AddListener(new UnityAction((object) this, __methodptr(OnClick)));
  }

  private void OnClick() => this.mOnClick(((Component) this).gameObject);

  public void AddListener(ButtonExt.ButtonClickEvent listener) => this.mOnClick += listener;

  public void RemoveListener(ButtonExt.ButtonClickEvent listener) => this.mOnClick -= listener;

  public delegate void ButtonClickEvent(GameObject go);
}
