// Decompiled with JetBrains decompiler
// Type: ExitGames.Client.Photon.Chat.ChatClient
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;
using System.Collections.Generic;

#nullable disable
namespace ExitGames.Client.Photon.Chat
{
  public class ChatClient : IPhotonPeerListener
  {
    private const int FriendRequestListMax = 1024;
    public const int DefaultMaxSubscribers = 100;
    private string chatRegion = "EU";
    public int MessageLimit;
    public readonly Dictionary<string, ChatChannel> PublicChannels;
    public readonly Dictionary<string, ChatChannel> PrivateChannels;
    private readonly HashSet<string> PublicChannelsUnsubscribing;
    private readonly IChatClientListener listener;
    public ChatPeer chatPeer;
    private const string ChatAppName = "chat";
    private bool didAuthenticate;
    private int msDeltaForServiceCalls = 50;
    private int msTimestampOfLastServiceCall;

    public ChatClient(IChatClientListener listener, ConnectionProtocol protocol = 0)
    {
      this.listener = listener;
      this.State = ChatState.Uninitialized;
      this.chatPeer = new ChatPeer((IPhotonPeerListener) this, protocol);
      this.PublicChannels = new Dictionary<string, ChatChannel>();
      this.PrivateChannels = new Dictionary<string, ChatChannel>();
      this.PublicChannelsUnsubscribing = new HashSet<string>();
    }

    public string NameServerAddress { get; private set; }

    public string FrontendAddress { get; private set; }

    public string ChatRegion
    {
      get => this.chatRegion;
      set => this.chatRegion = value;
    }

    public ChatState State { get; private set; }

    public ChatDisconnectCause DisconnectedCause { get; private set; }

    public bool CanChat => this.State == ChatState.ConnectedToFrontEnd && this.HasPeer;

    public bool CanChatInChannel(string channelName)
    {
      return this.CanChat && this.PublicChannels.ContainsKey(channelName) && !this.PublicChannelsUnsubscribing.Contains(channelName);
    }

    private bool HasPeer => this.chatPeer != null;

    public string AppVersion { get; private set; }

    public string AppId { get; private set; }

    public AuthenticationValues AuthValues { get; set; }

    public string UserId
    {
      get => this.AuthValues != null ? this.AuthValues.UserId : (string) null;
      private set
      {
        if (this.AuthValues == null)
          this.AuthValues = new AuthenticationValues();
        this.AuthValues.UserId = value;
      }
    }

    public bool UseBackgroundWorkerForSending { get; set; }

    public ConnectionProtocol TransportProtocol
    {
      get => this.chatPeer.TransportProtocol;
      set
      {
        if (this.chatPeer == null || this.chatPeer.PeerState != null)
          this.listener.DebugReturn((DebugLevel) 2, "Can't set TransportProtocol. Disconnect first! " + (this.chatPeer == null ? "The chatPeer is null." : "PeerState: " + (object) this.chatPeer.PeerState));
        else
          this.chatPeer.TransportProtocol = value;
      }
    }

    public Dictionary<ConnectionProtocol, Type> SocketImplementationConfig
    {
      get => this.chatPeer.SocketImplementationConfig;
    }

    public bool Connect(string appId, string appVersion, AuthenticationValues authValues)
    {
      this.chatPeer.TimePingInterval = 3000;
      this.DisconnectedCause = ChatDisconnectCause.None;
      if (authValues != null)
      {
        this.AuthValues = authValues;
        if (string.IsNullOrEmpty(this.AuthValues.UserId))
        {
          if (this.DebugOut >= 1)
            this.listener.DebugReturn((DebugLevel) 1, "Connect failed: no UserId specified in authentication values.");
          return false;
        }
        this.AppId = appId;
        this.AppVersion = appVersion;
        this.didAuthenticate = false;
        this.chatPeer.QuickResendAttempts = (byte) 2;
        this.chatPeer.SentCountAllowance = 7;
        this.PublicChannels.Clear();
        this.PrivateChannels.Clear();
        this.PublicChannelsUnsubscribing.Clear();
        this.NameServerAddress = this.chatPeer.NameServerAddress;
        bool flag = this.chatPeer.Connect();
        if (flag)
          this.State = ChatState.ConnectingToNameServer;
        if (this.UseBackgroundWorkerForSending)
        {
          int num = (int) SupportClass.StartBackgroundCalls(new Func<bool>(this.SendOutgoingInBackground), this.msDeltaForServiceCalls, "ChatClient Service Thread");
        }
        return flag;
      }
      if (this.DebugOut >= 1)
        this.listener.DebugReturn((DebugLevel) 1, "Connect failed: no authentication values specified");
      return false;
    }

