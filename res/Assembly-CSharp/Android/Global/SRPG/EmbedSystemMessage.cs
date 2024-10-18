// Decompiled with JetBrains decompiler
// Type: SRPG.EmbedSystemMessage
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace SRPG
{
  public class EmbedSystemMessage : MonoBehaviour
  {
    public const string PrefabPath = "e/UI/EmbedSystemMessage";
    public EmbedSystemMessage.SystemMessageEvent Delegate;
    public Text Message;
    public Button ButtonOk;

    public static EmbedSystemMessage Create(string msg, EmbedSystemMessage.SystemMessageEvent callback)
    {
      EmbedSystemMessage embedSystemMessage = UnityEngine.Object.Instantiate<EmbedSystemMessage>(UnityEngine.Resources.Load<EmbedSystemMessage>("e/UI/EmbedSystemMessage"));
      embedSystemMessage.Body = msg;
      embedSystemMessage.Delegate = callback;
      return embedSystemMessage;
    }

    private void Awake()
    {
      if (!((UnityEngine.Object) this.ButtonOk != (UnityEngine.Object) null))
        return;
      this.ButtonOk.onClick.AddListener(new UnityAction(this.OnOk));
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

    public delegate void SystemMessageEvent(bool yes);
  }
}
