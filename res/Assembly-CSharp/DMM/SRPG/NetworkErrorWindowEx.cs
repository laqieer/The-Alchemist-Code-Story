// Decompiled with JetBrains decompiler
// Type: SRPG.NetworkErrorWindowEx
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

#nullable disable
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
      if (!Object.op_Inequality((Object) this.ButtonOk, (Object) null))
        return;
      // ISSUE: method pointer
      ((UnityEvent) this.ButtonOk.onClick).AddListener(new UnityAction((object) this, __methodptr(OnOk)));
    }

    private void Start()
    {
    }

    public string Body
    {
      set => this.Message.text = value;
      get => this.Message.text;
    }

    private void OnOk()
    {
    }
  }
}
