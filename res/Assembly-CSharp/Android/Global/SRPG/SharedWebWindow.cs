// Decompiled with JetBrains decompiler
// Type: SRPG.SharedWebWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using UnityEngine;

namespace SRPG
{
  public class SharedWebWindow : MonoBehaviour
  {
    [SerializeField]
    private WebView Target;
    [SerializeField]
    private GameObject Caution;

    private void Awake()
    {
      if ((UnityEngine.Object) this.Target == (UnityEngine.Object) null)
      {
        Transform child = this.transform.FindChild("window");
        if ((UnityEngine.Object) child != (UnityEngine.Object) null)
        {
          WebView component = child.GetComponent<WebView>();
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
        Transform child = this.Target.transform.FindChild("caution");
        if (!((UnityEngine.Object) child != (UnityEngine.Object) null))
          return;
        this.Caution = child.gameObject;
        this.Caution.SetActive(false);
      }
    }

    private void Start()
    {
      if ((UnityEngine.Object) this.Target == (UnityEngine.Object) null)
        return;
      string text = FlowNode_Variable.Get("SHARED_WEBWINDOW_TITLE");
      if (!string.IsNullOrEmpty(text))
        this.Target.SetTitleText(text);
      string url = FlowNode_Variable.Get("SHARED_WEBWINDOW_URL");
      if (!string.IsNullOrEmpty(url))
      {
        this.Target.OpenURL(url);
        FlowNode_Variable.Set("SHARED_WEBWINDOW_URL", string.Empty);
      }
      else
        this.Caution.SetActive(true);
    }
  }
}
