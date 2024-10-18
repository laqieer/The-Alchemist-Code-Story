﻿// Decompiled with JetBrains decompiler
// Type: SRPG.ChatEditTemplateMessage
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System.Collections.Generic;
using UnityEngine;

#nullable disable
namespace SRPG
{
  [FlowNode.Pin(1, "初期化", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(100, "定型文の変更", FlowNode.PinTypes.Output, 100)]
  [FlowNode.Pin(101, "NGワードが存在", FlowNode.PinTypes.Output, 101)]
  public class ChatEditTemplateMessage : MonoBehaviour, IFlowInterface
  {
    [SerializeField]
    private SRPG_Button[] submit_buttons;
    [SerializeField]
    private InputFieldCensorship[] input_fields;
    private ChatUtility.Json_ChatTemplateData prefs_data;
    private List<ChatUtility.ChatInspectionMaster> chat_inspecton_master;
    private bool is_loaded_inspection_master;

    public void Activated(int pinID)
    {
      if (pinID != 1)
        return;
      this.Init();
    }

    private void Init()
    {
      if (!this.is_loaded_inspection_master)
      {
        this.chat_inspecton_master = ChatUtility.LoadInspectionMaster(ref this.is_loaded_inspection_master);
        if (!this.is_loaded_inspection_master)
          DebugUtility.LogError("FAILED : ChatUtility.LoadInspectionMaster");
      }
      this.InitButtons();
      this.InitInputFields();
    }

    private void InitButtons()
    {
      if (this.submit_buttons == null)
        return;
      for (int index = 0; index < this.submit_buttons.Length; ++index)
      {
        ((Object) this.submit_buttons[index]).name = index.ToString();
        this.submit_buttons[index].RemoveListener(new SRPG_Button.ButtonClickEvent(this.OnClickSubmitButton));
        this.submit_buttons[index].AddListener(new SRPG_Button.ButtonClickEvent(this.OnClickSubmitButton));
      }
    }

    private void InitInputFields()
    {
      if (this.input_fields == null)
        return;
      this.prefs_data = ChatUtility.LoadChatTemplateMessage();
      if (this.prefs_data == null)
        return;
      if (this.prefs_data.messages.Length < this.input_fields.Length)
      {
        string[] strArray = new string[this.input_fields.Length];
        for (int index = 0; index < this.input_fields.Length; ++index)
          strArray[index] = this.prefs_data.messages.Length < index ? LocalizedText.Get("sys.CHAT_DEFAULT_TEMPLATE_MESSAGE_" + (object) (index + 1)) : this.prefs_data.messages[index];
        this.prefs_data.messages = strArray;
      }
      for (int index = 0; index < this.prefs_data.messages.Length && this.input_fields.Length > index; ++index)
        this.input_fields[index].SetText(this.prefs_data.messages[index]);
    }

    private void OnClickSubmitButton(SRPG_Button button)
    {
      int result = 0;
      if (!int.TryParse(((Object) button).name, out result))
        return;
      if (this.input_fields[result].text.Length <= 0 || this.input_fields[result].text == this.prefs_data.messages[result])
      {
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 100);
      }
      else
      {
        string text = this.input_fields[result].text;
        string str = ChatUtility.ReplaceNGWord(this.input_fields[result].text, this.chat_inspecton_master, "*");
        if (text != str)
        {
          this.input_fields[result].SetText(this.prefs_data.messages[result]);
          FlowNode_GameObject.ActivateOutputLinks((Component) this, 101);
        }
        else
        {
          this.input_fields[result].SetText(str);
          this.prefs_data.messages[result] = str;
          ChatUtility.SaveTemplateMessage(this.prefs_data);
          FlowNode_GameObject.ActivateOutputLinks((Component) this, 100);
        }
      }
    }
  }
}
