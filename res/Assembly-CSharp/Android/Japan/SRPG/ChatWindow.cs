// Decompiled with JetBrains decompiler
// Type: SRPG.ChatWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace SRPG
{
  [FlowNode.Pin(10, "Chat Enter", FlowNode.PinTypes.Input, 10)]
  [FlowNode.Pin(11, "Chat Leave", FlowNode.PinTypes.Input, 11)]
  [FlowNode.Pin(20, "チャットウィンドウを開く", FlowNode.PinTypes.Input, 20)]
  [FlowNode.Pin(23, "チャットウィンドウを閉じる", FlowNode.PinTypes.Input, 23)]
  [FlowNode.Pin(21, "ウィンドウオープン終了", FlowNode.PinTypes.Output, 21)]
  [FlowNode.Pin(22, "ウィンドウクローズ終了", FlowNode.PinTypes.Output, 22)]
  [FlowNode.Pin(30, "ユーザーのアイコンをタップ", FlowNode.PinTypes.Output, 30)]
  [FlowNode.Pin(40, "ワールドチャットログ取得Request", FlowNode.PinTypes.Output, 40)]
  [FlowNode.Pin(42, "ワールドチャットログリセット", FlowNode.PinTypes.Input, 42)]
  [FlowNode.Pin(43, "ワールドチャットログ取得", FlowNode.PinTypes.Input, 43)]
  [FlowNode.Pin(45, "前回の発言から10秒以内にメッセージ送信時", FlowNode.PinTypes.Input, 45)]
  [FlowNode.Pin(46, "ルームチャットログ取得Request", FlowNode.PinTypes.Output, 46)]
  [FlowNode.Pin(47, "ギルドチャットログ取得Request", FlowNode.PinTypes.Output, 47)]
  [FlowNode.Pin(50, "チャットログリスト更新", FlowNode.PinTypes.Input, 50)]
  [FlowNode.Pin(51, "ウィンドウスライド", FlowNode.PinTypes.Input, 51)]
  [FlowNode.Pin(52, "ウィンドウスライドリセット", FlowNode.PinTypes.Input, 52)]
  [FlowNode.Pin(60, "ウィンドウ開いた", FlowNode.PinTypes.Input, 60)]
  [FlowNode.Pin(61, "ウィンドウ閉じた", FlowNode.PinTypes.Input, 61)]
  [FlowNode.Pin(70, "チャットログ更新失敗", FlowNode.PinTypes.Input, 70)]
  [FlowNode.Pin(100, "スタンプ送信セット", FlowNode.PinTypes.Input, 100)]
  [FlowNode.Pin(101, "スタンプ送信", FlowNode.PinTypes.Output, 101)]
  [FlowNode.Pin(110, "申請先のギルド情報を取得", FlowNode.PinTypes.Output, 110)]
  [FlowNode.Pin(111, "ギルド勧誘から加入申請", FlowNode.PinTypes.Output, 111)]
  [FlowNode.Pin(112, "ギルド加入申請最終確認", FlowNode.PinTypes.Input, 112)]
  public class ChatWindow : MonoSingleton<ChatWindow>, IFlowInterface
  {
    private static readonly float SPAN_UPDATE_WORLD_MESSAGE_UIOPEN = 10f;
    private static readonly float SPAN_UPDATE_WORLD_MESSAGE_UICLOSE = 20f;
    private static readonly float SPAN_UPDATE_ROOM_MESSAGE_UIOPEN = 7f;
    private static readonly float SPAN_UPDATE_ROOM_MESSAGE_UICLOSE = 20f;
    private static readonly float SPAN_UPDATE_GUILD_MESSAGE_UIOPEN = 7f;
    private static readonly float SPAN_UPDATE_GUILD_MESSAGE_UICLOSE = 20f;
    private static readonly float SPAN_UPDATE_ROOM_MEMBER = 1f;
    public static readonly byte MAX_CHAT_LOG_ITEM = 30;
    private string MaintenanceMsg = string.Empty;
    public int CharacterLimit = 140;
    private int mLastChannelID = -1;
    private bool mIsEnableOpenCloseButton = true;
    private List<ChatUtility.ChatInspectionMaster> mChatInspectionMaster = new List<ChatUtility.ChatInspectionMaster>();
    private const int PINID_IN_CHAT_ENTER = 10;
    private const int PINID_IN_CHAT_LEAVE = 11;
    private const int PINID_IN_CAHTWINDOW_OPEN = 20;
    private const int PINID_OU_OPEN_OUTPUT = 21;
    private const int PINID_OU_CLOSE_OUTPUT = 22;
    private const int PINID_IN_CAHTWINDOW_CLOSE = 23;
    private const int PINID_OU_UNITICON_TAP = 30;
    private const int PINID_OU_REQUEST_CHATLOG_WORLD = 40;
    private const int PINID_IN_UPDATE_CHATLOG_RESET = 42;
    private const int PINID_IN_UPDATE_CHATLOG = 43;
    private const int PINID_IN_SEND_CHAT_INTERVAL = 45;
    private const int PINID_OU_REQUEST_CHATLOG_ROOM = 46;
    private const int PINID_OU_REQUEST_CHATLOG_GUILD = 47;
    private const int PINID_IN_REFRESH_CHATLOGLIST = 50;
    private const int PINID_IN_SLIDE_WINDOW = 51;
    private const int PINID_IN_SLIDERESET_WINDOW = 52;
    private const int PINID_IN_WINDOW_OPENED = 60;
    private const int PINID_IN_WINDOW_CLOSED = 61;
    private const int PINID_IN_UPDATE_CHATLOG_FAILURE = 70;
    private const int PINID_IN_SEND_STAMP = 100;
    private const int PINID_OU_REQUEST_SEND_STAMP = 101;
    private const int PINID_OU_GET_GUILD_INFO = 110;
    private const int PINID_OU_SEND_GUILD_ENTRY_REQUEST = 111;
    private const int PINID_IN_OPEN_GUILD_ENTRY_REQUEST_CONFIRM = 112;
    private float elapsed_time_for_photon_room_member;
    private StateMachine<ChatWindow> mState;
    private bool mOpened;
    private bool Maintenance;
    [SerializeField]
    private GameObject MessageRoot;
    [SerializeField]
    private GameObject MessageTemplate;
    [SerializeField]
    private Scrollbar ChatScrollBar;
    [SerializeField]
    private InputFieldCensorship InputFieldMessage;
    [SerializeField]
    private SRPG_Button SendMessageButton;
    [SerializeField]
    private GameObject ChannelPanel;
    private Dictionary<ChatWindow.eChatType, SRPG_ToggleButton> mTabButtons;
    [SerializeField]
    private SRPG_ToggleButton Tab_World;
    [SerializeField]
    private SRPG_ToggleButton Tab_Room;
    [SerializeField]
    private SRPG_ToggleButton Tab_Guild;
    [SerializeField]
    private GameObject MaintenancePanel;
    [SerializeField]
    private GameObject MaintenanceText;
    [SerializeField]
    private GameObject NoUsedChatText;
    [SerializeField]
    private GameObject ClosedShowMessage;
    [SerializeField]
    private GameObject UpdateMessageBadge;
    [SerializeField]
    private GameObject WordlChatBadge;
    [SerializeField]
    private GameObject RoomChatBadge;
    [SerializeField]
    private GameObject GuildChatBadge;
    [SerializeField]
    private ScrollRect ScrollView;
    [SerializeField]
    private Text Caution;
    [SerializeField]
    private Animator CautionAnimator;
    [SerializeField]
    private Text MaintenanceMsgText;
    [SerializeField]
    private Button UsefulButton;
    [SerializeField]
    private GameObject UsefulRootObject;
    [SerializeField]
    private Text InputPlaceholderText;
    [SerializeField]
    private Text ClosedShowMessageText;
    [SerializeField]
    private SRPG_Button OpenCloseButton;
    private GameManager gm;
    private float mRestTime_Opend_UpdateWorldChat;
    private float mRestTime_Opend_UpdateRoomChat;
    private float mRestTime_Opend_UpdateGuildChat;
    private float mRestTime_Closed_UpdateWorldChat;
    private float mRestTime_Closed_UpdateRoomChat;
    private float mRestTime_Closed_UpdateGuildChat;
    private static long system_message_local_id;
    private Text mMaintenance;
    private Text mNoUsedChat;
    private bool is_need_reset_world;
    private bool is_need_reset_room;
    private bool is_need_reset_guild;
    private ChatLog mWorldChatLog;
    private ChatLog mRoomChatLog;
    private ChatLog mGuildChatLog;
    private ChatLog mOfficalChatLog;
    private ChatWindow.eChatType mCurrentChatType;
    private string mInputPlaceholderDefaultText_World;
    private string mInputPlaceholderDefaultText_Room;
    private string mInputPlaceholderDefaultText_Guild;
    private static ChatLog CacheRoomChatLog;
    private static ChatUtility.RoomInfo room_info;
    public static ChatUtility.RoomMemberManager room_member_manager;
    private List<ChatLogItem> mItems;
    private bool mRequesting;
    private bool mInitialized;
    private long mCurrentGuildId;
    [SerializeField]
    private GameObject RootWindow;
    [SerializeField]
    private float SlidePositionX;
    private bool mChatPermit;
    private FlowNode_SendChatMessage mFNode_Sendmessage;
    private bool IsActiveMaintenancePanel;
    private bool IsActiveNoUsedChatText;
    private bool IsActiveMaintenanceText;

    public bool IsClose
    {
      get
      {
        if (this.mState == null)
          return true;
        return this.mState.IsInState<ChatWindow.State_WaitClosed>();
      }
    }

    public bool IsOpened
    {
      get
      {
        return this.mOpened;
      }
    }

    public bool IsMaintenance
    {
      get
      {
        return this.Maintenance;
      }
    }

    public bool IsRequesting
    {
      get
      {
        return this.mRequesting;
      }
    }

    public bool IsInitialized
    {
      get
      {
        return this.mInitialized;
      }
      set
      {
        this.mInitialized = value;
      }
    }

    public bool IsPermit
    {
      get
      {
        return this.mChatPermit;
      }
    }

    private FlowNode_SendChatMessage FlowNodeSendChatMessage
    {
      get
      {
        if ((UnityEngine.Object) this.mFNode_Sendmessage == (UnityEngine.Object) null)
          this.mFNode_Sendmessage = this.gameObject.GetComponent<FlowNode_SendChatMessage>();
        return this.mFNode_Sendmessage;
      }
    }

    public ChatLog GetChatLogInstance(ChatWindow.eChatType _chat_type)
    {
      switch (_chat_type)
      {
        case ChatWindow.eChatType.World:
          if (this.mWorldChatLog == null)
            this.mWorldChatLog = new ChatLog();
          return this.mWorldChatLog;
        case ChatWindow.eChatType.Room:
          if (this.mRoomChatLog == null)
            this.mRoomChatLog = new ChatLog();
          return this.mRoomChatLog;
        case ChatWindow.eChatType.Guild:
          if (this.mGuildChatLog == null)
            this.mGuildChatLog = new ChatLog();
          return this.mGuildChatLog;
        default:
          return (ChatLog) null;
      }
    }

    public void SetChatLog(ChatLog _chat_log, ChatWindow.eChatType _chat_type)
    {
      if (_chat_log == null)
        return;
      this.GetChatLogInstance(_chat_type).AddMessage(_chat_log.messages);
      this.SaveRoomChatLogCache();
    }

    public void SetChatLogAndSystemMessageMerge(ChatLog _server_chat_log, long _exclude_id)
    {
      ChatLog chatLog = new ChatLog();
      List<ChatLogParam> chatLogParamList = new List<ChatLogParam>();
      ChatLogParam chatLogParam = ChatWindow.CacheRoomChatLog.messages.Find((Predicate<ChatLogParam>) (log => log.id == _exclude_id));
      for (int i = 0; i < ChatWindow.CacheRoomChatLog.messages.Count; ++i)
      {
        if (ChatWindow.CacheRoomChatLog.messages[i].messageType == ChatLogParam.eChatMessageType.SYSTEM || chatLogParam != null && ChatWindow.CacheRoomChatLog.messages[i].id == chatLogParam.id || _server_chat_log.messages.Find((Predicate<ChatLogParam>) (log => log.id == ChatWindow.CacheRoomChatLog.messages[i].id)) != null)
          chatLog.AddMessage(ChatWindow.CacheRoomChatLog.messages[i]);
        else
          chatLogParamList.Add(ChatWindow.CacheRoomChatLog.messages[i]);
      }
      for (int i = 0; i < _server_chat_log.messages.Count; ++i)
      {
        if (chatLog.messages.Find((Predicate<ChatLogParam>) (log => log.id == _server_chat_log.messages[i].id)) == null)
          chatLog.AddMessage(_server_chat_log.messages[i]);
      }
      if (chatLogParam != null)
      {
        int _index = chatLog.messages.IndexOf(chatLogParam);
        if (_index >= 0)
        {
          for (int index = 0; index < chatLogParamList.Count; ++index)
          {
            if (!(chatLogParamList[index].fuid != chatLogParam.fuid))
            {
              chatLog.RemoveByIndex(_index);
              break;
            }
          }
        }
      }
      this.mRoomChatLog = chatLog;
      this.SaveRoomChatLogCache();
    }

    private void UpdateRootWindowState(bool state)
    {
      if ((UnityEngine.Object) this.RootWindow != (UnityEngine.Object) null)
        this.RootWindow.SetActive(state);
      if (!((UnityEngine.Object) this.MessageRoot != (UnityEngine.Object) null))
        return;
      this.MessageRoot.SetActive(state);
    }

    public void Activated(int pinID)
    {
      switch (pinID)
      {
        case 42:
          this.ResetChatLog(this.mCurrentChatType);
          break;
        case 43:
          this.InputFieldMessage.text = string.Empty;
          this.RequestChatLog(this.mCurrentChatType, false);
          break;
        case 45:
          this.RefreshCaution();
          break;
        default:
          switch (pinID - 50)
          {
            case 0:
              this.UpdateMessageBadgeState();
              this.RefreshChatLogView(false);
              this.Maintenance = false;
              return;
            case 1:
              if (!((UnityEngine.Object) this.RootWindow != (UnityEngine.Object) null))
                return;
              this.RootWindow.transform.position = (Vector3) new Vector2(this.SlidePositionX, this.RootWindow.transform.position.y);
              return;
            case 2:
              if (!((UnityEngine.Object) this.RootWindow != (UnityEngine.Object) null))
                return;
              this.RootWindow.transform.position = (Vector3) new Vector2(0.0f, this.RootWindow.transform.position.y);
              return;
            default:
              if (pinID != 10)
              {
                if (pinID != 11)
                {
                  switch (pinID - 20)
                  {
                    case 0:
                      this.Open();
                      return;
                    case 3:
                      this.Close();
                      return;
                    default:
                      if (pinID != 60)
                      {
                        if (pinID != 61)
                        {
                          if (pinID != 70)
                          {
                            if (pinID != 100)
                            {
                              if (pinID != 112 || !((UnityEngine.Object) this.RootWindow != (UnityEngine.Object) null))
                                return;
                              this.OpenSendEntryRequestConfirmWindow();
                              return;
                            }
                            this.SetSendStamp();
                            this.SetActiveUsefulWindowObject(false);
                            return;
                          }
                          this.RefreshChatLogView(false);
                          this.Maintenance = true;
                          this.MaintenanceMsg = Network.ErrMsg;
                          Network.ResetError();
                          return;
                        }
                        this.DeleteChatMessageObjects();
                        this.mIsEnableOpenCloseButton = true;
                        return;
                      }
                      this.mIsEnableOpenCloseButton = true;
                      return;
                  }
                }
                else
                {
                  this.SetActiveOpenCloseButton(true);
                  if (string.IsNullOrEmpty(FlowNode_Variable.Get("CHAT_SCENE_STATE")))
                  {
                    this.ExitRoomSelf();
                    this.UpdateRootWindowState(false);
                    this.mChatPermit = false;
                  }
                  FlowNode_Variable.Set("CHAT_SCENE_STATE", string.Empty);
                  return;
                }
              }
              else
              {
                this.SetActiveOpenCloseButton(true);
                this.UpdateRootWindowState(true);
                this.mChatPermit = true;
                this.mIsEnableOpenCloseButton = true;
                FlowNode_Variable.Set("IS_EXTERNAL_API_PERMIT", string.Empty);
                return;
              }
          }
      }
    }

    private void CreateChatMessageObjects()
    {
      if (this.mItems != null)
        return;
      this.mItems = new List<ChatLogItem>((int) ChatWindow.MAX_CHAT_LOG_ITEM);
      for (int index = 0; index < (int) ChatWindow.MAX_CHAT_LOG_ITEM; ++index)
      {
        GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.MessageTemplate);
        gameObject.transform.SetParent(this.MessageRoot.transform, false);
        this.mItems.Add(gameObject.GetComponent<ChatLogItem>());
      }
    }

    public void DeleteChatMessageObjects()
    {
      if (this.mItems == null)
        return;
      for (int index = 0; index < this.mItems.Count; ++index)
        UnityEngine.Object.Destroy((UnityEngine.Object) this.mItems[index].gameObject);
      this.mItems.Clear();
      this.mItems = (List<ChatLogItem>) null;
    }

    public void Open()
    {
      this.UpdateWindowState(20);
      this.CreateChatMessageObjects();
      this.UpdateMessageBadgeState();
      this.RefreshChatLogView(true);
    }

    public void Close()
    {
      this.UpdateWindowState(23);
      this.UpdateMessageBadgeState();
      this.RefreshChatLogView(false);
    }

    public void SetActiveOpenCloseButton(bool is_active)
    {
      if ((UnityEngine.Object) this.OpenCloseButton == (UnityEngine.Object) null)
        return;
      this.OpenCloseButton.gameObject.SetActive(is_active);
    }

    private void ResetChatLog(ChatWindow.eChatType _chat_type)
    {
      if (_chat_type == ChatWindow.eChatType.Room)
      {
        this.ClearAllItems();
        this.CheckChannelUpdate();
        this.UpdateChannelPanel();
        this.SaveRoomChatLogCache();
        ChatLog chatLogInstance = this.GetChatLogInstance(ChatWindow.eChatType.Room);
        long topMessageIdServer = chatLogInstance.TopMessageIdServer;
        chatLogInstance.Reset();
        this.RequestRoomChatLog(topMessageIdServer, true);
        this.is_need_reset_world = true;
        this.is_need_reset_guild = true;
      }
      else if (_chat_type == ChatWindow.eChatType.Guild)
      {
        this.ClearAllItems();
        this.CheckChannelUpdate();
        this.UpdateChannelPanel();
        this.GetChatLogInstance(ChatWindow.eChatType.Guild).Reset();
        this.RequestChatLog(ChatWindow.eChatType.Guild, false);
        this.is_need_reset_world = true;
        this.is_need_reset_room = true;
      }
      else
      {
        this.ClearAllItems();
        this.CheckChannelUpdate();
        this.UpdateChannelPanel();
        this.GetChatLogInstance(ChatWindow.eChatType.World).Reset();
        this.RequestChatLog(ChatWindow.eChatType.World, false);
        this.is_need_reset_room = true;
        this.is_need_reset_guild = true;
      }
    }

    private void RefreshCaution()
    {
      string str = FlowNode_Variable.Get("MESSAGE_CAUTION_SEND_MESSAGE");
      if (string.IsNullOrEmpty(str))
      {
        if (!((UnityEngine.Object) this.CautionAnimator != (UnityEngine.Object) null))
          return;
        this.CautionAnimator.ResetTrigger("onShowCaution");
      }
      else
      {
        this.Caution.text = str;
        if (!((UnityEngine.Object) this.CautionAnimator != (UnityEngine.Object) null))
          return;
        this.CautionAnimator.SetTrigger("onShowCaution");
      }
    }

    private void UpdateWindowState(int inputPinID)
    {
      if (!this.mIsEnableOpenCloseButton)
        return;
      int pinID = 21;
      if (inputPinID == 23)
      {
        this.UsefulRootObject.SetActive(false);
        pinID = 22;
        this.mOpened = false;
      }
      else
        this.mOpened = true;
      this.mIsEnableOpenCloseButton = false;
      this.ClosedShowMessageText.text = string.Empty;
      this.ClosedShowMessage.SetActive(!this.mOpened);
      FlowNode_GameObject.ActivateOutputLinks((Component) this, pinID);
    }

    private void Awake()
    {
      if (ChatWindow.room_info == null)
        ChatWindow.room_info = new ChatUtility.RoomInfo();
      if (ChatWindow.room_member_manager == null)
        ChatWindow.room_member_manager = new ChatUtility.RoomMemberManager();
      if (ChatWindow.CacheRoomChatLog != null)
        this.CopyChatLog(ChatWindow.CacheRoomChatLog, ref this.mRoomChatLog);
      if ((UnityEngine.Object) this.MessageTemplate != (UnityEngine.Object) null)
        this.MessageTemplate.SetActive(false);
      if ((UnityEngine.Object) this.InputFieldMessage != (UnityEngine.Object) null)
        this.InputFieldMessage.onEndEdit.AddListener((UnityAction<string>) (_param1 => this.OnSetSendMessage()));
      if ((UnityEngine.Object) this.MaintenancePanel != (UnityEngine.Object) null)
      {
        this.MaintenancePanel.SetActive(false);
        this.IsActiveMaintenancePanel = false;
      }
      if ((UnityEngine.Object) this.NoUsedChatText != (UnityEngine.Object) null)
        this.IsActiveNoUsedChatText = this.NoUsedChatText.activeSelf;
      if ((UnityEngine.Object) this.MaintenanceText != (UnityEngine.Object) null)
        this.IsActiveMaintenanceText = this.MaintenanceText.activeSelf;
      if ((UnityEngine.Object) this.UpdateMessageBadge != (UnityEngine.Object) null)
        this.UpdateMessageBadge.SetActive(false);
      this.mTabButtons = new Dictionary<ChatWindow.eChatType, SRPG_ToggleButton>()
      {
        {
          ChatWindow.eChatType.World,
          this.Tab_World
        },
        {
          ChatWindow.eChatType.Room,
          this.Tab_Room
        },
        {
          ChatWindow.eChatType.Guild,
          this.Tab_Guild
        }
      };
      foreach (ChatWindow.eChatType key in this.mTabButtons.Keys)
      {
        if (!((UnityEngine.Object) this.mTabButtons[key] == (UnityEngine.Object) null))
          this.mTabButtons[key].AddListener(new SRPG_Button.ButtonClickEvent(this.OnTabChange));
      }
      this.ChangeChatTypeTab(!ChatWindow.room_info.IsActive ? ChatWindow.eChatType.World : ChatWindow.eChatType.Room);
      if ((UnityEngine.Object) this.UsefulButton != (UnityEngine.Object) null)
        this.UsefulButton.onClick.AddListener(new UnityAction(this.OnUsefulButton));
      ChatWindow.room_info.Init(this);
    }

    private void OnTabChange(SRPG_Button button)
    {
      if (!this.TabChange(button))
        ;
    }

    private bool TabChange(SRPG_Button button)
    {
      if (!button.IsInteractable())
        return false;
      foreach (ChatWindow.eChatType key in this.mTabButtons.Keys)
      {
        if (this.mTabButtons[key].name == button.name)
        {
          this.ChangeChatTypeTab(key);
          break;
        }
      }
      return true;
    }

    public void ChangeChatTypeTab(ChatWindow.eChatType _chat_type)
    {
      bool flag = this.mCurrentChatType == _chat_type;
      this.mCurrentChatType = _chat_type;
      this.SetMessageDataToFlowNode(this.InputFieldMessage.text, false, false, (ChatWindow.FinishSendCallback) null);
      if (flag)
        return;
      foreach (ChatWindow.eChatType key in this.mTabButtons.Keys)
        this.mTabButtons[key].IsOn = false;
      this.mTabButtons[_chat_type].IsOn = true;
      this.SetActiveUsefulWindowObject(false);
      if (this.is_need_reset_world && _chat_type == ChatWindow.eChatType.World)
      {
        this.is_need_reset_world = false;
        this.ClearAllItems();
        this.GetChatLogInstance(ChatWindow.eChatType.World).Reset();
        this.RequestChatLog(ChatWindow.eChatType.World, false);
      }
      else if (this.is_need_reset_room && _chat_type == ChatWindow.eChatType.Room)
      {
        this.is_need_reset_room = false;
        this.ClearAllItems();
        ChatLog chatLogInstance = this.GetChatLogInstance(ChatWindow.eChatType.Room);
        long topMessageIdServer = chatLogInstance.TopMessageIdServer;
        chatLogInstance.Reset();
        this.RequestRoomChatLog(topMessageIdServer, true);
      }
      else if (this.is_need_reset_guild && _chat_type == ChatWindow.eChatType.Guild)
      {
        this.is_need_reset_guild = false;
        this.ClearAllItems();
        this.GetChatLogInstance(ChatWindow.eChatType.Guild).Reset();
        this.RequestChatLog(ChatWindow.eChatType.Guild, false);
      }
      else
      {
        this.UpdateMessageBadgeState();
        this.RefreshChatLogView(true);
      }
    }

    private void OnUsefulButton()
    {
      if ((UnityEngine.Object) this.UsefulRootObject == (UnityEngine.Object) null)
        return;
      this.SetActiveUsefulWindowObject(!this.UsefulRootObject.activeSelf);
    }

    public void SetActiveUsefulWindowObject(bool _active)
    {
      if ((UnityEngine.Object) this.UsefulRootObject == (UnityEngine.Object) null)
        return;
      this.UsefulRootObject.SetActive(_active);
    }

    public void LoadTemplateMessage()
    {
      ChatTemplateMessage componentInChildren = this.GetComponentInChildren<ChatTemplateMessage>(true);
      if ((UnityEngine.Object) componentInChildren == (UnityEngine.Object) null)
        return;
      componentInChildren.LoadTemplateMessage();
    }

    private void Start()
    {
      this.gm = MonoSingleton<GameManager>.Instance;
      if (this.mChatInspectionMaster == null || this.mChatInspectionMaster.Count <= 0)
      {
        bool is_success = false;
        this.mChatInspectionMaster = ChatUtility.LoadInspectionMaster(ref is_success);
        if (!is_success)
        {
          DebugUtility.LogError("ChatWindow Error:Failed Load InspectionMaster!");
          return;
        }
      }
      if (!ChatUtility.SetupChatChannelMaster())
      {
        DebugUtility.LogError("ChatWindow Error:Failed Load ChatChannelMaster!");
      }
      else
      {
        if ((UnityEngine.Object) this.InputPlaceholderText != (UnityEngine.Object) null)
        {
          this.mInputPlaceholderDefaultText_World = this.InputPlaceholderText.text;
          this.mInputPlaceholderDefaultText_Room = LocalizedText.Get("sys.CHAT_DISABLE_INPUT_FIELD_ROOM");
          this.mInputPlaceholderDefaultText_Guild = LocalizedText.Get("sys.CHAT_DISABLE_INPUT_FIELD_GUILD");
        }
        if ((UnityEngine.Object) this.InputPlaceholderText == (UnityEngine.Object) null)
          DebugUtility.LogError("InputPlaceholderText is NULL");
        this.mState = new StateMachine<ChatWindow>(this);
        this.mState.GotoState<ChatWindow.State_Init>();
      }
    }

    public void SetMessageDataToFlowNode(string input_text, bool is_force_send = false, bool is_guild_invite = false, ChatWindow.FinishSendCallback callback = null)
    {
      if ((UnityEngine.Object) this.FlowNodeSendChatMessage == (UnityEngine.Object) null)
        DebugUtility.LogError("CHatWindow.cs -> OnSetSendMessage():FlowNode_SendChatMessage is Null References!");
      else if (string.IsNullOrEmpty(input_text))
      {
        this.FlowNodeSendChatMessage.ResetParam();
      }
      else
      {
        string message = input_text;
        if (message.Length > this.CharacterLimit)
          message = message.Substring(0, this.CharacterLimit);
        switch (this.mCurrentChatType)
        {
          case ChatWindow.eChatType.World:
            this.FlowNodeSendChatMessage.SetMessageData((int) GlobalVars.CurrentChatChannel, message, is_guild_invite, callback);
            break;
          case ChatWindow.eChatType.Room:
            this.FlowNodeSendChatMessage.SetMessageData(GlobalVars.SelectedMultiPlayRoomName, message, callback);
            break;
          case ChatWindow.eChatType.Guild:
            this.FlowNodeSendChatMessage.SetMessageData(message, callback);
            break;
        }
        if (!is_force_send)
          return;
        this.FlowNodeSendChatMessage.ReqestSendMessage();
      }
    }

    private void OnSetSendMessage()
    {
      this.SetMessageDataToFlowNode(this.InputFieldMessage.text, false, false, (ChatWindow.FinishSendCallback) null);
    }

    private void SetSendStamp()
    {
      if ((UnityEngine.Object) this.FlowNodeSendChatMessage == (UnityEngine.Object) null)
        return;
      int stamp_id = int.Parse(FlowNode_Variable.Get("SELECT_STAMP_ID"));
      switch (this.mCurrentChatType)
      {
        case ChatWindow.eChatType.World:
          this.FlowNodeSendChatMessage.SetStampData((int) GlobalVars.CurrentChatChannel, stamp_id, (ChatWindow.FinishSendCallback) null);
          break;
        case ChatWindow.eChatType.Room:
          this.FlowNodeSendChatMessage.SetStampData(GlobalVars.SelectedMultiPlayRoomName, stamp_id, (ChatWindow.FinishSendCallback) null);
          break;
        case ChatWindow.eChatType.Guild:
          this.FlowNodeSendChatMessage.SetStampData(stamp_id, (ChatWindow.FinishSendCallback) null);
          break;
      }
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 101);
    }

    private void UpdateSendMessageButtonInteractable()
    {
      if ((UnityEngine.Object) this.InputFieldMessage == (UnityEngine.Object) null || (UnityEngine.Object) this.SendMessageButton == (UnityEngine.Object) null || (UnityEngine.Object) this.UsefulButton == (UnityEngine.Object) null)
        return;
      if (this.mCurrentChatType == ChatWindow.eChatType.Room && !ChatWindow.room_info.IsActive)
      {
        SRPG_Button sendMessageButton = this.SendMessageButton;
        bool flag1 = false;
        this.InputFieldMessage.interactable = flag1;
        bool flag2 = flag1;
        this.UsefulButton.interactable = flag2;
        int num = flag2 ? 1 : 0;
        sendMessageButton.interactable = num != 0;
      }
      else if (this.mCurrentChatType == ChatWindow.eChatType.Guild && (MonoSingleton<GameManager>.Instance.Player.PlayerGuild == null || !MonoSingleton<GameManager>.Instance.Player.PlayerGuild.IsJoined))
      {
        SRPG_Button sendMessageButton = this.SendMessageButton;
        bool flag1 = false;
        this.InputFieldMessage.interactable = flag1;
        bool flag2 = flag1;
        this.UsefulButton.interactable = flag2;
        int num = flag2 ? 1 : 0;
        sendMessageButton.interactable = num != 0;
      }
      else
      {
        SRPG_Button sendMessageButton = this.SendMessageButton;
        bool flag1 = true;
        this.InputFieldMessage.interactable = flag1;
        bool flag2 = flag1;
        this.UsefulButton.interactable = flag2;
        int num = flag2 ? 1 : 0;
        sendMessageButton.interactable = num != 0;
      }
    }

    private void UpdateInputPlaceholderText()
    {
      if ((UnityEngine.Object) this.InputFieldMessage == (UnityEngine.Object) null || (UnityEngine.Object) this.InputPlaceholderText == (UnityEngine.Object) null)
        return;
      if (this.mCurrentChatType == ChatWindow.eChatType.Room && !ChatWindow.room_info.IsActive)
        this.InputPlaceholderText.text = this.mInputPlaceholderDefaultText_Room;
      else if (this.mCurrentChatType == ChatWindow.eChatType.Guild && (MonoSingleton<GameManager>.Instance.Player.PlayerGuild == null || !MonoSingleton<GameManager>.Instance.Player.PlayerGuild.IsJoined))
        this.InputPlaceholderText.text = this.mInputPlaceholderDefaultText_Guild;
      else
        this.InputPlaceholderText.text = this.mInputPlaceholderDefaultText_World;
    }

    private void Update()
    {
      if ((UnityEngine.Object) this.RootWindow == (UnityEngine.Object) null || !this.RootWindow.activeSelf || this.UpdateMaintenancePanel() || (!this.IsPermit && !ChatUtility.IsMultiQuestNow() || (!this.gm.IsExternalPermit() || this.mState == null)))
        return;
      this.UpdateSendMessageButtonInteractable();
      this.UpdateInputPlaceholderText();
      ChatWindow.room_info.Run();
      this.CheckRoomMember();
      this.mState.Update();
    }

    private void ClearAllItems()
    {
      if (this.mItems == null)
        return;
      for (int index = 0; index < this.mItems.Count; ++index)
        this.mItems[index].Clear();
    }

    private void UpdateChannelPanel()
    {
      if ((UnityEngine.Object) this.ChannelPanel == (UnityEngine.Object) null)
        return;
      int num1 = this.ChannelPanel.transform.childCount - 1;
      int length = GlobalVars.CurrentChatChannel.ToString().Length;
      int currentChatChannel = (int) GlobalVars.CurrentChatChannel;
      for (int index = num1; index > 0; --index)
      {
        Transform transform = this.ChannelPanel.transform.Find("value_" + Mathf.Pow(10f, (float) (index - 1)).ToString());
        if (!((UnityEngine.Object) transform == (UnityEngine.Object) null))
        {
          if (length < index)
          {
            transform.gameObject.SetActive(false);
          }
          else
          {
            int num2 = (int) Mathf.Pow(10f, (float) (index - 1));
            int num3 = currentChatChannel / num2;
            transform.gameObject.SetActive(true);
            transform.GetComponent<ImageArray>().ImageIndex = num3;
            currentChatChannel %= num2;
          }
        }
      }
    }

    private void RequestChatLog(ChatWindow.eChatType select, bool force_request = false)
    {
      switch (select)
      {
        case ChatWindow.eChatType.World:
          this.RequestWorldChatLog();
          break;
        case ChatWindow.eChatType.Room:
          this.RequestRoomChatLog(this.GetChatLogInstance(ChatWindow.eChatType.Room).LastMessageIdServer, force_request);
          break;
        case ChatWindow.eChatType.Guild:
          this.RequestGuildChatLog();
          break;
      }
    }

    private void RequestWorldChatLog()
    {
      ChatLog chatLogInstance = this.GetChatLogInstance(ChatWindow.eChatType.World);
      FlowNode_ReqChatMessageWorld component = this.gameObject.GetComponent<FlowNode_ReqChatMessageWorld>();
      if ((UnityEngine.Object) component != (UnityEngine.Object) null)
      {
        component.SetChatMessageInfo((int) GlobalVars.CurrentChatChannel, 0L, (int) ChatWindow.MAX_CHAT_LOG_ITEM, chatLogInstance.LastMessageIdServer);
        this.mRequesting = true;
      }
      if (!this.mRequesting)
        return;
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 40);
    }

    private void RequestRoomChatLog(long exclude_id, bool force_request = false)
    {
      if (!ChatWindow.room_info.IsActive && exclude_id <= 0L)
        return;
      if (force_request || ChatWindow.room_info.IsActive)
      {
        FlowNode_ReqChatMessageRoom component = this.gameObject.GetComponent<FlowNode_ReqChatMessageRoom>();
        if ((UnityEngine.Object) component != (UnityEngine.Object) null)
        {
          string room_token = !ChatWindow.room_info.IsActive ? string.Empty : GlobalVars.SelectedMultiPlayRoomName;
          bool is_sys_msg_merge = force_request;
          component.SetChatMessageInfo(room_token, 0L, (int) ChatWindow.MAX_CHAT_LOG_ITEM, exclude_id, is_sys_msg_merge);
          this.mRequesting = true;
        }
      }
      if (!this.mRequesting)
        return;
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 46);
    }

    public void RequestGuildChatLog()
    {
      bool flag1 = false;
      if (MonoSingleton<GameManager>.Instance.Player.PlayerGuild != null)
      {
        bool flag2 = false;
        if (this.mCurrentGuildId != (long) MonoSingleton<GameManager>.Instance.Player.PlayerGuild.Gid && MonoSingleton<GameManager>.Instance.Player.PlayerGuild.IsJoined)
        {
          this.mCurrentGuildId = (long) MonoSingleton<GameManager>.Instance.Player.PlayerGuild.Gid;
          flag2 = true;
        }
        if (!MonoSingleton<GameManager>.Instance.Player.PlayerGuild.IsJoined && this.mCurrentGuildId > 0L)
        {
          this.mCurrentGuildId = 0L;
          flag2 = true;
        }
        if (flag2)
        {
          this.GetChatLogInstance(ChatWindow.eChatType.Guild).Clear();
          flag1 = true;
        }
      }
      if (!flag1 && (MonoSingleton<GameManager>.Instance.Player.PlayerGuild == null || !MonoSingleton<GameManager>.Instance.Player.PlayerGuild.IsJoined))
        return;
      ChatLog chatLogInstance = this.GetChatLogInstance(ChatWindow.eChatType.Guild);
      FlowNode_ReqChatMessageGuild component = this.gameObject.GetComponent<FlowNode_ReqChatMessageGuild>();
      if ((UnityEngine.Object) component != (UnityEngine.Object) null)
      {
        component.SetChatMessageInfo(0L, (int) ChatWindow.MAX_CHAT_LOG_ITEM, chatLogInstance.LastMessageIdServer);
        this.mRequesting = true;
      }
      if (!this.mRequesting)
        return;
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 47);
    }

    public void ForceUpdateChatLog(ChatWindow.eChatType chat_type)
    {
      switch (chat_type)
      {
        case ChatWindow.eChatType.World:
          this.mRestTime_Opend_UpdateWorldChat = 0.0f;
          break;
        case ChatWindow.eChatType.Room:
          this.mRestTime_Opend_UpdateRoomChat = 0.0f;
          break;
        case ChatWindow.eChatType.Guild:
          this.mRestTime_Opend_UpdateGuildChat = 0.0f;
          break;
      }
    }

    private void RefreshChatLogView(bool _force_refresh = false)
    {
      ChatLog chatLogInstance = this.GetChatLogInstance(this.mCurrentChatType);
      if (chatLogInstance.is_dirty || _force_refresh)
      {
        chatLogInstance.is_dirty = false;
        this.StartCoroutine(this.RefreshChatMessage(chatLogInstance));
      }
      else
        this.mRequesting = false;
    }

    public void RefreshChatLogView(ChatLog target_chat_log)
    {
      if (target_chat_log == null || target_chat_log.messages == null)
        return;
      for (int index = 0; index < target_chat_log.messages.Count; ++index)
      {
        if (target_chat_log.messages[index].messageType != ChatLogParam.eChatMessageType.SYSTEM)
          target_chat_log.messages[index].message = ChatUtility.ReplaceNGWord(target_chat_log.messages[index].message, this.mChatInspectionMaster, "*");
      }
    }

    [DebuggerHidden]
    private IEnumerator RefreshChatMessage(ChatLog _chat_log)
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new ChatWindow.\u003CRefreshChatMessage\u003Ec__Iterator0() { _chat_log = _chat_log, \u0024this = this };
    }

    private void setPosX(RectTransform rt, float px)
    {
      if (!(bool) ((UnityEngine.Object) rt))
        return;
      Vector2 anchoredPosition = rt.anchoredPosition;
      anchoredPosition.x = px;
      rt.anchoredPosition = anchoredPosition;
    }

    private void OnTapUnitIcon(SRPG_Button button)
    {
      if ((UnityEngine.Object) button == (UnityEngine.Object) null)
        return;
      ChatLogItem componentInParent = button.GetComponentInParent<ChatLogItem>();
      if ((UnityEngine.Object) componentInParent == (UnityEngine.Object) null || componentInParent.ChatLogParam == null || string.IsNullOrEmpty(componentInParent.ChatLogParam.uid))
        return;
      FlowNode_Variable.Set("SelectUserID", componentInParent.ChatLogParam.uid);
      FlowNode_Variable.Set("IsBlackList", "0");
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 30);
    }

    private void SetActiveMaintenancePanel(bool active)
    {
      if (this.IsActiveMaintenancePanel == active)
        return;
      this.MaintenancePanel.SetActive(active);
      this.IsActiveMaintenancePanel = active;
    }

    private void SetActiveNoUsedChatText(bool active)
    {
      if (this.IsActiveNoUsedChatText == active)
        return;
      this.NoUsedChatText.SetActive(active);
      this.IsActiveNoUsedChatText = active;
    }

    private void SetActiveMaintenanceText(bool active)
    {
      if (this.IsActiveMaintenanceText == active)
        return;
      this.MaintenanceText.SetActive(active);
      this.IsActiveMaintenanceText = active;
    }

    private bool UpdateMaintenancePanel()
    {
      if (!GameUtility.Config_ChatState.Value)
      {
        this.SetActiveMaintenancePanel(true);
        this.SetActiveNoUsedChatText(true);
        this.SetActiveMaintenanceText(false);
        this.ResetChatOff();
        return true;
      }
      if (this.Maintenance)
      {
        this.SetActiveMaintenancePanel(true);
        this.SetActiveMaintenanceText(true);
        this.SetActiveNoUsedChatText(false);
        this.ResetChatOff();
        if (!string.IsNullOrEmpty(this.MaintenanceMsg))
          this.MaintenanceMsgText.text = this.MaintenanceMsg;
        return true;
      }
      this.SetActiveMaintenancePanel(false);
      this.SetActiveNoUsedChatText(false);
      this.SetActiveMaintenanceText(false);
      return false;
    }

    private void UpdateClosingMessage(bool _is_world_dirty = false, bool _is_room_dirty = false, bool _is_guild_dirty = false)
    {
      if (this.mOpened)
      {
        this.ClosedShowMessageText.text = string.Empty;
        this.ClosedShowMessage.SetActive(false);
      }
      else
      {
        this.ClosedShowMessage.SetActive(true);
        ChatLog chatLogInstance1 = this.GetChatLogInstance(ChatWindow.eChatType.World);
        ChatLog chatLogInstance2 = this.GetChatLogInstance(ChatWindow.eChatType.Room);
        ChatLog chatLogInstance3 = this.GetChatLogInstance(ChatWindow.eChatType.Guild);
        long num1 = 0;
        long num2 = 0;
        long num3 = 0;
        ChatLogParam chatLogParam1 = chatLogInstance1 == null || chatLogInstance1.messages.Count == 0 ? (ChatLogParam) null : chatLogInstance1.messages[chatLogInstance1.messages.Count - 1];
        ChatLogParam chatLogParam2 = chatLogInstance2 == null || chatLogInstance2.messages.Count == 0 ? (ChatLogParam) null : chatLogInstance2.messages[chatLogInstance2.messages.Count - 1];
        ChatLogParam chatLogParam3 = chatLogInstance3 == null || chatLogInstance3.messages.Count == 0 ? (ChatLogParam) null : chatLogInstance3.messages[chatLogInstance3.messages.Count - 1];
        string str = this.ClosedShowMessageText.text;
        if (!_is_world_dirty && !_is_room_dirty && !_is_guild_dirty)
          return;
        if (_is_world_dirty && chatLogParam1 != null)
          num1 = chatLogParam1.posted_at;
        if (_is_room_dirty && chatLogParam2 != null)
          num2 = chatLogParam2.posted_at;
        if (_is_guild_dirty && chatLogParam3 != null)
          num3 = chatLogParam3.posted_at;
        if (num1 != 0L || num2 != 0L || num3 != 0L)
        {
          List<long> longList = new List<long>() { num1, num2, num3 };
          longList.Sort();
          long num4 = longList[longList.Count - 1];
          if (num4 == num1)
            str = this.GetOneLineMessageText(chatLogParam1);
          else if (num4 == num2)
            str = this.GetOneLineMessageText(chatLogParam2);
          else if (num4 == num3)
            str = this.GetOneLineMessageText(chatLogParam3);
        }
        this.ClosedShowMessageText.text = str;
      }
    }

    private string GetOneLineMessageText(ChatLogParam _param)
    {
      string str = string.Empty;
      if (_param != null)
      {
        if (_param.messageType == ChatLogParam.eChatMessageType.MESSAGE)
          str = LocalizedText.Get("sys.TEXT_CLOSED_SHOW_MESSAGE", (object) _param.name, (object) ChatUtility.ReplaceNGWord(_param.message, this.mChatInspectionMaster, "*"));
        else if (_param.messageType == ChatLogParam.eChatMessageType.STAMP)
          str = LocalizedText.Get("sys.TEXT_CLOSED_SHOW_MESSAGE_STAMP", new object[1]
          {
            (object) _param.name
          });
        else if (_param.messageType == ChatLogParam.eChatMessageType.SYSTEM)
          str = _param.message.Replace("\n", string.Empty);
        else if (_param.messageType == ChatLogParam.eChatMessageType.GUILD_INVITE)
          str = LocalizedText.Get("sys.TEXT_CLOSED_SHOW_MESSAGE", (object) _param.name, (object) ChatUtility.ReplaceNGWord(_param.message, this.mChatInspectionMaster, "*"));
      }
      return str;
    }

    private void ResetChatOff()
    {
      this.ResetCloseShowMessage();
      this.UpdateMessageBadge.SetActive(false);
    }

    private void UpdateMessageBadgeState()
    {
      bool flag1 = this.RefreshWorldChatBadge();
      bool flag2 = this.RefreshRoomChatBadge();
      bool flag3 = this.RefreshGuildChatBadge();
      this.RefreshMainBadge(flag1, flag2, flag3);
      this.UpdateClosingMessage(flag1, flag2, flag3);
    }

    private void RefreshMainBadge(bool is_show_world_badge, bool is_show_room_badge, bool is_show_guild_badge)
    {
      if ((UnityEngine.Object) this.UpdateMessageBadge == (UnityEngine.Object) null)
        return;
      if (this.mOpened)
      {
        this.UpdateMessageBadge.SetActive(false);
      }
      else
      {
        if (!is_show_world_badge && !is_show_room_badge && !is_show_guild_badge)
          return;
        this.UpdateMessageBadge.SetActive(true);
      }
    }

    private bool RefreshWorldChatBadge()
    {
      if ((UnityEngine.Object) this.WordlChatBadge == (UnityEngine.Object) null)
        return false;
      ChatLog chatLogInstance = this.GetChatLogInstance(ChatWindow.eChatType.World);
      bool flag = chatLogInstance.is_dirty && chatLogInstance.LastMessagePostedAt > TimeManager.FromDateTime(MonoSingleton<GameManager>.Instance.Player.LoginDate);
      if (this.mCurrentChatType == ChatWindow.eChatType.World)
        this.WordlChatBadge.SetActive(false);
      else
        this.WordlChatBadge.SetActive(flag);
      return flag;
    }

    private bool RefreshRoomChatBadge()
    {
      if ((UnityEngine.Object) this.RoomChatBadge == (UnityEngine.Object) null)
        return false;
      ChatLog chatLogInstance = this.GetChatLogInstance(ChatWindow.eChatType.Room);
      bool flag = chatLogInstance.is_dirty && chatLogInstance.LastMessagePostedAt > TimeManager.FromDateTime(MonoSingleton<GameManager>.Instance.Player.LoginDate);
      if (this.mCurrentChatType == ChatWindow.eChatType.Room)
        this.RoomChatBadge.SetActive(false);
      else
        this.RoomChatBadge.SetActive(flag);
      return flag;
    }

    private bool RefreshGuildChatBadge()
    {
      if ((UnityEngine.Object) this.GuildChatBadge == (UnityEngine.Object) null)
        return false;
      ChatLog chatLogInstance = this.GetChatLogInstance(ChatWindow.eChatType.Guild);
      bool flag = chatLogInstance.is_dirty && chatLogInstance.LastMessagePostedAt > TimeManager.FromDateTime(MonoSingleton<GameManager>.Instance.Player.LoginDate);
      if (this.mCurrentChatType == ChatWindow.eChatType.Guild)
        this.GuildChatBadge.SetActive(false);
      else
        this.GuildChatBadge.SetActive(flag);
      return flag;
    }

    private void ResetCloseShowMessage()
    {
      if (!((UnityEngine.Object) this.ClosedShowMessageText != (UnityEngine.Object) null))
        return;
      this.ClosedShowMessageText.text = string.Empty;
    }

    private void CheckChannelUpdate()
    {
      if (this.mLastChannelID == -1)
        this.mLastChannelID = (int) GlobalVars.CurrentChatChannel;
      if (this.mLastChannelID != (int) GlobalVars.CurrentChatChannel && (UnityEngine.Object) this.FlowNodeSendChatMessage != (UnityEngine.Object) null)
        this.SetMessageDataToFlowNode(this.InputFieldMessage.text, false, false, (ChatWindow.FinishSendCallback) null);
      this.mLastChannelID = (int) GlobalVars.CurrentChatChannel;
    }

    private void CheckRoomMember()
    {
      if (!ChatWindow.room_info.IsActive)
      {
        ChatWindow.room_member_manager.Clear();
      }
      else
      {
        this.elapsed_time_for_photon_room_member -= Time.deltaTime;
        if ((double) this.elapsed_time_for_photon_room_member > 0.0)
          return;
        this.elapsed_time_for_photon_room_member = ChatWindow.SPAN_UPDATE_ROOM_MEMBER;
        if (PunMonoSingleton<MyPhoton>.Instance.IsConnectedInRoom())
          ChatWindow.room_member_manager.Refresh(PunMonoSingleton<MyPhoton>.Instance.GetRoomPlayerList());
        if (ChatWindow.room_member_manager.EntryMembers.Count <= 0 && ChatWindow.room_member_manager.LeaveMembers.Count <= 0)
          return;
        ChatLog chatLogInstance = this.GetChatLogInstance(ChatWindow.eChatType.Room);
        List<ChatLogParam> _message = new List<ChatLogParam>();
        ChatLogParam chatLogParam1 = (ChatLogParam) null;
        string str = !ChatWindow.room_info.QuestParam.IsMultiTower ? ChatWindow.room_info.QuestParam.name : ChatWindow.room_info.QuestParam.title + ChatWindow.room_info.QuestParam.name;
        for (int index = 0; index < ChatWindow.room_member_manager.EntryMembers.Count; ++index)
        {
          ChatLogParam chatLogParam2 = new ChatLogParam();
          chatLogParam2.id = this.GenerateSystemMessageId();
          chatLogParam2.messageType = ChatLogParam.eChatMessageType.SYSTEM;
          chatLogParam2.posted_at = TimeManager.FromDateTime(TimeManager.ServerTime);
          if (MonoSingleton<GameManager>.Instance.DeviceId == ChatWindow.room_member_manager.EntryMembers[index].UID)
          {
            string format = LocalizedText.Get("sys.CHAT_SYSTEM_MESSAGE_ENTRY_ROOM_SELF");
            chatLogParam2.message = string.Format(format, (object) str);
            chatLogParam1 = chatLogParam2;
          }
          else
          {
            string format = LocalizedText.Get("sys.CHAT_SYSTEM_MESSAGE_ENTRY_ROOM_OTHER");
            chatLogParam2.message = string.Format(format, (object) ChatWindow.room_member_manager.EntryMembers[index].Name);
          }
          _message.Add(chatLogParam2);
        }
        for (int index = 0; index < ChatWindow.room_member_manager.LeaveMembers.Count; ++index)
        {
          ChatLogParam chatLogParam2 = new ChatLogParam();
          chatLogParam2.id = this.GenerateSystemMessageId();
          chatLogParam2.messageType = ChatLogParam.eChatMessageType.SYSTEM;
          chatLogParam2.posted_at = TimeManager.FromDateTime(TimeManager.ServerTime);
          if (MonoSingleton<GameManager>.Instance.DeviceId == ChatWindow.room_member_manager.LeaveMembers[index].UID)
          {
            string format = LocalizedText.Get("sys.CHAT_SYSTEM_MESSAGE_LEAVE_ROOM_SELF");
            chatLogParam2.message = string.Format(format, (object) str);
          }
          else
          {
            string format = LocalizedText.Get("sys.CHAT_SYSTEM_MESSAGE_LEAVE_ROOM_OTHER");
            chatLogParam2.message = string.Format(format, (object) ChatWindow.room_member_manager.LeaveMembers[index].Name);
          }
          _message.Add(chatLogParam2);
        }
        if (chatLogParam1 != null)
        {
          _message.Clear();
          _message.Add(chatLogParam1);
        }
        chatLogInstance.AddMessage(_message);
        this.UpdateMessageBadgeState();
        this.RefreshChatLogView(false);
        ChatWindow.room_member_manager.EntryMembers.Clear();
        ChatWindow.room_member_manager.LeaveMembers.Clear();
      }
    }

    public void ExitRoomSelf()
    {
      if (!ChatWindow.room_info.IsActive)
        return;
      ChatWindow.room_info.ExitRoom();
      ChatWindow.room_member_manager.Clear();
      if (ChatWindow.room_info.QuestParam != null)
      {
        ChatLog chatLogInstance = this.GetChatLogInstance(ChatWindow.eChatType.Room);
        ChatLogParam chatLogParam = new ChatLogParam();
        chatLogParam.id = this.GenerateSystemMessageId();
        chatLogParam.messageType = ChatLogParam.eChatMessageType.SYSTEM;
        chatLogParam.posted_at = TimeManager.FromDateTime(TimeManager.ServerTime);
        string format = LocalizedText.Get("sys.CHAT_SYSTEM_MESSAGE_LEAVE_ROOM_SELF");
        string str = !ChatWindow.room_info.QuestParam.IsMultiTower ? ChatWindow.room_info.QuestParam.name : ChatWindow.room_info.QuestParam.title + ChatWindow.room_info.QuestParam.name;
        chatLogParam.message = string.Format(format, (object) str);
        chatLogInstance.AddMessage(chatLogParam);
      }
      this.SaveRoomChatLogCache();
    }

    private long GenerateSystemMessageId()
    {
      --ChatWindow.system_message_local_id;
      return ChatWindow.system_message_local_id;
    }

    private void SaveRoomChatLogCache()
    {
      this.CopyChatLog(this.GetChatLogInstance(ChatWindow.eChatType.Room), ref ChatWindow.CacheRoomChatLog);
    }

    private void CopyChatLog(ChatLog _base, ref ChatLog _target)
    {
      ChatLog chatLog = new ChatLog();
      for (int index = 0; index < _base.messages.Count; ++index)
        chatLog.AddMessage(_base.messages[index]);
      _target = chatLog;
    }

    public void OnGuildEntryRequest(GameObject obj)
    {
      FlowNode_ReqGuildInfo component = this.gameObject.GetComponent<FlowNode_ReqGuildInfo>();
      if ((UnityEngine.Object) component == (UnityEngine.Object) null)
        return;
      GuildData dataOfClass = DataSource.FindDataOfClass<GuildData>(obj, (GuildData) null);
      component.SetParam(dataOfClass.UniqueID);
      this.mRequesting = true;
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 110);
    }

    private void OpenSendEntryRequestConfirmWindow()
    {
      this.mRequesting = false;
      FlowNode_ReqGuildInfo component1 = this.gameObject.GetComponent<FlowNode_ReqGuildInfo>();
      SerializeValueBehaviour component2 = this.GetComponent<SerializeValueBehaviour>();
      if (!((UnityEngine.Object) component1 != (UnityEngine.Object) null) || !((UnityEngine.Object) component2 != (UnityEngine.Object) null))
        return;
      component2.list.SetObject(GuildSVB_Key.GUILD, (object) component1.GuildData);
      component2.list.SetObject(GuildSVB_Key.PLAYER, (object) MonoSingleton<GameManager>.Instance.Player);
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 111);
    }

    public enum eChatType : byte
    {
      None,
      World,
      Room,
      Guild,
    }

    public enum MessageTemplateType : byte
    {
      None,
      OtherUser,
      User,
      System,
    }

    public delegate void FinishSendCallback();

    private class State_NoUsed : State<ChatWindow>
    {
      public override void Begin(ChatWindow self)
      {
      }

      public override void Update(ChatWindow self)
      {
      }
    }

    private class State_Init : State<ChatWindow>
    {
      public override void Update(ChatWindow self)
      {
        if ((int) GlobalVars.CurrentChatChannel <= 0)
          return;
        self.CheckChannelUpdate();
        self.UpdateChannelPanel();
        self.mState.GotoState<ChatWindow.State_WaitClosed>();
      }
    }

    private class State_WaitOpened : State<ChatWindow>
    {
      public override void Begin(ChatWindow self)
      {
        if (!self.IsInitialized)
        {
          self.mRestTime_Opend_UpdateWorldChat = 0.0f;
          self.mRestTime_Opend_UpdateRoomChat = 0.0f;
          self.mRestTime_Opend_UpdateGuildChat = 0.0f;
        }
        self.IsInitialized = true;
      }

      public override void Update(ChatWindow self)
      {
        if (!self.IsOpened)
        {
          self.mState.GotoState<ChatWindow.State_WaitClosed>();
        }
        else
        {
          if (self.IsRequesting)
            return;
          if (self.mCurrentChatType == ChatWindow.eChatType.World && (double) self.mRestTime_Opend_UpdateWorldChat <= 0.0)
          {
            self.mRestTime_Opend_UpdateWorldChat = ChatWindow.SPAN_UPDATE_WORLD_MESSAGE_UIOPEN;
            self.RequestChatLog(self.mCurrentChatType, false);
          }
          else if (self.mCurrentChatType == ChatWindow.eChatType.Room && (double) self.mRestTime_Opend_UpdateRoomChat <= 0.0)
          {
            self.mRestTime_Opend_UpdateRoomChat = ChatWindow.SPAN_UPDATE_ROOM_MESSAGE_UIOPEN;
            self.RequestChatLog(self.mCurrentChatType, false);
          }
          else if (self.mCurrentChatType == ChatWindow.eChatType.Guild && (double) self.mRestTime_Opend_UpdateGuildChat <= 0.0)
          {
            self.mRestTime_Opend_UpdateGuildChat = ChatWindow.SPAN_UPDATE_GUILD_MESSAGE_UIOPEN;
            self.RequestChatLog(self.mCurrentChatType, false);
          }
          else
          {
            self.mRestTime_Opend_UpdateWorldChat = Mathf.Max(0.0f, self.mRestTime_Opend_UpdateWorldChat - Time.deltaTime);
            self.mRestTime_Opend_UpdateRoomChat = Mathf.Max(0.0f, self.mRestTime_Opend_UpdateRoomChat - Time.deltaTime);
            self.mRestTime_Opend_UpdateGuildChat = Mathf.Max(0.0f, self.mRestTime_Opend_UpdateGuildChat - Time.deltaTime);
          }
        }
      }
    }

    private class State_WaitClosed : State<ChatWindow>
    {
      public override void Begin(ChatWindow self)
      {
        self.mRestTime_Closed_UpdateWorldChat = ChatWindow.SPAN_UPDATE_WORLD_MESSAGE_UICLOSE;
        self.mRestTime_Closed_UpdateRoomChat = ChatWindow.SPAN_UPDATE_ROOM_MESSAGE_UICLOSE;
        self.mRestTime_Closed_UpdateGuildChat = ChatWindow.SPAN_UPDATE_GUILD_MESSAGE_UICLOSE;
      }

      public override void Update(ChatWindow self)
      {
        if (self.IsOpened)
        {
          self.mState.GotoState<ChatWindow.State_WaitOpened>();
        }
        else
        {
          if (self.IsRequesting)
            return;
          if (self.mCurrentChatType == ChatWindow.eChatType.World && (double) self.mRestTime_Closed_UpdateWorldChat <= 0.0)
          {
            self.mRestTime_Closed_UpdateWorldChat = ChatWindow.SPAN_UPDATE_WORLD_MESSAGE_UICLOSE;
            self.RequestChatLog(self.mCurrentChatType, false);
          }
          else if (self.mCurrentChatType == ChatWindow.eChatType.Room && (double) self.mRestTime_Closed_UpdateRoomChat <= 0.0)
          {
            self.mRestTime_Closed_UpdateRoomChat = ChatWindow.SPAN_UPDATE_ROOM_MESSAGE_UICLOSE;
            self.RequestChatLog(self.mCurrentChatType, false);
          }
          else if (self.mCurrentChatType == ChatWindow.eChatType.Guild && (double) self.mRestTime_Closed_UpdateGuildChat <= 0.0)
          {
            self.mRestTime_Closed_UpdateGuildChat = ChatWindow.SPAN_UPDATE_GUILD_MESSAGE_UICLOSE;
            self.RequestChatLog(self.mCurrentChatType, false);
          }
          else
          {
            self.mRestTime_Closed_UpdateWorldChat = Mathf.Max(0.0f, self.mRestTime_Closed_UpdateWorldChat - Time.deltaTime);
            self.mRestTime_Closed_UpdateRoomChat = Mathf.Max(0.0f, self.mRestTime_Closed_UpdateRoomChat - Time.deltaTime);
            self.mRestTime_Closed_UpdateGuildChat = Mathf.Max(0.0f, self.mRestTime_Closed_UpdateGuildChat - Time.deltaTime);
          }
        }
      }
    }
  }
}
