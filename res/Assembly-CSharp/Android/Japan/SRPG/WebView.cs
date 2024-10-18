// Decompiled with JetBrains decompiler
// Type: SRPG.WebView
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace SRPG
{
  public class WebView : MonoBehaviour
  {
    public Text Text_Title;
    public Button Btn_Close;
    public RawImage ClientArea;
    public UIUtility.DialogResultEvent OnClose;

    public void BeginClose()
    {
      UnityEngine.Object.DestroyImmediate((UnityEngine.Object) this.gameObject);
    }

    private void OnClickButton(GameObject obj)
    {
      if ((UnityEngine.Object) obj == (UnityEngine.Object) this.Btn_Close.gameObject && (UnityEngine.Object) this.Btn_Close != (UnityEngine.Object) null && this.OnClose != null)
        this.OnClose(this.gameObject);
      this.BeginClose();
    }

    public void SetTitleText(string text)
    {
      if (!((UnityEngine.Object) this.Text_Title != (UnityEngine.Object) null))
        return;
      this.Text_Title.text = text;
    }

    private void Awake()
    {
      if (!((UnityEngine.Object) this.Btn_Close != (UnityEngine.Object) null))
        return;
      UIUtility.AddEventListener(this.Btn_Close.gameObject, (UnityEvent) this.Btn_Close.onClick, new UIUtility.EventListener(this.OnClickButton));
    }

    public void SetHeaderField(string key, string value)
    {
    }

    public void OpenURL(string url, bool reopen = false)
    {
    }
  }
}
