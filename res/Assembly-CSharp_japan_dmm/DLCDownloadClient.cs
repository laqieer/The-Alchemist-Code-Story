// Decompiled with JetBrains decompiler
// Type: DLCDownloadClient
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System.Diagnostics;
using UnityEngine;

#nullable disable
public abstract class DLCDownloadClient
{
  protected long _downloadStart;
  protected long _downloadEnd;

  public bool IsDone { get; protected set; }

  public string HasError { get; protected set; }

  public int ContentLength { get; protected set; }

  public bool IsGotRest { get; protected set; }

  public bool CanWriteStream { get; set; }

  public virtual int LoadingSize { get; protected set; }

  public virtual byte[] DataBytes { get; protected set; }

  public virtual int DownloadedSize { get; protected set; }

  public double DownloadTime
  {
    get => (double) (this._downloadEnd - this._downloadStart) / (double) Stopwatch.Frequency;
  }

  public abstract void Download(string url, int size, MonoBehaviour coroutineExecuter = null);

  public abstract void Abort();

  public abstract void Dispose();
}
