// Decompiled with JetBrains decompiler
// Type: SRPG.WebHelpButton
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.Events;

namespace SRPG
{
  public class WebHelpButton : SRPG_Button
  {
    public bool usegAuth = true;
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
      url = GameSettings.Instance.WebHelp_URLMode.ComposeURL(url);
      Application.OpenURL(url);
    }
  }
}
