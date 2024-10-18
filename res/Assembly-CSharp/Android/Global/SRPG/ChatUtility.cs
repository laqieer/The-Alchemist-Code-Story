﻿// Decompiled with JetBrains decompiler
// Type: SRPG.ChatUtility
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using GR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using UnityEngine;

namespace SRPG
{
  public class ChatUtility
  {
    private static readonly string CHAT_INSPECTION_MASTER_PATH = "Data/ChatWord";
    private static readonly string CHAT_CHANNEL_MASTER_PATH = "Data/Channel";
    private static readonly Regex mTelephoneNumberFilter = new Regex("\\d{1,4}(-|ー|‐|－)\\d{1,4}(-|ー|‐|－)\\d{4}|\\d{10,11}");
    private static readonly Regex mEmailAddressFilter = new Regex("(.)+@(.)+(\\.|どっと|ドット|dot)(.*)");
    private static readonly List<Regex> mRegexFilters = new List<Regex>() { ChatUtility.mTelephoneNumberFilter, ChatUtility.mEmailAddressFilter };
    private static List<ChatUtility.ChatInspectionMaster> mCachedChatInspectionMaster;

    public static bool SetupChatChannelMaster()
    {
      if ((UnityEngine.Object) MonoSingleton<GameManager>.Instance == (UnityEngine.Object) null)
      {
        DebugUtility.LogError("ChatWindow Error:gm is NotInstance!");
        return false;
      }
      if (MonoSingleton<GameManager>.Instance.GetChatChannelMaster() != null && MonoSingleton<GameManager>.Instance.GetChatChannelMaster().Length > 0)
        return true;
      string src = AssetManager.LoadTextData(ChatUtility.CHAT_CHANNEL_MASTER_PATH);
      if (string.IsNullOrEmpty(src))
      {
        DebugUtility.LogError("ChatWindow Error:[" + ChatUtility.CHAT_CHANNEL_MASTER_PATH + "] is Not Found or Not Data!");
        return false;
      }
      bool flag = false;
      try
      {
        Json_ChatChannelMasterParam[] jsonArray = JSONParser.parseJSONArray<Json_ChatChannelMasterParam>(src);
        if (jsonArray != null)
        {
          if (MonoSingleton<GameManager>.Instance.Deserialize(GameUtility.Config_Language, new JSON_ChatChannelMaster() { channels = jsonArray }))
            flag = true;
        }
      }
      catch (Exception ex)
      {
        DebugUtility.LogError(ex.ToString());
        flag = false;
      }
      return flag;
    }

    public static List<ChatUtility.ChatInspectionMaster> LoadInspectionMaster(ref bool is_success)
    {
      if (ChatUtility.mCachedChatInspectionMaster != null)
        return ChatUtility.mCachedChatInspectionMaster;
      List<ChatUtility.ChatInspectionMaster> inspectionMasterList = new List<ChatUtility.ChatInspectionMaster>();
      is_success = false;
      string src = AssetManager.LoadTextData(ChatUtility.CHAT_INSPECTION_MASTER_PATH);
      if (string.IsNullOrEmpty(src))
      {
        DebugUtility.LogError("ChatWindow Error:[" + ChatUtility.CHAT_INSPECTION_MASTER_PATH + "] is Not Found or Not Data!");
        return inspectionMasterList;
      }
      try
      {
        ChatUtility.JSON_ChatInspectionMaster[] jsonArray = JSONParser.parseJSONArray<ChatUtility.JSON_ChatInspectionMaster>(src);
        if (jsonArray == null)
          throw new InvalidJSONException();
        foreach (ChatUtility.JSON_ChatInspectionMaster json in jsonArray)
        {
          ChatUtility.ChatInspectionMaster inspectionMaster = new ChatUtility.ChatInspectionMaster();
          if (inspectionMaster.Deserialize(json))
            inspectionMasterList.Add(inspectionMaster);
        }
      }
      catch (Exception ex)
      {
        DebugUtility.LogWarning("ChatWindow/SetupInspectionMaster parse error! e=" + ex.ToString());
        return inspectionMasterList;
      }
      is_success = true;
      ChatUtility.mCachedChatInspectionMaster = inspectionMasterList;
      return inspectionMasterList;
    }

    public static string ReplaceIllegalWordsWithSubstitute(string message, List<ChatUtility.ChatInspectionMaster> inInspectionData, char replacementChar = '*')
    {
      StringBuilder stringBuilder = new StringBuilder(message);
      List<string> regex = ChatUtility.FilterAccordingToRegex(message, ChatUtility.mRegexFilters);
      if (regex != null && regex.Count > 0)
      {
        int index = 0;
        for (int count = regex.Count; index < count; ++index)
        {
          string oldValue = regex[index];
          string newValue = new string(replacementChar, oldValue.Length);
          stringBuilder.Replace(oldValue, newValue);
        }
      }
      if (inInspectionData != null && inInspectionData.Count > 0)
      {
        int index = 0;
        for (int count = inInspectionData.Count; index < count; ++index)
        {
          string word = inInspectionData[index].word;
          if (!string.IsNullOrEmpty(word) && message.Contains(word))
          {
            string newValue = new string(replacementChar, word.Length);
            stringBuilder.Replace(word, newValue);
          }
        }
      }
      message = stringBuilder.ToString();
      return message;
    }

