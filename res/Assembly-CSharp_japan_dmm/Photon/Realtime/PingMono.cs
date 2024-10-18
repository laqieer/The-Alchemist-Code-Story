// Decompiled with JetBrains decompiler
// Type: Photon.Realtime.PingMono
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;
using System.Net.Sockets;

#nullable disable
namespace Photon.Realtime
{
  public class PingMono : PhotonPing
  {
    private Socket sock;

    public override bool StartPing(string ip)
    {
      this.Init();
      try
      {
        this.sock = !ip.Contains(".") ? new Socket(AddressFamily.InterNetworkV6, SocketType.Dgram, ProtocolType.Udp) : new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
        this.sock.ReceiveTimeout = 5000;
        this.sock.Connect(ip, 5055);
        this.PingBytes[this.PingBytes.Length - 1] = this.PingId;
        this.sock.Send(this.PingBytes);
        this.PingBytes[this.PingBytes.Length - 1] = (byte) ((uint) this.PingId - 1U);
      }
      catch (Exception ex)
      {
        this.sock = (Socket) null;
        Console.WriteLine((object) ex);
      }
      return false;
    }

    public override bool Done()
    {
      if (this.GotResult || this.sock == null)
        return true;
      if (this.sock.Available <= 0)
        return false;
      int num = this.sock.Receive(this.PingBytes, SocketFlags.None);
      if ((int) this.PingBytes[this.PingBytes.Length - 1] != (int) this.PingId || num != this.PingLength)
        this.DebugString += " ReplyMatch is false! ";
      this.Successful = num == this.PingBytes.Length && (int) this.PingBytes[this.PingBytes.Length - 1] == (int) this.PingId;
      this.GotResult = true;
      return true;
    }

    public override void Dispose()
    {
      try
      {
        this.sock.Close();
      }
      catch
      {
      }
      this.sock = (Socket) null;
    }
  }
}
