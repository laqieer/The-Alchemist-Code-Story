// Decompiled with JetBrains decompiler
// Type: SRPG.SharedWebWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using Gsc.App.NetworkHelper;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  public class SharedWebWindow : MonoBehaviour
  {
    [SerializeField]
    private WebView Target;
    [SerializeField]
    private GameObject Caution;
    [SerializeField]
    private bool usegAuth = true;

    private void Awake()
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.Target, (UnityEngine.Object) null))
      {
        Transform transform = ((Component) this).transform.Find("window");
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) transform, (UnityEngine.Object) null))
        {
          WebView component = ((Component) transform).GetComponent<WebView>();
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component, (UnityEngine.Object) null))
            this.Target = component;
        }
      }
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.Caution, (UnityEngine.Object) null))
        this.Caution.SetActive(false);
      else if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.Target, (UnityEngine.Object) null))
      {
        Transform transform = ((Component) this.Target).transform.Find("caution");
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) transform, (UnityEngine.Object) null))
        {
          this.Caution = ((Component) transform).gameObject;
          this.Caution.SetActive(false);
        }
      }
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.Target, (UnityEngine.Object) null))
        return;
      Transform transform1 = ((Component) this.Target).transform.Find("btn");
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) transform1, (UnityEngine.Object) null))
        return;
      Button component1 = ((Component) transform1).GetComponent<Button>();
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) component1, (UnityEngine.Object) null))
        return;
      this.SetClose((UIUtility.DialogResultEvent) (go => GlobalEvent.Invoke("WEBVIEW_DELETE", (object) null)), component1);
    }

    public void SetClose(UIUtility.DialogResultEvent onClose, Button button)
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.Target, (UnityEngine.Object) null))
        return;
      this.Target.Btn_Close = button;
      this.Target.OnClose = onClose;
    }

    [DebuggerHidden]
    private IEnumerator Start()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new SharedWebWindow.\u003CStart\u003Ec__Iterator0()
      {
        \u0024this = this
      };
    }

    public void UpdateWebView(bool reopen = false)
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.Target, (UnityEngine.Object) null))
        return;
      string text = FlowNode_Variable.Get("SHARED_WEBWINDOW_TITLE");
      if (!string.IsNullOrEmpty(text))
        this.Target.SetTitleText(text);
      string url = FlowNode_Variable.Get("SHARED_WEBWINDOW_URL");
      if (!string.IsNullOrEmpty(url))
      {
        this.Caution.SetActive(false);
        if (this.usegAuth)
        {
          Dictionary<string, string> dictionary = new Dictionary<string, string>();
          GsccBridge.SetWebViewHeaders(new Action<string, string>(dictionary.Add));
          foreach (KeyValuePair<string, string> keyValuePair in dictionary)
            this.Target.SetHeaderField(keyValuePair.Key, keyValuePair.Value);
        }
        this.Target.OpenURL(url, reopen);
        FlowNode_Variable.Set("SHARED_WEBWINDOW_URL", string.Empty);
      }
      else
        this.Caution.SetActive(true);
    }
  }
}
