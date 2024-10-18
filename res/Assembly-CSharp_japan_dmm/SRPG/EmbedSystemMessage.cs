// Decompiled with JetBrains decompiler
// Type: SRPG.EmbedSystemMessage
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  public class EmbedSystemMessage : MonoBehaviour
  {
    public const string PrefabPath = "e/UI/EmbedSystemMessage";
    public EmbedSystemMessage.SystemMessageEvent Delegate;
    public Text Message;
    public Button ButtonOk;

    public static EmbedSystemMessage Create(
      string msg,
      EmbedSystemMessage.SystemMessageEvent callback,
      bool dontDestroyOnLoad = false)
    {
      EmbedSystemMessage embedSystemMessage = Object.Instantiate<EmbedSystemMessage>(Resources.Load<EmbedSystemMessage>("e/UI/EmbedSystemMessage"));
      embedSystemMessage.Body = msg;
      embedSystemMessage.Delegate = callback;
      if (dontDestroyOnLoad)
        Object.DontDestroyOnLoad((Object) embedSystemMessage);
      return embedSystemMessage;
    }

    private void Awake()
    {
      if (!Object.op_Inequality((Object) this.ButtonOk, (Object) null))
        return;
      // ISSUE: method pointer
      ((UnityEvent) this.ButtonOk.onClick).AddListener(new UnityAction((object) this, __methodptr(OnOk)));
    }

    public string Body
    {
      set => this.Message.text = value;
      get => this.Message.text;
    }

    private void OnOk() => this.Delegate(true);

    public delegate void SystemMessageEvent(bool yes);
  }
}