    public void Service()
    {
      do
        ;
      while (this.HasPeer && this.chatPeer.DispatchIncomingCommands());
      if (this.UseBackgroundWorkerForSending || Environment.TickCount - this.msTimestampOfLastServiceCall <= this.msDeltaForServiceCalls && this.msTimestampOfLastServiceCall != 0)
        return;
      this.msTimestampOfLastServiceCall = Environment.TickCount;
      do
        ;
      while (this.HasPeer && this.chatPeer.SendOutgoingCommands());
    }

    private bool SendOutgoingInBackground()
    {
      do
        ;
      while (this.HasPeer && this.chatPeer.SendOutgoingCommands());
      return this.State != ChatState.Disconnected;
    }

    [Obsolete("Better use UseBackgroundWorkerForSending and Service().")]
    public void SendAcksOnly()
    {
      if (!this.HasPeer)
        return;
      this.chatPeer.SendAcksOnly();
    }

    public void Disconnect()
    {
      if (!this.HasPeer || this.chatPeer.PeerState == null)
        return;
      this.chatPeer.Disconnect();
    }

    public void StopThread()
    {
      if (!this.HasPeer)
        return;
      this.chatPeer.StopThread();
    }

    public bool Subscribe(string[] channels) => this.Subscribe(channels, 0);

    public bool Subscribe(string[] channels, int messagesFromHistory)
    {
      if (!this.CanChat)
      {
        if (this.DebugOut >= 1)
          this.listener.DebugReturn((DebugLevel) 1, "Subscribe called while not connected to front end server.");
        return false;
      }
      if (channels != null && channels.Length != 0)
        return this.SendChannelOperation(channels, (byte) 0, messagesFromHistory);
      if (this.DebugOut >= 2)
        this.listener.DebugReturn((DebugLevel) 2, "Subscribe can't be called for empty or null channels-list.");
      return false;
    }

    public bool Subscribe(string[] channels, int[] lastMsgIds)
    {
      if (!this.CanChat)
      {
        if (this.DebugOut >= 1)
          this.listener.DebugReturn((DebugLevel) 1, "Subscribe called while not connected to front end server.");
        return false;
      }
      if (channels == null || channels.Length == 0)
      {
        if (this.DebugOut >= 2)
          this.listener.DebugReturn((DebugLevel) 2, "Subscribe can't be called for empty or null channels-list.");
        return false;
      }
      for (int index = 0; index < channels.Length; ++index)
      {
        if (string.IsNullOrEmpty(channels[index]))
        {
          if (this.DebugOut >= 1)
            this.listener.DebugReturn((DebugLevel) 1, string.Format("Subscribe can't be called with a null or empty channel name at index {0}.", (object) index));
          return false;
        }
      }
      if (lastMsgIds == null || lastMsgIds.Length != channels.Length)
      {
        if (this.DebugOut >= 1)
          this.listener.DebugReturn((DebugLevel) 1, "Subscribe can't be called when \"lastMsgIds\" array is null or does not have the same length as \"channels\" array.");
        return false;
      }
      return this.chatPeer.SendOperation((byte) 0, new Dictionary<byte, object>()
      {
        {
          (byte) 0,
          (object) channels
        },
        {
          (byte) 9,
          (object) lastMsgIds
        },
        {
          (byte) 14,
          (object) -1
        }
      }, SendOptions.SendReliable);
    }

    public bool Unsubscribe(string[] channels)
    {
      if (!this.CanChat)
      {
        if (this.DebugOut >= 1)
          this.listener.DebugReturn((DebugLevel) 1, "Unsubscribe called while not connected to front end server.");
        return false;
      }
      if (channels == null || channels.Length == 0)
      {
        if (this.DebugOut >= 2)
          this.listener.DebugReturn((DebugLevel) 2, "Unsubscribe can't be called for empty or null channels-list.");
        return false;
      }
      foreach (string channel in channels)
        this.PublicChannelsUnsubscribing.Add(channel);
      return this.SendChannelOperation(channels, (byte) 1, 0);
    }

