// Decompiled with JetBrains decompiler
// Type: DownloaderClient
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.Networking;

public class DownloaderClient : DLCDownloadClient
{
  private const float sleepTime = 0.05f;
  private const int timeoutTime = 10;
  private const int bufferSize = 1048576;
  private MonoBehaviour _coroutineExecuter;
  private IEnumerator _downloadEnumerator;
  private IEnumerator _writeStreamEnumerator;
  private UnityWebRequest _request;
  private float _timeOutCount;

  public override int DownloadedSize
  {
    get
    {
      if (this.DataBytes == null)
        return 0;
      return this.DataBytes.Length;
    }
  }

  public override void Download(string url, MonoBehaviour coroutineExecuter)
  {
    this.IsDone = false;
    this.IsGotRest = false;
    this.ContentLength = 0;
    this.LoadingSize = 0;
    this.CanWriteStream = false;
    this.HasError = string.Empty;
    this._coroutineExecuter = coroutineExecuter;
    if (this._downloadEnumerator != null)
      this._coroutineExecuter.StopCoroutine(this._downloadEnumerator);
    this._downloadEnumerator = this._Download(url);
    this._coroutineExecuter.StartCoroutine(this._downloadEnumerator);
  }

  [DebuggerHidden]
  private IEnumerator _Download(string url)
  {
    // ISSUE: object of a compiler-generated type is created
    return (IEnumerator) new DownloaderClient.\u003C_Download\u003Ec__Iterator0()
    {
      url = url,
      \u0024this = this
    };
  }

  public override void Abort()
  {
    Debug.LogWarning((object) "Aborting");
    if (this._downloadEnumerator != null)
      this._coroutineExecuter.StopCoroutine(this._downloadEnumerator);
    this._downloadEnumerator = (IEnumerator) null;
    if (this._writeStreamEnumerator != null)
      this._coroutineExecuter.StopCoroutine(this._writeStreamEnumerator);
    this._writeStreamEnumerator = (IEnumerator) null;
    this.Dispose();
  }

  public override void Dispose()
  {
    this.DataBytes = (byte[]) null;
    this._request = (UnityWebRequest) null;
    if (this._writeStreamEnumerator != null)
    {
      this._coroutineExecuter.StopCoroutine(this._writeStreamEnumerator);
      this._writeStreamEnumerator = (IEnumerator) null;
    }
    if (this._downloadEnumerator != null)
    {
      this._coroutineExecuter.StopCoroutine(this._downloadEnumerator);
      this._downloadEnumerator = (IEnumerator) null;
    }
    this.CanWriteStream = false;
  }

  [DebuggerHidden]
  private IEnumerator WriteMemoryStreamResponse()
  {
    // ISSUE: object of a compiler-generated type is created
    return (IEnumerator) new DownloaderClient.\u003CWriteMemoryStreamResponse\u003Ec__Iterator1()
    {
      \u0024this = this
    };
  }

  private class DLCDownloadHandler : DownloadHandlerScript
  {
    private DownloaderClient loader;
    private List<byte> buf;

    public DLCDownloadHandler(DownloaderClient _loader, byte[] buffer)
      : base(buffer)
    {
      this.loader = _loader;
      this.buf = new List<byte>();
    }

    protected override void ReceiveContentLength(int contentLength)
    {
      if (this.loader.IsGotRest)
        return;
      this.loader.ContentLength = contentLength;
      this.loader.IsGotRest = true;
      this.loader.DataBytes = new byte[contentLength];
    }

    protected override bool ReceiveData(byte[] data, int dataLength)
    {
      this.loader._timeOutCount = 0.0f;
      if (!this.loader.IsGotRest && this.buf != null)
      {
        this.buf.AddRange((IEnumerable<byte>) data);
        return true;
      }
      if (this.buf != null && this.buf.Count > 0)
      {
        this.BufCopy(this.buf.ToArray(), this.buf.Count);
        this.buf.Clear();
        this.buf = (List<byte>) null;
      }
      this.BufCopy(data, dataLength);
      return true;
    }

    private void BufCopy(byte[] bytes, int length)
    {
      if (length + this.loader.LoadingSize > this.loader.DataBytes.Length)
      {
        byte[] numArray = new byte[length + this.loader.LoadingSize];
        Array.Copy((Array) this.loader.DataBytes, 0, (Array) numArray, 0, this.loader.LoadingSize);
        this.loader.DataBytes = numArray;
      }
      Array.Copy((Array) bytes, 0, (Array) this.loader.DataBytes, this.loader.LoadingSize, length);
      this.loader.LoadingSize += length;
    }

    protected override void CompleteContent()
    {
      if (!this.loader.IsGotRest)
      {
        this.loader.ContentLength = this.buf.Count;
        this.loader.LoadingSize = this.buf.Count;
        this.loader.IsGotRest = true;
        this.loader.DataBytes = this.buf.ToArray();
      }
      if (this.buf != null)
      {
        this.buf.Clear();
        this.buf = (List<byte>) null;
      }
      this.loader._downloadEnd = Stopwatch.GetTimestamp();
      this.loader.IsDone = true;
    }
  }
}
