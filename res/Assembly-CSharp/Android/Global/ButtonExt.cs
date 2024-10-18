// Decompiled with JetBrains decompiler
// Type: ButtonExt
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof (Button))]
public class ButtonExt : MonoBehaviour
{
  private ButtonExt.ButtonClickEvent mOnClick = (ButtonExt.ButtonClickEvent) (go => {});

  private void Start()
  {
    this.GetComponent<Button>().onClick.AddListener(new UnityAction(this.OnClick));
  }

  private void OnClick()
  {
    this.mOnClick(this.gameObject);
  }

  public void AddListener(ButtonExt.ButtonClickEvent listener)
  {
    this.mOnClick += listener;
  }

  public void RemoveListener(ButtonExt.ButtonClickEvent listener)
  {
    this.mOnClick -= listener;
  }

  public delegate void ButtonClickEvent(GameObject go);
}