    public bool PublishMessage(string channelName, object message, bool forwardAsWebhook = false)
    {
      return this.publishMessage(channelName, message, true, forwardAsWebhook);
    }

    internal bool PublishMessageUnreliable(
      string channelName,
      object message,
      bool forwardAsWebhook = false)
    {
      return this.publishMessage(channelName, message, false, forwardAsWebhook);
    }

    private bool publishMessage(
      string channelName,
      object message,
      bool reliable,
      bool forwardAsWebhook = false)
    {
      if (!this.CanChat)
      {
        if (this.DebugOut >= 1)
          this.listener.DebugReturn((DebugLevel) 1, "PublishMessage called while not connected to front end server.");
        return false;
      }
      if (string.IsNullOrEmpty(channelName) || message == null)
      {
        if (this.DebugOut >= 2)
          this.listener.DebugReturn((DebugLevel) 2, "PublishMessage parameters must be non-null and not empty.");
        return false;
      }
      Dictionary<byte, object> dictionary1 = new Dictionary<byte, object>()
      {
        {
          (byte) 1,
          (object) channelName
        },
        {
          (byte) 3,
          message
        }
      };
      if (forwardAsWebhook)
        dictionary1.Add((byte) 21, (object) (byte) 1);
      ChatPeer chatPeer = this.chatPeer;
      Dictionary<byte, object> dictionary2 = dictionary1;
      SendOptions sendOptions1 = new SendOptions();
      ((SendOptions) ref sendOptions1).Reliability = reliable;
      SendOptions sendOptions2 = sendOptions1;
      return chatPeer.SendOperation((byte) 2, dictionary2, sendOptions2);
    }

    public bool SendPrivateMessage(string target, object message, bool forwardAsWebhook = false)
    {
      return this.SendPrivateMessage(target, message, false, forwardAsWebhook);
    }

    public bool SendPrivateMessage(
      string target,
      object message,
      bool encrypt,
      bool forwardAsWebhook)
    {
      return this.sendPrivateMessage(target, message, encrypt, true, forwardAsWebhook);
    }

    internal bool SendPrivateMessageUnreliable(
      string target,
      object message,
      bool encrypt,
      bool forwardAsWebhook = false)
    {
      return this.sendPrivateMessage(target, message, encrypt, false, forwardAsWebhook);
    }

    private bool sendPrivateMessage(
      string target,
      object message,
      bool encrypt,
      bool reliable,
      bool forwardAsWebhook = false)
    {
      if (!this.CanChat)
      {
        if (this.DebugOut >= 1)
          this.listener.DebugReturn((DebugLevel) 1, "SendPrivateMessage called while not connected to front end server.");
        return false;
      }
      if (string.IsNullOrEmpty(target) || message == null)
      {
        if (this.DebugOut >= 2)
          this.listener.DebugReturn((DebugLevel) 2, "SendPrivateMessage parameters must be non-null and not empty.");
        return false;
      }
      Dictionary<byte, object> dictionary = new Dictionary<byte, object>()
      {
        {
          (byte) 225,
          (object) target
        },
        {
          (byte) 3,
          message
        }
      };
      if (forwardAsWebhook)
        dictionary.Add((byte) 21, (object) (byte) 1);
      SendOptions sendOptions1 = new SendOptions();
      ((SendOptions) ref sendOptions1).Reliability = reliable;
      sendOptions1.Channel = (byte) 0;
      sendOptions1.Encrypt = encrypt;
      SendOptions sendOptions2 = sendOptions1;
      return this.chatPeer.SendOperation((byte) 3, dictionary, sendOptions2);
    }

    private bool SetOnlineStatus(int status, object message, bool skipMessage)
    {
      if (!this.CanChat)
      {
        if (this.DebugOut >= 1)
          this.listener.DebugReturn((DebugLevel) 1, "SetOnlineStatus called while not connected to front end server.");
        return false;
      }
      Dictionary<byte, object> dictionary = new Dictionary<byte, object>()
      {
        {
          (byte) 10,
          (object) status
        }
      };
      if (skipMessage)
        dictionary[(byte) 12] = (object) true;
      else
        dictionary[(byte) 3] = message;
      return this.chatPeer.SendOperation((byte) 5, dictionary, SendOptions.SendReliable);
    }

