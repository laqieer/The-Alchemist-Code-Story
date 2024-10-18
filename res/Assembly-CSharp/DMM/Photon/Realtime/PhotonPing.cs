// Decompiled with JetBrains decompiler
// Type: Photon.Realtime.PhotonPing
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;

#nullable disable
namespace Photon.Realtime
{
  public abstract class PhotonPing : IDisposable
  {
    public string DebugString = string.Empty;
    public bool Successful;
    protected internal bool GotResult;
    protected internal int PingLength = 13;
    protected internal byte[] PingBytes = new byte[13]
    {
      (byte) 125,
      (byte) 125,
      (byte) 125,
      (byte) 125,
      (byte) 125,
      (byte) 125,
      (byte) 125,
      (byte) 125,
      (byte) 125,
      (byte) 125,
      (byte) 125,
      (byte) 125,
      (byte) 0
    };
    protected internal byte PingId;

    public virtual bool StartPing(string ip) => throw new NotImplementedException();

    public virtual bool Done() => throw new NotImplementedException();

    public virtual void Dispose() => throw new NotImplementedException();

    protected internal void Init()
    {
      this.GotResult = false;
      this.Successful = false;
      this.PingId = (byte) (Environment.TickCount % (int) byte.MaxValue);
    }
  }
}
