// Decompiled with JetBrains decompiler
// Type: ExitGames.Client.Photon.SocketUdp
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using System;
using System.Net;
using System.Net.Sockets;
using System.Security;
using System.Threading;

namespace ExitGames.Client.Photon
{
  internal class SocketUdp : IPhotonSocket, IDisposable
  {
    private readonly object syncer = new object();
    private Socket sock;

    public SocketUdp(PeerBase npeer)
      : base(npeer)
    {
      if (this.ReportDebugOfLevel(DebugLevel.ALL))
        this.Listener.DebugReturn(DebugLevel.ALL, "CSharpSocket: UDP, Unity3d.");
      this.Protocol = ConnectionProtocol.Udp;
      this.PollReceive = false;
    }

    public void Dispose()
    {
      this.State = PhotonSocketState.Disconnecting;
      if (this.sock != null)
      {
        try
        {
          if (this.sock.Connected)
            this.sock.Close();
        }
        catch (Exception ex)
        {
          this.EnqueueDebugReturn(DebugLevel.INFO, "Exception in Dispose(): " + (object) ex);
        }
      }
      this.sock = (Socket) null;
      this.State = PhotonSocketState.Disconnected;
    }

    public override bool Connect()
    {
      lock (this.syncer)
      {
        if (!base.Connect())
          return false;
        this.State = PhotonSocketState.Connecting;
        new Thread(new ThreadStart(this.DnsAndConnect))
        {
          Name = "photon dns thread",
          IsBackground = true
        }.Start();
        return true;
      }
    }

    public override bool Disconnect()
    {
      if (this.ReportDebugOfLevel(DebugLevel.INFO))
        this.EnqueueDebugReturn(DebugLevel.INFO, "CSharpSocket.Disconnect()");
      this.State = PhotonSocketState.Disconnecting;
      lock (this.syncer)
      {
        if (this.sock != null)
        {
          try
          {
            this.sock.Close();
          }
          catch (Exception ex)
          {
            this.EnqueueDebugReturn(DebugLevel.INFO, "Exception in Disconnect(): " + (object) ex);
          }
          this.sock = (Socket) null;
        }
      }
      this.State = PhotonSocketState.Disconnected;
      return true;
    }

    public override PhotonSocketError Send(byte[] data, int length)
    {
      lock (this.syncer)
      {
        if (this.sock != null)
        {
          if (this.sock.Connected)
          {
            try
            {
              this.sock.Send(data, 0, length, SocketFlags.None);
              goto label_9;
            }
            catch (Exception ex)
            {
              if (this.ReportDebugOfLevel(DebugLevel.ERROR))
                this.EnqueueDebugReturn(DebugLevel.ERROR, "Cannot send to: " + this.ServerAddress + ". " + ex.Message);
              return PhotonSocketError.Exception;
            }
          }
        }
        return PhotonSocketError.Skipped;
      }
label_9:
      return PhotonSocketError.Success;
    }

    public override PhotonSocketError Receive(out byte[] data)
    {
      data = (byte[]) null;
      return PhotonSocketError.NoData;
    }

    internal void DnsAndConnect()
    {
      IPAddress address = (IPAddress) null;
      try
      {
        lock (this.syncer)
        {
          address = IPhotonSocket.GetIpAddress(this.ServerAddress);
          if (address == null)
            throw new ArgumentException("Invalid IPAddress. Address: " + this.ServerAddress);
          if (address.AddressFamily != AddressFamily.InterNetwork && address.AddressFamily != AddressFamily.InterNetworkV6)
            throw new ArgumentException("AddressFamily '" + (object) address.AddressFamily + "' not supported. Address: " + this.ServerAddress);
          this.sock = new Socket(address.AddressFamily, SocketType.Dgram, ProtocolType.Udp);
          this.sock.Connect(address, this.ServerPort);
          this.AddressResolvedAsIpv6 = address.AddressFamily == AddressFamily.InterNetworkV6;
          this.State = PhotonSocketState.Connected;
          this.peerBase.OnConnect();
        }
      }
      catch (SecurityException ex)
      {
        if (this.ReportDebugOfLevel(DebugLevel.ERROR))
          this.Listener.DebugReturn(DebugLevel.ERROR, "Connect() to '" + this.ServerAddress + "' (" + (address != null ? address.AddressFamily.ToString() : string.Empty) + ") failed: " + ex.ToString());
        this.HandleException(StatusCode.SecurityExceptionOnConnect);
        return;
      }
      catch (Exception ex)
      {
        if (this.ReportDebugOfLevel(DebugLevel.ERROR))
          this.Listener.DebugReturn(DebugLevel.ERROR, "Connect() to '" + this.ServerAddress + "' (" + (address != null ? address.AddressFamily.ToString() : string.Empty) + ") failed: " + ex.ToString());
        this.HandleException(StatusCode.ExceptionOnConnect);
        return;
      }
      new Thread(new ThreadStart(this.ReceiveLoop))
      {
        Name = "photon receive thread",
        IsBackground = true
      }.Start();
    }

    public void ReceiveLoop()
    {
      byte[] numArray = new byte[this.MTU];
      while (this.State == PhotonSocketState.Connected)
      {
        try
        {
          int length = this.sock.Receive(numArray);
          this.HandleReceivedDatagram(numArray, length, true);
        }
        catch (Exception ex)
        {
          if (this.State != PhotonSocketState.Disconnecting)
          {
            if (this.State != PhotonSocketState.Disconnected)
            {
              if (this.ReportDebugOfLevel(DebugLevel.ERROR))
                this.EnqueueDebugReturn(DebugLevel.ERROR, "Receive issue. State: " + (object) this.State + ". Server: '" + this.ServerAddress + "' Exception: " + (object) ex);
              this.HandleException(StatusCode.ExceptionOnReceive);
            }
          }
        }
      }
      this.Disconnect();
    }
  }
}