    public bool SetOnlineStatus(int status) => this.SetOnlineStatus(status, (object) null, true);

    public bool SetOnlineStatus(int status, object message)
    {
      return this.SetOnlineStatus(status, message, false);
    }

    public bool AddFriends(string[] friends)
    {
      if (!this.CanChat)
      {
        if (this.DebugOut >= 1)
          this.listener.DebugReturn((DebugLevel) 1, "AddFriends called while not connected to front end server.");
        return false;
      }
      if (friends == null || friends.Length == 0)
      {
        if (this.DebugOut >= 2)
          this.listener.DebugReturn((DebugLevel) 2, "AddFriends can't be called for empty or null list.");
        return false;
      }
      if (friends.Length > 1024)
      {
        if (this.DebugOut >= 2)
          this.listener.DebugReturn((DebugLevel) 2, "AddFriends max list size exceeded: " + (object) friends.Length + " > " + (object) 1024);
        return false;
      }
      return this.chatPeer.SendOperation((byte) 6, new Dictionary<byte, object>()
      {
        {
          (byte) 11,
          (object) friends
        }
      }, SendOptions.SendReliable);
    }

    public bool RemoveFriends(string[] friends)
    {
      if (!this.CanChat)
      {
        if (this.DebugOut >= 1)
          this.listener.DebugReturn((DebugLevel) 1, "RemoveFriends called while not connected to front end server.");
        return false;
      }
      if (friends == null || friends.Length == 0)
      {
        if (this.DebugOut >= 2)
          this.listener.DebugReturn((DebugLevel) 2, "RemoveFriends can't be called for empty or null list.");
        return false;
      }
      if (friends.Length > 1024)
      {
        if (this.DebugOut >= 2)
          this.listener.DebugReturn((DebugLevel) 2, "RemoveFriends max list size exceeded: " + (object) friends.Length + " > " + (object) 1024);
        return false;
      }
      return this.chatPeer.SendOperation((byte) 7, new Dictionary<byte, object>()
      {
        {
          (byte) 11,
          (object) friends
        }
      }, SendOptions.SendReliable);
    }

    public string GetPrivateChannelNameByUser(string userName)
    {
      return string.Format("{0}:{1}", (object) this.UserId, (object) userName);
    }

    public bool TryGetChannel(string channelName, bool isPrivate, out ChatChannel channel)
    {
      return !isPrivate ? this.PublicChannels.TryGetValue(channelName, out channel) : this.PrivateChannels.TryGetValue(channelName, out channel);
    }

    public bool TryGetChannel(string channelName, out ChatChannel channel)
    {
      return this.PublicChannels.TryGetValue(channelName, out channel) || this.PrivateChannels.TryGetValue(channelName, out channel);
    }

    public DebugLevel DebugOut
    {
      set => this.chatPeer.DebugOut = value;
      get => this.chatPeer.DebugOut;
    }

    void IPhotonPeerListener.DebugReturn(DebugLevel level, string message)
    {
      this.listener.DebugReturn(level, message);
    }

    void IPhotonPeerListener.OnEvent(EventData eventData)
    {
      switch (eventData.Code)
      {
        case 0:
          this.HandleChatMessagesEvent(eventData);
          break;
        case 2:
          this.HandlePrivateMessageEvent(eventData);
          break;
        case 4:
          this.HandleStatusUpdate(eventData);
          break;
        case 5:
          this.HandleSubscribeEvent(eventData);
          break;
        case 6:
          this.HandleUnsubscribeEvent(eventData);
          break;
        case 8:
          this.HandleUserSubscribedEvent(eventData);
          break;
        case 9:
          this.HandleUserUnsubscribedEvent(eventData);
          break;
      }
    }

