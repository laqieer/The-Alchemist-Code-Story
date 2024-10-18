// Decompiled with JetBrains decompiler
// Type: SRPG.EmbedSystemMessageEx
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  public class EmbedSystemMessageEx : MonoBehaviour
  {
    public const string PrefabPath = "e/UI/EmbedSystemMessageEx";
    public Text Message;
    public GameObject ButtonTemplate;
    public GameObject CancelButtonTemplate;
    public GameObject ButtonBase;
    public string CloseEventName;

    public static EmbedSystemMessageEx Create(string msg)
    {
      EmbedSystemMessageEx embedSystemMessageEx = Object.Instantiate<EmbedSystemMessageEx>(Resources.Load<EmbedSystemMessageEx>("e/UI/EmbedSystemMessageEx"));
      embedSystemMessageEx.Body = msg;
      return embedSystemMessageEx;
    }

    public void AddButton(
      string btn_text,
      bool is_close,
      EmbedSystemMessageEx.SystemMessageEvent callback,
      bool enableBackKey = false)
    {
      this.CreateButton(this.ButtonTemplate, this.ButtonBase, btn_text, is_close, enableBackKey, callback);
    }

    public void AddCancelButton(
      string btn_text,
      bool is_close,
      EmbedSystemMessageEx.SystemMessageEvent callback,
      bool enableBackKey = false)
    {
      this.CreateButton(this.CancelButtonTemplate, this.ButtonBase, btn_text, is_close, enableBackKey, callback);
    }

    private void CreateButton(
      GameObject template,
      GameObject parentObject,
      string btn_text,
      bool is_close,
      bool enableBackKey,
      EmbedSystemMessageEx.SystemMessageEvent callback)
    {
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: variable of a compiler-generated type
      EmbedSystemMessageEx.\u003CCreateButton\u003Ec__AnonStorey0 buttonCAnonStorey0 = new EmbedSystemMessageEx.\u003CCreateButton\u003Ec__AnonStorey0();
      // ISSUE: reference to a compiler-generated field
      buttonCAnonStorey0.callback = callback;
      // ISSUE: reference to a compiler-generated field
      buttonCAnonStorey0.is_close = is_close;
      // ISSUE: reference to a compiler-generated field
      buttonCAnonStorey0.\u0024this = this;
      if (Object.op_Equality((Object) template, (Object) null) || Object.op_Equality((Object) parentObject, (Object) null))
        return;
      GameObject gameObject = Object.Instantiate<GameObject>(template);
      gameObject.SetActive(true);
      LText componentInChildren1 = gameObject.GetComponentInChildren<LText>();
      if (Object.op_Inequality((Object) componentInChildren1, (Object) null))
        componentInChildren1.text = btn_text;
      Button componentInChildren2 = gameObject.GetComponentInChildren<Button>();
      if (Object.op_Inequality((Object) componentInChildren2, (Object) null))
      {
        // ISSUE: method pointer
        ((UnityEvent) componentInChildren2.onClick).AddListener(new UnityAction((object) buttonCAnonStorey0, __methodptr(\u003C\u003Em__0)));
      }
      if (enableBackKey && Object.op_Equality((Object) gameObject.GetComponentInChildren<BackHandler>(), (Object) null) && Object.op_Inequality((Object) componentInChildren2, (Object) null))
        ((Component) componentInChildren2).gameObject.AddComponent<BackHandler>();
      gameObject.transform.SetParent(parentObject.transform, false);
    }

    private void TriggerCloseEvent()
    {
      if (string.IsNullOrEmpty(this.CloseEventName))
        return;
      FlowNode_TriggerLocalEvent.TriggerLocalEvent((Component) this, this.CloseEventName);
    }

    private void Awake()
    {
      if (Object.op_Inequality((Object) this.ButtonTemplate, (Object) null))
        this.ButtonTemplate.SetActive(false);
      if (!Object.op_Inequality((Object) this.CancelButtonTemplate, (Object) null))
        return;
      this.CancelButtonTemplate.SetActive(false);
    }

    public string Body
    {
      set => this.Message.text = value;
      get => this.Message.text;
    }

    public delegate void SystemMessageEvent(bool yes);
  }
}
