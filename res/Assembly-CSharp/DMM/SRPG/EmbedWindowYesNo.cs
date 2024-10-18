// Decompiled with JetBrains decompiler
// Type: SRPG.EmbedWindowYesNo
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  public class EmbedWindowYesNo : MonoBehaviour
  {
    public const string PrefabPath = "e/UI/EmbedWindowYesNo";
    public EmbedWindowYesNo.YesNoWindowEvent Delegate;
    public Text Message;
    public Button ButtonOk;
    public Button ButtonCancel;
    public GameObject DLCheck;
    public Toggle DLCToggle;

    public static EmbedWindowYesNo Create(string msg, EmbedWindowYesNo.YesNoWindowEvent callback)
    {
      EmbedWindowYesNo embedWindowYesNo = Object.Instantiate<EmbedWindowYesNo>(Resources.Load<EmbedWindowYesNo>("e/UI/EmbedWindowYesNo"));
      embedWindowYesNo.Body = msg;
      embedWindowYesNo.Delegate = callback;
      return embedWindowYesNo;
    }

    private void Awake()
    {
      if (Object.op_Inequality((Object) this.ButtonOk, (Object) null))
      {
        // ISSUE: method pointer
        ((UnityEvent) this.ButtonOk.onClick).AddListener(new UnityAction((object) this, __methodptr(OnOk)));
      }
      if (Object.op_Inequality((Object) this.ButtonCancel, (Object) null))
      {
        // ISSUE: method pointer
        ((UnityEvent) this.ButtonCancel.onClick).AddListener(new UnityAction((object) this, __methodptr(OnCancel)));
      }
      if (!Object.op_Inequality((Object) this.DLCheck, (Object) null))
        return;
      this.DLCheck.SetActive(false);
    }

    public string Body
    {
      set => this.Message.text = value;
      get => this.Message.text;
    }

    private void OnOk() => this.Delegate(true);

    private void OnCancel() => this.Delegate(false);

    public void ShowDLCToggle()
    {
    }

    public delegate void YesNoWindowEvent(bool yes);
  }
}