    void IPhotonPeerListener.OnOperationResponse(OperationResponse operationResponse)
    {
      switch (operationResponse.OperationCode)
      {
        case 230:
          this.HandleAuthResponse(operationResponse);
          break;
        default:
          if (operationResponse.ReturnCode == (short) 0 || this.DebugOut < 1)
            break;
          if (operationResponse.ReturnCode == (short) -2)
          {
            this.listener.DebugReturn((DebugLevel) 1, string.Format("Chat Operation {0} unknown on server. Check your AppId and make sure it's for a Chat application.", (object) operationResponse.OperationCode));
            break;
          }
          this.listener.DebugReturn((DebugLevel) 1, string.Format("Chat Operation {0} failed (Code: {1}). Debug Message: {2}", (object) operationResponse.OperationCode, (object) operationResponse.ReturnCode, (object) operationResponse.DebugMessage));
          break;
      }
    }

    void IPhotonPeerListener.OnStatusChanged(StatusCode statusCode)
    {
      if (statusCode != 1024)
      {
        if (statusCode != 1025)
        {
          if (statusCode != 1048)
          {
            if (statusCode != 1049)
              return;
            this.State = ChatState.Disconnecting;
            this.chatPeer.Disconnect();
          }
          else
          {
            if (this.didAuthenticate)
              return;
            this.didAuthenticate = this.chatPeer.AuthenticateOnNameServer(this.AppId, this.AppVersion, this.chatRegion, this.AuthValues);
            if (this.didAuthenticate || this.DebugOut < 1)
              return;
            ((IPhotonPeerListener) this).DebugReturn((DebugLevel) 1, "Error calling OpAuthenticate! Did not work. Check log output, AuthValues and if you're connected. State: " + (object) this.State);
          }
        }
        else if (this.State == ChatState.Authenticated)
        {
          this.ConnectToFrontEnd();
        }
        else
        {
          this.State = ChatState.Disconnected;
          this.listener.OnChatStateChange(ChatState.Disconnected);
          this.listener.OnDisconnected();
        }
      }
      else
      {
        if (!this.chatPeer.IsProtocolSecure)
          this.chatPeer.EstablishEncryption();
        else if (!this.didAuthenticate)
        {
          this.didAuthenticate = this.chatPeer.AuthenticateOnNameServer(this.AppId, this.AppVersion, this.chatRegion, this.AuthValues);
          if (!this.didAuthenticate && this.DebugOut >= 1)
            ((IPhotonPeerListener) this).DebugReturn((DebugLevel) 1, "Error calling OpAuthenticate! Did not work. Check log output, AuthValues and if you're connected. State: " + (object) this.State);
        }
        if (this.State == ChatState.ConnectingToNameServer)
        {
          this.State = ChatState.ConnectedToNameServer;
          this.listener.OnChatStateChange(this.State);
        }
        else
        {
          if (this.State != ChatState.ConnectingToFrontEnd)
            return;
          this.AuthenticateOnFrontEnd();
        }
      }
    }

    private bool SendChannelOperation(string[] channels, byte operation, int historyLength)
    {
      Dictionary<byte, object> dictionary = new Dictionary<byte, object>()
      {
        {
          (byte) 0,
          (object) channels
        }
      };
      if (historyLength != 0)
        dictionary.Add((byte) 14, (object) historyLength);
      return this.chatPeer.SendOperation(operation, dictionary, SendOptions.SendReliable);
    }

    private void HandlePrivateMessageEvent(EventData eventData)
    {
      object parameter1 = eventData.Parameters[(byte) 3];
      string parameter2 = (string) eventData.Parameters[(byte) 5];
      int parameter3 = (int) eventData.Parameters[(byte) 8];
      string str = this.UserId == null || !this.UserId.Equals(parameter2) ? this.GetPrivateChannelNameByUser(parameter2) : this.GetPrivateChannelNameByUser((string) eventData.Parameters[(byte) 225]);
      ChatChannel chatChannel;
      if (!this.PrivateChannels.TryGetValue(str, out chatChannel))
      {
        chatChannel = new ChatChannel(str);
        chatChannel.IsPrivate = true;
        chatChannel.MessageLimit = this.MessageLimit;
        this.PrivateChannels.Add(chatChannel.Name, chatChannel);
      }
      chatChannel.Add(parameter2, parameter1, parameter3);
      this.listener.OnPrivateMessage(parameter2, parameter1, str);
    }

