// Decompiled with JetBrains decompiler
// Type: ExitGames.Client.Photon.Chat.ChatPeer
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;
using System.Collections.Generic;
using System.Diagnostics;

#nullable disable
namespace ExitGames.Client.Photon.Chat
{
  public class ChatPeer : PhotonPeer
  {
    public const string NameServerHost = "ns.exitgames.com";
    public const string NameServerHttp = "http://ns.exitgamescloud.com:80/photon/n";
    private static readonly Dictionary<ConnectionProtocol, int> ProtocolToNameServerPort = new Dictionary<ConnectionProtocol, int>()
    {
      {
        (ConnectionProtocol) 0,
        5058
      },
      {
        (ConnectionProtocol) 1,
        4533
      },
      {
        (ConnectionProtocol) 4,
        9093
      },
      {
        (ConnectionProtocol) 5,
        19093
      }
    };

    public ChatPeer(IPhotonPeerListener listener, ConnectionProtocol protocol)
      : base(listener, protocol)
    {
      this.ConfigUnitySockets();
    }

    public string NameServerAddress => this.GetNameServerAddress();

    internal virtual bool IsProtocolSecure => this.UsedProtocol == 5;

    [Conditional("UNITY")]
    private void ConfigUnitySockets()
    {
      Type type = Type.GetType("ExitGames.Client.Photon.SocketWebTcp, Assembly-CSharp", false);
      if ((object) type == null)
        type = Type.GetType("ExitGames.Client.Photon.SocketWebTcp, Assembly-CSharp-firstpass", false);
      if ((object) type == null)
        return;
      this.SocketImplementationConfig[(ConnectionProtocol) 4] = type;
      this.SocketImplementationConfig[(ConnectionProtocol) 5] = type;
    }

    private string GetNameServerAddress()
    {
      int num = 0;
      ChatPeer.ProtocolToNameServerPort.TryGetValue(this.TransportProtocol, out num);
      switch ((int) this.TransportProtocol)
      {
        case 0:
        case 1:
          return string.Format("{0}:{1}", (object) "ns.exitgames.com", (object) num);
        case 4:
          return string.Format("ws://{0}:{1}", (object) "ns.exitgames.com", (object) num);
        case 5:
          return string.Format("wss://{0}:{1}", (object) "ns.exitgames.com", (object) num);
        default:
          throw new ArgumentOutOfRangeException();
      }
    }

    public bool Connect()
    {
      if (this.DebugOut >= 3)
        this.Listener.DebugReturn((DebugLevel) 3, "Connecting to nameserver " + this.NameServerAddress);
      return this.Connect(this.NameServerAddress, "NameServer");
    }

    public bool AuthenticateOnNameServer(
      string appId,
      string appVersion,
      string region,
      AuthenticationValues authValues)
    {
      if (this.DebugOut >= 3)
        this.Listener.DebugReturn((DebugLevel) 3, "OpAuthenticate()");
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
          dictionary[(byte) 217] = (object) (byte) authValues.AuthType;
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
      SendOptions sendOptions1 = new SendOptions();
      ((SendOptions) ref sendOptions1).Reliability = true;
      sendOptions1.Channel = (byte) 0;
      sendOptions1.Encrypt = this.IsEncryptionAvailable;
      SendOptions sendOptions2 = sendOptions1;
      return this.SendOperation((byte) 230, dictionary, sendOptions2);
    }
  }
}
