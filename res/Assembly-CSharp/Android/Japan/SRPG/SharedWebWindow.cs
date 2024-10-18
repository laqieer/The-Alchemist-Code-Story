// Decompiled with JetBrains decompiler
// Type: SRPG.SharedWebWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using Gsc.App.NetworkHelper;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace SRPG
{
  public class SharedWebWindow : MonoBehaviour
  {
    [SerializeField]
    private bool usegAuth = true;
    [SerializeField]
    private WebView Target;
    [SerializeField]
    private GameObject Caution;

    private void Awake()
    {
      if ((UnityEngine.Object) this.Target == (UnityEngine.Object) null)
      {
        Transform transform = this.transform.Find("window");
        if ((UnityEngine.Object) transform != (UnityEngine.Object) null)
        {
          WebView component = transform.GetComponent<WebView>();
          if ((UnityEngine.Object) component != (UnityEngine.Object) null)
            this.Target = component;
        }
      }
      if ((UnityEngine.Object) this.Caution != (UnityEngine.Object) null)
      {
        this.Caution.SetActive(false);
      }
      else
      {
        if (!((UnityEngine.Object) this.Target != (UnityEngine.Object) null))
          return;
        Transform transform = this.Target.transform.Find("caution");
        if (!((UnityEngine.Object) transform != (UnityEngine.Object) null))
          return;
        this.Caution = transform.gameObject;
        this.Caution.SetActive(false);
      }
    }

    private void Start()
    {
      this.UpdateWebView(false);
    }

    public void UpdateWebView(bool reopen = false)
    {
      if ((UnityEngine.Object) this.Target == (UnityEngine.Object) null)
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