    private void HandleChatMessagesEvent(EventData eventData)
    {
      object[] parameter1 = (object[]) eventData.Parameters[(byte) 2];
      string[] parameter2 = (string[]) eventData.Parameters[(byte) 4];
      string parameter3 = (string) eventData.Parameters[(byte) 1];
      int parameter4 = (int) eventData.Parameters[(byte) 8];
      ChatChannel chatChannel;
      if (!this.PublicChannels.TryGetValue(parameter3, out chatChannel))
      {
        if (this.DebugOut < 2)
          return;
        this.listener.DebugReturn((DebugLevel) 2, "Channel " + parameter3 + " for incoming message event not found.");
      }
      else
      {
        chatChannel.Add(parameter2, parameter1, parameter4);
        this.listener.OnGetMessages(parameter3, parameter2, parameter1);
      }
    }

    private void HandleSubscribeEvent(EventData eventData)
    {
      string[] parameter1 = (string[]) eventData.Parameters[(byte) 0];
      bool[] parameter2 = (bool[]) eventData.Parameters[(byte) 15];
      object obj;
      if (eventData.Parameters.TryGetValue((byte) 22, out obj))
      {
        Dictionary<object, object> newProperties = obj as Dictionary<object, object>;
        if (parameter1.Length == 1)
        {
          if (parameter2[0])
          {
            string str = parameter1[0];
            ChatChannel chatChannel;
            if (this.PublicChannels.TryGetValue(str, out chatChannel))
            {
              chatChannel.Subscribers.Clear();
              chatChannel.ClearProperties();
            }
            else
            {
              chatChannel = new ChatChannel(str);
              chatChannel.MessageLimit = this.MessageLimit;
              this.PublicChannels.Add(chatChannel.Name, chatChannel);
            }
            chatChannel.ReadProperties(newProperties);
            if (eventData.Parameters.TryGetValue((byte) 23, out obj))
            {
              string[] users = obj as string[];
              chatChannel.Subscribers.Add(this.UserId);
              chatChannel.AddSubscribers(users);
            }
          }
          this.listener.OnSubscribed(parameter1, parameter2);
          return;
        }
        this.listener.DebugReturn((DebugLevel) 1, "Unexpected: Subscribe event for multiple channels with channels properties returned. Ignoring properties.");
      }
      for (int index = 0; index < parameter1.Length; ++index)
      {
        if (parameter2[index])
        {
          string str = parameter1[index];
          ChatChannel chatChannel;
          if (!this.PublicChannels.TryGetValue(str, out chatChannel))
          {
            chatChannel = new ChatChannel(str);
            chatChannel.MessageLimit = this.MessageLimit;
            this.PublicChannels.Add(chatChannel.Name, chatChannel);
          }
        }
      }
      this.listener.OnSubscribed(parameter1, parameter2);
    }

    private void HandleUnsubscribeEvent(EventData eventData)
    {
      string[] channels = (string[]) eventData[(byte) 0];
      for (int index = 0; index < channels.Length; ++index)
      {
        string key = channels[index];
        this.PublicChannels.Remove(key);
        this.PublicChannelsUnsubscribing.Remove(key);
      }
      this.listener.OnUnsubscribed(channels);
    }

