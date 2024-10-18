﻿// Decompiled with JetBrains decompiler
// Type: SRPG.ChatLog
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
namespace SRPG
{
  public class ChatLog
  {
    private List<ChatLogParam> add_log_list = new List<ChatLogParam>();
    public List<ChatLogParam> messages = new List<ChatLogParam>();
    public List<ChatLogOfficialParam> official_messages = new List<ChatLogOfficialParam>();
    public bool is_dirty;
    private long top_message_id_server;
    private long last_message_id;
    private long last_message_posted_at;
    private long last_message_id_server;

    public ChatLog()
    {
      this.is_dirty = false;
      this.top_message_id_server = 0L;
      this.last_message_id = 0L;
      this.last_message_id_server = 0L;
      this.last_message_posted_at = 0L;
    }

    public long TopMessageIdServer
    {
      get => this.top_message_id_server >= 0L ? this.top_message_id_server : 0L;
    }

    public long LastMessageIdServer
    {
      get => this.last_message_id_server >= 0L ? this.last_message_id_server : 0L;
    }

    public long LastMessagePostedAt => this.last_message_posted_at;

    public void Clear()
    {
      this.add_log_list.Clear();
      this.messages.Clear();
      this.is_dirty = true;
      this.top_message_id_server = 0L;
      this.last_message_id = 0L;
      this.last_message_id_server = 0L;
      this.last_message_posted_at = 0L;
    }

    public void AddMessage(ChatLogParam _param)
    {
      this.AddMessage(new List<ChatLogParam>() { _param });
    }

    public void AddMessage(List<ChatLogParam> _message)
    {
      this.add_log_list.Clear();
      for (int i = 0; i < _message.Count; ++i)
      {
        if (this.messages.Find((Predicate<ChatLogParam>) (msg => msg.id == _message[i].id)) == null)
          this.add_log_list.Add(_message[i]);
      }
      int num1 = Mathf.Max(0, this.messages.Count + this.add_log_list.Count - (int) ChatWindow.MAX_CHAT_LOG_ITEM);
      for (int index = 0; index < num1; ++index)
        this.messages.RemoveAt(0);
      this.messages.AddRange((IEnumerable<ChatLogParam>) this.add_log_list);
      this.messages.Sort((Comparison<ChatLogParam>) ((a, b) =>
      {
        int num2 = (int) (a.posted_at - b.posted_at);
        return num2 != 0 ? num2 : (int) (a.id - b.id);
      }));
      this.RefreshId();
    }

    public void AddMessage(List<ChatLogOfficialParam> official_messages)
    {
      List<ChatLogParam> _message = new List<ChatLogParam>();
      _message.AddRange((IEnumerable<ChatLogParam>) official_messages.ToArray());
      this.AddMessage(_message);
    }

    public void Reset()
    {
      this.top_message_id_server = 0L;
      this.last_message_id = 0L;
      this.last_message_id_server = 0L;
      this.last_message_posted_at = 0L;
      this.messages.Clear();
    }

    public void RemoveByIndex(int _index)
    {
      if (this.messages.Count - 1 < _index)
        return;
      this.messages.RemoveAt(_index);
      this.RefreshId();
    }

    private void RefreshId()
    {
      for (int index = 0; index < this.messages.Count; ++index)
      {
        if (this.messages[index].messageType != ChatLogParam.eChatMessageType.SYSTEM && this.messages[index].messageType != ChatLogParam.eChatMessageType.SYSTEM_GUILD)
        {
          this.top_message_id_server = this.messages[index].id;
          break;
        }
      }
      long lastMessageId = this.last_message_id;
      for (int index = 0; index < this.messages.Count; ++index)
      {
        this.last_message_id = this.messages[index].id;
        this.last_message_posted_at = this.messages[index].posted_at;
        if (this.messages[index].messageType != ChatLogParam.eChatMessageType.SYSTEM && this.messages[index].messageType != ChatLogParam.eChatMessageType.SYSTEM_GUILD)
          this.last_message_id_server = this.messages[index].id;
      }
      if (lastMessageId == this.last_message_id)
        return;
      this.is_dirty = true;
    }

    public void Deserialize(JSON_ChatLog json)
    {
      if (json == null)
        return;
      this.messages.Clear();
      this.messages = new List<ChatLogParam>((int) ChatWindow.MAX_CHAT_LOG_ITEM);
      if (json.messages != null)
        this.messages.AddRange((IEnumerable<ChatLogParam>) json.messages);
      this.official_messages.Clear();
      this.official_messages = new List<ChatLogOfficialParam>((int) ChatWindow.MAX_CHAT_LOG_ITEM);
      if (json.official_messages == null)
        return;
      this.official_messages.AddRange((IEnumerable<ChatLogOfficialParam>) json.official_messages);
    }
  }
}
