// Decompiled with JetBrains decompiler
// Type: SRPG.NetworkErrorWindowEx
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace SRPG
{
  public class NetworkErrorWindowEx : MonoBehaviour
  {
    [SerializeField]
    private Text Message;
    [SerializeField]
    private Button ButtonOk;

    private void Awake()
    {
      if (!((UnityEngine.Object) this.ButtonOk != (UnityEngine.Object) null))
        return;
      this.ButtonOk.onClick.AddListener(new UnityAction(this.OnOk));
    }

    private void Start()
    {
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
    }
  }
}
