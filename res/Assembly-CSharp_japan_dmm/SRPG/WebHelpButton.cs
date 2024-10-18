// Decompiled with JetBrains decompiler
// Type: SRPG.WebHelpButton
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  public class WebHelpButton : SRPG_Button
  {
    public GameObject Target;
    [StringIsResourcePath(typeof (GameObject))]
    public string PrefabPath;
    public bool usegAuth = true;
    private IWebHelp mInterface;

    protected virtual void Start()
    {
      ((UIBehaviour) this).Start();
      if (Object.op_Inequality((Object) this.Target, (Object) null))
        this.mInterface = this.Target.GetComponentInChildren<IWebHelp>(true);
      // ISSUE: method pointer
      ((UnityEvent) this.onClick).AddListener(new UnityAction((object) this, __methodptr(ShowWebHelp)));
    }

    protected virtual void OnEnable()
    {
      ((Selectable) this).OnEnable();
      this.Update();
    }

    private void Update()
    {
      if (this.mInterface == null)
        return;
      ((Selectable) this).interactable = this.mInterface.GetHelpURL(out string _, out string _);
    }

    private void ShowWebHelp()
    {
      string url;
      if (!((Selectable) this).IsInteractable() || !this.mInterface.GetHelpURL(out url, out string _))
        return;
      url = GameSettings.Instance.WebHelp_URLMode.ComposeURL(url);
      Application.OpenURL(url);
    }
  }
}
