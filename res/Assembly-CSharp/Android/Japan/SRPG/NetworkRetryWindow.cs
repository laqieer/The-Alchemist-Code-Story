// Decompiled with JetBrains decompiler
// Type: SRPG.NetworkRetryWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

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
      if ((UnityEngine.Object) this.ButtonOk != (UnityEngine.Object) null)
        this.ButtonOk.onClick.AddListener(new UnityAction(this.OnOk));
      if (!((UnityEngine.Object) this.ButtonCancel != (UnityEngine.Object) null))
        return;
      this.ButtonCancel.onClick.AddListener(new UnityAction(this.OnCancel));
    }

    private void Start()
    {
      if (!((UnityEngine.Object) this.Title != (UnityEngine.Object) null))
        return;
      this.Title.text = LocalizedText.Get("embed.CONN_RETRY");
    }

    public string Body
    {
      set
      {
        this.Message.text = value;
      }
      get
      {
        return this.Message.text;
      }
    }

    private void OnOk()
    {
      this.Delegate(true);
    }

    private void OnCancel()
    {
      this.Delegate(false);
    }

    public delegate void RetryWindowEvent(bool retry);
  }
}
