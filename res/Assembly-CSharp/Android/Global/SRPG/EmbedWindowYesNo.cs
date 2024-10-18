// Decompiled with JetBrains decompiler
// Type: SRPG.EmbedWindowYesNo
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace SRPG
{
  public class EmbedWindowYesNo : MonoBehaviour
  {
    public const string PrefabPath = "e/UI/EmbedWindowYesNo";
    public EmbedWindowYesNo.YesNoWindowEvent Delegate;
    public Text Message;
    public Button ButtonOk;
    public Button ButtonCancel;

    public static EmbedWindowYesNo Create(string msg, EmbedWindowYesNo.YesNoWindowEvent callback)
    {
      EmbedWindowYesNo embedWindowYesNo = UnityEngine.Object.Instantiate<EmbedWindowYesNo>(UnityEngine.Resources.Load<EmbedWindowYesNo>("e/UI/EmbedWindowYesNo"));
      embedWindowYesNo.Body = msg;
      embedWindowYesNo.Delegate = callback;
      return embedWindowYesNo;
    }

    private void Awake()
    {
      if ((UnityEngine.Object) this.ButtonOk != (UnityEngine.Object) null)
        this.ButtonOk.onClick.AddListener(new UnityAction(this.OnOk));
      if (!((UnityEngine.Object) this.ButtonCancel != (UnityEngine.Object) null))
        return;
      this.ButtonCancel.onClick.AddListener(new UnityAction(this.OnCancel));
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

    public delegate void YesNoWindowEvent(bool yes);
  }
}
