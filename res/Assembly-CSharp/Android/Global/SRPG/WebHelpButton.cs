// Decompiled with JetBrains decompiler
// Type: SRPG.WebHelpButton
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.Events;

namespace SRPG
{
  public class WebHelpButton : SRPG_Button
  {
    public GameObject Target;
    [StringIsResourcePath(typeof (GameObject))]
    public string PrefabPath;
    private IWebHelp mInterface;

    protected override void Start()
    {
      base.Start();
      if ((UnityEngine.Object) this.Target != (UnityEngine.Object) null)
        this.mInterface = this.Target.GetComponentInChildren<IWebHelp>(true);
      this.onClick.AddListener(new UnityAction(this.ShowWebHelp));
    }

    protected override void OnEnable()
    {
      base.OnEnable();
      this.Update();
    }

    private void Update()
    {
      if (this.mInterface == null)
        return;
      string url;
      string title;
      this.interactable = this.mInterface.GetHelpURL(out url, out title);
    }

    private void ShowWebHelp()
    {
      string url;
      string title;
      if (!this.IsInteractable() || !this.mInterface.GetHelpURL(out url, out title))
        return;
      string name = this.PrefabPath;
      if (string.IsNullOrEmpty(name))
        name = GameSettings.Instance.WebHelp_PrefabPath;
      if (string.IsNullOrEmpty(name))
        return;
      GameObject original = AssetManager.Load<GameObject>(name);
      if ((UnityEngine.Object) original == (UnityEngine.Object) null)
        return;
      WebView component = UnityEngine.Object.Instantiate<GameObject>(original).GetComponent<WebView>();
      if (!((UnityEngine.Object) component != (UnityEngine.Object) null))
        return;
      if (!string.IsNullOrEmpty(title))
        component.SetTitleText(title);
      url = GameSettings.Instance.WebHelp_URLMode.ComposeURL(url);
      component.OnClose = (UIUtility.DialogResultEvent) (g => {});
      component.OpenURL(url);
    }
  }
}
