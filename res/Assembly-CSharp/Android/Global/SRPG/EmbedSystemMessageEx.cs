// Decompiled with JetBrains decompiler
// Type: SRPG.EmbedSystemMessageEx
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace SRPG
{
  public class EmbedSystemMessageEx : MonoBehaviour
  {
    public const string PrefabPath = "e/UI/EmbedSystemMessageEx";
    public Text Message;
    public GameObject ButtonTemplate;
    public GameObject ButtonBase;

    public static EmbedSystemMessageEx Create(string msg)
    {
      EmbedSystemMessageEx embedSystemMessageEx = UnityEngine.Object.Instantiate<EmbedSystemMessageEx>(UnityEngine.Resources.Load<EmbedSystemMessageEx>("e/UI/EmbedSystemMessageEx"));
      embedSystemMessageEx.Body = msg;
      return embedSystemMessageEx;
    }

    public void AddButton(string btn_text, bool is_close, EmbedSystemMessageEx.SystemMessageEvent callback)
    {
      if ((UnityEngine.Object) this.ButtonTemplate == (UnityEngine.Object) null || (UnityEngine.Object) this.ButtonBase == (UnityEngine.Object) null)
        return;
      GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.ButtonTemplate);
      gameObject.SetActive(true);
      LText componentInChildren1 = gameObject.GetComponentInChildren<LText>();
      if ((UnityEngine.Object) componentInChildren1 != (UnityEngine.Object) null)
        componentInChildren1.text = btn_text;
      Button componentInChildren2 = gameObject.GetComponentInChildren<Button>();
      if ((UnityEngine.Object) componentInChildren2 != (UnityEngine.Object) null)
        componentInChildren2.onClick.AddListener((UnityAction) (() => callback(true)));
      ButtonEvent componentInChildren3 = gameObject.GetComponentInChildren<ButtonEvent>();
      if ((UnityEngine.Object) componentInChildren3 != (UnityEngine.Object) null)
        componentInChildren3.enabled = is_close;
      gameObject.transform.SetParent(this.ButtonBase.transform, false);
    }

    private void Awake()
    {
      if (!((UnityEngine.Object) this.ButtonTemplate != (UnityEngine.Object) null))
        return;
      this.ButtonTemplate.SetActive(false);
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

    public delegate void SystemMessageEvent(bool yes);
  }
}
