// Decompiled with JetBrains decompiler
// Type: SRPG.ChatTemplateMessage
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  public class ChatTemplateMessage : MonoBehaviour
  {
    [SerializeField]
    private SRPG_Button[] template_message_buttons;
    [SerializeField]
    private ChatWindow chat_window;
    [SerializeField]
    private SRPG_Button next_page_button;
    [SerializeField]
    private SRPG_Button back_page_button;
    private ChatUtility.Json_ChatTemplateData prefs_data;
    private int ONE_PAGE_ITEM_MAX;
    private int LAST_PAGE;
    private int current_page;

    public void LoadTemplateMessage()
    {
      if (this.template_message_buttons == null)
        return;
      this.prefs_data = ChatUtility.LoadChatTemplateMessage();
      if (this.prefs_data == null)
        return;
      this.ONE_PAGE_ITEM_MAX = this.template_message_buttons.Length;
      this.LAST_PAGE = Mathf.Max(this.prefs_data.messages.Length / this.ONE_PAGE_ITEM_MAX + (this.prefs_data.messages.Length % this.ONE_PAGE_ITEM_MAX != 0 ? 1 : 0) - 1, 0);
      this.SetupButtons();
    }

    private void Awake()
    {
      this.LoadTemplateMessage();
      this.SetupPageButton();
    }

    private void SetupButtons()
    {
      if (this.template_message_buttons == null)
        return;
      int num1 = this.current_page * this.ONE_PAGE_ITEM_MAX;
      int num2 = num1 + (this.ONE_PAGE_ITEM_MAX - 1);
      int index1 = 0;
      for (int index2 = 0; index2 < this.template_message_buttons.Length; ++index2)
      {
        ((UnityEventBase) this.template_message_buttons[index2].onClick).RemoveAllListeners();
        ((Component) this.template_message_buttons[index2]).gameObject.SetActive(false);
      }
      for (int index3 = num1; index3 <= num2; ++index3)
      {
        // ISSUE: object of a compiler-generated type is created
        // ISSUE: variable of a compiler-generated type
        ChatTemplateMessage.\u003CSetupButtons\u003Ec__AnonStorey0 buttonsCAnonStorey0 = new ChatTemplateMessage.\u003CSetupButtons\u003Ec__AnonStorey0();
        // ISSUE: reference to a compiler-generated field
        buttonsCAnonStorey0.\u0024this = this;
        if (this.prefs_data.messages.Length <= index3)
          break;
        if (this.prefs_data.messages[index3] != null && !Object.op_Equality((Object) this.template_message_buttons[index1], (Object) null))
        {
          ((Component) this.template_message_buttons[index1]).gameObject.SetActive(true);
          Text componentInChildren = ((Component) this.template_message_buttons[index1]).GetComponentInChildren<Text>();
          if (Object.op_Inequality((Object) componentInChildren, (Object) null))
            componentInChildren.text = this.prefs_data.messages[index3];
          // ISSUE: reference to a compiler-generated field
          buttonsCAnonStorey0.msg_index = index3;
          // ISSUE: method pointer
          ((UnityEvent) this.template_message_buttons[index1].onClick).AddListener(new UnityAction((object) buttonsCAnonStorey0, __methodptr(\u003C\u003Em__0)));
          ++index1;
        }
      }
    }

    private void SetupPageButton()
    {
      // ISSUE: method pointer
      ((UnityEvent) this.next_page_button.onClick).AddListener(new UnityAction((object) this, __methodptr(\u003CSetupPageButton\u003Em__0)));
      // ISSUE: method pointer
      ((UnityEvent) this.back_page_button.onClick).AddListener(new UnityAction((object) this, __methodptr(\u003CSetupPageButton\u003Em__1)));
    }

    private void OnTapTemplateMessage(int msg_index)
    {
      if (Object.op_Equality((Object) this.chat_window, (Object) null))
        return;
      this.chat_window.SetMessageDataToFlowNode(this.prefs_data.messages[msg_index], true);
      this.chat_window.SetActiveUsefulWindowObject(false);
    }

    private void PageNext()
    {
      ++this.current_page;
      if (this.current_page > this.LAST_PAGE)
        this.current_page = 0;
      this.SetupButtons();
    }

    private void PageBack()
    {
      --this.current_page;
      if (this.current_page < 0)
        this.current_page = this.LAST_PAGE;
      this.SetupButtons();
    }
  }
}
