// Decompiled with JetBrains decompiler
// Type: ButtonExt
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

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
