// Decompiled with JetBrains decompiler
// Type: SRPG.WebView
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

#nullable disable
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
      if (!Object.op_Inequality((Object) ((Component) this).gameObject, (Object) null))
        return;
      Object.DestroyImmediate((Object) ((Component) this).gameObject);
    }

    private void OnClickButton(GameObject obj)
    {
      if (Object.op_Inequality((Object) this.Btn_Close, (Object) null) && this.OnClose != null && Object.op_Equality((Object) obj, (Object) ((Component) this.Btn_Close).gameObject))
      {
        this.OnClose(((Component) this).gameObject);
        SystemSound.Play(SystemSound.ECue.WindowClose);
      }
      this.BeginClose();
    }

    public void SetTitleText(string text)
    {
      if (!Object.op_Inequality((Object) this.Text_Title, (Object) null))
        return;
      this.Text_Title.text = text;
    }

    private void Awake()
    {
      if (!Object.op_Inequality((Object) this.Btn_Close, (Object) null))
        return;
      UIUtility.AddEventListener(((Component) this.Btn_Close).gameObject, (UnityEvent) this.Btn_Close.onClick, new UIUtility.EventListener(this.OnClickButton));
    }

    public void SetHeaderField(string key, string value)
    {
    }

    public void OpenURL(string url, bool reopen = false)
    {
    }
  }
}