    private void HandleAuthResponse(OperationResponse operationResponse)
    {
      if (this.DebugOut >= 3)
        this.listener.DebugReturn((DebugLevel) 3, operationResponse.ToStringFull() + " on: " + this.chatPeer.NameServerAddress);
      if (operationResponse.ReturnCode == (short) 0)
      {
        if (this.State == ChatState.ConnectedToNameServer)
        {
          this.State = ChatState.Authenticated;
          this.listener.OnChatStateChange(this.State);
          if (operationResponse.Parameters.ContainsKey((byte) 221))
          {
            if (this.AuthValues == null)
              this.AuthValues = new AuthenticationValues();
            this.AuthValues.Token = operationResponse[(byte) 221] as string;
            this.FrontendAddress = (string) operationResponse[(byte) 230];
            this.chatPeer.Disconnect();
          }
          else
          {
            if (this.DebugOut < 1)
              return;
            this.listener.DebugReturn((DebugLevel) 1, "No secret in authentication response.");
          }
        }
        else
        {
          if (this.State != ChatState.ConnectingToFrontEnd)
            return;
          this.State = ChatState.ConnectedToFrontEnd;
          this.listener.OnChatStateChange(this.State);
          this.listener.OnConnected();
        }
      }
      else
      {
        switch (operationResponse.ReturnCode)
        {
          case -3:
            this.DisconnectedCause = ChatDisconnectCause.OperationNotAllowedInCurrentState;
            break;
          case 32755:
            this.DisconnectedCause = ChatDisconnectCause.CustomAuthenticationFailed;
            break;
          case 32756:
            this.DisconnectedCause = ChatDisconnectCause.InvalidRegion;
            break;
          case 32757:
            this.DisconnectedCause = ChatDisconnectCause.MaxCcuReached;
            break;
          case short.MaxValue:
            this.DisconnectedCause = ChatDisconnectCause.InvalidAuthentication;
            break;
        }
        if (this.DebugOut >= 1)
          this.listener.DebugReturn((DebugLevel) 1, "Authentication request error: " + (object) operationResponse.ReturnCode + ". Disconnecting.");
        this.State = ChatState.Disconnecting;
        this.chatPeer.Disconnect();
      }
    }

    private void HandleStatusUpdate(EventData eventData)
    {
      string parameter1 = (string) eventData.Parameters[(byte) 5];
      int parameter2 = (int) eventData.Parameters[(byte) 10];
      object message = (object) null;
      bool gotMessage = eventData.Parameters.ContainsKey((byte) 3);
      if (gotMessage)
        message = eventData.Parameters[(byte) 3];
      this.listener.OnStatusUpdate(parameter1, parameter2, gotMessage, message);
    }

    private void ConnectToFrontEnd()
    {
      this.State = ChatState.ConnectingToFrontEnd;
      if (this.DebugOut >= 3)
        this.listener.DebugReturn((DebugLevel) 3, "Connecting to frontend " + this.FrontendAddress);
      this.chatPeer.Connect(this.FrontendAddress, "chat");
    }

    private bool AuthenticateOnFrontEnd()
    {
      if (this.AuthValues != null)
      {
        if (string.IsNullOrEmpty(this.AuthValues.Token))
        {
          if (this.DebugOut >= 1)
            this.listener.DebugReturn((DebugLevel) 1, "Can't authenticate on front end server. Secret is not set");
          return false;
        }
        return this.chatPeer.SendOperation((byte) 230, new Dictionary<byte, object>()
        {
          {
            (byte) 221,
            (object) this.AuthValues.Token
          }
        }, SendOptions.SendReliable);
      }
      if (this.DebugOut >= 1)
        this.listener.DebugReturn((DebugLevel) 1, "Can't authenticate on front end server. Authentication Values are not set");
      return false;
    }

    private void HandleUserUnsubscribedEvent(EventData eventData)
    {
      string parameter1 = eventData.Parameters[(byte) 1] as string;
      string parameter2 = eventData.Parameters[(byte) 225] as string;
      ChatChannel chatChannel;
      if (this.PublicChannels.TryGetValue(parameter1, out chatChannel))
      {
        if (!chatChannel.PublishSubscribers && this.DebugOut >= 2)
          this.listener.DebugReturn((DebugLevel) 2, string.Format("Channel \"{0}\" for incoming UserUnsubscribed (\"{1}\") event does not have PublishSubscribers enabled.", (object) parameter1, (object) parameter2));
        if (!chatChannel.Subscribers.Remove(parameter2) && this.DebugOut >= 2)
          this.listener.DebugReturn((DebugLevel) 2, string.Format("Channel \"{0}\" does not contain unsubscribed user \"{1}\".", (object) parameter1, (object) parameter2));
      }
      else if (this.DebugOut >= 2)
        this.listener.DebugReturn((DebugLevel) 2, string.Format("Channel \"{0}\" not found for incoming UserUnsubscribed (\"{1}\") event.", (object) parameter1, (object) parameter2));
      this.listener.OnUserUnsubscribed(parameter1, parameter2);
    }

