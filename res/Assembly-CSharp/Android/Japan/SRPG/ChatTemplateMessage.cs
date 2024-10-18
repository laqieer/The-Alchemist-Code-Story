// Decompiled with JetBrains decompiler
// Type: SRPG.ChatTemplateMessage
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

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
        this.template_message_buttons[index2].onClick.RemoveAllListeners();
        this.template_message_buttons[index2].gameObject.SetActive(false);
      }
      for (int index2 = num1; index2 <= num2; ++index2)
      {
        if (this.prefs_data.messages.Length <= index2)
          break;
        if (this.prefs_data.messages[index2] != null && !((UnityEngine.Object) this.template_message_buttons[index1] == (UnityEngine.Object) null))
        {
          this.template_message_buttons[index1].gameObject.SetActive(true);
          Text componentInChildren = this.template_message_buttons[index1].GetComponentInChildren<Text>();
          if ((UnityEngine.Object) componentInChildren != (UnityEngine.Object) null)
            componentInChildren.text = this.prefs_data.messages[index2];
          int msg_index = index2;
          this.template_message_buttons[index1].onClick.AddListener((UnityAction) (() => this.OnTapTemplateMessage(msg_index)));
          ++index1;
        }
      }
    }

    private void SetupPageButton()
    {
      this.next_page_button.onClick.AddListener((UnityAction) (() => this.PageNext()));
      this.back_page_button.onClick.AddListener((UnityAction) (() => this.PageBack()));
    }

    private void OnTapTemplateMessage(int msg_index)
    {
      if ((UnityEngine.Object) this.chat_window == (UnityEngine.Object) null)
        return;
      this.chat_window.SetMessageDataToFlowNode(this.prefs_data.messages[msg_index], true, false, (ChatWindow.FinishSendCallback) null);
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