    private static List<string> FilterAccordingToRegex(string text, List<Regex> regexFilters)
    {
      List<string> stringList = new List<string>();
      using (List<Regex>.Enumerator enumerator = regexFilters.GetEnumerator())
      {
        while (enumerator.MoveNext())
        {
          foreach (Match match in enumerator.Current.Matches(text))
            stringList.Add(match.Value);
        }
      }
      return stringList;
    }

    public static ChatUtility.Json_ChatTemplateData LoadChatTemplateMessage()
    {
      if (!PlayerPrefsUtility.HasKey(PlayerPrefsUtility.CHAT_TEMPLATE_MESSAGE))
        return ChatUtility.CreateNewTemplateMessagePrefsData();
      ChatUtility.Json_ChatTemplateData new_prefs_data = (ChatUtility.Json_ChatTemplateData) null;
      try
      {
        if (PlayerPrefsUtility.HasKey(PlayerPrefsUtility.CHAT_TEMPLATE_MESSAGE))
          new_prefs_data = JsonUtility.FromJson<ChatUtility.Json_ChatTemplateData>(PlayerPrefsUtility.GetString(PlayerPrefsUtility.CHAT_TEMPLATE_MESSAGE, string.Empty));
      }
      catch (Exception ex)
      {
        DebugUtility.LogException(ex);
      }
      if (new_prefs_data == null)
        new_prefs_data = ChatUtility.CreateNewTemplateMessagePrefsData();
      List<string> templateMessageList = ChatUtility.GetDefaultTemplateMessageList();
      if (new_prefs_data.messages.Length < templateMessageList.Count)
      {
        List<string> stringList = new List<string>((IEnumerable<string>) new_prefs_data.messages);
        for (int length = new_prefs_data.messages.Length; length < templateMessageList.Count; ++length)
          stringList.Add(templateMessageList[length]);
        new_prefs_data.messages = stringList.ToArray();
        ChatUtility.SaveTemplateMessage(new_prefs_data);
      }
      return new_prefs_data;
    }

    public static void SaveTemplateMessage(ChatUtility.Json_ChatTemplateData new_prefs_data)
    {
      try
      {
        PlayerPrefsUtility.SetString(PlayerPrefsUtility.CHAT_TEMPLATE_MESSAGE, JsonUtility.ToJson((object) new_prefs_data), false);
        if (!((UnityEngine.Object) MonoSingleton<ChatWindow>.Instance != (UnityEngine.Object) null))
          return;
        MonoSingleton<ChatWindow>.Instance.LoadTemplateMessage();
      }
      catch (Exception ex)
      {
        DebugUtility.LogException(ex);
      }
    }

    private static ChatUtility.Json_ChatTemplateData CreateNewTemplateMessagePrefsData()
    {
      ChatUtility.Json_ChatTemplateData new_prefs_data = new ChatUtility.Json_ChatTemplateData();
      List<string> templateMessageList = ChatUtility.GetDefaultTemplateMessageList();
      new_prefs_data.messages = templateMessageList.ToArray();
      ChatUtility.SaveTemplateMessage(new_prefs_data);
      return new_prefs_data;
    }

    private static List<string> GetDefaultTemplateMessageList()
    {
      List<string> stringList = new List<string>();
      string empty = string.Empty;
      bool success = false;
      int num = 0;
      while (true)
      {
        string str = LocalizedText.Get("sys.CHAT_DEFAULT_TEMPLATE_MESSAGE_" + (object) (num + 1), ref success);
        if (success)
        {
          stringList.Add(str);
          ++num;
        }
        else
          break;
      }
      return stringList;
    }

    public static bool IsMultiQuestNow()
    {
      return (UnityEngine.Object) SceneBattle.Instance != (UnityEngine.Object) null && SceneBattle.Instance.Battle != null && (SceneBattle.Instance.Battle.IsMultiPlay && !SceneBattle.Instance.Battle.IsMultiTower) && !SceneBattle.Instance.Battle.IsMultiVersus;
    }

    public class Json_ChatInspectionMaster
    {
      public ChatUtility.JSON_ChatInspectionMaster[] chatinspections;
    }

    public class JSON_ChatInspectionMaster
    {
      public int pk;
      public ChatUtility.JSON_ChatInspectionMaster.Fields fields;

      public class Fields
      {
        public int id;
        public string ngword;
        public int reflection;
      }
    }

    public class ChatInspectionMaster
    {
      public bool reflection = true;
      public int id;
      public string word;

      public bool Deserialize(ChatUtility.JSON_ChatInspectionMaster json)
      {
        if (json == null || json.fields == null)
          return false;
        this.id = json.fields.id;
        this.word = json.fields.ngword;
        this.reflection = json.fields.reflection == 1;
        return true;
      }
    }

