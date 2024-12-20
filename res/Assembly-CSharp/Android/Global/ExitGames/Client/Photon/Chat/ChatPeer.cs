﻿// Decompiled with JetBrains decompiler
// Type: ExitGames.Client.Photon.Chat.ChatPeer
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace ExitGames.Client.Photon.Chat
{
  public class ChatPeer : PhotonPeer
  {
    private static readonly Dictionary<ConnectionProtocol, int> ProtocolToNameServerPort = new Dictionary<ConnectionProtocol, int>()
    {
      {
        ConnectionProtocol.Udp,
        5058
      },
      {
        ConnectionProtocol.Tcp,
        4533
      },
      {
        ConnectionProtocol.WebSocket,
        9093
      },
      {
        ConnectionProtocol.WebSocketSecure,
        19093
      }
    };
    public const string NameServerHost = "ns.exitgames.com";
    public const string NameServerHttp = "http://ns.exitgamescloud.com:80/photon/n";

    public ChatPeer(IPhotonPeerListener listener, ConnectionProtocol protocol)
      : base(listener, protocol)
    {
      this.ConfigUnitySockets();
    }

    public string NameServerAddress
    {
      get
      {
        return this.GetNameServerAddress();
      }
    }

    internal virtual bool IsProtocolSecure
    {
      get
      {
        return this.UsedProtocol == ConnectionProtocol.WebSocketSecure;
      }
    }

    [Conditional("UNITY")]
    private void ConfigUnitySockets()
    {
      Type type = Type.GetType("ExitGames.Client.Photon.SocketWebTcp, Assembly-CSharp", false);
      if ((object) type == null)
        type = Type.GetType("ExitGames.Client.Photon.SocketWebTcp, Assembly-CSharp-firstpass", false);
      if ((object) type == null)
        return;
      ((Dictionary<ConnectionProtocol, Type>) this.SocketImplementationConfig)[ConnectionProtocol.WebSocket] = type;
      ((Dictionary<ConnectionProtocol, Type>) this.SocketImplementationConfig)[ConnectionProtocol.WebSocketSecure] = type;
    }

    private string GetNameServerAddress()
    {
      int num = 0;
      ChatPeer.ProtocolToNameServerPort.TryGetValue(this.TransportProtocol, out num);
      switch (this.TransportProtocol)
      {
        case ConnectionProtocol.Udp:
        case ConnectionProtocol.Tcp:
          return string.Format("{0}:{1}", (object) "ns.exitgames.com", (object) num);
        case ConnectionProtocol.WebSocket:
          return string.Format("ws://{0}:{1}", (object) "ns.exitgames.com", (object) num);
        case ConnectionProtocol.WebSocketSecure:
          return string.Format("wss://{0}:{1}", (object) "ns.exitgames.com", (object) num);
        default:
          throw new ArgumentOutOfRangeException();
      }
    }

    public bool Connect()
    {
      if (this.DebugOut >= DebugLevel.INFO)
        this.Listener.DebugReturn(DebugLevel.INFO, "Connecting to nameserver " + this.NameServerAddress);
      return this.Connect(this.NameServerAddress, "NameServer");
    }

    public bool AuthenticateOnNameServer(string appId, string appVersion, string region, AuthenticationValues authValues)
    {
      if (this.DebugOut >= DebugLevel.INFO)
        this.Listener.DebugReturn(DebugLevel.INFO, "OpAuthenticate()");
      Dictionary<byte, object> dictionary = new Dictionary<byte, object>();
      dictionary[(byte) 220] = (object) appVersion;
      dictionary[(byte) 224] = (object) appId;
      dictionary[(byte) 210] = (object) region;
      if (authValues != null)
      {
        if (!string.IsNullOrEmpty(authValues.UserId))
          dictionary[(byte) 225] = (object) authValues.UserId;
        if (authValues != null && authValues.AuthType != CustomAuthenticationType.None)
        {
          dictionary[(byte) 217] = (object) authValues.AuthType;
          if (!string.IsNullOrEmpty(authValues.Token))
          {
            dictionary[(byte) 221] = (object) authValues.Token;
          }
          else
          {
            if (!string.IsNullOrEmpty(authValues.AuthGetParameters))
              dictionary[(byte) 216] = (object) authValues.AuthGetParameters;
            if (authValues.AuthPostData != null)
              dictionary[(byte) 214] = authValues.AuthPostData;
          }
        }
      }
      return this.OpCustom((byte) 230, dictionary, true, (byte) 0, this.IsEncryptionAvailable);
    }
  }
}
