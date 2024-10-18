// Decompiled with JetBrains decompiler
// Type: SRPG.NetworkRetryWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using Gsc;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  public class NetworkRetryWindow : MonoBehaviour
  {
    public NetworkRetryWindow.RetryWindowEvent Delegate;
    public Text Title;
    public Text Message;
    public Button ButtonOk;
    public Button ButtonCancel;

    private void Awake()
    {
      if (Object.op_Inequality((Object) this.ButtonOk, (Object) null))
      {
        // ISSUE: method pointer
        ((UnityEvent) this.ButtonOk.onClick).AddListener(new UnityAction((object) this, __methodptr(OnOk)));
      }
      if (!Object.op_Inequality((Object) this.ButtonCancel, (Object) null))
        return;
      if (!SDK.Initialized)
      {
        ((Component) this.ButtonCancel).gameObject.SetActive(false);
      }
      else
      {
        // ISSUE: method pointer
        ((UnityEvent) this.ButtonCancel.onClick).AddListener(new UnityAction((object) this, __methodptr(OnCancel)));
      }
    }

    private void Start()
    {
      if (!Object.op_Inequality((Object) this.Title, (Object) null))
        return;
      this.Title.text = LocalizedText.Get("embed.CONN_RETRY");
    }

    public string Body
    {
      set => this.Message.text = value;
      get => this.Message.text;
    }

    private void OnOk() => this.Delegate(true);

    private void OnCancel() => this.Delegate(false);

    public delegate void RetryWindowEvent(bool retry);
  }
}