    public class Json_ChatTemplateData
    {
      public string[] messages;
    }

    public class RoomMemberManager
    {
      private ChatUtility.RoomMember tmp_entry_member;
      private MyPhoton.MyPlayer tmp_leave_member;
      private List<ChatUtility.RoomMember> room_members;
      private List<ChatUtility.RoomMember> entry_members;
      private List<ChatUtility.RoomMember> leave_members;

      public RoomMemberManager()
      {
        this.room_members = new List<ChatUtility.RoomMember>();
        this.entry_members = new List<ChatUtility.RoomMember>();
        this.leave_members = new List<ChatUtility.RoomMember>();
      }

      public List<ChatUtility.RoomMember> RoomMembers
      {
        get
        {
          return this.room_members;
        }
      }

      public List<ChatUtility.RoomMember> EntryMembers
      {
        get
        {
          return this.entry_members;
        }
      }

      public List<ChatUtility.RoomMember> LeaveMembers
      {
        get
        {
          return this.leave_members;
        }
      }

      public void Refresh(List<MyPhoton.MyPlayer> _new_players)
      {
        for (int i = 0; i < _new_players.Count; ++i)
        {
          if (_new_players[i].photonPlayerID > -1)
          {
            this.tmp_entry_member = this.room_members.Find((Predicate<ChatUtility.RoomMember>) (a => a.PlayerId == _new_players[i].playerID));
            if (this.tmp_entry_member == null)
            {
              JSON_MyPhotonPlayerParam photonPlayerParam = JSON_MyPhotonPlayerParam.Parse(_new_players[i].json);
              ChatUtility.RoomMember roomMember = new ChatUtility.RoomMember();
              roomMember.SetParam(_new_players[i].photonPlayerID, _new_players[i].playerID, photonPlayerParam.UID, photonPlayerParam.playerName);
              if (!this.entry_members.Contains(roomMember))
                this.entry_members.Add(roomMember);
            }
          }
        }
        for (int i = 0; i < this.room_members.Count; ++i)
        {
          if (this.room_members[i].PhotonPlayerId > -1)
          {
            this.tmp_leave_member = _new_players.Find((Predicate<MyPhoton.MyPlayer>) (a => a.playerID == this.room_members[i].PlayerId));
            if (this.tmp_leave_member == null && !this.leave_members.Contains(this.room_members[i]))
              this.leave_members.Add(this.room_members[i]);
          }
        }
        for (int index = 0; index < this.entry_members.Count; ++index)
        {
          if (!this.room_members.Contains(this.entry_members[index]))
            this.room_members.Add(this.entry_members[index]);
        }
        for (int index = 0; index < this.leave_members.Count; ++index)
        {
          if (this.room_members.Contains(this.leave_members[index]))
            this.room_members.Remove(this.leave_members[index]);
        }
      }

      public void Clear()
      {
        this.room_members.Clear();
      }
    }

    public class RoomMember
    {
      private int photon_player_id;
      private int player_id;
      private string uid;
      private string name;

      public int PhotonPlayerId
      {
        get
        {
          return this.photon_player_id;
        }
      }

      public int PlayerId
      {
        get
        {
          return this.player_id;
        }
      }

      public string UID
      {
        get
        {
          return this.uid;
        }
      }

      public string Name
      {
        get
        {
          return this.name;
        }
      }

      public void SetParam(int _photon_player_id, int _player_id, string _uid, string _name)
      {
        this.photon_player_id = _photon_player_id;
        this.player_id = _player_id;
        this.uid = _uid;
        this.name = _name;
      }
    }

    public class RoomInfo
    {
      private ChatWindow chat_window;
      private bool is_active;
      private QuestParam quest_param;

      public bool IsActive
      {
        get
        {
          return this.is_active;
        }
      }

      public QuestParam QuestParam
      {
        get
        {
          return this.quest_param;
        }
      }

      public void Init(ChatWindow _chat_window)
      {
        this.chat_window = _chat_window;
      }

      private void SetParam(string _iname)
      {
        this.quest_param = MonoSingleton<GameManager>.Instance.FindQuest(_iname);
      }

      public void Run()
      {
        this.Refresh();
      }

      private void Refresh()
      {
        if (!this.is_active && PunMonoSingleton<MyPhoton>.Instance.IsConnectedInRoom())
        {
          this.SetParam(JSON_MyPhotonRoomParam.Parse(PunMonoSingleton<MyPhoton>.Instance.GetCurrentRoom().json).iname);
          this.is_active = true;
          this.chat_window.ChangeChatTypeTab(ChatWindow.eChatType.Room);
        }
        else if (ChatUtility.IsMultiQuestNow())
          this.is_active = true;
        else if (this.is_active && !PunMonoSingleton<MyPhoton>.Instance.IsConnectedInRoom())
        {
          this.chat_window.ExitRoomSelf();
          this.is_active = false;
        }
        else
          this.is_active = PunMonoSingleton<MyPhoton>.Instance.IsConnectedInRoom();
      }

      public void ExitRoom()
      {
        this.is_active = false;
      }
    }
  }
}
