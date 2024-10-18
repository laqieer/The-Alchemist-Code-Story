// Decompiled with JetBrains decompiler
// Type: DLCDownloadClient
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using System.Diagnostics;
using UnityEngine;

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
    get
    {
      return (double) (this._downloadEnd - this._downloadStart) / (double) Stopwatch.Frequency;
    }
  }

  public abstract void Download(string url, MonoBehaviour coroutineExecuter = null);

  public abstract void Abort();

  public abstract void Dispose();
}
