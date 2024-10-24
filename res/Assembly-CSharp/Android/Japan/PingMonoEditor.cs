﻿// Decompiled with JetBrains decompiler
// Type: PingMonoEditor
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using ExitGames.Client.Photon;
using System;
using System.Net.Sockets;
using UnityEngine;

public class PingMonoEditor : PhotonPing
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
      Debug.Log((object) "ReplyMatch is false! ");
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