    private void HandleUserSubscribedEvent(EventData eventData)
    {
      string parameter1 = eventData.Parameters[(byte) 1] as string;
      string parameter2 = eventData.Parameters[(byte) 225] as string;
      ChatChannel chatChannel;
      if (this.PublicChannels.TryGetValue(parameter1, out chatChannel))
      {
        if (!chatChannel.PublishSubscribers && this.DebugOut >= 2)
          this.listener.DebugReturn((DebugLevel) 2, string.Format("Channel \"{0}\" for incoming UserSubscribed (\"{1}\") event does not have PublishSubscribers enabled.", (object) parameter1, (object) parameter2));
        if (!chatChannel.Subscribers.Add(parameter2))
        {
          if (this.DebugOut >= 2)
            this.listener.DebugReturn((DebugLevel) 2, string.Format("Channel \"{0}\" already contains newly subscribed user \"{1}\".", (object) parameter1, (object) parameter2));
        }
        else if (chatChannel.MaxSubscribers > 0 && chatChannel.Subscribers.Count > chatChannel.MaxSubscribers && this.DebugOut >= 2)
          this.listener.DebugReturn((DebugLevel) 2, string.Format("Channel \"{0}\"'s MaxSubscribers exceeded. count={1} > MaxSubscribers={2}.", (object) parameter1, (object) chatChannel.Subscribers.Count, (object) chatChannel.MaxSubscribers));
      }
      else if (this.DebugOut >= 2)
        this.listener.DebugReturn((DebugLevel) 2, string.Format("Channel \"{0}\" not found for incoming UserSubscribed (\"{1}\") event.", (object) parameter1, (object) parameter2));
      this.listener.OnUserSubscribed(parameter1, parameter2);
    }

    public bool Subscribe(
      string channel,
      int lastMsgId = 0,
      int messagesFromHistory = -1,
      ChannelCreationOptions creationOptions = null)
    {
      if (creationOptions == null)
        creationOptions = ChannelCreationOptions.Default;
      int maxSubscribers = creationOptions.MaxSubscribers;
      bool publishSubscribers = creationOptions.PublishSubscribers;
      if (maxSubscribers < 0)
      {
        if (this.DebugOut >= 1)
          this.listener.DebugReturn((DebugLevel) 1, "Cannot set MaxSubscribers < 0.");
        return false;
      }
      if (lastMsgId < 0)
      {
        if (this.DebugOut >= 1)
          this.listener.DebugReturn((DebugLevel) 1, "lastMsgId cannot be < 0.");
        return false;
      }
      if (messagesFromHistory < -1)
      {
        if (this.DebugOut >= 2)
          this.listener.DebugReturn((DebugLevel) 2, "messagesFromHistory < -1, setting it to -1");
        messagesFromHistory = -1;
      }
      if (lastMsgId > 0 && messagesFromHistory == 0)
      {
        if (this.DebugOut >= 2)
          this.listener.DebugReturn((DebugLevel) 2, "lastMsgId will be ignored because messagesFromHistory == 0");
        lastMsgId = 0;
      }
      Dictionary<object, object> dictionary1 = (Dictionary<object, object>) null;
      if (publishSubscribers)
      {
        if (maxSubscribers > 100)
        {
          if (this.DebugOut >= 1)
            this.listener.DebugReturn((DebugLevel) 1, string.Format("Cannot set MaxSubscribers > {0} when PublishSubscribers == true.", (object) 100));
          return false;
        }
        dictionary1 = new Dictionary<object, object>();
        dictionary1[(object) (byte) 254] = (object) true;
      }
      if (maxSubscribers > 0)
      {
        if (dictionary1 == null)
          dictionary1 = new Dictionary<object, object>();
        dictionary1[(object) byte.MaxValue] = (object) maxSubscribers;
      }
      Dictionary<byte, object> dictionary2 = new Dictionary<byte, object>()
      {
        {
          (byte) 0,
          (object) new string[1]{ channel }
        }
      };
      if (messagesFromHistory != 0)
        dictionary2.Add((byte) 14, (object) messagesFromHistory);
      if (lastMsgId > 0)
        dictionary2.Add((byte) 9, (object) new int[1]
        {
          lastMsgId
        });
      if (dictionary1 != null && dictionary1.Count > 0)
        dictionary2.Add((byte) 22, (object) dictionary1);
      return this.chatPeer.SendOperation((byte) 0, dictionary2, SendOptions.SendReliable);
    }
  }
}
